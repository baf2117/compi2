namespace C3D
{
    partial class optimizar1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(optimizar1));
            this.button1 = new System.Windows.Forms.Button();
            this.consola = new FastColoredTextBoxNS.FastColoredTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.consola1 = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.consola)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.consola1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(35, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 38);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.consola.Location = new System.Drawing.Point(12, 56);
            this.consola.Name = "consola";
            this.consola.Paddings = new System.Windows.Forms.Padding(0);
            this.consola.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.consola.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("consola.ServiceColors")));
            this.consola.Size = new System.Drawing.Size(337, 408);
            this.consola.TabIndex = 1;
            this.consola.Zoom = 100;
            this.consola.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.consola_TextChanged);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Location = new System.Drawing.Point(369, 250);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 38);
            this.button2.TabIndex = 2;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // consola1
            // 
            this.consola1.AutoCompleteBracketsList = new char[] {
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
            this.consola1.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.consola1.BackBrush = null;
            this.consola1.CharHeight = 14;
            this.consola1.CharWidth = 8;
            this.consola1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.consola1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.consola1.IsReplaceMode = false;
            this.consola1.Location = new System.Drawing.Point(419, 56);
            this.consola1.Name = "consola1";
            this.consola1.Paddings = new System.Windows.Forms.Padding(0);
            this.consola1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.consola1.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("consola1.ServiceColors")));
            this.consola1.Size = new System.Drawing.Size(337, 408);
            this.consola1.TabIndex = 3;
            this.consola1.Zoom = 100;
            this.consola1.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.consola1_TextChanged);
            // 
            // optimizar1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 489);
            this.Controls.Add(this.consola1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.consola);
            this.Controls.Add(this.button1);
            this.Name = "optimizar1";
            this.Text = "optimizar1";
            ((System.ComponentModel.ISupportInitialize)(this.consola)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.consola1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private FastColoredTextBoxNS.FastColoredTextBox consola;
        private System.Windows.Forms.Button button2;
        private FastColoredTextBoxNS.FastColoredTextBox consola1;
    }
}