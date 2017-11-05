using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C3D
{
    public partial class Guardar : Form
    {
        public String contenido ="";
        public Guardar(String contenido)
        {
            InitializeComponent();
            Form1 aux = Program.ventana;

            foreach(TreeNode nodo in aux.Directorios.Nodes)
            {
                this.comboBox1.Items.Add(nodo.Text);
            }

            this.contenido = contenido;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String carpeta = comboBox1.Text;
            String nombre = textBox1.Text;
            String tipo = comboBox2.Text;
            Form1 aux = Program.ventana;
            if (carpeta.Equals("") | nombre.Equals("") | tipo.Equals(""))
            {
                MessageBox.Show("No ha llenado todos los campos", "Error",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);

            }else
            {

                foreach (TreeNode nodo in aux.Directorios.Nodes)
                {
                    if (nodo.Text.Equals(carpeta))
                    {
                        String path = nodo.Name + "\\" + nombre + "." + tipo;

                        TreeNode archivo;
                        archivo = new System.Windows.Forms.TreeNode("");
                        archivo.ImageIndex = 1;
                        archivo.Name = path;
                        archivo.Text = nombre + "." + tipo;
                        nodo.Nodes.Add(archivo);

                        string[] lines = contenido.Split('\n');
                        System.IO.File.WriteAllLines(path, lines);
                        if (Form1.bandera)
                        {
                            Form1.bandera = false;
                            goto salir;
                        }

                        TabPage tabPagex = aux.tabControl1.SelectedTab;
                        foreach (FastColoredTextBox color in tabPagex.Controls)
                        {
                            color.Name = path;
                            goto seguir;
                        }
                        seguir:

                        tabPagex.Text = nombre + "." + tipo;
                        tabPagex.Name = path;

                        salir:
                        this.Close();                       
                        return;

                    }
                       
                }

               


            }
        }
    }
}
