namespace C3D
{
    partial class Arelaciones
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
            this.Clasea = new System.Windows.Forms.ComboBox();
            this.Claseb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Agregar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Clasea
            // 
            this.Clasea.FormattingEnabled = true;
            this.Clasea.Location = new System.Drawing.Point(12, 47);
            this.Clasea.Name = "Clasea";
            this.Clasea.Size = new System.Drawing.Size(121, 21);
            this.Clasea.TabIndex = 0;
            // 
            // Claseb
            // 
            this.Claseb.FormattingEnabled = true;
            this.Claseb.Location = new System.Drawing.Point(12, 114);
            this.Claseb.Name = "Claseb";
            this.Claseb.Size = new System.Drawing.Size(121, 21);
            this.Claseb.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ClaseA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "ClaseB";
            // 
            // Agregar
            // 
            this.Agregar.Location = new System.Drawing.Point(35, 166);
            this.Agregar.Name = "Agregar";
            this.Agregar.Size = new System.Drawing.Size(75, 23);
            this.Agregar.TabIndex = 4;
            this.Agregar.Text = "Agregar";
            this.Agregar.UseVisualStyleBackColor = true;
            this.Agregar.Click += new System.EventHandler(this.Agregar_Click);
            // 
            // Arelaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(152, 201);
            this.Controls.Add(this.Agregar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Claseb);
            this.Controls.Add(this.Clasea);
            this.Name = "Arelaciones";
            this.Text = "Arelaciones";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Clasea;
        private System.Windows.Forms.ComboBox Claseb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Agregar;
    }
}