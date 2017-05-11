namespace InterfazAdmin
{
    partial class AmcoPedidos
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.seleccionEmpresa1 = new InterfazArchivoAdmin.SeleccionEmpresa();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(85, 225);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(544, 43);
            this.button1.TabIndex = 0;
            this.button1.Text = "Procesar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(26, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(696, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "http://stage.amcoonline.net/api/purchase_orders/order_materials?d=2017-02-08";
            // 
            // seleccionEmpresa1
            // 
            this.seleccionEmpresa1.Location = new System.Drawing.Point(20, 69);
            this.seleccionEmpresa1.Name = "seleccionEmpresa1";
            this.seleccionEmpresa1.Size = new System.Drawing.Size(743, 75);
            this.seleccionEmpresa1.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(23, 147);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 17);
            this.label13.TabIndex = 40;
            this.label13.Text = "Concepto";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(100, 140);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(456, 24);
            this.comboBox1.TabIndex = 39;
            // 
            // AmcoPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 341);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.seleccionEmpresa1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "AmcoPedidos";
            this.Text = "AmcoPedidos";
            this.Load += new System.EventHandler(this.AmcoPedidos_Load);
            this.Shown += new System.EventHandler(this.AmcoPedidos_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private InterfazArchivoAdmin.SeleccionEmpresa seleccionEmpresa1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}