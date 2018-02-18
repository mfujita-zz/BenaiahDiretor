using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenaiahCadastroFuncionarias
{
    public partial class FormExcluir : Form
    {
        public FormExcluir()
        {
            InitializeComponent();
        }

        private void FormExcluir_Load(object sender, EventArgs e)
        {
            lblAviso.Text = "Escreva o nome completo da funcionária a ser removido. Esta operação não pode ser desfeita.";
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            SqlConnection conexao = new SqlConnection("Server=ULTRABOOK\\SQLEXPRESS;Database=Benaiah;Trusted_Connection=True;");
            conexao.Open();
            SqlCommand comando = new SqlCommand("delete from funcionaria where nome = @nome", conexao);
            comando.Parameters.AddWithValue("@nome", txtExcluir.Text.Trim());
            comando.ExecuteNonQuery();
            conexao.Close();
            MessageBox.Show("Funcionária " + txtExcluir.Text.Trim() + " excluída com sucesso");
            Close();
        }
    }
}
