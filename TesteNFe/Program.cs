using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ns_nfe_core.src.emissao;

namespace TesteNFe
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            //var nfe = EmissaoSincrona.sendPostRequest();
            configParceiro.token = "ADQWREQW561D32AWS1D6";
            var consulta = new StatusProcessamento.Body{ 
                CNPJ = "07364617000135",
                nsNRec = "3735952"
            };
            var resposta = await StatusProcessamento.sendPostRequest(consulta);
            Console.WriteLine(resposta);

        }
    }
}
