namespace Usuarios_planta.Formularios
{
    partial class Informes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnDescargar_Excel = new FontAwesome.Sharp.IconButton();
            this.dgvDatos_Punto = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpFecha_Punto = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.btnPunto_Control = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos_Punto)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDescargar_Excel
            // 
            this.btnDescargar_Excel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(66)))), ((int)(((byte)(84)))));
            this.btnDescargar_Excel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDescargar_Excel.Font = new System.Drawing.Font("Roboto Cn", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDescargar_Excel.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDescargar_Excel.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            this.btnDescargar_Excel.IconColor = System.Drawing.Color.Gainsboro;
            this.btnDescargar_Excel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDescargar_Excel.IconSize = 19;
            this.btnDescargar_Excel.Location = new System.Drawing.Point(1159, 751);
            this.btnDescargar_Excel.Name = "btnDescargar_Excel";
            this.btnDescargar_Excel.Size = new System.Drawing.Size(109, 36);
            this.btnDescargar_Excel.TabIndex = 281;
            this.btnDescargar_Excel.Text = "Exp. Excel";
            this.btnDescargar_Excel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDescargar_Excel.UseVisualStyleBackColor = false;
            this.btnDescargar_Excel.Click += new System.EventHandler(this.btnDescargar_Excel_Click);
            // 
            // dgvDatos_Punto
            // 
            this.dgvDatos_Punto.AllowDrop = true;
            this.dgvDatos_Punto.AllowUserToAddRows = false;
            this.dgvDatos_Punto.AllowUserToOrderColumns = true;
            this.dgvDatos_Punto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDatos_Punto.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDatos_Punto.BackgroundColor = System.Drawing.Color.White;
            this.dgvDatos_Punto.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(66)))), ((int)(((byte)(84)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(66)))), ((int)(((byte)(84)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDatos_Punto.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDatos_Punto.ColumnHeadersHeight = 40;
            this.dgvDatos_Punto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDatos_Punto.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDatos_Punto.EnableHeadersVisualStyles = false;
            this.dgvDatos_Punto.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvDatos_Punto.Location = new System.Drawing.Point(48, 158);
            this.dgvDatos_Punto.Name = "dgvDatos_Punto";
            this.dgvDatos_Punto.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDatos_Punto.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDatos_Punto.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            this.dgvDatos_Punto.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDatos_Punto.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos_Punto.Size = new System.Drawing.Size(1220, 587);
            this.dgvDatos_Punto.TabIndex = 280;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Cursor = System.Windows.Forms.Cursors.Default;
            this.label9.Font = new System.Drawing.Font("Roboto Cn", 12F);
            this.label9.Location = new System.Drawing.Point(49, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 19);
            this.label9.TabIndex = 279;
            this.label9.Text = "Seleccionar Fecha";
            // 
            // dtpFecha_Punto
            // 
            this.dtpFecha_Punto.CustomFormat = "dd/MM/yyyy";
            this.dtpFecha_Punto.Font = new System.Drawing.Font("Roboto Cn", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFecha_Punto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha_Punto.Location = new System.Drawing.Point(52, 123);
            this.dtpFecha_Punto.Name = "dtpFecha_Punto";
            this.dtpFecha_Punto.Size = new System.Drawing.Size(112, 26);
            this.dtpFecha_Punto.TabIndex = 278;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Cursor = System.Windows.Forms.Cursors.Default;
            this.label8.Font = new System.Drawing.Font("Roboto Cn", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(66)))), ((int)(((byte)(84)))));
            this.label8.Location = new System.Drawing.Point(514, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(228, 38);
            this.label8.TabIndex = 277;
            this.label8.Text = "Punto de control";
            // 
            // btnPunto_Control
            // 
            this.btnPunto_Control.BackColor = System.Drawing.Color.White;
            this.btnPunto_Control.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPunto_Control.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPunto_Control.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPunto_Control.ForeColor = System.Drawing.Color.White;
            this.btnPunto_Control.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnPunto_Control.IconColor = System.Drawing.Color.Black;
            this.btnPunto_Control.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPunto_Control.IconSize = 30;
            this.btnPunto_Control.Location = new System.Drawing.Point(170, 123);
            this.btnPunto_Control.Name = "btnPunto_Control";
            this.btnPunto_Control.Size = new System.Drawing.Size(38, 29);
            this.btnPunto_Control.TabIndex = 276;
            this.btnPunto_Control.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPunto_Control.UseVisualStyleBackColor = false;
            this.btnPunto_Control.Click += new System.EventHandler(this.btnPunto_Control_Click);
            // 
            // Informes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1296, 799);
            this.Controls.Add(this.btnDescargar_Excel);
            this.Controls.Add(this.dgvDatos_Punto);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dtpFecha_Punto);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnPunto_Control);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Informes";
            this.Text = "Informes";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos_Punto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnDescargar_Excel;
        private System.Windows.Forms.DataGridView dgvDatos_Punto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpFecha_Punto;
        private System.Windows.Forms.Label label8;
        private FontAwesome.Sharp.IconButton btnPunto_Control;
    }
}