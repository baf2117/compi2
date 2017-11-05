using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C3D
{
    public partial class Folder : Form
    {
        public String direccion;
        public Folder(String direccion)
        {
            InitializeComponent();
            this.direccion = direccion;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String nombre = textBox1.Text;
            if (nombre.Equals(""))
            {

                MessageBox.Show("No ha llenado todos los campos", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                this.direccion += "\\" + nombre;
                if (Directory.Exists(this.direccion))
                {
                    MessageBox.Show("La carpeta ya existe", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DirectoryInfo di = Directory.CreateDirectory(this.direccion);

                Form1 aux = Program.ventana;
                TreeNode archivo;
                archivo = new System.Windows.Forms.TreeNode("");
                archivo.ImageIndex = 0;
                archivo.Name = this.direccion;
                archivo.Text = textBox1.Text;
                aux.Directorios.Nodes.Add(archivo);
                this.Close();
            }
            catch (Exception ee)
            {

            }
        }
    }
}
