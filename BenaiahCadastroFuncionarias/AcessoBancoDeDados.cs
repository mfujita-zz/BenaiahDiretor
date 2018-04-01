using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenaiahCadastroFuncionarias
{
    class AcessoBancoDeDados
    {
        public string BancoDados()
        {
            return "Server=ULTRABOOK\\SQLEXPRESS;Database=LarBenaiah;Trusted_Connection=True;";
        }
    }
}
