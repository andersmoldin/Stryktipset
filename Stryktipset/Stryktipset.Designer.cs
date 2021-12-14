namespace Stryktipset
{
    partial class Stryktipset
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LäsInOmgångar = new System.Windows.Forms.Button();
            this.antalOmgångar = new System.Windows.Forms.Label();
            this.omgångar = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // LäsInOmgångar
            // 
            this.LäsInOmgångar.Location = new System.Drawing.Point(900, 12);
            this.LäsInOmgångar.Name = "LäsInOmgångar";
            this.LäsInOmgångar.Size = new System.Drawing.Size(112, 23);
            this.LäsInOmgångar.TabIndex = 0;
            this.LäsInOmgångar.Text = "Läs in omgångar";
            this.LäsInOmgångar.UseVisualStyleBackColor = true;
            this.LäsInOmgångar.Click += new System.EventHandler(this.LäsInOmgångar_Click);
            // 
            // antalOmgångar
            // 
            this.antalOmgångar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.antalOmgångar.AutoSize = true;
            this.antalOmgångar.Location = new System.Drawing.Point(900, 38);
            this.antalOmgångar.Name = "antalOmgångar";
            this.antalOmgångar.Size = new System.Drawing.Size(76, 15);
            this.antalOmgångar.TabIndex = 1;
            this.antalOmgångar.Text = "Omgångar: 0";
            // 
            // omgångar
            // 
            this.omgångar.Location = new System.Drawing.Point(12, 12);
            this.omgångar.Name = "omgångar";
            this.omgångar.Size = new System.Drawing.Size(691, 585);
            this.omgångar.TabIndex = 2;
            // 
            // Stryktipset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 609);
            this.Controls.Add(this.omgångar);
            this.Controls.Add(this.antalOmgångar);
            this.Controls.Add(this.LäsInOmgångar);
            this.Name = "Stryktipset";
            this.Text = "Stryktipset";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LäsInOmgångar;
        private System.Windows.Forms.Label antalOmgångar;
        private System.Windows.Forms.TreeView omgångar;
    }
}

