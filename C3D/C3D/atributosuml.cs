using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3D
{
    class atributosuml
    {
        public String Nombre;
        public int tipo; // atributo = 1 ; metodo = 2 ; funcion = 3;
        public String visibilidad;
        public String cuerpo;
        public String tipo2;
        public ArrayList tipopara = new ArrayList();
        public ArrayList parametros = new ArrayList();
    }
}
