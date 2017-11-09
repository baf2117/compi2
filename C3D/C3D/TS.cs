using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace C3D
{
    class TS
    {
        public String nombre;
        public int Tipo = 0;// 1 = clase, 2 = parametro , 3 = objeto , 4 = variable, atributo = 5 , metodo = 6 , funcion = 7,constructor = 8 , retorno = 9
        public int tipo2; // 1 = integer  , 2 = string , 3 = booleano , 4 = decimal , 0 = void, 5 = clase, objeto = 6, caracter =7 ;
        public int posicion;
        public int heredado = 0;
        public int localidad = 0;
        public int visibilidad;// 1 = publica , 2 = privada , 3 = protegida , 0 = local
        public ArrayList importadas = new ArrayList();
        public String etiqueta;
        public int peso;
        public int arreglo;
        public int dimensiones;
        public int[] tamanios;
        public int[] valores;
        public int error = 0;
        public static int relativa2;
        private String auxte = "";
        public static Boolean enstack = false;
        private int tipoa = 0;
        public static String errore = "";
        public static int tipoex = 0;// int = 0, char = 3, string = 2, decimal = 2 , booleano = 4
        public ArrayList elementos = new ArrayList();
        public static bool biffalse = false;
        public static ArrayList TablaSimbolos = new ArrayList();
        public static TS actualc = null;
        public static TS actualM = null;
        public static ArrayList import = new ArrayList();
        public static ArrayList display = new ArrayList();
        public static ArrayList importadasgeneral = new ArrayList();

        public TS()
        {
            int[] tamanios = new int[2];
        }

        public TS(String nombre, int Tipo, int posicion, int tipo2, int visibilidad, int peso, String etiqueta, int arreglo, int dimensiones, int[] tamanios)
        {

            this.nombre = nombre;
            this.Tipo = Tipo;
            this.posicion = posicion;
            this.tipo2 = tipo2;
            this.visibilidad = visibilidad;
            this.peso = peso;
            this.etiqueta = etiqueta;
            this.arreglo = arreglo;
            this.dimensiones = dimensiones;
            this.tamanios = tamanios;
            this.localidad = 0;
            this.importadas = new ArrayList();
        }

        public void recolectar(ParseTreeNode raiz)
        {

            ParseTreeNode auxhijo;
            ParseTreeNode importaciones = null;
            ArrayList import2 = new ArrayList();
            int relativa;
            ArrayList elementos = new ArrayList();

            if (raiz.ChildNodes.Count == 1)
            {
                raiz = raiz.ChildNodes.ElementAt(0);
            }
            else
            {

                importaciones = raiz.ChildNodes.ElementAt(0);
                raiz = raiz.ChildNodes.ElementAt(1);
            }

            if (importaciones != null)
            {

                String path = "";
                TabPage tabPagex = Program.ventana.tabControl1.SelectedTab;
                path = tabPagex.Name;
                String[] path2 = path.Split('\\');
                path = "";
                for (int l = 0; l < path2.Count() - 1; l++)
                {
                    if (l == path2.Count() - 2)
                    {
                        path += path2[l];
                    }
                    else
                    {
                        path += path2[l] + "\\";
                    }

                }
                String contenido = "";
                String nombre;
                String linea;
                String path3 = path;
                foreach (ParseTreeNode hijo in importaciones.ChildNodes)
                {
                    contenido = "";
                    nombre = importadastree(hijo.ChildNodes.ElementAt(0));
                    String[] name = nombre.Split('.');
                    path = path3 + "\\" + nombre;

                    try
                    {

                        StreamReader sr = new StreamReader(path);                      
                        linea = sr.ReadLine();
                        while (linea != null)
                        {
                            contenido += linea + "\n";

                            linea = sr.ReadLine();
                        }
                        sr.Close();

                        import2.Add(name[0]);
                        if (enimportadas(name[0]))
                        {
                            goto sal1;
                        }
                        importadasgeneral.Add(name[0]);
                        if (name[1].Equals("olc"))
                        {
                            Sintactico1.analizarolc(contenido);
                        }
                        else
                        {
                           contenido= contenido.Replace('\\', '~');
                            Sintactico1.analizartree(contenido);
                        }
                        sal1:;

                    }
                    catch (Exception e)
                    {
                        try
                        {
                            StreamReader sr = new StreamReader(nombre);
                            String[] imprt3 = nombre.Split('\\');
                            int tama = imprt3.Count();
                            String prename = imprt3[tama - 1];
                            String[] name2 = prename.Split('.');

                            import2.Add(name2[0]);
                            linea = sr.ReadLine();
                            while (linea != null)
                            {
                                contenido += linea + "\n";

                                linea = sr.ReadLine();
                            }
                            contenido = contenido.ToLower();
                            if (enimportadas(name2[0]))
                            {
                                goto sal2;
                            }
                            importadasgeneral.Add(name2[0]);
                            if (name2[1].Equals("olc"))
                            {
                                Sintactico1.analizarolc(contenido);
                            }
                            else
                            {
                                contenido = contenido.Replace('\\', '~');
                                Sintactico1.analizartree(contenido);
                            }

                            sr.Close();
                            sal2:;
                        }
                        catch (Exception f)
                        {

                            errore += "El archivo especificado no existe, " + nombre + "\n";
                            return;

                        }
                    }



                }



            }
            Ejecucion3d.temporal = 1;
            import = import2;

            foreach (ParseTreeNode hijo in raiz.ChildNodes)
            {
                auxhijo = hijo.ChildNodes.ElementAt(4);
                relativa = 0;
                ParseTreeNode herencia = hijo.ChildNodes.ElementAt(2);
                foreach (ParseTreeNode hijo2 in auxhijo.ChildNodes)
                {
                    switch (hijo2.Term.Name)
                    {
                        case "DECG":
                            ArrayList globales = variables(hijo2, relativa);

                            int tama = globales.Count;
                            relativa += tama;
                            foreach (TS glob in globales)
                            {
                                elementos.Add(glob);
                            }
                            break;
                    }

                }
                TS nue = new TS(hijo.ChildNodes.ElementAt(0).Token.Text, 1, 0, 5, 1, relativa, "", 0, 0, null);
                nue.elementos = elementos;
                TS.TablaSimbolos.Add(nue);
                actualc = nue;
                foreach (String imp in import)
                {
                    actualc.importadas.Add(imp);
                }

                actualc.importadas.Add(actualc.nombre);
                import.Clear();
                if (herencia.ChildNodes.Count > 0)
                {
                    String hern = herencia.ChildNodes.ElementAt(0).Token.Text;
                    bool bandera = true;
                    foreach (String import in nue.importadas)
                    {
                        if (import.Equals(hern))
                        {
                            bandera = false;
                        }

                    }
                    if (!bandera)
                    {


                        foreach (TS tablita in TablaSimbolos)
                        {

                            if (tablita.nombre.Equals(hern))
                            {
                                int pos = actualc.peso;
                                foreach (TS tablita2 in tablita.elementos)
                                {

                                    if (!existe(tablita2, actualc))
                                    {

                                        if (tablita2.visibilidad == 1)
                                        {
                                            TS nuevoh = new TS(tablita2.nombre, tablita2.Tipo, pos, tablita2.tipo2, tablita2.visibilidad, 1, tablita2.etiqueta, tablita2.arreglo, tablita2.dimensiones, tablita2.tamanios);
                                            nuevoh.heredado = 1;
                                            nuevoh.auxte = tablita2.auxte;
                                            nue.elementos.Add(nuevoh);
                                        }
                                        if (tablita2.Tipo == 5)
                                        {
                                            pos++;
                                        }

                                    }

                                }


                            }

                        }

                    }
                }

                recolectarEscribirtree(auxhijo);
            }

        }

        public void recolectarolc(ParseTreeNode raiz)
        {

            ParseTreeNode auxhijo;
            ParseTreeNode importaciones = null;
            ArrayList import2 = new ArrayList();
            int relativa;
            ArrayList elementos = new ArrayList();

            if (raiz.ChildNodes.Count == 1)
            {
                raiz = raiz.ChildNodes.ElementAt(0);
            }
            else
            {

                importaciones = raiz.ChildNodes.ElementAt(0);
                raiz = raiz.ChildNodes.ElementAt(1);
            }


            if (importaciones != null)
            {

                String path = "";
                TabPage tabPagex = Program.ventana.tabControl1.SelectedTab;
                path = tabPagex.Name;
                String[] path2 = path.Split('\\');
                path = "";
                for (int l = 0; l < path2.Count() - 1; l++)
                {
                    if (l == path2.Count() - 2)
                    {
                        path += path2[l];
                    }
                    else
                    {
                        path += path2[l] + "\\";
                    }

                }
                String contenido = "";
                String nombre;
                String linea;
                String path3 = path;
                foreach (ParseTreeNode hijo in importaciones.ChildNodes)
                {
                    contenido = "";
                    nombre = hijo.ChildNodes.ElementAt(0).Token.Text;
                    nombre = comillas(nombre);
                    String[] name = nombre.Split('.');



                    path = path3 + "\\" + nombre;

                    try
                    {

                        StreamReader sr = new StreamReader(path);
                        linea = sr.ReadLine();
                        while (linea != null)
                        {
                            contenido += linea + "\n";

                            linea = sr.ReadLine();
                        }
                        sr.Close();

                        import2.Add(name[0]);
                        if (enimportadas(name[0]))
                        {
                            goto sal1;
                        }
                        importadasgeneral.Add(name[0]);
                        if (name[1].Equals("olc")) {
                            Sintactico1.analizarolc(contenido);
                        }
                        else
                        {
                            contenido = contenido.Replace('\\', '~');
                            Sintactico1.analizartree(contenido);
                        }
                        sal1:;

                    }
                    catch (Exception e)
                    {
                        try
                        {
                            StreamReader sr = new StreamReader(nombre);
                            String[] imprt3 = nombre.Split('\\');
                            int tama = imprt3.Count();
                            String prename = imprt3[tama - 1];
                            String[] name2 = prename.Split('.');
                            
                            import2.Add(name2[0]);
                            linea = sr.ReadLine();
                            while (linea != null)
                            {
                                contenido += linea + "\n";

                                linea = sr.ReadLine();
                            }
                            contenido = contenido.ToLower();
                            if (enimportadas(name2[0]))
                            {
                                goto sal2;
                            }
                            importadasgeneral.Add(name2[0]);
                            if (name2[1].Equals("olc"))
                            {
                                Sintactico1.analizarolc(contenido);
                            }
                            else
                            {
                                contenido = contenido.Replace('\\', '~');
                                Sintactico1.analizartree(contenido);
                            }

                            sr.Close();
                            sal2:;
                        }
                        catch (Exception f)
                        {

                            errore += "El archivo especificado no existe, " + nombre + "\n";
                            return;

                        }
                    }



                }



            }
            Ejecucion3d.temporal = 1;
            import = import2;

            foreach (ParseTreeNode hijo in raiz.ChildNodes)
            {
                ParseTreeNode herencia = hijo.ChildNodes.ElementAt(1);
                auxhijo = hijo.ChildNodes.ElementAt(2);
                relativa = 0;
                foreach (ParseTreeNode hijo2 in auxhijo.ChildNodes)
                {

                    switch (hijo2.Term.Name)
                    {
                        case "DECGF":
                            ArrayList globales = variablesolcg(hijo2, relativa);

                            int tama = 0;

                            foreach (TS glob in globales)
                            {
                                elementos.Add(glob);
                                tama += glob.peso;

                            }
                            relativa += tama;
                            break;

                    }

                }

                TS nue = new TS(hijo.ChildNodes.ElementAt(0).Token.Text, 1, 0, 5, 1, relativa, "", 0, 0, null);
                nue.elementos = elementos;
                TS.TablaSimbolos.Add(nue);
                actualc = nue;
                foreach (String imp in import)
                {
                    actualc.importadas.Add(imp);

                }
                actualc.importadas.Add(actualc.nombre);
                import.Clear();
                if (herencia.ChildNodes.Count > 0)
                {
                    String hern = herencia.ChildNodes.ElementAt(0).Token.Text;
                    bool bandera = true;
                    foreach (String import in nue.importadas)
                    {
                        if (import.Equals(hern)) {
                            bandera = false;
                        }

                    }
                    if (!bandera)
                    {


                        foreach (TS tablita in TablaSimbolos)
                        {

                            if (tablita.nombre.Equals(hern))
                            {
                                int pos = actualc.peso;
                                foreach (TS tablita2 in tablita.elementos)
                                {

                                    if (!existe(tablita2, actualc))
                                    {

                                        if (tablita2.visibilidad == 1)
                                        {
                                            TS nuevoh = new TS(tablita2.nombre, tablita2.Tipo, pos, tablita2.tipo2, tablita2.visibilidad, 1, tablita2.etiqueta, tablita2.arreglo, tablita2.dimensiones, tablita2.tamanios);
                                            nuevoh.heredado = 1;
                                            nuevoh.auxte = tablita2.auxte;
                                            nue.elementos.Add(nuevoh);
                                        }
                                        if (tablita2.Tipo == 5)
                                        {
                                            pos++;
                                        }

                                    }

                                }


                            }

                        }

                    }
                }
                recolectarEscribirolc(auxhijo);

            }





        }

        public bool existe(TS tablita, TS clase) {

            foreach (TS tablita2 in clase.elementos)
            {
                if (tablita.nombre.Equals(tablita2.nombre))
                {
                    return true;
                }
            }

            return false;
        }

        public int heredados(TS tablita) {
            int retorno = 0;
            foreach (TS tablita2 in tablita.elementos)
            {
                if (tablita2.heredado == 1 && tablita2.Tipo == 5)
                {
                    retorno++;
                }
            }
            return retorno;
        }

        public void recolectarEscribirolc(ParseTreeNode raiz)
        {

            #region constructor
            ArrayList constructores1 = constructores(raiz);
            Ejecucion3d.temporal = 1;
            if (constructores1.Count == 0)
            {
                TS constructorx = new TS("init_" + actualc.nombre + "1", 8, 0, 0, 1, 1, "init_" + actualc.nombre + "1", 0, 0, null);
                actualc.elementos.Add(constructorx);
                TS retorno1 = new TS("retorno", 9, 0, 9, 1, 1, "", 0, 0, null);
                constructorx.elementos.Add(retorno1);
                Ejecucion3d.cadenota += "init_" + actualc.nombre + "1(){\n";
                String t1 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t1 + "=hp;\n";
                int pesofinal = actualc.peso + heredados(actualc);
                Ejecucion3d.cadenota += "hp = hp + " + pesofinal + ";\n";
                String t2 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t2 + "=sp;\n";
                Ejecucion3d.cadenota += "stack[" + t2 + "]=" + t1 + ";\n";
                Ejecucion3d.cadenota += "return;}\n";
                Ejecucion3d.temporal = 1;


                //--------------------------------------------------------------------------------------------------------------------
                TS constructorx2 = new TS("init_2" + actualc.nombre + "1", 8, 0, 0, 1, 1, "init_2" + actualc.nombre + "1", 0, 0, null);
                actualc.elementos.Add(constructorx2);
                TS poshp = new TS("retorno", 9, 0, 9, 1, 1, "", 0, 0, null);
                constructorx2.elementos.Add(poshp);
                Ejecucion3d.cadenota += "init_2" + actualc.nombre + "1(){\n";
                t1 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t1 + "=sp+1;\n";
                t2 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t2 + "=stack[" + t1 + "];\n";

                ParseTreeNode aux, aux2;
                int conta = 0;
                foreach (ParseTreeNode atri in raiz.ChildNodes)
                {
                    if (atri.Term.Name.Equals("DECGF"))
                    {
                        if (atri.ChildNodes.Count == 3)
                        {
                            aux = atri.ChildNodes.ElementAt(2);


                        }
                        else
                        {
                            aux = atri.ChildNodes.ElementAt(3);

                        }

                        if (aux.ChildNodes.ElementAt(0).Term.Name.Equals("DECG"))
                        {
                            aux2 = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);

                            String t3 = "00";

                            if (aux2.ChildNodes.Count > 0)
                            {

                                if (aux2.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Term.Name.Equals("VALA"))
                                {

                                    auxte = t2;
                                    tipoa = 1;
                                    t3 = Exp(aux2.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));
                                    Ejecucion3d.cadenota += "heap[" + t2 + "]=";
                                    Ejecucion3d.cadenota += t3 + ";\n";
                                }
                                else
                                {
                                    t3 = Exp(aux2.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));
                                    Ejecucion3d.cadenota += "heap[" + t2 + "]=";
                                    Ejecucion3d.cadenota += t3 + ";\n";
                                }
                            }
                            else
                            {
                                if (esarre(conta) > 0)
                                {
                                    t3 = auxte;
                                }
                                Ejecucion3d.cadenota += "heap[" + t2 + "]=";
                                Ejecucion3d.cadenota += t3 + ";\n";

                            }


                            Ejecucion3d.cadenota += t2 + " = " + t2 + " + 1;\n";
                            aux2 = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);

                            if (aux2.ChildNodes.Count > 0)
                            {

                                aux2 = aux2.ChildNodes.ElementAt(0);

                                if (aux2.Term.Name.Equals("LID"))
                                {

                                    for (int i = 0; i < aux2.ChildNodes.Count; i++)
                                    {

                                        Ejecucion3d.cadenota += "heap[" + t2 + "]=" + t3 + ";\n";
                                        Ejecucion3d.cadenota += t2 + " = " + t2 + " + 1;\n";

                                    }


                                }

                            }


                            conta++;
                        }



                    }


                }


                int posx = 0;
                foreach (TS hijo in actualc.elementos) {

                    if (hijo.heredado == 1)
                    {
                        String t3 = "00";
                        if (esarre(posx) > 0)
                        {
                            t3 = auxte;
                        }
                        Ejecucion3d.cadenota += "heap[" + t2 + "]=";
                        Ejecucion3d.cadenota += t3 + ";\n";
                        Ejecucion3d.cadenota += t2 + " = " + t2 + " + 1;\n";
                    }
                    posx++;
                }

                Ejecucion3d.cadenota += "return;}\n";

            }
            else
            {

                TS constructorx = new TS("init_" + actualc.nombre + "1", 8, 0, 0, 1, 1, "init_" + actualc.nombre + "1", 0, 0, null);
                actualc.elementos.Add(constructorx);

                TS retorno1 = new TS("retorno", 9, 0, 9, 1, 1, "", 0, 0, null);
                constructorx.elementos.Add(retorno1);

                Ejecucion3d.cadenota += "init_" + actualc.nombre + "1(){\n";
                String t1 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t1 + "=hp;\n";
                int pesofinal = actualc.peso + heredados(actualc);
                Ejecucion3d.cadenota += "hp = hp + " + pesofinal + ";\n";
                String t2 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t2 + "=sp;\n";
                Ejecucion3d.cadenota += "stack[" + t2 + "]=" + t1 + ";\n";
                Ejecucion3d.cadenota += "return;}\n";
                Ejecucion3d.temporal = 1;
                int j = 1;
                foreach (ParseTreeNode constri in constructores1)
                {
                    //--------------------------------------------------------------------------------------------------------------------
                    TS constructorx2 = new TS("init_2" + actualc.nombre + j, 8, 0, 0, 1, 1, "init_2" + actualc.nombre + j, 0, 0, null);
                    actualM = constructorx2;
                    actualc.elementos.Add(constructorx2);
                    TS poshp = new TS("retorno", 9, 0, 9, 1, 1, "", 0, 0, null);
                    constructorx2.elementos.Add(poshp);
                    Ejecucion3d.cadenota += "init_2" + actualc.nombre + j + "(){\n";
                    t1 = Ejecucion3d.generatemp();
                    Ejecucion3d.cadenota += t1 + "=sp+1;\n";
                    t2 = Ejecucion3d.generatemp();
                    Ejecucion3d.cadenota += t2 + "=stack[" + t1 + "];\n";

                    ParseTreeNode para;
                    actualM.peso = 2;
                    relativa2 = 2;
                    String etiqueta = "";

                    if (constri.ChildNodes.Count == 3)
                    {

                        para = constri.ChildNodes.ElementAt(1);

                        foreach (ParseTreeNode parametro2 in para.ChildNodes)
                        {

                            String tipov = parametro2.ChildNodes.ElementAt(0).Token.Text;
                            int tipov2;

                            switch (tipov)
                            {
                                case "entero":
                                    tipov2 = 1;
                                    break;
                                case "cadena":
                                    tipov2 = 2;
                                    break;
                                case "booleano":
                                    tipov2 = 3;
                                    break;
                                case "decimal":
                                    tipov2 = 4;
                                    break;
                                default:
                                    tipov2 = 6;
                                    etiqueta = tipov;
                                    break;
                            }

                            if (parametro2.ChildNodes.Count == 2)
                            {

                                TS parametro1 = new TS(parametro2.ChildNodes.ElementAt(1).Token.Text, 2, relativa2, tipov2, visibilidad, 1, etiqueta, 0, 0, null);
                                constructorx2.elementos.Add(parametro1);
                                actualM.peso++;
                                relativa2++;

                            }
                            else
                            {
                                ParseTreeNode dimen1 = parametro2.ChildNodes.ElementAt(2);
                                int dimen11 = dimen1.ChildNodes.Count;
                                int[] dimen2 = new int[dimen11];
                                int k = 0;
                                int peso1 = 1;
                                foreach (ParseTreeNode dimen3 in dimen1.ChildNodes)
                                {
                                    dimen2[k] = Convert.ToInt32(dimen3.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text);
                                    peso1 *= dimen2[k];
                                    k++;

                                }
                                TS parametro1 = new TS(parametro2.ChildNodes.ElementAt(1).Token.Text, 2, relativa2, tipov2, visibilidad, 1, tipov, 1, dimen11, dimen2);
                                constructorx2.elementos.Add(parametro1);
                                relativa2++;
                                actualM.peso++;
                                for (int l = 0; l < peso1; l++)
                                {
                                    parametro1 = new TS(parametro2.ChildNodes.ElementAt(1).Token.Text + l, 2, relativa2, tipov2, visibilidad, 1, tipov, 0, 0, null);
                                    constructorx2.elementos.Add(parametro1);
                                    relativa2++;
                                    actualM.peso++;
                                }



                            }



                        }






                    }

                    ParseTreeNode aux, aux2;


                    int conta = 0;
                    foreach (ParseTreeNode atri in raiz.ChildNodes)
                    {
                        if (atri.Term.Name.Equals("DECGF"))
                        {
                            if (atri.ChildNodes.Count == 3)
                            {
                                aux = atri.ChildNodes.ElementAt(2);

                            }
                            else
                            {
                                aux = atri.ChildNodes.ElementAt(3);

                            }

                            if (aux.ChildNodes.ElementAt(0).Term.Name.Equals("DECG"))
                            {
                                aux2 = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);

                                String t3 = "00";

                                if (aux2.ChildNodes.Count > 0)
                                {

                                    if (aux2.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Term.Name.Equals("VALA"))
                                    {
                                        String tx = Exp(aux2.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));
                                        Ejecucion3d.cadenota += "heap[" + t2 + "]=";
                                        Ejecucion3d.cadenota += tx + ";\n";
                                        auxte = t2;
                                        tipoa = 1;

                                    }
                                    else
                                    {
                                        t3 = Exp(aux2.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));
                                        Ejecucion3d.cadenota += "heap[" + t2 + "]=";
                                        Ejecucion3d.cadenota += t3 + ";\n";
                                    }
                                }
                                else
                                {
                                    if (esarre(conta) > 0)
                                    {
                                        t3 = auxte;
                                    }
                                    Ejecucion3d.cadenota += "heap[" + t2 + "]=";
                                    Ejecucion3d.cadenota += t3 + ";\n";

                                }

                                Ejecucion3d.cadenota += t2 + " = " + t2 + " + 1;\n";
                                aux2 = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);

                                if (aux2.ChildNodes.Count > 0)
                                {

                                    aux2 = aux2.ChildNodes.ElementAt(0);

                                    if (aux2.Term.Name.Equals("LID"))
                                    {

                                        for (int i = 0; i < aux2.ChildNodes.Count; i++)
                                        {
                                            Ejecucion3d.cadenota += "heap[" + t2 + "]=" + t3 + ";\n";
                                            Ejecucion3d.cadenota += t2 + " = " + t2 + " + 1;\n";

                                        }


                                    }

                                }

                                conta++;

                            }



                        }


                    }
                    int heredados1 = heredados(actualc);
                    for (int i = 0; i < heredados1; i++)
                    {
                        Ejecucion3d.cadenota += "heap[" + t2 + "]=";
                        Ejecucion3d.cadenota += "00;\n";

                    }
                    ParseTreeNode bloque;
                    if (constri.ChildNodes.Count == 2)
                    {

                        bloque = constri.ChildNodes.ElementAt(1);
                    }
                    else
                    {

                        bloque = constri.ChildNodes.ElementAt(2);
                    }

                    Bloque(bloque);


                    Ejecucion3d.cadenota += "return;}\n";
                    j++;
                }
            }
            #endregion


            #region metodos_main

            ParseTreeNode aux4, aux5, aux6;

            foreach (ParseTreeNode hijo in raiz.ChildNodes)
            {

                if (hijo.Term.Name.Equals("METODOS"))
                {
                    ParseTreeNode aux;
                    ParseTreeNode parametros = null;
                    ParseTreeNode bloque1;
                    String nombre = "";
                    Ejecucion3d.temporal = 1;
                    String etiqueta = Ejecucion3d.generalabel();
                    int visibilidad = 1;
                    if (hijo.ChildNodes.Count > 3)
                    {
                        aux = hijo.ChildNodes.ElementAt(0);
                        switch (aux.Token.Text)
                        {
                            case "privado":
                                visibilidad = 2;
                                break;
                            case "protegido":
                                visibilidad = 3;
                                break;
                            default:
                                visibilidad = 1;
                                break;
                        }
                        parametros = hijo.ChildNodes.ElementAt(2);
                        bloque1 = hijo.ChildNodes.ElementAt(3);
                        nombre = hijo.ChildNodes.ElementAt(1).Token.Text;


                    }
                    else if (hijo.ChildNodes.Count == 3)
                    {

                        String prueba = hijo.ChildNodes.ElementAt(0).Term.Name;

                        if (prueba.Equals("id"))
                        {

                            parametros = hijo.ChildNodes.ElementAt(1);
                            nombre = hijo.ChildNodes.ElementAt(0).Token.Text;
                            bloque1 = hijo.ChildNodes.ElementAt(2);

                        }
                        else
                        {

                            aux = hijo.ChildNodes.ElementAt(0);
                            switch (aux.Token.Text)
                            {
                                case "privado":
                                    visibilidad = 2;
                                    break;
                                case "protegido":
                                    visibilidad = 3;
                                    break;
                                default:
                                    visibilidad = 1;
                                    break;
                            }

                            nombre = hijo.ChildNodes.ElementAt(1).Token.Text;
                            bloque1 = hijo.ChildNodes.ElementAt(2);

                        }

                    }
                    else
                    {

                        nombre = hijo.ChildNodes.ElementAt(0).Token.Text;
                        bloque1 = hijo.ChildNodes.ElementAt(1);

                    }

                    TS nuevo = new TS(nombre, 6, 0, 0, visibilidad, 0, etiqueta, 0, 0, null);
                    int pos = sobreescribir(nombre);
                    if (pos > 0)
                    {
                        actualc.elementos.RemoveAt(pos);
                    }
                    actualc.elementos.Add(nuevo);
                    TS ret = new TS("ret", 9, 0, 9, visibilidad, 1, "", 0, 0, null);
                    nuevo.elementos.Add(ret);
                    nuevo.peso++;
                    actualM = nuevo;

                    if (parametros != null)
                    {

                        ArrayList parametros2 = nombresp(parametros);

                        foreach (TS parametrito in parametros2)
                        {
                            actualM.elementos.Add(parametrito);
                            actualM.peso++;

                        }


                    }
                    Ejecucion3d.cadenota += actualM.etiqueta + "(){\n";
                    Ejecucion3d.temporal = 1;
                    Bloque(bloque1);

                    Ejecucion3d.cadenota += "return;}\n";

                }
                else if (hijo.Term.Name.Equals("PRINCIPAL"))
                {
                    Ejecucion3d.temporal = 1;
                    Ejecucion3d.cadenota += "main(){ \n";
                    String t1 = Ejecucion3d.generatemp();
                    Ejecucion3d.cadenota += t1 + "= sp + 0;\n";
                    Ejecucion3d.cadenota += "stack[" + t1 + "]=00;\n";
                    Ejecucion3d.cadenota += "sp = sp +0;\n";
                    Ejecucion3d.cadenota += "init_" + actualc.nombre + "1();\n";
                    String t2 = Ejecucion3d.generatemp();
                    Ejecucion3d.cadenota += t2 + "=stack[sp];\n";


                    TS nuevo = new TS("main", 6, 0, 0, 1, 0, "main", 0, 0, null);
                    actualc.elementos.Add(nuevo);
                    actualM = nuevo;
                    actualM.peso++;
                    Ejecucion3d.cadenota += "selfp=" + t2 + ";\n";
                    String t3 = Ejecucion3d.generatemp();
                    Ejecucion3d.cadenota += "sp = sp - 0;\n";
                    Ejecucion3d.cadenota += t3 + "=sp+" + actualM.peso + ";\n";
                    Ejecucion3d.cadenota += "stack[" + t3 + "]=00;\n";
                    String t4 = Ejecucion3d.generatemp();
                    Ejecucion3d.cadenota += t4 + "=" + t3 + "+ 1 ;\n";
                    Ejecucion3d.cadenota += "stack[" + t4 + "]=" + t2 + ";\n";
                    Ejecucion3d.cadenota += "sp = sp +" + actualM.peso + ";\n";
                    Ejecucion3d.cadenota += "init_2" + actualc.nombre + "1();\n";
                    Ejecucion3d.cadenota += "sp = sp -" + actualM.peso + ";\n";
                    ParseTreeNode bloque = hijo.ChildNodes.ElementAt(0);
                    Bloque(bloque);
                    Ejecucion3d.cadenota += "goto fin;";
                    Ejecucion3d.cadenota += "}\n";
                } else if (hijo.Term.Name.Equals("DECGF"))
                {
                    ParseTreeNode auxf;
                    String nombre = "";
                    String tipov = "";
                    if (hijo.ChildNodes.Count() == 3)
                    {
                        if (!hijo.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Term.Name.Equals("FUNCIONES"))
                        {
                            goto sal;
                        }
                        auxf = hijo.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0);
                        nombre = hijo.ChildNodes.ElementAt(1).Token.Text;
                        tipov = hijo.ChildNodes.ElementAt(0).Token.Text;

                    }
                    else
                    {
                        if (!hijo.ChildNodes.ElementAt(3).ChildNodes.ElementAt(0).Term.Name.Equals("FUNCIONES"))
                        {
                            goto sal;
                        }
                        auxf = hijo.ChildNodes.ElementAt(3).ChildNodes.ElementAt(0);
                        nombre = hijo.ChildNodes.ElementAt(2).Token.Text;
                        tipov = hijo.ChildNodes.ElementAt(1).Token.Text;
                    }

                    ParseTreeNode aux;
                    ParseTreeNode parametros = null;
                    ParseTreeNode bloque1;

                    Ejecucion3d.temporal = 1;
                    String etiqueta = Ejecucion3d.generalabel();
                    int visibilidad = 1;
                    if (hijo.ChildNodes.Count > 3)
                    {
                        aux = hijo.ChildNodes.ElementAt(0);
                        switch (aux.Token.Text)
                        {
                            case "privado":
                                visibilidad = 2;
                                break;
                            case "protegido":
                                visibilidad = 3;
                                break;
                            default:
                                visibilidad = 1;
                                break;
                        }

                    }


                    int tipov2;

                    switch (tipov)
                    {
                        case "entero":
                            tipov2 = 1;
                            break;
                        case "cadena":
                            tipov2 = 2;
                            break;
                        case "booleano":
                            tipov2 = 3;
                            break;
                        case "decimal":
                            tipov2 = 4;
                            break;
                        case "caracter":
                            tipov2 = 7;
                            break;
                        default:
                            tipov2 = 6;
                            break;
                    }

                    parametros = auxf.ChildNodes.ElementAt(0);
                    bloque1 = auxf.ChildNodes.ElementAt(1);



                    TS nuevo = new TS(nombre, 7, 0, tipov2, visibilidad, 0, etiqueta, 0, 0, null);
                    if (nuevo.tipo2 == 6)
                    {
                        nuevo.auxte = tipov;

                    }
                    int pos = sobreescribir(nombre);
                    if (pos > 0)
                    {
                        actualc.elementos.RemoveAt(pos);
                    }
                    actualc.elementos.Add(nuevo);
                    TS ret = new TS("ret", 9, 0, 9, visibilidad, 1, "", 0, 0, null);
                    nuevo.elementos.Add(ret);
                    nuevo.peso++;
                    actualM = nuevo;

                    if (parametros != null)
                    {

                        ArrayList parametros2 = nombresp(parametros);

                        foreach (TS parametrito in parametros2)
                        {
                            actualM.elementos.Add(parametrito);
                            actualM.peso++;

                        }


                    }
                    Ejecucion3d.cadenota += actualM.etiqueta + "(){\n";
                    Ejecucion3d.temporal = 1;
                    Bloque(bloque1);

                    Ejecucion3d.cadenota += "return;}\n";
                    sal:;

                }
                #endregion



            }


        }

        public void recolectarEscribirtree(ParseTreeNode raiz)
        {

            #region constructor
            ArrayList constructores1 = constructores(raiz);
            Ejecucion3d.temporal = 1;
            if (constructores1.Count == 0)
            {
                TS constructorx = new TS("init_" + actualc.nombre + "1", 8, 0, 0, 1, 1, "init_" + actualc.nombre + "1", 0, 0, null);
                actualc.elementos.Add(constructorx);
                TS retorno1 = new TS("retorno", 9, 0, 0, 1, 1, "", 0, 0, null);
                constructorx.elementos.Add(retorno1);
                Ejecucion3d.cadenota += "init_" + actualc.nombre + "1(){\n";
                String t1 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t1 + "=hp;\n";
                int pesofinal = actualc.peso + heredados(actualc);
                Ejecucion3d.cadenota += "hp = hp + " + pesofinal + ";\n";
                String t2 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t2 + "=sp;\n";
                Ejecucion3d.cadenota += "stack[" + t2 + "]=" + t1 + ";\n";
                Ejecucion3d.cadenota += "return;}\n";
                Ejecucion3d.temporal = 1;


                //--------------------------------------------------------------------------------------------------------------------
                TS constructorx2 = new TS("init_2" + actualc.nombre + "1", 8, 0, 0, 1, 1, "init_2" + actualc.nombre + "1", 0, 0, null);
                actualc.elementos.Add(constructorx2);
                constructorx2.elementos.Add(retorno1);
                TS poshp = new TS("retorno", 9, 0, 0, 1, 1, "", 0, 0, null);
                constructorx.elementos.Add(poshp);
                Ejecucion3d.cadenota += "init_2" + actualc.nombre + "1(){\n";
                t1 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t1 + "=sp+1;\n";
                t2 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t2 + "=stack[" + t1 + "];\n";

                ParseTreeNode aux, aux2;
                int conta = 0;
                foreach (ParseTreeNode atri in raiz.ChildNodes)
                {
                    if (atri.Term.Name.Equals("DECG"))
                    {
                        aux2 = atri.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2);
                        String t3 = "00";

                        if (aux2.ChildNodes.Count > 0)
                        {

                            if (aux2.ChildNodes.ElementAt(0).Term.Name.Equals("VALA"))
                            {
                                Ejecucion3d.cadenota += "heap[" + t2 + "]=";
                                Ejecucion3d.cadenota += "00;\n";
                                auxte = t2;
                                tipoa = 1;
                                Expt(aux2.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));
                            }
                            else
                            {
                                t3 = Expt(aux2.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));
                                Ejecucion3d.cadenota += "heap[" + t2 + "]=";
                                Ejecucion3d.cadenota += t3 + ";\n";
                            }
                        }
                        else
                        {
                            if (esarre(conta) > 0)
                            {
                                t3 = auxte;
                            }

                            Ejecucion3d.cadenota += "heap[" + t2 + "]=";
                            Ejecucion3d.cadenota += t3 + ";\n";

                        }


                        Ejecucion3d.cadenota += t2 + " = " + t2 + " + 1;\n";
                        aux2 = atri.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);

                        if (aux2.ChildNodes.Count > 0)
                        {

                            aux2 = aux2.ChildNodes.ElementAt(0);

                            if (aux2.Term.Name.Equals("LID"))
                            {

                                for (int i = 0; i < aux2.ChildNodes.Count; i++)
                                {
                                    Ejecucion3d.cadenota += "heap[" + t2 + "]=" + t3 + ";\n";
                                    Ejecucion3d.cadenota += t2 + " = " + t2 + " + 1;\n";

                                }


                            }

                        }

                        conta++;

                    }

                }

                int heredados1 = heredados(actualc);
                for (int i = 0; i < heredados1; i++)
                {
                    Ejecucion3d.cadenota += "heap[" + t2 + "]=";
                    Ejecucion3d.cadenota += "00;\n";
                    Ejecucion3d.cadenota += t2 + " = " + t2 + " + 1;\n";

                }

                Ejecucion3d.cadenota += "return;}\n";

            }
            else
            {

                TS constructorx = new TS("init_" + actualc.nombre + "1", 8, 0, 0, 1, 1, "init_" + actualc.nombre + "1", 0, 0, null);
                actualc.elementos.Add(constructorx);

                TS retorno1 = new TS("retorno", 9, 0, 0, 1, 1, "", 0, 0, null);
                constructorx.elementos.Add(retorno1);

                Ejecucion3d.cadenota += "init_" + actualc.nombre + "1(){\n";
                String t1 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t1 + "=hp;\n";
                int pesofinal = actualc.peso + heredados(actualc);
                Ejecucion3d.cadenota += "hp = hp + " + pesofinal + ";\n";
                String t2 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t2 + "=sp;\n";
                Ejecucion3d.cadenota += "stack[" + t2 + "]=" + t1 + ";\n";
                Ejecucion3d.cadenota += "return;}\n";
                Ejecucion3d.temporal = 1;
                int j = 1;
                foreach (ParseTreeNode constri in constructores1)
                {
                    //--------------------------------------------------------------------------------------------------------------------
                    TS constructorx2 = new TS("init_2" + actualc.nombre + j, 8, 0, 0, 1, 1, "init_2" + actualc.nombre + j, 0, 0, null);
                    actualM = constructorx2;
                    actualc.elementos.Add(constructorx2);
                    constructorx2.elementos.Add(retorno1);
                    Ejecucion3d.cadenota += "init_2" + actualc.nombre + j + "(){\n";
                    t1 = Ejecucion3d.generatemp();
                    Ejecucion3d.cadenota += t1 + "=sp+1;\n";
                    t2 = Ejecucion3d.generatemp();
                    Ejecucion3d.cadenota += t2 + "=stack[" + t1 + "];\n";

                    ParseTreeNode para;
                    actualM.peso = 2;
                    relativa2 = 2;
                    String etiqueta = "";

                    para = constri.ChildNodes.ElementAt(1);

                    foreach (ParseTreeNode parametro2 in para.ChildNodes)
                    {

                        String tipov = parametro2.ChildNodes.ElementAt(0).Token.Text;
                        int tipov2;

                        switch (tipov)
                        {
                            case "entero":
                                tipov2 = 1;
                                break;
                            case "cadena":
                                tipov2 = 2;
                                break;
                            case "booleano":
                                tipov2 = 3;
                                break;
                            case "decimal":
                                tipov2 = 4;
                                break;
                            default:
                                tipov2 = 6;
                                etiqueta = tipov;
                                break;
                        }

                        if (parametro2.ChildNodes.Count == 2)
                        {

                            TS parametro1 = new TS(parametro2.ChildNodes.ElementAt(1).Token.Text, 2, relativa2, tipov2, visibilidad, 1, etiqueta, 0, 0, null);
                            constructorx2.elementos.Add(parametro1);
                            actualM.peso++;
                            relativa2++;
                        }
                        else
                        {
                            ParseTreeNode dimen1 = parametro2.ChildNodes.ElementAt(2);
                            int dimen11 = dimen1.ChildNodes.Count;
                            int[] dimen2 = new int[dimen11];
                            int k = 0;
                            int peso1 = 1;
                            foreach (ParseTreeNode dimen3 in dimen1.ChildNodes)
                            {
                                dimen2[k] = Convert.ToInt32(dimen3.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text);
                                peso1 *= dimen2[k];
                                k++;

                            }
                            TS parametro1 = new TS(parametro2.ChildNodes.ElementAt(1).Token.Text, 2, relativa2, tipov2, visibilidad, 1, tipov, 1, dimen11, dimen2);
                            constructorx2.elementos.Add(parametro1);
                            relativa2++;
                            actualM.peso++;
                            for (int l = 0; l < peso1; l++)
                            {
                                parametro1 = new TS(parametro2.ChildNodes.ElementAt(1).Token.Text + l, 2, relativa2, tipov2, visibilidad, 1, tipov, 0, 0, null);
                                constructorx2.elementos.Add(parametro1);
                                relativa2++;
                                actualM.peso++;
                            }



                        }



                    }

                    ParseTreeNode aux, aux2;
                    int conta = 0;
                    foreach (ParseTreeNode atri in raiz.ChildNodes)
                    {
                        if (atri.Term.Name.Equals("DECG"))
                        {

                            aux2 = atri.ChildNodes.ElementAt(0).ChildNodes.ElementAt(2);
                            String t3 = "00";

                            if (aux2.ChildNodes.Count > 0)
                            {

                                if (aux2.ChildNodes.ElementAt(0).Term.Name.Equals("VALA"))
                                {
                                    Ejecucion3d.cadenota += "heap[" + t2 + "]=";
                                    Ejecucion3d.cadenota += "00;\n";
                                    auxte = t2;
                                    tipoa = 1;
                                    Expt(aux2.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));
                                }
                                else
                                {
                                    if (esarre(conta) > 0)
                                    {
                                        t3 = auxte;
                                    }
                                    else
                                    {
                                        t3 = Expt(aux2.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));

                                    }
                                    Ejecucion3d.cadenota += "heap[" + t2 + "]=";
                                    Ejecucion3d.cadenota += t3 + ";\n";
                                }
                            }
                            else
                            {

                                if (esarre(conta) > 0)
                                {
                                    t3 = auxte;
                                }

                                Ejecucion3d.cadenota += "heap[" + t2 + "]=";
                                Ejecucion3d.cadenota += t3 + ";\n";

                            }


                            Ejecucion3d.cadenota += t2 + " = " + t2 + " + 1;\n";
                            aux2 = atri.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);

                            if (aux2.ChildNodes.Count > 0)
                            {

                                aux2 = aux2.ChildNodes.ElementAt(0);

                                if (aux2.Term.Name.Equals("LID"))
                                {

                                    for (int i = 0; i < aux2.ChildNodes.Count - 1; i++)
                                    {
                                        Ejecucion3d.cadenota += "heap[" + t2 + "]=" + t3 + ";\n";
                                        Ejecucion3d.cadenota += t2 + " = " + t2 + " + 1;\n";

                                    }


                                }

                            }

                            conta++;

                        }


                    }
                    int heredados1 = heredados(actualc);
                    for (int i = 0; i < heredados1; i++)
                    {
                        Ejecucion3d.cadenota += "heap[" + t2 + "]=";
                        Ejecucion3d.cadenota += "00;\n";

                    }
                    ParseTreeNode bloque;
                    if (constri.ChildNodes.Count == 2)
                    {

                        bloque = constri.ChildNodes.ElementAt(1);
                    }
                    else
                    {

                        bloque = constri.ChildNodes.ElementAt(3);
                    }

                    Bloquet(bloque);


                    Ejecucion3d.cadenota += "return;}\n";
                    j++;
                }
            }
            #endregion


            #region metodos_main

            ParseTreeNode aux4, aux5, aux6;

            foreach (ParseTreeNode hijo in raiz.ChildNodes)
            {

                if (hijo.Term.Name.Equals("METODOS"))
                {
                    ParseTreeNode aux;
                    ParseTreeNode parametros = null;
                    ParseTreeNode bloque1;
                    String nombre = "";
                    Ejecucion3d.temporal = 1;
                    String etiqueta = Ejecucion3d.generalabel();
                    int visibilidad = 1;
                    if (hijo.ChildNodes.Count > 5)
                    {
                        aux = hijo.ChildNodes.ElementAt(0);
                        switch (aux.Token.Text)
                        {
                            case "privado":
                                visibilidad = 2;
                                break;
                            case "protegido":
                                visibilidad = 3;
                                break;
                            default:
                                visibilidad = 1;
                                break;
                        }
                        parametros = hijo.ChildNodes.ElementAt(3);
                        bloque1 = hijo.ChildNodes.ElementAt(5);
                        nombre = hijo.ChildNodes.ElementAt(1).Token.Text;


                    }
                    else
                    {
                        parametros = hijo.ChildNodes.ElementAt(1);
                        nombre = hijo.ChildNodes.ElementAt(0).Token.Text;
                        bloque1 = hijo.ChildNodes.ElementAt(2);
                    }

                    TS nuevo = new TS(nombre, 6, 0, 0, visibilidad, 0, etiqueta, 0, 0, null);

                    int pos = sobreescribir(nombre);
                    if (pos > 0)
                    {
                        actualc.elementos.RemoveAt(pos);        
                    }
                    actualc.elementos.Add(nuevo);
                    

                    TS ret = new TS("ret", 0, 0, 0, visibilidad, 1, "", 0, 0, null);
                    nuevo.elementos.Add(ret);
                    nuevo.peso++;
                    actualM = nuevo;

                    if (parametros != null)
                    {

                        ArrayList parametros2 = nombresp(parametros);

                        foreach (TS parametrito in parametros2)
                        {
                            actualM.elementos.Add(parametrito);
                            actualM.peso++;

                        }


                    }
                    Ejecucion3d.cadenota += actualM.etiqueta + "(){\n";
                    Ejecucion3d.temporal = 1;
                    Bloquet(bloque1);

                    Ejecucion3d.cadenota += "return;}\n";

                }

                if (hijo.Term.Name.Equals("FUNCIONES"))
                {
                    ParseTreeNode aux;
                    ParseTreeNode parametros = null;
                    ParseTreeNode bloque1;
                    String nombre = "";
                    String tipof = "";
                    Ejecucion3d.temporal = 1;
                    String etiqueta = Ejecucion3d.generalabel();
                    int visibilidad = 1;
                    if (hijo.ChildNodes.Count == 7)
                    {
                        aux = hijo.ChildNodes.ElementAt(0);
                        switch (aux.Token.Text)
                        {
                            case "privado":
                                visibilidad = 2;
                                break;
                            case "protegido":
                                visibilidad = 3;
                                break;
                            default:
                                visibilidad = 1;
                                break;
                        }
                        parametros = hijo.ChildNodes.ElementAt(4);
                        bloque1 = hijo.ChildNodes.ElementAt(6);
                        tipof = hijo.ChildNodes.ElementAt(1).Token.Text;
                        nombre = hijo.ChildNodes.ElementAt(2).Token.Text;


                    } else if (hijo.ChildNodes.Count == 6)
                    {
                        if (hijo.ChildNodes.ElementAt(1).Term.Name.Equals("id"))
                        {
                            visibilidad = 1;
                            parametros = hijo.ChildNodes.ElementAt(3);
                            bloque1 = hijo.ChildNodes.ElementAt(5);
                            nombre = hijo.ChildNodes.ElementAt(1).Token.Text;
                            tipof = hijo.ChildNodes.ElementAt(0).Token.Text;
                        }
                        else
                        {
                            aux = hijo.ChildNodes.ElementAt(0);
                            switch (aux.Token.Text)
                            {
                                case "privado":
                                    visibilidad = 2;
                                    break;
                                case "protegido":
                                    visibilidad = 3;
                                    break;
                                default:
                                    visibilidad = 1;
                                    break;
                            }
                            tipof = hijo.ChildNodes.ElementAt(1).Token.Text;
                            bloque1 = hijo.ChildNodes.ElementAt(5);
                            nombre = hijo.ChildNodes.ElementAt(2).Token.Text;
                            parametros = null;
                        }
                    }
                    else
                    {
                        tipof = hijo.ChildNodes.ElementAt(0).Token.Text;
                        parametros = null;
                        nombre = hijo.ChildNodes.ElementAt(1).Token.Text;
                        bloque1 = hijo.ChildNodes.ElementAt(4);
                        visibilidad = 1;
                    }
                    int tipov2;

                    switch (tipof)
                    {
                        case "entero":
                            tipov2 = 1;
                            break;
                        case "cadena":
                            tipov2 = 2;
                            break;
                        case "booleano":
                            tipov2 = 3;
                            break;
                        case "decimal":
                            tipov2 = 4;
                            break;
                        case "caracter":
                            tipov2 = 7;
                            break;
                        default:
                            tipov2 = 6;
                            break;
                    }
                    TS nuevo = new TS(nombre, 7, 0, tipov2, visibilidad, 0, etiqueta, 0, 0, null);
                    if (nuevo.tipo2 == 6)
                    {
                        nuevo.auxte = tipof;
                    }
                   
                    TS ret = new TS("ret", 0, 0, 0, visibilidad, 1, "", 0, 0, null);
                    if (nuevo.tipo2 == 6)
                    {
                        nuevo.auxte = tipof;

                    }
                    int pos = sobreescribir(nombre);
                    if (pos > 0)
                    {
                        actualc.elementos.RemoveAt(pos);
                    }
                    actualc.elementos.Add(nuevo);
                    nuevo.elementos.Add(ret);
                    nuevo.peso++;
                    actualM = nuevo;

                    if (parametros != null)
                    {

                        ArrayList parametros2 = nombresp(parametros);

                        foreach (TS parametrito in parametros2)
                        {
                            actualM.elementos.Add(parametrito);
                            actualM.peso++;

                        }


                    }
                    Ejecucion3d.cadenota += actualM.etiqueta + "(){\n";
                    Ejecucion3d.temporal = 1;
                    Bloquet(bloque1);

                    Ejecucion3d.cadenota += "return;}\n";

                }

                #endregion

            }
        }

        public ArrayList constructores(ParseTreeNode raiz)
        {
            ArrayList ret = new ArrayList();

            foreach (ParseTreeNode hijo in raiz.ChildNodes)
            {
                if (hijo.Term.Name.Equals("CONSTRUCTOR"))
                {

                    ret.Add(hijo);

                }

            }

            return ret;
        }

        public void inicio3d()
        {
            Ejecucion3d.cadenota += "sp = 0; \nhp =0; \nselfp = 0; \ngoto main; \n";


            #region:strtoint
            Ejecucion3d.cadenota += "parseint(){\n";
            Ejecucion3d.cadenota += "t1 = sp + 1;\nt4=stack[t1];\nt3 = 0;\n";
            String ini1 = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += ini1 + ":\nt2 = heap[t4];\nif t2 =\\0 ";
            String sal = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += "goto " + sal + ";\nif t2 > 57 goto error_parser;\nif t2 < 48 goto error_parser;\nt3 = t3 * 10;\nt2 = t2 - 48;\n";
            Ejecucion3d.cadenota += "t3 = t3 + t2;\nt4 = t4 + 1;\ngoto " + ini1 + ";\n";
            Ejecucion3d.cadenota += sal + ":\nstack[sp] = t3;\nreturn;}\n";
            #endregion


            #region:strtodouble
            Ejecucion3d.cadenota += "parsedouble(){\n";
            Ejecucion3d.cadenota += "t1 = sp + 1;\nt4=stack[t1];\nt3 = 0;\n";
            ini1 = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += ini1 + ":\nt2 = heap[t4];\nif t2 =\\0 ";
            sal = Ejecucion3d.generalabel();
            String deci = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += "goto error_parser;\nif t2 > 57 goto error_parser;\nif t2 < 48 goto error_parser;\nif t2=46 goto " + deci + ";\n";
            Ejecucion3d.cadenota += "t3 = t3 * 10;\nt2 = t2 - 30;\n";
            Ejecucion3d.cadenota += "t3 = t3 + t2;\nt4 = t4 + 1;\ngoto " + ini1 + ";\n";
            Ejecucion3d.cadenota += deci + ":\n t5 = 1; \nt6 =10;\nt4=t4+1;\nt7 = 0;\n";
            String deci2 = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += deci2 + ":\nt2 = heap[t4];\nif t2 =\\0";
            Ejecucion3d.cadenota += " goto " + sal + ";\nif t2 > 57 goto error_parser;\nif t2 < 48 goto error_parser;\n";
            Ejecucion3d.cadenota += "t5 = t5 /t6;\nt8 = t5 * t2;\nt7 = t7 + t8;\nt4 = t4+1;\nt6=t6*10;\ngoto ";
            Ejecucion3d.cadenota += deci2 + ";\nt9 = t3+t7;\nstack[sp] = t9;\nreturn;}\n";
            #endregion


            #region:inttostr
            Ejecucion3d.cadenota += "inttostr(){\n";
            Ejecucion3d.cadenota += "stack[sp]=hp;\nt1 = sp + 1 ;\nt2 = stack[t1];\nt3 =1;\n";
            ini1 = Ejecucion3d.generalabel();
            sal = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += ini1 + ":\n if t2<t3 goto " + sal + ";\nt3 = t3 * 10;\ngoto " + ini1 + ";\n";
            Ejecucion3d.cadenota += sal + ":\nt3 = t3/10;\nt5 = t3;\n";
            deci = Ejecucion3d.generalabel();
            ini1 = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += deci + ":\nif t5 > t2 goto " + ini1 + ";\nt5 = t5 + t3;\ngoto " + deci + ";\n";
            deci2 = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += ini1 + ":\nt5 = t5 - t3;\nt6 = t5 /t3;\nt6 = t6 + 48;\nheap[hp]= t6;\nhp = hp + 1;\nt2 = t2 - t5;\nif t2 <= 0 goto " + deci2 + ";\n";
            String etix = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += "goto " + sal + ";\n" + deci2 + ":\nif t3=1 goto " + etix + ";\nheap[hp]= 48;\nhp = hp + 1;\nt3 = t3/10;\nif t5=0 goto " + etix + ";\ngoto " + deci2 + ";\n" + etix + ":" + "\nheap[hp]=\\0;\nhp = hp +1;\n";
            Ejecucion3d.cadenota += "return;}\n";
            #endregion

            #region:doubletostr
            Ejecucion3d.cadenota += "doubletostr(){\n";
            Ejecucion3d.cadenota += "stack[sp]=hp;\nt1 = sp + 1 ;\nt2 = stack[t1];\nt3 =1;\n";
            ini1 = Ejecucion3d.generalabel();
            sal = Ejecucion3d.generalabel();
            String label9 = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += "if t2<1 goto " + label9 + ";\n";
            Ejecucion3d.cadenota += ini1 + ":\n if t2<t3 goto " + sal + ";\nt3 = t3 * 10;\ngoto " + ini1 + ";\n";
            Ejecucion3d.cadenota += sal + ":\nt3 = t3/10;\nt5 = t3;\n";
            deci = Ejecucion3d.generalabel();
            ini1 = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += deci + ":\nif t5 > t2 goto " + ini1 + ";\nt5 = t5 + t3;\ngoto " + deci + ";\n";
            deci2 = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += ini1 + ":\nt5 = t5 - t3;\nt6 = t5 /t3;\nt6 = t6 + 48;\nheap[hp]= t6;\nhp = hp + 1;\nt2 = t2 - t5;\nif t2 <= 0 goto " + deci2 + ";\n";
            Ejecucion3d.cadenota += "goto " + sal + ";\n";
            Ejecucion3d.cadenota += label9 + ": \nheap[hp]=30;\nhp = hp + 1;\n";
            Ejecucion3d.cadenota += deci2 + ":\nheap[hp]=46;\nhp = hp +1;\nt7 = 10;\n";
            String label6 = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += label6 + ":\nt8 = 1;\nt9 = t2 * t7;\n";
            String label7 = Ejecucion3d.generalabel();
            String label8 = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += label7 + ":\nif t8> t9 goto " + label8 + ";\nt8 = t8 +1;\n";
            Ejecucion3d.cadenota += "goto " + label7 + ";\n";
            Ejecucion3d.cadenota += label8 + ":\nt8 = t8-1;\nt6 = t8 +48;\nheap[hp]=t6;\nhp = hp+1;\nt9 = t8 /t7;\nt2 = t2 - t9;\nt7 = t7 * 10;\nif t2 != 0 goto " + label6 + ";\n";
            Ejecucion3d.cadenota += "heap[hp]=30;\nhp = hp + 1;\nheap[hp]=\\0;\nhp = hp +1;\nreturn;}\n";
            #endregion

            #region:doubletoint
            Ejecucion3d.cadenota += "doubletoint(){\n";
            Ejecucion3d.cadenota += "t1 = sp + 1 ;\nt2 = stack[t1];\nt3 =1;\nt4 =0;\n";
            ini1 = Ejecucion3d.generalabel();
            sal = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += ini1 + ":\n if t2<t3 goto " + sal + ";\nt3 = t3 * 10;\ngoto " + ini1 + ";\n";
            Ejecucion3d.cadenota += sal + ":\nt3 = t3/10;\nt5 = t3;\n";
            deci = Ejecucion3d.generalabel();
            ini1 = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += deci + ":\nif t5 > t2 goto " + ini1 + ";\nt5 = t5 + t3;\ngoto " + deci + ";\n";
            deci2 = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += ini1 + ":\nt5 = t5 - t3;\nt4 = t4 +t5;\nt2 = t2 - t5;\nif t2 <= 0 goto " + deci2 + ";\n";
            Ejecucion3d.cadenota += "goto " + sal + ";\n" + deci2 + ":\nstack[sp]=t4;\nreturn;}\n";
            #endregion

            #region:imprimir
            Ejecucion3d.cadenota += "imprimir(){\n";
            Ejecucion3d.cadenota += "t1 = sp + 1 ;\nt2 = stack[t1];\n";
            ini1 = Ejecucion3d.generalabel();
            sal = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += ini1 + ":\nt3 =heap[t2];\nif t3 = \\0 goto  " + sal + ";\nprint(\"%d\",t3);\nt2 = t2 + 1;\ngoto " + ini1 + ";\n";
            Ejecucion3d.cadenota += sal + ":\nreturn;}\n";

            #endregion

            #region:unirstr
            Ejecucion3d.cadenota += "unir(){\n";
            Ejecucion3d.cadenota += "stack[sp]= hp;\nt1 = sp +1 ;\nt2= stack[t1];\nt3 = sp + 2;\nt4 = stack[t3];\n";
            ini1 = Ejecucion3d.generalabel();
            sal = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += ini1 + ":\nt5 =heap[t2];\nif t5 = \\0 goto " + sal + ";\n";
            Ejecucion3d.cadenota += "heap[hp]=t5;\nhp = hp + 1;\nt2 = t2 + 1;\ngoto " + ini1 + ";\n";
            ini1 = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += sal + ":\nt6 =heap[t4];\nif t6 = \\0 goto " + ini1 + ";\nheap[hp]=t6;\nhp = hp + 1;\nt4 = t4 + 1;\ngoto " + sal + ";\n";
            Ejecucion3d.cadenota += ini1 + ":\nheap[hp]=\\0;\nhp = hp + 1;\nreturn;}\n";

            #endregion

        }

        public ArrayList variables(ParseTreeNode raiz, int relativa)
        {

            int visibilidad = 1;
            ParseTreeNode aux;

            ArrayList globales = new ArrayList();
            if (raiz.ChildNodes.Count == 2)
            {
                aux = raiz.ChildNodes.ElementAt(0);
                switch (aux.Token.Text)
                {
                    case "privado":
                        visibilidad = 2;
                        break;
                    case "protegido":
                        visibilidad = 3;
                        break;
                    default:
                        visibilidad = 1;
                        break;
                }

                aux = raiz.ChildNodes.ElementAt(1);

            }
            else
            {

                aux = raiz.ChildNodes.ElementAt(0);

            }

            String tipov = aux.ChildNodes.ElementAt(0).Token.Text;
            int tipov2;

            switch (tipov)
            {
                case "entero":
                    tipov2 = 1;
                    break;
                case "cadena":
                    tipov2 = 2;
                    break;
                case "booleano":
                    tipov2 = 3;
                    break;
                case "caracter":
                    tipov2 = 4;
                    break;
                default:
                    tipov2 = 6;
                    break;
            }

            if (aux.ChildNodes.ElementAt(1).ChildNodes.Count == 1)
            {
                aux = aux.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0);

                foreach (ParseTreeNode hijo in aux.ChildNodes)
                {

                    TS nuevo = new TS(hijo.Token.Text, 5, relativa, tipov2, visibilidad, 1, "", 0, 0, null);
                    nuevo.auxte = tipov;
                    relativa++;
                    globales.Add(nuevo);

                }
            }
            else
            {
                ParseTreeNode aux3 = aux.ChildNodes.ElementAt(1);
                ParseTreeNode aux4 = aux3.ChildNodes.ElementAt(1);
                int dimen = aux4.ChildNodes.Count;

                int val2;
                int[] dimensi = new int[dimen];
                int[] valores = new int[dimen];
                int i = 0;
                int peso = 1;
                foreach (ParseTreeNode hijo in aux4.ChildNodes)
                {

                    val2 = resolvertreea(hijo.ChildNodes.ElementAt(1));
                    peso *= val2;
                    valores[i] = peso;
                    dimensi[i] = val2;
                    i++;
                }

                TS nuevo2 = new TS(aux3.ChildNodes.ElementAt(0).Token.Text, 5, relativa, tipov2, visibilidad, 1, "", 1, dimen, dimensi);
                nuevo2.valores = valores;
                nuevo2.auxte = tipov;
                relativa++;
                globales.Add(nuevo2);

                return globales;


            }

            return globales;
        }

        public ArrayList variablesolcg(ParseTreeNode raiz, int relativa)
        {

            int visibilidad = 1;
            ParseTreeNode aux;
            ParseTreeNode aux2;
            ParseTreeNode aux3;

            ArrayList globales = new ArrayList();

            if (raiz.ChildNodes.Count == 4)
            {

                if (raiz.ChildNodes.ElementAt(3).ChildNodes.ElementAt(0).Term.Name.Equals("FUNCIONES"))
                    return globales;

            }
            else
            {
                if (raiz.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Term.Name.Equals("FUNCIONES"))
                    return globales;

            }


            if (raiz.ChildNodes.Count == 4)
            {
                aux = raiz.ChildNodes.ElementAt(0);
                switch (aux.Token.Text)
                {
                    case "privado":
                        visibilidad = 2;
                        break;
                    case "protegido":
                        visibilidad = 3;
                        break;
                    default:
                        visibilidad = 1;
                        break;
                }

                aux = raiz.ChildNodes.ElementAt(1);
                aux2 = raiz.ChildNodes.ElementAt(2);
                aux3 = raiz.ChildNodes.ElementAt(3).ChildNodes.ElementAt(0);

            }
            else
            {

                aux = raiz.ChildNodes.ElementAt(0);
                aux2 = raiz.ChildNodes.ElementAt(1);
                aux3 = raiz.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0);

            }

            String tipov = aux.Token.Text;
            int tipov2;

            switch (tipov)
            {
                case "entero":
                    tipov2 = 1;
                    break;
                case "cadena":
                    tipov2 = 2;
                    break;
                case "booleano":
                    tipov2 = 3;
                    break;
                case "caracter":
                    tipov2 = 4;
                    break;
                default:
                    tipov2 = 6;
                    break;
            }

            if (aux3.ChildNodes.ElementAt(0).ChildNodes.Count != 0)
            {

                if (aux3.ChildNodes.ElementAt(0).Term.Name.Equals("LVEC"))
                {

                    aux3 = aux3.ChildNodes.ElementAt(0);
                    int dimen = aux3.ChildNodes.Count;

                    int val2;
                    int[] dimensi = new int[dimen];
                    int[] valores = new int[dimen];
                    int i = 0;
                    int peso = 1;
                    foreach (ParseTreeNode hijo in aux3.ChildNodes)
                    {

                        val2 = resolverolca(hijo.ChildNodes.ElementAt(0));
                        peso *= val2;
                        valores[i] = peso;
                        dimensi[i] = val2;
                        i++;
                    }

                    TS nuevo2 = new TS(aux2.Token.Text, 5, relativa, tipov2, visibilidad, 1, "", 1, dimen, dimensi);
                    nuevo2.valores = valores;
                    nuevo2.auxte = tipov;
                    relativa++;
                    globales.Add(nuevo2);

                    return globales;

                }
                else
                {

                    aux = aux3.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);

                    foreach (ParseTreeNode hijo in aux.ChildNodes)
                    {

                        TS nuevo = new TS(hijo.Token.Text, 5, relativa, tipov2, visibilidad, 1, "", 0, 0, null);
                        relativa++;
                        nuevo.auxte = tipov;
                        globales.Add(nuevo);

                    }

                }

            }
            else
            {
                TS nuevo2 = new TS(aux2.Token.Text, 5, relativa, tipov2, visibilidad, 1, "", 0, 0, null);
                relativa++;
                nuevo2.auxte = tipov;
                globales.Add(nuevo2);


            }


            return globales;
        }

        public TS metodo(ParseTreeNode raiz, int relativa)
        {

            TS met = new TS();
            int relativa2 = 0;
            ArrayList elementos = new ArrayList();

            int visibilidad;
            ParseTreeNode aux;

            ArrayList globales = new ArrayList();
            aux = raiz.ChildNodes.ElementAt(0);
            switch (aux.Token.Text)
            {
                case "privado":
                    visibilidad = 2;
                    break;
                case "protegido":
                    visibilidad = 3;
                    break;
                default:
                    visibilidad = 1;
                    break;
            }

            aux = raiz.ChildNodes.ElementAt(1);

            if (raiz.ChildNodes.Count == 6)
            {


                aux = raiz.ChildNodes.ElementAt(3);
                String tipov;
                int tipov2;

                foreach (ParseTreeNode para in aux.ChildNodes)
                {

                    tipov = para.ChildNodes.ElementAt(0).Token.Text;


                    switch (tipov)
                    {
                        case "entero":
                            tipov2 = 1;
                            break;
                        case "cadena":
                            tipov2 = 2;
                            break;
                        case "booleano":
                            tipov2 = 3;
                            break;
                        default:
                            tipov2 = 4;
                            break;
                    }


                    TS nueva = new TS(para.ChildNodes.ElementAt(1).Token.Text, 2, relativa2, tipov2, 0, 1, "", 0, 0, null);
                    elementos.Add(nueva);
                    relativa2++;


                }
                aux = raiz.ChildNodes.ElementAt(5);
            }
            else
            {
                aux = raiz.ChildNodes.ElementAt(4);
            }

            ArrayList vari;

            foreach (ParseTreeNode hijo in aux.ChildNodes)
            {


                if (hijo.Term.Name.Equals("DEC"))
                {

                    Terminal ter = new Terminal("aux");
                    SourceLocation sour = new SourceLocation();
                    object val = new object();
                    Token tok = new Token(ter, sour, "aux", val);
                    ParseTreeNode aux2 = new ParseTreeNode(tok);
                    aux2.ChildNodes.Add(hijo);
                    vari = variables(aux2, relativa2);

                    foreach (TS variable in vari)
                    {
                        variable.visibilidad = 0;
                        variable.Tipo = 4;
                        relativa2++;
                        elementos.Add(variable);
                    }


                }
            }

            met.visibilidad = visibilidad;
            met.nombre = raiz.ChildNodes.ElementAt(0).Token.Text;
            met.Tipo = 6;
            met.tipo2 = 0;
            met.posicion = relativa;
            met.elementos = elementos;

            return met;
        }

        public String Exp(ParseTreeNode raiz)
        {

            String ret = "";
            if (raiz.Term.Name.Equals("INSTANCIA"))
            {
                String tipov = raiz.ChildNodes[1].Token.Text;
                int tipov2;

                switch (tipov)
                {
                    case "entero":
                        tipov2 = 1;
                        break;
                    case "cadena":
                        tipov2 = 2;
                        break;
                    case "booleano":
                        tipov2 = 3;
                        break;
                    case "decimal":
                        tipov2 = 4;
                        break;
                    default:
                        tipov2 = 6;
                        break;
                }

                if (tipov2 != 6)
                    {
                        errore += "Problema en declarar primitivo como instancia, col: " + raiz.ChildNodes[0].Token.Location.Column + ",Fil :" + raiz.ChildNodes[0].Token.Location.Line + "\n";
                        return "";

                    }
                  


                    if (!tipov.Equals(tipov))
                    {

                        errore += "Problema en  instancia, col: " + raiz.ChildNodes[0].Token.Location.Column + ",Fil :" + raiz.ChildNodes[0].Token.Location.Line + "\n";
                        return "";
                    }
                    bool bandera = true;
                    foreach (String clase in actualc.importadas)
                    {

                        if (clase.Equals(tipov))
                        {
                            bandera = false;


                        }


                    }

                   


                    ParseTreeNode insta = raiz;

                    int canti = 0;
                    int canti2 = 0;
                    ParseTreeNode para;
                    if (insta.ChildNodes.Count > 2)
                    {
                        para = insta.ChildNodes.ElementAt(2);
                        canti = para.ChildNodes.Count;
                    }

                    foreach (TS clase in TablaSimbolos)
                    {
                        if (clase.nombre.Equals(tipov))
                        {

                            foreach (TS constru in clase.elementos)
                            {
                                String[] arregla = Regex.Split(constru.nombre, "init_");
                                char sig = '1';
                                if (arregla.Count() > 1)
                                {
                                    String auxs = arregla[1];
                                    sig = auxs[0];
                                }




                                if (constru.Tipo == 8 && sig.Equals('2'))
                                {

                                    canti2 = cantip(constru);

                                    if (canti2 == canti)
                                    {

                                        if (canti2 == 0)
                                        {
                                            String t1 = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += t1 + "= sp + " + actualM.peso + ";\n";
                                            Ejecucion3d.cadenota += "stack[" + t1 + "] = 00;\n";
                                            Ejecucion3d.cadenota += "sp = sp + " + actualM.peso + ";\n";
                                            Ejecucion3d.cadenota += "init_" + tipov + "1();\n";
                                            t1 = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += t1 + "=stack[sp];\n";
                                            Ejecucion3d.cadenota += "sp = sp - " + actualM.peso + ";\n";
                                           
                                            String t2 = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += t2 + "= sp + " + actualM.peso + ";\n";
                                            Ejecucion3d.cadenota += "stack[" + t2 + "] = 00;\n";
                                            Ejecucion3d.cadenota += t2 + "=" + t2 + " + 1 ;\n";
                                            Ejecucion3d.cadenota += "stack[" + t2 + "] = " + t1 + ";\n";
                                            Ejecucion3d.cadenota += "sp = sp + " + actualM.peso + ";\n";
                                            String tsp = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += tsp + "= selfp;\n";
                                            Ejecucion3d.cadenota += "selfp = " + t1 + ";\n";
                                            Ejecucion3d.cadenota += constru.etiqueta + "();\n";
                                            Ejecucion3d.cadenota += "sp = sp - " + actualM.peso + ";\n";
                                            Ejecucion3d.cadenota += "selfp = " + tsp + ";\n";
                                            return t1;
                                        }
                                        else
                                        {


                                            String t1 = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += t1 + "= sp + " + actualM.peso + ";\n";
                                            Ejecucion3d.cadenota += "stack[" + t1 + "] = 00;\n";
                                            Ejecucion3d.cadenota += "sp = sp + " + actualM.peso + ";\n";
                                            Ejecucion3d.cadenota += "init_" + tipov + "1();\n";
                                            t1 = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += t1 + "=stack[sp];\n";
                                            Ejecucion3d.cadenota += "sp = sp - " + actualM.peso + ";\n";
                                           
                                            String t2 = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += t2 + "= sp + " + actualM.peso + ";\n";
                                            Ejecucion3d.cadenota += "stack[" + t2 + "] = 00;\n";
                                            Ejecucion3d.cadenota += t2 + "=" + t2 + " + 1 ;\n";
                                            Ejecucion3d.cadenota += "stack[" + t2 + "] = " + t1 + ";\n";

                                            ParseTreeNode parafin = insta.ChildNodes.ElementAt(2);
                                            foreach (ParseTreeNode parax in parafin.ChildNodes)
                                            {

                                                String ex = Exp(parax);
                                                Ejecucion3d.cadenota += t2 + "=" + t2 + " + 1 ;\n";
                                                Ejecucion3d.cadenota += "stack[" + t2 + "] = " + ex + ";\n";

                                            }
                                            Ejecucion3d.cadenota += "sp = sp + " + actualM.peso + ";\n";
                                            String tsp = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += tsp + "= selfp;\n";
                                            Ejecucion3d.cadenota += "selfp = " + t1 + ";\n";
                                            Ejecucion3d.cadenota += constru.etiqueta + "();\n";
                                            Ejecucion3d.cadenota += "sp = sp - " + actualM.peso + ";\n";
                                            Ejecucion3d.cadenota += "selfp = " + tsp + ";\n";
                                            return t1;

                                        }


                                    }
                                }

                            }

                        }
                    }



                

            }
            if (raiz.Term.Name.Equals("VALA"))
            {

                String t1 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t1 + "=hp;\n";
                ArrayList valores = vala(raiz);
                foreach (ParseTreeNode hijo in valores)
                {
                    String t3 = Exp(hijo);

                    Ejecucion3d.cadenota += "heap[hp]=" + t3 + ";\n";
                    Ejecucion3d.cadenota += "hp = hp + 1 ;\n";
                }


                return t1;

            }

            if (raiz.ChildNodes.Count == 1)
            {

                if (raiz.ChildNodes.ElementAt(0).Term.Name.Equals("LLAMADA"))
                {

                    ParseTreeNode aux = raiz.ChildNodes.ElementAt(0);
                    String nombre = aux.ChildNodes.ElementAt(0).Token.Text;

                    int canti = aux.ChildNodes.ElementAt(1).ChildNodes.Count();

                    foreach (TS hijo in actualc.elementos)
                    {

                        if (hijo.nombre.Equals(nombre))
                        {
                            int canti2 = cantip(hijo);
                            if (canti2 == canti)
                            {
                                String t1 = Ejecucion3d.generatemp();
                                Ejecucion3d.cadenota += t1 + "= sp + " + actualM.peso + ";\n";
                                Ejecucion3d.cadenota += "stack[" + t1 + "]=00;\n";
                                Ejecucion3d.cadenota += t1 + "=" + t1 + "+ 1;\n";
                                foreach (ParseTreeNode para in aux.ChildNodes.ElementAt(1).ChildNodes)
                                {
                                    String t2 = Exp(para);
                                    Ejecucion3d.cadenota += "stack[" + t1 + "]=" + t2 + ";\n";
                                    Ejecucion3d.cadenota += t1 + "=" + t1 + "+ 1;\n";
                                }

                                Ejecucion3d.cadenota += "sp = sp +" + actualM.peso + ";\n";
                                Ejecucion3d.cadenota += hijo.etiqueta + "();\n";
                                String ret1 = Ejecucion3d.generatemp();
                                Ejecucion3d.cadenota += ret1 + "= stack[sp];\n";
                                Ejecucion3d.cadenota += "sp = sp -" + actualM.peso + ";\n";
                                return ret1;

                            }

                        }


                    }





                }

                else if (!raiz.ChildNodes.ElementAt(0).Term.Name.Equals("ATRIBUTO"))
                {
                    tipoex = 4;
                    ret = raiz.ChildNodes.ElementAt(0).Token.Text;
                    ParseTreeNode auxx = raiz.ChildNodes.ElementAt(0);
                    if (ret.Equals("false"))
                    {
                        ret = "0";
                    }

                    if (ret.Equals("true"))
                    {
                        ret = "1";

                    }
                    switch (auxx.Term.Name)
                    {
                        case "numero":
                            tipoex = 0;
                            break;
                        case "decimal":
                            tipoex = 1;
                            break;
                        case "caracter":
                            tipoex = 3;
                            char aux2 = auxx.Token.Text[0];
                            int i = aux2;
                            ret = i.ToString();
                            break;

                        case "cadena":
                            tipoex = 2;
                            String cad = auxx.Token.Text;
                            String[] cad2 = cad.Split('\"');
                            cad = cad2[1];
                            int ii = 0;
                            String cad1 = "";
                            String tx = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += tx + "= hp;\n";
                            int tama = cad.Count() + 1;
                            Ejecucion3d.cadenota += "hp = hp + " + tama + ";\n";
                            String ty = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += ty + " = " + tx + ";\n";
                            Boolean escape = false;
                            foreach (char mander in cad)
                            {
                                ii = mander;
                                cad1 = ii.ToString();

                                if (escape && cad1.Equals("110"))
                                {
                                    cad1 = "10";
                                    escape = false;
                                } else if (escape)
                                {
                                    Ejecucion3d.cadenota += "heap[" + ty + "]=92;\n";
                                    Ejecucion3d.cadenota += ty + "=" + ty + "+ 1 ;\n";
                                    escape = false;
                                }

                                if (cad1.Equals("92"))
                                {
                                    escape = true;
                                }

                                if (!escape)
                                {

                                    Ejecucion3d.cadenota += "heap[" + ty + "]=" + cad1 + ";\n";
                                    Ejecucion3d.cadenota += ty + "=" + ty + "+ 1 ;\n";
                                }
                            }
                            Ejecucion3d.cadenota += "heap[" + ty + "]= \\0 ;\n";
                            ret = tx;

                            break;

                        default:
                            tipoex = 4;
                            break;
                    }



                }
                else
                {

                    ParseTreeNode aux, aux2;

                    aux = raiz.ChildNodes.ElementAt(0);
                    String nombre = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                    bool bandera = false;

                    if (aux.ChildNodes.Count == 1)
                    {

                        foreach (TS tablita in actualM.elementos)
                        {

                            if (tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                            {
                                String t2 = Ejecucion3d.generatemp();
                                Ejecucion3d.cadenota += t2 + "= sp + " + tablita.posicion + ";\n";


                                if (tablita.arreglo == 1)
                                {
                                    if (aux.ChildNodes.ElementAt(0).ChildNodes.Count > 1)
                                    {

                                        ParseTreeNode lvec = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);
                                        ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                                        int ii = 0;
                                        foreach (ParseTreeNode EE in lvec.ChildNodes)
                                        {
                                            E[ii] = EE.ChildNodes.ElementAt(0);
                                            ii++;
                                        }
                                        String t3 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t3 + "= stack[" + t2 + "];\n";
                                        enstack = true;
                                        t2 = serializar(E, tablita.tamanios, t3, tablita.valores);
                                        enstack = false;
                                        String t5 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t5 + "= heap[" + t2 + "];\n";
                                        tipoex = conver(tablita.tipo2);
                                        return t5;


                                    }
                                    else
                                    {

                                        errore += "Imposible acceder a un atributo tipo arreglo sin posicion especificada," + aux.ChildNodes.ElementAt(0).Token.Text;
                                        return "";
                                    }


                                }
                                else
                                {

                                    String t5 = Ejecucion3d.generatemp();
                                    tipoex = conver(tablita.tipo2);
                                    Ejecucion3d.cadenota += t5 + "= stack[" + t2 + "];\n";
                                    return t5;

                                }

                            }
                        }
                        if (!bandera)
                        {

                            foreach (TS tablita in actualc.elementos)
                            {

                                if (tablita.nombre.Equals(nombre))
                                {
                                    String t2 = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += t2 + "= selfp + " + tablita.posicion + ";\n";
                                    if (tablita.arreglo == 1)
                                    {
                                        if (aux.ChildNodes.ElementAt(0).ChildNodes.Count > 1)
                                        {

                                            ParseTreeNode lvec = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);
                                            ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                                            int ii = 0;
                                            foreach (ParseTreeNode EE in lvec.ChildNodes)
                                            {
                                                E[ii] = EE.ChildNodes.ElementAt(0);
                                                ii++;
                                            }
                                            t2 = serializar(E, tablita.tamanios, t2, tablita.valores);
                                            String t5 = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += t5 + "= heap[" + t2 + "];\n";
                                            tipoex = conver(tablita.tipo2);
                                            return t5;


                                        }
                                        else
                                        {

                                            errore += "Imposible acceder a un atributo tipo arreglo sin posicion especificada," + aux.ChildNodes.ElementAt(0).Token.Text;
                                            return "";
                                        }


                                    }
                                    else
                                    {

                                        String t5 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t5 + "= heap[" + t2 + "];\n";
                                        tipoex = conver(tablita.tipo2);
                                        return t5;

                                    }


                                }


                            }




                        }

                        if (!bandera)
                        {

                            error = 1;
                            return "";

                        }


                    }
                    else
                    {
                        String t4 = "";
                        foreach (TS tablita in actualM.elementos)
                        {

                            if (tablita.Tipo == 3 && tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                            {
                                String t2 = Ejecucion3d.generatemp();
                                Ejecucion3d.cadenota += t2 + "= sp + " + tablita.posicion + ";\n";
                                Ejecucion3d.cadenota += t2 + "= stack[" + t2 + "];\n";
                                String clase = tablita.etiqueta;
                                if (tablita.arreglo == 1)
                                {
                                    if (aux.ChildNodes.ElementAt(0).ChildNodes.Count > 1)
                                    {

                                        ParseTreeNode lvec = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);
                                        ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                                        int ii = 0;
                                        foreach (ParseTreeNode EE in lvec.ChildNodes)
                                        {
                                            E[ii] = EE.ChildNodes.ElementAt(0);
                                            ii++;
                                        }
                                        t2 = serializar(E, tablita.tamanios, t2, tablita.valores);


                                    }
                                    else
                                    {

                                        errore += "Imposible acceder a un atributo tipo arreglo sin posicion especificada," + aux.ChildNodes.ElementAt(0).Token.Text;
                                        return "";
                                    }


                                }

                                t4 = t2;
                                int i = 0;

                                Boolean banderavalor = false;
                                foreach (ParseTreeNode atri in aux.ChildNodes)
                                {
                                    if (i == 0)
                                    {

                                    }
                                    else
                                    {
                                        nombre = atri.ChildNodes.ElementAt(0).Token.Text;

                                        foreach (TS clasesita in TS.TablaSimbolos)
                                        {
                                            if (clasesita.nombre.Equals(clase))
                                            {

                                                foreach (TS tablita2 in clasesita.elementos)
                                                {
                                                    if (tablita2.nombre.Equals(nombre))
                                                    {
                                                        banderavalor = false;
                                                        //ver si es metodo o funcion y si es funcion que devuelva objeto para seguir 
                                                        String t3 = Ejecucion3d.generatemp();
                                                        String reto = Ejecucion3d.generatemp();
                                                        if (tablita2.Tipo == 6 || tablita2.Tipo == 7)
                                                        {
                                                            Ejecucion3d.cadenota += t3 + "= selfp;\n";
                                                            Ejecucion3d.cadenota += "selfp = " + t4 + ";\n";
                                                            ParseTreeNode param = atri.ChildNodes.ElementAt(1);
                                                            String txy = Ejecucion3d.generatemp();
                                                            Ejecucion3d.cadenota += txy + "= sp +" + actualM.peso + ";\n";
                                                            Ejecucion3d.cadenota += "stack[" + txy + "]=00;\n";
                                                            Ejecucion3d.cadenota += txy + "=" + txy + "+ 1;\n";
                                                            foreach (ParseTreeNode param1 in param.ChildNodes)
                                                            {
                                                                String tyx = Exp(param1);
                                                                Ejecucion3d.cadenota += "stack[" + txy + "]=" + tyx + ";\n";
                                                                Ejecucion3d.cadenota += txy + "=" + txy + "+ 1;\n";
                                                            }
                                                            Ejecucion3d.cadenota += "sp = sp +" + actualM.peso + ";\n";
                                                            Ejecucion3d.cadenota += tablita2.etiqueta + "();\n";
                                                            Ejecucion3d.cadenota += reto + "=stack[sp];\n";
                                                            Ejecucion3d.cadenota += "sp = sp -" + actualM.peso + ";\n";
                                                            Ejecucion3d.cadenota += "selfp = " + t3 + ";\n";
                                                            banderavalor = true;
                                                            t4 = reto;
                                                        }else { 

                                                        Ejecucion3d.cadenota += t3 + "=" + t4 + "+" + tablita2.posicion + ";\n";
                                                        if (tablita2.arreglo == 1)
                                                        {
                                                            if (atri.ChildNodes.Count > 1)
                                                            {

                                                                ParseTreeNode lvec = atri.ChildNodes.ElementAt(1);
                                                                ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                                                                int ii = 0;
                                                                foreach (ParseTreeNode EE in lvec.ChildNodes)
                                                                {
                                                                    E[ii] = EE.ChildNodes.ElementAt(0);
                                                                    ii++;
                                                                }
                                                                t4 = serializar(E, tablita2.tamanios, t3, tablita2.valores);


                                                            }
                                                            else
                                                            {

                                                                errore += "Imposible acceder a un atributo tipo arreglo sin posicion especificada," + aux.ChildNodes.ElementAt(0).Token.Text;
                                                                return "";
                                                            }


                                                        }
                                                        else if (tablita2.Tipo == 3)
                                                        {
                                                            Ejecucion3d.cadenota += t4 + "= heap[" + t3 + "];\n";


                                                        }
                                                        else
                                                        {
                                                            t4 = t3;

                                                        }
                                                    }
                                                        if (tablita2.tipo2 == 6&&tablita2.Tipo==7)
                                                        {
                                                            clase = tablita2.auxte;
                                                        }
                                                        else
                                                        {
                                                            clase = tablita2.etiqueta;
                                                        }
                                                        tipoex = conver(tablita2.tipo2);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    i++;
                                }
                                if (banderavalor)
                                {
                                    return t4;
                                }

                                String t5 = Ejecucion3d.generatemp();
                                Ejecucion3d.cadenota += t5 + "= heap[" + t4 + "];\n";
                                return t5;
                            }
                        }
                        if (!bandera)
                        {
                            foreach (TS tablita in actualc.elementos)
                            {

                                if ((tablita.Tipo == 3|tablita.tipo2==6) && tablita.nombre.Equals(nombre))
                                {
                                    String t2 = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += t2 + "= selfp + " + tablita.posicion + ";\n";
                                    if (tablita.arreglo == 1)
                                    {
                                        if (aux.ChildNodes.ElementAt(0).ChildNodes.Count > 1)
                                        {

                                            ParseTreeNode lvec = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);
                                            ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                                            int ii = 0;
                                            foreach (ParseTreeNode EE in lvec.ChildNodes)
                                            {
                                                E[ii] = EE.ChildNodes.ElementAt(0);
                                                ii++;
                                            }
                                            t2 = serializar(E, tablita.tamanios, t2, tablita.valores);


                                        }
                                        else
                                        {

                                            errore += "Imposible acceder a un atributo tipo arreglo sin posicion especificada," + aux.ChildNodes.ElementAt(0).Token.Text;
                                            return "";
                                        }


                                    }
                                    String clase = tablita.etiqueta;
                                    if (clase.Equals(""))
                                    {
                                        clase = tablita.auxte;
                                    }
                                    t4 = t2;
                                    int i = 0;


                                    foreach (ParseTreeNode atri in aux.ChildNodes)
                                    {
                                        if (i == 0)
                                        {

                                        }
                                        else
                                        {
                                            try
                                            {
                                                nombre = atri.Token.Text;
                                            }
                                            catch
                                            {
                                                nombre = atri.ChildNodes[0].Token.Text;
                                            }

                                            foreach (TS clasesita in TS.TablaSimbolos)
                                            {
                                                if (clasesita.nombre.Equals(clase))
                                                {

                                                    foreach (TS tablita2 in clasesita.elementos)
                                                    {
                                                        if (tablita2.nombre.Equals(nombre))
                                                        {
                                                            String t3 = Ejecucion3d.generatemp();

                                                            Ejecucion3d.cadenota += t3 + " = heap[" + t4 + "];\n";
                                                            t4 = Ejecucion3d.generatemp();
                                                            Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + tablita2.posicion + ";\n";
                                                            if (tablita2.arreglo == 1)
                                                            {
                                                                if (atri.ChildNodes.ElementAt(0).ChildNodes.Count > 1)
                                                                {

                                                                    ParseTreeNode lvec = atri.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);
                                                                    ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                                                                    int ii = 0;
                                                                    foreach (ParseTreeNode EE in lvec.ChildNodes)
                                                                    {
                                                                        E[ii] = EE.ChildNodes.ElementAt(0);
                                                                        ii++;
                                                                    }
                                                                    t2 = serializar(E, tablita.tamanios, t2, tablita2.valores);


                                                                }
                                                                else
                                                                {

                                                                    errore += "Imposible acceder a un atributo tipo arreglo sin posicion especificada," + aux.ChildNodes.ElementAt(0).Token.Text;
                                                                    return "";
                                                                }


                                                            }
                                                            tipoex = conver(tablita2.tipo2);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        i++;
                                    }

                                    String t5 = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += t5 + "= heap[" + t4 + "];\n";

                                    return t5;
                                }
                            }



                        }

                        if (!bandera)
                        {

                            error = 1;
                            return "";

                        }


                    }



                }


            }
            else
            {

                String t1 = Exp(raiz.ChildNodes.ElementAt(0));


                String t3 = Ejecucion3d.generatemp();
                String tipo = raiz.ChildNodes.ElementAt(1).Token.Text;
                String t2 = "";
                int tip1 = tipoex;
                t2 = Exp(raiz.ChildNodes.ElementAt(2));
                int tip2 = tipoex;

                switch (tipo)
                {
                    case ">":
                    case "<":
                    case "<=":
                    case ">=":
                    case "==":
                    case "!=":

                        Ejecucion3d.cadenota += t3 + "= 0";
                        String eti1 = Ejecucion3d.generalabel();
                        String t4 = "if " + t1 + tipo + t2 + "then goto " + eti1 + ";\n";
                        Ejecucion3d.cadenota += t4;
                        String eti2 = Ejecucion3d.generalabel();
                        t4 = "goto " + eti2 + ";\n";
                        Ejecucion3d.cadenota += t4;
                        t4 = eti1 + ": " + t3 + "= 1" + ";\n";
                        Ejecucion3d.cadenota += t4;
                        t4 = eti2 + ": ;\n";
                        Ejecucion3d.cadenota += t4;
                        tipoex = 4;
                        break;

                    case "+":
                        if (tip1 == 2 || tip2 == 2)
                        {
                            String tx;

                            switch (tip1)
                            {
                                case 0:
                                    tx = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += tx + "= sp + " + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "stack[" + tx + "]=00;\n";
                                    Ejecucion3d.cadenota += tx + "=" + tx + "+1;\n";
                                    Ejecucion3d.cadenota += "stack[" + tx + "]=" + t1 + ";\n";
                                    Ejecucion3d.cadenota += "sp = sp +" + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "inttostr();\n";
                                    Ejecucion3d.cadenota += t1 + "=stack[sp];\n";
                                    Ejecucion3d.cadenota += "sp = sp -" + actualM.peso + ";\n";
                                    break;
                                case 1:
                                    tx = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += tx + "= sp + " + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "stack[" + tx + "]=00;\n";
                                    Ejecucion3d.cadenota += tx + "=" + tx + "+1;\n";
                                    Ejecucion3d.cadenota += "stack[" + tx + "]=" + t1 + ";\n";
                                    Ejecucion3d.cadenota += "sp = sp +" + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "doubletostr();\n";
                                    Ejecucion3d.cadenota += t1 + "=stack[sp];\n";
                                    Ejecucion3d.cadenota += "sp = sp -" + actualM.peso + ";\n";
                                    break;


                                case 2:
                                    break;
                                default:
                                    //error **************************************************************
                                    break;
                            }
                            switch (tip2)
                            {
                                // int = 0, char = 3, string = 2, decimal = 1 , booleano = 4
                                case 0:
                                    tx = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += tx + "= sp + " + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "stack[" + tx + "]=00;\n";
                                    Ejecucion3d.cadenota += tx + "=" + tx + "+1;\n";
                                    Ejecucion3d.cadenota += "stack[" + tx + "]=" + t2 + ";\n";
                                    Ejecucion3d.cadenota += "sp = sp +" + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "inttostr();\n";
                                    Ejecucion3d.cadenota += t2 + "=stack[sp];\n";
                                    Ejecucion3d.cadenota += "sp = sp -" + actualM.peso + ";\n";
                                    break;
                                case 1:
                                    tx = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += tx + "= sp + " + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "stack[" + tx + "]=00;\n";
                                    Ejecucion3d.cadenota += tx + "=" + tx + "+1;\n";
                                    Ejecucion3d.cadenota += "stack[" + tx + "]=" + t2 + ";\n";
                                    Ejecucion3d.cadenota += "sp = sp +" + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "doubletostr();\n";
                                    Ejecucion3d.cadenota += t2 + "=stack[sp];\n";
                                    Ejecucion3d.cadenota += "sp = sp -" + actualM.peso + ";\n";
                                    break;

                                case 2:
                                    break;
                                default:
                                    //error **************************************************************
                                    break;
                            }

                            tx = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += tx + "= sp + " + actualM.peso + ";\n";
                            Ejecucion3d.cadenota += "stack[" + tx + "]=00;\n";
                            Ejecucion3d.cadenota += tx + "=" + tx + "+1;\n";
                            Ejecucion3d.cadenota += "stack[" + tx + "]=" + t1 + ";\n";
                            Ejecucion3d.cadenota += tx + "=" + tx + "+1;\n";
                            Ejecucion3d.cadenota += "stack[" + tx + "]=" + t2 + ";\n";
                            Ejecucion3d.cadenota += "sp = sp +" + actualM.peso + ";\n";
                            Ejecucion3d.cadenota += "unir();\n";
                            Ejecucion3d.cadenota += t3 + "=stack[sp];\n";
                            Ejecucion3d.cadenota += "sp = sp -" + actualM.peso + ";\n";
                            tipoex = 2;

                        }
                        else if (tip1 < 2 && tip2 < 2)
                        {
                            if (tip1 == 1 || tip2 == 1)
                            {
                                tipoex = 1;

                            }
                            else { tipoex = 0; }

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "+" + t2 + ";\n";

                        }
                        else if (tip1 == 0 || tipo2 == 0)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "+" + t2 + ";\n";
                            tipoex = 0;

                        }
                        else if (tip1 == 1 || tipo2 == 1)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "+" + t2 + ";\n";
                            tipoex = 1;

                        }
                        else if (tip1 == 4 && tip2 == 4)
                        {
                            String ti = Ejecucion3d.generatemp();
                            String eti = Ejecucion3d.generalabel();
                            String sal = Ejecucion3d.generalabel();
                            Ejecucion3d.cadenota += ti + "= " + t1 + "+" + t2 + ";\n";
                            Ejecucion3d.cadenota += t3 + "= 0;\n";
                            Ejecucion3d.cadenota += "if " + ti + " == 0 then goto " + sal + "\n";
                            Ejecucion3d.cadenota += t3 + "= 1;";
                            Ejecucion3d.cadenota += sal + ":\n";
                            tipoex = 4;

                        }
                        else
                        {

                            //error************************

                        }



                        break;
                    case "-":
                        if (tip1 == 2 || tip2 == 2)
                        {
                            //*****************error

                        }
                        else if (tip1 < 2 && tip2 < 2)
                        {
                            if (tip1 == 1 || tip2 == 1)
                            {
                                tipoex = 1;

                            }
                            else { tipoex = 0; }

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "-" + t2 + ";\n";

                        }
                        else if (tip1 == 0 || tipo2 == 0)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "-" + t2 + ";\n";
                            tipoex = 0;

                        }
                        else if (tip1 == 1 || tipo2 == 1)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "-" + t2 + ";\n";
                            tipoex = 1;

                        }
                        else
                        {
                            //******************error
                        }

                        break;

                    case "*":
                        if (tip1 == 2 || tip2 == 2)
                        {
                            //*****************casteo de cadenas algo pisado de hacer

                        }
                        else if (tip1 < 2 && tip2 < 2)
                        {
                            if (tip1 == 1 || tip2 == 1)
                            {
                                tipoex = 1;

                            }
                            else { tipoex = 0; }

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "*" + t2 + ";\n";

                        }
                        else if (tip1 == 0 || tipo2 == 0)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "*" + t2 + ";\n";
                            tipoex = 0;

                        }
                        else if (tip1 == 1 || tipo2 == 1)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "*" + t2 + ";\n";
                            tipoex = 1;

                        }
                        else if (tip1 == 4 && tip2 == 4)
                        {
                            String ti = Ejecucion3d.generatemp();
                            String eti = Ejecucion3d.generalabel();
                            String sal = Ejecucion3d.generalabel();
                            Ejecucion3d.cadenota += ti + "= " + t1 + "+" + t2 + ";\n";
                            Ejecucion3d.cadenota += t3 + "= 0;\n";
                            Ejecucion3d.cadenota += "if " + ti + " <2 then goto " + sal + "\n";
                            Ejecucion3d.cadenota += t3 + "= 1;";
                            Ejecucion3d.cadenota += sal + ":\n";
                            tipoex = 4;

                        }
                        else
                        {

                            //error************************

                        }

                        break;
                    case "/":
                        if (tip1 == 2 || tip2 == 2)
                        {
                            //*****************casteo de cadenas algo pisado de hacer

                        }
                        else if (tip1 < 2 && tip2 < 2)
                        {
                            if (tip1 == 1 || tip2 == 1)
                            {
                                tipoex = 1;

                            }
                            else { tipoex = 1; }

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "/" + t2 + ";\n";

                        }
                        else if (tip1 == 0 || tipo2 == 0)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "/" + t2 + ";\n";
                            tipoex = 1;

                        }
                        else if (tip1 == 1 || tipo2 == 1)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "/" + t2 + ";\n";
                            tipoex = 1;

                        }

                        else
                        {

                            //error************************

                        }

                        break;

                    case "pow":
                        if (tip1 == 2 || tip2 == 2)
                        {
                            //*****************casteo de cadenas algo pisado de hacer

                        }
                        else if (tip1 < 2 && tip2 < 2)
                        {
                            if (tip1 == 1 || tip2 == 1)
                            {
                                tipoex = 1;

                            }
                            else { tipoex = 0; }

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "^" + t2 + ";\n";

                        }
                        else if (tip1 == 0 || tipo2 == 0)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "^" + t2 + ";\n";
                            tipoex = 0;

                        }
                        else if (tip1 == 1 || tipo2 == 1)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "^" + t2 + ";\n";
                            tipoex = 1;

                        }

                        else
                        {

                            //error************************

                        }
                        break;
                    default:

                        break;
                }



                ret = t3;

            }
            return ret;
        }

        public String Expt(ParseTreeNode raiz)
        {

            String ret = "";

            if (raiz.Term.Name.Equals("VALA"))
            {

                String t1 = auxte;
                ArrayList valores = vala(raiz);


                foreach (ParseTreeNode hijo in valores)
                {
                    Ejecucion3d.cadenota += t1 + "=" + t1 + "+ 1 ;\n";
                    String t3 = Exp(hijo);
                    if (tipoa == 2)
                    {
                        Ejecucion3d.cadenota += "stack[" + t1 + "]=" + t3 + ";\n";
                    }
                    else
                    {
                        Ejecucion3d.cadenota += "heap[" + t1 + "]=" + t3 + ";\n";

                    }

                }


                return "";

            }

            //*******************************************************

            if (raiz.ChildNodes.Count == 1)
            {
                if (!raiz.ChildNodes.ElementAt(0).Term.Name.Equals("ATRIBUTO"))
                {
                    tipoex = 4;
                    ret = raiz.ChildNodes.ElementAt(0).Token.Text;
                    ParseTreeNode auxx = raiz.ChildNodes.ElementAt(0);
                    if (ret.Equals("false"))
                    {
                        ret = "0";
                    }

                    if (ret.Equals("true"))
                    {
                        ret = "1";

                    }
                    switch (auxx.Term.Name)
                    {
                        case "numero":
                            tipoex = 0;
                            break;
                        case "decimal":
                            tipoex = 1;
                            break;
                        case "caracter":
                            tipoex = 3;
                            char aux2 = auxx.Token.Text[0];
                            int i = aux2;
                            ret = i.ToString();
                            break;

                        case "cadena":
                            tipoex = 2;
                            String cad = auxx.Token.Text;
                            String[] cad2 = cad.Split('\"');
                            cad = cad2[1];
                            int ii = 0;
                            String cad1 = "";
                            String tx = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += tx + "= hp;\n";
                            int tama = cad.Count() + 1;
                            Ejecucion3d.cadenota += "hp = hp + " + tama + ";\n";
                            String ty = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += ty + " = " + tx + ";\n";
                            foreach (char mander in cad)
                            {
                                ii = mander;
                                cad1 = ii.ToString();

                                Ejecucion3d.cadenota += "heap[" + ty + "]=" + cad1 + ";\n";
                                Ejecucion3d.cadenota += ty + "=" + ty + "+ 1 ;\n";

                            }
                            Ejecucion3d.cadenota += "heap[" + ty + "]= \\0 ;\n";
                            ret = tx;

                            break;

                        default:
                            tipoex = 4;
                            break;
                    }



                }
                else
                {

                    ParseTreeNode aux, aux2;

                    aux = raiz.ChildNodes.ElementAt(0);
                    String nombre = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;

                    bool bandera = false;

                    if (aux.ChildNodes.Count == 1)
                    {

                        foreach (TS tablita in actualM.elementos)
                        {

                            if (tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                            {
                                String t2 = Ejecucion3d.generatemp();
                                Ejecucion3d.cadenota += t2 + "= sp + " + tablita.posicion + ";\n";


                                if (tablita.arreglo == 1)
                                {
                                    if (aux.ChildNodes.ElementAt(0).ChildNodes.Count > 1)
                                    {

                                        ParseTreeNode lvec = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);
                                        ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                                        int ii = 0;
                                        foreach (ParseTreeNode EE in lvec.ChildNodes)
                                        {
                                            E[ii] = EE.ChildNodes.ElementAt(0);
                                            ii++;
                                        }
                                        t2 = serializar(E, tablita.tamanios, t2, tablita.valores);
                                        String t5 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t5 + "= stack[" + t2 + "];\n";
                                        tipoex = conver(tablita.tipo2);
                                        return t5;


                                    }
                                    else
                                    {

                                        errore += "Imposible acceder a un atributo tipo arreglo sin posicion especificada," + aux.ChildNodes.ElementAt(0).Token.Text;
                                        return "";
                                    }


                                }
                                else
                                {

                                    String t5 = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += t5 + "= stack[" + t2 + "];\n";
                                    tipoex = conver(tablita.tipo2);
                                    return t5;

                                }

                            }
                        }
                        if (!bandera)
                        {

                            foreach (TS tablita in actualc.elementos)
                            {
                                if (tablita.nombre.Equals(nombre) && tablita.Tipo == 7)
                                {
                                    ParseTreeNode auxx = aux.ChildNodes[0];
                                    int canti = cantip(tablita);

                                    int canti2 = auxx.ChildNodes[1].ChildNodes.Count() ;
                                    if (raiz.ChildNodes.Count > 1)
                                    {
                                        canti2 = raiz.ChildNodes.ElementAt(2).ChildNodes.Count();
                                    }

                                    if (canti2 != canti)
                                    {
                                        errore += "Error al llamar a la funcion, " + nombre + "inconsistencia en los parametros, col: " + raiz.ChildNodes.ElementAt(0).Token.Location.Column + " fil: " + raiz.ChildNodes.ElementAt(0).Token.Location.Line;
                                        return "";
                                    }

                                    String ty = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += ty + "= sp +" + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "stack [" + ty + "] = 00;\n";
                                    Ejecucion3d.cadenota += ty + "=" + ty + "+1;\n";
                                    String tz = "";
                                    if (canti > 0)
                                    {
                                        foreach (ParseTreeNode lee in auxx.ChildNodes.ElementAt(1).ChildNodes)
                                        {
                                            tz = Exp(lee.ChildNodes[1]);
                                            Ejecucion3d.cadenota += "stack [" + ty + "] =" + tz + ";\n";
                                            Ejecucion3d.cadenota += ty + "=" + ty + "+1;\n";
                                        }

                                    }
                                    String reto = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += "sp = sp + " + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += tablita.etiqueta + "();\n";
                                    Ejecucion3d.cadenota += reto + "= stack[sp];\n";
                                    Ejecucion3d.cadenota += "sp = sp - " + actualM.peso + ";\n";

                                    tipoex = conver(tablita.tipo2);
                                    return reto;
                                }

                                else if (tablita.nombre.Equals(nombre) && tablita.Tipo == 5)
                                {
                                    String t2 = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += t2 + "= selfp + " + tablita.posicion + ";\n";
                                    if (tablita.arreglo == 1)
                                    {
                                        if (aux.ChildNodes.ElementAt(0).ChildNodes.Count > 1)
                                        {

                                            ParseTreeNode lvec = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);
                                            ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                                            int ii = 0;
                                            foreach (ParseTreeNode EE in lvec.ChildNodes)
                                            {
                                                E[ii] = EE.ChildNodes.ElementAt(0);
                                                ii++;
                                            }
                                            t2 = serializar(E, tablita.tamanios, t2, tablita.valores);
                                            String t5 = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += t5 + "= heap[" + t2 + "];\n";
                                            tipoex = conver(tablita.tipo2);
                                            return t5;


                                        }
                                        else
                                        {

                                            errore += "Imposible acceder a un atributo tipo arreglo sin posicion especificada," + aux.ChildNodes.ElementAt(0).Token.Text;
                                            return "";
                                        }


                                    }
                                    else
                                    {

                                        String t5 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t5 + "= heap[" + t2 + "];\n";
                                        tipoex = conver(tablita.tipo2);
                                        return t5;

                                    }


                                }


                            }




                        }

                        if (!bandera)
                        {

                            error = 1;
                            return "";

                        }


                    }
                    else
                    {
                        String t4 = "";
                        foreach (TS tablita in actualM.elementos)
                        {

                            if (tablita.Tipo == 3 && tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                            {
                                String t2 = Ejecucion3d.generatemp();
                                Ejecucion3d.cadenota += t2 + "= sp + " + tablita.posicion + ";\n";
                                String clase = tablita.etiqueta;
                                if (tablita.arreglo == 1)
                                {
                                    if (aux.ChildNodes.ElementAt(0).ChildNodes.Count > 1)
                                    {

                                        ParseTreeNode lvec = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);
                                        ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                                        int ii = 0;
                                        foreach (ParseTreeNode EE in lvec.ChildNodes)
                                        {
                                            E[ii] = EE.ChildNodes.ElementAt(0);
                                            ii++;
                                        }
                                        t2 = serializar(E, tablita.tamanios, t2, tablita.valores);


                                    }
                                    else
                                    {

                                        errore += "Imposible acceder a un atributo tipo arreglo sin posicion especificada," + aux.ChildNodes.ElementAt(0).Token.Text;
                                        return "";
                                    }


                                }

                                t4 = t2;
                                int i = 0;


                                foreach (ParseTreeNode atri in aux.ChildNodes)
                                {
                                    if (i == 0)
                                    {

                                    }
                                    else
                                    {
                                        nombre = atri.Token.Text;

                                        foreach (TS clasesita in TS.TablaSimbolos)
                                        {
                                            if (clasesita.nombre.Equals(clase))
                                            {

                                                foreach (TS tablita2 in clasesita.elementos)
                                                {
                                                    if (tablita2.nombre.Equals(nombre))
                                                    {
                                                        String t3 = Ejecucion3d.generatemp();
                                                        Ejecucion3d.cadenota += t3 + "= heap[" + t4 + "];\n";
                                                        t4 = Ejecucion3d.generatemp();
                                                        Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + tablita2.posicion + ";\n";
                                                        if (tablita2.arreglo == 1)
                                                        {
                                                            if (atri.ChildNodes.Count > 1)
                                                            {

                                                                ParseTreeNode lvec = atri.ChildNodes.ElementAt(1);
                                                                ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                                                                int ii = 0;
                                                                foreach (ParseTreeNode EE in lvec.ChildNodes)
                                                                {
                                                                    E[ii] = EE.ChildNodes.ElementAt(0);
                                                                    ii++;
                                                                }
                                                                t4 = serializar(E, tablita2.tamanios, t4, tablita2.valores);


                                                            }
                                                            else
                                                            {

                                                                errore += "Imposible acceder a un atributo tipo arreglo sin posicion especificada," + aux.ChildNodes.ElementAt(0).Token.Text;
                                                                return "";
                                                            }


                                                        }
                                                        clase = tablita2.etiqueta;



                                                    }
                                                }
                                            }
                                        }
                                    }
                                    i++;
                                }

                                String t5 = Ejecucion3d.generatemp();
                                Ejecucion3d.cadenota += t5 + "= heap[" + t4 + "];\n";
                                return t5;
                            }
                        }
                        if (!bandera)
                        {
                            foreach (TS tablita in actualc.elementos)
                            {

                                if (tablita.Tipo == 3 && tablita.nombre.Equals(nombre))
                                {
                                    String t2 = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += t2 + "= selfp + " + tablita.posicion + ";\n";
                                    if (tablita.arreglo == 1)
                                    {
                                        if (aux.ChildNodes.ElementAt(0).ChildNodes.Count > 1)
                                        {

                                            ParseTreeNode lvec = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);
                                            ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                                            int ii = 0;
                                            foreach (ParseTreeNode EE in lvec.ChildNodes)
                                            {
                                                E[ii] = EE.ChildNodes.ElementAt(0);
                                                ii++;
                                            }
                                            t2 = serializar(E, tablita.tamanios, t2, tablita.valores);


                                        }
                                        else
                                        {

                                            errore += "Imposible acceder a un atributo tipo arreglo sin posicion especificada," + aux.ChildNodes.ElementAt(0).Token.Text;
                                            return "";
                                        }


                                    }
                                    String clase = tablita.etiqueta;
                                    t4 = t2;
                                    int i = 0;


                                    foreach (ParseTreeNode atri in aux.ChildNodes)
                                    {
                                        if (i == 0)
                                        {

                                        }
                                        else
                                        {
                                            nombre = atri.Token.Text;

                                            foreach (TS clasesita in TS.TablaSimbolos)
                                            {
                                                if (clasesita.nombre.Equals(clase))
                                                {

                                                    foreach (TS tablita2 in clasesita.elementos)
                                                    {
                                                        if (tablita2.nombre.Equals(nombre))
                                                        {
                                                            String t3 = Ejecucion3d.generatemp();

                                                            Ejecucion3d.cadenota += t3 + " = heap[" + t4 + "];\n";
                                                            t4 = Ejecucion3d.generatemp();
                                                            Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + tablita2.posicion + ";\n";
                                                            if (tablita2.arreglo == 1)
                                                            {
                                                                if (atri.ChildNodes.ElementAt(0).ChildNodes.Count > 1)
                                                                {

                                                                    ParseTreeNode lvec = atri.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);
                                                                    ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                                                                    int ii = 0;
                                                                    foreach (ParseTreeNode EE in lvec.ChildNodes)
                                                                    {
                                                                        E[ii] = EE.ChildNodes.ElementAt(0);
                                                                        ii++;
                                                                    }
                                                                    t2 = serializar(E, tablita2.tamanios, t2, tablita2.valores);


                                                                }
                                                                else
                                                                {

                                                                    errore += "Imposible acceder a un atributo tipo arreglo sin posicion especificada," + aux.ChildNodes.ElementAt(0).Token.Text;
                                                                    return "";
                                                                }


                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        i++;
                                    }

                                    String t5 = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += t5 + "= heap[" + t4 + "];\n";
                                    return t5;
                                }
                            }



                        }

                        if (!bandera)
                        {

                            error = 1;
                            return "";

                        }


                    }



                }


            }
            else
            {

                String t1 = Expt(raiz.ChildNodes.ElementAt(0));


                String t3 = Ejecucion3d.generatemp();
                String tipo = raiz.ChildNodes.ElementAt(2).Token.Text;
                String t2 = "";
                int tip1 = tipoex;
                t2 = Expt(raiz.ChildNodes.ElementAt(1));
                int tip2 = tipoex;

                switch (tipo)
                {
                    case ">":
                    case "<":
                    case "<=":
                    case ">=":
                    case "==":
                    case "!=":

                        Ejecucion3d.cadenota += t3 + "= 0";
                        String eti1 = Ejecucion3d.generalabel();
                        String t4 = "if " + t1 + tipo + t2 + "then goto " + eti1 + ";\n";
                        Ejecucion3d.cadenota += t4;
                        String eti2 = Ejecucion3d.generalabel();
                        t4 = "goto " + eti2 + ";\n";
                        Ejecucion3d.cadenota += t4;
                        t4 = eti1 + ": " + t3 + "= 1" + ";\n";
                        Ejecucion3d.cadenota += t4;
                        t4 = eti2 + ": ;\n";
                        Ejecucion3d.cadenota += t4;
                        tipoex = 4;
                        break;

                    case "+":
                        if (tip1 == 2 || tip2 == 2)
                        {
                            String tx;
                            switch (tip1)
                            {
                                case 0:
                                    tx = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += tx + "= sp + " + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "stack[" + tx + "]=00;\n";
                                    Ejecucion3d.cadenota += tx + "=" + tx + "+1;\n";
                                    Ejecucion3d.cadenota += "stack[" + tx + "]=" + t1 + ";\n";
                                    Ejecucion3d.cadenota += "sp = sp +" + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "inttostr();\n";
                                    Ejecucion3d.cadenota += t1 + "=stack[sp];\n";
                                    Ejecucion3d.cadenota += "sp = sp -" + actualM.peso + ";\n";
                                    break;
                                case 1:
                                    tx = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += tx + "= sp + " + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "stack[" + tx + "]=00;\n";
                                    Ejecucion3d.cadenota += tx + "=" + tx + "+1;\n";
                                    Ejecucion3d.cadenota += "stack[" + tx + "]=" + t1 + ";\n";
                                    Ejecucion3d.cadenota += "sp = sp +" + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "doubletostr();\n";
                                    Ejecucion3d.cadenota += t1 + "=stack[sp];\n";
                                    Ejecucion3d.cadenota += "sp = sp -" + actualM.peso + ";\n";
                                    break;


                                case 2:
                                    break;
                                default:
                                    //error **************************************************************
                                    break;
                            }
                            switch (tip2)
                            {
                                case 0:
                                    tx = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += tx + "= sp + " + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "stack[" + tx + "]=00;\n";
                                    Ejecucion3d.cadenota += tx + "=" + tx + "+1;\n";
                                    Ejecucion3d.cadenota += "stack[" + tx + "]=" + t2 + ";\n";
                                    Ejecucion3d.cadenota += "sp = sp +" + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "inttostr();\n";
                                    Ejecucion3d.cadenota += t2 + "=stack[sp];\n";
                                    Ejecucion3d.cadenota += "sp = sp -" + actualM.peso + ";\n";
                                    break;
                                case 1:
                                    tx = Ejecucion3d.generatemp();
                                    Ejecucion3d.cadenota += tx + "= sp + " + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "stack[" + tx + "]=00;\n";
                                    Ejecucion3d.cadenota += tx + "=" + tx + "+1;\n";
                                    Ejecucion3d.cadenota += "stack[" + tx + "]=" + t2 + ";\n";
                                    Ejecucion3d.cadenota += "sp = sp +" + actualM.peso + ";\n";
                                    Ejecucion3d.cadenota += "doubletostr();\n";
                                    Ejecucion3d.cadenota += t2 + "=stack[sp];\n";
                                    Ejecucion3d.cadenota += "sp = sp -" + actualM.peso + ";\n";
                                    break;
                                case 2:
                                    break;
                                default:
                                    //error **************************************************************
                                    break;
                            }

                            tx = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += tx + "= sp + " + actualM.peso + ";\n";
                            Ejecucion3d.cadenota += "stack[" + tx + "]=00;\n";
                            Ejecucion3d.cadenota += tx + "=" + tx + "+1;\n";
                            Ejecucion3d.cadenota += "stack[" + tx + "]=" + t1 + ";\n";
                            Ejecucion3d.cadenota += tx + "=" + tx + "+1;\n";
                            Ejecucion3d.cadenota += "stack[" + tx + "]=" + t2 + ";\n";
                            Ejecucion3d.cadenota += "unir();\n";
                            Ejecucion3d.cadenota += t3 + "=stack[sp];\n";
                            Ejecucion3d.cadenota += "sp = sp -" + actualM.peso + ";\n";
                            tipoex = 2;

                        }
                        else if (tip1 < 2 && tip2 < 2)
                        {
                            if (tip1 == 1 || tip2 == 1)
                            {
                                tipoex = 1;

                            }
                            else { tipoex = 0; }

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "+" + t2 + ";\n";

                        }
                        else if (tip1 == 0 || tipo2 == 0)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "+" + t2 + ";\n";
                            tipoex = 0;

                        }
                        else if (tip1 == 1 || tipo2 == 1)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "+" + t2 + ";\n";
                            tipoex = 1;

                        }
                        else if (tip1 == 4 && tip2 == 4)
                        {
                            String ti = Ejecucion3d.generatemp();
                            String eti = Ejecucion3d.generalabel();
                            String sal = Ejecucion3d.generalabel();
                            Ejecucion3d.cadenota += ti + "= " + t1 + "+" + t2 + ";\n";
                            Ejecucion3d.cadenota += t3 + "= 0;\n";
                            Ejecucion3d.cadenota += "if " + ti + " == 0 then goto " + sal + "\n";
                            Ejecucion3d.cadenota += t3 + "= 1;";
                            Ejecucion3d.cadenota += sal + ":\n";
                            tipoex = 4;

                        }
                        else
                        {

                            //error************************

                        }



                        break;
                    case "-":
                        if (tip1 == 2 || tip2 == 2)
                        {
                            //*****************error

                        }
                        else if (tip1 < 2 && tip2 < 2)
                        {
                            if (tip1 == 1 || tip2 == 1)
                            {
                                tipoex = 1;

                            }
                            else { tipoex = 0; }

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "-" + t2 + ";\n";

                        }
                        else if (tip1 == 0 || tipo2 == 0)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "-" + t2 + ";\n";
                            tipoex = 0;

                        }
                        else if (tip1 == 1 || tipo2 == 1)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "-" + t2 + ";\n";
                            tipoex = 1;

                        }
                        else
                        {
                            //******************error
                        }

                        break;

                    case "*":
                        if (tip1 == 2 || tip2 == 2)
                        {
                            //*****************casteo de cadenas algo pisado de hacer

                        }
                        else if (tip1 < 2 && tip2 < 2)
                        {
                            if (tip1 == 1 || tip2 == 1)
                            {
                                tipoex = 1;

                            }
                            else { tipoex = 0; }

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "*" + t2 + ";\n";

                        }
                        else if (tip1 == 0 || tipo2 == 0)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "*" + t2 + ";\n";
                            tipoex = 0;

                        }
                        else if (tip1 == 1 || tipo2 == 1)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "*" + t2 + ";\n";
                            tipoex = 1;

                        }
                        else if (tip1 == 4 && tip2 == 4)
                        {
                            String ti = Ejecucion3d.generatemp();
                            String eti = Ejecucion3d.generalabel();
                            String sal = Ejecucion3d.generalabel();
                            Ejecucion3d.cadenota += ti + "= " + t1 + "+" + t2 + ";\n";
                            Ejecucion3d.cadenota += t3 + "= 0;\n";
                            Ejecucion3d.cadenota += "if " + ti + " <2 then goto " + sal + "\n";
                            Ejecucion3d.cadenota += t3 + "= 1;";
                            Ejecucion3d.cadenota += sal + ":\n";
                            tipoex = 4;

                        }
                        else
                        {

                            //error************************

                        }

                        break;
                    case "/":
                        if (tip1 == 2 || tip2 == 2)
                        {
                            //*****************casteo de cadenas algo pisado de hacer

                        }
                        else if (tip1 < 2 && tip2 < 2)
                        {
                            if (tip1 == 1 || tip2 == 1)
                            {
                                tipoex = 1;

                            }
                            else { tipoex = 1; }

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "/" + t2 + ";\n";

                        }
                        else if (tip1 == 0 || tipo2 == 0)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "/" + t2 + ";\n";
                            tipoex = 1;

                        }
                        else if (tip1 == 1 || tipo2 == 1)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "/" + t2 + ";\n";
                            tipoex = 1;

                        }

                        else
                        {

                            //error************************

                        }

                        break;

                    case "pow":
                        if (tip1 == 2 || tip2 == 2)
                        {
                            //*****************casteo de cadenas algo pisado de hacer

                        }
                        else if (tip1 < 2 && tip2 < 2)
                        {
                            if (tip1 == 1 || tip2 == 1)
                            {
                                tipoex = 1;

                            }
                            else { tipoex = 0; }

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "^" + t2 + ";\n";

                        }
                        else if (tip1 == 0 || tipo2 == 0)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "^" + t2 + ";\n";
                            tipoex = 0;

                        }
                        else if (tip1 == 1 || tipo2 == 1)
                        {

                            Ejecucion3d.cadenota += t3 + " = " + t1 + "^" + t2 + ";\n";
                            tipoex = 1;

                        }

                        else
                        {

                            //error************************

                        }
                        break;
                    default:

                        break;
                }



                ret = t3;

            }
            return ret;
        }

        public void imprimir(ParseTreeNode raiz)
        {

            String t1 = Exp(raiz.ChildNodes.ElementAt(0));
            String t2 = Ejecucion3d.generatemp();
            Ejecucion3d.cadenota += t2 + "= sp + " + actualM.peso + ";\n";
            Ejecucion3d.cadenota += "stack[" + t2 + "]= 00;\n";
            Ejecucion3d.cadenota += t2 + "=" + t2 + "+1;\n";
            Ejecucion3d.cadenota += "stack[" + t2 + "]=" + t1 + ";\n";
            Ejecucion3d.cadenota += "sp = sp +" + actualM.peso + ";\n";
            Ejecucion3d.cadenota += "imprimir();\n";
            Ejecucion3d.cadenota += "sp = sp -" + actualM.peso + ";\n";

        }

        public void asignar(ParseTreeNode raiz)
        {
            ParseTreeNode aux, aux2;

            aux = raiz.ChildNodes.ElementAt(0);
            aux2 = raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0);
            String nombre = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
            auxte = nombre;
            String t1 = Exp(aux2);
            bool bandera = false;

            if (aux.ChildNodes.Count == 1)
            {

                foreach (TS tablita in actualM.elementos)
                {
                    if (tablita.arreglo == 1 && (tablita.Tipo == 4 | tablita.Tipo == 2) && tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                    {
                        String t2 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t2 + "= sp + " + tablita.posicion + ";\n";


                        ParseTreeNode lvec = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);
                        ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                        int ii = 0;
                        foreach (ParseTreeNode EE in lvec.ChildNodes)
                        {
                            E[ii] = EE.ChildNodes.ElementAt(0);
                            ii++;
                        }

                        String t3 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t3 + "= stack[" + t2 + "];\n";
                        enstack = true;
                        t2 = serializar(E, tablita.tamanios, t3, tablita.valores);
                        enstack = false;
                        String t5 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += "heap[" + t2 + "]=" + t1 + ";\n";


                        bandera = true;
                        break;
                    }
                    else if ((tablita.Tipo == 4 | tablita.Tipo == 2) && tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                    {
                        String t2 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t2 + "= sp + " + tablita.posicion + ";\n";
                        Ejecucion3d.cadenota += "stack[" + t2 + "] = " + t1 + ";\n";
                        bandera = true;
                        break;
                    }
                }
                if (!bandera)
                {

                    foreach (TS tablita in actualc.elementos)
                    {

                        if (tablita.Tipo == 5 && tablita.nombre.Equals(nombre))
                        {
                            String t2 = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += t2 + "= selfp + " + tablita.posicion + ";\n";
                            Ejecucion3d.cadenota += "heap[" + t2 + "] = " + t1 + ";\n";
                            bandera = true;
                            break;
                        }


                    }




                }

                if (!bandera)
                {

                    error = 1;
                    return;

                }


            }
            else
            {
                String t4 = "";
                foreach (TS tablita in actualM.elementos)
                {

                    if (tablita.Tipo == 3 && tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                    {
                        String t2 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t2 + "= sp + " + tablita.posicion + ";\n";
                        String clase = tablita.etiqueta;
                        t4 = t2;
                        int i = 0;


                        foreach (ParseTreeNode atri in aux.ChildNodes)
                        {
                            if (i == 0)
                            {

                            }
                            else
                            {
                                nombre = atri.ChildNodes.ElementAt(0).Token.Text;

                                foreach (TS clasesita in TS.TablaSimbolos)
                                {
                                    if (clasesita.nombre.Equals(clase))
                                    {

                                        foreach (TS tablita2 in clasesita.elementos)
                                        {
                                            if (tablita2.nombre.Equals(nombre) && tablita2.Tipo == 5)
                                            {
                                                String t3 = Ejecucion3d.generatemp();
                                                Ejecucion3d.cadenota += t3 + "= stack[" + t4 + "];\n";
                                                t4 = Ejecucion3d.generatemp();
                                                Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + tablita2.posicion + ";\n";

                                                clase = tablita2.etiqueta;



                                            }
                                        }
                                    }
                                }
                            }
                            i++;
                        }

                        Ejecucion3d.cadenota += "heap[" + t4 + "]=" + t1 + ";\n";
                        bandera = true;
                        break;
                    }
                }
                if (!bandera)
                {
                    foreach (TS tablita in actualc.elementos)
                    {

                        if ((tablita.Tipo == 3|tablita.tipo2==6) && tablita.nombre.Equals(nombre))
                        {
                            String t2 = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += t2 + "= selfp + " + tablita.posicion + ";\n";

                            String clase = tablita.etiqueta;
                            if (clase.Equals(""))
                            {
                                clase = tablita.auxte;
                            }
                            t4 = t2;
                            int i = 0;


                            foreach (ParseTreeNode atri in aux.ChildNodes)
                            {
                                if (i == 0)
                                {

                                }
                                else
                                {
                                    try
                                    {
                                        nombre = atri.Token.Text;
                                    }
                                    catch
                                    {
                                        nombre = atri.ChildNodes[0].Token.Text;
                                    }

                                    foreach (TS clasesita in TS.TablaSimbolos)
                                    {
                                        if (clasesita.nombre.Equals(clase))
                                        {

                                            foreach (TS tablita2 in clasesita.elementos)
                                            {
                                                if (tablita2.nombre.Equals(nombre))
                                                {
                                                    String t3 = Ejecucion3d.generatemp();

                                                    Ejecucion3d.cadenota += t3 + " = heap[" + t4 + "];\n";
                                                    t4 = Ejecucion3d.generatemp();
                                                    Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + tablita2.posicion + ";\n";
                                                    goto salx1;
                                                }
                                                
                                            }
                                            salx1:;
                                        }
                                        goto salx2;
                                    }
                                    salx2:;
                                }
                                i++;
                            }

                            Ejecucion3d.cadenota += "heap[" + t4 + "]=" + t1 + ";\n";
                            bandera = true;
                            break;
                        }
                    }



                }

                if (!bandera)
                {

                    error = 1;
                    return;

                }


            }

        }

        public void asignart(ParseTreeNode raiz)
        {
            ParseTreeNode aux, aux2;

            aux = raiz.ChildNodes.ElementAt(0);
            aux2 = raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0);
            String nombre = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
            String t1 = Expt(aux2);
            bool bandera = false;

            if (aux.ChildNodes.Count == 1)
            {

                foreach (TS tablita in actualM.elementos)
                {

                    if ((tablita.Tipo == 4 | tablita.Tipo == 2) && tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                    {
                        String t2 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t2 + "= sp + " + tablita.posicion + ";\n";
                        Ejecucion3d.cadenota += "stack[" + t2 + "] = " + t1 + ";\n";
                        bandera = true;
                        break;
                    }
                }
                if (!bandera)
                {

                    foreach (TS tablita in actualc.elementos)
                    {

                        if (tablita.Tipo == 5 && tablita.nombre.Equals(nombre))
                        {
                            String t2 = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += t2 + "= selfp + " + tablita.posicion + ";\n";
                            Ejecucion3d.cadenota += "heap[" + t2 + "] = " + t1 + ";\n";
                            bandera = true;
                            break;
                        }


                    }




                }

                if (!bandera)
                {

                    error = 1;
                    return;

                }


            }
            else
            {
                String t4 = "";
                foreach (TS tablita in actualM.elementos)
                {

                    if (tablita.Tipo == 3 && tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                    {
                        String t2 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t2 + "= sp + " + tablita.posicion + ";\n";
                        String clase = tablita.etiqueta;
                        t4 = t2;
                        int i = 0;


                        foreach (ParseTreeNode atri in aux.ChildNodes)
                        {
                            if (i == 0)
                            {

                            }
                            else
                            {
                                nombre = atri.ChildNodes.ElementAt(0).Token.Text;

                                foreach (TS clasesita in TS.TablaSimbolos)
                                {
                                    if (clasesita.nombre.Equals(clase))
                                    {

                                        foreach (TS tablita2 in clasesita.elementos)
                                        {
                                            if (tablita2.nombre.Equals(nombre) && tablita.Tipo == 5)
                                            {
                                                String t3 = Ejecucion3d.generatemp();
                                                Ejecucion3d.cadenota += t3 + "= heap[" + t4 + "];\n";
                                                t4 = Ejecucion3d.generatemp();
                                                Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + tablita2.posicion + ";\n";

                                                clase = tablita2.etiqueta;



                                            }
                                        }
                                    }
                                }
                            }
                            i++;
                        }

                        Ejecucion3d.cadenota += "heap[" + t4 + "]=" + t1 + ";\n";
                        bandera = true;
                        break;
                    }
                }
                if (!bandera)
                {
                    foreach (TS tablita in actualc.elementos)
                    {

                        if (tablita.Tipo == 3 && tablita.nombre.Equals(nombre))
                        {
                            String t2 = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += t2 + "= selfp + " + tablita.posicion + ";\n";

                            String clase = tablita.etiqueta;
                            t4 = t2;
                            int i = 0;


                            foreach (ParseTreeNode atri in aux.ChildNodes)
                            {
                                if (i == 0)
                                {

                                }
                                else
                                {
                                    nombre = atri.Token.Text;

                                    foreach (TS clasesita in TS.TablaSimbolos)
                                    {
                                        if (clasesita.nombre.Equals(clase))
                                        {

                                            foreach (TS tablita2 in clasesita.elementos)
                                            {
                                                if (tablita2.nombre.Equals(nombre))
                                                {
                                                    String t3 = Ejecucion3d.generatemp();

                                                    Ejecucion3d.cadenota += t3 + " = heap[" + t4 + "];\n";
                                                    t4 = Ejecucion3d.generatemp();
                                                    Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + tablita2.posicion + ";\n";

                                                }
                                            }
                                        }
                                    }
                                }
                                i++;
                            }

                            Ejecucion3d.cadenota += "heap[" + t4 + "]=" + t1 + ";\n";
                            bandera = true;
                            break;
                        }
                    }



                }

                if (!bandera)
                {

                    error = 1;
                    return;

                }


            }

        }

        public void declararv(ParseTreeNode raiz)
        {

            int visibilidad = 1;
            ParseTreeNode aux;
            ParseTreeNode aux2;
            ParseTreeNode aux3;

            aux = raiz.ChildNodes.ElementAt(0);
            aux2 = raiz.ChildNodes.ElementAt(1);
            aux3 = raiz.ChildNodes.ElementAt(2);

            String tipov = aux.Token.Text;
            int tipov2;

            switch (tipov)
            {
                case "entero":
                    tipov2 = 1;
                    break;
                case "cadena":
                    tipov2 = 2;
                    break;
                case "booleano":
                    tipov2 = 3;
                    break;
                case "decimal":
                    tipov2 = 4;
                    break;
                default:
                    tipov2 = 6;
                    break;
            }


            if (aux2.ChildNodes.Count > 1)
            {

                aux3 = aux2.ChildNodes.ElementAt(1);
                int dimen = aux3.ChildNodes.Count;
                String val1;
                int val2;
                int[] dimensi = new int[dimen];
                int i = 0;
                int peso = 1;
                foreach (ParseTreeNode hijo in aux3.ChildNodes)
                {


                    val2 = resolverolca(hijo.ChildNodes.ElementAt(0));
                    peso *= val2;
                    dimensi[i] = val2;
                    i++;
                }



                TS nuevo2 = new TS(aux2.ChildNodes.ElementAt(0).Token.Text, 4, actualM.peso, tipov2, visibilidad, 1, "", 1, dimen, dimensi);
                String t1 = Ejecucion3d.generatemp();
                String t3;
                if (raiz.ChildNodes.ElementAt(2).ChildNodes.Count > 0)
                {
                    t3 = Exp(raiz.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));
                } else
                {
                    t3 = Ejecucion3d.generatemp();
                    Ejecucion3d.cadenota += t3 + "=hp;\n";
                    for (int ij = 0; ij < peso; ij++)
                    {
                        Ejecucion3d.cadenota += "heap[hp]=00;\n";
                        Ejecucion3d.cadenota += "hp= hp+1;\n";
                    }

                }

                Ejecucion3d.cadenota += t1 + "= sp +" + nuevo2.posicion + ";\n";
                Ejecucion3d.cadenota += "stack[" + t1 + "] = " + t3 + ";\n";
                actualM.peso++;
                actualM.elementos.Add(nuevo2);

                auxte = t1;


                return;

            }
            else if (aux3.ChildNodes.Count > 0 && aux3.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Term.Name.Equals("INSTANCIA"))
            {

                if (tipov2 != 6)
                {
                    errore += "Problema en declarar primitivo como instancia, col: " + aux3.Token.Location.Column + ",Fil :" + aux3.Token.Location.Line + "\n";
                    return;

                }
                String auxin = aux3.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).Token.Text;


                if (!tipov.Equals(auxin))
                {

                    errore += "Problema en  instancia, col: " + aux3.Token.Location.Column + ",Fil :" + aux3.Token.Location.Line + "\n";
                    return;
                }
                bool bandera = true;
                foreach (String clase in actualc.importadas)
                {

                    if (clase.Equals(tipov))
                    {
                        bandera = false;


                    }


                }

                ParseTreeNode aux5 = aux3.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);

                if (bandera)
                {
                    errore += "Objeto inaxesible desde la clase actual col: " + aux5.Token.Location.Column + ",Fil :" + aux5.Token.Location.Line + "\n";
                    return;
                }


                ParseTreeNode insta = aux3.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);

                int canti = 0;
                int canti2 = 0;
                ParseTreeNode para;
                if (insta.ChildNodes.Count > 2)
                {
                    para = insta.ChildNodes.ElementAt(2);
                    canti = para.ChildNodes.Count;
                }

                foreach (TS clase in TablaSimbolos)
                {
                    if (clase.nombre.Equals(tipov))
                    {

                        foreach (TS constru in clase.elementos)
                        {
                            String[] arregla = Regex.Split(constru.nombre, "init_");
                            char sig = '1';
                            if (arregla.Count() > 1)
                            {
                                String auxs = arregla[1];
                                sig = auxs[0];
                            }




                            if (constru.Tipo == 8 && sig.Equals('2'))
                            {

                                canti2 = cantip(constru);

                                if (canti2 == canti)
                                {

                                    if (canti2 == 0)
                                    {
                                        String t1 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t1 + "= sp + " + actualM.peso + ";\n";
                                        Ejecucion3d.cadenota += "stack[" + t1 + "] = 00;\n";
                                        Ejecucion3d.cadenota += "sp = sp + " + actualM.peso + ";\n";
                                        Ejecucion3d.cadenota += "init_" + tipov + "1();\n";
                                        t1 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t1 + "=stack[sp];\n";
                                        Ejecucion3d.cadenota += "sp = sp - " + actualM.peso + ";\n";
                                        String nombre = raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                                        TS nuevo = new TS(nombre, 3, actualM.peso, 6, 0, 1, tipov, 0, 0, null);
                                        actualM.elementos.Add(nuevo);
                                        actualM.peso++;
                                        String t2 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t2 + "= sp + " + actualM.peso + ";\n";
                                        Ejecucion3d.cadenota += "stack[" + t2 + "] = 00;\n";
                                        Ejecucion3d.cadenota += t2 + "=" + t2 + " + 1 ;\n";
                                        Ejecucion3d.cadenota += "stack[" + t2 + "] = " + t1 + ";\n";
                                        Ejecucion3d.cadenota += "sp = sp + " + actualM.peso + ";\n";
                                        String tsp = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += tsp + "= selfp;\n";
                                        Ejecucion3d.cadenota += "selfp = " + t1 + ";\n";
                                        Ejecucion3d.cadenota += constru.etiqueta + "();\n";
                                        Ejecucion3d.cadenota += "sp = sp - " + actualM.peso + ";\n";
                                        Ejecucion3d.cadenota += "selfp = " + tsp + ";\n";
                                        return;
                                    }
                                    else
                                    {


                                        String t1 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t1 + "= sp + " + actualM.peso + ";\n";
                                        Ejecucion3d.cadenota += "stack[" + t1 + "] = 00;\n";
                                        Ejecucion3d.cadenota += "sp = sp + " + actualM.peso + ";\n";
                                        Ejecucion3d.cadenota += "init_" + tipov + "1();\n";
                                        t1 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t1 + "=stack[sp];\n";
                                        Ejecucion3d.cadenota += "sp = sp - " + actualM.peso + ";\n";
                                        String nombre = raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                                        TS nuevo = new TS(nombre, 3, actualM.peso, 6, 0, 1, tipov, 0, 0, null);
                                        actualM.elementos.Add(nuevo);
                                        actualM.peso++;
                                        String t2 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t2 + "= sp + " + actualM.peso + ";\n";
                                        Ejecucion3d.cadenota += "stack[" + t2 + "] = 00;\n";
                                        Ejecucion3d.cadenota += t2 + "=" + t2 + " + 1 ;\n";
                                        Ejecucion3d.cadenota += "stack[" + t2 + "] = " + t1 + ";\n";

                                        ParseTreeNode parafin = insta.ChildNodes.ElementAt(2);
                                        foreach (ParseTreeNode parax in parafin.ChildNodes)
                                        {

                                            String ex = Exp(parax);
                                            Ejecucion3d.cadenota += t2 + "=" + t2 + " + 1 ;\n";
                                            Ejecucion3d.cadenota += "stack[" + t2 + "] = " + ex + ";\n";

                                        }
                                        Ejecucion3d.cadenota += "sp = sp + " + actualM.peso + ";\n";
                                        String tsp = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += tsp + "= selfp;\n";
                                        Ejecucion3d.cadenota += "selfp = " + t1 + ";\n";
                                        Ejecucion3d.cadenota += constru.etiqueta + "();\n";
                                        Ejecucion3d.cadenota += "sp = sp - " + actualM.peso + ";\n";
                                        Ejecucion3d.cadenota += "selfp = " + tsp + ";\n";
                                        return;

                                    }


                                }
                            }

                        }

                    }
                }



            }
            else
            {

                String t3 = "00";
                if (aux3.ChildNodes.Count > 0)
                {

                    t3 = Exp(aux3.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));
                }
                aux = aux2.ChildNodes.ElementAt(0);

                String t1 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t1 + " = sp +" + actualM.peso + ";\n";

                foreach (ParseTreeNode hijo in aux.ChildNodes)
                {
                    Ejecucion3d.cadenota += "stack[" + t1 + "] =" + t3 + ";\n";
                    TS nuevo = new TS(hijo.Token.Text, 4, actualM.peso, tipov2, visibilidad, 1, "", 0, 0, null);
                    actualM.peso++;
                    actualM.elementos.Add(nuevo);
                    Ejecucion3d.cadenota += t1 + "=" + t1 + "+ 1;\n";
                }

            }



        }

        public void declararvt(ParseTreeNode raiz)
        {

            int visibilidad = 1;
            ParseTreeNode aux;
            ParseTreeNode aux2;
            ParseTreeNode aux3;

            aux = raiz.ChildNodes.ElementAt(0);
            aux2 = raiz.ChildNodes.ElementAt(1);
            aux3 = raiz.ChildNodes.ElementAt(2);

            String tipov = aux.Token.Text;
            int tipov2;

            switch (tipov)
            {
                case "entero":
                    tipov2 = 1;
                    break;
                case "cadena":
                    tipov2 = 2;
                    break;
                case "booleano":
                    tipov2 = 3;
                    break;
                case "decimal":
                    tipov2 = 4;
                    break;
                default:
                    tipov2 = 6;
                    break;
            }


            if (aux2.ChildNodes.Count > 1)
            {

                aux3 = aux2.ChildNodes.ElementAt(1);
                int dimen = aux3.ChildNodes.Count;
                String val1;
                int val2;
                int[] dimensi = new int[dimen];
                int i = 0;
                int peso = 1;
                foreach (ParseTreeNode hijo in aux3.ChildNodes)
                {

                    val1 = hijo.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                    val2 = Convert.ToInt32(val1);
                    peso *= val2;
                    dimensi[i] = val2;
                    i++;
                }



                TS nuevo2 = new TS(aux2.Token.Text, 5, actualM.peso, tipov2, visibilidad, 1, "", 1, dimen, dimensi);
                String t1 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t1 + "= sp +" + nuevo2.posicion + ";\n";
                Ejecucion3d.cadenota += "stack[" + t1 + "] = 00;\n";
                actualM.peso++;
                actualM.elementos.Add(nuevo2);

                for (int k = 0; k < peso; k++)
                {
                    TS nuevo22 = new TS(aux2.Token.Text + k, 5, actualM.peso, tipov2, visibilidad, 1, "", 0, 0, null);

                    actualM.peso++;
                    actualM.elementos.Add(nuevo2);

                }
                auxte = t1;
                Exp(raiz.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));

                return;

            }
            else if (aux3.ChildNodes.Count > 0 && aux3.ChildNodes.ElementAt(0).Term.Name.Equals("INSTANCIA"))
            {

                if (tipov2 != 6)
                {
                    errore += "Problema en declarar primitivo como instancia, col: " + aux3.Token.Location.Column + ",Fil :" + aux3.Token.Location.Line + "\n";
                    return;

                }
                String auxin = aux3.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;

                if (!tipov.Equals(auxin))
                {

                    errore += "Problema en  instancia, col: " + aux3.Token.Location.Column + ",Fil :" + aux3.Token.Location.Line + "\n";
                    return;
                }
                bool bandera = true;
                foreach (String clase in actualc.importadas)
                {

                    if (clase.Equals(tipov))
                    {
                        bandera = false;


                    }


                }
                ParseTreeNode aux5 = aux3.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0);

                if (bandera)
                {
                    errore += "Objeto inaxesible desde la clase actual col: " + aux5.Token.Location.Column + ",Fil :" + aux5.Token.Location.Line + "\n";
                    return;
                }


                ParseTreeNode insta = aux3.ChildNodes.ElementAt(0);

                int canti = 0;
                int canti2 = 0;
                ParseTreeNode para;
                if (insta.ChildNodes.Count > 1)
                {
                    para = insta.ChildNodes.ElementAt(1);
                    canti = para.ChildNodes.Count;
                }

                foreach (TS clase in TablaSimbolos)
                {
                    if (clase.nombre.Equals(tipov))
                    {

                        foreach (TS constru in clase.elementos)
                        {
                            if (constru.Tipo == 8)
                            {

                                canti2 = cantip(constru);

                                if (canti2 == canti)
                                {

                                    if (canti2 == 0)
                                    {
                                        String t1 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t1 + "= sp + " + actualM.peso + ";\n";
                                        Ejecucion3d.cadenota += "stack[" + t1 + "] = 00;\n";
                                        Ejecucion3d.cadenota += "sp = sp + " + actualM.peso + ";\n";
                                        Ejecucion3d.cadenota += "init_" + tipov + "1();\n";
                                        t1 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t1 + "=stack[sp];\n";
                                        Ejecucion3d.cadenota += "sp = sp - " + actualM.peso + ";\n";
                                        String nombre = raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                                        TS nuevo = new TS(nombre, 3, actualM.peso, 6, 0, 1, tipov, 0, 0, null);
                                        actualM.elementos.Add(nuevo);
                                        actualM.peso++;
                                        String t2 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t2 + "= sp + " + actualM.peso + ";\n";
                                        Ejecucion3d.cadenota += "stack[" + t2 + "] = 00;\n";
                                        Ejecucion3d.cadenota += t2 + "=" + t2 + " + 1 ;\n";
                                        Ejecucion3d.cadenota += "stack[" + t2 + "] = " + t1 + ";\n";
                                        Ejecucion3d.cadenota += "sp = sp + " + actualM.peso + ";\n";
                                        String tsp = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += tsp + "= selfp;\n";
                                        Ejecucion3d.cadenota += "selfp = " + t1 + ";\n";
                                        Ejecucion3d.cadenota += constru.etiqueta + "();\n";
                                        Ejecucion3d.cadenota += "sp = sp - " + actualM.peso + ";\n";
                                        Ejecucion3d.cadenota += "selfp = " + tsp + ";\n";
                                        return;
                                    }
                                    else
                                    {


                                        String t1 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t1 + "= sp + " + actualM.peso + ";\n";
                                        Ejecucion3d.cadenota += "stack[" + t1 + "] = 00;\n";
                                        Ejecucion3d.cadenota += "sp = sp + " + actualM.peso + ";\n";
                                        Ejecucion3d.cadenota += "init_" + tipov + "1();\n";
                                        Ejecucion3d.cadenota += "sp = sp - " + actualM.peso + ";\n";
                                        t1 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t1 + "=stack[sp];\n";
                                        Ejecucion3d.cadenota += "sp = sp - " + actualM.peso + ";\n";
                                        String nombre = raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                                        TS nuevo = new TS(nombre, 3, actualM.peso, 6, 0, 1, tipov, 0, 0, null);
                                        actualM.elementos.Add(nuevo);
                                        String t2 = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += t2 + "= sp + " + actualM.peso + ";\n";
                                        Ejecucion3d.cadenota += "stack[" + t2 + "] = 00;\n";
                                        Ejecucion3d.cadenota += t2 + "=" + t2 + " + 1 ;\n";
                                        Ejecucion3d.cadenota += "stack[" + t2 + "] = " + t1 + ";\n";

                                        ParseTreeNode parafin = insta.ChildNodes.ElementAt(1);
                                        foreach (ParseTreeNode parax in parafin.ChildNodes)
                                        {

                                            String ex = Exp(parax);
                                            Ejecucion3d.cadenota += t2 + "=" + t2 + " + 1 ;\n";
                                            Ejecucion3d.cadenota += "stack[" + t2 + "] = " + ex + ";\n";

                                        }
                                        Ejecucion3d.cadenota += "sp = sp + " + actualM.peso;
                                        String tsp = Ejecucion3d.generatemp();
                                        Ejecucion3d.cadenota += tsp + "= selfp;\n";
                                        Ejecucion3d.cadenota += "selfp = " + t1 + ";\n";
                                        Ejecucion3d.cadenota += constru.etiqueta + "();\n";
                                        Ejecucion3d.cadenota += "sp = sp - " + actualM.peso;
                                        Ejecucion3d.cadenota += "selfp = " + tsp + ";\n";
                                        return;

                                    }


                                }
                            }

                        }

                    }
                }



            }
            else
            {


                String t3 = "00";
                if (aux3.ChildNodes.Count > 0)
                {

                    t3 = Expt(aux3.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0));
                }
                aux = aux2.ChildNodes.ElementAt(0);

                String t1 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t1 + " = sp +" + actualM.peso + ";\n";

                foreach (ParseTreeNode hijo in aux.ChildNodes)
                {
                    Ejecucion3d.cadenota += "stack[" + t1 + "] =" + t3 + ";\n";
                    TS nuevo = new TS(hijo.Token.Text, 4, actualM.peso, tipov2, visibilidad, 1, "", 0, 0, null);
                    actualM.peso++;
                    actualM.elementos.Add(nuevo);
                    Ejecucion3d.cadenota += t1 + "=" + t1 + "+ 1;\n";
                }

            }



        }

        public void self(ParseTreeNode raiz)
        {
            ParseTreeNode lvec;
            String t4 = "";
            String t1 = Exp(raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0));
            ParseTreeNode aux = raiz.ChildNodes.ElementAt(0);
            String nombre = raiz.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
            foreach (TS tablita in actualc.elementos)
            {

                if (tablita.nombre.Equals(nombre))
                {


                    String t2 = Ejecucion3d.generatemp();

                    Ejecucion3d.cadenota += t2 + "= selfp + " + tablita.posicion + ";\n";

                    String clase = tablita.etiqueta;
                    int i = 0;

                    if (tablita.arreglo == 1)
                    {
                        if (aux.ChildNodes.ElementAt(0).ChildNodes.Count > 1)
                        {

                            lvec = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);
                            ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                            int ii = 0;
                            foreach (ParseTreeNode EE in lvec.ChildNodes)
                            {
                                E[ii] = EE.ChildNodes.ElementAt(0);
                                ii++;
                            }
                            t2 = serializar(E, tablita.tamanios, t2, tablita.valores);


                        }
                        else
                        {

                            errore += "Imposible acceder a un atributo tipo arreglo sin posicion especificada," + aux.ChildNodes.ElementAt(0).Token.Text;
                            return;
                        }


                    }
                    t4 = t2;

                    foreach (ParseTreeNode atri in aux.ChildNodes)
                    {
                        if (i == 0)
                        {

                        }
                        else
                        {
                            nombre = atri.ChildNodes.ElementAt(0).Token.Text;

                            foreach (TS clasesita in TS.TablaSimbolos)
                            {
                                if (clasesita.nombre.Equals(clase))
                                {

                                    foreach (TS tablita2 in clasesita.elementos)
                                    {
                                        if (tablita2.nombre.Equals(nombre))
                                        {
                                            String t3 = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += t3 + "= heap[" + t4 + "];\n";
                                            t4 = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + tablita2.posicion + ";\n";
                                            if (tablita2.arreglo == 1)
                                            {
                                                if (aux.ChildNodes.Count > 1)
                                                {

                                                    lvec = atri.ChildNodes.ElementAt(1);
                                                    ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                                                    int ii = 0;
                                                    foreach (ParseTreeNode EE in lvec.ChildNodes)
                                                    {
                                                        E[ii] = EE.ChildNodes.ElementAt(0);
                                                        ii++;
                                                    }
                                                    t4 = serializar(E, tablita2.tamanios, t4, tablita2.valores);


                                                }
                                                else
                                                {

                                                    errore += "Imposible acceder a un atributo tipo arreglo sin posicion especificada," + aux.ChildNodes.ElementAt(0).Token.Text;
                                                    return;
                                                }


                                            }

                                            clase = tablita2.etiqueta;
                                        }
                                    }
                                }
                            }
                        }
                        i++;
                    }

                    Ejecucion3d.cadenota += "heap[" + t4 + "]=" + t1 + ";\n";

                    break;
                }
            }



        }

        public void selft(ParseTreeNode raiz)
        {
            ParseTreeNode lvec;
            String t4 = "";
            String t1 = Expt(raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0));
            ParseTreeNode aux = raiz.ChildNodes.ElementAt(0);
            String nombre = raiz.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
            foreach (TS tablita in actualc.elementos)
            {

                if (tablita.nombre.Equals(nombre))
                {


                    String t2 = Ejecucion3d.generatemp();

                    Ejecucion3d.cadenota += t2 + "= selfp + " + tablita.posicion + ";\n";

                    String clase = tablita.etiqueta;
                    int i = 0;

                    if (tablita.arreglo == 1)
                    {
                        if (aux.ChildNodes.ElementAt(0).ChildNodes.Count > 1)
                        {

                            lvec = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1);
                            ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                            int ii = 0;
                            foreach (ParseTreeNode EE in lvec.ChildNodes)
                            {
                                E[ii] = EE.ChildNodes.ElementAt(1);
                                ii++;
                            }
                            t2 = serializart(E, tablita.tamanios, t2, tablita.valores);


                        }
                        else
                        {

                            errore += "Imposible acceder a un atributo tipo arreglo sin posicion especificada," + aux.ChildNodes.ElementAt(0).Token.Text;
                            return;
                        }


                    }
                    t4 = t2;

                    foreach (ParseTreeNode atri in aux.ChildNodes)
                    {
                        if (i == 0)
                        {

                        }
                        else
                        {
                            nombre = atri.ChildNodes.ElementAt(0).Token.Text;

                            foreach (TS clasesita in TS.TablaSimbolos)
                            {
                                if (clasesita.nombre.Equals(clase))
                                {

                                    foreach (TS tablita2 in clasesita.elementos)
                                    {
                                        if (tablita2.nombre.Equals(nombre))
                                        {
                                            String t3 = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += t3 + "= heap[" + t4 + "];\n";
                                            t4 = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + tablita2.posicion + ";\n";
                                            if (tablita2.arreglo == 1)
                                            {
                                                if (aux.ChildNodes.Count > 1)
                                                {

                                                    lvec = atri.ChildNodes.ElementAt(1);
                                                    ParseTreeNode[] E = new ParseTreeNode[lvec.ChildNodes.Count];
                                                    int ii = 0;
                                                    foreach (ParseTreeNode EE in lvec.ChildNodes)
                                                    {
                                                        E[ii] = EE.ChildNodes.ElementAt(1);
                                                        ii++;
                                                    }
                                                    t4 = serializart(E, tablita2.tamanios, t4, tablita2.valores);


                                                }
                                                else
                                                {

                                                    errore += "Imposible acceder a un atributo tipo arreglo sin posicion especificada," + aux.ChildNodes.ElementAt(0).Token.Text;
                                                    return;
                                                }


                                            }

                                            clase = tablita2.etiqueta;
                                        }
                                    }
                                }
                            }
                        }
                        i++;
                    }

                    Ejecucion3d.cadenota += "heap[" + t4 + "]=" + t1 + ";\n";

                    break;
                }
            }



        }

        public ArrayList vala(ParseTreeNode raiz)
        {

            ArrayList retorno = new ArrayList();
            ArrayList aux;
            if (raiz.ChildNodes.Count > 1)
            {

                foreach (ParseTreeNode hijo in raiz.ChildNodes)
                {
                    aux = vala(hijo);

                    foreach (ParseTreeNode hijo2 in aux)
                    {
                        retorno.Add(hijo2);
                    }
                }


            }
            else
            {
                if (raiz.ChildNodes.ElementAt(0).Term.Name.Equals("LE"))
                {

                    foreach (ParseTreeNode hijo in raiz.ChildNodes.ElementAt(0).ChildNodes)
                    {

                        retorno.Add(hijo);

                    }

                }
                else
                {

                    retorno = vala(raiz.ChildNodes.ElementAt(0));

                }
            }

            return retorno;
        }

        public String serializar(ParseTreeNode[] E, int[] dimensiones, String tx, int[] valores)
        {
            String ret = Ejecucion3d.generatemp();
            String t3 = "";
            if (!enstack)
            {
                Ejecucion3d.cadenota += ret + "= heap[" + tx + "];\n";
            } else
            {
                ret = tx;
            }
            if (E.Count() == 1)
            {
                String t1 = Exp(E[0]);
                String t2 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t2 + "=" + ret + "+" + t1 + ";\n";

                return t2;

            } else
            {
                int acu = 1;
                String t1;
                String t2;

                String t4;
                for (int i = 0; i < dimensiones.Count(); i++)
                {
                    if (i == 0)
                    {
                        t1 = Exp(E[0]);

                        t2 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t2 + "=" + t1 + "*" + dimensiones[1] + ";\n";
                        t3 = Ejecucion3d.generatemp();
                        t4 = Exp(E[1]);
                        Ejecucion3d.cadenota += t3 + "=" + t2 + "+" + t4 + ";\n";
                        acu = dimensiones[0] * dimensiones[1];
                        i++;
                    } else
                    {
                        t1 = Exp(E[i]);

                        t2 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t2 + "=" + t1 + "*" + acu + ";\n";
                        t4 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + t2 + ";\n";
                        t3 = t4;
                        acu *= dimensiones[i];
                    }


                }


            }
            String tx1 = Ejecucion3d.generatemp();
            Ejecucion3d.cadenota += tx1 + "=" + ret + "+" + t3 + ";\n";
            return tx1;
        }

        public String serializart(ParseTreeNode[] E, int[] dimensiones, String tx, int[] valores)
        {
            String ret = Ejecucion3d.generatemp();
            String t3 = "";
            if (!enstack)
            {
                Ejecucion3d.cadenota += ret + "= heap[" + tx + "];\n";
            }
            else
            {
                ret = tx;
            }
            if (E.Count() == 1)
            {
                String t1 = Expt(E[0]);
                String t2 = Ejecucion3d.generatemp();
                Ejecucion3d.cadenota += t2 + "=" + ret + "+" + t1 + ";\n";

                return t2;

            }
            else
            {
                int acu = 1;
                String t1;
                String t2;

                String t4;
                for (int i = 0; i < dimensiones.Count(); i++)
                {
                    if (i == 0)
                    {
                        t1 = Expt(E[0]);

                        t2 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t2 + "=" + t1 + "*" + dimensiones[1] + ";\n";
                        t3 = Ejecucion3d.generatemp();
                        t4 = Expt(E[1]);
                        Ejecucion3d.cadenota += t3 + "=" + t2 + "+" + t4 + ";\n";
                        acu = dimensiones[0] * dimensiones[1];
                        i++;
                    }
                    else
                    {
                        t1 = Expt(E[i]);

                        t2 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t2 + "=" + t1 + "*" + acu + ";\n";
                        t4 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + t2 + ";\n";
                        t3 = t4;
                        acu *= dimensiones[i];
                    }


                }


            }
            String tx1 = Ejecucion3d.generatemp();
            Ejecucion3d.cadenota += tx1 + "=" + ret + "+" + t3 + ";\n";
            return tx1;
        }

        public ArrayList cond(ParseTreeNode raiz)
        {

            ArrayList ret = new ArrayList();


            if (raiz.ChildNodes.Count == 1)
            {

                String t1 = Exp(raiz);
                String eti1 = Ejecucion3d.generalabel();
                String eti2 = Ejecucion3d.generalabel();
                Ejecucion3d.cadenota += "if " + t1 + "= 1  goto" + eti1 + "\n";
                Ejecucion3d.cadenota += "goto " + eti2 + "\n";

            }
            else
            {
                ArrayList t1;
                ArrayList t2;
                String tipo = "";
                if (raiz.ChildNodes.ElementAt(1).Term.Name.Equals("OPEREL"))
                {
                    tipo = raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text;
                } else
                {
                    tipo = raiz.ChildNodes.ElementAt(1).Token.Text;
                }
                switch (tipo)
                {
                    case ">":
                    case "<":
                    case "<=":
                    case ">=":
                    case "!=":
                        String t11 = Exp(raiz.ChildNodes.ElementAt(0));
                        String t22 = Exp(raiz.ChildNodes.ElementAt(2));
                        String eti1 = Ejecucion3d.generalabel();
                        String eti2 = Ejecucion3d.generalabel();
                        if (!biffalse)
                        {
                            String t4 = "if " + t11 + tipo + t22 + " goto " + eti1 + ";\n";
                            Ejecucion3d.cadenota += t4;

                            t4 = "goto " + eti2 + ";\n";
                            Ejecucion3d.cadenota += t4;
                            ret.Add(eti1);
                            ret.Add(eti2);
                        }
                        else
                        {
                            tipo = contrario(tipo);
                            String t4 = "iffalse " + t11 + tipo + t22 + " goto " + eti2 + ";\n";
                            Ejecucion3d.cadenota += t4;

                            t4 = "goto " + eti1 + ";\n";
                            Ejecucion3d.cadenota += t4;
                            ret.Add(eti2);
                            ret.Add(eti1);
                        }


                        return ret;
                    case "==":
                        String t111 = Exp(raiz.ChildNodes.ElementAt(0));
                        String t221 = Exp(raiz.ChildNodes.ElementAt(2));
                        String eti11 = Ejecucion3d.generalabel();
                        String eti21 = Ejecucion3d.generalabel();
                        if (!biffalse)
                        {
                            String t4 = "if " + t111 + "=" + t221 + " goto " + eti11 + ";\n";
                            Ejecucion3d.cadenota += t4;

                            t4 = "goto " + eti21 + ";\n";
                            Ejecucion3d.cadenota += t4;
                            ret.Add(eti11);
                            ret.Add(eti21);

                        }
                        else
                        {
                            String t4 = "iffalse " + t111 + "!=" + t221 + " goto " + eti21 + ";\n";
                            Ejecucion3d.cadenota += t4;
                            t4 = "goto " + eti11 + ";\n";
                            Ejecucion3d.cadenota += t4;
                            ret.Add(eti21);
                            ret.Add(eti11);
                        }


                        return ret;
                    case "and":
                        biffalse = true;
                        t1 = cond(raiz.ChildNodes.ElementAt(0));
                        Ejecucion3d.cadenota += t1[0].ToString() + ":\n";
                        t2 = cond(raiz.ChildNodes.ElementAt(2));
                        String lf = t1[1].ToString() + "," + t2[1].ToString();
                        ret.Add(t2[0].ToString());
                        String[] lista1 = t2[0].ToString().Split(',');
                        Ejecucion3d.cadenota += " goto " + lista1[0] + ";\n";
                        ret.Add(lf);
                        return ret;
                    case "or":
                        biffalse = false;
                        t1 = cond(raiz.ChildNodes.ElementAt(0));
                        Ejecucion3d.cadenota += t1[1].ToString() + ":\n";
                        t2 = cond(raiz.ChildNodes.ElementAt(2));
                        String lv = t1[0].ToString() + "," + t2[0].ToString();
                        ret.Add(lv);
                        ret.Add(t2[1].ToString());
                        return ret;
                    case "xor":
                        biffalse = false;
                        String txor = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += txor + "= 0;\n" ;
                        t1 = cond(raiz.ChildNodes.ElementAt(0));
                        String Lxor1 = Ejecucion3d.generalabel();
                        Ejecucion3d.cadenota += t1[0].ToString()+":\n";
                        Ejecucion3d.cadenota += txor + "=" + txor + "+ 1;\n";
                        Ejecucion3d.cadenota += "goto "+Lxor1+";\n";
                        Ejecucion3d.cadenota += t1[1].ToString() + ":\n";
                        Ejecucion3d.cadenota += txor + "=" + txor + "- 1;\n";
                        Ejecucion3d.cadenota += Lxor1 + ":\n";
                        t2 = cond(raiz.ChildNodes.ElementAt(2));
                        Lxor1 = Ejecucion3d.generalabel();
                        Ejecucion3d.cadenota += t2[0].ToString() + ":\n";
                        Ejecucion3d.cadenota += txor + "=" + txor + "+ 1;\n";
                        Ejecucion3d.cadenota += "goto " + Lxor1 + ";\n";
                        Ejecucion3d.cadenota += t2[1].ToString() + ":\n";
                        Ejecucion3d.cadenota += txor + "=" + txor + "- 1;\n";
                        Ejecucion3d.cadenota += Lxor1 + ":\n";
                        Lxor1 = Ejecucion3d.generalabel();
                        String Lxor2 = Ejecucion3d.generalabel();
                        Ejecucion3d.cadenota += "if "+txor +"= 0 goto "+Lxor1+";\n";
                        Ejecucion3d.cadenota += "goto " + Lxor2 + ";\n";
                        ret.Add(Lxor1);
                        ret.Add(Lxor2);
                        return ret;


                }



            }



            return null;
        }

        public ArrayList condt(ParseTreeNode raiz)
        {

            ArrayList ret = new ArrayList();


            if (raiz.ChildNodes.Count == 1)
            {

                String t1 = Exp(raiz);
                String eti1 = Ejecucion3d.generalabel();
                String eti2 = Ejecucion3d.generalabel();
                Ejecucion3d.cadenota += "if " + t1 + "= 1  goto" + eti1 + "\n";
                Ejecucion3d.cadenota += "goto " + eti2 + "\n";

            }
            else
            {
                ArrayList t1;
                ArrayList t2;
                String tipo = "";
                if (raiz.ChildNodes.ElementAt(2).Term.Name.Equals("OPEREL"))
                {
                    tipo = raiz.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Text;
                }
                else
                {
                    tipo = raiz.ChildNodes.ElementAt(2).Token.Text;
                }
                switch (tipo)
                {
                    case ">":
                    case "<":
                    case "<=":
                    case ">=":
                    case "!=":
                        String t11 = Expt(raiz.ChildNodes.ElementAt(0));
                        String t22 = Expt(raiz.ChildNodes.ElementAt(1));
                        String eti1 = Ejecucion3d.generalabel();
                        String eti2 = Ejecucion3d.generalabel();
                        if (!biffalse)
                        {
                            String t4 = "if " + t11 + tipo + t22 + " goto " + eti1 + ";\n";
                            Ejecucion3d.cadenota += t4;

                            t4 = "goto " + eti2 + ";\n";
                            Ejecucion3d.cadenota += t4;
                            ret.Add(eti1);
                            ret.Add(eti2);
                        }
                        else
                        {
                            tipo = contrario(tipo);
                            String t4 = "iffalse " + t11 + tipo + t22 + " goto " + eti2 + ";\n";
                            Ejecucion3d.cadenota += t4;

                            t4 = "goto " + eti1 + ";\n";
                            Ejecucion3d.cadenota += t4;
                            ret.Add(eti2);
                            ret.Add(eti1);

                        }
                      
                        return ret;
                    case "==":
                        String t111 = Expt(raiz.ChildNodes.ElementAt(0));
                        String t221 = Expt(raiz.ChildNodes.ElementAt(1));
                        String eti11 = Ejecucion3d.generalabel();
                        String eti21 = Ejecucion3d.generalabel();
                        if (!biffalse)
                        {
                            String t4 = "if " + t111 + "=" + t221 + " goto " + eti11 + ";\n";
                            Ejecucion3d.cadenota += t4;

                            t4 = "goto " + eti21 + ";\n";
                            Ejecucion3d.cadenota += t4;
                            ret.Add(eti11);
                            ret.Add(eti21);
                        }
                        else
                        {
                            tipo = contrario(tipo);
                            String t4 = "iffalse " + t111 + tipo + t221 + " goto " + eti21 + ";\n";
                            Ejecucion3d.cadenota += t4;

                            t4 = "goto " + eti11 + ";\n";
                            Ejecucion3d.cadenota += t4;
                            ret.Add(eti21);
                            ret.Add(eti11);

                        }
                        
                        return ret;
                    case "and":
                        biffalse = true;
                        t1 = condt(raiz.ChildNodes.ElementAt(0));
                        Ejecucion3d.cadenota += t1[0].ToString() + ":\n";
                        t2 = condt(raiz.ChildNodes.ElementAt(2));
                        String lf = t1[1].ToString() + "," + t2[1].ToString();
                        ret.Add(t2[0].ToString());
                        String[] lista1 = t2[0].ToString().Split(',');
                        Ejecucion3d.cadenota += " goto " + lista1[0] + ";\n";
                        ret.Add(lf);
                        return ret;
                    case "or":
                        biffalse = false;
                        t1 = condt(raiz.ChildNodes.ElementAt(0));
                        Ejecucion3d.cadenota += t1[1].ToString() + ":\n";
                        t2 = condt(raiz.ChildNodes.ElementAt(2));
                        String lv = t1[0].ToString() + "," + t2[0].ToString();
                        ret.Add(lv);
                        ret.Add(t2[1].ToString());
                        return ret;
                    case "xor":
                        biffalse = false;
                        String txor = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += txor + "= 0;\n";
                        t1 = condt(raiz.ChildNodes.ElementAt(0));
                        String Lxor1 = Ejecucion3d.generalabel();
                        Ejecucion3d.cadenota += t1[0].ToString() + ":\n";
                        Ejecucion3d.cadenota += txor + "=" + txor + "+ 1;\n";
                        Ejecucion3d.cadenota += "goto " + Lxor1 + ";\n";
                        Ejecucion3d.cadenota += t1[1].ToString() + ":\n";
                        Ejecucion3d.cadenota += txor + "=" + txor + "- 1;\n";
                        Ejecucion3d.cadenota += Lxor1 + ":\n";
                        t2 = condt(raiz.ChildNodes.ElementAt(2));
                        Lxor1 = Ejecucion3d.generalabel();
                        Ejecucion3d.cadenota += t2[0].ToString() + ":\n";
                        Ejecucion3d.cadenota += txor + "=" + txor + "+ 1;\n";
                        Ejecucion3d.cadenota += "goto " + Lxor1 + ";\n";
                        Ejecucion3d.cadenota += t2[1].ToString() + ":\n";
                        Ejecucion3d.cadenota += txor + "=" + txor + "- 1;\n";
                        Ejecucion3d.cadenota += Lxor1 + ":\n";
                        Lxor1 = Ejecucion3d.generalabel();
                        String Lxor2 = Ejecucion3d.generalabel();
                        Ejecucion3d.cadenota += "if " + txor + "= 0 goto " + Lxor1 + ";\n";
                        Ejecucion3d.cadenota += "goto " + Lxor2 + ";\n";
                        ret.Add(Lxor1);
                        ret.Add(Lxor2);
                        return ret;


                }



            }



            return null;
        }

        public void SI(ParseTreeNode raiz)
        {

            ArrayList Banderas = cond(raiz.ChildNodes.ElementAt(0));

            String t3 = Ejecucion3d.generatemp();

            String lv = Banderas[0].ToString();
            String lf = Banderas[1].ToString();
            String sal = Ejecucion3d.generalabel();
            display.Add(sal);

            Ejecucion3d.cadenota += lv + ":\n";
            int pos = actualM.peso;
            Bloque(raiz.ChildNodes.ElementAt(1));
            Ejecucion3d.cadenota += "goto " + sal + ";\n";
            Ejecucion3d.cadenota += lf + ":\n";


            ParseTreeNode elif = raiz.ChildNodes.ElementAt(2);
            ParseTreeNode elsee = raiz.ChildNodes.ElementAt(2);


            int i = 0;
            foreach (TS elemento1 in actualM.elementos)
            {
                if (i >= pos)
                {
                    elemento1.localidad = 1;

                }

                i++;
            }


            if (elif.ChildNodes.Count > 0)
            {


                foreach (ParseTreeNode hijo in elif.ChildNodes)
                {
                    if (hijo.ChildNodes.Count == 1)
                    {
                        elsee = hijo;
                        goto elsee2;
                    }

                    if (hijo.ChildNodes.Count() > 0) {
                        Banderas = cond(hijo.ChildNodes.ElementAt(0));
                        t3 = Ejecucion3d.generatemp();

                        lv = Banderas[0].ToString();
                        lf = Banderas[1].ToString();

                        Ejecucion3d.cadenota += lv + ":\n";
                        pos = actualM.peso;
                        Bloque(hijo.ChildNodes.ElementAt(1));

                        i = 0;
                        foreach (TS elemento1 in actualM.elementos)
                        {
                            if (i >= pos)
                            {
                                elemento1.localidad = 1;

                            }

                            i++;
                        }

                        Ejecucion3d.cadenota += "goto " + sal + ";\n";
                        Ejecucion3d.cadenota += lf + ":\n";
                    }
                }

            }
            goto salida;

            elsee2:

            if (elsee.ChildNodes.Count == 1)
            {
                pos = actualM.peso;
                Bloque(elsee.ChildNodes.ElementAt(0));

                i = 0;
                foreach (TS elemento1 in actualM.elementos)
                {
                    if (i >= pos)
                    {
                        elemento1.localidad = 1;

                    }

                    i++;
                }

            }
            salida:

            Ejecucion3d.cadenota += sal + ":\n";
            int tt = display.Count;
            display.RemoveAt(tt - 1);

        }

        public void SIT(ParseTreeNode raiz)
        {

            ArrayList Banderas = condt(raiz.ChildNodes.ElementAt(1));

            String t3 = Ejecucion3d.generatemp();

            String lv = Banderas[0].ToString();
            String lf = Banderas[1].ToString();
            String sal = Ejecucion3d.generalabel();
            display.Add(sal);

            Ejecucion3d.cadenota += lv + ":\n";
            int pos = actualM.peso;
            Bloquet(raiz.ChildNodes.ElementAt(3));
            Ejecucion3d.cadenota += "goto " + sal + ";\n";
            Ejecucion3d.cadenota += lf + ":\n";


            ParseTreeNode elif = raiz.ChildNodes.ElementAt(4);
            ParseTreeNode elsee = raiz.ChildNodes.ElementAt(5);


            int i = 0;
            foreach (TS elemento1 in actualM.elementos)
            {
                if (i >= pos)
                {
                    elemento1.localidad = 1;

                }

                i++;
            }


            if (elif.ChildNodes.Count > 0)
            {


                foreach (ParseTreeNode hijo in elif.ChildNodes)
                {

                    if (hijo.ChildNodes.Count() > 0)
                    {
                        Banderas = condt(hijo.ChildNodes.ElementAt(1));
                        t3 = Ejecucion3d.generatemp();

                        lv = Banderas[0].ToString();
                        lf = Banderas[1].ToString();

                        Ejecucion3d.cadenota += lv + ":\n";
                        pos = actualM.peso;
                        Bloquet(hijo.ChildNodes.ElementAt(1));

                        i = 0;
                        foreach (TS elemento1 in actualM.elementos)
                        {
                            if (i >= pos)
                            {
                                elemento1.localidad = 1;

                            }

                            i++;
                        }

                        Ejecucion3d.cadenota += "goto " + sal + ";\n";
                        Ejecucion3d.cadenota += lf + ":\n";
                    }
                }

            }


            if (elsee.ChildNodes.Count == 1)
            {
                pos = actualM.peso;
                Bloquet(elsee.ChildNodes.ElementAt(0));

                i = 0;
                foreach (TS elemento1 in actualM.elementos)
                {
                    if (i >= pos)
                    {
                        elemento1.localidad = 1;

                    }

                    i++;
                }

            }


            Ejecucion3d.cadenota += sal + ":\n";
            int tt = display.Count;
            display.RemoveAt(tt - 1);

        }

        public void Mientras(ParseTreeNode raiz)
        {

            String lini = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += lini + ":\n";
            ArrayList Banderas = cond(raiz.ChildNodes.ElementAt(0));

            String lv = Banderas[0].ToString();
            String lf = Banderas[1].ToString();
            display.Add(lf);

            int pos = actualM.peso;
            Ejecucion3d.cadenota += lv + ":\n";
            Bloque(raiz.ChildNodes.ElementAt(1));

            int i = 0;
            foreach (TS elemento1 in actualM.elementos)
            {
                if (i >= pos)
                {
                    elemento1.localidad = 1;

                }

                i++;
            }

            Ejecucion3d.cadenota += "goto " + lini + ";\n";
            Ejecucion3d.cadenota += lf + ":\n";
            int tt = display.Count;
            display.RemoveAt(tt - 1);
        }

        public void Mientrast(ParseTreeNode raiz)
        {

            String lini = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += lini + ":\n";
            ArrayList Banderas = condt(raiz.ChildNodes.ElementAt(1));

            String lv = Banderas[0].ToString();
            String lf = Banderas[1].ToString();
            display.Add(lf);

            int pos = actualM.peso;
            Ejecucion3d.cadenota += lv + ":\n";
            Bloquet(raiz.ChildNodes.ElementAt(3));

            int i = 0;
            foreach (TS elemento1 in actualM.elementos)
            {
                if (i >= pos)
                {
                    elemento1.localidad = 1;

                }

                i++;
            }

            Ejecucion3d.cadenota += "goto " + lini + ";\n";
            Ejecucion3d.cadenota += lf + ":\n";
            int tt = display.Count;
            display.RemoveAt(tt - 1);
        }

        public void Loop(ParseTreeNode raiz)
        {

            String lini = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += lini + ":\n";
            String lf = Ejecucion3d.generalabel();
            display.Add(lf);
            int pos = actualM.peso;
            Bloquet(raiz.ChildNodes.ElementAt(0));

            int i = 0;
            foreach (TS elemento1 in actualM.elementos)
            {
                if (i >= pos)
                {
                    elemento1.localidad = 1;

                }

                i++;
            }

            Ejecucion3d.cadenota += "goto " + lini + ";\n";
            Ejecucion3d.cadenota += lf + ":\n";
            int tt = display.Count;
            display.RemoveAt(tt - 1);

        }

        public void HMientras(ParseTreeNode raiz)
        {

            String lini = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += lini + ":\n";
            int pos = actualM.peso;
            String sal = Ejecucion3d.generalabel();
            display.Add(sal);
            Bloque(raiz.ChildNodes.ElementAt(0));

            int i = 0;
            foreach (TS elemento1 in actualM.elementos)
            {
                if (i >= pos)
                {
                    elemento1.localidad = 1;

                }

                i++;
            }


            ArrayList Banderas = cond(raiz.ChildNodes.ElementAt(1));

            String lv = Banderas[0].ToString();
            String lf = Banderas[1].ToString();


            Ejecucion3d.cadenota += lv + ":\n";
            Ejecucion3d.cadenota += "goto " + lini + ";\n";
            Ejecucion3d.cadenota += lf + ":\n";
            Ejecucion3d.cadenota += sal + ":\n";
            int tt = display.Count;
            display.RemoveAt(tt - 1);
        }

        public void HMientrast(ParseTreeNode raiz)
        {

            String lini = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += lini + ":\n";
            int pos = actualM.peso;
            String sal = Ejecucion3d.generalabel();
            display.Add(sal);
            Bloquet(raiz.ChildNodes.ElementAt(0));

            int i = 0;
            foreach (TS elemento1 in actualM.elementos)
            {
                if (i >= pos)
                {
                    elemento1.localidad = 1;

                }

                i++;
            }


            ArrayList Banderas = condt(raiz.ChildNodes.ElementAt(2));

            String lv = Banderas[0].ToString();
            String lf = Banderas[1].ToString();


            Ejecucion3d.cadenota += lv + ":\n";
            Ejecucion3d.cadenota += "goto " + lini + ";\n";
            Ejecucion3d.cadenota += lf + ":\n";
            Ejecucion3d.cadenota += sal + ":\n";
            int tt = display.Count;
            display.RemoveAt(tt - 1);
        }

        public void Repetir(ParseTreeNode raiz)
        {
            String lini = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += lini + ":\n";
            int pos = actualM.peso;
            String sal = Ejecucion3d.generalabel();
            display.Add(sal);
            Bloque(raiz.ChildNodes.ElementAt(0));
            int i = 0;
            foreach (TS elemento1 in actualM.elementos)
            {
                if (i >= pos)
                {
                    elemento1.localidad = 1;

                }

                i++;
            }
            ArrayList Banderas = cond(raiz.ChildNodes.ElementAt(1));

            String lv = Banderas[0].ToString();
            String lf = Banderas[1].ToString();


            Ejecucion3d.cadenota += lf + ":\n";

            Ejecucion3d.cadenota += "goto " + lini + ";\n";

            Ejecucion3d.cadenota += lv + ":\n";
            Ejecucion3d.cadenota += sal + ":\n";
            int tt = display.Count;
            display.RemoveAt(tt - 1);

        }

        public void Repetirt(ParseTreeNode raiz)
        {

            String lini = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += lini + ":\n";
            int pos = actualM.peso;
            String sal = Ejecucion3d.generalabel();
            display.Add(sal);
            Bloquet(raiz.ChildNodes.ElementAt(0));
            int i = 0;
            foreach (TS elemento1 in actualM.elementos)
            {
                if (i >= pos)
                {
                    elemento1.localidad = 1;

                }

                i++;
            }
            ArrayList Banderas = condt(raiz.ChildNodes.ElementAt(2));

            String lv = Banderas[0].ToString();
            String lf = Banderas[1].ToString();


            Ejecucion3d.cadenota += lf + ":\n";

            Ejecucion3d.cadenota += "goto " + lini + ";\n";

            Ejecucion3d.cadenota += lv + ":\n";
            Ejecucion3d.cadenota += sal + ":\n";
            int tt = display.Count;
            display.RemoveAt(tt - 1);
        }

        public void Para(ParseTreeNode raiz)
        {

            TS nueva = new TS(raiz.ChildNodes.ElementAt(1).Token.Text, 4, actualM.peso, 1, 0, 1, "", 0, 0, null);
            int pos = actualM.peso;
            String t1 = Exp(raiz.ChildNodes.ElementAt(2));
            String t2 = Ejecucion3d.generatemp();
            Ejecucion3d.cadenota += t2 + "= sp +" + nueva.posicion + ";\n";
            Ejecucion3d.cadenota += "stack [" + t2 + "] = " + t1 + ";\n";
            actualM.peso++;
            actualM.elementos.Add(nueva);
            String lini = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += lini + ":\n";
            ArrayList Banderas = cond(raiz.ChildNodes.ElementAt(3));
            String lv = Banderas[0].ToString();
            String lf = Banderas[1].ToString();
            display.Add(lf);

            Ejecucion3d.cadenota += lv + ": \n";
            Bloque(raiz.ChildNodes.ElementAt(5));
            MM(raiz.ChildNodes.ElementAt(4));

            Ejecucion3d.cadenota += "goto " + lini + ";\n";
            Ejecucion3d.cadenota += lf + ": \n";
            nueva.localidad = 1;
            int i = 0;
            foreach (TS elemento1 in actualM.elementos)
            {
                if (i > pos)
                {
                    elemento1.localidad = 1;

                }

                i++;
            }

            int tt = display.Count;
            display.RemoveAt(tt - 1);



        }

        public void Parat(ParseTreeNode raiz)
        {

            TS nueva = new TS(raiz.ChildNodes.ElementAt(2).Token.Text, 4, actualM.peso, 1, 0, 1, "", 0, 0, null);
            int pos = actualM.peso;
            String t1 = Expt(raiz.ChildNodes.ElementAt(3));
            String t2 = Ejecucion3d.generatemp();
            Ejecucion3d.cadenota += t2 + "= sp +" + nueva.posicion + ";\n";
            Ejecucion3d.cadenota += "stack [" + t2 + "] = " + t1 + ";\n";
            actualM.peso++;
            actualM.elementos.Add(nueva);
            String lini = Ejecucion3d.generalabel();
            Ejecucion3d.cadenota += lini + ":\n";
            ArrayList Banderas = condt(raiz.ChildNodes.ElementAt(4));
            String lv = Banderas[0].ToString();
            String lf = Banderas[1].ToString();
            display.Add(lf);
            Ejecucion3d.cadenota += lv + ": \n";
            Bloquet(raiz.ChildNodes.ElementAt(7));
            MMt(raiz.ChildNodes.ElementAt(5));

            Ejecucion3d.cadenota += "goto " + lini + ";\n";
            Ejecucion3d.cadenota += lf + ": \n";
            nueva.localidad = 1;
            int i = 0;
            foreach (TS elemento1 in actualM.elementos)
            {
                if (i > pos)
                {
                    elemento1.localidad = 1;

                }

                i++;
            }
            int tt = display.Count;
            display.RemoveAt(tt - 1);


        }

        public void CX(ParseTreeNode raiz)
        {
            ArrayList Bandera1 = cond(raiz.ChildNodes.ElementAt(0));
            String lv1 = Bandera1[0].ToString();
            String lf1 = Bandera1[1].ToString();

            Ejecucion3d.cadenota += lf1 + ":\n";

            ArrayList Bandera2 = cond(raiz.ChildNodes.ElementAt(1));
            String lv2 = Bandera2[0].ToString();
            String lf2 = Bandera2[1].ToString();


            String ini = Ejecucion3d.generalabel();

            Ejecucion3d.cadenota += ini + ":\n";

            ArrayList Bandera3 = cond(raiz.ChildNodes.ElementAt(0));
            String lv3 = Bandera3[0].ToString();
            String lf3 = Bandera3[1].ToString();

            Ejecucion3d.cadenota += lv3 + ":\n";

            ArrayList Bandera4 = cond(raiz.ChildNodes.ElementAt(1));
            String lv4 = Bandera4[0].ToString();
            String lf4 = Bandera4[1].ToString();

            Ejecucion3d.cadenota += lv1 + "," + lv2 + "," + lv4 + ":\n";
            int pos = actualM.peso;
            display.Add(lf4);
            Bloque(raiz.ChildNodes.ElementAt(2));
            Ejecucion3d.cadenota += "goto " + ini + ";\n";

            Ejecucion3d.cadenota += lf2 + "," + lf3 + "," + lf4 + ":\n";

            int tt = display.Count;
            display.RemoveAt(tt - 1);
            int i = 0;
            foreach (TS elemento1 in actualM.elementos)
            {
                if (i > pos)
                {
                    elemento1.localidad = 1;

                }

                i++;
            }

        }

        public void MM(ParseTreeNode raiz)
        {
            ParseTreeNode aux;

            aux = raiz.ChildNodes.ElementAt(0);
            String nombre = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
            String t1 = raiz.ChildNodes.ElementAt(1).Token.Text;
            if (t1.Equals("++")) { t1 = "+ 1"; } else { t1 = "- 1"; }
            bool bandera = false;

            if (aux.ChildNodes.Count == 1)
            {

                foreach (TS tablita in actualM.elementos)
                {

                    if (tablita.Tipo == 4 && tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                    {
                        String t2 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t2 + "= sp + " + tablita.posicion + ";\n";
                        String t3 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t3 + " = stack[" + t2 + "];\n";
                        Ejecucion3d.cadenota += t3 + " = " + t3 + t1 + ";\n";
                        Ejecucion3d.cadenota += "stack[" + t2 + "] = " + t3 + ";\n";
                        bandera = true;
                        break;
                    }
                }
                if (!bandera)
                {

                    foreach (TS tablita in actualc.elementos)
                    {

                        if (tablita.Tipo == 5 && tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                        {
                            String t2 = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += t2 + "= selfp + " + tablita.posicion + ";\n";
                            String t3 = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += t3 + " = heap[" + t2 + "];\n";
                            Ejecucion3d.cadenota += t3 + " = " + t3 + t1 + ";\n";
                            Ejecucion3d.cadenota += "heap[" + t2 + "] = " + t3 + ";\n";
                            bandera = true;
                            break;
                        }


                    }




                }

                if (!bandera)
                {

                    error = 1;
                    return;

                }


            }
            else
            {
                String t4 = "";
                foreach (TS tablita in actualM.elementos)
                {

                    if (tablita.Tipo == 3 && tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                    {
                        String t2 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t2 + "= sp + " + tablita.posicion + ";\n";
                        String clase = tablita.etiqueta;
                        t4 = t2;
                        int i = 0;


                        foreach (ParseTreeNode atri in aux.ChildNodes)
                        {
                            if (i == 0)
                            {

                            }
                            else
                            {
                                nombre = atri.Token.Text;

                                foreach (TS clasesita in TS.TablaSimbolos)
                                {
                                    if (clasesita.nombre.Equals(clase))
                                    {

                                        foreach (TS tablita2 in clasesita.elementos)
                                        {
                                            if (tablita2.nombre.Equals(nombre))
                                            {
                                                String t3 = Ejecucion3d.generatemp();
                                                Ejecucion3d.cadenota += t3 + "= heap[" + t4 + "];\n";
                                                t4 = Ejecucion3d.generatemp();
                                                Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + tablita2.posicion + ";\n";

                                                clase = tablita2.etiqueta;



                                            }
                                        }
                                    }
                                }
                            }
                            i++;
                        }

                        String t8 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t8 + " = heap[" + t4 + "];\n";
                        Ejecucion3d.cadenota += t8 + " = " + t8 + t1 + ";\n";
                        Ejecucion3d.cadenota += "heap[" + t4 + "] = " + t8 + ";\n";
                        bandera = true;
                        break;
                    }
                }
                if (!bandera)
                {
                    foreach (TS tablita in actualc.elementos)
                    {

                        if (tablita.Tipo == 3 && tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                        {
                            String t2 = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += t2 + "= selfp + " + tablita.posicion + ";\n";

                            String clase = tablita.etiqueta;
                            t4 = t2;
                            int i = 0;


                            foreach (ParseTreeNode atri in aux.ChildNodes)
                            {
                                if (i == 0)
                                {

                                }
                                else
                                {
                                    nombre = atri.Token.Text;

                                    foreach (TS clasesita in TS.TablaSimbolos)
                                    {
                                        if (clasesita.nombre.Equals(clase))
                                        {

                                            foreach (TS tablita2 in clasesita.elementos)
                                            {
                                                if (tablita2.nombre.Equals(nombre))
                                                {
                                                    String t3 = Ejecucion3d.generatemp();

                                                    Ejecucion3d.cadenota += t3 + " = heap[" + t4 + "];\n";
                                                    t4 = Ejecucion3d.generatemp();
                                                    Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + tablita2.posicion + ";\n";

                                                }
                                            }
                                        }
                                    }
                                }
                                i++;
                            }

                            String t8 = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += t8 + " = heap[" + t4 + "];\n";
                            Ejecucion3d.cadenota += t8 + " = " + t8 + t1 + ";\n";
                            Ejecucion3d.cadenota += "heap[" + t4 + "] = " + t8 + ";\n";
                            bandera = true;
                            break;
                        }
                    }



                }

                if (!bandera)
                {

                    error = 1;
                    return;

                }


            }


        }

        public void MMt(ParseTreeNode raiz)
        {
            ParseTreeNode aux;

            aux = raiz.ChildNodes.ElementAt(0);
            String nombre = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
            String t1 = raiz.ChildNodes.ElementAt(1).Token.Text;
            if (t1.Equals("++")) { t1 = "+ 1"; } else { t1 = "- 1"; }
            bool bandera = false;

            if (aux.ChildNodes.Count == 1)
            {

                foreach (TS tablita in actualM.elementos)
                {

                    if (tablita.Tipo == 4 && tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                    {
                        String t2 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t2 + "= sp + " + tablita.posicion + ";\n";
                        String t3 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t3 + " = stack[" + t2 + "];\n";
                        Ejecucion3d.cadenota += t3 + " = " + t3 + t1 + ";\n";
                        Ejecucion3d.cadenota += "stack[" + t2 + "] = " + t3 + ";\n";
                        bandera = true;
                        break;
                    }
                }
                if (!bandera)
                {

                    foreach (TS tablita in actualc.elementos)
                    {

                        if (tablita.Tipo == 5 && tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                        {
                            String t2 = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += t2 + "= selfp + " + tablita.posicion + ";\n";
                            String t3 = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += t3 + " = heap[" + t2 + "];\n";
                            Ejecucion3d.cadenota += t3 + " = " + t3 + t1 + ";\n";
                            Ejecucion3d.cadenota += "heap[" + t2 + "] = " + t3 + ";\n";
                            bandera = true;
                            break;
                        }


                    }




                }

                if (!bandera)
                {

                    error = 1;
                    return;

                }


            }
            else
            {
                String t4 = "";
                foreach (TS tablita in actualM.elementos)
                {

                    if (tablita.Tipo == 3 && tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                    {
                        String t2 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t2 + "= sp + " + tablita.posicion + ";\n";
                        String clase = tablita.etiqueta;
                        t4 = t2;
                        int i = 0;


                        foreach (ParseTreeNode atri in aux.ChildNodes)
                        {
                            if (i == 0)
                            {

                            }
                            else
                            {
                                nombre = atri.Token.Text;

                                foreach (TS clasesita in TS.TablaSimbolos)
                                {
                                    if (clasesita.nombre.Equals(clase))
                                    {

                                        foreach (TS tablita2 in clasesita.elementos)
                                        {
                                            if (tablita2.nombre.Equals(nombre))
                                            {
                                                String t3 = Ejecucion3d.generatemp();
                                                Ejecucion3d.cadenota += t3 + "= heap[" + t4 + "];\n";
                                                t4 = Ejecucion3d.generatemp();
                                                Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + tablita2.posicion + ";\n";

                                                clase = tablita2.etiqueta;



                                            }
                                        }
                                    }
                                }
                            }
                            i++;
                        }

                        String t8 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t8 + " = heap[" + t4 + "];\n";
                        Ejecucion3d.cadenota += t8 + " = " + t8 + t1 + ";\n";
                        Ejecucion3d.cadenota += "heap[" + t4 + "] = " + t8 + ";\n";
                        bandera = true;
                        break;
                    }
                }
                if (!bandera)
                {
                    foreach (TS tablita in actualc.elementos)
                    {

                        if (tablita.Tipo == 3 && tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                        {
                            String t2 = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += t2 + "= selfp + " + tablita.posicion + ";\n";

                            String clase = tablita.etiqueta;
                            t4 = t2;
                            int i = 0;


                            foreach (ParseTreeNode atri in aux.ChildNodes)
                            {
                                if (i == 0)
                                {

                                }
                                else
                                {
                                    nombre = atri.Token.Text;

                                    foreach (TS clasesita in TS.TablaSimbolos)
                                    {
                                        if (clasesita.nombre.Equals(clase))
                                        {

                                            foreach (TS tablita2 in clasesita.elementos)
                                            {
                                                if (tablita2.nombre.Equals(nombre))
                                                {
                                                    String t3 = Ejecucion3d.generatemp();

                                                    Ejecucion3d.cadenota += t3 + " = heap[" + t4 + "];\n";
                                                    t4 = Ejecucion3d.generatemp();
                                                    Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + tablita2.posicion + ";\n";

                                                }
                                            }
                                        }
                                    }
                                }
                                i++;
                            }

                            String t8 = Ejecucion3d.generatemp();
                            Ejecucion3d.cadenota += t8 + " = heap[" + t4 + "];\n";
                            Ejecucion3d.cadenota += t8 + " = " + t8 + t1 + ";\n";
                            Ejecucion3d.cadenota += "heap[" + t4 + "] = " + t8 + ";\n";
                            bandera = true;
                            break;
                        }
                    }



                }

                if (!bandera)
                {

                    error = 1;
                    return;

                }


            }


        }

        public void Bloque(ParseTreeNode bloque)
        {

            foreach (ParseTreeNode bloquesito in bloque.ChildNodes)
            {
                String valor = bloquesito.Term.Name;

                switch (valor)
                {
                    case "SELF":
                        self(bloquesito);
                        break;
                    case "IF":
                        SI(bloquesito);
                        break;
                    case "MIENTRAS":
                        Mientras(bloquesito);
                        break;
                    case "HACER":
                        HMientras(bloquesito);
                        break;
                    case "REPETIR":
                        Repetir(bloquesito);
                        break;
                    case "X":
                        CX(bloquesito);
                        break;
                    case "FOR":
                        Para(bloquesito);
                        break;
                    case "DEC":
                        declararv(bloquesito);
                        break;
                    case "ASIGNACION":
                        asignar(bloquesito);
                        break;
                    case "ATRIBUTO":
                        Llamadao(bloquesito);
                        break;
                    case "LLAMADA":
                        llamada(bloquesito);
                        break;
                    case "IMPRESION":
                        imprimir(bloquesito);
                        break;
                    case "RET":
                        Retorno(bloquesito);
                        break;
                    case "MM":
                        MM(bloquesito);
                        break;
                    default:
                        break;
                }


            }
        }

        public void Bloquet(ParseTreeNode bloque)
        {

            foreach (ParseTreeNode bloquesito in bloque.ChildNodes)
            {
                String valor = bloquesito.Term.Name;

                switch (valor)
                {
                    case "SELF":
                        selft(bloquesito);
                        break;
                    case "IF":
                        SIT(bloquesito);
                        break;
                    case "MIENTRAS":
                        Mientrast(bloquesito);
                        break;
                    case "HACER":
                        HMientrast(bloquesito);
                        break;
                    case "REPETIR":
                        Repetirt(bloquesito);
                        break;
                    case "X":
                        CX(bloquesito);
                        break;
                    case "FOR":
                        Parat(bloquesito);
                        break;
                    case "DEC":
                        declararvt(bloquesito);
                        break;
                    case "ASIGNACION":
                        asignart(bloquesito);
                        break;
                    case "ATRIBUTO":
                        Llamadao(bloquesito);
                        break;
                    case "LOOP":
                        Loop(bloquesito);
                        break;
                    case "LLAMADA":
                        llamada(bloquesito);
                        break;
                    case "MM":
                        MMt(bloquesito);
                        break;
                    case "SALIR":
                        Salir();
                        break;
                    case "ELEGIR":
                        Elegir(bloquesito);
                        break;
                    case "RET":
                        Retornot(bloquesito);
                        break;
                    default:
                        break;
                }


            }
        }

        public void Salir()
        {

            int tama = display.Count;
            object eti = display[tama - 1];
            String eti2 = eti.ToString();
            Ejecucion3d.cadenota += "goto " + eti2 + ";\n";

        }

        public void Retorno(ParseTreeNode bloque)
        {
            String t2 = Exp(bloque.ChildNodes.ElementAt(1));
            Ejecucion3d.cadenota += "stack[sp]=" + t2 + ";\n";
        }

        public void Retornot(ParseTreeNode bloque)
        {
            String t2 = Expt(bloque.ChildNodes.ElementAt(1));
            Ejecucion3d.cadenota += "stack[sp]=" + t2 + ";\n";
        }

        public int cantip(TS elemento)
        {
            int can = 0;
            foreach (TS pari in elemento.elementos)
            {
                if (pari.Tipo == 2)
                {
                    can++;

                }

            }

            return can;
        }

        public ArrayList tipos(TS elemento)
        {
            ArrayList ret = new ArrayList();


            foreach (TS pari in elemento.elementos)
            {
                if (pari.Tipo == 2)
                {
                    ret.Add(pari.tipo2);
                }

            }



            return ret;
        }

        public ArrayList tipos2(ParseTreeNode raiz)
        {
            ArrayList ret = new ArrayList();
            String tipov;
            int tipov2 = 1;
            foreach (ParseTreeNode hijo in raiz.ChildNodes)
            {

                tipov = hijo.ChildNodes.ElementAt(0).Token.Text;

                switch (tipov)
                {
                    case "entero":
                        tipov2 = 1;
                        break;
                    case "cadena":
                        tipov2 = 2;
                        break;
                    case "booleano":
                        tipov2 = 3;
                        break;
                    case "decimal":
                        tipov2 = 4;
                        break;
                    default:
                        tipov2 = 6;
                        break;
                }
                ret.Add(tipov2);

            }

            return ret;
        }

        public ArrayList nombresp(ParseTreeNode raiz)
        {
            ArrayList ret = new ArrayList();
            String tipov = "";
            String nombre = "";
            int tipov2 = 1;
            int pos = 1;
            foreach (ParseTreeNode hijo in raiz.ChildNodes)
            {

                tipov = hijo.ChildNodes.ElementAt(0).Token.Text;
                nombre = hijo.ChildNodes.ElementAt(1).Token.Text;
                switch (tipov)
                {
                    case "entero":
                        tipov2 = 1;
                        break;
                    case "cadena":
                        tipov2 = 2;
                        break;
                    case "booleano":
                        tipov2 = 3;
                        break;
                    case "decimal":
                        tipov2 = 4;
                        break;
                    default:
                        tipov2 = 6;
                        break;
                }
                //ver si es arreglo y llenaro si se necesita***********************
                TS nuevo = new TS(nombre, 2, pos, tipov2, 0, 1, "", 0, 0, null);
                ret.Add(nuevo);
                pos++;
            }

            return ret;
        }

        public String comillas(String valor)
        {
            String[] val = valor.Split('\"');

            valor = val[1];
            return valor;
        }

        public void Llamadao(ParseTreeNode raiz)
        {



            if (raiz.ChildNodes.Count < 2)
            {
                errore += "Incoerencia al llamar a una variable o a un objeto sin funcion asociada, col: " + raiz.ChildNodes.ElementAt(0).Token.Location.Column + " fil: " + raiz.ChildNodes.ElementAt(0).Token.Location.Line + "\n";
                return;
            }

            ParseTreeNode aux, aux2;
            aux = raiz;
            String nombre = aux.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;

            bool bandera = false;

            String t4 = "";
            foreach (TS tablita in actualM.elementos)
            {

                if (tablita.Tipo == 3 && tablita.nombre.Equals(nombre) && tablita.localidad == 0)
                {
                    String t2 = Ejecucion3d.generatemp();
                    Ejecucion3d.cadenota += t2 + "= sp + " + tablita.posicion + ";\n";
                    String clase = tablita.etiqueta;
                    t4 = t2;
                    int i = 0;


                    foreach (ParseTreeNode atri in aux.ChildNodes)
                    {
                        if (i == 0)
                        {

                        }
                        else
                        {
                            nombre = atri.ChildNodes.ElementAt(0).Token.Text;

                            foreach (TS clasesita in TS.TablaSimbolos)
                            {
                                if (clasesita.nombre.Equals(clase))
                                {

                                    foreach (TS tablita2 in clasesita.elementos)
                                    {
                                        if (tablita2.nombre.Equals(nombre) && tablita2.Tipo == 5 && tablita2.tipo2 == 6)
                                        {
                                            String t3 = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += t3 + "= heap[" + t4 + "];\n";
                                            t4 = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + tablita2.posicion + ";\n";

                                            clase = tablita2.etiqueta;

                                        }
                                        else if (tablita2.nombre.Equals(nombre) && (tablita2.Tipo == 7 || tablita2.Tipo == 6))
                                        {
                                            if (atri.ChildNodes.Count == 1)
                                            {
                                                errore += "Error al llamar a la funcion, " + nombre + "se le trata como una variable, col: " + atri.ChildNodes.ElementAt(0).Token.Location.Column + " fil: " + raiz.ChildNodes.ElementAt(0).Token.Location.Line;
                                                return;

                                            }

                                            int canti = cantip(tablita2);
                                            ParseTreeNode le = atri.ChildNodes.ElementAt(1);
                                            int canti2 = le.ChildNodes.Count;

                                            if (canti2 != canti)
                                            {
                                                errore += "Error al llamar a la funcion, " + nombre + "inconsistencia en los parametros, col: " + atri.ChildNodes.ElementAt(0).Token.Location.Column + " fil: " + raiz.ChildNodes.ElementAt(0).Token.Location.Line;
                                                return;
                                            }

                                            String t3 = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += t3 + "= heap[" + t4 + "];\n";
                                            t4 = t3;

                                            clase = tablita2.etiqueta;

                                            String ty = Ejecucion3d.generatemp();
                                            Ejecucion3d.cadenota += ty + "= sp +" + actualM.peso + ";\n";
                                            Ejecucion3d.cadenota += "stack [" + ty + "] = 00;\n";
                                            Ejecucion3d.cadenota += ty + "=" + ty + "+1;\n";
                                            String tz = "";
                                            foreach (ParseTreeNode lee in le.ChildNodes)
                                            {
                                                tz = Exp(lee);
                                                Ejecucion3d.cadenota += "stack [" + ty + "] =" + tz + ";\n";
                                                Ejecucion3d.cadenota += ty + "=" + ty + "+1;\n";
                                            }


                                            Ejecucion3d.cadenota += "DS [selfp2 ]= selfp;\n";
                                            Ejecucion3d.cadenota += "selfp2 = selfp2 + 1;\n";
                                            Ejecucion3d.cadenota += "selfp = " + t4 + ";\n";
                                            Ejecucion3d.cadenota += "sp = sp + " + actualM.peso + ";\n";
                                            Ejecucion3d.cadenota += tablita2.etiqueta + "();\n";
                                            Ejecucion3d.cadenota += t4 + "= stack[sp];\n";
                                            Ejecucion3d.cadenota += "sp = sp - " + actualM.peso + ";\n";
                                            Ejecucion3d.cadenota += "selfp2 = selfp2 - 1;\n";
                                            Ejecucion3d.cadenota += "selfp = DS [selfp2 ];\n";





                                        }

                                    }
                                }
                            }
                        }
                        i++;
                    }


                    bandera = true;
                    break;
                }
            }
            if (!bandera)
            {
                foreach (TS tablita in actualc.elementos)
                {

                    if (tablita.Tipo == 3 && tablita.nombre.Equals(nombre))
                    {
                        String t2 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t2 + "= selfp + " + tablita.posicion + ";\n";

                        String clase = tablita.etiqueta;
                        t4 = t2;
                        int i = 0;


                        foreach (ParseTreeNode atri in aux.ChildNodes)
                        {
                            if (i == 0)
                            {

                            }
                            else
                            {
                                nombre = atri.Token.Text;

                                foreach (TS clasesita in TS.TablaSimbolos)
                                {
                                    if (clasesita.nombre.Equals(clase))
                                    {

                                        foreach (TS tablita2 in clasesita.elementos)
                                        {
                                            if (tablita2.nombre.Equals(nombre) && tablita2.tipo2 == 6 && tablita2.Tipo == 5)
                                            {
                                                String t3 = Ejecucion3d.generatemp();

                                                Ejecucion3d.cadenota += t3 + " = heap[" + t4 + "];\n";
                                                t4 = Ejecucion3d.generatemp();
                                                Ejecucion3d.cadenota += t4 + "=" + t3 + "+" + tablita2.posicion + ";\n";

                                            }
                                            if (tablita2.nombre.Equals(nombre) && (tablita2.Tipo == 7 || tablita2.Tipo == 6))
                                            {
                                                if (atri.ChildNodes.Count == 1)
                                                {
                                                    errore += "Error al llamar a la funcion, " + nombre + "se le trata como una variable, col: " + atri.ChildNodes.ElementAt(0).Token.Location.Column + " fil: " + raiz.ChildNodes.ElementAt(0).Token.Location.Line;
                                                    return;

                                                }

                                                int canti = cantip(tablita2);
                                                ParseTreeNode le = atri.ChildNodes.ElementAt(1);
                                                int canti2 = le.ChildNodes.Count;

                                                if (canti2 != canti)
                                                {
                                                    errore += "Error al llamar a la funcion, " + nombre + "inconsistencia en los parametros, col: " + atri.ChildNodes.ElementAt(0).Token.Location.Column + " fil: " + raiz.ChildNodes.ElementAt(0).Token.Location.Line;
                                                    return;
                                                }

                                                String t3 = Ejecucion3d.generatemp();
                                                Ejecucion3d.cadenota += t3 + "= heap[" + t4 + "];\n";
                                                t4 = t3;

                                                String ty = Ejecucion3d.generatemp();
                                                Ejecucion3d.cadenota += ty + "= sp +" + actualM.peso + ";\n";
                                                Ejecucion3d.cadenota += "stack [" + ty + "] = 00;\n";
                                                Ejecucion3d.cadenota += ty + "=" + ty + "+1;\n";
                                                String tz = "";
                                                foreach (ParseTreeNode lee in le.ChildNodes)
                                                {
                                                    tz = Exp(lee);
                                                    Ejecucion3d.cadenota += "stack [" + ty + "] =" + tz + ";\n";
                                                    Ejecucion3d.cadenota += ty + "=" + ty + "+1;\n";
                                                }


                                                Ejecucion3d.cadenota += "DS [selfp2 ]= selfp;\n";
                                                Ejecucion3d.cadenota += "selfp2 = selfp2 + 1;\n";
                                                Ejecucion3d.cadenota += "selfp = " + t4 + ";\n";
                                                Ejecucion3d.cadenota += "sp = sp + " + actualM.peso + ";\n";
                                                Ejecucion3d.cadenota += tablita2.etiqueta + "();\n";
                                                Ejecucion3d.cadenota += t4 + "= stack[sp];\n";
                                                Ejecucion3d.cadenota += "sp = sp - " + actualM.peso + ";\n";
                                                Ejecucion3d.cadenota += "selfp2 = selfp2 - 1;\n";
                                                Ejecucion3d.cadenota += "selfp = DS [selfp2 ];\n";


                                            }
                                        }
                                    }
                                }
                            }
                            i++;
                        }


                        bandera = true;
                        break;
                    }
                }



            }

            if (!bandera)
            {

                error = 1;
                return;

            }


        }

        public void llamada(ParseTreeNode raiz)
        {

            String nombre = raiz.ChildNodes.ElementAt(0).Token.Text;

            foreach (TS tablita in actualc.elementos)
            {

                if (tablita.nombre.Equals(nombre) && tablita.Tipo > 5)
                {
                    int canti = cantip(tablita);

                    int canti2 = 0;
                    if (raiz.ChildNodes.Count > 1)
                    {
                        canti2 = raiz.ChildNodes.ElementAt(2).ChildNodes.Count();
                    }

                    if (canti2 != canti)
                    {
                        errore += "Error al llamar a la funcion, " + nombre + "inconsistencia en los parametros, col: " + raiz.ChildNodes.ElementAt(0).Token.Location.Column + " fil: " + raiz.ChildNodes.ElementAt(0).Token.Location.Line;
                        return;
                    }

                    String ty = Ejecucion3d.generatemp();
                    Ejecucion3d.cadenota += ty + "= sp +" + actualM.peso + ";\n";
                    Ejecucion3d.cadenota += "stack [" + ty + "] = 00;\n";
                    Ejecucion3d.cadenota += ty + "=" + ty + "+1;\n";
                    String tz = "";
                    if (canti > 0)
                    {
                        foreach (ParseTreeNode lee in raiz.ChildNodes.ElementAt(1).ChildNodes)
                        {
                            tz = Exp(lee);
                            Ejecucion3d.cadenota += "stack [" + ty + "] =" + tz + ";\n";
                            Ejecucion3d.cadenota += ty + "=" + ty + "+1;\n";
                        }

                    }

                    Ejecucion3d.cadenota += "sp = sp + " + actualM.peso + ";\n";
                    Ejecucion3d.cadenota += tablita.etiqueta + "();\n";
                    Ejecucion3d.cadenota += "sp = sp - " + actualM.peso + ";\n";


                }


            }


        }

        public int conver(int entra)
        {
            // 1 = integer  , 2 = string , 3 = booleano , 4 = decimal , 0 = void, 5 = clase, objeto = 6 ;
            // int = 0, char = 3, string = 2, decimal = 1 , booleano = 4
            switch (entra)
            {
                case 1:
                    return 0;
                case 2:
                    return 2;
                case 3:
                    return 3;
                case 4:
                    return 1;
                case 6:
                    return 6;

            }

            return 2;
        }

        public void Elegir(ParseTreeNode raiz)
        {
            String t1 = Expt(raiz.ChildNodes.ElementAt(1));
            String sal = Ejecucion3d.generalabel();
            display.Add(sal);
            ParseTreeNode casos = raiz.ChildNodes.ElementAt(3);

            foreach (ParseTreeNode hijo in casos.ChildNodes)
            {
                String t2 = Expt(hijo.ChildNodes.ElementAt(0));
                String salc = Ejecucion3d.generalabel();
                Ejecucion3d.cadenota += "if " + t1 + " != " + t2 + " goto " + salc + ";\n";
                Bloquet(hijo.ChildNodes.ElementAt(1));
                Ejecucion3d.cadenota += salc + ":\n";

            }
            ParseTreeNode def = raiz.ChildNodes.ElementAt(4);
            if (def.ChildNodes.Count > 0)
            {
                Bloquet(def.ChildNodes.ElementAt(0));
            }


            Ejecucion3d.cadenota += sal + ":\n";
            int tt = display.Count;
            display.RemoveAt(tt - 1);
        }

        public int resolverolca(ParseTreeNode raiz)
        {
            if (raiz.ChildNodes.Count == 1)
            {
                return Convert.ToInt32(raiz.ChildNodes.ElementAt(0).Token.Text);


            } else
            {
                int val1 = resolverolca(raiz.ChildNodes.ElementAt(0));
                int val2 = resolverolca(raiz.ChildNodes.ElementAt(2));
                String tipo = raiz.ChildNodes.ElementAt(1).Token.Text;

                switch (tipo)
                {
                    case "+":
                        return val1 + val2;
                    case "-":
                        return val1 - val2;
                    case "*":
                        return val1 * val2;
                    case "/":
                        return val1 / val2;
                    case "^":
                        return Convert.ToInt32(Math.Pow(val1, val2));
                }

            }
            return 0;
        }

        public int resolvertreea(ParseTreeNode raiz)
        {
            if (raiz.ChildNodes.Count == 1)
            {
                return Convert.ToInt32(raiz.ChildNodes.ElementAt(0).Token.Text);

            }
            else
            {
                int val1 = resolvertreea(raiz.ChildNodes.ElementAt(0));
                int val2 = resolvertreea(raiz.ChildNodes.ElementAt(1));
                String tipo = raiz.ChildNodes.ElementAt(2).Token.Text;

                switch (tipo)
                {
                    case "+":
                        return val1 + val2;
                    case "-":
                        return val1 - val2;
                    case "*":
                        return val1 * val2;
                    case "/":
                        return val1 / val2;
                    case "^":
                        return Convert.ToInt32(Math.Pow(val1, val2));
                }

            }
            return 0;
        }

        public String contrario(String tipo)
        {
            switch (tipo)
            {
                case ">":
                    return "<=";
                case "<":
                    return ">=";
                case "==":
                    return "!=";
                case "!=":
                    return "=";
                case "<=":
                    return ">";
                default:
                    return "<";

            }

        }

        public int esarre(int pos)
        {

            int conta = 0;
            foreach (TS hijo in actualc.elementos)
            {
                if (conta == pos)
                {
                    if (hijo.arreglo == 1)
                    {
                        int peso = 1;
                        for (int i = 0; i < hijo.tamanios.Count(); i++)
                        {
                            peso *= hijo.tamanios[i];
                        }

                        String t3 = Ejecucion3d.generatemp();
                        Ejecucion3d.cadenota += t3 + "=hp;\n";
                        for (int i = 0; i < peso; i++)
                        {
                            Ejecucion3d.cadenota += "heap[hp]=00;\n";
                            Ejecucion3d.cadenota += "hp = hp + 1;\n";
                        }
                        auxte = t3;
                        return peso;
                    }
                    else
                    {
                        return 0;
                    }
                }
                conta++;

            }
            return 0;
        }

        public bool enimportadas(String nombre){
            
            foreach(String hijo in importadasgeneral)
            {
                if (hijo.Equals(nombre))
                    return true;
            }
            
            
            return false;
            }

        public String importadastree(ParseTreeNode raiz)
        {
            String ret = "";

            if (raiz.ChildNodes.Count == 7)
            {
                ret = "http://mynube/" + raiz.ChildNodes.ElementAt(5).Token.Text + "." + raiz.ChildNodes.ElementAt(6).ChildNodes.ElementAt(0).Token.Text;

            }
            else if(raiz.ChildNodes.Count==2)
            {
                ret = raiz.ChildNodes.ElementAt(0).Token.Text + "." + raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text;

            }else
            {

                ret = "C:";
                ParseTreeNode path = raiz.ChildNodes.ElementAt(1);
                foreach(ParseTreeNode hijo in path.ChildNodes)
                {
                    ret += "\\"+hijo.Token.Text ;

                }

                ret += "." + raiz.ChildNodes.ElementAt(2).ChildNodes.ElementAt(0).Token.Text;
            }


            return ret;
        }

        public int sobreescribir(String nombre)
        {
            int pos = 0;
            foreach(TS hijo in actualc.elementos)
            {
                if (hijo.nombre.Equals(nombre) && hijo.Tipo > 5)
                {
                    return pos;
                }
                pos++;
            }
            return -1;
        }
    }

    


}
