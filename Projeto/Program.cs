using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_2_AED2
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        //Evento ao click no botao
        private async void button1_Click(object sender, EventArgs e)
        {
            var text = this.richTextBox1.Text;
            var palavras = text.Split(" "); //Divide em palavras
            //verificando existencia no dic 
            foreach(var palavra in palavras)
            {
                if (!await checkDicionario(palavra))
                {
                    adcionaDicionario(palavra);
                }
            }
            //Definindo fonte padrao no dic
            richTextBox1.SelectAll();
            richTextBox1.SelectionFont = new Font("Arial", (float)9);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
