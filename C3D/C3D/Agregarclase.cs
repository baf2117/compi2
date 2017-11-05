using System;
using System.Collections;
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
    public partial class Agregarclase : Form
    {
        public static ArrayList Atributos = new ArrayList();
        public static ArrayList tipopara = new ArrayList();
        public static ArrayList parametros = new ArrayList();
        public Agregarclase()
        {
            InitializeComponent();
            Atributos.Clear();
            tipopara.Clear();
            parametros.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String nombre = Nombrea.Text;
            String tipo = tipoa.Text;
            String visi = visia.Text;

            Nombrea.Text="";


            if (nombre.Equals("") | tipo.Equals("") | visi.Equals(""))
            {
                MessageBox.Show("Asegurese de tener todos los campos llenos");
            }else
            {
                atributosuml nuevo = new atributosuml();
                nuevo.Nombre = nombre;
                nuevo.tipo = 1;
                nuevo.tipo2 = tipo;
                nuevo.visibilidad = visi;

                foreach(atributosuml hijo in Atributos)
                {
                    if (hijo.Nombre.Equals(nombre))
                    {
                        MessageBox.Show("Existe otro atributo con el mismo nombre");
                        return;
                    }


                }
                Atributos.Add(nuevo);
                MessageBox.Show("Atributo agregado");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String nombre = nombrec.Text;
            if (nombre.Equals(""))
            {
                MessageBox.Show("No se puede crear una clase sin nombre");
                return;
            }
            foreach (clasesuml hijo in clasesuml.Lista)
            {
                if (hijo.Nombre.Equals(nombre))
                {
                    MessageBox.Show("Ya existe una clase con el nombre "+nombre);
                    return;

                }

            }
            atributosuml aux;
            ArrayList listaux = new ArrayList();
            foreach(atributosuml hijo in Atributos)
            {
                aux = new atributosuml();
                aux.Nombre = hijo.Nombre;
                aux.parametros = hijo.parametros;
                aux.tipo = hijo.tipo;
                aux.tipo2 = hijo.tipo2;
                aux.tipopara = hijo.tipopara;
                aux.visibilidad = hijo.visibilidad;
                listaux.Add(aux);   
            }


            clasesuml nueva = new clasesuml(nombre, listaux);
            clasesuml.Lista.Add(nueva);
            this.Close();
            MessageBox.Show("Clase agregada");
           
            UML.clases.Items.Add(nombre);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            String nombre = nombrem.Text;
            String tipo = tipom.Text;
            String visi = visim.Text;
            nombrem.Text = "";

            if (nombre.Equals("") | tipo.Equals("") | visi.Equals(""))
            {
                MessageBox.Show("Asegurese de tener todos los campos llenos");
                return;
            }
            foreach (atributosuml hijo in Atributos)
            {
                if (hijo.Nombre.Equals(nombre))
                {
                    MessageBox.Show("Existe un atributo con el el nombre "+nombre);
                    return;
                }


            }

            atributosuml nuevo = new atributosuml();
            nuevo.Nombre = nombre;
            nuevo.tipo2 = tipo;
            nuevo.visibilidad = visi;
            if (tipo.Equals("void"))
            {
                nuevo.tipo = 2;
            }
            else
            {
                nuevo.tipo = 3;

            }
            ArrayList auxp = new ArrayList();
            ArrayList auxtp = new ArrayList();

            foreach(String hijo in parametros)
            {
                auxp.Add(hijo);
            }
            foreach (String hijo in tipopara)
            {
                auxtp.Add(hijo);
            }

            nuevo.parametros = auxp;
            nuevo.tipopara = auxtp;
            
            Atributos.Add(nuevo);
            
            MessageBox.Show("Metodo agregado");
            parametros.Clear();
            tipopara.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String nombre = nombrep.Text;
            String tipo = tipop.Text;
            nombrep.Text = "";

            if (nombre.Equals("") | tipo.Equals(""))
            {
                MessageBox.Show("Asegurese de tener todos los campos llenos");
                return;
            }

            foreach (String hijo in parametros)
            {
                if (hijo.Equals(nombre))
                {
                    MessageBox.Show("Existe un parametro con el el nombre " + nombre);
                    return;
                }
            }

            parametros.Add(nombre);
            tipopara.Add(tipo);

            MessageBox.Show("Parametro agregado");

        }
    }
}
