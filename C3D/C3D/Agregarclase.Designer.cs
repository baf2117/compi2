namespace C3D
{
    partial class Agregarclase
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nombrec = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.visia = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tipoa = new System.Windows.Forms.ComboBox();
            this.Nombrea = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.nombrep = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tipop = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.visim = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tipom = new System.Windows.Forms.ComboBox();
            this.nombrem = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(251, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Agregar C.\r\n";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre";
            // 
            // nombrec
            // 
            this.nombrec.Location = new System.Drawing.Point(80, 36);
            this.nombrec.Name = "nombrec";
            this.nombrec.Size = new System.Drawing.Size(117, 20);
            this.nombrec.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.visia);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tipoa);
            this.groupBox1.Controls.Add(this.Nombrea);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(15, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 111);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Atributos";
            // 
            // visia
            // 
            this.visia.FormattingEnabled = true;
            this.visia.Items.AddRange(new object[] {
            "publico",
            "privado",
            "protegido"});
            this.visia.Location = new System.Drawing.Point(65, 67);
            this.visia.Name = "visia";
            this.visia.Size = new System.Drawing.Size(117, 21);
            this.visia.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(236, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Agregar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Visibilidad";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(202, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tipo";
            // 
            // tipoa
            // 
            this.tipoa.FormattingEnabled = true;
            this.tipoa.Items.AddRange(new object[] {
            "entero",
            "cadena",
            "caracter",
            "booleano",
            "decimal"});
            this.tipoa.Location = new System.Drawing.Point(236, 25);
            this.tipoa.Name = "tipoa";
            this.tipoa.Size = new System.Drawing.Size(121, 21);
            this.tipoa.TabIndex = 2;
            // 
            // Nombrea
            // 
            this.Nombrea.Location = new System.Drawing.Point(65, 25);
            this.Nombrea.Name = "Nombrea";
            this.Nombrea.Size = new System.Drawing.Size(117, 20);
            this.Nombrea.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nombre";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.visim);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tipom);
            this.groupBox2.Controls.Add(this.nombrem);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(15, 212);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(374, 262);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Metodos";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.nombrep);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.tipop);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Location = new System.Drawing.Point(10, 93);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(358, 108);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Parametros";
            // 
            // nombrep
            // 
            this.nombrep.Location = new System.Drawing.Point(56, 35);
            this.nombrep.Name = "nombrep";
            this.nombrep.Size = new System.Drawing.Size(117, 20);
            this.nombrep.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Nombre";
            // 
            // tipop
            // 
            this.tipop.FormattingEnabled = true;
            this.tipop.Items.AddRange(new object[] {
            "entero",
            "cadena",
            "caracter",
            "booleano",
            "decimal"});
            this.tipop.Location = new System.Drawing.Point(227, 34);
            this.tipop.Name = "tipop";
            this.tipop.Size = new System.Drawing.Size(121, 21);
            this.tipop.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(193, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Tipo";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(226, 61);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "Agregar Parametro";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(10, 220);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(84, 23);
            this.button4.TabIndex = 22;
            this.button4.Text = "Agregar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // visim
            // 
            this.visim.FormattingEnabled = true;
            this.visim.Items.AddRange(new object[] {
            "publico",
            "privado",
            "protegido"});
            this.visim.Location = new System.Drawing.Point(65, 55);
            this.visim.Name = "visim";
            this.visim.Size = new System.Drawing.Size(117, 21);
            this.visim.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Visibilidad";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(202, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Tipo";
            // 
            // tipom
            // 
            this.tipom.FormattingEnabled = true;
            this.tipom.Items.AddRange(new object[] {
            "entero",
            "cadena",
            "caracter",
            "booleano",
            "decimal",
            "metodo"});
            this.tipom.Location = new System.Drawing.Point(236, 19);
            this.tipom.Name = "tipom";
            this.tipom.Size = new System.Drawing.Size(121, 21);
            this.tipom.TabIndex = 18;
            // 
            // nombrem
            // 
            this.nombrem.Location = new System.Drawing.Point(65, 19);
            this.nombrem.Name = "nombrem";
            this.nombrem.Size = new System.Drawing.Size(117, 20);
            this.nombrem.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Nombre";
            // 
            // Agregarclase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 473);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.nombrec);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Agregarclase";
            this.Text = "Agregarclase";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nombrec;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox tipoa;
        private System.Windows.Forms.TextBox Nombrea;
        private System.Windows.Forms.ComboBox visia;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox tipop;
        private System.Windows.Forms.TextBox nombrep;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox visim;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox tipom;
        private System.Windows.Forms.TextBox nombrem;
        private System.Windows.Forms.Label label8;
    }
}