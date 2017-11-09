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
using System.IO;
using System.Collections;
using Irony.Parsing;

namespace C3D
{
    public partial class Form1 : Form
    {   public static int index = 6;
        public static bool bandera = false;
        TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);        
        TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        TextStyle OrangeStyle = new TextStyle(Brushes.Orange, null, FontStyle.Italic);
        TextStyle PurpleStyle = new TextStyle(Brushes.Purple, null, FontStyle.Italic);
     
        MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        public Form1()
        {
            InitializeComponent();
            Program.ventana = this;
        }

        private void Ejecutar_Click(object sender, EventArgs e)
        {
            TabPage tabPagex = tabControl1.SelectedTab;
            Consola.Text = "";
            foreach (FastColoredTextBox hijo in tabPagex.Controls)
            {
                String contenido = hijo.Text;
                // contenido = contenido.Replace("\\", "~");
                //contenido = contenido.Replace("\r\n", "&");
                //contenido = contenido.Replace("\n", "&");
                //contenido = contenido.ToLower();
                 TS ts = new TS();
                 ts.inicio3d();
                Ejecucion3d.Pila.Clear();
                Ejecucion3d.stack.Clear();
                Ejecucion3d.heap.Clear();
                Ejecucion3d.linea.Clear();
                Ejecucion3d.pm = 0;
                Ejecucion3d.am = 0;
                for (int i = 0; i < 100; i++) {
                    Ejecucion3d nuevo = new Ejecucion3d();
                    nuevo.valor = "";
                    Ejecucion3d.stack.AddLast(nuevo);
                }
                for (int i = 0; i < 100; i++)
                {
                    Ejecucion3d nuevo = new Ejecucion3d();
                    nuevo.valor = "";
                    Ejecucion3d.heap.AddLast(nuevo);
                }
                Sintactico1.analizarolc(contenido);
                Ejecucion3d.escribir3d();
                Ejecucion3d eje = new Ejecucion3d();
                eje.analizar3D(Ejecucion3d.cadenota);
                Ejecucion3d.cadenota = "";
                return;
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TabPage tabPagex = tabControl1.SelectedTab;
            foreach(FastColoredTextBox hijo in tabPagex.Controls)
            {
                hijo.Text = "";
                return;
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void ventana_TextChanged(object sender, TextChangedEventArgs e)
        {

                    CSharpSyntaxHighlight(e);
        
       }

        public  void CSharpSyntaxHighlight(TextChangedEventArgs e)
        {
            ventana.LeftBracket = '(';
            ventana.RightBracket = ')';
            ventana.LeftBracket2 = '\x0';
            ventana.RightBracket2 = '\x0';
            //clear style of changed range
            e.ChangedRange.ClearStyle(BlueStyle, GrayStyle, OrangeStyle);

            //string highlighting
            e.ChangedRange.SetStyle(OrangeStyle, @"""""|@""""|''|"".*""|'.?'");
            //comment highlighting
            e.ChangedRange.SetStyle(GrayStyle, @"(/-[^-]*-/)|({--[^-]*--})", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(GrayStyle, @"(##[^\n]*)|(//[^\n]*)", RegexOptions.Singleline);

            //number highlighting
            // e.ChangedRange.SetStyle(MagentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            e.ChangedRange.SetStyle(PurpleStyle, @"\b\d+[\.]?\d*");
            e.ChangedRange.SetStyle(BlueStyle, @"\bt\d+");
            e.ChangedRange.SetStyle(BlueStyle, @"\bL\d+");
            e.ChangedRange.SetStyle(BlueStyle, @"\b(iffalse|__constructor|mientras|hacer|si|si_no|si_no_si|Sino|not|or|and|Para|retorno|Si|caso|defecto|Mientras|Repetir|until|cadena|caracter|decimal|goto|then|selfp|heap|stack|hp|sp|clase|llamar|hereda_de|entero|boolean|true|false|publico|privado|protegido|metodo|nuevo|principal|imprimir|importar|self|este|#region\b|#endregion\b)");

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();

            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks

        }

        private void uMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UML vistauml = new UML();
            
            vistauml.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TabPage tabPagex = new System.Windows.Forms.TabPage();
            tabPagex.Location = new System.Drawing.Point(4, 22);
            tabPagex.Name = "tabPage"+index;
            tabPagex.Padding = new System.Windows.Forms.Padding(3);
            tabPagex.Size = new System.Drawing.Size(699, 289);
            tabPagex.TabIndex = index;
            tabPagex.Text = "ventana"+index;
            tabPagex.UseVisualStyleBackColor = true;
            index++;
           
            FastColoredTextBox  ventanax = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)(ventanax)).BeginInit();

            ventanax.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            ventanax.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            ventanax.BackBrush = null;
            ventanax.CharHeight = 14;
            ventanax.CharWidth = 8;
            ventanax.Cursor = System.Windows.Forms.Cursors.IBeam;
            ventanax.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            ventanax.IsReplaceMode = false;
            ventanax.Location = new System.Drawing.Point(0, 0);
            ventanax.Name = "ventana"+index;
            ventanax.Paddings = new System.Windows.Forms.Padding(0);
            ventanax.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            ventanax.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("ventana.ServiceColors")));
            ventanax.Size = new System.Drawing.Size(818, 289);
            ventanax.TabIndex = 9;
            ventanax.Zoom = 100;
            ventanax.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.ventana_TextChanged);

            tabPagex.Controls.Add(ventanax);

            this.tabControl1.Controls.Add(tabPagex);
        }

        private void fastColoredTextBox6_TextChanged(object sender, TextChangedEventArgs e)
        {
            CSharpSyntaxHighlight(e);
        }

        private void fastColoredTextBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            CSharpSyntaxHighlight(e);
        }

        private void fastColoredTextBox4_TextChanged(object sender, TextChangedEventArgs e)
        {
            CSharpSyntaxHighlight(e);
        }

        private void fastColoredTextBox5_TextChanged(object sender, TextChangedEventArgs e)
        {
            CSharpSyntaxHighlight(e);
        }

        private void fastColoredTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            CSharpSyntaxHighlight(e);
        }

        private void fastColoredTextBox1_Load(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
           
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
           
            

        }

        private void Abrir_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            string[] files;
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
               files  = Directory.GetFiles(fbd.SelectedPath);
                String Nombre = "";
                String [] dirs = fbd.SelectedPath.Split('\\');
                String[] archs;
                int tama = dirs.Count();
                Nombre = dirs[tama - 1];
                TreeNode Folder = new System.Windows.Forms.TreeNode(Nombre);
                Folder.ImageIndex = 0;
                Folder.Name = fbd.SelectedPath;
                Folder.Text = Nombre;
                
                TreeNode archivo;
                foreach (String hijo in files)
                {
                    
                    archs = hijo.Split('\\');
                    tama = archs.Count();
                    Nombre = archs[tama - 1];
                    archivo = new System.Windows.Forms.TreeNode(Nombre);
                    archivo.ImageIndex = 1;
                    archivo.Name = hijo;
                    archivo.Text = Nombre;
                    Folder.Nodes.Add(archivo);
                }

                this.Directorios.Nodes.Add(Folder);
                
                
            }


        }

        private void guardar_Click(object sender, EventArgs e)
        {
            TabPage tabPagex = tabControl1.SelectedTab;
            foreach (FastColoredTextBox hijo in tabPagex.Controls)
            {
               if(File.Exists(hijo.Name))
                {
                    string[] lines = hijo.Text.Split('\n');
                    System.IO.File.WriteAllLines(hijo.Name, lines);
                }else {
                    Guardar guar = new Guardar(hijo.Text);
                    Program.ventana = this;
                    guar.Show();

                }
                return;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage tabPagex = tabControl1.SelectedTab;

        }

        private void Directorios_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.ImageIndex != 0)
            {
                String direc = e.Node.Name;

                foreach (TabPage tab in tabControl1.Controls)
                {

                    if (tab.Name.Equals(direc))
                    {

                        return;
                    }
                }

                   String[] archs = direc.Split('\\');
                   int tama = archs.Count();
                   String  Nombre = archs[tama - 1];

                    TabPage tabPagex = new System.Windows.Forms.TabPage();
                    tabPagex.Location = new System.Drawing.Point(4, 22);
                    tabPagex.Name = direc;
                    tabPagex.Padding = new System.Windows.Forms.Padding(3);
                    tabPagex.Size = new System.Drawing.Size(699, 289);
                    tabPagex.TabIndex = index;
                    tabPagex.Text =Nombre;
                    tabPagex.UseVisualStyleBackColor = true;
                    

                    FastColoredTextBox ventanax = new FastColoredTextBoxNS.FastColoredTextBox();
                    ((System.ComponentModel.ISupportInitialize)(ventanax)).BeginInit();

                    ventanax.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
                    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
                    ventanax.AutoScrollMinSize = new System.Drawing.Size(27, 14);
                    ventanax.BackBrush = null;
                    ventanax.CharHeight = 14;
                    ventanax.CharWidth = 8;
                    ventanax.Cursor = System.Windows.Forms.Cursors.IBeam;
                    ventanax.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
                    ventanax.IsReplaceMode = false;
                    ventanax.Location = new System.Drawing.Point(0, 0);
                    ventanax.Name = direc;
                    ventanax.Paddings = new System.Windows.Forms.Padding(0);
                    ventanax.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
                    ventanax.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("ventana.ServiceColors")));
                    ventanax.Size = new System.Drawing.Size(818, 289);
                    ventanax.TabIndex = 9;
                    ventanax.Zoom = 100;
                    ventanax.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.ventana_TextChanged);

                   string[] lines = System.IO.File.ReadAllLines(direc);

                String conte = "";
                foreach (string line in lines)
                {
                    conte += "\n"+line;
                }

                ventanax.Text = conte;

                tabPagex.Controls.Add(ventanax);

                this.tabControl1.Controls.Add(tabPagex);

                



            }

        }

        private void Archivo_Click(object sender, EventArgs e)
        {
            Guardar guar = new Guardar("");
            Program.ventana = this;
            bandera = true;
            guar.Show();
        }

        private void CarpetaA_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            Folder fol = new Folder(fbd.SelectedPath);
            fol.Show();

        }

        private void ventana_Load(object sender, EventArgs e)
        {

        }

        private void debug_Click(object sender, EventArgs e)
        {
            String cadena = tresd.Text;
            _3DG gramatica = new _3DG();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parse = new Parser(lenguaje);
            ParseTree arbol = parse.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;
            Program.ventana.optimizado.Text = cadena;
            Ejecucion3d ejec = new Ejecucion3d();
            if(raiz!= null)
            {
                Ejecucion3d.Pila.Clear();
                Ejecucion3d.stack.Clear();
                Ejecucion3d.heap.Clear();
                Ejecucion3d.pm = 0;
                Ejecucion3d.am = 0;
                for (int i = 0; i < 100; i++)
                {
                    Ejecucion3d nuevo = new Ejecucion3d();
                    nuevo.valor = "";
                    Ejecucion3d.stack.AddLast(nuevo);
                }
                for (int i = 0; i < 100; i++)
                {
                    Ejecucion3d nuevo = new Ejecucion3d();
                    nuevo.valor = "";
                    Ejecucion3d.heap.AddLast(nuevo);
                }
                raiz = ejec.reparar(raiz);
                LinkedList<ParseTreeNode> lista =  ejec.linealizar(raiz);
                Debuger deb = new Debuger(lista,cadena);
                deb.Show();
            }
        }

        private void gramáticaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteG nuevo = new ReporteG();
            nuevo.Show();
        }
    }
}

