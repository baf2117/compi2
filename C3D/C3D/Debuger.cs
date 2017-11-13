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
using System.Threading;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;

namespace C3D
{
    public partial class Debuger : Form
    {

        public static  Thread debug;
        public LinkedList<ParseTreeNode> raiz;
        TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        TextStyle OrangeStyle = new TextStyle(Brushes.Orange, null, FontStyle.Italic);
        TextStyle PurpleStyle = new TextStyle(Brushes.Purple, null, FontStyle.Italic);
        Boolean banderaret = false;
        Boolean banderalinea = false;
        Boolean banderaeje = false;
        Boolean fin = false;
        int pos = 0;
        int linea;
        


        MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        public Debuger(LinkedList<ParseTreeNode> raiz,String texto)
        {
            
            InitializeComponent();
            Ejecucion3d.linea = raiz;
            consola.Text = texto;
            this.raiz = raiz;
            debug = new Thread(new ThreadStart(ejecutardeb));
            
            debug.Start();
            
            dibujarTS();
            
        }

        private void fastColoredTextBox1_Load(object sender, EventArgs e)
        {

        }

        private void fastColoredTextBox1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
          CSharpSyntaxHighlight(e);
        }

        public  void ejecutardeb()
        {


            Ejecucion3d ejec = new Ejecucion3d();
            String nombre = raiz.ElementAt(0).Term.Name;
            pos = 0;
            ParseTreeNode accion = raiz.ElementAt(pos);

            while (!nombre.Equals("fin"))
            {

                switch (nombre)
                {

                    case "ASIGNACIONT":
                        ejec.Asignartempo(accion);
                        pos++;
                        break;
                    case "ASIGNAHP":
                        ejec.asignahp(accion);
                        pos++;
                        break;
                    case "ASIGNASP":
                        ejec.asignasp(accion);
                        pos++;
                        break;
                    case "IF":
                        pos = ejec.IFF(accion, pos);
                        break;
                    case "IFFALSE":
                        pos = ejec.IFFA(accion, pos);
                        break;
                    case "ASIGNASEL2":
                        //     ejec.asignaselfp2(accion);
                        pos++;
                        break;
                    case "ASIGNASEL1":
                        ejec.asignaselfp(accion);
                        pos++;
                        break;
                    case "ASIGNADS":
                        //    ejec.asignads(accion);
                        pos++;
                        break;
                    case "ASIGNASTACK":
                        ejec.asignastack(accion);
                        pos++;
                        break;
                    case "ASIGNAHEAP":
                        ejec.asignaheap(accion);
                        pos++;
                        break;
                    case "LLAMADA":
                        pos = ejec.llamada(accion.ChildNodes.ElementAt(0).Token.Text, pos + 1);
                        pos++;
                        break;
                    case "GOTO":
                        pos = ejec.gotoo(accion.ChildNodes.ElementAt(1).Token.Text);
                        pos++;
                        break;

                    case "IMPRIMIR":
                        ejec.imprimir(accion.ChildNodes.ElementAt(3));
                        pos++;//por el momento
                        break;

                    case "RETORNO":
                        pos = ejec.regreso();
                        banderaret = false;
                        break;

                    default:
                        pos++;
                        break;


                }
                if (pos == raiz.Count)
                {
                    fin = true;
                    goto fin;

                }
                accion = raiz.ElementAt(pos);
                nombre = accion.Term.Name;
                Range rango = new Range(consola, pos);
                consola.Selection = rango;
                consola.SelectionColor = System.Drawing.Color.Black;
                if (banderalinea && linea == pos)
                {
                    banderalinea = false;

                }
                if (banderaeje && linea == pos)
                {
                    banderaeje = false;
                    debug.Resume();
                }

                if (banderaeje)
                {
                    Thread.Sleep(1000);
                    dibujarStack();
                    dibujarHeap();
                    dibujarPila();
                }


                if (!banderaret && !banderalinea && !banderaeje)
                {
                    debug.Suspend();
                }


            }
            fin:;
        }

        private void Ejecutar_Click(object sender, EventArgs e)
        {
            if (!fin)
            {
                debug.Resume();
                dibujarStack();
                dibujarHeap();
                dibujarPila();
            }

        }

        public void CSharpSyntaxHighlight(TextChangedEventArgs e)
        {
            consola.LeftBracket = '(';
            consola.RightBracket = ')';
            consola.LeftBracket2 = '\x0';
            consola.RightBracket2 = '\x0';
            //clear style of changed range
            e.ChangedRange.ClearStyle(BlueStyle, GrayStyle, OrangeStyle);

            //string highlighting
            e.ChangedRange.SetStyle(OrangeStyle, @"""""|@""""|''|"".*""|'.?'");
            //comment highlighting
            e.ChangedRange.SetStyle(GrayStyle, @"(/-[^-]*-/)|({--[^-]*--})", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(GrayStyle, @"(##.*)|(//.*)", RegexOptions.Singleline);

            //number highlighting
            // e.ChangedRange.SetStyle(MagentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            e.ChangedRange.SetStyle(PurpleStyle, @"\b\d+[\.]?\d*");
            e.ChangedRange.SetStyle(BlueStyle, @"\bt\d+");
            e.ChangedRange.SetStyle(BlueStyle, @"\bL\d+");
            e.ChangedRange.SetStyle(BlueStyle, @"\b(iffalse|goto|then|selfp|heap|stack|hp|sp|clase|llamar|hereda_de|entero|boolean|true|false|publico|privado|protegido|metodo|nuevo|principal|imprimir|importar|self|este|#region\b|#endregion\b)");

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();

            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!fin)
            {
                banderaret = true;
                debug.Resume();
                dibujarStack();
                dibujarHeap();
                dibujarPila();
            }
        }

