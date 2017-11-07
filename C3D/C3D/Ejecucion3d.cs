using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Interpreter;
using Irony.Parsing;
using System.Collections;
using System.IO;

namespace C3D
{
    class Ejecucion3d
    {
        public static int temporal = 1;
        public static int etiqueta = 1;
        public static String cadenota = "";
        public static int sp = 0;
        public static int hp = 0;
        public static int selfp = -1;
        public static int selfp2 = -1;
        public static int am = 0;
        public static int al = 0;
        public static int pm = 0;
        public static int pl = 0;
        public String nombre = "";
        public String valor = "";
        public static LinkedList<Ejecucion3d> stack = new LinkedList<Ejecucion3d>();
        public static LinkedList<Ejecucion3d> heap = new LinkedList<Ejecucion3d>();
        public static LinkedList<Ejecucion3d> ds = new LinkedList<Ejecucion3d>();
        public static LinkedList<Ejecucion3d> Pila = new LinkedList<Ejecucion3d>();
        public static LinkedList<ParseTreeNode> linea = new LinkedList<ParseTreeNode>();
        


        public static String generatemp() {

            String ret = "t" + temporal.ToString();
            temporal++;
            return ret;
        }

        public static String generalabel()
        {

            String ret = "L" + etiqueta.ToString();
            etiqueta++;
            return ret;
        }

