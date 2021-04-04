using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ConsumindoWebApi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Cliente
        {
            public int Codigo { get; set; }
            public string Nome { get; set; }
            public int Tipo { get; set; }
            public DateTime DataCadastro { get; set; }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44302/api/clientes");
                var resposta = await client.GetAsync("");
                string dados = await resposta.Content.ReadAsStringAsync();
                List<Cliente> clientes = new JavaScriptSerializer().Deserialize<List<Cliente>>(dados);
                dataGridView1.DataSource = clientes;
                
            }

        }

        private async void Inserir_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44302/api/clientes");
                Cliente cli = new Cliente();
                cli.Codigo = Convert.ToInt32(textBox1.Text);
                cli.Nome = textBox2.Text;
                cli.DataCadastro = dateTimePicker1.Value;
                cli.Tipo = Convert.ToInt32(textBox3.Text);

                try
                {
                    var resposta = await client.PostAsJsonAsync("", cli);
                    string retorno = await resposta.Content.ReadAsStringAsync();
                    if (resposta.IsSuccessStatusCode)
                        MessageBox.Show("Cliente Inserido");
                    else
                        MessageBox.Show(retorno);
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
        }
    }
}

