namespace MainForm
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.CB_userName = new System.Windows.Forms.ComboBox();
            this.LB_recipeName = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.CB_userName);
            this.panel1.Controls.Add(this.LB_recipeName);
            this.panel1.Location = new System.Drawing.Point(0, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(970, 100);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(593, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CB_userName
            // 
            this.CB_userName.FormattingEnabled = true;
            this.CB_userName.Location = new System.Drawing.Point(25, 32);
            this.CB_userName.Name = "CB_userName";
            this.CB_userName.Size = new System.Drawing.Size(207, 21);
            this.CB_userName.TabIndex = 1;
            this.CB_userName.SelectionChangeCommitted += new System.EventHandler(this.CB_userName_SelectionChangeCommitted);
            // 
            // LB_recipeName
            // 
            this.LB_recipeName.FormattingEnabled = true;
            this.LB_recipeName.Location = new System.Drawing.Point(277, 2);
            this.LB_recipeName.Name = "LB_recipeName";
            this.LB_recipeName.Size = new System.Drawing.Size(257, 95);
            this.LB_recipeName.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 480);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox CB_userName;
        private System.Windows.Forms.ListBox LB_recipeName;
    }
}

