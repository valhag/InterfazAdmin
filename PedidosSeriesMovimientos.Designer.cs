
namespace InterfazAdmin
{
    partial class PedidosSeriesMovimientos
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDecimales5 = new Controles.TBDecimales();
            this.tbDecimales4 = new Controles.TBDecimales();
            this.tbDecimales3 = new Controles.TBDecimales();
            this.tbDecimales2 = new Controles.TBDecimales();
            this.tbDecimales1 = new Controles.TBDecimales();
            this.codigocatalogocomercial2 = new Controles.codigocatalogocomercial();
            this.codigocatalogocomercial1 = new Controles.codigocatalogocomercial();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Precio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "IVA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Total";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Id});
            this.dataGridView1.Location = new System.Drawing.Point(28, 222);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(224, 216);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellLeave);
            this.dataGridView1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValidated);
            this.dataGridView1.Enter += new System.EventHandler(this.dataGridView1_Enter);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Numero de Serie";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Pedimento";
            this.Column2.Name = "Column2";
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Capture numeros de serie";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Numero Serie";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(112, 178);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(270, 20);
            this.textBox1.TabIndex = 11;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(340, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Guardar ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(340, 262);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(156, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Cantidad";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(457, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Neto";
            // 
            // tbDecimales5
            // 
            this.tbDecimales5.Enabled = false;
            this.tbDecimales5.Location = new System.Drawing.Point(523, 82);
            this.tbDecimales5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbDecimales5.Name = "tbDecimales5";
            this.tbDecimales5.Size = new System.Drawing.Size(114, 21);
            this.tbDecimales5.TabIndex = 17;
            // 
            // tbDecimales4
            // 
            this.tbDecimales4.Location = new System.Drawing.Point(112, 85);
            this.tbDecimales4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbDecimales4.Name = "tbDecimales4";
            this.tbDecimales4.Size = new System.Drawing.Size(114, 21);
            this.tbDecimales4.TabIndex = 15;
            // 
            // tbDecimales3
            // 
            this.tbDecimales3.Enabled = false;
            this.tbDecimales3.Location = new System.Drawing.Point(112, 146);
            this.tbDecimales3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbDecimales3.Name = "tbDecimales3";
            this.tbDecimales3.Size = new System.Drawing.Size(114, 21);
            this.tbDecimales3.TabIndex = 7;
            // 
            // tbDecimales2
            // 
            this.tbDecimales2.Enabled = false;
            this.tbDecimales2.Location = new System.Drawing.Point(112, 119);
            this.tbDecimales2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbDecimales2.Name = "tbDecimales2";
            this.tbDecimales2.Size = new System.Drawing.Size(114, 21);
            this.tbDecimales2.TabIndex = 5;
            // 
            // tbDecimales1
            // 
            this.tbDecimales1.Location = new System.Drawing.Point(312, 82);
            this.tbDecimales1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbDecimales1.Name = "tbDecimales1";
            this.tbDecimales1.Size = new System.Drawing.Size(114, 21);
            this.tbDecimales1.TabIndex = 3;
            // 
            // codigocatalogocomercial2
            // 
            this.codigocatalogocomercial2.Enabled = false;
            this.codigocatalogocomercial2.Location = new System.Drawing.Point(25, 51);
            this.codigocatalogocomercial2.Margin = new System.Windows.Forms.Padding(2);
            this.codigocatalogocomercial2.Name = "codigocatalogocomercial2";
            this.codigocatalogocomercial2.Size = new System.Drawing.Size(571, 21);
            this.codigocatalogocomercial2.TabIndex = 1;
            // 
            // codigocatalogocomercial1
            // 
            this.codigocatalogocomercial1.Enabled = false;
            this.codigocatalogocomercial1.Location = new System.Drawing.Point(25, 26);
            this.codigocatalogocomercial1.Margin = new System.Windows.Forms.Padding(2);
            this.codigocatalogocomercial1.Name = "codigocatalogocomercial1";
            this.codigocatalogocomercial1.Size = new System.Drawing.Size(571, 21);
            this.codigocatalogocomercial1.TabIndex = 0;
            // 
            // PedidosSeriesMovimientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 450);
            this.Controls.Add(this.tbDecimales5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbDecimales4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tbDecimales3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbDecimales2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDecimales1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.codigocatalogocomercial2);
            this.Controls.Add(this.codigocatalogocomercial1);
            this.Name = "PedidosSeriesMovimientos";
            this.Text = "PedidosSeriesMovimientos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PedidosSeriesMovimientos_FormClosing);
            this.Load += new System.EventHandler(this.PedidosSeriesMovimientos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controles.codigocatalogocomercial codigocatalogocomercial1;
        private Controles.codigocatalogocomercial codigocatalogocomercial2;
        private System.Windows.Forms.Label label1;
        private Controles.TBDecimales tbDecimales1;
        private Controles.TBDecimales tbDecimales2;
        private System.Windows.Forms.Label label2;
        private Controles.TBDecimales tbDecimales3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private Controles.TBDecimales tbDecimales4;
        private System.Windows.Forms.Label label6;
        private Controles.TBDecimales tbDecimales5;
        private System.Windows.Forms.Label label7;
    }
}