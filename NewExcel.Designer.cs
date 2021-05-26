namespace InterfazAdmin
{
    partial class NewExcel
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
            this.empresasComercial1 = new Controles.EmpresasComercial();
            this.botonExcel2 = new controles.BotonExcel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // empresasComercial1
            // 
            this.empresasComercial1.Location = new System.Drawing.Point(24, 38);
            this.empresasComercial1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.empresasComercial1.Name = "empresasComercial1";
            this.empresasComercial1.Size = new System.Drawing.Size(609, 54);
            this.empresasComercial1.TabIndex = 1;
            // 
            // botonExcel2
            // 
            this.botonExcel2.Location = new System.Drawing.Point(24, 153);
            this.botonExcel2.Margin = new System.Windows.Forms.Padding(2);
            this.botonExcel2.Name = "botonExcel2";
            this.botonExcel2.Size = new System.Drawing.Size(533, 29);
            this.botonExcel2.TabIndex = 48;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 209);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(594, 23);
            this.button1.TabIndex = 49;
            this.button1.Text = "Enviar a Comercial";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 94);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 17);
            this.label1.TabIndex = 51;
            this.label1.Text = "Facturas";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(150, 91);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(346, 21);
            this.comboBox1.TabIndex = 50;
            // 
            // NewExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 284);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.botonExcel2);
            this.Controls.Add(this.empresasComercial1);
            this.Name = "NewExcel";
            this.Text = "NewExcel";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewExcel_FormClosed);
            this.Load += new System.EventHandler(this.NewExcel_Load);
            this.Shown += new System.EventHandler(this.NewExcel_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controles.EmpresasComercial empresasComercial1;
        private controles.BotonExcel botonExcel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}