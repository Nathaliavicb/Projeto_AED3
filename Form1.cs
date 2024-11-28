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

namespace Projeto_1_AED3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font("Arial", (float)9);
        }

        private async Task<bool> checkDicionario(string palavra)
        {
            var linhas = File.ReadAllLines("dicionario.txt", Encoding.GetEncoding("ISO-8859-1"));
            foreach (var linha in linhas)
            {
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

        public void adcionaDicionario(string palavra)
        {
            var linhas = File.ReadAllLines("dicionario.txt", Encoding.GetEncoding("ISO-8859-1"));
            linhas[linhas.Length - 1] += palavra.ToLower() + ",";
            File.WriteAllLines("dicionario.txt", linhas, Encoding.GetEncoding("ISO-8859-1"));
        }

        private async void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                var txt = richTextBox1.Text.Split(" ");
                var palavra = txt[txt.Length - 1];

                if (!await checkDicionario(palavra))
                {
                    this.richTextBox1.SelectionStart = this.richTextBox1.Text.Length - palavra.Length;
                    this.richTextBox1.SelectionLength = palavra.Length;
                    
                    richTextBox1.SelectionFont = new Font("Arial", (float)9, FontStyle.Underline);

                    // Funcionando colocando com UPPER ao invés de underline
                    // richTextBox1.Text = richTextBox1.Text.Replace(richTextBox1.SelectedText, richTextBox1.SelectedText.ToUpper());
                    this.richTextBox1.SelectionStart = richTextBox1.Text.Length;

                    richTextBox1.SelectionFont = new Font("Arial", (float)9);
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var text = this.richTextBox1.Text;
            var palavras = text.Split(" ");
            foreach(var palavra in palavras)
            {
                if (!await checkDicionario(palavra))
                {
                    adcionaDicionario(palavra);
                }
            }
            richTextBox1.SelectAll();
            richTextBox1.SelectionFont = new Font("Arial", (float)9);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
