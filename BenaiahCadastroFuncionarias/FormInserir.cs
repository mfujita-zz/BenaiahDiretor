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
    public partial class FormInserir : Form
    {
        public FormInserir()
        {
            InitializeComponent();
        }

        private void FormInserir_Load(object sender, EventArgs e)
        {
            lblInstrucao.Text = "Digite NOME, SETOR e a SENHA para nova funcionária.";
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            SqlConnection conexao = new SqlConnection("Server=ULTRABOOK\\SQLEXPRESS;Database=Benaiah;Trusted_Connection=True;");
            conexao.Open();

            //Obtem o número de registros para encontrar o próximo índice vago
            SqlCommand comando = new SqlCommand("select count(*) from funcionaria", conexao);
            int qtdeRegistros = (int)comando.ExecuteScalar();

            comando = new SqlCommand("insert into funcionaria (ID_func, nome, setor, senha) values (@ID, @nome, @setor, @senha)", conexao);
            comando.Parameters.AddWithValue("@ID", qtdeRegistros);
            comando.Parameters.AddWithValue("@nome", txtNome.Text.Trim());
            comando.Parameters.AddWithValue("@setor", txtSetor.Text.Trim());
            comando.Parameters.AddWithValue("@senha", txtSenha.Text.Trim());
            comando.ExecuteNonQuery();
            conexao.Close();
            MessageBox.Show("Funcionária " + txtNome.Text.Trim() + "\nSetor " + txtSetor.Text.Trim() + "\nSenha " + txtSenha.Text + "\nadicionado com sucesso.");
            Close();
        }
    }
}
