using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExemploEmissaoNFe.src;
using ExemploEmissaoNFe.src.Classes.NFe;

namespace ExemploEmissaoNFe
{
    public partial class frmNFe : Form
    {
        
        public frmNFe()
        {
            InitializeComponent();
        }

        private void btnEnviarNota_Click(object sender, EventArgs e)
        {
            nfe_v4_00 notaFiscal = new nfe_v4_00();
            notaFiscal.infNFe = new TNFeInfNFe();
            notaFiscal.infNFe.versao = "4.00";
            notaFiscal.infNFe.ide = new TNFeInfNFeIde();
            notaFiscal.infNFe.ide.cUF = (TCodUfIBGE)Enum.Parse(typeof(TCodUfIBGE), "Item" + 43);
            notaFiscal.infNFe.ide.cNF = "00462186";
            notaFiscal.infNFe.ide.natOp = "VENDA A PRAZO - S";
            notaFiscal.infNFe.ide.mod = (TMod)Enum.Parse(typeof(TMod), "Item" + 55);
            notaFiscal.infNFe.ide.serie = "6";
            notaFiscal.infNFe.ide.nNF = "150";
            notaFiscal.infNFe.ide.dhEmi = DateTime.Now.ToString("s") + "-03:00";
            notaFiscal.infNFe.ide.dhSaiEnt = "";
            //Tipo de Operação - 0=Entrada;1 = Saída
            notaFiscal.infNFe.ide.tpNF = (TNFeInfNFeIdeTpNF)Enum.Parse(typeof(TNFeInfNFeIdeTpNF), "Item" + 1);
            //Identificador de local de destino da operação - 1=Operação interna; 2=Operação interestadual; 3=Operação com exterior
            notaFiscal.infNFe.ide.idDest = (TNFeInfNFeIdeIdDest)Enum.Parse(typeof(TNFeInfNFeIdeIdDest), "Item" + 1);
            notaFiscal.infNFe.ide.cMunFG = "4305108";
            //Formato de Impressão do DANFE - 1=DANFE normal, Retrato; 2=DANFE normal, Paisagem;
            notaFiscal.infNFe.ide.tpImp = (TNFeInfNFeIdeTpImp)Enum.Parse(typeof(TNFeInfNFeIdeTpImp), "Item" + 1);
            //Tipo de Emissão da NF-e
            notaFiscal.infNFe.ide.tpEmis = (TNFeInfNFeIdeTpEmis)Enum.Parse(typeof(TNFeInfNFeIdeTpEmis), "Item" + 1);
            notaFiscal.infNFe.ide.cDV = "";
            //Identificação do Ambiente
            notaFiscal.infNFe.ide.tpAmb = (TAmb)Enum.Parse(typeof(TAmb), "Item" + 2);
            //Tipo de Emissão da NF-e - 1=NF-e normal; 2=NF-e complementar; 3=NF-e de ajuste; 4=Devolução de mercadoria.
            notaFiscal.infNFe.ide.finNFe = (TFinNFe)Enum.Parse(typeof(TFinNFe), "Item" + 1);
            //Indica operação com Consumidor final - 0=Normal; 1 = Consumidor final;
            notaFiscal.infNFe.ide.indFinal = (TNFeInfNFeIdeIndFinal)Enum.Parse(typeof(TNFeInfNFeIdeIndFinal), "Item" + 0);
            //Tipo de Emissão da NF-e - 0=Não se aplica; 1=Operação presencial; 2=Operação não presencial, pela Internet; 3=Operação não presencial, Teleatendimento; 4=NFC-e em operação com entrega a domicílio; 5=Operação presencial, fora do estabelecimento; 9=Operação não presencial, outros
            notaFiscal.infNFe.ide.indPres = (TNFeInfNFeIdeIndPres)Enum.Parse(typeof(TNFeInfNFeIdeIndPres), "Item" + 0);
            //Indicador de intermediador/marketplace
            //notaFiscal.infNFe.ide.indIntermed = (TNFeInfNFeIdeIndIntermed)Enum.Parse(typeof(TNFeInfNFeIdeIndIntermed), "Item" + 0);
            //Processo de emissão da NF-e
            notaFiscal.infNFe.ide.procEmi = (TProcEmi)Enum.Parse(typeof(TProcEmi), "Item" + 0);
            //DADOS EMITENTE
            notaFiscal.infNFe.emit = new TNFeInfNFeEmit();
            notaFiscal.infNFe.emit.ItemElementName = ItemChoiceType2.CNPJ;
            notaFiscal.infNFe.emit.Item = "07364617000135";
            notaFiscal.infNFe.emit.IE = "0170108708";
            notaFiscal.infNFe.emit.xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
            notaFiscal.infNFe.emit.xFant = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO";
            notaFiscal.infNFe.emit.CRT = (TNFeInfNFeEmitCRT)Enum.Parse(typeof(TNFeInfNFeEmitCRT), "Item" + 1);
            //DADOS ENDERECO EMITENTE
            notaFiscal.infNFe.emit.enderEmit = new TEnderEmi();
            notaFiscal.infNFe.emit.enderEmit.xLgr = "Rua Bento Osvaldo Trisch,777";
            notaFiscal.infNFe.emit.enderEmit.nro = ".";
            notaFiscal.infNFe.emit.enderEmit.xCpl = "CX POSTAL 91";
            notaFiscal.infNFe.emit.enderEmit.xBairro = "Pendancino";
            notaFiscal.infNFe.emit.enderEmit.cMun = "4303509";
            notaFiscal.infNFe.emit.enderEmit.xMun = "Caxias do Sul";
            notaFiscal.infNFe.emit.enderEmit.UF = (TUfEmi)Enum.Parse(typeof(TUfEmi), "RS");
            notaFiscal.infNFe.emit.enderEmit.xMun = "Caxias do Sul";
            notaFiscal.infNFe.emit.enderEmit.fone = "005432200200";
            //DADOS DESTINATARIO
            notaFiscal.infNFe.dest = new TNFeInfNFeDest();
            //na variavel cnpj_cpf define se o que esta vindo do banco de dados é um cnpj ou cpf
            var cnpj_cpf = "012.294.520-44";
            cnpj_cpf = cnpj_cpf.Trim();
            cnpj_cpf = cnpj_cpf.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
            if (cnpj_cpf.Length != 11)
            {
                notaFiscal.infNFe.dest.ItemElementName = ItemChoiceType3.CNPJ;
                notaFiscal.infNFe.dest.Item = cnpj_cpf;
            }
            else
            {
                notaFiscal.infNFe.dest.ItemElementName = ItemChoiceType3.CPF;
                notaFiscal.infNFe.dest.Item = cnpj_cpf;
            }
            notaFiscal.infNFe.dest.IE = "0170108708";
            notaFiscal.infNFe.dest.indIEDest = TNFeInfNFeDestIndIEDest.Item9;
            notaFiscal.infNFe.dest.xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
            //DADOS ENDERECO EMITENTE
            notaFiscal.infNFe.dest.enderDest = new TEndereco();
            notaFiscal.infNFe.dest.enderDest.xLgr = "Rua Bento Osvaldo Trisch,777";
            notaFiscal.infNFe.dest.enderDest.nro = ".";
            notaFiscal.infNFe.dest.enderDest.xCpl = "CX POSTAL 91";
            notaFiscal.infNFe.dest.enderDest.xBairro = "Pendancino";
            notaFiscal.infNFe.dest.enderDest.cMun = "4303509";
            notaFiscal.infNFe.dest.enderDest.xMun = "Caxias do Sul";
            notaFiscal.infNFe.dest.enderDest.UF = (TUf)Enum.Parse(typeof(TUf), "RS");
            notaFiscal.infNFe.dest.enderDest.xMun = "Caxias do Sul";
            notaFiscal.infNFe.dest.enderDest.fone = "005432200200";

            //ITENS DA NOTA

            //AQUI É A LISTA DE ITENS QUE RETORNARIA DO BANDO DE DADOS
            List<ProdutosBancoDados> prodBanco = GetProdutos();
            notaFiscal.infNFe.det = new TNFeInfNFeDet[prodBanco.Count()];

            int indice = 0;
            foreach (var produto in prodBanco)
            {
                //DADOS PRODUTO-----------------------------------------------
                notaFiscal.infNFe.det[indice] = new TNFeInfNFeDet();
                notaFiscal.infNFe.det[indice].nItem = produto.IdProd.ToString();
                notaFiscal.infNFe.det[indice].prod = new TNFeInfNFeDetProd();
                notaFiscal.infNFe.det[indice].prod.cProd = produto.cProd;
                notaFiscal.infNFe.det[indice].prod.cEAN = produto.cEAN;
                notaFiscal.infNFe.det[indice].prod.xProd = produto.xProd;

                //DADOS IMPOSTO---------------------------------------
                notaFiscal.infNFe.det[indice].imposto = new TNFeInfNFeDetImposto();
                notaFiscal.infNFe.det[indice].imposto.vTotTrib = "0.00";

                //DADOS ICMS----------------------------------------------------------
                notaFiscal.infNFe.det[indice].imposto.Items = new TNFeInfNFeDetImpostoICMS[1];
                var impICMS = new TNFeInfNFeDetImpostoICMS();
                object detIcms = null;
                //dynamic icmsAux;
                //aqui é o cst de icms que vem do banco de dados para validar qual icms utilizar
                var icms_cst = produto.ICMS_CST;
                if (!string.IsNullOrWhiteSpace(icms_cst))
                {
                    switch (produto.ICMS_CST)
                    {
                        case "00":
                            detIcms = new TNFeInfNFeDetImpostoICMSICMS00();
                            var icms00Aux = (TNFeInfNFeDetImpostoICMSICMS00)detIcms;
                            icms00Aux.orig = (Torig)Enum.Parse(typeof(Torig), "Item" + produto.OrigemMercadoria);
                            icms00Aux.modBC = TNFeInfNFeDetImpostoICMSICMS00ModBC.Item0;
                            icms00Aux.vBC = "0.00";
                            icms00Aux.pICMS = "0.00";
                            icms00Aux.vICMS = "0.00";
                            //os campos de FCP(Fundo de combate a pobreza pode ser validado para ser criado no xml somente se tiver o campo preenchido no banco de dados do cliente)
                            //pFCP = "0.00";
                            //vFCP = "0.00";
                            impICMS.Item = icms00Aux;
                            break;
                        case "10":
                            detIcms = new TNFeInfNFeDetImpostoICMSICMS10();
                            var icms10Aux = (TNFeInfNFeDetImpostoICMSICMS10)detIcms;
                            icms10Aux.orig = (Torig)Enum.Parse(typeof(Torig), "Item" + produto.OrigemMercadoria);
                            icms10Aux.CST = TNFeInfNFeDetImpostoICMSICMS10CST.Item10;
                            icms10Aux.modBC = TNFeInfNFeDetImpostoICMSICMS10ModBC.Item0;
                            icms10Aux.vBC = "0.00";
                            icms10Aux.pICMS = "0.00";
                            icms10Aux.vICMS = "0.00";
                            //os campos de FCP(Fundo de combate a pobreza pode ser validado para ser criado no xml somente se tiver o campo preenchido no banco de dados do cliente)
                            //icms10Aux.vBCFCP = "0.00";
                            //icms10Aux.pFCP = "0.00";
                            //icms10Aux.vFCP = "0.00";
                            icms10Aux.modBCST = TNFeInfNFeDetImpostoICMSICMS10ModBCST.Item0;
                            icms10Aux.pMVAST = "0.00";
                            icms10Aux.pRedBCST = "0.00";
                            icms10Aux.vBCST = "0.00";
                            icms10Aux.pICMSST = "0.00";
                            icms10Aux.vICMSST = "0.00";
                            //icms10Aux.vBCFCP = "0.00";
                            //icms10Aux.pFCP = "0.00";
                            //icms10Aux.vFCP = "0.00";
                            impICMS.Item = icms10Aux;
                            break;
                        case "20":
                            detIcms = new TNFeInfNFeDetImpostoICMSICMS20();
                            var icms20Aux = (TNFeInfNFeDetImpostoICMSICMS20)detIcms;
                            icms20Aux.orig = (Torig)Enum.Parse(typeof(Torig), "Item" + produto.OrigemMercadoria);
                            icms20Aux.CST = TNFeInfNFeDetImpostoICMSICMS20CST.Item20;
                            icms20Aux.modBC = TNFeInfNFeDetImpostoICMSICMS20ModBC.Item0;
                            icms20Aux.pRedBC = "0.00";
                            icms20Aux.vBC = "0.00";
                            icms20Aux.pICMS = "0.00";
                            icms20Aux.vICMS = "0.00";
                            //icms10Aux.vBCFCP = "0.00";
                            //icms10Aux.pFCP = "0.00";
                            //icms10Aux.vFCP = "0.00";
                            //icms20Aux.vICMSDeson = "0.00";
                            //icms20Aux.motDesICMS = TNFeInfNFeDetImpostoICMSICMS20MotDesICMS.Item9;
                            impICMS.Item = icms20Aux;
                            break;
                        case "30":
                            detIcms = new TNFeInfNFeDetImpostoICMSICMS30();
                            var icms30Aux = (TNFeInfNFeDetImpostoICMSICMS30)detIcms;
                            icms30Aux.orig = (Torig)Enum.Parse(typeof(Torig), "Item" + produto.OrigemMercadoria);
                            icms30Aux.CST = TNFeInfNFeDetImpostoICMSICMS30CST.Item30;
                            icms30Aux.modBCST = TNFeInfNFeDetImpostoICMSICMS30ModBCST.Item0;
                            icms30Aux.pMVAST = "0.00";
                            icms30Aux.pRedBCST = "0.00";
                            icms30Aux.vBCST = "0.00";
                            icms30Aux.pICMSST = "0.00";
                            //icms30Aux.vBCFCPST = "0.00";
                            //icms30Aux.pFCPST = "0.00";
                            //icms30Aux.vFCPST = "0.00";
                            icms30Aux.vICMSDeson = "0.00";
                            icms30Aux.motDesICMS = TNFeInfNFeDetImpostoICMSICMS30MotDesICMS.Item9;
                            impICMS.Item = icms30Aux;
                            break;
                        case "40":
                        case "41":
                        case "50":
                            detIcms = new TNFeInfNFeDetImpostoICMSICMS40();
                            var icms40Aux = (TNFeInfNFeDetImpostoICMSICMS40)detIcms;
                            icms40Aux.orig = (Torig)Enum.Parse(typeof(Torig), "Item" + produto.OrigemMercadoria);
                            icms40Aux.CST = (TNFeInfNFeDetImpostoICMSICMS40CST)Enum.Parse(typeof(TNFeInfNFeDetImpostoICMSICMS40CST), "Item" + 40);
                            icms40Aux.vICMSDeson = "0.00";
                            icms40Aux.motDesICMS = TNFeInfNFeDetImpostoICMSICMS40MotDesICMS.Item9;
                            impICMS.Item = icms40Aux;
                            break;
                        case "51":
                            detIcms = new TNFeInfNFeDetImpostoICMSICMS51();
                            var icms51Aux = (TNFeInfNFeDetImpostoICMSICMS51)detIcms;
                            icms51Aux.orig = (Torig)Enum.Parse(typeof(Torig), "Item" + produto.OrigemMercadoria);
                            icms51Aux.CST = (TNFeInfNFeDetImpostoICMSICMS51CST)Enum.Parse(typeof(TNFeInfNFeDetImpostoICMSICMS51CST), "Item" + 51);
                            icms51Aux.modBC = TNFeInfNFeDetImpostoICMSICMS51ModBC.Item0;
                            icms51Aux.pRedBC = "0.00";
                            icms51Aux.vBC = "0.00";
                            icms51Aux.pICMS = "0.00";
                            icms51Aux.vICMSOp = "0.00";
                            icms51Aux.pDif = "0.00";
                            icms51Aux.vICMS = "0.00";
                            //icms51Aux.vBCFCP = "0.00";
                            //icms51Aux.pFCP = "0.00";
                            //icms51Aux.vFCP = "0.00";
                            impICMS.Item = icms51Aux;
                            break;
                        case "60":
                            detIcms = new TNFeInfNFeDetImpostoICMSICMS60();
                            var icms60Aux = (TNFeInfNFeDetImpostoICMSICMS60)detIcms;
                            icms60Aux.orig = (Torig)Enum.Parse(typeof(Torig), "Item" + produto.OrigemMercadoria);
                            icms60Aux.CST = (TNFeInfNFeDetImpostoICMSICMS60CST)Enum.Parse(typeof(TNFeInfNFeDetImpostoICMSICMS60CST), "Item" + 60);
                            icms60Aux.vBCSTRet = "0.00";
                            icms60Aux.pST = "0.00";
                            icms60Aux.vICMSSubstituto = "0.00";
                            icms60Aux.vICMSSTRet = "0.00";
                            //icms60Aux.vBCFCPSTRet = "0.00";
                            //icms60Aux.pFCPSTRet = "0.00";
                            //icms60Aux.vFCPSTRet = "0.00";
                            icms60Aux.vBCSTRet = "0.00";
                            icms60Aux.pST = "0.00";
                            icms60Aux.vICMSSubstituto = "0.00";
                            impICMS.Item = icms60Aux;
                            break;
                        case "70":
                            detIcms = new TNFeInfNFeDetImpostoICMSICMS70();
                            var icms70Aux = (TNFeInfNFeDetImpostoICMSICMS70)detIcms;
                            icms70Aux.orig = (Torig)Enum.Parse(typeof(Torig), "Item" + produto.OrigemMercadoria);
                            icms70Aux.CST = (TNFeInfNFeDetImpostoICMSICMS70CST)Enum.Parse(typeof(TNFeInfNFeDetImpostoICMSICMS70CST), "Item" + 70);
                            icms70Aux.modBC = TNFeInfNFeDetImpostoICMSICMS70ModBC.Item0;
                            icms70Aux.pRedBC = "0.00";
                            icms70Aux.vBC = "0.00";
                            icms70Aux.pICMS = "0.00";
                            icms70Aux.vBC = "0.00";
                            //campos opcionais
                            //icms70Aux.vBCFCP = "0.00";
                            //icms70Aux.pFCP = "0.00";
                            //icms70Aux.vFCP = "0.00";
                            //icms70Aux.modBCST = TNFeInfNFeDetImpostoICMSICMS70ModBCST.Item0;
                            //icms70Aux.pMVAST = "0.00";
                            //icms70Aux.pRedBCST = "0.00";
                            //icms70Aux.vBCST = "0.00";
                            //icms70Aux.pICMSST = "0.00";
                            //icms70Aux.vICMSST = "0.00";
                            //campos opcionais
                            //icms70Aux.vBCFCPST = "0.00";
                            //icms70Aux.pFCPST = "0.00";
                            //icms70Aux.vFCPST = "0.00";
                            //campos opcionais
                            //icms70Aux.vICMSDeson = "0.00";
                            //icms70Aux.motDesICMS = TNFeInfNFeDetImpostoICMSICMS70MotDesICMS.Item9;
                            impICMS.Item = icms70Aux;
                            break;
                        case "90":
                            detIcms = new TNFeInfNFeDetImpostoICMSICMS70();
                            var icms90Aux = (TNFeInfNFeDetImpostoICMSICMS70)detIcms;
                            icms90Aux.orig = (Torig)Enum.Parse(typeof(Torig), "Item" + produto.OrigemMercadoria);
                            icms90Aux.CST = (TNFeInfNFeDetImpostoICMSICMS70CST)Enum.Parse(typeof(TNFeInfNFeDetImpostoICMSICMS70CST), "Item" + 90);
                            icms90Aux.modBC = TNFeInfNFeDetImpostoICMSICMS70ModBC.Item0;
                            icms90Aux.pRedBC = "0.00";
                            icms90Aux.pICMS = "0.00";
                            icms90Aux.vBC = "0.00";
                            impICMS.Item = icms90Aux;
                            break;
                        case "101":
                            detIcms = new TNFeInfNFeDetImpostoICMSICMSSN101();
                            var icms101Aux = (TNFeInfNFeDetImpostoICMSICMSSN101)detIcms;
                            icms101Aux.orig = (Torig)Enum.Parse(typeof(Torig), "Item" + produto.OrigemMercadoria);
                            icms101Aux.CSOSN = (TNFeInfNFeDetImpostoICMSICMSSN101CSOSN)Enum.Parse(typeof(TNFeInfNFeDetImpostoICMSICMSSN101CSOSN), "Item" + 101);
                            icms101Aux.pCredSN = "0.00";
                            icms101Aux.vCredICMSSN = "0.00";
                            impICMS.Item = icms101Aux;
                            break;
                        case "102":
                        case "103":
                        case "300":
                        case "400":
                            detIcms = new TNFeInfNFeDetImpostoICMSICMSSN102();
                            var icms102Aux = (TNFeInfNFeDetImpostoICMSICMSSN102)detIcms;
                            icms102Aux.orig = (Torig)Enum.Parse(typeof(Torig), "Item" + produto.OrigemMercadoria);
                            icms102Aux.CSOSN = (TNFeInfNFeDetImpostoICMSICMSSN102CSOSN)Enum.Parse(typeof(TNFeInfNFeDetImpostoICMSICMSSN102CSOSN), "Item" + 102);
                            impICMS.Item = icms102Aux;
                            break;
                        case "201":
                            detIcms = new TNFeInfNFeDetImpostoICMSICMSSN201();
                            var icms201Aux = (TNFeInfNFeDetImpostoICMSICMSSN201)detIcms;
                            icms201Aux.orig = (Torig)Enum.Parse(typeof(Torig), "Item" + produto.OrigemMercadoria);
                            icms201Aux.CSOSN = (TNFeInfNFeDetImpostoICMSICMSSN201CSOSN)Enum.Parse(typeof(TNFeInfNFeDetImpostoICMSICMSSN201CSOSN), "Item" + 201);
                            icms201Aux.modBCST = TNFeInfNFeDetImpostoICMSICMSSN201ModBCST.Item0;
                            icms201Aux.pMVAST = "0.00";
                            icms201Aux.pRedBCST = "0.00";
                            icms201Aux.vBCST = "0.00";
                            icms201Aux.pICMSST = "0.00";
                            icms201Aux.vICMSST = "0.00";
                            impICMS.Item = icms201Aux;
                            break;
                        case "202":
                        case "203":
                            detIcms = new TNFeInfNFeDetImpostoICMSICMSSN202();
                            var icms202Aux = (TNFeInfNFeDetImpostoICMSICMSSN202)detIcms;
                            icms202Aux.orig = (Torig)Enum.Parse(typeof(Torig), "Item" + produto.OrigemMercadoria);
                            icms202Aux.CSOSN = (TNFeInfNFeDetImpostoICMSICMSSN202CSOSN)Enum.Parse(typeof(TNFeInfNFeDetImpostoICMSICMSSN202CSOSN), "Item" + 202);
                            icms202Aux.modBCST = TNFeInfNFeDetImpostoICMSICMSSN202ModBCST.Item0;
                            icms202Aux.pMVAST = "0.00";
                            icms202Aux.pRedBCST = "0.00";
                            icms202Aux.vBCST = "0.00";
                            icms202Aux.pICMSST = "0.00";
                            icms202Aux.vICMSST = "0.00";
                            impICMS.Item = icms202Aux;
                            break;
                        case "500":
                            detIcms = new TNFeInfNFeDetImpostoICMSICMSSN500();
                            var icms500Aux = (TNFeInfNFeDetImpostoICMSICMSSN500)detIcms;
                            icms500Aux.orig = (Torig)Enum.Parse(typeof(Torig), "Item" + produto.OrigemMercadoria);
                            icms500Aux.CSOSN = (TNFeInfNFeDetImpostoICMSICMSSN500CSOSN)Enum.Parse(typeof(TNFeInfNFeDetImpostoICMSICMSSN500CSOSN), "Item" + 500);
                            icms500Aux.vBCSTRet = "0.00";
                            icms500Aux.pST = "0.00";
                            icms500Aux.vICMSSubstituto = "0.00";
                            icms500Aux.vICMSSTRet = "0.00";
                            impICMS.Item = icms500Aux;
                            break;
                        case "900":
                            detIcms = new TNFeInfNFeDetImpostoICMSICMSSN900();
                            var icms900Aux = (TNFeInfNFeDetImpostoICMSICMSSN900)detIcms;
                            icms900Aux.orig = (Torig)Enum.Parse(typeof(Torig), "Item" + produto.OrigemMercadoria);
                            icms900Aux.CSOSN = (TNFeInfNFeDetImpostoICMSICMSSN900CSOSN)Enum.Parse(typeof(TNFeInfNFeDetImpostoICMSICMSSN900CSOSN), "Item" + 900);
                            icms900Aux.modBC = TNFeInfNFeDetImpostoICMSICMSSN900ModBC.Item0;
                            icms900Aux.vBC = "0.00";
                            icms900Aux.pRedBC = "0.00";
                            icms900Aux.pICMS = "0.00";
                            icms900Aux.vICMS = "0.00";
                            impICMS.Item = icms900Aux;
                            break;
                        default:
                            //se não for nenhum dos icms acima, será o ICMS de PARTILHA
                            detIcms = new TNFeInfNFeDetImpostoICMSUFDest();
                            var icmsPartAux = (TNFeInfNFeDetImpostoICMSUFDest)detIcms;
                            icmsPartAux.vBCUFDest = "0.00";
                            icmsPartAux.vBCFCPUFDest = "0.00";
                            icmsPartAux.pFCPUFDest = "0.00";
                            icmsPartAux.pICMSUFDest = "0.00";
                            icmsPartAux.pICMSInter = TNFeInfNFeDetImpostoICMSUFDestPICMSInter.Item1200;
                            icmsPartAux.pICMSInterPart = "0.00";
                            icmsPartAux.vFCPUFDest = "0.00";
                            icmsPartAux.vICMSUFDest = "0.00";
                            icmsPartAux.vICMSUFDest = "0.00";
                            icmsPartAux.vICMSUFRemet = "0.00";
                            impICMS.Item = icmsPartAux;
                            break;
                    }
                }
                notaFiscal.infNFe.det[indice].imposto.Items[0] = impICMS;

                //DADOS PIS---------------------------------------------------------------
                notaFiscal.infNFe.det[indice].imposto.PIS = new TNFeInfNFeDetImpostoPIS();
                var impPIS = new TNFeInfNFeDetImpostoPIS();
                object detPis = null;

                //aqui é o CST de PIS que vem do banco de dados do cliente
                var pis_cst = produto.PIS_CST;
                if (!string.IsNullOrWhiteSpace(pis_cst))
                {
                    switch (produto.PIS_CST)
                    {
                        case "01":
                        case "02":
                            detPis = new TNFeInfNFeDetImpostoPISPISAliq();
                            var pisAliqAux = (TNFeInfNFeDetImpostoPISPISAliq)detPis;
                            pisAliqAux.CST = (TNFeInfNFeDetImpostoPISPISAliqCST)Enum.Parse(typeof(TNFeInfNFeDetImpostoPISPISAliqCST), "Item" + pis_cst);
                            pisAliqAux.vBC = "0.00";
                            pisAliqAux.pPIS = "0.00";
                            pisAliqAux.vPIS = "0.00";
                            impPIS.Item = pisAliqAux;
                            break;
                        case "04":
                            detPis = new TNFeInfNFeDetImpostoPISPISNT();
                            var pisNTAux = (TNFeInfNFeDetImpostoPISPISNT)detPis;
                            pisNTAux.CST = (TNFeInfNFeDetImpostoPISPISNTCST)Enum.Parse(typeof(TNFeInfNFeDetImpostoPISPISNTCST), "Item" + pis_cst);
                            impPIS.Item = pisNTAux;
                            break;
                        default:
                            break;
                    }
                }
                notaFiscal.infNFe.det[indice].imposto.PIS = impPIS;

                //DADOS COFINS----------------------------------------------------------
                notaFiscal.infNFe.det[indice].imposto.COFINS = new TNFeInfNFeDetImpostoCOFINS();
                var impCOFINS = new TNFeInfNFeDetImpostoCOFINS();
                object detCofins = null;

                //aqui é o CST de PIS que vem do banco de dados do cliente
                var cofins_cst = produto.COFINS_CST;
                if (!string.IsNullOrWhiteSpace(cofins_cst))
                {
                    switch (produto.COFINS_CST)
                    {
                        case "01":
                        case "02":
                            detCofins = new TNFeInfNFeDetImpostoCOFINSCOFINSAliq();
                            var cofinsAliqAux = (TNFeInfNFeDetImpostoCOFINSCOFINSAliq)detPis;
                            cofinsAliqAux.CST = (TNFeInfNFeDetImpostoCOFINSCOFINSAliqCST)Enum.Parse(typeof(TNFeInfNFeDetImpostoCOFINSCOFINSAliqCST), "Item" + cofins_cst);
                            cofinsAliqAux.vBC = "0.00";
                            cofinsAliqAux.pCOFINS = "0.00";
                            cofinsAliqAux.vCOFINS = "0.00";
                            impCOFINS.Item = cofinsAliqAux;
                            break;
                        case "04":
                            detCofins = new TNFeInfNFeDetImpostoCOFINSCOFINSNT();
                            var cofinsNTAux = (TNFeInfNFeDetImpostoCOFINSCOFINSNT)detPis;
                            cofinsNTAux.CST = (TNFeInfNFeDetImpostoCOFINSCOFINSNTCST)Enum.Parse(typeof(TNFeInfNFeDetImpostoCOFINSCOFINSNTCST), "Item" + cofins_cst);
                            impCOFINS.Item = cofinsNTAux;
                            break;
                        default:
                            break;
                    }
                }
                notaFiscal.infNFe.det[indice].imposto.COFINS = impCOFINS;
                indice++;
            }//fim do foreach

            //DADOS TOTAIS DA NFE
            notaFiscal.infNFe.total = new TNFeInfNFeTotal();
            notaFiscal.infNFe.total.ICMSTot = new TNFeInfNFeTotalICMSTot();
            notaFiscal.infNFe.total.ICMSTot.vBC = double.Parse("25.00").ToString("0.00");
            notaFiscal.infNFe.total.ICMSTot.vICMS = double.Parse("3.00").ToString("0.00");
            notaFiscal.infNFe.total.ICMSTot.vBCST = double.Parse("0.00").ToString("0.00");
            notaFiscal.infNFe.total.ICMSTot.vST = double.Parse("0.00").ToString("0.00");
            notaFiscal.infNFe.total.ICMSTot.vProd = double.Parse("25.00").ToString("0.00");
            notaFiscal.infNFe.total.ICMSTot.vFrete = double.Parse("0.00").ToString("0.00");
            notaFiscal.infNFe.total.ICMSTot.vSeg = double.Parse("0.00").ToString("0.00");
            notaFiscal.infNFe.total.ICMSTot.vDesc = double.Parse("0.00").ToString("0.00");
            notaFiscal.infNFe.total.ICMSTot.vII = double.Parse("0.00").ToString("0.00");
            notaFiscal.infNFe.total.ICMSTot.vIPI = double.Parse("0.00").ToString("0.00");
            notaFiscal.infNFe.total.ICMSTot.vPIS = double.Parse("0.00").ToString("0.00");
            notaFiscal.infNFe.total.ICMSTot.vCOFINS = double.Parse("0.00").ToString("0.00");
            notaFiscal.infNFe.total.ICMSTot.vOutro = double.Parse("0.00").ToString("0.00");
            notaFiscal.infNFe.total.ICMSTot.vNF = double.Parse("0.00").ToString("0.00");
            notaFiscal.infNFe.total.ISSQNtot = new TNFeInfNFeTotalISSQNtot();

            //DADOS DO TRANSPORTADOR
            notaFiscal.infNFe.transp = new TNFeInfNFeTransp();
            var modFrete = "0";
            switch (modFrete)
            {
                case "0":
                    notaFiscal.infNFe.transp.modFrete = TNFeInfNFeTranspModFrete.Item0;
                    break;
                case "1":
                    notaFiscal.infNFe.transp.modFrete = TNFeInfNFeTranspModFrete.Item1;
                    break;
                case "2":
                    notaFiscal.infNFe.transp.modFrete = TNFeInfNFeTranspModFrete.Item2;
                    break;
                case "9":
                    notaFiscal.infNFe.transp.modFrete = TNFeInfNFeTranspModFrete.Item9;
                    break;
            }
            var existeTransportador = false;
            if (existeTransportador == true)
            {
                notaFiscal.infNFe.transp.transporta = new TNFeInfNFeTranspTransporta();
                var transCnpj = ("07.364.617.0001-35").Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
                if (!string.IsNullOrWhiteSpace(transCnpj))
                {
                    notaFiscal.infNFe.transp.transporta.ItemElementName = ItemChoiceType6.CNPJ;
                    notaFiscal.infNFe.transp.transporta.Item = transCnpj;
                }
                notaFiscal.infNFe.transp.transporta.IE = "0108090704";
                notaFiscal.infNFe.transp.transporta.xNome = "EMPRESA DE TESTE";
                notaFiscal.infNFe.transp.transporta.xEnder = "ENDERECO RUA DE TESTE";
                notaFiscal.infNFe.transp.transporta.xMun = "PORTO ALEGRE";
                notaFiscal.infNFe.transp.transporta.UF = TUf.RS;
            }
            notaFiscal.infNFe.pag = new TNFeInfNFePag();
            notaFiscal.infNFe.pag.vTroco = double.Parse("0.00").ToString("0.00");
            
            List<FormasPagtoBancoDados> detPagBanco = GetFormasPagtoAvistaeCredito();
            notaFiscal.infNFe.pag.detPag = new TNFeInfNFePagDetPag[detPagBanco.Count()];
            int indiceDetPag = 0;
            foreach (var detPagamento in detPagBanco)
            {
                notaFiscal.infNFe.pag.detPag[indiceDetPag] = new TNFeInfNFePagDetPag();
                notaFiscal.infNFe.pag.detPag[indiceDetPag].indPag =(TNFeInfNFePagDetPagIndPag)Enum.Parse(typeof(TNFeInfNFePagDetPagIndPag), "Item" + detPagamento.indPag);
                notaFiscal.infNFe.pag.detPag[indiceDetPag].tPag = (TNFeInfNFePagDetPagTPag)Enum.Parse(typeof(TNFeInfNFePagDetPagTPag), "Item" + detPagamento.tPag);
                notaFiscal.infNFe.pag.detPag[indiceDetPag].vPag = detPagamento.vPag;
                //Se o meio de pagamento for 03=Cartão de Crédito, 04=Cartão de Débito
                if (detPagamento.tPag =="03" || detPagamento.tPag == "04")
                {
                    notaFiscal.infNFe.pag.detPag[indiceDetPag].card = new TNFeInfNFePagDetPagCard();
                    notaFiscal.infNFe.pag.detPag[indiceDetPag].card.tpIntegra = (TNFeInfNFePagDetPagCardTpIntegra)Enum.Parse(typeof(TNFeInfNFePagDetPagCardTpIntegra), "Item" + detPagamento.tpIntegra);
                }

                //Se o meio de pagamento for 15=Boleto Bancário é necessário informar os grupos Cobrança e Fatura
                if (detPagamento.tPag == "15")
                {
                    notaFiscal.infNFe.cobr = new TNFeInfNFeCobr();
                    notaFiscal.infNFe.cobr.fat = new TNFeInfNFeCobrFat();
                    notaFiscal.infNFe.cobr.fat.nFat = "757580";
                    notaFiscal.infNFe.cobr.fat.vOrig = "20.00";
                    notaFiscal.infNFe.cobr.fat.vDesc = "0.00";
                    notaFiscal.infNFe.cobr.fat.vLiq = "20.00";
                    notaFiscal.infNFe.cobr.dup = new TNFeInfNFeCobrDup[1];
                    notaFiscal.infNFe.cobr.dup[0].nDup = "1";
                    notaFiscal.infNFe.cobr.dup[0].dVenc = "27/09/2021";
                    notaFiscal.infNFe.cobr.dup[0].vDup = "20.00";
                }
                indiceDetPag++;
            }//fim do foreach formas pagtp
            
            notaFiscal.infNFe.infAdic = new TNFeInfNFeInfAdic();
            notaFiscal.infNFe.infAdic.infAdFisco = "DESCRICAO PADRAO DA SEFAZ";
            Console.WriteLine(notaFiscal.ToXMLString());
            Console.ReadLine();
        }


