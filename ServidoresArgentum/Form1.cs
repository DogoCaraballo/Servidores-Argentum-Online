using AngleSharp.Dom;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AngleSharp;

namespace ServidoresArgentum
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //iniciar trabajo de busqueda.
            RefrescarServidores();
        }

        private void RefrescarServidores()
        {
            _ = ScrapeWebsite("https://www.imperiumao.com.ar/es/", label1,"span", "class", "JugadoresOnline");
            _ = ScrapeWebsite("https://tierrasdelsur.cc/", label2, "a", "title", "Ver estado de los servidores");
            _ = ScrapeWebsite("https://www.furiusao.com.ar/", label3, "td", "height", "27");
            _ = ScrapeWebsite("https://www.fenixao.com.ar/", label4, "texto", "class", "textoonline");
            _ = ScrapeWebsite("https://mercuryao.com/Oficial/", label5, "td", "background", "images/users.png");
            _ = ScrapeWebsite("https://www.comunidadargentum.com/", label6, "div", "id", "usronline");
        }


        private async Task ScrapeWebsite(String pagina, Label label, String etiqueta, String atributo, String equals)
        {


            //leo la pagina
            var config = new Configuration().WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(pagina);

            var blueListItemsLinq = document.All.Where(m => m.LocalName == etiqueta && m.GetAttribute(atributo) == equals);

            //consigo lo que estoy buscando
            String testo;
            var item = blueListItemsLinq.First();
            testo = item.Text().ToString();

            //remuevo otras <etiquetas> o &nbsp; que pudieron quedar atrapadas en lo que obtuve
            testo = removerMierda(testo);

            //meto el resultado numerico en el label
            label.Text = obtenerNumeros(testo).ToString();
        }

        String removerMierda(String str)
        {
            int index,index2;
            if(str.Contains("<") && str.Contains(">")){
                index = str.IndexOf("<");
                index2 = str.IndexOf(">");
                str.Remove(index, index2 - index);
            }
            if (str.Contains("<") && str.Contains(">"))
            {
                index = str.IndexOf("<");
                index2 = str.IndexOf(">");
                str.Remove(index, index2 - index);
            }
            if (str.Contains("&nbsp;"))
            {
                str.Replace("&nbsp;", "");
            }
            if (str.Contains("&nbsp"))
            {
                str.Replace("&nbsp", "");
            }

            return str;
        }

        private int obtenerNumeros(String target)
        {
            string b = string.Empty;
            int val;

            for (int i = 0; i < target.Length; i++)
            {
                if (Char.IsDigit(target[i]))
                    b += target[i];
            }

            if (b.Length > 0)
            {
                val = int.Parse(b);
                return val;
            }
            else return 0;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://steamcommunity.com/id/dogocaraballo");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/DogoCaraballo/Servidores-Argentum-Online");
        }
    }
}
