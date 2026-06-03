using System;
using System.Windows.Forms;

namespace BuscaTexto {
    public partial class Form1 : Form {
        private SearchForm currentSearchForm;
        private string currentAlgorithm;

        public Form1() {
            InitializeComponent();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e) {
            texto.Text = "";
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show(this,
               "Busca em Texto - 2026/1\n\nDesenvolvido por:\n72500964 - Otávio Tadeu Magalhães Ferreira\nProf. Virgílio Borges de Oliveira\n\nAlgoritmos e Estruturas de Dados II\nFaculdade COTEMIG\nSomente para fins didáticos.",
               "Sobre o trabalho...",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information);
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e) {
            using (OpenFileDialog ofd = new OpenFileDialog()) {
                ofd.Filter = "Text files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf";
                if (ofd.ShowDialog() == DialogResult.OK) {
                    if (ofd.FileName.EndsWith(".rtf", StringComparison.OrdinalIgnoreCase)) {
                        texto.LoadFile(ofd.FileName, RichTextBoxStreamType.RichText);
                    } else {
                        texto.LoadFile(ofd.FileName, RichTextBoxStreamType.PlainText);
                    }
                }
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void forcaBrutaToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenSearchForm("Força Bruta");
        }

        private void rabinKarpToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenSearchForm("Rabin-Karp");
        }

        private void kmpToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenSearchForm("KMP");
        }

        private void boyerMooreToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenSearchForm("Boyer-Moore");
        }

        private void OpenSearchForm(string algorithm) {
            if (currentSearchForm != null && !currentSearchForm.IsDisposed) {
                currentSearchForm.Close();
            }
            currentAlgorithm = algorithm;
            currentSearchForm = new SearchForm(algorithm);
            currentSearchForm.OnSearch += SearchForm_OnSearch;
            currentSearchForm.OnReplace += SearchForm_OnReplace;
            currentSearchForm.Show(this);
        }

        private void LimparDestaques() {
            int selStart = texto.SelectionStart;
            int selLength = texto.SelectionLength;
            texto.SelectAll();
            texto.SelectionBackColor = texto.BackColor;
            texto.Select(selStart, selLength);
        }

        private void SearchForm_OnSearch(object sender, EventArgs e) {
            RealizarBusca(false);
        }

        private void SearchForm_OnReplace(object sender, EventArgs e) {
            RealizarBusca(true);
        }

        private void RealizarBusca(bool replace) {
            if (currentSearchForm == null) return;
            string pattern = currentSearchForm.Pattern;
            string replaceWith = currentSearchForm.ReplaceWith;
            bool caseSensitive = currentSearchForm.CaseSensitive;
            
            LimparDestaques();

            string t = texto.Text;
            string p = pattern;

            if (!caseSensitive) {
                t = t.ToLower();
                p = p.ToLower();
            }

            System.Collections.Generic.List<int> posicoes = new System.Collections.Generic.List<int>();

            switch (currentAlgorithm) {
                case "Força Bruta":
                    posicoes = BuscaForcaBruta.ForcaBruta(p, t);
                    break;
                case "Rabin-Karp":
                    posicoes = BuscaRabinKarp.RKSearch(p, t);
                    break;
                case "KMP":
                    posicoes = BuscaKMP.KMPSearch(p, t);
                    break;
                case "Boyer-Moore":
                    posicoes = BuscaBoyerMoore.BMSearch(p, t);
                    break;
            }

            if (posicoes.Count == 0) {
                MessageBox.Show(this, "Nenhuma ocorrência encontrada.", "Resultado da Busca");
                return;
            }

            if (replace) {
                posicoes.Reverse();
                foreach (int pos in posicoes) {
                    texto.Select(pos, pattern.Length);
                    texto.SelectedText = replaceWith;
                }
                MessageBox.Show(this, $"{posicoes.Count} ocorrências substituídas.", "Substituir");
            } else {
                foreach (int pos in posicoes) {
                    texto.Select(pos, pattern.Length);
                    texto.SelectionBackColor = System.Drawing.Color.Yellow;
                }
                MessageBox.Show(this, $"{posicoes.Count} ocorrências destacadas.", "Resultado da Busca");
            }
            texto.Select(0, 0);
        }
    }
}
