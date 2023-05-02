using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Calculo_Sl_Liq
{
    public partial class FormInss : Form
    {
        int id = 0;
        public FormInss()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormInss_Load(object sender, EventArgs e)
        {
            SelecionarTab();

        }

        public void SelecionarTab()
        {
            Conexao.Conectar();
            string sql = "SELECT * FROM tab_inss";
            dataGridView1.DataSource = ClassInss.Selecionar(sql);
        }
        public void Limpar()
        {
            txt_aliq.Clear();
            txt_ano_inss.Clear();
            txt_fx_final.Clear();
            txt_fx_inicial.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Conexao.Conectar();
            double fx_inicial = Double.Parse(txt_fx_inicial.Text);
            double fx_final = double.Parse(txt_fx_final.Text);
            double aliq_inss = double.Parse(txt_aliq.Text);
            int ano = int.Parse(txt_ano_inss.Text);
            ClassInss.Inserir(fx_inicial,fx_final,aliq_inss,ano);
            MessageBox.Show("Faixa cadastrada");
            SelecionarTab();
            Limpar();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            txt_fx_inicial.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_fx_final.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txt_aliq.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txt_ano_inss.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {   
          var resultado =  MessageBox.Show("Deseja excluir ?",
                "Excluir",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if(resultado == DialogResult.Yes)
            {
                Conexao.Conectar();
                ClassInss.Excluir(id);
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
            int ano = int.Parse(txt_ano_inss.Text);
            ClassInss.Atualizar(fx_inicial, fx_final, aliq_inss, ano,id);
            MessageBox.Show("Atualizado");
            SelecionarTab();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
            
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
