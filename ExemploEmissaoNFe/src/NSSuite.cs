﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

namespace ExemploEmissaoNFe.src
{
    public class NSSuite
    {
        private static string token = "";
        private static Endpoints Endpoints = new Endpoints();

        // Esta funcao envia um conteúdo para uma URL, em requisicoes do tipo POST
        public static string enviaConteudoParaAPI(string conteudo, string url, string tpConteudo)
        {
            string retorno = "";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.Method = "POST";

            httpWebRequest.Headers["X-AUTH-TOKEN"] = token;

            if (tpConteudo == "txt")
            {
                httpWebRequest.ContentType = "text/plain;charset=utf-8";
            }
            else if (tpConteudo == "xml")
            {
                httpWebRequest.ContentType = "application/xml;charset=utf-8";
            }
            else
            {
                httpWebRequest.ContentType = "application/json;charset=utf-8";
            }

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(conteudo);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    retorno = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;

                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        retorno = streamReader.ReadToEnd();
                    }

                    switch (System.Convert.ToInt32(response.StatusCode))
                    {
                        case 401:
                            {
                                MessageBox.Show("Token nao enviado ou invalido");
                                break;
                            }

                        case 403:
                            {
                                MessageBox.Show("Token sem permissao");
                                break;
                            }

                        case 404:
                            {
                                MessageBox.Show("Nao encontrado, verifique o retorno para mais informacoes");
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
            }

            return retorno;
        }

        public static string emitirNFeSincrono(string conteudo, string tpConteudo, string CNPJ, string tpDown, string tpAmb, string caminho, bool exibeNaTela = false, bool a3 = false)
        {
            string statusEnvio, statusConsulta, statusDownload, motivo, nsNRec, chNFe, cStat, nProt;
            string retorno, resposta;
            IList<string> erros = null;
            string modelo = "55";

            statusEnvio = "";
            statusConsulta = "";
            statusDownload = "";
            motivo = "";
            nsNRec = "";
            chNFe = "";
            cStat = "";
            nProt = "";

            Genericos.gravarLinhaLog(modelo, "[EMISSAO_SINCRONA_INICIO]");

            resposta = emitirDocumento(modelo, conteudo, tpConteudo, CNPJ, a3);

            var EmitirRespNFe = JsonConvert.DeserializeObject<EmitirRespNFe>(resposta);
            statusEnvio = EmitirRespNFe.status;

            // Testa se o envio foi feito com sucesso (200) ou se é para reconsultar (-6)
            if ((statusEnvio == "200") || (statusEnvio == "-6") || (statusEnvio == "-7"))
            {
                nsNRec = EmitirRespNFe.nsNRec;

                // É necessário aguardar alguns milisegundos antes de consultar o status de processamento
                Thread.Sleep(400);

                ConsStatusProcessamentoReqNFe ConsStatusProcessamentoReqNFe = new ConsStatusProcessamentoReqNFe()
                {
                    CNPJ = CNPJ,
                    nsNRec = nsNRec,
                    tpAmb = tpAmb
                };

                resposta = consultarStatusProcessamento(modelo, ConsStatusProcessamentoReqNFe);

                var ConsStatusProcessamentoRespNFe = JsonConvert.DeserializeObject<ConsStatusProcessamentoRespNFe>(resposta);
                statusConsulta = ConsStatusProcessamentoRespNFe.status;

                // Testa se a consulta foi feita com sucesso (200)
                if (statusConsulta == "200")
                {
                    cStat = ConsStatusProcessamentoRespNFe.cStat;

                    // Testa se o cStat é igual a 100 ou 150, pois ambos significam "Autorizado"
                    if ((cStat == "100") || (cStat == "150"))
                    {
                        chNFe = ConsStatusProcessamentoRespNFe.chNFe;

                        nProt = ConsStatusProcessamentoRespNFe.nProt;

                        motivo = ConsStatusProcessamentoRespNFe.xMotivo;

                        DownloadReqNFe DownloadReqNFe = new DownloadReqNFe()
                        {
                            chNFe = chNFe,
                            tpAmb = tpAmb,
                            tpDown = tpDown
                        };

                        resposta = downloadDocumentoESalvar(modelo, DownloadReqNFe, caminho, chNFe + "-procNFe", exibeNaTela);

                        var DownloadRespNFe = JsonConvert.DeserializeObject<DownloadRespNFe>(resposta);
                        statusDownload = DownloadRespNFe.status;

                        // Testa se houve problema no download
                        if (!statusDownload.Equals("200"))
                        {
                            motivo = DownloadRespNFe.motivo;
                        }
                    }
                    else
                    {
                        motivo = ConsStatusProcessamentoRespNFe.xMotivo;
                    }

                }
                else if (statusConsulta.Equals("-2"))
                {
                    cStat = ConsStatusProcessamentoRespNFe.cStat;

                    motivo = ConsStatusProcessamentoRespNFe.erro.xMotivo;
                }
                else
                {
                    motivo = ConsStatusProcessamentoRespNFe.motivo;
                }
            }

            else if (statusEnvio.Equals("-4") || statusEnvio.Equals("-2"))
            {
                motivo = EmitirRespNFe.motivo;

                try { erros = EmitirRespNFe.erros; }
                catch (Exception ex ){}
            }

            else if (statusEnvio.Equals("-999") || statusEnvio.Equals("-5"))
            {
                motivo = EmitirRespNFe.erro.xMotivo;
            }
            else
            {
                try { motivo = EmitirRespNFe.motivo; }
                catch { motivo = EmitirRespNFe.ToString(); }
            }

            EmitirSincronoRetNFe EmitirSincronoRetNFe = new EmitirSincronoRetNFe()
            {
                statusEnvio = statusEnvio,
                statusConsulta = statusConsulta,
                statusDownload = statusDownload,
                cStat = cStat,
                chNFe = chNFe,
                nProt = nProt,
                motivo = motivo,
                nsNRec = nsNRec,
                erros = erros
            };

            retorno = JsonConvert.SerializeObject(EmitirSincronoRetNFe);

            Genericos.gravarLinhaLog(modelo, "[JSON_RETORNO]");
            Genericos.gravarLinhaLog(modelo, retorno);
            Genericos.gravarLinhaLog(modelo, "[EMISSAO_SINCRONA_FIM]");

            return retorno;
        }