        private void Tabla_Paint(object sender, PaintEventArgs e)
        {

        }

        public void agregarfila(String id,String tipo,String ambito, String posicion, String peso)
        {


            RowStyle temp = TablaS.RowStyles[0];           
            TablaS.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
            TablaS.Controls.Add(new Label() { Text = id }, 0, TablaS.RowCount-2);
            TablaS.Controls.Add(new Label() { Text = tipo }, 1, TablaS.RowCount-2);
            TablaS.Controls.Add(new Label() { Text = ambito }, 2, TablaS.RowCount-2);
            TablaS.Controls.Add(new Label() { Text = posicion }, 3, TablaS.RowCount-2);
            TablaS.Controls.Add(new Label() { Text = peso}, 4, TablaS.RowCount-2);
            TablaS.RowCount++;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!fin)
            {
                banderalinea = true;
                linea = Convert.ToInt32(lineas.Text) - 1;
                debug.Resume();
                dibujarStack();
                dibujarHeap();
                dibujarPila();
            }
        }

        public void dibujarTS()
        {
            agregarfila("Id", "Tipo", "Ambito", "Posicion", "Peso");
            foreach(TS clase in TS.TablaSimbolos)
            {
                agregarfila(clase.nombre, "Clase","--","--",clase.peso+"");

                foreach (TS hijo in clase.elementos)
                {
                    String tipo = "";
                    switch (hijo.Tipo)
                    {
                        //1 = clase, 2 = parametro , 3 = objeto , 4 = variable, atributo = 5 , metodo = 6 , funcion = 7,constructor = 8 , retorno = 9
                        case 2:
                        case 4:
                        case 3:
                        case 5:
                           
                            switch (hijo.tipo2)
                            {// 1 = integer  , 2 = string , 3 = booleano , 4 = decimal , 0 = void, 5 = clase, objeto = 6 ;
                                case 1:
                                    tipo = "Entero";
                                    break;
                                case 2:
                                    tipo = "Cadena";
                                    break;
                                case 3:
                                    tipo = "Booleano";
                                    break;
                                case 4:
                                    
                                    tipo = "Decimal";
                                    break;
                                case 6:
                                    tipo = clase.etiqueta;
                                    break;

                            }
                            agregarfila(hijo.nombre, tipo, clase.nombre, hijo.posicion + "", hijo.peso + "");
                            break;
                        case 6:
                        case 7:
                        case 8:
                            tipo = "procedimiento";
                            agregarfila(hijo.nombre, tipo, clase.nombre,"--" , hijo.peso + "");
                            String tipo2="";
                            foreach(TS hijo2 in hijo.elementos)
                            {
                                switch (hijo2.tipo2)
                                {// 1 = integer  , 2 = string , 3 = booleano , 4 = decimal , 0 = void, 5 = clase, objeto = 6 ;
                                    case 1:
                                        tipo2 = "Entero";
                                        break;
                                    case 2:
                                        tipo2 = "Cadena";
                                        break;
                                    case 3:
                                        tipo2 = "Booleano";
                                        break;
                                    case 4:
                                        
                                        tipo2 = "Decimal";
                                        break;
                                    case 6:
                                        tipo2 = clase.etiqueta;
                                        break;
                                    case 9:
                                        tipo2 = "Retorno";
                                        break;


                                }
                                agregarfila(hijo2.nombre, tipo2, hijo.nombre, hijo2.posicion + "", "1");
                                }
                            
                            break;
                       
                            

                    }

                  
                }


                
            }
            for (int i = 1; i <= raiz.Count; i++)
            {

                lineas.Items.Add(i);

            }

        }

        public void dibujarStack()
        {
            String heaps = "";
            try
            {
            textBox1.Text = Ejecucion3d.sp+"";
            textBox2.Text = Ejecucion3d.hp + "";
            textBox3.Text = Ejecucion3d.selfp + "";


            
           
                foreach (Ejecucion3d pilas in Ejecucion3d.stack)
                {
                    heaps += pilas.valor + "\r\n";
                }
                Stack.Text = heaps;
            }
            catch(Exception e)
            {

            }
           
        }

        public void dibujarHeap()
        {
            String heaps = "";

            try
            {
                foreach (Ejecucion3d pilas in Ejecucion3d.heap)
                {
                    heaps += pilas.valor + "\r\n";

                }
                Heap.Text = heaps;
            }
            catch(Exception e)
            {

            }
            
        }
     
        public void dibujarPila()
        {
            String pilac = "";
            try
            {
                foreach (Ejecucion3d pilas in Ejecucion3d.Pila)
                {
                    pilac += pilas.nombre + "   |   " + pilas.valor + "\r\n";

                }
                Pila.Text = pilac;
            }
            catch(Exception e)
            {

            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!fin)
            {
                banderaeje = true;
                if (lineas.Text.Equals(""))
                {
                    linea = raiz.Count();
                }
                else
                {
                    linea = Convert.ToInt32(lineas.Text) - 1;
                }
                debug.Resume();
                dibujarStack();
                dibujarHeap();
                dibujarPila();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            banderaeje = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!fin)
            {
                banderalinea = true;
                linea = pos + 1;
                debug.Resume();
                dibujarStack();
                dibujarHeap();
                dibujarPila();
            }
        }
       private async Task PutTaskDelay()
        {
            await Task.Delay(1000);
        }
    }
}
