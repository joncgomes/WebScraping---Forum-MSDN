using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WebScrap_2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //recurso para baixar a paagina (classe web client)
            // faz parte da biblioteca system.net;
            var wc = new WebClient();

            string pagina = wc.DownloadString("https://social.msdn.microsoft.com/Forums/pt-BR/home");

            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(pagina); // string carregada com as informações da página.

            dataGridView1.Rows.Clear(); // essa função limpa todas as linhas da Grid;

            // variaveis

            string id = string.Empty;

            string titulo = string.Empty;
            string postagem = string.Empty;
            string exibicao = string.Empty;
            string resposta = string.Empty;
            string link = string.Empty;

            foreach(HtmlAgilityPack.HtmlNode node in htmlDocument.GetElementbyId("threadList").ChildNodes)
            {
                if(node.Attributes.Count > 0)
                {
                    id = node.Attributes["data-threadid"].Value;
                    link = "https://social.msdn.microsoft.com/Forums/pt-BR/" + id;                
                    titulo = node.Descendants().First(x => x.Id.Equals("threadTitle_" + id)).InnerText;
                    postagem = WebUtility.HtmlDecode(node.Descendants().First(x => x.Attributes["class"] != null && x.Attributes["class"].Value.Equals("lastpost")).InnerText);
                    exibicao = WebUtility.HtmlDecode(node.Descendants().First(x => x.Attributes["class"] != null && x.Attributes["class"].Value.Equals("viewcount")).InnerText);
                    resposta = WebUtility.HtmlDecode(node.Descendants().First(x => x.Attributes["class"] != null && x.Attributes["class"].Value.Equals("replycount")).InnerText);

                    if(!string.IsNullOrEmpty(titulo))
                    {
                        dataGridView1.Rows.Add(titulo, postagem, exibicao, resposta, link);
                    }
                }
            }
        }
    }
}
