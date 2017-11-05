namespace C3D
{
    partial class Debuger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Debuger));
            this.consola = new FastColoredTextBoxNS.FastColoredTextBox();
            this.Ejecutar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.TablaS = new System.Windows.Forms.TableLayoutPanel();
            this.lineas = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Pila = new System.Windows.Forms.TextBox();
            this.Heap = new System.Windows.Forms.TextBox();
            this.Stack = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.consola)).BeginInit();
            this.SuspendLayout();
            // 
            // consola
            // 
            this.consola.AutoCompleteBracketsList = new char[] {
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
            this.consola.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.consola.BackBrush = null;
            this.consola.CharHeight = 14;
            this.consola.CharWidth = 8;
            this.consola.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.consola.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.consola.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.consola.IsReplaceMode = false;
            this.consola.Location = new System.Drawing.Point(25, 84);
            this.consola.Name = "consola";
            this.consola.Paddings = new System.Windows.Forms.Padding(0);
            this.consola.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.consola.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("consola.ServiceColors")));
            this.consola.Size = new System.Drawing.Size(398, 220);
            this.consola.TabIndex = 0;
            this.consola.Zoom = 100;
            this.consola.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fastColoredTextBox1_TextChanged);
            this.consola.Load += new System.EventHandler(this.fastColoredTextBox1_Load);
            // 
            // Ejecutar
            // 
            this.Ejecutar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Ejecutar.BackgroundImage")));
            this.Ejecutar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Ejecutar.Location = new System.Drawing.Point(25, 12);
            this.Ejecutar.Name = "Ejecutar";
            this.Ejecutar.Size = new System.Drawing.Size(57, 54);
            this.Ejecutar.TabIndex = 2;
            this.Ejecutar.UseVisualStyleBackColor = true;
            this.Ejecutar.Click += new System.EventHandler(this.Ejecutar_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(103, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 54);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Location = new System.Drawing.Point(181, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(57, 54);
            this.button2.TabIndex = 4;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Location = new System.Drawing.Point(244, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(57, 54);
            this.button3.TabIndex = 5;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // TablaS
            // 
            this.TablaS.AutoScroll = true;
            this.TablaS.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TablaS.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.TablaS.ColumnCount = 5;
            this.TablaS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TablaS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TablaS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TablaS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TablaS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TablaS.Location = new System.Drawing.Point(51, 358);
            this.TablaS.Name = "TablaS";
            this.TablaS.RowCount = 2;
            this.TablaS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TablaS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TablaS.Size = new System.Drawing.Size(454, 100);
            this.TablaS.TabIndex = 6;
            // 
            // lineas
            // 
            this.lineas.FormattingEnabled = true;
            this.lineas.Location = new System.Drawing.Point(307, 45);
            this.lineas.Name = "lineas";
            this.lineas.Size = new System.Drawing.Size(121, 21);
            this.lineas.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(563, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "STACK";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(674, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "HEAP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(791, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "PILA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(563, 358);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "SP";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(590, 358);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(44, 20);
            this.textBox1.TabIndex = 15;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(590, 391);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(44, 20);
            this.textBox2.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(563, 394);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "HP";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(590, 420);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(44, 20);
            this.textBox3.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(545, 427);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "SELFP";
            // 
            // Pila
            // 
            this.Pila.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pila.Location = new System.Drawing.Point(760, 84);
            this.Pila.Multiline = true;
            this.Pila.Name = "Pila";
            this.Pila.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Pila.Size = new System.Drawing.Size(116, 220);
            this.Pila.TabIndex = 20;
            // 
            // Heap
            // 
            this.Heap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Heap.Location = new System.Drawing.Point(656, 84);
            this.Heap.Multiline = true;
            this.Heap.Name = "Heap";
            this.Heap.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Heap.Size = new System.Drawing.Size(72, 220);
            this.Heap.TabIndex = 21;
            // 
            // Stack
            // 
            this.Stack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stack.Location = new System.Drawing.Point(562, 84);
            this.Stack.Multiline = true;
            this.Stack.Name = "Stack";
            this.Stack.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Stack.Size = new System.Drawing.Size(72, 220);
            this.Stack.TabIndex = 22;
            // 
            // Debuger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 502);
            this.Controls.Add(this.Stack);
            this.Controls.Add(this.Heap);
            this.Controls.Add(this.Pila);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lineas);
            this.Controls.Add(this.TablaS);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Ejecutar);
            this.Controls.Add(this.consola);
            this.Name = "Debuger";
            this.Text = "Debuger";
            ((System.ComponentModel.ISupportInitialize)(this.consola)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox consola;
        private System.Windows.Forms.Button Ejecutar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TableLayoutPanel TablaS;
        private System.Windows.Forms.ComboBox lineas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Pila;
        private System.Windows.Forms.TextBox Heap;
        private System.Windows.Forms.TextBox Stack;
    }
}