        public static List<ProdutosBancoDados> GetProdutos()
        {
            var listaProdutos = new List<ProdutosBancoDados>()
            {
                new ProdutosBancoDados { IdProd=1,cProd="TESTE",cEAN="SEM GTIN", xProd="TESTE", OrigemMercadoria="0", ICMS_CST="20", modBC="0", vBC="25.00", pICMS="12.00", vICMS="3.00",ValorICMSDesoneracao="2.50", PIS_CST="04", COFINS_CST="49", IPI_CST="51", cEnq="123"},
                new ProdutosBancoDados { IdProd=2,cProd="TESTE 02",cEAN="SEM GTIN", xProd="TESTE 02", OrigemMercadoria="0", ICMS_CST="20", modBC="0", vBC="15.00", pICMS="12.00", vICMS="1.50",ValorICMSDesoneracao="2.50", PIS_CST="04", COFINS_CST="49", IPI_CST="51", cEnq="123"},
            };
            return listaProdutos;
        }

        public static List<FormasPagtoBancoDados> GetFormasPagtoAvistaeCredito()
        {
            var listaFormasPagto = new List<FormasPagtoBancoDados>()
            {
                new FormasPagtoBancoDados { IdInfPag=1, indPag="0",tPag="01",vPag="25.00"},
                new FormasPagtoBancoDados { IdInfPag=2, indPag="0",tPag="03", tpIntegra="2", CNPJ="68184002000111", tBand="01"}
            };
            return listaFormasPagto;
        }

