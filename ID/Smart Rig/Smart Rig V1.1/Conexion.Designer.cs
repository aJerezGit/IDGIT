namespace Smart_Rig_V1._1
{
    partial class frmConexion
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.EstiloForms = new MetroFramework.Components.MetroStyleManager(this.components);
            this.pnlConexion = new MetroFramework.Controls.MetroPanel();
            this.btnConexion = new MetroFramework.Controls.MetroButton();
            this.metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
            this.cbPuertos = new MetroFramework.Controls.MetroComboBox();
            this.cbVelocidad = new MetroFramework.Controls.MetroComboBox();
            this.lblVelocidad = new MetroFramework.Controls.MetroLabel();
            this.lblPuerto = new MetroFramework.Controls.MetroLabel();
            this.lblConexion = new MetroFramework.Controls.MetroLabel();
            this.lblNombreBD = new MetroFramework.Controls.MetroLabel();
            this.lblBD = new MetroFramework.Controls.MetroLabel();
            this.pnlBD = new System.Windows.Forms.Panel();
            this.btnBD = new MetroFramework.Controls.MetroButton();
            this.txtNombreBD = new MetroFramework.Controls.MetroTextBox();
            this.PuertoSerial = new System.IO.Ports.SerialPort(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.EstiloForms)).BeginInit();
            this.pnlConexion.SuspendLayout();
            this.pnlBD.SuspendLayout();
            this.SuspendLayout();
            // 
            // EstiloForms
            // 
            this.EstiloForms.Owner = this;
            this.EstiloForms.Style = MetroFramework.MetroColorStyle.Green;
            // 
            // pnlConexion
            // 
            this.pnlConexion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            this.pnlConexion.Controls.Add(this.btnConexion);
            this.pnlConexion.Controls.Add(this.metroTextBox1);
            this.pnlConexion.Controls.Add(this.cbPuertos);
            this.pnlConexion.Controls.Add(this.cbVelocidad);
            this.pnlConexion.Controls.Add(this.lblVelocidad);
            this.pnlConexion.Controls.Add(this.lblPuerto);
            this.pnlConexion.Controls.Add(this.lblConexion);
            this.pnlConexion.HorizontalScrollbarBarColor = true;
            this.pnlConexion.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlConexion.HorizontalScrollbarSize = 10;
            this.pnlConexion.Location = new System.Drawing.Point(50, 100);
            this.pnlConexion.Name = "pnlConexion";
            this.pnlConexion.Size = new System.Drawing.Size(500, 400);
            this.pnlConexion.TabIndex = 0;
            this.pnlConexion.VerticalScrollbarBarColor = true;
            this.pnlConexion.VerticalScrollbarHighlightOnWheel = false;
            this.pnlConexion.VerticalScrollbarSize = 10;
            // 
            // btnConexion
            // 
            this.btnConexion.Location = new System.Drawing.Point(186, 330);
            this.btnConexion.Name = "btnConexion";
            this.btnConexion.Size = new System.Drawing.Size(75, 23);
            this.btnConexion.TabIndex = 8;
            this.btnConexion.Text = "Siguiente";
            this.btnConexion.UseSelectable = true;
            this.btnConexion.Click += new System.EventHandler(this.btnConexion_Click);
            // 
            // metroTextBox1
            // 
            // 
            // 
            // 
            this.metroTextBox1.CustomButton.Image = null;
            this.metroTextBox1.CustomButton.Location = new System.Drawing.Point(152, 1);
            this.metroTextBox1.CustomButton.Name = "";
            this.metroTextBox1.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.metroTextBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox1.CustomButton.TabIndex = 1;
            this.metroTextBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox1.CustomButton.UseSelectable = true;
            this.metroTextBox1.CustomButton.Visible = false;
            this.metroTextBox1.Lines = new string[0];
            this.metroTextBox1.Location = new System.Drawing.Point(578, 178);
            this.metroTextBox1.MaxLength = 32767;
            this.metroTextBox1.Name = "metroTextBox1";
            this.metroTextBox1.PasswordChar = '\0';
            this.metroTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox1.SelectedText = "";
            this.metroTextBox1.SelectionLength = 0;
            this.metroTextBox1.SelectionStart = 0;
            this.metroTextBox1.ShortcutsEnabled = true;
            this.metroTextBox1.Size = new System.Drawing.Size(174, 23);
            this.metroTextBox1.TabIndex = 6;
            this.metroTextBox1.UseSelectable = true;
            this.metroTextBox1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // cbPuertos
            // 
            this.cbPuertos.FormattingEnabled = true;
            this.cbPuertos.ItemHeight = 23;
            this.cbPuertos.Location = new System.Drawing.Point(219, 185);
            this.cbPuertos.Name = "cbPuertos";
            this.cbPuertos.Size = new System.Drawing.Size(121, 29);
            this.cbPuertos.TabIndex = 7;
            this.cbPuertos.UseSelectable = true;
            // 
            // cbVelocidad
            // 
            this.cbVelocidad.FormattingEnabled = true;
            this.cbVelocidad.ItemHeight = 23;
            this.cbVelocidad.Location = new System.Drawing.Point(219, 250);
            this.cbVelocidad.Name = "cbVelocidad";
            this.cbVelocidad.Size = new System.Drawing.Size(121, 29);
            this.cbVelocidad.TabIndex = 6;
            this.cbVelocidad.UseSelectable = true;
            // 
            // lblVelocidad
            // 
            this.lblVelocidad.AutoSize = true;
            this.lblVelocidad.Location = new System.Drawing.Point(95, 250);
            this.lblVelocidad.Name = "lblVelocidad";
            this.lblVelocidad.Size = new System.Drawing.Size(69, 19);
            this.lblVelocidad.TabIndex = 5;
            this.lblVelocidad.Text = "Velocidad:";
            // 
            // lblPuerto
            // 
            this.lblPuerto.AutoSize = true;
            this.lblPuerto.Location = new System.Drawing.Point(95, 185);
            this.lblPuerto.Name = "lblPuerto";
            this.lblPuerto.Size = new System.Drawing.Size(52, 19);
            this.lblPuerto.TabIndex = 4;
            this.lblPuerto.Text = "Puerto:";
            // 
            // lblConexion
            // 
            this.lblConexion.AutoSize = true;
            this.lblConexion.Location = new System.Drawing.Point(34, 100);
            this.lblConexion.Name = "lblConexion";
            this.lblConexion.Size = new System.Drawing.Size(436, 19);
            this.lblConexion.TabIndex = 2;
            this.lblConexion.Text = "A continuación configuraremos el puerto serial al que nos conectaremos.";
            // 
            // lblNombreBD
            // 
            this.lblNombreBD.AutoSize = true;
            this.lblNombreBD.Location = new System.Drawing.Point(95, 185);
            this.lblNombreBD.Name = "lblNombreBD";
            this.lblNombreBD.Size = new System.Drawing.Size(62, 19);
            this.lblNombreBD.TabIndex = 5;
            this.lblNombreBD.Text = "Nombre:";
            // 
            // lblBD
            // 
            this.lblBD.AutoSize = true;
            this.lblBD.Location = new System.Drawing.Point(141, 100);
            this.lblBD.Name = "lblBD";
            this.lblBD.Size = new System.Drawing.Size(230, 19);
            this.lblBD.TabIndex = 3;
            this.lblBD.Text = "Por favor ingrese el nombre del pozo";
            this.lblBD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlBD
            // 
            this.pnlBD.Controls.Add(this.btnBD);
            this.pnlBD.Controls.Add(this.txtNombreBD);
            this.pnlBD.Controls.Add(this.lblBD);
            this.pnlBD.Controls.Add(this.lblNombreBD);
            this.pnlBD.Location = new System.Drawing.Point(650, 100);
            this.pnlBD.Name = "pnlBD";
            this.pnlBD.Size = new System.Drawing.Size(500, 400);
            this.pnlBD.TabIndex = 2;
            // 
            // btnBD
            // 
            this.btnBD.Location = new System.Drawing.Point(219, 330);
            this.btnBD.Name = "btnBD";
            this.btnBD.Size = new System.Drawing.Size(75, 23);
            this.btnBD.TabIndex = 9;
            this.btnBD.Text = "Siguiente";
            this.btnBD.UseSelectable = true;
            this.btnBD.Click += new System.EventHandler(this.btnBD_Click);
            // 
            // txtNombreBD
            // 
            // 
            // 
            // 
            this.txtNombreBD.CustomButton.Image = null;
            this.txtNombreBD.CustomButton.Location = new System.Drawing.Point(179, 1);
            this.txtNombreBD.CustomButton.Name = "";
            this.txtNombreBD.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtNombreBD.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtNombreBD.CustomButton.TabIndex = 1;
            this.txtNombreBD.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtNombreBD.CustomButton.UseSelectable = true;
            this.txtNombreBD.CustomButton.Visible = false;
            this.txtNombreBD.Lines = new string[0];
            this.txtNombreBD.Location = new System.Drawing.Point(219, 185);
            this.txtNombreBD.MaxLength = 32767;
            this.txtNombreBD.Name = "txtNombreBD";
            this.txtNombreBD.PasswordChar = '\0';
            this.txtNombreBD.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNombreBD.SelectedText = "";
            this.txtNombreBD.SelectionLength = 0;
            this.txtNombreBD.SelectionStart = 0;
            this.txtNombreBD.ShortcutsEnabled = true;
            this.txtNombreBD.Size = new System.Drawing.Size(201, 23);
            this.txtNombreBD.TabIndex = 6;
            this.txtNombreBD.UseSelectable = true;
            this.txtNombreBD.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtNombreBD.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // frmConexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Controls.Add(this.pnlBD);
            this.Controls.Add(this.pnlConexion);
            this.Name = "frmConexion";
            this.Text = "Conexión";
            this.Load += new System.EventHandler(this.frmConexion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EstiloForms)).EndInit();
            this.pnlConexion.ResumeLayout(false);
            this.pnlConexion.PerformLayout();
            this.pnlBD.ResumeLayout(false);
            this.pnlBD.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public MetroFramework.Components.MetroStyleManager EstiloForms;
        private MetroFramework.Controls.MetroPanel pnlConexion;
        private MetroFramework.Controls.MetroLabel lblPuerto;
        private MetroFramework.Controls.MetroLabel lblConexion;
        private MetroFramework.Controls.MetroComboBox cbVelocidad;
        private MetroFramework.Controls.MetroLabel lblVelocidad;
        private MetroFramework.Controls.MetroComboBox cbPuertos;
        private MetroFramework.Controls.MetroButton btnConexion;
        private MetroFramework.Controls.MetroLabel lblNombreBD;
        private MetroFramework.Controls.MetroTextBox metroTextBox1;
        private System.Windows.Forms.Panel pnlBD;
        private MetroFramework.Controls.MetroTextBox txtNombreBD;
        private MetroFramework.Controls.MetroLabel lblBD;
        private MetroFramework.Controls.MetroButton btnBD;
        private System.IO.Ports.SerialPort PuertoSerial;
    }
}

