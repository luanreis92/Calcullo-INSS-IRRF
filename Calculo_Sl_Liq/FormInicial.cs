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

namespace Calculo_Sl_Liq
{
    public partial class FormInicial : Form
    {
        int id = 0;
        public FormInicial()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Código

            }catch (Exception ex)
            {
                // Mensssagem de Erro
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show(Exception.Message);
            }

            string ano = txtAno.Text;
            Conexao.Conectar();
            string sql = $"SELECT * FROM tab_inss where ano = {ano}";
            DataTable dt = new DataTable();
            dt = ClassInss.Selecionar(sql);

            double fx_fin_01 = double.Parse(dt.Rows[0]["fx_sl_final"].ToString());//1302,00
            double aliq_01 = double.Parse(dt.Rows[0]["aliquota"].ToString());//7,5

            double fx_fin_02 = double.Parse(dt.Rows[1]["fx_sl_final"].ToString());//2571,29
            double aliq_02 = double.Parse(dt.Rows[1]["aliquota"].ToString());//9,0

            double fx_fin_03 = double.Parse(dt.Rows[2]["fx_sl_final"].ToString());//3856,94
            double aliq_03 = double.Parse(dt.Rows[2]["aliquota"].ToString());//12

            double fx_fin_04 = double.Parse(dt.Rows[3]["fx_sl_final"].ToString());//7507,49
            double aliq_04 = double.Parse(dt.Rows[3]["aliquota"].ToString());//14

            double slBruto = double.Parse(txtSLB.Text);
            double vlInss = 0;

            if(slBruto <=0)
            {
                throw new FormatException();
            }

            if (slBruto <= fx_fin_01)
            {
                vlInss = (slBruto * aliq_01 / 100);
            }
            else if (slBruto <= fx_fin_02)
            {
                vlInss = ((slBruto - fx_fin_01) * aliq_02 / 100) + (fx_fin_01 * aliq_01 / 100);
            }
            else if (slBruto <= fx_fin_03)
            {
                vlInss = ((slBruto - fx_fin_02) * aliq_03 / 100) + ((fx_fin_02 - fx_fin_01) * aliq_02 / 100) + (fx_fin_01 * aliq_01 / 100);
            }
            else if (slBruto <= fx_fin_04)
            {
                vlInss = ((slBruto - fx_fin_03) * aliq_04 / 100) + ((fx_fin_03 - fx_fin_02) * aliq_03 / 100) + ((fx_fin_02 - fx_fin_01) * aliq_02 / 100) + (fx_fin_01 * aliq_01 / 100);
            }
            else
            {
                vlInss = ((fx_fin_04 - fx_fin_03) * aliq_04 / 100) + ((fx_fin_03 - fx_fin_02) * aliq_03 / 100) + ((fx_fin_02 - fx_fin_01) * aliq_02 / 100) + (fx_fin_01 * aliq_01 / 100);
            }

            txtINSS.Text = vlInss.ToString("n2");

            double quantDep = double.Parse(txtDep.Text) * 189.59;
            double slIr = slBruto - vlInss - quantDep;
            double vlIr = 0;

            string sql2 = $"SELECT * FROM tab_irrf where ano = {ano}";
            DataTable dt2 = ClassInss.Selecionar(sql2);

            double fx_fin_ir_01 = double.Parse(dt2.Rows[0]["fx_sl_final"].ToString());
           
            double fx_fin_ir_02 = double.Parse(dt2.Rows[1]["fx_sl_final"].ToString());
            double aliq_ir_02 = double.Parse(dt2.Rows[1]["aliquota"].ToString());
            double deducao_02 = double.Parse(dt2.Rows[1]["deducao"].ToString());

            double fx_fin_ir_03 = double.Parse(dt2.Rows[2]["fx_sl_final"].ToString());
            double aliq_ir_03 = double.Parse(dt2.Rows[2]["aliquota"].ToString());
            double deducao_03 = double.Parse(dt2.Rows[2]["deducao"].ToString());

            double fx_fin_ir_04 = double.Parse(dt2.Rows[3]["fx_sl_final"].ToString());
            double aliq_ir_04 = double.Parse(dt2.Rows[3]["aliquota"].ToString());
            double deducao_04 = double.Parse(dt2.Rows[3]["deducao"].ToString());

            double aliq_ir_05 = double.Parse(dt2.Rows[4]["aliquota"].ToString());
            double deducao_05 = double.Parse(dt2.Rows[4]["deducao"].ToString());

