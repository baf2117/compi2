using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using System.IO;


namespace C3D
{
    class Sintactico1: Grammar
    {

    public static ParseTreeNode analizartree(String cadena)
    {

        Gramatica gramatica = new Gramatica();
        LanguageData lenguaje = new LanguageData(gramatica);
        Parser parse = new Parser(lenguaje);
        ParseTree arbol = parse.Parse(cadena);
        ParseTreeNode raiz = arbol.Root;

        if (raiz == null)
        {
            return arbol.Root;

        }
        generarImagen(raiz);
        TS ts = new TS();
        ts.recolectar(raiz);
        return raiz;
    }

        public static ParseTreeNode analizartree2(String cadena)
        {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parse = new Parser(lenguaje);
            ParseTree arbol = parse.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;

            if (raiz == null)
            {
                return arbol.Root;

            }
            generarImagen(raiz);
            
            return raiz;
        }

        public static ParseTreeNode analizarolc(String cadena)
        {
            Gramaticao gramatica = new Gramaticao();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parse = new Parser(lenguaje);
            ParseTree arbol = parse.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;

            if (raiz == null)
            {
                return arbol.Root;

            }
            generarImagen(raiz);
            TS ts = new TS();            
            ts.recolectarolc(raiz);
            return raiz;
        }

        public static ParseTreeNode analizarolc2(String cadena)
        {
            Gramaticao gramatica = new Gramaticao();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parse = new Parser(lenguaje);
            ParseTree arbol = parse.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;

            if (raiz == null)
            {
                return arbol.Root;

            }
            generarImagen(raiz);
            return raiz;
        }

        public static void generarImagen(ParseTreeNode raiz)
    {
        String grafica = Controldot.getDot(raiz);
        System.IO.File.Delete("C:\\Users\\Brayan\\Desktop\\graph.jpg");

        var filename = "C:\\Users\\Brayan\\Desktop\\graph.txt";

        TextWriter tw = new StreamWriter(filename);
        tw.WriteLine(grafica);
        tw.Close();

        String path = Directory.GetCurrentDirectory();
            




        try
        {

            var command = "dot -Tjpg C:\\Users\\Brayan\\Desktop\\graph.txt -o C:\\Users\\Brayan\\Desktop\\graph.jpg";
            var procStarInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/C" + command);
            var proc = new System.Diagnostics.Process();
            proc.StartInfo = procStarInfo;
            proc.Start();
            proc.WaitForExit();


        }
        catch (Exception x) { }

    }

        public static void generarImagen2(int numero)
        {
            String grafica = diagrama();
            System.IO.File.Delete("C:\\Users\\Brayan\\Desktop\\clase"+numero+".jpg");

            var filename = "C:\\Users\\Brayan\\Desktop\\clase.txt";

            TextWriter tw = new StreamWriter(filename);
            tw.WriteLine(grafica);
            tw.Close();

            String path = Directory.GetCurrentDirectory();





            try
            {

                var command = "dot -Tjpg C:\\Users\\Brayan\\Desktop\\clase.txt -o C:\\Users\\Brayan\\Desktop\\clase" + numero +".jpg";
                var procStarInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/C" + command);
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStarInfo;
                proc.Start();
                proc.WaitForExit();


            }
            catch (Exception x) { }

        }


        public static String diagrama()
        {
            String cadena = "digraph g {\ngraph[\nrankdir = \"LR\"\n]; ";
            cadena += "node [\nfontsize = \"16\"\nshape = \"ellipse\"\n]; ";
            cadena += "edge [\n]; ";
            String visi = "+";

            foreach(clasesuml hijo in clasesuml.Lista)
            {

                cadena += "\n\"" + hijo.Nombre + "\" [\nlabel = \"<f0>" + hijo.Nombre + "|<f1>";
                foreach(atributosuml hijo2 in hijo.atributos)
                {
                    
                    if (hijo2.tipo == 1)
                    {
                        switch (hijo2.visibilidad)
                        {
                            case "publico":
                                visi = "+";
                                break;
                            case "privado":
                                visi = "-";
                                break;
                            default:
                                visi = "#";
                                break;
                        }
                        cadena +=visi +""+hijo2.Nombre+ " : "+hijo2.tipo2+"\\n";

                    }

                }
                cadena += "|<f2>";

                foreach (atributosuml hijo2 in hijo.atributos)
                {

                    if (hijo2.tipo != 1)
                    {
                        switch (hijo2.visibilidad)
                        {
                            case "publico":
                                visi = "+";
                                break;
                            case "privado":
                                visi = "-";
                                break;
                            default:
                                visi = "#";
                                break;
                        }
                        cadena += visi + "" + hijo2.Nombre + "() : " + hijo2.tipo2+"\\n";

                    }

                }
                cadena += "\"\nshape = \"record\"];" ;

            }

            foreach(Relaciones rel in clasesuml.Relaciones)
            {
                switch (rel.tipo)
                {

                    case 1:
                        cadena += rel.clasea + "->" + rel.claseb + "[arrowhead=onormal ];\n";
                        break;
                    case 2:
                        cadena += rel.clasea + "->" + rel.claseb + "[arrowhead=odiamond ];\n";
                        break;
                    case 3:
                        cadena += rel.clasea + "->" + rel.claseb + "[arrowhead=diamond ];\n";
                        break;
                    case 4:
                        cadena += rel.clasea + "->" + rel.claseb + "[arrowhead=none label =\" > \"];\n";
                        
                        break;
                    default:
                        cadena += rel.clasea + "->" + rel.claseb + "[arrowhead=onormal ];\n";
                        break;



                }

            }
            cadena += "\n}";
            return cadena;
        }
    }

    
    }

