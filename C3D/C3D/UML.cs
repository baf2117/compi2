using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;
using System.Collections;
using Irony.Parsing;

namespace C3D
{
    public partial class UML : Form
    {
        TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        TextStyle OrangeStyle = new TextStyle(Brushes.Orange, null, FontStyle.Italic);
        TextStyle PurpleStyle = new TextStyle(Brushes.Purple, null, FontStyle.Italic);
        TextStyle MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));
        public static int relacion = 0;
        public static int imagen2 = 1;
        public UML()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            

        }

        private void venta_TextChanged(object sender, TextChangedEventArgs e)
        {
            CSharpSyntaxHighlight(e);
        }

        private void CSharpSyntaxHighlight(TextChangedEventArgs e)
        {
            venta.LeftBracket = '(';
            venta.RightBracket = ')';
            venta.LeftBracket2 = '\x0';
            venta.RightBracket2 = '\x0';
            //clear style of changed range
            e.ChangedRange.ClearStyle(BlueStyle, BoldStyle, GrayStyle, MagentaStyle, GreenStyle, BrownStyle, OrangeStyle);

            //string highlighting
            e.ChangedRange.SetStyle(OrangeStyle, @"""""|@""""|''|"".*""|'.?'");
            //comment highlighting
            e.ChangedRange.SetStyle(GrayStyle, @"(/-[^-]*-/)|({--[^-]*--})", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(GrayStyle, @"(##.*)|(//.*)", RegexOptions.Singleline);

            //number highlighting
            // e.ChangedRange.SetStyle(MagentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            e.ChangedRange.SetStyle(PurpleStyle, @"\b\d+[\.]?\d*");
            e.ChangedRange.SetStyle(BlueStyle, @"\b(clase|llamar|hereda_de|entero|boolean|true|false|publico|privado|protegido|metodo|nuevo|principal|imprimir|importar|self|este|#region\b|#endregion\b)");

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();

            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks

        }

        private void OLC(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Agregarclase vistauml = new Agregarclase();
            vistauml.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            relacion = 1;
            Arelaciones n = new Arelaciones();
            n.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            relacion = 2;
            Arelaciones n = new Arelaciones();
            n.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            relacion = 3;
            Arelaciones n = new Arelaciones();
            n.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            relacion = 4;
            Arelaciones n = new Arelaciones();
            n.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            relacion = 5;
            Arelaciones n = new Arelaciones();
            n.Show();
        }

        private void Codigo_Click(object sender, EventArgs e)
        {
            String clase = clases.Text;
            String tipo = Lenguaje.Text;

            if (clase.Equals("") | tipo.Equals(""))
            {
                MessageBox.Show("No ha definido la clase o el tipo de lenguaje");
                return;
            }

            clasesuml nueva = new clasesuml("",null);
            foreach(clasesuml hijo in clasesuml.Lista)
            {
                if (hijo.Nombre.Equals(clase))
                {
                    nueva = hijo;
                    goto Inicio;
                }

            }

            Inicio:
            switch (tipo)
            {
                case "OLC":
                    olc(nueva);
                    break;



                case "Tree":
                    tree(nueva);
                    break;

            }



        }

        private void olc(clasesuml clase)
        {
            venta.Text = "";
            venta.Text += "clase "+clase.Nombre;
            foreach (Relaciones rel in clasesuml.Relaciones)
            {

                if (rel.clasea.Equals(clase.Nombre) && rel.tipo == 1)
                {
                    venta.Text += " hereda_de " + rel.claseb + "{\n";
                    goto siguiente;
                }
            }

            venta.Text +=  "{\n";

            siguiente:
            foreach (atributosuml hijo in clase.atributos)
            {
                if (hijo.tipo == 1) {
                    venta.Text += hijo.visibilidad+" "+ hijo.tipo2+" "+hijo.Nombre+";\n";
                }
            }
           
            int rel1 = 1;
            LinkedList<String> comp = new LinkedList<String>();
            LinkedList<String> agre = new LinkedList<String>();
            LinkedList<String> agre2 = new LinkedList<String>();
            foreach (Relaciones rel in clasesuml.Relaciones)
            {

                if (rel.clasea.Equals(clase.Nombre)&&rel.tipo==4)
                {

                    venta.Text += "privado " + rel.claseb + " relacion"+rel1+"[0];\n";
                    rel1++;
                }
                if (rel.claseb.Equals(clase.Nombre) && rel.tipo == 4)
                {

                    venta.Text += "privado " + rel.clasea + " relacion" + rel1 + "= nuevo " + rel.clasea+"();\n";
                    rel1++;
                }

                if (rel.claseb.Equals(clase.Nombre) && rel.tipo == 3)
                {

                    venta.Text += "privado " + rel.clasea + " relacion" + rel1 + ";\n";
                    comp.AddLast("relacion" + rel1);
                    rel1++;
                }

                if (rel.claseb.Equals(clase.Nombre) && rel.tipo == 2)
                {

                    venta.Text += "privado " + rel.clasea + " relacion" + rel1+";\n";
                    agre.AddLast(rel.clasea+" relacion" + rel1);
                    agre2.AddLast("este.relacion"+rel1+" = relacion"+rel1+";\n");
                    rel1++;
                }

            }
            //constructores
            venta.Text += "\n " + clase.Nombre + "(";
                
            for(int i =0; i < agre.Count; i++)
            {
                if (i == 0)
                {
                    venta.Text += agre.ElementAt(i);
                }
                else
                {
                    venta.Text += ","+ agre.ElementAt(i);

                }
                

            }
    
            venta.Text += "){\n";
            for (int i = 0; i < agre.Count; i++)
            {
                
                    venta.Text += agre2.ElementAt(i);
               


            }

            int posc = 0;
            foreach (Relaciones rel in clasesuml.Relaciones)
            {

                if (rel.claseb.Equals(clase.Nombre) && rel.tipo == 3)
                {

                    venta.Text += "este." +comp.ElementAt(posc)  + "= nuevo "+rel.clasea+"();\n";
                    posc++;
                }


            }



            venta.Text += "}\n\n";
            if (agre.Count != 0)
            {
                venta.Text += "\n" + clase.Nombre + "(";
                venta.Text += "){\n\n}\n\n";
            }
            LinkedList<String> para = new LinkedList<string>();
            LinkedList<String> tpara = new LinkedList<string>();
            foreach (atributosuml hijo in clase.atributos)
            {
                if (hijo.tipo != 1)
                {
                   
                    venta.Text += hijo.visibilidad + " " + hijo.tipo2 + " " + hijo.Nombre + "(";
                    int tama = hijo.parametros.Count;
                    foreach(String hijo1 in hijo.parametros)
                    {
                        para.AddLast(hijo1);

                    }
                    foreach (String hijo1 in hijo.tipopara)
                    {
                        tpara.AddLast(hijo1);

                    }
                    for (int i = 0; i < tama; i++)
                    {
                        if (i == 0)
                        {
                            venta.Text += tpara.ElementAt(i) + " "+para.ElementAt(i)+" ";
                        }
                        else
                        {
                            venta.Text += tpara.ElementAt(i) + "," + para.ElementAt(i) + " ";
                        }
                    }

                    venta.Text += "){\n}\n";
                }
            }
            venta.Text += "}";
        }


        private void tree(clasesuml clase)
        {
            venta.Text = "";
            venta.Text += "clase " + clase.Nombre;
            foreach (Relaciones rel in clasesuml.Relaciones)
            {

                if (rel.clasea.Equals(clase.Nombre) && rel.tipo == 1)
                {
                    venta.Text += "[" + rel.claseb + "]:\n";
                    goto siguiente;
                }
            }

            venta.Text += "[]:\n";
            siguiente:
            foreach (atributosuml hijo in clase.atributos)
            {
                if (hijo.tipo == 1)
                {
                    venta.Text += "\t"+hijo.visibilidad + " " + hijo.tipo2 + " " + hijo.Nombre + "\n";
                }
            }

            int rel1 = 1;
            LinkedList<String> comp = new LinkedList<String>();
            LinkedList<String> agre = new LinkedList<String>();
            LinkedList<String> agre2 = new LinkedList<String>();
            foreach (Relaciones rel in clasesuml.Relaciones)
            {

                if (rel.clasea.Equals(clase.Nombre) && rel.tipo == 4)
                {

                    venta.Text += "\t" +"privado " + rel.claseb + " relacion" + rel1 + "[0]\n";
                    rel1++;
                }
                if (rel.claseb.Equals(clase.Nombre) && rel.tipo == 4)
                {

                    venta.Text += "\t" + "privado " + rel.clasea + " relacion" + rel1 + " => nuevo " + rel.clasea + "[]\n";
                    rel1++;
                }

                if (rel.claseb.Equals(clase.Nombre) && rel.tipo == 3)
                {

                    venta.Text += "\t" + "privado " + rel.clasea + " relacion" + rel1 + "\n";
                    comp.AddLast("relacion" + rel1);
                    rel1++;
                }

                if (rel.claseb.Equals(clase.Nombre) && rel.tipo == 2)
                {

                    venta.Text += "\t" + "privado " + rel.clasea + " relacion" + rel1 + "\n";
                    agre.AddLast(rel.clasea + " relacion" + rel1);
                    agre2.AddLast("\t" + "\t" + "self.relacion" + rel1 + " => relacion" + rel1 + "\n");
                    rel1++;
                }

            }
            //constructores
            venta.Text += "\n\t" + "__constructor " + "[";

            for (int i = 0; i < agre.Count; i++)
            {
                if (i == 0)
                {
                    venta.Text += agre.ElementAt(i);
                }
                else
                {
                    venta.Text += "," + agre.ElementAt(i);

                }


            }

            venta.Text += "]:\n";
            for (int i = 0; i < agre.Count; i++)
            {

                venta.Text += agre2.ElementAt(i);



            }

            int posc = 0;
            foreach (Relaciones rel in clasesuml.Relaciones)
            {

                if (rel.claseb.Equals(clase.Nombre) && rel.tipo == 3)
                {

                    venta.Text += "\t" + "\t" + "self." + comp.ElementAt(posc) + "=> nuevo " + rel.clasea + "[]\n";
                    posc++;
                }


            }
            if (agre.Count != 0)
            {
                venta.Text += "\n\t" + "__constructor " + "[]:";
            }

            venta.Text += "\n\n";
            LinkedList<String> para = new LinkedList<string>();
            LinkedList<String> tpara = new LinkedList<string>();
            foreach (atributosuml hijo in clase.atributos)
            {
                if (hijo.tipo != 1)
                {
                    if (hijo.tipo == 2)
                    {
                        venta.Text += "\t" + hijo.visibilidad + " metodo " + hijo.tipo2 + " " + hijo.Nombre + "[";
                    }
                    else
                    {
                        venta.Text += "\t" + hijo.visibilidad + " funcion " + hijo.tipo2 + " " + hijo.Nombre + "[";
                    }
                    
                    int tama = hijo.parametros.Count;
                    foreach (String hijo1 in hijo.parametros)
                    {
                        para.AddLast(hijo1);

                    }
                    foreach (String hijo1 in hijo.tipopara)
                    {
                        tpara.AddLast(hijo1);

                    }
                    for (int i = 0; i < tama; i++)
                    {
                        if (i == 0)
                        {
                            venta.Text += tpara.ElementAt(i) + " " + para.ElementAt(i) + " ";
                        }
                        else
                        {
                            venta.Text += tpara.ElementAt(i) + "," + para.ElementAt(i) + " ";
                        }
                    }

                    venta.Text += "]:\n\n";
                }
            }
          

        }

        private void imagen_Click(object sender, EventArgs e)
        {
            Sintactico1.generarImagen2(imagen2);
            
            pictureBox1.Image = Image.FromFile("C:\\Users\\Brayan\\Desktop\\clase" + imagen2 + ".jpg");
            imagen2++;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            String tipol = Lenguaje.Text;
            ParseTreeNode raiz;
            switch (tipol)
            {
                case "OLC":
                    raiz = Sintactico1.analizarolc2(venta.Text);
                    agregarolc(raiz);
                    break;
                case "Tree":
                    raiz = Sintactico1.analizartree2(venta.Text);
                    agregartree(raiz);
                    break;


            }
        }

        private void agregarolc(ParseTreeNode raiz)
        {
            if (raiz == null)
                return;
            raiz = raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0);
            ArrayList rela = new ArrayList();
            String nombre = raiz.ChildNodes.ElementAt(0).Token.Text;
            clasesuml nueva = new clasesuml(nombre,null);
            foreach(clasesuml hijo in clasesuml.Lista)
            {
                if (hijo.Nombre.Equals(nombre)){

                    MessageBox.Show("Ya existe una clase con el nombre "+nombre);
                    return;
                }

            }

            
            ParseTreeNode cuerpo = raiz.ChildNodes.ElementAt(2);
            ParseTreeNode auxn;
            ArrayList atributos1 = new ArrayList();
            ArrayList relac = new ArrayList();
            atributosuml auxa;
            String visi = "publico";
            foreach(ParseTreeNode nodo in cuerpo.ChildNodes)
            {
                auxa = new atributosuml();

                switch (nodo.Term.Name)
                {

                    case "DECGF":
                        if (nodo.ChildNodes.ElementAt(3).ChildNodes.ElementAt(0).Term.Name.Equals("DECG"))
                        {
                            visi = nodo.ChildNodes.ElementAt(0).Token.Text;
                            if (!nodo.ChildNodes.ElementAt(1).Term.Name.Equals("id"))
                            {

                                auxa.visibilidad = visi;
                                auxa.tipo = 1;
                                auxa.tipo2 = nodo.ChildNodes.ElementAt(1).Token.Text;
                                auxa.Nombre = nodo.ChildNodes.ElementAt(2).Token.Text;
                                atributos1.Add(auxa);
                            }
                            else
                            {
                                auxn = nodo.ChildNodes.ElementAt(3);
                                if (auxn.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).ChildNodes.Count>0)
                                {
                                    Relaciones relb = new Relaciones();
                                    relb.tipo = 4;
                                    relb.clasea = nombre;
                                    relb.claseb = nodo.ChildNodes.ElementAt(1).Token.Text;
                                    relac.Add(relb);

                                }else if(auxn.ChildNodes.ElementAt(0).ChildNodes.ElementAt(1).ChildNodes.Count>0)
                                {
                                    Relaciones relb = new Relaciones();
                                    relb.tipo = 4;
                                    relb.claseb = nombre;
                                    relb.clasea = nodo.ChildNodes.ElementAt(1).Token.Text;
                                    relac.Add(relb);

                                }

                            }

                        }else
                        {
                            auxa.tipo = 3;
                            auxa.tipo2 = nodo.ChildNodes.ElementAt(1).Token.Text;
                            auxa.visibilidad = nodo.ChildNodes.ElementAt(0).Token.Text;
                            auxa.Nombre = nodo.ChildNodes.ElementAt(2).Token.Text;
                            atributos1.Add(auxa);

                        }
                        break;
                    case "METODO":
                        auxa.tipo = 2;
                        auxa.tipo2 = "metodo";
                        auxa.visibilidad = nodo.ChildNodes.ElementAt(0).Token.Text;
                        auxa.Nombre = nodo.ChildNodes.ElementAt(1).Token.Text;
                        atributos1.Add(auxa);
                        break;
                    case "CONSTRUCTOR":
                        if (nodo.ChildNodes.Count > 1) {
                            ParseTreeNode parametros = nodo.ChildNodes.ElementAt(1);
                            ParseTreeNode bloque = nodo.ChildNodes.ElementAt(1);
                            if (parametros.Term.Name.Equals("PARAMETROS"))
                            {
                                foreach(ParseTreeNode para in parametros.ChildNodes)
                                {
                                    Relaciones relb = new Relaciones();
                                    relb.tipo = 2;
                                    relb.claseb = nombre;
                                    relb.clasea = para.ChildNodes.ElementAt(0).Token.Text;
                                    relac.Add(relb);

                                }

                                bloque = nodo.ChildNodes.ElementAt(2);

                            }
                            else
                            {

                                bloque = nodo.ChildNodes.ElementAt(1);
                            }

                            foreach(ParseTreeNode bloquesito in bloque.ChildNodes)
                            {

                                if (bloquesito.ChildNodes.ElementAt(1).ChildNodes.Count > 1)
                                {
                                    Relaciones relb = new Relaciones();
                                    relb.tipo = 3;
                                    relb.claseb = nombre;
                                    relb.clasea = bloquesito.ChildNodes.ElementAt(1).ChildNodes.ElementAt(1).Token.Text;
                                    relac.Add(relb);


                                }
                            }
                                
                                
                                }
                        break;

                }




            }



            if (raiz.ChildNodes.ElementAt(1).ChildNodes.Count != 0)
            {
                String hereda = raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text;
                foreach (clasesuml hijo in clasesuml.Lista)
                {
                    if (hijo.Nombre.Equals(hereda))
                    {
                        Relaciones rel = new Relaciones();
                        rel.tipo = 1;
                        rel.clasea = nombre;
                        rel.claseb = hereda;
                        clasesuml.Relaciones.Add(rel);
                        goto siguiente;
                    }

                }
                MessageBox.Show("No existe la clase de la que se desea heredar: " + hereda);
                return;

            }

            siguiente:
            foreach(Relaciones relach in relac)
            {
                clasesuml.Relaciones.Add(relach);

            }
            nueva.atributos = atributos1;
            clasesuml.Lista.Add(nueva);
            clases.Items.Add(nueva.Nombre);

        }

        private void agregartree(ParseTreeNode raiz)
        {
            if (raiz == null)
                return;
            raiz = raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0);
            ArrayList rela = new ArrayList();
            String nombre = raiz.ChildNodes.ElementAt(0).Token.Text;
            clasesuml nueva = new clasesuml(nombre, null);
            foreach (clasesuml hijo in clasesuml.Lista)
            {
                if (hijo.Nombre.Equals(nombre))
                {

                    MessageBox.Show("Ya existe una clase con el nombre " + nombre);
                    return;
                }

            }


            ParseTreeNode cuerpo = raiz.ChildNodes.ElementAt(4);
            ParseTreeNode auxn;
            ArrayList atributos1 = new ArrayList();
            ArrayList relac = new ArrayList();
            atributosuml auxa;
            String visi = "publico";
            foreach (ParseTreeNode nodo in cuerpo.ChildNodes)
            {
                auxa = new atributosuml();

                switch (nodo.Term.Name)
                {

                    case "DECG":
                          visi = nodo.ChildNodes.ElementAt(0).Token.Text;
                            if (!nodo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Term.Name.Equals("id"))
                            {

                                auxa.visibilidad = visi;
                                auxa.tipo = 1;
                                auxa.tipo2 = nodo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text;
                                auxa.Nombre = nodo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).Token.Text;
                                atributos1.Add(auxa);
                            }
                            else
                            {
                                auxn = nodo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(1);
                                if (auxn.ChildNodes.Count > 1)
                                {
                                    Relaciones relb = new Relaciones();
                                    relb.tipo = 4;
                                    relb.clasea = nombre;
                                    relb.claseb = nodo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text;
                                    relac.Add(relb);

                                }
                                else if (nodo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(2).ChildNodes.Count > 0)
                                {
                                    Relaciones relb = new Relaciones();
                                    relb.tipo = 4;
                                    relb.claseb = nombre;
                                    relb.clasea = nodo.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text;
                                    relac.Add(relb);

                                }

                            }

                       
                        break;
                    case "METODO":
                        auxa.tipo = 2;
                        auxa.tipo2 = "metodo";
                        auxa.visibilidad = nodo.ChildNodes.ElementAt(0).Token.Text;
                        auxa.Nombre = nodo.ChildNodes.ElementAt(1).Token.Text;
                        atributos1.Add(auxa);
                        break;
                    case "FUNCION":
                        auxa.tipo = 3;
                        auxa.tipo2 = nodo.ChildNodes.ElementAt(1).Token.Text;
                        auxa.visibilidad = nodo.ChildNodes.ElementAt(0).Token.Text;
                        auxa.Nombre = nodo.ChildNodes.ElementAt(2).Token.Text;
                        atributos1.Add(auxa);
                        break;
                    case "CONSTRUCTOR":
                        if (nodo.ChildNodes.Count > 2)
                        {
                            ParseTreeNode parametros = nodo.ChildNodes.ElementAt(1);
                            ParseTreeNode bloque = nodo.ChildNodes.ElementAt(1);
                            if (parametros.Term.Name.Equals("PARAMETROS"))
                            {
                                foreach (ParseTreeNode para in parametros.ChildNodes)
                                {
                                    Relaciones relb = new Relaciones();
                                    relb.tipo = 2;
                                    relb.claseb = nombre;
                                    relb.clasea = para.ChildNodes.ElementAt(0).Token.Text;
                                    relac.Add(relb);

                                }

                                bloque = nodo.ChildNodes.ElementAt(3);

                            }
                            else
                            {

                                bloque = nodo.ChildNodes.ElementAt(2);
                            }

                            foreach (ParseTreeNode bloquesito in bloque.ChildNodes)
                            {

                                if (bloquesito.ChildNodes.ElementAt(1).ChildNodes.Count > 1)
                                {
                                    Relaciones relb = new Relaciones();
                                    relb.tipo = 3;
                                    relb.claseb = nombre;
                                    relb.clasea = bloquesito.ChildNodes.ElementAt(1).ChildNodes.ElementAt(1).Token.Text;
                                    relac.Add(relb);


                                }
                            }


                        }
                        break;

                }




            }



            if (raiz.ChildNodes.ElementAt(2).ChildNodes.Count != 0)
            {
                String hereda = raiz.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text;
                foreach (clasesuml hijo in clasesuml.Lista)
                {
                    if (hijo.Nombre.Equals(hereda))
                    {
                        Relaciones rel = new Relaciones();
                        rel.tipo = 1;
                        rel.clasea = nombre;
                        rel.claseb = hereda;
                        clasesuml.Relaciones.Add(rel);
                        goto siguiente;
                    }

                }
                MessageBox.Show("No existe la clase de la que se desea heredar: " + hereda);
                return;

            }

            siguiente:
            foreach (Relaciones relach in relac)
            {
                clasesuml.Relaciones.Add(relach);

            }
            nueva.atributos = atributos1;
            clasesuml.Lista.Add(nueva);
            clases.Items.Add(nueva.Nombre);

        }
    }
}