        public static List<FormasPagtoBancoDados> GetFormasPagtoCredito()
        {
            var listaFormasPagto = new List<FormasPagtoBancoDados>()
            {
                new FormasPagtoBancoDados { IdInfPag=1,indPag="1",tPag="03", tpIntegra="2", CNPJ="68184002000111", tBand="01", cAut="12345678910111213141"}
            };
            return listaFormasPagto;
        }

        private void frmNFe_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelamento_Click(object sender, EventArgs e)
        {
            CancelarReqNFe cancNFe = new CancelarReqNFe
            {
                chNFe = "11111111111111111111111111111111111111111111",
                nProt = "143210000351228",
                tpAmb = "2",
                dhEvento = "2019-03-15T15:37:14-03:00",
                xJust = "TESTE DE CANCELAMENTO INTEGRAÇÃO NS"
            };
            //No metodo cancelarDocumento é necessário enviar o modelo do documento, o JSON do cancelamento, eo CNPJ do emitente
            var resposta = NSSuite.cancelarDocumento("55", cancNFe, "07364617000135", false);
            Console.WriteLine(resposta);
            Console.ReadLine();
        }

        private void btnCCe_Click(object sender, EventArgs e)
        {
            CorrigirReqNFe cartaCorrecNFe = new CorrigirReqNFe
            {
                chNFe = "11111111111111111111111111111111111111111111",
                xCorrecao = "143210000351228",
                tpAmb = "2",
                dhEvento = "2019-03-15T15:37:14-03:00",
                nSeqEvento = "1"

            };
            //No metodo cancelarDocumento é necessário enviar o modelo do documento, o JSON do cancelamento, eo CNPJ do emitente
            var resposta = NSSuite.corrigirDocumento("55", cartaCorrecNFe, "07364617000135", false);
            Console.WriteLine(resposta);
            Console.ReadLine();
        }

