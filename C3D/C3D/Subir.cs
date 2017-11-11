using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C3D
{
    public partial class Subir : Form
    {
        private static readonly HttpClient client = new HttpClient();
        public Subir()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "olc (.olc)|*.olc|tree (.tree)|*.tree";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = true;
            string text = "";
            string nombre="";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) 
            {
               nombre  = openFileDialog1.FileName;
                try
                {
                  text  = File.ReadAllText(nombre);
                    
                }
                catch (IOException)
                {
                }
            }
            if (text.Length > 0)
            {
                String[] name = nombre.Split('\\');
                String name2 = name[name.Length - 1];
                textBox1.Text = name2;
                textBox2.Text = text;

            }
        
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            String nombre = textBox1.Text;
            String conte = textBox2.Text;
            String desc = textBox3.Text;

            var values = new Dictionary<string, string>
            {
               { "nombre", nombre },
               { "cont", conte},
               { "desc",desc },
               { "user",Form1.usuario }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://localhost:3000/subir", content);

            var responseString = await response.Content.ReadAsStringAsync();

            if (responseString.Equals("si"))
            {
                MessageBox.Show("Archivo subido con exito");
                this.Close();
               
            }
            else
            {
                MessageBox.Show("Error inesperado, intente más tarde");
                this.Close();
                return;
            }
        }

        private void Subir_Load(object sender, EventArgs e)
        {

        }
    }
}
