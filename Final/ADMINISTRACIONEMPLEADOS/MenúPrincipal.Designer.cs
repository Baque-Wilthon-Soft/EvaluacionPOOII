namespace ADMINISTRACIONEMPLEADOS
{
    partial class MenúPrincipal
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Departamentos = new System.Windows.Forms.Button();
            this.btnCargos = new System.Windows.Forms.Button();
            this.btnEmpleados = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.Departamentos);
            this.panel1.Controls.Add(this.btnCargos);
            this.panel1.Controls.Add(this.btnEmpleados);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(307, 313);
            this.panel1.TabIndex = 0;
            // 
            // Departamentos
            // 
            this.Departamentos.BackColor = System.Drawing.Color.White;
            this.Departamentos.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Departamentos.Location = new System.Drawing.Point(84, 182);
            this.Departamentos.Margin = new System.Windows.Forms.Padding(4);
            this.Departamentos.Name = "Departamentos";
            this.Departamentos.Size = new System.Drawing.Size(146, 66);
            this.Departamentos.TabIndex = 2;
            this.Departamentos.Text = "Departamentos";
            this.Departamentos.UseVisualStyleBackColor = false;
            this.Departamentos.Click += new System.EventHandler(this.Departamentos_Click);
            // 
            // btnCargos
            // 
            this.btnCargos.BackColor = System.Drawing.Color.White;
            this.btnCargos.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargos.Location = new System.Drawing.Point(84, 108);
            this.btnCargos.Margin = new System.Windows.Forms.Padding(4);
            this.btnCargos.Name = "btnCargos";
            this.btnCargos.Size = new System.Drawing.Size(146, 66);
            this.btnCargos.TabIndex = 1;
            this.btnCargos.Text = "Cargos";
            this.btnCargos.UseVisualStyleBackColor = false;
            this.btnCargos.Click += new System.EventHandler(this.btnCargos_Click);
            // 
            // btnEmpleados
            // 
            this.btnEmpleados.BackColor = System.Drawing.Color.White;
            this.btnEmpleados.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmpleados.Location = new System.Drawing.Point(84, 34);
            this.btnEmpleados.Margin = new System.Windows.Forms.Padding(4);
            this.btnEmpleados.Name = "btnEmpleados";
            this.btnEmpleados.Size = new System.Drawing.Size(146, 66);
            this.btnEmpleados.TabIndex = 0;
            this.btnEmpleados.Text = "Empleados";
            this.btnEmpleados.UseVisualStyleBackColor = false;
            this.btnEmpleados.Click += new System.EventHandler(this.btnEmpleados_Click);
            // 
            // MenúPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 313);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MenúPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenúPrincipal";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Departamentos;
        private System.Windows.Forms.Button btnCargos;
        private System.Windows.Forms.Button btnEmpleados;
    }
}

