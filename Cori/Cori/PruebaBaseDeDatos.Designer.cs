
namespace Cori
{
    partial class PruebaBaseDeDatos
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
            this.BTNPersona = new DevComponents.DotNetBar.ButtonX();
            this.BTNUsuario = new DevComponents.DotNetBar.ButtonX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // BTNPersona
            // 
            this.BTNPersona.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BTNPersona.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BTNPersona.Location = new System.Drawing.Point(45, 35);
            this.BTNPersona.Name = "BTNPersona";
            this.BTNPersona.Size = new System.Drawing.Size(163, 65);
            this.BTNPersona.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BTNPersona.TabIndex = 0;
            this.BTNPersona.Text = "Reg Persona";
            this.BTNPersona.Click += new System.EventHandler(this.BTNPersona_Click);
            // 
            // BTNUsuario
            // 
            this.BTNUsuario.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BTNUsuario.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BTNUsuario.Location = new System.Drawing.Point(45, 117);
            this.BTNUsuario.Name = "BTNUsuario";
            this.BTNUsuario.Size = new System.Drawing.Size(163, 65);
            this.BTNUsuario.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BTNUsuario.TabIndex = 1;
            this.BTNUsuario.Text = "Reg Usuario";
            this.BTNUsuario.Click += new System.EventHandler(this.BTNUsuario_Click);
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Location = new System.Drawing.Point(45, 203);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(163, 65);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 2;
            this.buttonX3.Text = "Reg Cliente";
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX4.Location = new System.Drawing.Point(45, 291);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Size = new System.Drawing.Size(163, 65);
            this.buttonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX4.TabIndex = 3;
            this.buttonX4.Text = "Reg Producto";
            // 
            // PruebaBaseDeDatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonX4);
            this.Controls.Add(this.buttonX3);
            this.Controls.Add(this.BTNUsuario);
            this.Controls.Add(this.BTNPersona);
            this.Name = "PruebaBaseDeDatos";
            this.Text = "PruebaBaseDeDatos";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX BTNPersona;
        private DevComponents.DotNetBar.ButtonX BTNUsuario;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX buttonX4;
    }
}