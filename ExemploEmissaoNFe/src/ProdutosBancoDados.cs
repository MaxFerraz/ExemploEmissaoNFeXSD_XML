using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploEmissaoNFe.src
{
    public class ProdutosBancoDados
    {
        public int IdProd { get; set; }
        public string cProd { get; set; }
        public string cEAN { get; set; }
        public string xProd { get; set; }
        //public string NCM { get; set; }
        //public string NVE { get; set; }
        //public string EXTIPI { get; set; }
        //public string CFOP { get; set; }
        //public string uCom { get; set; }
        //public string qCom { get; set; }
        //public string vUnCom { get; set; }
        //public string vProd { get; set; }
        //public string cEANTrib { get; set; }
        //public string uTrib { get; set; }
        //public string qTrib { get; set; }
        //public string vUnTrib { get; set; }
        //public string vFrete { get; set; }
        public string OrigemMercadoria { get; set; }
        public string ICMS_CST { get; set; }
        public string modBC { get; set; }
        public string vBC { get; set; }
        public string pICMS { get; set; }
        public string vICMS { get; set; }
        public string ValorICMSDesoneracao { get; set; }

        public string PIS_CST { get; set; }
        public string COFINS_CST { get; set; }

        public string IPI_CST { get; set; }
        public string cEnq { get; set; }
        public string IPI_vBC { get; set; }
        public string IPI_pIpi { get; set; }
        public string IPI_vIpi { get; set; }

    }
}
