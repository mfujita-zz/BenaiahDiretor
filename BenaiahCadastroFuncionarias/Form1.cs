using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenaiahCadastroFuncionarias
{
    public partial class Form1 : Form
    {
        List<string> listaSetor = new List<string>();
        List<string> question1 = new List<string>();
        List<string> question2 = new List<string>();
        List<string> answer1 = new List<string>();
        List<string> answer2 = new List<string>();

        int Coz1 = 0, Coz2 = 0, Coz3 = 0, Coz4 = 0;
        int enf1 = 0, enf2 = 0, enf3 = 0, enf4 = 0;
        int sge1 = 0, sge2 = 0, sge3 = 0, sge4 = 0;
        int tec1 = 0, tec2 = 0, tec3 = 0, tec4 = 0;
        int out1 = 0, out2 = 0, out3 = 0, out4 = 0;
        int tot1 = 0, tot2 = 0, tot3 = 0, tot4 = 0;

        BackgroundWorker bw;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Size = new Size(Screen.PrimaryScreen.WorkingArea.Width * 80 / 100, Screen.PrimaryScreen.WorkingArea.Height);
            Location = new Point(0, 0);
            dgv.Size = new Size(ClientRectangle.Width * 95 / 100, ClientRectangle.Height * 85 / 100);
            nome.Width = dgv.Width * 40 / 100;
            setor.Width = dgv.Width * 30 / 100;
            senha.Width = dgv.Width * 26 / 100;
            //Botão Atualizar
            btnRelatorio.Location = new Point(dgv.Left, dgv.Height + (ClientRectangle.Height - dgv.Bottom) / 2);
            //Botão Inserir
            btnInserir.Location = new Point((ClientRectangle.Width - btnInserir.Width) / 2, dgv.Height + (ClientRectangle.Height - dgv.Bottom) / 2);
            //Botão Excluir
            btnExcluir.Location = new Point(dgv.Right - btnExcluir.Width, dgv.Height + (ClientRectangle.Height - dgv.Bottom) / 2);
            ListaTodosDados();
            PopulaPerguntasResposta();
        }

        private void PopulaPerguntasResposta()
        {
            listaSetor.Add("Cozinha");
            listaSetor.Add("Enfermagem");
            listaSetor.Add("Serviços gerais");
            listaSetor.Add("Técnica");
            listaSetor.Add("Outros");

            question1.Add("Permanece regularmente no local de trabalho para execução de suas atribuições.");
            question1.Add("Cumpre o horário estabelecido.");
            question1.Add("Informa antecipadamente imprevistos que impeçam o seu comparecimento ou cumprimento do horário.");
            question1.Add("Relaciona-se bem com os colegas de trabalho.");
            question1.Add("Trata com cortesia e respeito os idosos que precisam do trabalho designado.");
            question1.Add("Age de acordo com as normas legais e regulamentares.");
            question1.Add("Organiza suas atividades diárias para realizá-las no prazo estabelecido.");
            question1.Add("Realiza com qualidade as atividades que lhe são designadas.");
            question1.Add("Apresenta sugestões para o aperfeiçoamento do serviço.");
            question1.Add("Colabora com os colegas de trabalho, visando manter a coesão e a harmonia na equipe.");
            question1.Add("Busca novos conhecimentos que contribuam para o desenvolvimento dos trabalhos.");
            question1.Add("Habilidade para perceber, interpretar e discernir aspectos importantes no desenvolvimento do trabalho.");
            question1.Add("Capacidade de lidar com situações fora da rotina e a habilidade para criar e desenvolver novas ideias, percebendo, interpretando e discernindo aspectos importantes no desenvolvimento do trabalho.");

            question2.Add("Comunicação (ouve e encoraja outros expressar suas ideias e opiniões de modo objetivo).");
            question2.Add("Trabalho em equipe (contribui ativamente para o esforço da equipe, divide seu conhecimento e experiência com os outros).");
            question2.Add("Solução de problemas (toma decisões e faz julgamentos informais sobre como executar o trabalho; pensa estrategicamente, criativa nas propostas para solução de problemas).");
            question2.Add("Técnica/funcional (tem profundo conhecimento e capacidade em sua especialidade).");
            question2.Add("Melhoria Contínua (promove inovações, busca aperfeiçoar-se).");
            question2.Add("Capacidade de organização (organização do tempo e distribuição de serviços).");
            question2.Add("Visão global do ambiente (entendimento do processo de atendimento dos idosos).");
            question2.Add("Liderança (encoraja o trabalho em equipe, direciona e conduz projetos)");

            answer1.Add("A maior parte do tempo");
            answer1.Add("A menor parte do tempo");
            answer1.Add("Sempre");
            answer1.Add("Nunca");

            answer2.Add("Excede expectativas");
            answer2.Add("Atinge Expectativas");
            answer2.Add("Precisa melhorar");
            answer2.Add("Insatisfatório");
        }

        private void ListaTodosDados()
        {
            dgv.Rows.Clear();

            SqlConnection conexao = new SqlConnection("Server=ULTRABOOK\\SQLEXPRESS;Database=Benaiah;Trusted_Connection=True;");
            conexao.Open();

            SqlCommand comando = new SqlCommand("select * from funcionaria", conexao);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    dgv.RowTemplate.Height = 30;
                    dgv.Rows.Add(reader["Nome"], reader["Setor"], reader["Senha"]);
                }
            }

            conexao.Close();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            FormExcluir excluir = new FormExcluir();
            excluir.ShowDialog();
            ListaTodosDados();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            FormInserir inserir = new FormInserir();
            inserir.ShowDialog();
            ListaTodosDados();
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int linha = dgv.CurrentCell.RowIndex;
                string nomeFuncSelecionada = dgv.Rows[linha].Cells["nome"].Value.ToString().Trim();

                SqlConnection conexao = new SqlConnection("Server=ULTRABOOK\\SQLEXPRESS;Database=Benaiah;Trusted_Connection=True;");
                conexao.Open();

                SqlCommand comando = new SqlCommand("select ID_func from funcionaria where nome = @nome", conexao);
                comando.Parameters.AddWithValue("@nome", nomeFuncSelecionada);

                string Identificacao = "";
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Identificacao = reader["ID_func"].ToString();
                    }
                }

                comando = new SqlCommand("update funcionaria set nome = @nome, setor = @setor, senha = @senha where ID_func = " + Identificacao, conexao);
                comando.Parameters.AddWithValue("@nome", dgv.Rows[linha].Cells["nome"].Value.ToString());
                comando.Parameters.AddWithValue("@setor", dgv.Rows[linha].Cells["setor"].Value.ToString());
                comando.Parameters.AddWithValue("@senha", dgv.Rows[linha].Cells["senha"].Value.ToString());
                comando.ExecuteNonQuery();
                conexao.Close();

                ListaTodosDados();
            }
            catch //Caso comece a editar o conteúdo da célula e não altere nada, retorna -1 (SetCurrentCellAddressCore).
            {
                return;
            }
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
//https://www.codeproject.com/articles/99143/backgroundworker-class-sample-for-beginners
            bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(ExtraiDadosBDGeraRelatorio);
            bw.RunWorkerAsync();
        }

        private void ExtraiDadosBDGeraRelatorio(object sender, DoWorkEventArgs e)
        {
            SqlConnection conexao = new SqlConnection("Server = ULTRABOOK\\SQLEXPRESS; Database = Benaiah; Trusted_Connection = True;");
        conexao.Open();
            SqlCommand comando = new SqlCommand("select nome, setor from funcionaria", conexao);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    string nome = reader["nome"].ToString().Trim();
    string setor = reader["setor"].ToString().Trim();
    FileStream fsIndividual = new FileStream(nome + ".html", FileMode.Create);
    StreamWriter sw = new StreamWriter(fsIndividual);
    sw.WriteLine("<html>");
                    sw.WriteLine("<body>");
                    sw.WriteLine("<style>");
                    sw.WriteLine("td { text-align:center; width: 160px; }");
                    sw.WriteLine("</style>");
                    sw.WriteLine("<h2><div align=center>" + nome + "</div></h2>");

                    if (!setor.Equals("Técnica"))
                    {
                        for (int i = 0; i<question1.Count; i++)
                        {
                            RelatorioIndividualParte1(nome, question1[i], setor, sw);
}
                    }
                    else
                    {
                        for (int i = 0; i<question1.Count; i++)
                        {
                            RelatorioIndividualParte1(nome, question1[i], setor, sw);
                        }
                        if (nome.Equals("JULIANA PINARELLI DE CURTIS"))
                        {
                            for (int i = 0; i<question2.Count; i++)
                            {
                                RelatorioIndividualParte2(nome, question2[i], setor, sw);
                            }
                        }
                        else
                        {
                            for (int i = 0; i<question2.Count - 1; i++)
                            {
                                RelatorioIndividualParte2(nome, question2[i], setor, sw);
                            }
                        }
                    }

                    sw.WriteLine("</body>");
                    sw.WriteLine("</html>");
                    sw.Close();
                }
            }            

            MessageBox.Show("Sucesso!");
        }



        private void RelatorioIndividualParte1(string nomeFuncionaria, string pergunta, string setorAvaliada, StreamWriter sw)
        {
            sw.WriteLine(pergunta);
            sw.WriteLine("<meta charset=utf8>");
            sw.WriteLine("<table border=1>");
            //sw.WriteLine("<tr><td colspan=2>" + (numeroPergunta + 1) + ". " + question[numeroPergunta] + "</td></tr>");

            int Coz1 = 0, Coz2 = 0, Coz3 = 0, Coz4 = 0;
            int enf1 = 0, enf2 = 0, enf3 = 0, enf4 = 0;
            int sge1 = 0, sge2 = 0, sge3 = 0, sge4 = 0;
            int tec1 = 0, tec2 = 0, tec3 = 0, tec4 = 0;
            int out1 = 0, out2 = 0, out3 = 0, out4 = 0;
            int tot1 = 0, tot2 = 0, tot3 = 0, tot4 = 0;

            using (SqlConnection conexao = new SqlConnection("Server = ULTRABOOK\\SQLEXPRESS; Database = Benaiah; Trusted_Connection = True;"))
            {
                conexao.Open();

                foreach (var resposta in answer1)
                {
                    //SqlCommand comando = new SqlCommand(//"select resposta, count(resposta) as frequencia from Respostas where nomeAvaliada = @nomeAvaliada and pergunta like '%" + question1[numeroPergunta] + "%' group by resposta", conexao);
                    SqlCommand comando = new SqlCommand("select resposta, count(resposta) as frequencia from Respostas where nomeAvaliada = @nomeAvaliada and setorDaAvaliadora = @setor and pergunta like '%" + pergunta + "%' and resposta = @resposta group by resposta", conexao);
                    comando.Parameters.AddWithValue("@nomeAvaliada", nomeFuncionaria);
                    comando.Parameters.AddWithValue("@setor", "Cozinha");
                    //comando.Parameters.AddWithValue("@pergunta", pergunta);
                    comando.Parameters.AddWithValue("@resposta", resposta);
                    //sw.WriteLine("<p> select resposta, count(resposta) as frequencia from Respostas where nomeAvaliada = " + nomeFuncionaria + " and setorDaAvaliadora = 'Cozinha' and pergunta like '%Permanece regularmente no local de trabalho para execução de suas atribuições%' and resposta = '" + resposta + "' group by resposta");
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //sw.WriteLine("<tr><td>" + reader["resposta"].ToString() + "</td></tr>");
                            //sw.WriteLine("<tr><td>" + reader["nome"].ToString() + "</td><td>" + reader["setor"].ToString() + "</td><td>" + reader["pergunta"].ToString() + "</td><td>" + reader["resposta"] + "</td></tr>");
                            //sw.WriteLine("<tr><td>" + reader["resposta"] + "</td><td>" + reader["frequencia"] + "</td></tr>");
                            if (reader["resposta"].ToString().Equals(answer1[0])) { Coz1 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer1[1])) { Coz2 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer1[2])) { Coz3 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer1[3])) { Coz4 = Convert.ToSByte(reader["frequencia"].ToString()); }
                        }
                    }
                }

                foreach (var resposta in answer1)
                {
                    SqlCommand comando = new SqlCommand("select resposta, count(resposta) as frequencia from Respostas where nomeAvaliada = @nomeAvaliada and setorDaAvaliadora = @setor and pergunta like '%" + pergunta + "%' and resposta = @resposta group by resposta", conexao);
                    comando.Parameters.AddWithValue("@nomeAvaliada", nomeFuncionaria);
                    comando.Parameters.AddWithValue("@setor", "Enfermagem");
                    comando.Parameters.AddWithValue("@resposta", resposta);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["resposta"].ToString().Equals(answer1[0])) { enf1 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer1[1])) { enf2 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer1[2])) { enf3 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer1[3])) { enf4 = Convert.ToSByte(reader["frequencia"].ToString()); }
                        }
                    }
                }

                foreach (var resposta in answer1)
                {
                    SqlCommand comando = new SqlCommand("select resposta, count(resposta) as frequencia from Respostas where nomeAvaliada = @nomeAvaliada and setorDaAvaliadora = @setor and pergunta like '%" + pergunta + "%' and resposta = @resposta group by resposta", conexao);
                    comando.Parameters.AddWithValue("@nomeAvaliada", nomeFuncionaria);
                    comando.Parameters.AddWithValue("@setor", "Serviços gerais");
                    comando.Parameters.AddWithValue("@resposta", resposta);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["resposta"].ToString().Equals(answer1[0])) { sge1 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer1[1])) { sge2 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer1[2])) { sge3 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer1[3])) { sge4 = Convert.ToSByte(reader["frequencia"].ToString()); }
                        }
                    }
                }

                foreach (var resposta in answer1)
                {
                    SqlCommand comando = new SqlCommand("select resposta, count(resposta) as frequencia from Respostas where nomeAvaliada = @nomeAvaliada and setorDaAvaliadora = @setor and pergunta like '%" + pergunta + "%' and resposta = @resposta group by resposta", conexao);
                    comando.Parameters.AddWithValue("@nomeAvaliada", nomeFuncionaria);
                    comando.Parameters.AddWithValue("@setor", "Técnica");
                    comando.Parameters.AddWithValue("@resposta", resposta);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["resposta"].ToString().Equals(answer1[0])) { tec1 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer1[1])) { tec2 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer1[2])) { tec3 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer1[3])) { tec4 = Convert.ToSByte(reader["frequencia"].ToString()); }
                        }
                    }
                }


                foreach (var resposta in answer1)
                {
                    SqlCommand comando = new SqlCommand("select resposta, count(resposta) as frequencia from Respostas where nomeAvaliada = @nomeAvaliada and setorDaAvaliadora = @setor and pergunta like '%" + pergunta + "%' and resposta = @resposta group by resposta", conexao);
                    comando.Parameters.AddWithValue("@nomeAvaliada", nomeFuncionaria);
                    comando.Parameters.AddWithValue("@setor", "Outros");
                    comando.Parameters.AddWithValue("@resposta", resposta);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["resposta"].ToString().Equals(answer1[0])) { out1 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer1[1])) { out2 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer1[2])) { out3 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer1[3])) { out4 = Convert.ToSByte(reader["frequencia"].ToString()); }
                        }
                    }
                }
            }

            tot1 = Coz1 + enf1 + sge1 + tec1 + out1;
            tot2 = Coz2 + enf2 + sge2 + tec2 + out2;
            tot3 = Coz3 + enf3 + sge3 + tec3 + out3;
            tot4 = Coz4 + enf4 + sge4 + tec4 + out4;

            FazTabela(setorAvaliada, sw);

            sw.WriteLine("</table>");
            sw.WriteLine("<p>");
        }



        private void RelatorioIndividualParte2(string nomeFuncionaria, string pergunta, string setorAvaliada, StreamWriter sw)
        {
            sw.WriteLine(pergunta);
            sw.WriteLine("<meta charset=utf8>");
            sw.WriteLine("<table border=1>");
            //sw.WriteLine("<tr><td colspan=2>" + (numeroPergunta + 1) + ". " + question[numeroPergunta] + "</td></tr>");

            int Coz1 = 0, Coz2 = 0, Coz3 = 0, Coz4 = 0;
            int enf1 = 0, enf2 = 0, enf3 = 0, enf4 = 0;
            int sge1 = 0, sge2 = 0, sge3 = 0, sge4 = 0;
            int tec1 = 0, tec2 = 0, tec3 = 0, tec4 = 0;
            int out1 = 0, out2 = 0, out3 = 0, out4 = 0;
            int tot1 = 0, tot2 = 0, tot3 = 0, tot4 = 0;

            using (SqlConnection conexao = new SqlConnection("Server = ULTRABOOK\\SQLEXPRESS; Database = Benaiah; Trusted_Connection = True;"))
            {
                conexao.Open();

                foreach (var resposta in answer2)
                {
                    //SqlCommand comando = new SqlCommand(//"select resposta, count(resposta) as frequencia from Respostas where nomeAvaliada = @nomeAvaliada and pergunta like '%" + question1[numeroPergunta] + "%' group by resposta", conexao);
                    SqlCommand comando = new SqlCommand("select resposta, count(resposta) as frequencia from Respostas where nomeAvaliada = @nomeAvaliada and setorDaAvaliadora = @setor and pergunta like '%" + pergunta + "%' and resposta = @resposta group by resposta", conexao);
                    comando.Parameters.AddWithValue("@nomeAvaliada", nomeFuncionaria);
                    comando.Parameters.AddWithValue("@setor", "Cozinha");
                    //comando.Parameters.AddWithValue("@pergunta", pergunta);
                    comando.Parameters.AddWithValue("@resposta", resposta);
                    //sw.WriteLine("<p> select resposta, count(resposta) as frequencia from Respostas where nomeAvaliada = " + nomeFuncionaria + " and setorDaAvaliadora = 'Cozinha' and pergunta like '%Permanece regularmente no local de trabalho para execução de suas atribuições%' and resposta = '" + resposta + "' group by resposta");
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //sw.WriteLine("<tr><td>" + reader["resposta"].ToString() + "</td></tr>");
                            //sw.WriteLine("<tr><td>" + reader["nome"].ToString() + "</td><td>" + reader["setor"].ToString() + "</td><td>" + reader["pergunta"].ToString() + "</td><td>" + reader["resposta"] + "</td></tr>");
                            //sw.WriteLine("<tr><td>" + reader["resposta"] + "</td><td>" + reader["frequencia"] + "</td></tr>");
                            if (reader["resposta"].ToString().Equals(answer2[0])) { Coz1 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer2[1])) { Coz2 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer2[2])) { Coz3 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer2[3])) { Coz4 = Convert.ToSByte(reader["frequencia"].ToString()); }
                        }
                    }
                }

                foreach (var resposta in answer2)
                {
                    SqlCommand comando = new SqlCommand("select resposta, count(resposta) as frequencia from Respostas where nomeAvaliada = @nomeAvaliada and setorDaAvaliadora = @setor and pergunta like '%" + pergunta + "%' and resposta = @resposta group by resposta", conexao);
                    comando.Parameters.AddWithValue("@nomeAvaliada", nomeFuncionaria);
                    comando.Parameters.AddWithValue("@setor", "Enfermagem");
                    comando.Parameters.AddWithValue("@resposta", resposta);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["resposta"].ToString().Equals(answer2[0])) { enf1 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer2[1])) { enf2 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer2[2])) { enf3 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer2[3])) { enf4 = Convert.ToSByte(reader["frequencia"].ToString()); }
                        }
                    }
                }

                foreach (var resposta in answer2)
                {
                    SqlCommand comando = new SqlCommand("select resposta, count(resposta) as frequencia from Respostas where nomeAvaliada = @nomeAvaliada and setorDaAvaliadora = @setor and pergunta like '%" + pergunta + "%' and resposta = @resposta group by resposta", conexao);
                    comando.Parameters.AddWithValue("@nomeAvaliada", nomeFuncionaria);
                    comando.Parameters.AddWithValue("@setor", "Serviços gerais");
                    comando.Parameters.AddWithValue("@resposta", resposta);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["resposta"].ToString().Equals(answer2[0])) { sge1 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer2[1])) { sge2 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer2[2])) { sge3 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer2[3])) { sge4 = Convert.ToSByte(reader["frequencia"].ToString()); }
                        }
                    }
                }

                foreach (var resposta in answer2)
                {
                    SqlCommand comando = new SqlCommand("select resposta, count(resposta) as frequencia from Respostas where nomeAvaliada = @nomeAvaliada and setorDaAvaliadora = @setor and pergunta like '%" + pergunta + "%' and resposta = @resposta group by resposta", conexao);
                    comando.Parameters.AddWithValue("@nomeAvaliada", nomeFuncionaria);
                    comando.Parameters.AddWithValue("@setor", "Técnica");
                    comando.Parameters.AddWithValue("@resposta", resposta);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["resposta"].ToString().Equals(answer2[0])) { tec1 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer2[1])) { tec2 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer2[2])) { tec3 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer2[3])) { tec4 = Convert.ToSByte(reader["frequencia"].ToString()); }
                        }
                    }
                }


                foreach (var resposta in answer2)
                {
                    SqlCommand comando = new SqlCommand("select resposta, count(resposta) as frequencia from Respostas where nomeAvaliada = @nomeAvaliada and setorDaAvaliadora = @setor and pergunta like '%" + pergunta + "%' and resposta = @resposta group by resposta", conexao);
                    comando.Parameters.AddWithValue("@nomeAvaliada", nomeFuncionaria);
                    comando.Parameters.AddWithValue("@setor", "Outros");
                    comando.Parameters.AddWithValue("@resposta", resposta);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["resposta"].ToString().Equals(answer2[0])) { out1 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer2[1])) { out2 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer2[2])) { out3 = Convert.ToSByte(reader["frequencia"].ToString()); }
                            else if (reader["resposta"].ToString().Equals(answer2[3])) { out4 = Convert.ToSByte(reader["frequencia"].ToString()); }
                        }
                    }
                }
            }

            tot1 = Coz1 + enf1 + sge1 + tec1 + out1;
            tot2 = Coz2 + enf2 + sge2 + tec2 + out2;
            tot3 = Coz3 + enf3 + sge3 + tec3 + out3;
            tot4 = Coz4 + enf4 + sge4 + tec4 + out4;

            FazTabela(setorAvaliada, sw);

            //sw.WriteLine("<tr><th>Resposta</th><th>Total</th><th>Cozinha</th><th>Enfermagem</th><th>Serviços gerais</th><th>Técnica</th><th>Outros</th></tr>");
            //sw.WriteLine("<tr><td align=\"left\">" + answer2[0] + "</td><td>" + tot1 + "</td><td>" + Coz1 + "</td><td>" + enf1 + "</td><td>" + sge1 + "</td><td>" + tec1 + "</td><td>" + out1 + "</td></tr>");
            //sw.WriteLine("<tr><td align=\"left\">" + answer2[1] + "</td><td>" + tot2 + "</td><td>" + Coz2 + "</td><td>" + enf2 + "</td><td>" + sge2 + "</td><td>" + tec2 + "</td><td>" + out2 + "</td></tr>");
            //sw.WriteLine("<tr><td align=\"left\">" + answer2[2] + "</td><td>" + tot3 + "</td><td>" + Coz3 + "</td><td>" + enf3 + "</td><td>" + sge3 + "</td><td>" + tec3 + "</td><td>" + out3 + "</td></tr>");
            //sw.WriteLine("<tr><td align=\"left\">" + answer2[3] + "</td><td>" + tot4 + "</td><td>" + Coz4 + "</td><td>" + enf4 + "</td><td>" + sge4 + "</td><td>" + tec4 + "</td><td>" + out4 + "</td></tr>");

            sw.WriteLine("</table>");
            sw.WriteLine("<p>");
        }

        private void FazTabela(string setorAvaliada, StreamWriter sw)
        {
            if (setorAvaliada.Equals("Cozinha"))
            {
                sw.WriteLine("<tr><th>Resposta</th><th>Total</th> <th bgcolor=#ffff00>Cozinha</th> <th>Enfermagem</th><th>Serviços gerais</th><th>Técnica</th><th>Outros</th></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[0] + "</td><td>" + tot1 + "</td><td bgcolor=#ffff00>" + Coz1 + "</td><td>" + enf1 + "</td><td>" + sge1 + "</td><td>" + tec1 + "</td><td>" + out1 + "</td></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[1] + "</td><td>" + tot2 + "</td><td bgcolor=#ffff00>" + Coz2 + "</td><td>" + enf2 + "</td><td>" + sge2 + "</td><td>" + tec2 + "</td><td>" + out2 + "</td></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[2] + "</td><td>" + tot3 + "</td><td bgcolor=#ffff00>" + Coz3 + "</td><td>" + enf3 + "</td><td>" + sge3 + "</td><td>" + tec3 + "</td><td>" + out3 + "</td></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[3] + "</td><td>" + tot4 + "</td><td bgcolor=#ffff00>" + Coz4 + "</td><td>" + enf4 + "</td><td>" + sge4 + "</td><td>" + tec4 + "</td><td>" + out4 + "</td></tr>");
            }
            else if (setorAvaliada.Equals("Enfermagem"))
            {
                sw.WriteLine("<tr><th>Resposta</th><th>Total</th><th>Cozinha</th><th bgcolor=#00ff00>Enfermagem</th><th>Serviços gerais</th><th>Técnica</th><th>Outros</th></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[0] + "</td><td>" + tot1 + "</td><td>" + Coz1 + "</td><td bgcolor=#00ff00>" + enf1 + "</td><td>" + sge1 + "</td><td>" + tec1 + "</td><td>" + out1 + "</td></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[1] + "</td><td>" + tot2 + "</td><td>" + Coz2 + "</td><td bgcolor=#00ff00>" + enf2 + "</td><td>" + sge2 + "</td><td>" + tec2 + "</td><td>" + out2 + "</td></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[2] + "</td><td>" + tot3 + "</td><td>" + Coz3 + "</td><td bgcolor=#00ff00>" + enf3 + "</td><td>" + sge3 + "</td><td>" + tec3 + "</td><td>" + out3 + "</td></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[3] + "</td><td>" + tot4 + "</td><td>" + Coz4 + "</td><td bgcolor=#00ff00>" + enf4 + "</td><td>" + sge4 + "</td><td>" + tec4 + "</td><td>" + out4 + "</td></tr>");
            }
            else if (setorAvaliada.Equals("Serviços gerais"))
            {
                sw.WriteLine("<tr><th>Resposta</th><th>Total</th><th>Cozinha</th><th>Enfermagem</th><th bgcolor=#00ffff>Serviços gerais</th><th>Técnica</th><th>Outros</th></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[0] + "</td><td>" + tot1 + "</td><td>" + Coz1 + "</td><td>" + enf1 + "</td><td bgcolor=#00ffff>" + sge1 + "</td><td>" + tec1 + "</td><td>" + out1 + "</td></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[1] + "</td><td>" + tot2 + "</td><td>" + Coz2 + "</td><td>" + enf2 + "</td><td bgcolor=#00ffff>" + sge2 + "</td><td>" + tec2 + "</td><td>" + out2 + "</td></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[2] + "</td><td>" + tot3 + "</td><td>" + Coz3 + "</td><td>" + enf3 + "</td><td bgcolor=#00ffff>" + sge3 + "</td><td>" + tec3 + "</td><td>" + out3 + "</td></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[3] + "</td><td>" + tot4 + "</td><td>" + Coz4 + "</td><td>" + enf4 + "</td><td bgcolor=#00ffff>" + sge4 + "</td><td>" + tec4 + "</td><td>" + out4 + "</td></tr>");
            }
            else if (setorAvaliada.Equals("Técnica"))
            {
                sw.WriteLine("<tr><th>Resposta</th><th>Total</th><th>Cozinha</th><th>Enfermagem</th><th>Serviços gerais</th><th bgcolor=#FA58F4>Técnica</th><th>Outros</th></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[0] + "</td><td>" + tot1 + "</td><td>" + Coz1 + "</td><td>" + enf1 + "</td><td>" + sge1 + "</td><td bgcolor=#FA58F4>" + tec1 + "</td><td>" + out1 + "</td></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[1] + "</td><td>" + tot2 + "</td><td>" + Coz2 + "</td><td>" + enf2 + "</td><td>" + sge2 + "</td><td bgcolor=#FA58F4>" + tec2 + "</td><td>" + out2 + "</td></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[2] + "</td><td>" + tot3 + "</td><td>" + Coz3 + "</td><td>" + enf3 + "</td><td>" + sge3 + "</td><td bgcolor=#FA58F4>" + tec3 + "</td><td>" + out3 + "</td></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[3] + "</td><td>" + tot4 + "</td><td>" + Coz4 + "</td><td>" + enf4 + "</td><td>" + sge4 + "</td><td bgcolor=#FA58F4>" + tec4 + "</td><td>" + out4 + "</td></tr>");
            }
            else if (setorAvaliada.Equals("Outros"))
            {
                sw.WriteLine("<tr><th>Resposta</th><th>Total</th><th>Cozinha</th><th>Enfermagem</th><th>Serviços gerais</th><th>Técnica</th><th bgcolor=#FF8000>Outros</th></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[0] + "</td><td>" + tot1 + "</td><td>" + Coz1 + "</td><td>" + enf1 + "</td><td>" + sge1 + "</td><td>" + tec1 + "</td><td bgcolor=#FF8000>" + out1 + "</td></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[1] + "</td><td>" + tot2 + "</td><td>" + Coz2 + "</td><td>" + enf2 + "</td><td>" + sge2 + "</td><td>" + tec2 + "</td><td bgcolor=#FF8000>" + out2 + "</td></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[2] + "</td><td>" + tot3 + "</td><td>" + Coz3 + "</td><td>" + enf3 + "</td><td>" + sge3 + "</td><td>" + tec3 + "</td><td bgcolor=#FF8000>" + out3 + "</td></tr>");
                sw.WriteLine("<tr><td align=\"left\">" + answer1[3] + "</td><td>" + tot4 + "</td><td>" + Coz4 + "</td><td>" + enf4 + "</td><td>" + sge4 + "</td><td>" + tec4 + "</td><td bgcolor=#FF8000>" + out4 + "</td></tr>");
            }
        }
    }
}
