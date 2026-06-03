using System;
using System.Windows.Forms;

namespace BuscaTexto {
    public partial class SearchForm : Form {
        public string Pattern => txtPattern.Text;
        public string ReplaceWith => txtReplace.Text;
        public bool CaseSensitive => chkCaseSensitive.Checked;

        public event EventHandler OnSearch;
        public event EventHandler OnReplace;

        public SearchForm(string algorithmName) {
            InitializeComponent();
            this.Text = "Buscar - " + algorithmName;
        }

        private void btnBuscar_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(Pattern)) {
                MessageBox.Show("Digite o termo a ser buscado.");
                return;
            }
            OnSearch?.Invoke(this, EventArgs.Empty);
        }

        private void btnSubstituir_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(Pattern)) {
                MessageBox.Show("Digite o termo a ser buscado.");
                return;
            }
            OnReplace?.Invoke(this, EventArgs.Empty);
        }
    }
}
