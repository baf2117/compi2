using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace C3D
{
    public partial class Ingreso : Form
    {
        private static readonly HttpClient client = new HttpClient();
        public Ingreso()
        {
            InitializeComponent();
        }

        private async void login_Click(object sender, EventArgs e)
        {
            String nombre = user.Text;
            String pass1 = pass.Text;
            var response = await client.GetStringAsync("http://localhost:3000/ingresoc?user=" + nombre + "&pass=" + pass1);
           if (response.Equals("si"))
            {
                Form1.usuario = nombre;
                this.Close();
                MessageBox.Show("Sesión iniciada");
                
            }
            else
            {
                MessageBox.Show("No se puedo ingresar compruebe sus credenciales");
                return;
            }

        }

    }
}
