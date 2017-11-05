using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3D
{
    class clasesuml
    {
        public String Nombre;
        public ArrayList atributos = new ArrayList();
        public static ArrayList Lista = new ArrayList();
        public static ArrayList Relaciones = new ArrayList();


        public clasesuml(String Nombre, ArrayList atributos)
        {
            this.Nombre = Nombre;
            this.atributos = atributos;
        }

        public void agregar(String Nombre, ArrayList atributos) {

            clasesuml nuevo = new clasesuml(Nombre, atributos);
            Lista.Add(nuevo);
        }
    }
}