        public static void escribir3d() {

            cadenota += "fin:\n";

            string path = @"C:\Users\Brayan\Desktop\3DA\ejecutable.3d";

            Program.ventana.tresd.Text = cadenota;

            try
            {

                // Delete the file if it exists.
                if (File.Exists(path))
                {
                    // Note that no lock is put on the
                    // file and the possibility exists
                    // that another process could do
                    // something with it between
                    // the calls to Exists and Delete.
                    File.Delete(path);
                }

                // Create the file.
                using (FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(cadenota);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                // Open the stream and read it back.
                /*using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }*/
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            
         

        }

        public void analizar3D(String cadena)
        {
            _3DG gramatica = new _3DG();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parse = new Parser(lenguaje);
            ParseTree arbol = parse.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;

            if (raiz != null)
            {
                //  linea = linealizar(raiz);
                //  Sintactico1.generarImagen(raiz);
                raiz = reparar(raiz);
                //optimizar(raiz);
                linea = linealizar(raiz);
                ejecutar(linealizar(raiz));
                
               

            }
           
            
        }

        public LinkedList<ParseTreeNode> linealizar(ParseTreeNode raiz)
        {
            LinkedList<ParseTreeNode> ret = new LinkedList<ParseTreeNode>();
            foreach(ParseTreeNode  hijo in raiz.ChildNodes)
            {
                if (hijo.Term.Name.Equals("METODO"))
                {
                    ret.AddLast(hijo.ChildNodes.ElementAt(0));
                    
                    LinkedList<ParseTreeNode> aux = linealizar(hijo.ChildNodes.ElementAt(4));

                    foreach(ParseTreeNode hijo2 in aux)
                    {
                        ret.AddLast(hijo2);
                    }

                }else
                {

                    ret.AddLast(hijo);
                }


            }




            return ret;
        }

        public void ejecutar(LinkedList<ParseTreeNode> raiz) {

            String nombre = raiz.ElementAt(0).Term.Name;
            int pos=0;
            ParseTreeNode accion = raiz.ElementAt(pos);
            while (!nombre.Equals("fin"))
            {
                switch (nombre)
                {
                    case "ASIGNACIONT":
                        Asignartempo(accion);
                        pos++;
                        break;
                    case "ASIGNAHP":
                        asignahp(accion);
                        pos++;
                        break;
                    case "ASIGNASP":
                        asignasp(accion);
                        pos++;
                        break;
                    case "IF":
                        pos = IFF(accion, pos);
                        break;
                    case "IFFALSE":
                        pos = IFFA(accion, pos);
                        break;
                    case "ASIGNASEL2":
                        //     ejec.asignaselfp2(accion);
                        pos++;
                        break;
                    case "ASIGNASEL1":
                        asignaselfp(accion);
                        pos++;
                        break;
                    case "ASIGNADS":
                        //    ejec.asignads(accion);
                        pos++;
                        break;
                    case "ASIGNASTACK":
                        asignastack(accion);
                        pos++;
                        break;
                    case "ASIGNAHEAP":
                        asignaheap(accion);
                        pos++;
                        break;
                    case "LLAMADA":
                        pos = llamada(accion.ChildNodes.ElementAt(0).Token.Text, pos + 1);
                        pos++;
                        break;
                    case "GOTO":
                        pos = gotoo(accion.ChildNodes.ElementAt(1).Token.Text);
                        if (accion.ChildNodes.ElementAt(1).Token.Text.Equals("fin"))
                        {
                            goto fin;
                        }                  
                        pos++;
                        break;

                    case "IMPRIMIR":
                        imprimir(accion.ChildNodes.ElementAt(3));
                        pos++;//por el momento
                        break;

                    case "RETORNO":
                        pos = regreso();
                       
                        break;

                    default:
                        pos++;
                        break;

                }
                if (pos == raiz.Count)
                {
                   
                    goto fin;

                }
                accion = raiz.ElementAt(pos);
                nombre = accion.Term.Name;


            }
            fin:;
        }

        public void ejecutardeb(LinkedList<ParseTreeNode> raiz)
        {



            String nombre = raiz.ElementAt(0).Term.Name;
            int pos = 0;
            ParseTreeNode accion = raiz.ElementAt(pos);
            while (!nombre.Equals("fin"))
            {
                switch (nombre)
                {
                    case "ASIGNACIONT":
                        Asignartempo(accion);
                        pos++;
                        break;
                    case "ASIGNAHP":
                        asignahp(accion);
                        pos++;
                        break;
                    case "ASIGNASP":
                        asignasp(accion);
                        pos++;
                        break;
                    case "ASIGNASEL2":
                        asignaselfp2(accion);
                        pos++;
                        break;
                    case "ASIGNASEL1":
                        asignaselfp(accion);
                        pos++;
                        break;
                    case "ASIGNADS":
                        asignads(accion);
                        pos++;
                        break;
                    case "ASIGNASTACK":
                        asignastack(accion);
                        pos++;
                        break;
                    case "ASIGNAHEAP":
                        asignaheap(accion);
                        pos++;
                        break;
                    case "LLAMADA":
                        pos = llamada(accion.ChildNodes.ElementAt(0).Token.Text, pos + 1);
                        pos++;
                        break;
                    case "GOTO":
                        pos = gotoo(accion.ChildNodes.ElementAt(1).Token.Text);
                        pos++;
                        break;

                    case "IMPRIMIR":
                        break;

                    default:
                        pos = regreso();
                        break;


                }
                accion = raiz.ElementAt(pos);
                nombre = accion.Term.Name;


            }
        }

        public void asignasp(ParseTreeNode raiz)
        {
            sp =Convert.ToInt32(Exp(raiz.ChildNodes.ElementAt(2)));
        }

        public void asignahp(ParseTreeNode raiz)
        {
            hp = Convert.ToInt32(Exp(raiz.ChildNodes.ElementAt(2)));
        }

        public void asignaselfp(ParseTreeNode raiz)
        {
            selfp = Convert.ToInt32(Exp(raiz.ChildNodes.ElementAt(2)));
        }

        public void asignaselfp2(ParseTreeNode raiz)
        {
            selfp2 = Convert.ToInt32(Exp(raiz.ChildNodes.ElementAt(2)));
        }

        public void asignastack(ParseTreeNode raiz)
        {
            String accion = raiz.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Text;
            String accion2 = raiz.ChildNodes.ElementAt(5).ChildNodes.ElementAt(0).Token.Text;
            int pos = 0;
            
            bool bandera = true;
            double val = 0;

            switch (accion)
            {
                case "sp":
                    pos = sp;
                    break;
                case "hp":
                    pos = hp;
                    break;
                case "selfp":
                    pos = selfp;
                    break;
                case "selfp2":
                    pos = selfp2;
                    break;
                default:
                    String aux = buscarpila(accion);
                    if (aux.Equals("00"))
                    {
                        //NULL POINTER 


                    }
                    else
                    {
                        pos = Convert.ToInt32(aux);
                    }
                    break;
            }

            switch (accion2)
            {
                case "sp":
                    val = sp;
                    break;
                case "hp":
                    val = hp;
                    break;
                case "selfp":
                    val = selfp;
                    break;
                case "selfp2":
                    val = selfp2;
                    break;
                default:
                    String aux = buscarpila(accion2);
                    if (aux.Equals("valor"))
                    {
                        bandera = false;
                        
                    }
                    else
                    {
                        val = Convert.ToDouble(aux);
                    }
                    break;

            }

            int tama = stack.Count;
            Ejecucion3d nuevo;
            if (pos > (tama - 1))
            {
                for(int i = pos; i <= tama; i++)
                {
                    nuevo = new Ejecucion3d();
                    stack.AddLast(nuevo);
                }

            }
            nuevo = new Ejecucion3d();
            if (bandera)
            {
                stack.ElementAt(pos).valor = val.ToString();
            }else
            {
                stack.ElementAt(pos).valor = accion2;
            }

            stack.AddLast(nuevo);

            


        }

        public void asignaheap(ParseTreeNode raiz)
        {
            String accion = raiz.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Text;
            String accion2 = raiz.ChildNodes.ElementAt(5).ChildNodes.ElementAt(0).Token.Text;
            int pos = 0;
            bool bandera = true;
            double val = 0;

            switch (accion)
            {
                case "sp":
                    pos = sp;
                    break;
                case "hp":
                    pos = hp;
                    break;
                case "selfp":
                    pos = selfp;
                    break;
                case "selfp2":
                    pos = selfp2;
                    break;
                default:
                    String aux = buscarpila(accion);
                    if (aux.Equals("00"))
                    {
                        //NULL POINTER 


                    }
                    else
                    {
                        pos = Convert.ToInt32(aux);
                    }
                    break;
            }

            switch (accion2)
            {
                case "sp":
                    val = sp;
                    break;
                case "hp":
                    val = hp;
                    break;
                case "selfp":
                    val = selfp;
                    break;
                case "selfp2":
                    val = selfp2;
                    break;
                default:
                    String aux = buscarpila(accion2);
                    if (aux.Equals("valor"))
                    {
                        bandera = false;

                    }
                    else
                    {
                        val = Convert.ToDouble(aux);
                    }
                    break;

            }
            int tama = stack.Count;
            Ejecucion3d nuevo;
            
            if (pos > (tama - 1))
            {
                for (int i = pos; i <= tama; i++)
                {
                    nuevo = new Ejecucion3d();
                    heap.AddLast(nuevo);
                }

            }
            nuevo = new Ejecucion3d();
            heap.AddLast(nuevo);
            nuevo = new Ejecucion3d();
            heap.AddLast(nuevo);
            nuevo = new Ejecucion3d();
            heap.AddLast(nuevo);
            if (bandera)
            {
                heap.ElementAt(pos).valor = val.ToString();
            }
            else
            {
                heap.ElementAt(pos).valor = accion2;
            }
            

            


        }

        public void asignads(ParseTreeNode raiz)
        {
            String accion = raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text;
            String accion2 = raiz.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Text;
            int pos = 0;
            double val = 0;

            switch (accion)
            {
                case "sp":
                    pos = sp;
                    break;
                case "hp":
                    pos = hp;
                    break;
                case "selfp":
                    pos = selfp;
                    break;
                case "selfp2":
                    pos = selfp2;
                    break;
                default:
                    String aux = buscarpila(accion2);
                    if (aux.Equals("00"))
                    {
                        //NULL POINTER 


                    }
                    else
                    {
                        pos = Convert.ToInt32(aux);
                    }
                    break;
            }

            switch (accion2)
            {
                case "sp":
                    val = sp;
                    break;
                case "hp":
                    val = hp;
                    break;
                case "selfp":
                    val = selfp;
                    break;
                case "selfp2":
                    val = selfp2;
                    break;
                default:
                    String aux = buscarpila(accion2);
                    val = Convert.ToDouble(aux);
                    break;

            }

            ds.ElementAt(0).valor = val.ToString();


        }

        public String Exp(ParseTreeNode E)
        {
            String ret = "00";
            String val = "";
            if (E.ChildNodes.Count == 1)
            {
                ParseTreeNode VALOR = E.ChildNodes.ElementAt(0);

                if (VALOR.ChildNodes.Count == 1)
                {
                    val = VALOR.ChildNodes.ElementAt(0).Term.Name;
                    if (val.Equals("numero"))
                    {
                        ret =VALOR.ChildNodes.ElementAt(0).Token.Text;
                    }
                    else if (val.Equals("decimal"))
                    {
                        ret = VALOR.ChildNodes.ElementAt(0).Token.Text;
                    }else if(val.Equals("temporal"))
                    {
                        ret = buscarpila(VALOR.ChildNodes.ElementAt(0).Token.Text);
                    }else
                    {
                        val = VALOR.ChildNodes.ElementAt(0).Token.Text;
                        
                        if (val.Equals("sp"))
                        {
                            ret = sp.ToString();
                        }else if (val.Equals("hp"))
                        {
                            ret = hp.ToString();
                        }else if(val.Equals("selfp2"))
                        {
                            ret = selfp2.ToString();

                        }else
                        {
                            ret = selfp.ToString();

                        }

                    }


                }
                else
                {
                    String accion = VALOR.ChildNodes.ElementAt(0).Token.Text;
                    String accion2  = VALOR.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Text;
                    int pos = 0;

                    switch (accion2)
                    {
                        case "sp":
                            pos = sp;
                            break;
                        case "hp":
                            pos = hp;
                            break;
                        case "selfp":
                            pos = selfp;
                            break;
                        case "selfp2":
                            pos = selfp2;
                            break;
                        default:
                            String aux = buscarpila(accion2);
                            if (aux.Equals("00"))
                            {
                                //NULL POINTER 


                            }else
                            {
                                pos = Convert.ToInt32(aux);
                            }
                            break;
                    }

                    switch (accion)
                    {
                        case "stack":
                            ret =  stack.ElementAt(pos).valor;
                            break;
                        case "heap":
                            ret = heap.ElementAt(pos).valor;
                            break;
                        default:
                            ret = ds.ElementAt(pos).valor;
                            break;
                    }

                }

              

            }else
            {
                ParseTreeNode VALOR = E.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);
                ParseTreeNode VALOR2 = E.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0);
                double val1 = 0;
                double val2 = 0;
                String accion = E.ChildNodes.ElementAt(1).Token.Text;
                
                switch (VALOR.Token.Text)
                {
                    case "sp":
                        val1 = sp;
                        break;
                    case "hp":
                        val1 = hp;
                        break;
                    case "selfp":
                        val1 = selfp;
                        break;
                    case "selfp2":
                        val1 = selfp2;
                        break;
                   
                    default:
                        String accion3 = VALOR.Term.Name;
                        switch (accion3)
                        {
                            case "numero":
                            case "decimal":
                                val1 = Convert.ToDouble(VALOR.Token.Text);
                                break;
                            default:
                                val1 = Convert.ToDouble(buscarpila(VALOR.Token.Text));
                                break;
                        }
                        
                        break;
                }

                switch (VALOR2.Token.Text)
                {
                    case "sp":
                        val2 = sp;
                        break;
                    case "hp":
                        val2 = hp;
                        break;
                    case "selfp":
                        val2 = selfp;
                        break;
                    case "selfp2":
                        val2 = selfp2;
                        break;
                    default:
                        String accion3 = VALOR2.Term.Name;
                        switch (accion3)
                        {
                            case "numero":
                            case "decimal":
                                val2 = Convert.ToDouble(VALOR2.Token.Text);
                                break;
                            default:
                                val2 = Convert.ToDouble(buscarpila(VALOR2.Token.Text));
                                break;
                        }
                        break;
                }

                switch (accion)
                {
                    case "+":
                        val1 =val1 +val2;
                        break;
                    case "-":
                        val1 = val1 - val2;
                        break;
                    case "/":
                        val1 = val1 / val2;
                        break;
                    case "*":
                        val1 = val1 * val2;
                        break;
                    
                }
                ret = val1.ToString();

            }

            return ret;
        }