            if (slIr <= fx_fin_ir_01)
            {
                vlIr = 0;
            }
            else if(slIr <= fx_fin_ir_02)
            {
                vlIr= (slIr *aliq_ir_02 /100) - deducao_02;
            }
            else if( slIr <= fx_fin_ir_03)
            {
                vlIr = (slIr * aliq_ir_03 / 100) - deducao_03;
            }
            else if(slIr <= fx_fin_ir_04)
            {
                vlIr = (slIr * aliq_ir_04 / 100) - deducao_04;
            }
            else if(slIr > fx_fin_ir_04)
            {
                vlIr = (slIr * aliq_ir_05 / 100) - deducao_05;
            }

            txtIr.Text = vlIr.ToString("n2");

            double slLiq = slBruto -vlInss - vlIr - double.Parse(txtDesc.Text) ;
            txtSliq.Text = slLiq.ToString("n2");
            btninicialCalc();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            selecionartudo();
        }
        void selecionartudo()
        {
            txtAno.Text = DateTime.Now.ToString("yyyy");
            Conexao.Conectar();
            string sql = "select * from tab_salarios";
            dataGridView1.DataSource = ClassSalarios.Selecionar(sql);
            button3.Enabled = false;
            button3.BackColor = System.Drawing.Color.White;
            button3.ForeColor = System.Drawing.Color.Black;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Conexao.Conectar();
            ClassSalarios.Inserir(txtNome.Text,double.Parse(txtSLB.Text),
                double.Parse(txtDesc.Text),int.Parse(txtDep.Text),
                double.Parse(txtINSS.Text),double.Parse(txtIr.Text),
                double.Parse(txtSliq.Text));
            MessageBox.Show("Cadastro realizado");
            selecionartudo();           
            
        }

     
        private void inssToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInss tela = new FormInss();           
            tela.ShowDialog();
        }

     

        private void button4_Click(object sender, EventArgs e)
        {
            string coluna = comboBox1.Text;
            string valor = txtSelecionar.Text;
            Conexao.Conectar();
            string sql = $"SELECT * FROM tab_salarios WHERE {coluna} LIKE '{valor}%' ";
            dataGridView1.DataSource = ClassSalarios.Selecionar(sql);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnlimpar();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            double slb =double.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());
            txtNome.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSLB.Text = slb.ToString("n2");
            txtDesc.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtDep.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtINSS.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtIr.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtSliq.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            
            if(id > 0)
            {
                btnselecionado();
                txtSLB.Enabled = false;
                txtDesc.Enabled = false;
                txtDep.Enabled = false;
                button1.Enabled = false;

            }
           

        }
        void btnselecionado()
        {
            button2.Enabled = true;
            button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(59)))));
            button2.ForeColor = System.Drawing.Color.White;
            button5.Enabled = true;
            button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(59)))));
            button5.ForeColor = System.Drawing.Color.White;
            button6.Enabled = true;
            button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(59)))));
            button6.ForeColor = System.Drawing.Color.White;
        }

        void btninicialCalc()
        {
            button3.Enabled = true;//btn Inserir
            button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(59)))));
            button3.ForeColor = System.Drawing.Color.White;
            button2.Enabled = true;//btnLimpar
            button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(59)))));
            button2.ForeColor = System.Drawing.Color.White;
        }

        void btnlimpar()
        {
            txtSLB.Enabled = true;
            txtDesc.Enabled = true;
            txtDep.Enabled = true;
            dataGridView1.DataSource = null;
            txtNome.Clear();
            txtSLB.Clear();
            txtSliq.Clear();
            txtDesc.Text = "0";
            txtINSS.Clear();
            txtDep.Text = "0";
            txtIr.Clear();
            button5.Enabled = false;
            button5.BackColor = System.Drawing.Color.White;
            button5.ForeColor = System.Drawing.Color.Black;
            button6.Enabled = false;
            button6.BackColor = System.Drawing.Color.White;
            button6.ForeColor = System.Drawing.Color.Black;
            button3.Enabled = false;
            button3.BackColor = System.Drawing.Color.White;
            button3.ForeColor = System.Drawing.Color.Black;
            button1.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja Alterar o Nome deste registro?",
              "Atualizar",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                Conexao.Conectar();
                ClassSalarios.Atualizar(txtNome.Text, id);
                MessageBox.Show("Registro Alterado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                selecionartudo();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja excluir ?",
               "Excluir",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                Conexao.Conectar();
                ClassSalarios.Excluir(id);
                MessageBox.Show("Registro excluido","Informação",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                selecionartudo();
            }
        }

        private void sairToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void IrrfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormIrrf tela = new FormIrrf();
            tela.ShowDialog();
        }
    }
}
