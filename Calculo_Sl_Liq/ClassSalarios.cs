using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_Sl_Liq
{
    internal class ClassSalarios
    {
        public static void Inserir(string nome, double sl_bruto,
            double desc_outros,int quant_dep, double inss, 
            double irrf,double sl_liq)
        {
            string sql = "INSERT INTO " +
                "tab_salarios(nome,sl_bruto,desc_outros," +
                "quant_dep,inss,irrf,sl_liq) " +
                "VALUES(@nome,@sl_bruto,@desc_outros," +
                "@quant_dep,@inss,@irrf,@sl_liq)";

            SqlCommand cmd = new SqlCommand(sql,Conexao.conn);
            cmd.Parameters.AddWithValue("nome", nome);
            cmd.Parameters.AddWithValue("sl_bruto", sl_bruto);
            cmd.Parameters.AddWithValue("desc_outros", desc_outros);
            cmd.Parameters.AddWithValue("quant_dep", quant_dep);
            cmd.Parameters.AddWithValue("inss", inss);
            cmd.Parameters.AddWithValue("irrf", irrf);
            cmd.Parameters.AddWithValue("sl_liq", sl_liq);
            cmd.ExecuteNonQuery();

        }

        public static DataTable Selecionar(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql,Conexao.conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        public static void Excluir(int id)
        {
            string sql = "DELETE FROM tab_salarios WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sql, Conexao.conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }

        public static void Atualizar(string nome, int id)
        {
            string sql = "UPDATE tab_salarios SET nome =@nome " +               
                "WHERE id =@id";

            SqlCommand cmd = new SqlCommand(sql, Conexao.conn);
            cmd.Parameters.AddWithValue("nome", nome);        
            cmd.Parameters.AddWithValue("id", id);

            cmd.ExecuteNonQuery();
        }

    }
}