        private void btnIuntilizacao_Click(object sender, EventArgs e)
        {
            InutilizarReqNFe inutilizarNFe = new InutilizarReqNFe()
            {
                nNFIni = "1",
                nNFFin = "3",
                cUF = "2",
                ano = "2021-03-15T15:37:14-03:00",
                tpAmb = "1",
                CNPJ = "1111111111111",
                serie = "0",
                xJust = "TESTE DE INUTILIZACAO"
            };
            //No metodo cancelarDocumento é necessário enviar o modelo do documento, o JSON do cancelamento, eo CNPJ do emitente
            var resposta = NSSuite.inutilizarNumeracao("55", inutilizarNFe, "1111111111111", false);
            Console.WriteLine(resposta);
            Console.ReadLine();
        }

        private void btnConsultaCadastroContrib_Click(object sender, EventArgs e)
        {
            InutilizarReqNFe inutilizarNFe = new InutilizarReqNFe()
            {
                nNFIni = "1",
                nNFFin = "3",
                cUF = "2",
                ano = "2021-03-15T15:37:14-03:00",
                tpAmb = "1",
                CNPJ = "1111111111111",
                serie = "0",
                xJust = "TESTE DE INUTILIZACAO"
            };
            //No metodo cancelarDocumento é necessário enviar o modelo do documento, o JSON do cancelamento, eo CNPJ do emitente
            var resposta = NSSuite.inutilizarNumeracao("55", inutilizarNFe, "1111111111111", false);
            Console.WriteLine(resposta);
            Console.ReadLine();
        }
    }
}