        public void Asignartempo(ParseTreeNode raiz)
        {
            String tempo = raiz.ChildNodes.ElementAt(0).Token.Text;
            int pos = buscarpilapos(tempo);
            String valor = Exp(raiz.ChildNodes.ElementAt(2));
            
            if (pos == -1)
            {
                Ejecucion3d nuevo = new Ejecucion3d();
                nuevo.valor = valor;
                nuevo.nombre = tempo;
                Pila.AddLast(nuevo);
                pm++;
                

            }else
            {
                Pila.ElementAt(pos).valor = valor.ToString();

            }



        }

        public String buscarpila(String tempo)
        {
            String ret = "valor";
            int limite1 = am;
            int limite2 = pm;

            int tama = linea.Count();

            for(int i = limite1; i < limite2; i++)
            {
                if (Pila.ElementAt(i).nombre.Equals(tempo))
                {
                    if (Pila.ElementAt(i).valor.Equals("\\0")){
                        return "98596321789456321.966997812345";
                    }
                    return Pila.ElementAt(i).valor;
                }

            }

            return ret;

        }

        public int buscarpilapos(String tempo)
        {
            
            int limite1 = am;
            int limite2 = pm;

            int tama = linea.Count();

            for (int i = limite1; i < limite2; i++)
            {
                if (Pila.ElementAt(i).nombre.Equals(tempo))
                {
                    return i;
                }

            }
           
            return -1;

        }

        public int llamada(String label,int pos)
        {
            int tama = linea.Count();
            String val = "";
            Ejecucion3d nuevo;

            for (int i = 0; i < tama; i++)
            {
               val = linea.ElementAt(i).Term.Name;

                if (val.Equals("label"))
                {

                    val = linea.ElementAt(i).Token.Text;
                    if (val.Equals(label)){

                        nuevo = new Ejecucion3d();
                        nuevo.valor = pos.ToString();
                        nuevo.nombre = "pos";
                        Pila.AddLast(nuevo);
                        nuevo = new Ejecucion3d();
                        nuevo.valor = am.ToString();
                        nuevo.nombre = "am";
                        Pila.AddLast(nuevo);
                        nuevo = new Ejecucion3d();
                        nuevo.valor = pm.ToString();
                        nuevo.nombre = "pm";
                        Pila.AddLast(nuevo);
                        
                        pm = pm + 3;
                        am = pm;
                        return i;

                    }

                }

            }
            return 0;
        }

        public int gotoo(String label)
        {
            int tama = linea.Count();
            String val = "";
            Ejecucion3d nuevo;

            for (int i = 0; i < tama; i++)
            {
                val = linea.ElementAt(i).Term.Name;

                if (val.Equals("ETIQUETA")|val.Equals("label"))
                {
                    if (val.Equals("label"))
                    {
                        val = linea.ElementAt(i).Token.Text;
                    }else
                    {
                        val = linea.ElementAt(i).ChildNodes.ElementAt(0).Token.Text;
                    }
                    String[] val1 = val.Split(',');

                    foreach (String val22 in val1)
                    { 
                        if (val22.Equals(label))
                        {
                            return i;

                        }
                    }
                }

            }
            return 0;
        }

        public int regreso()
        {
            int pos = am;
            int regreso = 0;
            pm = Convert.ToInt32(Pila.ElementAt(pos - 1).valor);
            am = Convert.ToInt32(Pila.ElementAt(pos - 2).valor);
            regreso= Convert.ToInt32(Pila.ElementAt(pos - 3).valor);
            int tama = Pila.Count();
            for(int i= tama - 1; i > pm-1; i--)
            {
                Pila.RemoveLast();

            }



            return regreso;

        }

        public void imprimir(ParseTreeNode VALOR)
        {
            String val = buscarpila(VALOR.ChildNodes.ElementAt(0).Token.Text);
            int vali = Convert.ToInt32(val);
            char valc = (char)vali;
            val = valc.ToString();
            Program.ventana.Consola.Text += val;
        }

