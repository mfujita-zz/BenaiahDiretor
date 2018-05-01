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
            AcessoBancoDeDados bd = new AcessoBancoDeDados();
            SqlConnection conexao = new SqlConnection(bd.BancoDados());
            conexao.Open();

            //Obtém o IDsetor a partir do que foi escolhido no Combobox cbSetor.
            SqlCommand comando = new SqlCommand("select IDsetor from atuacao where setor = @setor", conexao);
            comando.Parameters.AddWithValue("@setor", cbSetor.Text);
            int IDsetor = 0;
            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    IDsetor = Convert.ToInt16(reader["IDsetor"].ToString().Trim());
                }
            }

            comando = new SqlCommand("insert into funcionaria (nome, IDsetor, senha) values (@nome, @IDsetor, @senha)", conexao);
            comando.Parameters.AddWithValue("@nome", txtNome.Text.Trim());
            comando.Parameters.AddWithValue("@IDsetor", IDsetor);
            comando.Parameters.AddWithValue("@senha", txtSenha.Text.Trim());
            comando.ExecuteNonQuery();
            conexao.Close();
            MessageBox.Show("Funcionária " + txtNome.Text.Trim() + "\nSetor " + cbSetor.Text.Trim() + "\nSenha " + txtSenha.Text + "\nadicionado com sucesso.");
            Close();
        }
    }
}
