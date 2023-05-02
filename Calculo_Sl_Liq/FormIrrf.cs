using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculo_Sl_Liq
{
    public partial class FormIrrf : Form
    {
        int id = 0;
        public FormIrrf()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Conexao.Conectar();
            double fx_inicial = double.Parse(txt_fx_inicial.Text);
            double fx_final = double.Parse(txt_fx_final.Text);
            double aliq_inss = double.Parse(txt_aliq.Text);
            double deducao = double.Parse(txtDeducao.Text);
            int ano = int.Parse(txt_ano_inss.Text);
            ClassIrrf.Inserir(fx_inicial, fx_final, aliq_inss,deducao, ano);
            MessageBox.Show("Faixa cadastrada");
            SelecionarTab();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja excluir ?",
               "Excluir",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                Conexao.Conectar();
                ClassIrrf.Excluir(id);
                MessageBox.Show("Registro excluido");
                SelecionarTab();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Conexao.Conectar();
            double fx_inicial = double.Parse(txt_fx_inicial.Text);
            double fx_final = double.Parse(txt_fx_final.Text);
            double aliq_inss = double.Parse(txt_aliq.Text);
            double deducao = double.Parse(txtDeducao.Text);
            int ano = int.Parse(txt_ano_inss.Text);
            ClassIrrf.Atualizar(fx_inicial, fx_final, aliq_inss,deducao ,ano, id);
            MessageBox.Show("Faixa Atualizada");
            SelecionarTab();
        }

        public void SelecionarTab()
        {
            Conexao.Conectar();
            string sql = "SELECT * FROM tab_irrf";
            dataGridView1.DataSource = ClassInss.Selecionar(sql);
        }
        private void FormIrrf_Load(object sender, EventArgs e)
        {
            SelecionarTab();
        }
    }
}
