public class Endpoints
{
    // NF-e
    public string NFeEnvio { get; set; } = "https://nfe.ns.eti.br/nfe/issue";
    public string NFeConsStatusProcessamento { get; set; } = "https://nfe.ns.eti.br/nfe/issue/status";
    public string NFeDownload { get; set; } = "https://nfe.ns.eti.br/nfe/get";
    public string NFeDownloadEvento { get; set; } = "https://nfe.ns.eti.br/nfe/get/event";
    public string NFeCancelamento { get; set; } = "https://nfe.ns.eti.br/nfe/cancel";
    public string NFeCCe { get; set; } = "https://nfe.ns.eti.br/nfe/cce";
    public string NFeConsStatusSefaz { get; set; } = "https://nfe.ns.eti.br/util/wssefazstatus";
    public string NFeConsCad { get; set; } = "https://nfe.ns.eti.br/util/conscad";
    public string NFeConsSit { get; set; } = "https://nfe.ns.eti.br/nfe/stats";
    public string NFeEnvioEmail { get; set; } = "https://nfe.ns.eti.br/util/resendemail";
    public string NFeInutilizacao { get; set; } = "https://nfe.ns.eti.br/nfe/inut";
    public string NFeListarNSNRecs { get; set; } = "https://nfe.ns.eti.br/util/list/nsnrecs";
    public string NFePrevia { get; set; } = "https://nfe.ns.eti.br/util/preview/nfe";
    public string NFeGerarPDFDeXML { get; set; } = "https://nfe.ns.eti.br/util/generatepdf";
    public string NFeGerarXMLEmissao { get; set; } = "https://nfe.ns.eti.br/util/generatexml";
    public string NFeGerarXMLCancelamento { get; set; } = "https://nfe.ns.eti.br/util/generatecancel";
    public string NFeGerarXMLCorrecao { get; set; } = "https://nfe.ns.eti.br/util/generatecce";
    public string NFeGerarXMLInut { get; set; } = "https://nfe.ns.eti.br/util/generateinut";
}
