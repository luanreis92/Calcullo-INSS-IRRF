using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_Sl_Liq
{
    internal class Conexao
    {
        public static SqlConnection conn;
        private static string 
        conexao = "Data Source=SJC0652846W10-1;" +
            "Initial Catalog=Banco;" +
            "User=sa;" +
            "Password=Senac123";
        public static void Conectar()
        {
            conn = new SqlConnection(conexao);
            conn.Open();
        }
        public static void Desconectar()
        {
            conn.Close();
        }

    }
}