        public void optimizar(ParseTreeNode raiz) {
            LinkedList<ParseTreeNode> lista = linealizar(raiz);
            LinkedList<ParseTreeNode> listita = new LinkedList<ParseTreeNode>();
            LinkedList<ParseTreeNode> listita2 = new LinkedList<ParseTreeNode>();

            int mirilla = 10;
            int tama = lista.Count;
            int conta = 0;
            int pos;
            while (mirilla < tama)
            {
                pos = 0;
                listita2 = new LinkedList<ParseTreeNode>();
                
                while (conta < tama)
                {
                  for(int i = 0; i < mirilla; i++)
                    {
                        if (pos < tama)
                        {
                            listita.AddLast(lista.ElementAt(pos));
                        }
                         pos++;
                        conta++;
                    }
                    listita = optimizar2(listita);
                    foreach (ParseTreeNode hijo in listita)
                    {
                        listita2.AddLast(hijo);
                    }
                    listita.Clear();

                }
                lista = listita2;
                tama = lista.Count();
                conta = 0;
                mirilla *= 2;
            }
            lista = optimizar2(lista);
            lista = regla22(lista);
            linea = lista;
            //ejecutar(lista);
            String candenota2 = cadenaop(lista);
            Program.ventana.optimizado.Text = candenota2;
            

        }

        public LinkedList<ParseTreeNode>  optimizar2(LinkedList<ParseTreeNode> raiz)
        {

            LinkedList<ParseTreeNode> lista = new LinkedList<ParseTreeNode>();
            ParseTreeNode aux,aux2,aux3,hijo;
            String val1, val2,val11,val22;
            Boolean salto = true;
            Boolean agregar = true;
            int pos = 0;
           
            for(int j = 0; j<raiz.Count;j++)
            {
                hijo = raiz.ElementAt(j);
                switch (hijo.Term.Name)
                {
                    case "ASIGNASP":
                        if (hijo.ChildNodes.ElementAt(2).ChildNodes.Count > 1)
                        {
                            aux = optial(hijo.ChildNodes.ElementAt(2));
                            aux2 = hijo.ChildNodes.ElementAt(3);
                            hijo.ChildNodes.RemoveAt(2);
                            hijo.ChildNodes.RemoveAt(2);
                            hijo.ChildNodes.Add(aux);
                            hijo.ChildNodes.Add(aux2);
                            lista.AddLast(hijo);

                        }
                        else
                        { aux = hijo.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);
                            if (!aux.Token.Text.Equals("sp"))
                            {

                                lista.AddLast(hijo);
                                val1 = "sp";
                                val2 = aux.Token.Text;
                                for (int i = pos+1; i < raiz.Count; i++)
                                {

                                    aux2 = raiz.ElementAt(i);
                                    if (aux2.Term.Name.Equals("ETIQUETA"))
                                    {
                                        salto = false;
                                    }
                                    else
                                    {
                                        if (aux2.Term.Name.Equals("label") || aux2.Term.Name.Equals("RETORNO")) {

                                            goto label;
                                        }
                                        if (aux2.ChildNodes.ElementAt(2).ChildNodes.Count == 1 && !aux2.Term.Name.Equals("GOTO")&& aux2.Term.Name.Equals("ETIQUETA")&& aux2.Term.Name.Equals("IF"))
                                        {
                                            val22 = aux2.ChildNodes.ElementAt(0).Token.Text;
                                            val11 = aux2.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                                            if (val22.Equals(val2) && val11.Equals(val1) && salto)
                                            {
                                                agregar = false;
                                                raiz.Remove(aux2);

                                            }

                                        }else
                                        {
                                            val22 = aux2.ChildNodes.ElementAt(0).Token.Text;
                                            if (val22.Equals(val2))
                                            {
                                                salto = false;
                                            }

                                        }
                                        label:;

                                    }
                                }


                            }

                        }


                        pos++;
                        break;
                    case "ASIGNAHP":
                        if (hijo.ChildNodes.ElementAt(2).ChildNodes.Count > 1)
                        {
                            aux = optial(hijo.ChildNodes.ElementAt(2));
                            aux2 = hijo.ChildNodes.ElementAt(3);
                            hijo.ChildNodes.RemoveAt(2);
                            hijo.ChildNodes.RemoveAt(2);
                            hijo.ChildNodes.Add(aux);
                            hijo.ChildNodes.Add(aux2);
                            lista.AddLast(hijo);
                        }
                        else
                        {
                            aux = hijo.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);
                            if (!aux.Token.Text.Equals("hp"))
                            {
                                lista.AddLast(hijo);
                                val1 = "hp";
                                val2 = aux.Token.Text;
                                for (int i = pos+1; i < raiz.Count; i++)
                                {

                                    aux2 = raiz.ElementAt(i);
                                    if (aux2.Term.Name.Equals("ETIQUETA"))
                                    {
                                        salto = false;
                                    }
                                    else
                                    {
                                        if (aux2.Term.Name.Equals("label") || aux2.Term.Name.Equals("RETORNO"))
                                        {
                                            goto label2;
                                        }

                                        if (aux2.ChildNodes.ElementAt(2).ChildNodes.Count == 1 && !aux2.Term.Name.Equals("GOTO") && aux2.Term.Name.Equals("ETIQUETA") && aux2.Term.Name.Equals("IF"))
                                        {
                                            val22 = aux2.ChildNodes.ElementAt(0).Token.Text;
                                            val11 = aux2.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                                            if (val22.Equals(val2) && val11.Equals(val1) && salto)
                                            {
                                                agregar = false;
                                                raiz.Remove(aux2);

                                            }

                                        }
                                        else
                                        {
                                            val22 = aux2.ChildNodes.ElementAt(0).Token.Text;
                                            if (val22.Equals(val2))
                                            {
                                                salto = false;
                                            }

                                        }
                                        label2:;
                                    }
                                }
                            }

                        }


                        pos++;
                        break;
                    case "ASIGNASEL1":
                        if (hijo.ChildNodes.ElementAt(2).ChildNodes.Count > 1)
                        {
                            aux = optial(hijo.ChildNodes.ElementAt(2));
                            aux2 = hijo.ChildNodes.ElementAt(3);
                            hijo.ChildNodes.RemoveAt(2);
                            hijo.ChildNodes.RemoveAt(2);
                            hijo.ChildNodes.Add(aux);
                            hijo.ChildNodes.Add(aux2);
                            lista.AddLast(hijo);
                        }
                        else
                        {
                            aux = hijo.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);
                            if (!aux.Token.Text.Equals("selfp"))
                            {
                                lista.AddLast(hijo);
                                val1 = "selfp";
                                val2 = aux.Token.Text;
                                for (int i = pos+1; i < raiz.Count; i++)
                                {

                                    aux2 = raiz.ElementAt(i);
                                    if (aux2.Term.Name.Equals("ETIQUETA"))
                                    {
                                        salto = false;
                                    }
                                    else
                                    {
                                        if (aux2.Term.Name.Equals("label") || aux2.Term.Name.Equals("RETORNO"))
                                        {
                                            goto label3;
                                        }
                                        if (aux2.ChildNodes.ElementAt(2).ChildNodes.Count == 1 && !aux2.Term.Name.Equals("GOTO") && aux2.Term.Name.Equals("ETIQUETA") && aux2.Term.Name.Equals("IF"))
                                        {
                                            val22 = aux2.ChildNodes.ElementAt(0).Token.Text;
                                            val11 = aux2.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                                            if (val22.Equals(val2) && val11.Equals(val1) && salto)
                                            {
                                                agregar = false;
                                                raiz.Remove(aux2);

                                            }

                                        }
                                        else
                                        {
                                            val22 = aux2.ChildNodes.ElementAt(0).Token.Text;
                                            if (val22.Equals(val2))
                                            {
                                                salto = false;
                                            }

                                        }
                                        label3:;
                                    }
                                }
                            }

                        }


