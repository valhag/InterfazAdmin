namespace InterfazAdmin
{
    partial class Addenda
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.botonExcel2 = new controles.BotonExcel();
            this.empresasComercial1 = new Controles.EmpresasComercial();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 84);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 17);
            this.label1.TabIndex = 56;
            this.label1.Text = "Facturas";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(152, 81);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(346, 21);
            this.comboBox1.TabIndex = 55;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 199);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(594, 23);
            this.button1.TabIndex = 54;
            this.button1.Text = "Enviar a Comercial";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // botonExcel2
            // 
            this.botonExcel2.Location = new System.Drawing.Point(26, 143);
            this.botonExcel2.Margin = new System.Windows.Forms.Padding(2);
            this.botonExcel2.Name = "botonExcel2";
            this.botonExcel2.Size = new System.Drawing.Size(533, 29);
            this.botonExcel2.TabIndex = 53;
            // 
            // empresasComercial1
            // 
            this.empresasComercial1.Location = new System.Drawing.Point(26, 28);
            this.empresasComercial1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.empresasComercial1.Name = "empresasComercial1";
            this.empresasComercial1.Size = new System.Drawing.Size(609, 54);
            this.empresasComercial1.TabIndex = 52;
            // 
            // Addenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 291);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.botonExcel2);
            this.Controls.Add(this.empresasComercial1);
            this.Name = "Addenda";
            this.Text = "Addenda";
            this.Load += new System.EventHandler(this.Addenda_Load);
            this.Shown += new System.EventHandler(this.Addenda_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private controles.BotonExcel botonExcel2;
        private Controles.EmpresasComercial empresasComercial1;
    }
}