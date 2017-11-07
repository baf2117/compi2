using Irony.Parsing;
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
    public partial class ReporteG : Form
    {
        public static int arbol = 1;
        public ReporteG()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String Text = Entrada.Text;
            Text = "clase Compi2 {" + Text + "}";
            ParseTreeNode raiz = Sintactico1.analizarolc2(Text);
            if (raiz == null)
            {
                Text = Entrada.Text;
                String[] lista = Text.Split('\n');
                Text = "clase Y2 []:\n";
                foreach(String hijo in lista)
                {
                    Text += "\t" + hijo + "\n";
                }
                raiz = Sintactico1.analizartree2(Text);
                if (raiz != null)
                {
                    Sintactico1.generarImagen3(raiz.ChildNodes[1].ChildNodes[0].ChildNodes[4].ChildNodes[0], arbol);
                    //pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Image = Image.FromFile("C:\\Users\\Brayan\\Desktop\\graph" + arbol + ".jpg");
                }
                else
                {
                    MessageBox.Show("Error Sintacticio o lexico ", "Error Sintacticio o lexico", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }

            }
            else
            {
                Sintactico1.generarImagen3(raiz.ChildNodes[1].ChildNodes[0].ChildNodes[2].ChildNodes[0], arbol);
                //pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = Image.FromFile("C:\\Users\\Brayan\\Desktop\\graph" + arbol + ".jpg");
                


                arbol++;

            }
        }
    }
}