                        pos++;
                        break;
                    case "ASIGNASEL2":
                        if (hijo.ChildNodes.ElementAt(2).ChildNodes.Count > 1)
                        {
                            aux = optial(hijo.ChildNodes.ElementAt(2));
                            aux2 = hijo.ChildNodes.ElementAt(3);
                            hijo.ChildNodes.RemoveAt(2);
                            hijo.ChildNodes.RemoveAt(2);
                            hijo.ChildNodes.Add(aux);
                            hijo.ChildNodes.Add(aux2);
                            lista.AddLast(hijo);
                        }
                        else
                        {
                            aux = hijo.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);
                            if (!aux.Token.Text.Equals("selfp2"))
                            {
                                lista.AddLast(hijo);
                                val1 = "selfp2";
                                val2 = aux.Token.Text;
                                for (int i = pos + 1; i < raiz.Count; i++)
                                {

                                    aux2 = raiz.ElementAt(i);
                                    if (aux2.Term.Name.Equals("ETIQUETA"))
                                    {
                                        salto = false;
                                    }
                                    else
                                    {
                                        if (aux2.Term.Name.Equals("label") || aux2.Term.Name.Equals("RETORNO"))
                                        {

                                            goto label4;
                                        }
                                        if (aux2.ChildNodes.ElementAt(2).ChildNodes.Count == 1 && !aux2.Term.Name.Equals("GOTO") && aux2.Term.Name.Equals("ETIQUETA") && aux2.Term.Name.Equals("IF"))
                                        {
                                            val22 = aux2.ChildNodes.ElementAt(0).Token.Text;
                                            val11 = aux2.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                                            if (val22.Equals(val2) && val11.Equals(val1) && salto)
                                            {
                                                agregar = false;
                                                raiz.Remove(aux2);

                                            }

                                        }
                                        else
                                        {
                                            val22 = aux2.ChildNodes.ElementAt(0).Token.Text;
                                            if (val22.Equals(val2))
                                            {
                                                salto = false;
                                            }

                                        }
                                        label4:;
                                    }
                                }
                            }

                        }


