
namespace InterfazAdmin
{
    partial class cfdiTraslado
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
            this.label13 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.botonExcel1 = new controles.BotonExcel();
            this.seleccionEmpresa1 = new InterfazArchivoAdmin.SeleccionEmpresa();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(66, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(557, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Importar Excel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(97, 104);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(97, 13);
            this.label13.TabIndex = 44;
            this.label13.Text = "Concepto Traslado";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(214, 96);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(348, 21);
            this.comboBox1.TabIndex = 43;
            // 
            // botonExcel1
            // 
            this.botonExcel1.Location = new System.Drawing.Point(116, 122);
            this.botonExcel1.Margin = new System.Windows.Forms.Padding(2);
            this.botonExcel1.Name = "botonExcel1";
            this.botonExcel1.Size = new System.Drawing.Size(533, 29);
            this.botonExcel1.TabIndex = 45;
            // 
            // seleccionEmpresa1
            // 
            this.seleccionEmpresa1.Location = new System.Drawing.Point(66, 42);
            this.seleccionEmpresa1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.seleccionEmpresa1.Name = "seleccionEmpresa1";
            this.seleccionEmpresa1.Size = new System.Drawing.Size(557, 61);
            this.seleccionEmpresa1.TabIndex = 2;
            this.seleccionEmpresa1.Load += new System.EventHandler(this.cfdiTraslado_Load_1);
            // 
            // cfdiTraslado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 215);
            this.Controls.Add(this.botonExcel1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.seleccionEmpresa1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cfdiTraslado";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generacion Traslado";
            this.Load += new System.EventHandler(this.cfdiTraslado_Load_1);
            this.Shown += new System.EventHandler(this.cfdiTraslado_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private InterfazArchivoAdmin.SeleccionEmpresa seleccionEmpresa1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBox1;
        private controles.BotonExcel botonExcel1;
    }
}