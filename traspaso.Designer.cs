
namespace InterfazAdmin
{
    partial class traspaso
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.botonExcel1 = new controles.BotonExcel();
            this.empresasComercial1 = new Controles.EmpresasComercial();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 166);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(602, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Generar Traspaso";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(167, 120);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(452, 21);
            this.comboBox1.TabIndex = 41;
            this.comboBox1.Visible = false;
            // 
            // botonExcel1
            // 
            this.botonExcel1.Location = new System.Drawing.Point(30, 11);
            this.botonExcel1.Margin = new System.Windows.Forms.Padding(2);
            this.botonExcel1.Name = "botonExcel1";
            this.botonExcel1.Size = new System.Drawing.Size(532, 29);
            this.botonExcel1.TabIndex = 1;
            // 
            // empresasComercial1
            // 
            this.empresasComercial1.Location = new System.Drawing.Point(43, 62);
            this.empresasComercial1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.empresasComercial1.Name = "empresasComercial1";
            this.empresasComercial1.Size = new System.Drawing.Size(619, 54);
            this.empresasComercial1.TabIndex = 0;
            // 
            // traspaso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 223);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.botonExcel1);
            this.Controls.Add(this.empresasComercial1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "traspaso";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generar Traspaso";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.traspaso_FormClosed);
            this.Load += new System.EventHandler(this.traspaso_Load);
            this.Shown += new System.EventHandler(this.traspaso_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private Controles.EmpresasComercial empresasComercial1;
        private controles.BotonExcel botonExcel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}