                        pos++;
                        break;
                    case "ASIGNACIONT":
                        if (hijo.ChildNodes.ElementAt(2).ChildNodes.Count > 1)
                        {
                            aux = optial(hijo.ChildNodes.ElementAt(2));
                            aux2 = hijo.ChildNodes.ElementAt(3);
                            hijo.ChildNodes.RemoveAt(2);
                            hijo.ChildNodes.RemoveAt(2);
                            hijo.ChildNodes.Add(aux);
                            hijo.ChildNodes.Add(aux2);
                            lista.AddLast(hijo);
                        }
                        else
                        {
                            aux = hijo.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);
                            if (!aux.Token.Text.Equals(hijo.ChildNodes.ElementAt(0).Token.Text))
                            {
                                lista.AddLast(hijo);
                                val1 = hijo.ChildNodes.ElementAt(0).Token.Text;
                                val2 = aux.Token.Text;
                                for (int i = pos+1; i < raiz.Count; i++)
                                {

                                    aux2 = raiz.ElementAt(i);
                                    if (aux2.Term.Name.Equals("ETIQUETA"))
                                    {
                                        salto = false;
                                    }
                                    else
                                    {
                                        if (aux2.Term.Name.Equals("label")|| aux2.Term.Name.Equals("RETORNO"))
                                        {

                                            goto label5;
                                        }
                                        if (aux2.ChildNodes.ElementAt(2).ChildNodes.Count == 1 && !aux2.Term.Name.Equals("GOTO") && aux2.Term.Name.Equals("ETIQUETA") && aux2.Term.Name.Equals("IF"))
                                        {
                                            val22 = aux2.ChildNodes.ElementAt(0).Token.Text;
                                            val11 = aux2.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                                            if (val22.Equals(val2) && val11.Equals(val1) && salto)
                                            {
                                                agregar = false;
                                                raiz.Remove(aux2);

                                            }

                                        }
                                        else
                                        {
                                            val22 = aux2.ChildNodes.ElementAt(0).Token.Text;
                                            if (val22.Equals(val2))
                                            {
                                                salto = false;
                                            }

                                        }
                                        label5:;
                                    }
                                }
                            }

                        }


                        pos++;
                        break;

                    default:
                        lista.AddLast(hijo);
                        pos++;
                        break;
                }


            }
            return regla2(lista);
        }

        public LinkedList<ParseTreeNode> regla2(LinkedList<ParseTreeNode> raiz)
        {
            
            ParseTreeNode aux;
            INICIO:
            int pos = 0;
            int pos1 = 0;
            int pos2 = 0;
            bool salto = true;
            String etiqueta = "";
            foreach(ParseTreeNode hijo in raiz)
            {
                if (hijo.Term.Name.Equals("GOTO"))
                {
                    if (hijo.ChildNodes.Count == 3) { 
                    etiqueta = hijo.ChildNodes.ElementAt(1).Token.Text;
                    pos1 = pos;
                    salto = true;
                }
                }

                if (hijo.Term.Name.Equals("ETIQUETA"))
                {
                    if (etiqueta.Equals(hijo.ChildNodes.ElementAt(0).Token.Text) && salto && !etiqueta.Equals("fin"))
                    {
                        pos2 = pos;
                        for(int i = pos2-1; i >= pos1; i--)
                        {
                            aux = raiz.ElementAt(i);
                            raiz.Remove(aux);
                        }
                        goto INICIO;
                    }
                    salto = false;
                }

                pos++;
            }



            return regla3(raiz);
        }

        public LinkedList<ParseTreeNode> regla3(LinkedList<ParseTreeNode> raiz)
        {
            
            ParseTreeNode aux;
           
            int pos = 0;
            int pos1 = 0;
            
            String etiqueta1 = "";
            String etiqueta2 = "";
            INICIO:
            pos = 0;
            pos1 = 0;
            etiqueta1 = "";
            etiqueta2 = "";

            foreach (ParseTreeNode hijo in raiz)
            {
                if (hijo.Term.Name.Equals("IF"))
                {
                    etiqueta1 = hijo.ChildNodes.ElementAt(5).Token.Text;
                    if (pos <( raiz.Count - 1))
                    {
                        int posx = pos + 1;
                        if (raiz.ElementAt(posx).Term.Name.Equals("GOTO")&&raiz.ElementAt(posx).ChildNodes.Count==3)
                        {
                            etiqueta2 = raiz.ElementAt(posx).ChildNodes.ElementAt(1).Token.Text;
                            pos1 = pos;
                            pos += 2;
                            if (pos < raiz.Count - 1)
                            {
                                if (raiz.ElementAt(pos).Term.Name.Equals("ETIQUETA"))
                                {
                                    aux = raiz.ElementAt(pos);
                                    if (aux.ChildNodes.ElementAt(0).Token.Text.Equals(etiqueta1))
                                    {

                                        aux = raiz.ElementAt(pos1);
                                        if (!aux.Term.Name.Equals("IF"))
                                        {
                                            goto sal;
                                        }
                                        Terminal te = new Terminal("label");
                                        SourceLocation S = new SourceLocation();
                                        object val = new object();
                                        val = etiqueta2;
                                        Token T = new Token(te,S,etiqueta2,val);
                                        aux.ChildNodes.ElementAt(5).Token = T;
                                        String cont = contrario(aux.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Text);
                                        Terminal te2 = new Terminal("Key symbol");
                                        SourceLocation S2 = new SourceLocation();
                                        object val2 = new object();
                                        val2 = cont;
                                        Token T2 = new Token(te2, S2, cont, val2);
                                        aux.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token = T2;
                                        aux = raiz.ElementAt(pos);
                                        raiz.Remove(aux);
                                        aux = raiz.ElementAt(pos - 1);
                                        raiz.Remove(aux);
                                        goto INICIO;
                                    }

                                }

                            }

                        }
                        

                    }
                   
                }

                pos++;
            }

            sal:

            return regla4(raiz);
        }

        public LinkedList<ParseTreeNode> regla4(LinkedList<ParseTreeNode> raiz)
        {
            INICIO:
            ParseTreeNode aux;

            int pos = 0;
            int pos1 = 0;

            String tipo1 = "";
            String tipo2 = "";
            foreach (ParseTreeNode hijo in raiz)
            {
                if (hijo.Term.Name.Equals("IF"))
                {
                    aux = hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0);
                    tipo1 = aux.Term.Name;
                    aux = hijo.ChildNodes.ElementAt(3).ChildNodes.ElementAt(0);
                    tipo2 = aux.Term.Name;

                    if ((tipo1.Equals("numero")| tipo1.Equals("decimal"))&& (tipo2.Equals("numero") | tipo2.Equals("decimal")))
                    {
                        if(resolverop(hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text, hijo.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Text, hijo.ChildNodes.ElementAt(3).ChildNodes.ElementAt(0).Token.Text))
                        {
                            aux = hijo.ChildNodes.ElementAt(0);
                            hijo.ChildNodes.Remove(aux);
                            aux = hijo.ChildNodes.ElementAt(0);
                            hijo.ChildNodes.Remove(aux);
                            aux = hijo.ChildNodes.ElementAt(0);
                            hijo.ChildNodes.Remove(aux);
                            aux = hijo.ChildNodes.ElementAt(0);
                            hijo.ChildNodes.Remove(aux);
                            hijo.Term.Name = "GOTO";
                            goto INICIO;
                        }
                        else
                        {

                            raiz.Remove(hijo);
                            goto INICIO;

                        }

                    }
                }

                pos++;
            }



            return regla5(raiz);
        }

        public LinkedList<ParseTreeNode> regla5(LinkedList<ParseTreeNode> raiz)
        {
            
            ParseTreeNode aux;

            int pos = 0;
            int pos1 = 0;

            String eti1 = "";
            String eti2 = "";
            foreach (ParseTreeNode hijo in raiz)
            {
                if (hijo.Term.Name.Equals("ETIQUETA"))
                {
                    eti1 = hijo.ChildNodes.ElementAt(0).Token.Text;
                    if (pos < raiz.Count - 1)
                    {
                        aux = raiz.ElementAt(pos + 1);
                        if (aux.Term.Name.Equals("GOTO") && aux.ChildNodes.Count == 3)
                        {
                            eti2 = aux.ChildNodes.ElementAt(1).Token.Text;
                            for(int i = 0; i < raiz.Count; i++)
                            {
                                aux = raiz.ElementAt(i);

                                if (aux.Term.Name.Equals("IF")){

                                    if (aux.ChildNodes.ElementAt(5).Token.Text.Equals(eti1)) {
                                        Terminal te = new Terminal("label");
                                        SourceLocation S = new SourceLocation();
                                        object val = new object();
                                        val = eti2;
                                        Token T = new Token(te, S, eti2, val);
                                        aux.ChildNodes.ElementAt(5).Token = T;
                                    }
                                }

                                if (aux.Term.Name.Equals("GOTO")&&aux.ChildNodes.Count==3)
                                {

                                    if (aux.ChildNodes.ElementAt(1).Token.Text.Equals(eti1))
                                    {
                                        Terminal te = new Terminal("label");
                                        SourceLocation S = new SourceLocation();
                                        object val = new object();
                                        val = eti2;
                                        Token T = new Token(te, S, eti2, val);
                                        aux.ChildNodes.ElementAt(1).Token = T;
                                    }
                                }

                            }
                            goto INICIO;

                        }

                    }
                }

                pos++;
            }

            INICIO:

            return raiz;
        }

        public bool resolverop(String t1, String tipo, String t2)
        {
            double t11;
            double t12;
            switch (tipo)
            {
                case "=":
                    if (t1.Equals(t2))
                        return true;
                    break;
                case ">":
                     t11 = Convert.ToDouble(t1);
                     t12 = Convert.ToDouble(t1);
                    if (t11 > t12)
                        return true;
                    break;
                case "<":
                    t11 = Convert.ToDouble(t1);
                    t12 = Convert.ToDouble(t1);
                    if (t11 < t12)
                        return true;
                    break;
                case ">=":
                    t11 = Convert.ToDouble(t1);
                    t12 = Convert.ToDouble(t1);
                    if (t11 >= t12)
                        return true;
                    break;
                case "<=":
                    t11 = Convert.ToDouble(t1);
                    t12 = Convert.ToDouble(t1);
                    if (t11 <= t12)
                        return true;
                    break;
                case "!=":
                    if (!t1.Equals(t2))
                        return true;
                    break;



            }



            return false;
        }
        public ParseTreeNode optial(ParseTreeNode E)
        {
            String tipo = E.ChildNodes.ElementAt(1).Token.Text;
            ParseTreeNode aux;
            

            switch (tipo)
            {
                case "+":
                    aux = E.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);
                    if (aux.Token.Text.Equals("0"))
                    {
                        E.ChildNodes.RemoveAt(0);
                        E.ChildNodes.RemoveAt(0);
                    }
                    else
                    {
                     aux = E.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0);
                        if (aux.Token.Text.Equals("0"))
                        {
                            E.ChildNodes.RemoveAt(1);
                            E.ChildNodes.RemoveAt(1);
                        }

                    }


                    break;

                case "-":
                   
                        aux = E.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0);
                        if (aux.Token.Text.Equals("0"))
                        {
                            E.ChildNodes.RemoveAt(1);
                            E.ChildNodes.RemoveAt(1);
                        }


                    break;

                case "*":
                    aux = E.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);
                    if (aux.Token.Text.Equals("1"))
                    {
                        E.ChildNodes.RemoveAt(0);
                        E.ChildNodes.RemoveAt(0);
                    }
                    else
                    {
                        aux = E.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0);
                        if (aux.Token.Text.Equals("1"))
                        {
                            E.ChildNodes.RemoveAt(1);
                            E.ChildNodes.RemoveAt(1);
                        }else
                        {

                            if (aux.Token.Text.Equals("0"))
                            {
                                E.ChildNodes.RemoveAt(0);
                                E.ChildNodes.RemoveAt(0);
                            }else
                            {
                                aux = E.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);
                                if (aux.Token.Text.Equals("0"))
                                {
                                    E.ChildNodes.RemoveAt(1);
                                    E.ChildNodes.RemoveAt(1);
                                }else
                                {
                                    aux = E.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0);
                                    if (aux.Token.Text.Equals("2"))
                                    {
                                        E.ChildNodes.RemoveAt(2);
                                        E.ChildNodes.Add(E.ChildNodes.ElementAt(0));
                                        SourceLocation sou = new SourceLocation();
                                        Token sum = new Token(null, sou, "+", null);
                                        E.ChildNodes.ElementAt(1).Token = sum;
                                        
                                    }
                                    else
                                    {
                                        aux = E.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);
                                        if (aux.Token.Text.Equals("2"))
                                        {
                                            E.ChildNodes.RemoveAt(0);
                                            ParseTreeNode aux2 = E.ChildNodes.ElementAt(0);
                                            E.ChildNodes.RemoveAt(0);
                                            SourceLocation sou = new SourceLocation();
                                            Token sum = new Token(null, sou, "+", null);
                                            aux2.ChildNodes.ElementAt(1).Token = sum;
                                            E.ChildNodes.Add(aux2);
                                            E.ChildNodes.Add(E.ChildNodes.ElementAt(0));
                                        }


                                    }

                                }


                            }

                        }

                    }
                    break;

                case "/":
                  
                        aux = E.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0);
                        if (aux.Token.Text.Equals("1"))
                        {
                            E.ChildNodes.RemoveAt(1);
                            E.ChildNodes.RemoveAt(1);
                    }else
                    {
                        aux = E.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);
                        if (aux.Token.Text.Equals("0"))
                        {
                            E.ChildNodes.RemoveAt(1);
                            E.ChildNodes.RemoveAt(1);
                        }
                    }

                    break;






            }




            return E;
        }

        public String contrario(String tipo)
        {
            switch (tipo)
            {
                case "=":
                    return "!=";
                case "!=":
                    return "=";
                case ">":
                    return "<=";
                case "<":
                    return ">=";
                case ">=":
                    return "<";
                default:
                    return ">";
            }


        }

        public String cadenaop(LinkedList<ParseTreeNode> raiz)
        {
            String ret = "";
            ParseTreeNode aux;
            bool bandera = false;
            
            foreach(ParseTreeNode hijo in raiz)
            {
                switch (hijo.Term.Name)
                {

                    case "label":
                        if (bandera)
                        {
                            ret += "}\n";
                        }
                        ret += hijo.Token.Text + "(){\n";
                        bandera = true;
                        break;
                    case "RETORNO":
                        ret += "return;";
                        break;
                    case "GOTO":
                        if (hijo.ChildNodes.Count == 3)
                        {
                            ret += "goto " + hijo.ChildNodes.ElementAt(1).Token.Text + " ;\n";
                        }else
                        {
                            ret += "if " + hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text;
                            ret += " " + hijo.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Text;
                            ret += " " + hijo.ChildNodes.ElementAt(3).ChildNodes.ElementAt(0).Token.Text;
                            ret += " goto " + hijo.ChildNodes.ElementAt(5).Token.Text + ";\n";

                        }
                        break;
                    case "IF":
                        ret += "if "+hijo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text;
                        ret += " " + hijo.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Text;
                        ret += " " + hijo.ChildNodes.ElementAt(3).ChildNodes.ElementAt(0).Token.Text;
                        ret += " goto " + hijo.ChildNodes.ElementAt(5).Token.Text+";\n";
                        break;
                    case "ASIGNASP":
                    case "ASIGNAHP":
                    case "ASIGNASEL1":
                    case "ASIGNACIONT":
                    case "ASIGNASEL2":
                        ret += hijo.ChildNodes.ElementAt(0).Token.Text+" =";
                        aux = hijo.ChildNodes.ElementAt(2);
                        if (aux.ChildNodes.Count == 3)
                        {
                            ret += " " + aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                            ret += " " + aux.ChildNodes.ElementAt(1).Token.Text;
                            ret += " " + aux.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Text;

                        }
                        else if(aux.ChildNodes.Count==1){
                            if (aux.ChildNodes.ElementAt(0).ChildNodes.Count != 4)
                            {
                                ret += " " + aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                            }
                            else
                            {
                                aux = aux.ChildNodes.ElementAt(0);
                                ret += " " + aux.ChildNodes.ElementAt(0).Token.Text;
                                ret += " [" + aux.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Text;
                                ret += " ]";

                            }
                        }

                       
                        ret += ";\n ";
                        break;
                    case "ETIQUETA":
                        if (hijo.ChildNodes.ElementAt(0).Token.Text.Equals("fin"))
                        {
                            ret += "}";

                        }
                        ret += hijo.ChildNodes.ElementAt(0).Token.Text+":\n";
                        break;
                    case "IMPRIMIR":
                        ret += "print(\"%d\","+hijo.ChildNodes.ElementAt(3).ChildNodes.ElementAt(0).Token.Text+");\n";
                        break;
                    case "LLAMADA":

                        ret +=hijo.ChildNodes.ElementAt(0).Token.Text+"();\n" ;
                        break;
                    default:
                        ret += hijo.ChildNodes.ElementAt(0).Token.Text +"[";
                        ret += hijo.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Text + "]=";
                        ret += hijo.ChildNodes.ElementAt(5).ChildNodes.ElementAt(0).Token.Text + ";\n";
                        break;

                }
            }
            Program.ventana.optimizado.Text = ret;
            return ret;
        }

        public LinkedList<ParseTreeNode> regla22(LinkedList<ParseTreeNode> raiz)
        {

            ParseTreeNode aux;
            int pos = 0;
            INICIO:
            
            int pos1 = 0;
            
            bool salto = true;
            String etiqueta = "";
            for(int i = pos; i < raiz.Count; i++)
            {
                aux = raiz.ElementAt(i);

                if (aux.Term.Name.Equals("GOTO")&&aux.ChildNodes.Count==3)
                {
                    pos1 = i;
                    etiqueta = aux.ChildNodes.ElementAt(1).Token.Text;
                    for(int j = i + 1; j < raiz.Count; j++)
                    {
                        aux = raiz.ElementAt(j);
                        if (aux.Term.Name.Equals("ETIQUETA"))
                        {
                            if (aux.ChildNodes.ElementAt(0).Token.Text.Equals(etiqueta)&&salto)
                            {
                                for(int k = j-1; k >= pos1; k--)
                                {
                                    aux = raiz.ElementAt(k);
                                    raiz.Remove(aux);
                                }
                                pos = i;
                                goto INICIO;
                            }else
                            {
                                salto = false;
                            }
                        }


                    }
                }


            }
            return regla3(raiz);
        }

        public int IFF(ParseTreeNode raiz,int pos)
        {
            ParseTreeNode valor1 = raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0);
            String accion = raiz.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Text;
            ParseTreeNode valor2 = raiz.ChildNodes.ElementAt(3).ChildNodes.ElementAt(0);

            double val1 = 0;
            double val2 = 0;
            switch (valor1.Term.Name)
            {
                case "numero":
                case "decimal":
                    val1 = Convert.ToDouble(valor1.Token.Text);
                    break;
                case "temporal":
                    val1 = Convert.ToDouble(buscarpila(valor1.Token.Text));
                    break;
                default:
                    val1 = 98596321789456321.966997812345;
                    break;
            }
            switch (valor2.Term.Name)
            {
                case "numero":
                case "decimal":
                    val2 = Convert.ToDouble(valor2.Token.Text);
                    break;
                case "temporal":
                    val2 = Convert.ToDouble(buscarpila(valor2.Token.Text));
                    break;
                default:
                    val2 = 98596321789456321.966997812345;
                    break;

            }

            switch (accion)
            {
                case ">":
                    if (val1 > val2)
                    {
                        return gotoo(raiz.ChildNodes.ElementAt(5).Token.Text);
                    }
                    break;
                case "<":
                    if (val1 < val2)
                    {
                        return gotoo(raiz.ChildNodes.ElementAt(5).Token.Text);
                    }
                    break;
                case "=":
                    if (val1 == val2)
                    {
                        return gotoo(raiz.ChildNodes.ElementAt(5).Token.Text);
                    }
                    break;
                case "!=":
                    if (val1 != val2)
                    {
                        return gotoo(raiz.ChildNodes.ElementAt(5).Token.Text);
                    }
                    break;
                case ">=":
                    if (val1 >= val2)
                    {
                        return gotoo(raiz.ChildNodes.ElementAt(5).Token.Text);
                    }
                    break;
                case "<=":
                    if (val1 <= val2)
                    {
                        return gotoo(raiz.ChildNodes.ElementAt(5).Token.Text);
                    }
                    break;



            }



            return pos+1;
        }

        public int IFFA(ParseTreeNode raiz, int pos)
        {
            ParseTreeNode valor1 = raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0);
            String accion = raiz.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Text;
            ParseTreeNode valor2 = raiz.ChildNodes.ElementAt(3).ChildNodes.ElementAt(0);

            double val1 = 0;
            double val2 = 0;
            switch (valor1.Term.Name)
            {
                case "numero":
                case "decimal":
                    val1 = Convert.ToDouble(valor1.Token.Text);
                    break;
                case "temporal":
                    val1 = Convert.ToDouble(buscarpila(valor1.Token.Text));
                    break;
                default:
                    val1 = 98596321789456321.966997812345;
                    break;
            }
            switch (valor2.Term.Name)
            {
                case "numero":
                case "decimal":
                    val2 = Convert.ToDouble(valor2.Token.Text);
                    break;
                case "temporal":
                    val2 = Convert.ToDouble(buscarpila(valor2.Token.Text));
                    break;
                default:
                    val2 = 98596321789456321.966997812345;
                    break;

            }

            switch (accion)
            {
                case ">":
                    if (val1 <= val2)
                    {
                        return gotoo(raiz.ChildNodes.ElementAt(5).Token.Text);
                    }
                    break;
                case "<":
                    if (val1 >=val2)
                    {
                        return gotoo(raiz.ChildNodes.ElementAt(5).Token.Text);
                    }
                    break;
                case "=":
                    if (val1 != val2)
                    {
                        return gotoo(raiz.ChildNodes.ElementAt(5).Token.Text);
                    }
                    break;
                case "!=":
                    if (val1 == val2)
                    {
                        return gotoo(raiz.ChildNodes.ElementAt(5).Token.Text);
                    }
                    break;
                case ">=":
                    if (val1 < val2)
                    {
                        return gotoo(raiz.ChildNodes.ElementAt(5).Token.Text);
                    }
                    break;
                case "<=":
                    if (val1 > val2)
                    {
                        return gotoo(raiz.ChildNodes.ElementAt(5).Token.Text);
                    }
                    break;



            }



            return pos + 1;
        }

        public ParseTreeNode reparar(ParseTreeNode raiz)
        {

            foreach(ParseTreeNode hijo in raiz.ChildNodes)
            {
                if (hijo.Term.Name.Equals("METODO"))
                {

                    foreach (ParseTreeNode hijo2 in hijo.ChildNodes)
                    {
                        if (hijo2.Term.Name.Equals("BLOQUE"))
                        {
                            foreach (ParseTreeNode hijo3 in hijo2.ChildNodes)
                            {
                                if (hijo3.Term.Name.Equals("ETIQUETA"))
                                {
                                    ParseTreeNode aux = hijo3.ChildNodes.ElementAt(0);
                                    String label = "";
                                    int i = 0;
                                    foreach(ParseTreeNode label1 in aux.ChildNodes)
                                    {
                                      
                                        if (i == 0)
                                        {
                                            label += label1.Token.Text;
                                        }else
                                        {
                                            label += ","+label1.Token.Text;
                                        }
                                        i++;
                                    }
                                    Terminal te = new Terminal("label");
                                    SourceLocation S = new SourceLocation();
                                    object val = new object();
                                    val = label;
                                    Token T = new Token(te, S, label, val);
                                    ParseTreeNode aux2 = hijo3.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);
                                    aux2.Token = T;
                                    ParseTreeNode aux3 = hijo3.ChildNodes.ElementAt(1);
                                    hijo3.ChildNodes.Clear();
                                    hijo3.ChildNodes.Add(aux2);
                                    hijo3.ChildNodes.Add(aux3);
                                   

                                }
                            }
                        }
                    }



                }else if (hijo.Term.Name.Equals("ETIQUETA"))
                {
                    String label = hijo.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                    Terminal te = new Terminal("label");
                    SourceLocation S = new SourceLocation();
                    object val = new object();
                    val = label;
                    Token T = new Token(te, S, label, val);
                    ParseTreeNode aux2 = hijo.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);
                    aux2.Token = T;
                    ParseTreeNode aux3 = hijo.ChildNodes.ElementAt(1);
                    hijo.ChildNodes.Clear();
                    hijo.ChildNodes.Add(aux2);
                    hijo.ChildNodes.Add(aux3);

                }
            }



            return raiz;
        }
    }
}
