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
    public partial class Arelaciones : Form
    {
        public Arelaciones()
        {
            InitializeComponent();
            foreach(clasesuml hijo in clasesuml.Lista)
            {
                Clasea.Items.Add(hijo.Nombre);
                Claseb.Items.Add(hijo.Nombre);
            }
        }

        private void Agregar_Click(object sender, EventArgs e)
        {
            String a = Clasea.Text;
            String b = Claseb.Text;
            if (a.Equals("")| b.Equals(""))
            {
                MessageBox.Show("No se puede crear la relacion, falta especificar alguna clase");
                return;
            }

            Relaciones nueva = new Relaciones();
            nueva.tipo = UML.relacion;
            nueva.clasea = a;
            nueva.claseb = b;

            clasesuml.Relaciones.Add(nueva);
            MessageBox.Show("Relacion agregada");
        }
    }
}
