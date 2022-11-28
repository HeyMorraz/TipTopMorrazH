namespace TipTopMorrazH.OrdenamientoExterno
{
    partial class Externo
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonIntercalacion = new System.Windows.Forms.Button();
            this.buttonMeazclaDirecta = new System.Windows.Forms.Button();
            this.buttonEquilibrada = new System.Windows.Forms.Button();
            this.buttonVolver = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonVolver);
            this.panel1.Controls.Add(this.buttonEquilibrada);
            this.panel1.Controls.Add(this.buttonMeazclaDirecta);
            this.panel1.Controls.Add(this.buttonIntercalacion);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(283, 363);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Font = new System.Drawing.Font("Showcard Gothic", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(26, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 18);
            this.label1.TabIndex = 20;
            this.label1.Text = "Elija el tipo de ordenamiento";
            // 
            // buttonIntercalacion
            // 
            this.buttonIntercalacion.BackColor = System.Drawing.Color.LightSlateGray;
            this.buttonIntercalacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonIntercalacion.ForeColor = System.Drawing.Color.Black;
            this.buttonIntercalacion.Location = new System.Drawing.Point(37, 91);
            this.buttonIntercalacion.Name = "buttonIntercalacion";
            this.buttonIntercalacion.Size = new System.Drawing.Size(207, 37);
            this.buttonIntercalacion.TabIndex = 29;
            this.buttonIntercalacion.Text = "Intercalacion de Archivos";
            this.buttonIntercalacion.UseVisualStyleBackColor = false;
            this.buttonIntercalacion.Click += new System.EventHandler(this.ButtonIntercalacion_Click);
            // 
            // buttonMeazclaDirecta
            // 
            this.buttonMeazclaDirecta.BackColor = System.Drawing.Color.LightSlateGray;
            this.buttonMeazclaDirecta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMeazclaDirecta.ForeColor = System.Drawing.Color.Black;
            this.buttonMeazclaDirecta.Location = new System.Drawing.Point(37, 163);
            this.buttonMeazclaDirecta.Name = "buttonMeazclaDirecta";
            this.buttonMeazclaDirecta.Size = new System.Drawing.Size(207, 37);
            this.buttonMeazclaDirecta.TabIndex = 30;
            this.buttonMeazclaDirecta.Text = "Mezcla Directa";
            this.buttonMeazclaDirecta.UseVisualStyleBackColor = false;
            this.buttonMeazclaDirecta.Click += new System.EventHandler(this.ButtonMeazclaDirecta_Click);
            // 
            // buttonEquilibrada
            // 
            this.buttonEquilibrada.BackColor = System.Drawing.Color.LightSlateGray;
            this.buttonEquilibrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEquilibrada.ForeColor = System.Drawing.Color.Black;
            this.buttonEquilibrada.Location = new System.Drawing.Point(37, 234);
            this.buttonEquilibrada.Name = "buttonEquilibrada";
            this.buttonEquilibrada.Size = new System.Drawing.Size(207, 37);
            this.buttonEquilibrada.TabIndex = 31;
            this.buttonEquilibrada.Text = "Mezcla Equilibrada";
            this.buttonEquilibrada.UseVisualStyleBackColor = false;
            this.buttonEquilibrada.Click += new System.EventHandler(this.ButtonEquilibrada_Click);
            // 
            // buttonVolver
            // 
            this.buttonVolver.BackColor = System.Drawing.Color.LightSlateGray;
            this.buttonVolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonVolver.ForeColor = System.Drawing.Color.Black;
            this.buttonVolver.Location = new System.Drawing.Point(96, 315);
            this.buttonVolver.Name = "buttonVolver";
            this.buttonVolver.Size = new System.Drawing.Size(88, 37);
            this.buttonVolver.TabIndex = 32;
            this.buttonVolver.Text = "Volver";
            this.buttonVolver.UseVisualStyleBackColor = false;
            this.buttonVolver.Click += new System.EventHandler(this.ButtonVolver_Click);
            // 
            // Externo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 365);
            this.Controls.Add(this.panel1);
            this.Name = "Externo";
            this.Text = "Externo";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonVolver;
        private System.Windows.Forms.Button buttonEquilibrada;
        private System.Windows.Forms.Button buttonMeazclaDirecta;
        private System.Windows.Forms.Button buttonIntercalacion;
    }
}