        // Métodos genéricos, compartilhados entre diversas funções
        public static string emitirDocumento(string modelo, string conteudo, string tpConteudo, string cnpjEmitente, bool a3)
        {
            string urlEnvio;
            string node;

            switch (modelo)
            {
                case "55":
                    {
                        urlEnvio = Endpoints.NFeEnvio;
                        node = "infNFe";
                        break;
                    }

                default:
                    {
                        throw new Exception("Nao definido endpoint de envio para o modelo " + modelo);
                    }
            }

            if (a3)
            {
                string xml;
                try
                {
                    if ("JSON".Equals(tpConteudo.ToUpper()) || "TXT".Equals(tpConteudo.ToUpper()))
                    {
                        string respostaJSON = gerarXMLEmissao(modelo, conteudo, tpConteudo);
                        dynamic nodeJSON = JsonConvert.DeserializeObject(respostaJSON);
                        xml = nodeJSON.xml;
                        tpConteudo = "xml";
                    }
                    else
                    {
                        xml = conteudo;
                    }

                    X509Certificate2 cert = Genericos.buscaCertificado(cnpjEmitente.Trim());
                    if (cert == null)
                    {
                        MessageBox.Show("Certificado Digital nao encontrado");
                        return null;
                    }

                    conteudo = Genericos.assinaXML(xml.Trim(), node, cert);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            Genericos.gravarLinhaLog(modelo, "[ENVIO_DADOS]");
            Genericos.gravarLinhaLog(modelo, conteudo);

            string resposta = enviaConteudoParaAPI(conteudo, urlEnvio, tpConteudo);

            Genericos.gravarLinhaLog(modelo, "[ENVIO_RESPOSTA]");
            Genericos.gravarLinhaLog(modelo, resposta);

            return resposta;
        }

        public static string consultarStatusProcessamento(string modelo, ConsStatusProcessamentoReq ConsStatusProcessamentoReq)
        {
            string urlConsulta;

            switch (modelo)
            {
                case "55":
                    {
                        urlConsulta = Endpoints.NFeConsStatusProcessamento;
                        break;
                    }

                default:
                    {
                        throw new Exception("Nao definido endpoint de consulta para o modelo " + modelo);
                    }
            }

            string json = JsonConvert.SerializeObject(ConsStatusProcessamentoReq);

            Genericos.gravarLinhaLog(modelo, "[CONSULTA_DADOS]");
            Genericos.gravarLinhaLog(modelo, json);

            string resposta = enviaConteudoParaAPI(json, urlConsulta, "json");

            Genericos.gravarLinhaLog(modelo, "[CONSULTA_RESPOSTA]");
            Genericos.gravarLinhaLog(modelo, resposta);

            return resposta;
        }

        public static string downloadDocumento(string modelo, DownloadReq DownloadReq)
        {
            string urlDownload;

            switch (modelo)
            {
                case "55":
                    {
                        urlDownload = Endpoints.NFeDownload;
                        break;
                    }

                default:
                    {
                        throw new Exception("Nao definido endpoint de download para o modelo " + modelo);
                    }
            }

            string json = JsonConvert.SerializeObject(DownloadReq);

            Genericos.gravarLinhaLog(modelo, "[DOWNLOAD_DADOS]");
            Genericos.gravarLinhaLog(modelo, json);

            string resposta = enviaConteudoParaAPI(json, urlDownload, "json");

            string status;

            DownloadResp DownloadResp = new DownloadResp();
            DownloadResp = JsonConvert.DeserializeObject<DownloadResp>(resposta);
            status = DownloadResp.status;

            // O retorno da API será gravado somente em caso de erro, 
            // para não gerar um log extenso com o PDF e XML
            if (!status.Equals("200") & !status.Equals("100"))
            {
                Genericos.gravarLinhaLog(modelo, "[DOWNLOAD_RESPOSTA]");
                Genericos.gravarLinhaLog(modelo, resposta);
            }
            else
            {
                Genericos.gravarLinhaLog(modelo, "[DOWNLOAD_STATUS]");
                Genericos.gravarLinhaLog(modelo, status);
            }

            return resposta;
        }

        public static string downloadDocumentoESalvar(string modelo, DownloadReq DownloadReq, string caminho, string nome, bool exibeNaTela = false)
        {
            string resposta = downloadDocumento(modelo, DownloadReq);
            DownloadResp DownloadResp = new DownloadResp();

            string status;

            DownloadResp = JsonConvert.DeserializeObject<DownloadResp>(resposta);
            status = DownloadResp.status;
   
            if (status.Equals("200") || status.Equals("100"))
            {
                // Cria o diretório, caso não exista
                try
                {
                    if (!Directory.Exists(caminho)) Directory.CreateDirectory(caminho);
                    if (!caminho.EndsWith(@"\")) caminho += @"\";
                }

                catch (IOException ex)
                {
                    Genericos.gravarLinhaLog(modelo, "[CRIAR_DIRETORIO]" + caminho);
                    Genericos.gravarLinhaLog(modelo, ex.Message);
                    throw new Exception("Erro: " + ex.Message);
                }

                // Verifica quais arquivos deve salvar
                if (DownloadReq.tpDown.ToUpper().Contains("X"))
                {
                    string xml = DownloadResp.xml;
                    Genericos.salvarXML(xml, caminho, nome);
                }

                if (DownloadReq.tpDown.ToUpper().Contains("P"))
                {
                    string pdf = DownloadResp.pdf;
                    Genericos.salvarPDF(pdf, caminho, nome);

                    if (exibeNaTela) Process.Start(caminho + nome + ".pdf");
                }
            }

            else
            {
                MessageBox.Show("Ocorreu um erro, veja o retorno da API para mais informações");
            }

            return resposta;
        }

        public static string downloadEvento(string modelo, DownloadEventoReq DownloadEventoReq)
        {
            string urlDownloadEvento;

            switch (modelo)
            {
                case "55":
                    {
                        urlDownloadEvento = Endpoints.NFeDownloadEvento;
                        break;
                    }

                default:
                    {
                        throw new Exception("Nao definido endpoint de download de evento para o modelo " + modelo);
                    }
            }

            string json = JsonConvert.SerializeObject(DownloadEventoReq);

            Genericos.gravarLinhaLog(modelo, "[DOWNLOAD_EVENTO_DADOS]");
            Genericos.gravarLinhaLog(modelo, json);

            string resposta = enviaConteudoParaAPI(json, urlDownloadEvento, "json");

            string status;

            DownloadEventoResp DownloadEventoResp = new DownloadEventoResp();
            DownloadEventoResp = JsonConvert.DeserializeObject<DownloadEventoResp>(resposta);
            status = DownloadEventoResp.status;

            // O retorno da API será gravado somente em caso de erro, 
            // para não gerar um log extenso com o PDF e XML
            if (!status.Equals("200") && !status.Equals("100"))
            {
                Genericos.gravarLinhaLog(modelo, "[DOWNLOAD_EVENTO_RESPOSTA]");
                Genericos.gravarLinhaLog(modelo, resposta);
            }
            else
            {
                Genericos.gravarLinhaLog(modelo, "[DOWNLOAD_EVENTO_STATUS]");
                Genericos.gravarLinhaLog(modelo, status);
            }

            return resposta;
        }

        public static void downloadEventoESalvar(string modelo, DownloadEventoReq DownloadEventoReq, string caminho, string chave, string nSeqEvento, bool exibeNaTela = false)
        {
            string resposta = downloadEvento(modelo, DownloadEventoReq);
            string tpEventoSalvar = "";
            DownloadEventoResp DownloadEventoResp = new DownloadEventoResp();

            string status;

            DownloadEventoResp = JsonConvert.DeserializeObject<DownloadEventoResp>(resposta);
            status = DownloadEventoResp.status;


            if (status.Equals("200") || status.Equals("100"))
            {
                // Cria o diretório, caso não exista
                try
                {
                    if (!Directory.Exists(caminho)) Directory.CreateDirectory(caminho);
                    if (!caminho.EndsWith(@"\")) caminho += @"\";
                }
                catch (IOException ex)
                {
                    Genericos.gravarLinhaLog(modelo, "[CRIAR_DIRETORIO]" + caminho);
                    Genericos.gravarLinhaLog(modelo, ex.Message);
                    throw new Exception("Erro: " + ex.Message);
                }

                if (DownloadEventoReq.tpEvento.ToUpper().Equals("CANC"))
                    tpEventoSalvar = "110111";

                else if (DownloadEventoReq.tpEvento.ToUpper().Equals("ENC"))
                    tpEventoSalvar = "110110";

                else
                    tpEventoSalvar = "110115";

                // Verifica quais arquivos deve salvar
                if (DownloadEventoReq.tpDown.ToUpper().Contains("X"))
                {
                    string xml = DownloadEventoResp.xml;
                    Genericos.salvarXML(xml, caminho, tpEventoSalvar + chave + nSeqEvento + "-procEven");
                }

                if (DownloadEventoReq.tpDown.ToUpper().Contains("P"))
                {
                    string pdf = DownloadEventoResp.pdf;

                    if ((pdf != null) && (pdf != ""))
                    {
                        Genericos.salvarPDF(pdf, caminho, tpEventoSalvar + chave + nSeqEvento + "-procEven");
                        if (exibeNaTela) Process.Start(caminho + tpEventoSalvar + chave + nSeqEvento + "-procEven.pdf");
                    }
                }

            }
            else
                MessageBox.Show("Ocorreu um erro, veja o retorno da API para mais informações");
        }

        public static string cancelarDocumento(string modelo, CancelarReq CancelarReq, string cnpjEmitente, bool a3)
        {
            string urlCancelamento;
            string node = "infEvento";

            switch (modelo)
            {
                case "55":
                    {
                        urlCancelamento = Endpoints.NFeCancelamento;
                        break;
                    }

                default:
                    {
                        throw new Exception("Nao definido endpoint de cancelamento para o modelo " + modelo);
                    }
            }

            string conteudo = JsonConvert.SerializeObject(CancelarReq);

            if (a3)
            {
                string xml;
                try
                {
                    string respostaJSON = gerarXMLCancelamento(modelo, conteudo, "json");
                    dynamic nodeJSON = JsonConvert.DeserializeObject(respostaJSON);
                    xml = nodeJSON.xml;

                    X509Certificate2 cert = Genericos.buscaCertificado(cnpjEmitente.Trim());
                    if (cert == null)
                    {
                        MessageBox.Show("Certificado Digital nao encontrado");
                        return null;
                    }

                    conteudo = Genericos.assinaXML(xml.Trim(), node, cert);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            Genericos.gravarLinhaLog(modelo, "[CANCELAMENTO_DADOS]");
            Genericos.gravarLinhaLog(modelo, conteudo);

            string resposta = enviaConteudoParaAPI(conteudo, urlCancelamento, "json");

            Genericos.gravarLinhaLog(modelo, "[CANCELAMENTO_RESPOSTA]");
            Genericos.gravarLinhaLog(modelo, resposta);

            return resposta;
        }

        public static string previaDocumento(string modelo, string tpConteudo, string conteudo)
        {
            string urlPrevia;
            switch (modelo)
            {
                case "55":
                    {
                        urlPrevia = Endpoints.NFePrevia;
                        break;
                    }

                default:
                    {
                        throw new Exception("Nao definido endpoint de cancelamento para o modelo " + modelo);
                    }
            }

            Genericos.gravarLinhaLog(modelo, "[PREVIA_DADOS]");
            Genericos.gravarLinhaLog(modelo, conteudo);

            string resposta = enviaConteudoParaAPI(conteudo, urlPrevia, tpConteudo);

            Genericos.gravarLinhaLog(modelo, "[PREVIA_RESPOSTA]");
            Genericos.gravarLinhaLog(modelo, resposta);

            PreviaResp previaResp = JsonConvert.DeserializeObject<PreviaResp>(resposta);

            if (!previaResp.status.Equals("200"))
            {
                string motivo = previaResp.motivo;
                var erros = previaResp.erros;
                MessageBox.Show($"{motivo}, o(s) erro(s): {erros}");
            }

            return previaResp.pdf;
        }

        public static string cancelarDocumentoESalvar(string modelo, CancelarReq CancelarReq, DownloadEventoReq DownloadEventoReq, string caminho, string chave, string cnpjEmitente, bool exibeNaTela = false, bool a3 = false)
        {
            string resposta = cancelarDocumento(modelo, CancelarReq, cnpjEmitente, a3);

            CancelarResp CancelarResp = new CancelarResp();
            string status;
            string cStat;

            CancelarResp = JsonConvert.DeserializeObject<CancelarResp>(resposta);
            status = CancelarResp.status;

            if (status.Equals("200") || status.Equals("135"))
            {
                cStat = CancelarResp.retEvento.cStat;
                if (cStat.Equals("135")) downloadEventoESalvar(modelo, DownloadEventoReq, caminho, chave, "1", exibeNaTela);
            }
            else
            {
                MessageBox.Show("Ocorreu um erro ao cancelar, veja o retorno da API para mais informacoes");
            }

            return resposta;
        }

        public static string corrigirDocumento(string modelo, CorrigirReq CorrigirReq, string cnpjEmitente, bool a3)
        {
            string urlCCe;
            string node = "infEvento";

            switch (modelo)
            {
                case "55":
                    {
                        urlCCe = Endpoints.NFeCCe;
                        break;
                    }

                default:
                    {
                        throw new Exception("Nao definido endpoint de carta de correção para o modelo " + modelo);
                    }
            }

            string conteudo = JsonConvert.SerializeObject(CorrigirReq);

            if (a3)
            {
                string xml;
                try
                {
                    string respostaJSON = gerarXMLCorrecao(modelo, conteudo, "json");
                    dynamic nodeJSON = JsonConvert.DeserializeObject(respostaJSON);
                    xml = nodeJSON.xml;

                    X509Certificate2 cert = Genericos.buscaCertificado(cnpjEmitente.Trim());
                    if (cert == null)
                    {
                        MessageBox.Show("Certificado Digital nao encontrado");
                        return null;
                    }

                    conteudo = Genericos.assinaXML(xml.Trim(), node, cert);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            Genericos.gravarLinhaLog(modelo, "[CCE_DADOS]");
            Genericos.gravarLinhaLog(modelo, conteudo);

            string resposta = enviaConteudoParaAPI(conteudo, urlCCe, "json");

            Genericos.gravarLinhaLog(modelo, "[CCE_RESPOSTA]");
            Genericos.gravarLinhaLog(modelo, resposta);

            return resposta;
        }

        public static string corrigirDocumentoESalvar(string modelo, CorrigirReq CorrigirReq, DownloadEventoReq DownloadEventoReq, string caminho, string chave, string nSeqEvento, string cnpjEmitente, bool exibeNaTela = false, bool a3 = false)
        {
            string resposta = corrigirDocumento(modelo, CorrigirReq, cnpjEmitente, a3);
            CorrigirResp CorrigirResp = new CorrigirResp();
            string cStat;

            CorrigirResp = JsonConvert.DeserializeObject<CorrigirResp>(resposta);
            cStat = CorrigirResp.retEvento.cStat;

            if (cStat.Equals("135"))
            {
                downloadEventoESalvar(modelo, DownloadEventoReq, caminho, chave, nSeqEvento, exibeNaTela);
            }
            else
            {
                MessageBox.Show("Ocorreu um erro ao corrigir, veja o retorno da API para mais informacoes");
            }

            return resposta;
        }

        public static string inutilizarNumeracao(string modelo, InutilizarReq InutilizarReq, string cnpjEmitente, bool a3)
        {
            string urlInutilizacao;
            string node = "infInut";

            switch (modelo)
            {
                case "55":
                    {
                        urlInutilizacao = Endpoints.NFeInutilizacao;
                        break;
                    }

                default:
                    {
                        throw new Exception("Nao definido endpoint de inutilizacao para o modelo " + modelo);
                    }
            }

            string conteudo = JsonConvert.SerializeObject(InutilizarReq);

            if (a3)
            {
                string xml;

                try
                {
                    string respostaJSON = gerarXMLInutilizacao(modelo, conteudo, "json");
                    dynamic nodeJSON = JsonConvert.DeserializeObject(respostaJSON);
                    xml = nodeJSON.xml;

                    X509Certificate2 cert = Genericos.buscaCertificado(cnpjEmitente.Trim());
                    if (cert == null)
                    {
                        MessageBox.Show("Certificado Digital não encontrado");
                        return null;
                    }

                    conteudo = Genericos.assinaXML(xml.Trim(), node, cert);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            Genericos.gravarLinhaLog(modelo, "[INUTILIZACAO_DADOS]");
            Genericos.gravarLinhaLog(modelo, conteudo);

            string resposta = enviaConteudoParaAPI(conteudo, urlInutilizacao, "json");

            Genericos.gravarLinhaLog(modelo, "[INUTILIZACAO_RESPOSTA]");
            Genericos.gravarLinhaLog(modelo, resposta);

            return resposta;
        }

        public static string inutilizarNumeracaoESalvar(string modelo, InutilizarReq InutilizarReq, string caminho, string cnpjEmitente, bool a3 = false)
        {
            string resposta = inutilizarNumeracao(modelo, InutilizarReq, cnpjEmitente, a3);
            string status;
            string xml;
            string chave;

            xml = null;
            chave = null;

            switch (modelo)
            {
                case "55":
                    {
                        InutilizarRespNFe InutilizarRespNFe = new InutilizarRespNFe();
                        InutilizarRespNFe = JsonConvert.DeserializeObject<InutilizarRespNFe>(resposta);
                        status = InutilizarRespNFe.status;

                        if (status.Equals("200"))
                        {
                            string cStat = InutilizarRespNFe.retornoInutNFe.cStat;
                            if (cStat.Equals("102"))
                            {
                                xml = InutilizarRespNFe.retornoInutNFe.xmlInut;
                                chave = InutilizarRespNFe.retornoInutNFe.chave;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ocorreu um erro ao inutilizar a numeracao, veja o retorno da API para mais informacoes");
                        }
                        break;
                    }

                default:
                    {
                        throw new Exception("Nao existe para este modelo inutilizacao " + modelo);
                    }

            }

            if (!string.IsNullOrEmpty(xml))
            {
                if (!Directory.Exists(caminho)) Directory.CreateDirectory(caminho);
                Genericos.salvarXML(xml, caminho, chave);
            }

            return resposta;
        }

        public static string consultarCadastroContribuinte(string modelo, ConsCadReq ConsCadReq)
        {
            string urlConsCad;

            switch (modelo)
            {
                case "55":
                    {
                        urlConsCad = Endpoints.NFeConsCad;
                        break;
                    }

                default:
                    {
                        throw new Exception("Não definido endpoint de consulta de cadastro para o modelo " + modelo);
                    }
            }

            string json = JsonConvert.SerializeObject(ConsCadReq, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            Genericos.gravarLinhaLog(modelo, "[CONS_CAD_DADOS]");
            Genericos.gravarLinhaLog(modelo, json);

            string resposta = enviaConteudoParaAPI(json, urlConsCad, "json");

            Genericos.gravarLinhaLog(modelo, "[CONS_CAD_RESPOSTA]");
            Genericos.gravarLinhaLog(modelo, resposta);

            return resposta;
        }

        public static string consultarSituacaoDocumento(string modelo, ConsSitReq ConsSitReq)
        {
            string urlConsSit;

            switch (modelo)
            {
                case "55":
                    {
                        urlConsSit = Endpoints.NFeConsSit;
                        break;
                    }

                default:
                    {
                        throw new Exception("Não definido endpoint de consulta de situação para o modelo " + modelo);
                    }
            }

            string json = JsonConvert.SerializeObject(ConsSitReq);

            Genericos.gravarLinhaLog(modelo, "[CONS_SIT_DADOS]");
            Genericos.gravarLinhaLog(modelo, json);

            string resposta = enviaConteudoParaAPI(json, urlConsSit, "json");

            Genericos.gravarLinhaLog(modelo, "[CONS_SIT_RESPOSTA]");
            Genericos.gravarLinhaLog(modelo, resposta);

            return resposta;
        }

        public static string listarNSNRecs(string modelo, ListarNSNRecReq ListarNSNRecReq)
        {
            string urlListarNSNRecs;

            switch (modelo)
            {
                case "55":
                    {
                        urlListarNSNRecs = Endpoints.NFeListarNSNRecs;
                        break;
                    }

                default:
                    {
                        throw new Exception("Nao definido endpoint de listagem de nsNRec para o modelo " + modelo);
                    }
            }

            string json = JsonConvert.SerializeObject(ListarNSNRecReq);

            Genericos.gravarLinhaLog(modelo, "[LISTAR_NSNRECS_DADOS]");
            Genericos.gravarLinhaLog(modelo, json);

            string resposta = enviaConteudoParaAPI(json, urlListarNSNRecs, "json");

            Genericos.gravarLinhaLog(modelo, "[LISTAR_NSNRECS_RESPOSTA]");
            Genericos.gravarLinhaLog(modelo, resposta);

            return resposta;
        }

        public static string enviarEmailDocumento(string modelo, EnviarEmailReq EnviarEmailReq)
        {
            string urlEnviarEmail;

            switch (modelo)
            {
                case "55":
                    {
                        urlEnviarEmail = Endpoints.NFeEnvioEmail;
                        break;
                    }

                default:
                    {
                        throw new Exception("Nao definido endpoint de envio de e-mail para o modelo " + modelo);
                    }
            }

            string json = JsonConvert.SerializeObject(EnviarEmailReq);

            Genericos.gravarLinhaLog(modelo, "[ENVIAR_EMAIL_DADOS]");
            Genericos.gravarLinhaLog(modelo, json);

            string resposta = enviaConteudoParaAPI(json, urlEnviarEmail, "json");

            Genericos.gravarLinhaLog(modelo, "[ENVIAR_EMAIL_RESPOSTA]");
            Genericos.gravarLinhaLog(modelo, resposta);

            return resposta;
        }

        public static string gerarPDFDeXML(string modelo, GerarPDFDeXMLReq gerarPDFReq)
        {
            string urlGerarPDF;
            switch (modelo)
            {
                case "55":
                    {
                        urlGerarPDF = Endpoints.NFeGerarPDFDeXML;
                        break;
                    }
                default:
                    {
                        throw new Exception("Nao definido endpoint de geracao de PDF a partir de XML processado para o modelo " + modelo);
                    }
            }

            string json = JsonConvert.SerializeObject(gerarPDFReq);

            Genericos.gravarLinhaLog(modelo, "[GERACAO_PDF_DADOS]");
            Genericos.gravarLinhaLog(modelo, json);

            string resposta = enviaConteudoParaAPI(json, urlGerarPDF, "json");

            Genericos.gravarLinhaLog(modelo, "[GERACAO_PDF_RESPOSTA]");
            Genericos.gravarLinhaLog(modelo, resposta);
            return resposta;
        }

        public static string gerarXMLEmissao(string modelo, string conteudo, string tpConteudo)
        {
            string urlGerarXML;

            switch (modelo)
            {
                case "55":
                    {
                        urlGerarXML = Endpoints.NFeGerarXMLEmissao;
                        break;
                    }

                default:
                    {
                        throw new Exception("Nao definido endpoint de geracao de XML de emissao para o modelo " + modelo);
                    }
            }
            Genericos.gravarLinhaLog(modelo, "[GERACAO_XML_EMISSAO_DADOS]");
            Genericos.gravarLinhaLog(modelo, conteudo);

            string resposta = enviaConteudoParaAPI(conteudo, urlGerarXML, tpConteudo);

            Genericos.gravarLinhaLog(modelo, "[GERACAO_XML_EMISSAO_RESPOSTA]");
            Genericos.gravarLinhaLog(modelo, resposta);
            return resposta;
        }

        public static string gerarXMLCancelamento(string modelo, string conteudo, string tpConteudo)
        {
            string urlGerarXML;

            switch (modelo)
            {
                case "55":
                    {
                        urlGerarXML = Endpoints.NFeGerarXMLCancelamento;
                        break;
                    }

                default:
                    {
                        throw new Exception("Nao definido endpoint de gerar XML de cancelamento para o modelo " + modelo);
                    }
            }
            Genericos.gravarLinhaLog(modelo, "[GERACAO_XML_CANCELAMENTO_DADOS]");
            Genericos.gravarLinhaLog(modelo, conteudo);

            string resposta = enviaConteudoParaAPI(conteudo, urlGerarXML, tpConteudo);

            Genericos.gravarLinhaLog(modelo, "[GERACAO_XML_CANCELAMENTO_RESPOSTA]");
            Genericos.gravarLinhaLog(modelo, resposta);
            return resposta;
        }

        public static string gerarXMLCorrecao(string modelo, string conteudo, string tpConteudo)
        {
            string urlGerarXML;

            switch (modelo)
            {
                case "55":
                    {
                        urlGerarXML = Endpoints.NFeGerarXMLCorrecao;
                        break;
                    }

                default:
                    {
                        throw new Exception("Nao definido endpoint de gerar XML de correcao para o modelo " + modelo);
                    }
            }
            Genericos.gravarLinhaLog(modelo, "[GERACAO_XML_CORRECAO_DADOS]");
            Genericos.gravarLinhaLog(modelo, conteudo);

            string resposta = enviaConteudoParaAPI(conteudo, urlGerarXML, tpConteudo);

            Genericos.gravarLinhaLog(modelo, "[GERACAO_XML_CORRECAO_RESPOSTA]");
            Genericos.gravarLinhaLog(modelo, resposta);
            return resposta;
        }

        public static string gerarXMLInutilizacao(string modelo, string conteudo, string tpConteudo)
        {
            string urlGerarXML;

            switch (modelo)
            {
                case "55":
                    {
                        urlGerarXML = Endpoints.NFeGerarXMLInut;
                        break;
                    }

                default:
                    {
                        throw new Exception("Nao definido endpoint de gerar XML de inutilizacao para o modelo " + modelo);
                    }
            }
            Genericos.gravarLinhaLog(modelo, "[GERACAO_XML_INUTILIZACAO_DADOS]");
            Genericos.gravarLinhaLog(modelo, conteudo);

            string resposta = enviaConteudoParaAPI(conteudo, urlGerarXML, tpConteudo);

            Genericos.gravarLinhaLog(modelo, "[GERACAO_XML_INUTILIZACAO_RESPOSTA]");
            Genericos.gravarLinhaLog(modelo, resposta);
            return resposta;
        }
    }
}
