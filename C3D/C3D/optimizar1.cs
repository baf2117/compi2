using FastColoredTextBoxNS;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C3D
{
    public partial class optimizar1 : Form
    {

        TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        TextStyle OrangeStyle = new TextStyle(Brushes.Orange, null, FontStyle.Italic);
        TextStyle PurpleStyle = new TextStyle(Brushes.Purple, null, FontStyle.Italic);
        public optimizar1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //cambio
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "3d (.3d)|*.3d";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = true;
            string text = "";
            string nombre = "";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                nombre = openFileDialog1.FileName;
                try
                {
                    text = File.ReadAllText(nombre);

                }
                catch (IOException)
                {
                }
            }
            if (text.Length > 0)
            {
                String[] name = nombre.Split('\\');
                String name2 = name[name.Length - 1];
                
                consola.Text = text;

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

        public void CSharpSyntaxHighlight1(TextChangedEventArgs e)
        {
            consola1.LeftBracket = '(';
            consola1.RightBracket = ')';
            consola1.LeftBracket2 = '\x0';
            consola1.RightBracket2 = '\x0';
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

        private void consola_TextChanged(object sender, TextChangedEventArgs e)
        {
            CSharpSyntaxHighlight(e);
        }

        private void consola1_TextChanged(object sender, TextChangedEventArgs e)
        {
            CSharpSyntaxHighlight1(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Ejecucion3d nuevo = new Ejecucion3d();
            ParseTreeNode raiz = nuevo.analizar3D1(consola.Text);
            if (raiz != null)
            {
                nuevo.optimizar(raiz);
                consola1.Text = Ejecucion3d.cadenaopti;
            }

        }
    }
}
