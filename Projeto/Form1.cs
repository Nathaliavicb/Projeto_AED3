using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_AED3
{
    public partial class Form1 : Form
    {
         // Construtor da classe 
        public Form1()
        {
            InitializeComponent();
        }
        // Configurar fonte do RichTextBox
        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font("Arial", (float)9);
        }
         // Verificando se a palavra já existe no dic
        private async Task<bool> checkDicionario(string palavra)
        {
            //Lê as linhas do dic
            var linhas = File.ReadAllLines("dicionario.txt", Encoding.GetEncoding("ISO-8859-1"));
            foreach (var linha in linhas)
            {
                //Separando por , e verificando se a palavra já xiste
                foreach(var item in linha.Split(","))
                {
                    if (palavra.ToLower() == item)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        //Nova palavra ao final
        //ReadAllLines -> Le as linhas
        public void adcionaDicionario(string palavra)
        {
            var linhas = File.ReadAllLines("dicionario.txt", Encoding.GetEncoding("ISO-8859-1"));
            linhas[linhas.Length - 1] += palavra.ToLower() + ",";
            File.WriteAllLines("dicionario.txt", linhas, Encoding.GetEncoding("ISO-8859-1"));
        }

        //Se a tecla pressionada for ESPAÇO
        private async void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                var txt = richTextBox1.Text.Split(" ");
                var palavra = txt[txt.Length - 1];

                //EXISTE
                if (!await checkDicionario(palavra))
                {
                    //NÃO EXISTE - SELEÇÃO
                    this.richTextBox1.SelectionStart = this.richTextBox1.Text.Length - palavra.Length;
                    this.richTextBox1.SelectionLength = palavra.Length;
                    
                    //SUBLINHANDO CASO NAO EXISTA
                    richTextBox1.SelectionFont = new Font("Arial", (float)9, FontStyle.Underline);

                    // Colocando com UPPER ao invés de underline
                    // richTextBox1.Text = richTextBox1.Text.Replace(richTextBox1.SelectedText, richTextBox1.SelectedText.ToUpper());
                    this.richTextBox1.SelectionStart = richTextBox1.Text.Length;

                    richTextBox1.SelectionFont = new Font("Arial", (float)9);
                }
            }
        }
        
    }
}
