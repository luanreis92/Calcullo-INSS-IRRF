using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_Sl_Liq
{
    internal class ClassInss
    {
        public static void Inserir(double fx_sl_inicial,double fx_sl_final,
            double aliq_inss, int anoInss)
        {
            string sql = "INSERT INTO " +
                "tab_inss(fx_sl_inicial,fx_sl_final,aliquota,ano) " +
                "VALUES(@fx_sl_inicial,@fx_sl_final,@aliquota,@ano)";

            SqlCommand cmd = new SqlCommand(sql, Conexao.conn);
            cmd.Parameters.AddWithValue("fx_sl_inicial", fx_sl_inicial);
            cmd.Parameters.AddWithValue("fx_sl_final", fx_sl_final);
            cmd.Parameters.AddWithValue("aliquota", aliq_inss);
            cmd.Parameters.AddWithValue("ano", anoInss);
          
            cmd.ExecuteNonQuery();

        }

        public static DataTable Selecionar(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, Conexao.conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        public static void Excluir(int id)
        {
            string sql = "DELETE FROM tab_inss WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sql, Conexao.conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }

        public static void Atualizar(double fx_sl_inicial, double fx_sl_final,
            double aliq_inss, int anoInss, int id)
        {
            string sql = "UPDATE tab_inss SET fx_sl_inicial =@fx_sl_inicial," +
                "fx_sl_final =@fx_sl_final, aliquota =@aliquota, ano =@ano " +
                "WHERE id =@id";

            SqlCommand cmd = new SqlCommand(sql, Conexao.conn);
            cmd.Parameters.AddWithValue("fx_sl_inicial", fx_sl_inicial);
            cmd.Parameters.AddWithValue("fx_sl_final", fx_sl_final);
            cmd.Parameters.AddWithValue("aliquota", aliq_inss);
            cmd.Parameters.AddWithValue("ano", anoInss);
            cmd.Parameters.AddWithValue("id", id);

            cmd.ExecuteNonQuery();
        }
    }
}
