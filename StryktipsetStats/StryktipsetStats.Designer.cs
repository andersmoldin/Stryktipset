namespace StryktipsetStats
{
    partial class StryktipsetStats
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
            this.SlumpaEnOmgång = new System.Windows.Forms.Button();
            this.KörAllaOmgångar = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.listView1 = new System.Windows.Forms.ListView();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.plotterOmgångar = new ScottPlot.FormsPlot();
            this.plotterSystem = new ScottPlot.FormsPlot();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SlumpaEnOmgång
            // 
            this.SlumpaEnOmgång.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SlumpaEnOmgång.AutoSize = true;
            this.SlumpaEnOmgång.Location = new System.Drawing.Point(1310, 12);
            this.SlumpaEnOmgång.Name = "SlumpaEnOmgång";
            this.SlumpaEnOmgång.Size = new System.Drawing.Size(141, 25);
            this.SlumpaEnOmgång.TabIndex = 0;
            this.SlumpaEnOmgång.Text = "Slumpa en omgång";
            this.SlumpaEnOmgång.UseVisualStyleBackColor = true;
            this.SlumpaEnOmgång.Click += new System.EventHandler(this.SlumpaEnOmgång_Click);
            // 
            // KörAllaOmgångar
            // 
            this.KörAllaOmgångar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.KörAllaOmgångar.AutoSize = true;
            this.KörAllaOmgångar.Location = new System.Drawing.Point(1457, 12);
            this.KörAllaOmgångar.Name = "KörAllaOmgångar";
            this.KörAllaOmgångar.Size = new System.Drawing.Size(141, 25);
            this.KörAllaOmgångar.TabIndex = 0;
            this.KörAllaOmgångar.Text = "Kör alla omgångar";
            this.KörAllaOmgångar.UseVisualStyleBackColor = true;
            this.KörAllaOmgångar.Click += new System.EventHandler(this.KörAllaOmgångar_Click);
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView.Location = new System.Drawing.Point(1187, 403);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(411, 418);
            this.listView.TabIndex = 3;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "System";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Metod";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Loopar";
            this.columnHeader3.Width = 80;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(1187, 43);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(411, 354);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1169, 809);
            this.tabControl.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.plotterOmgångar);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1161, 781);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Omgångar";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.plotterSystem);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1161, 781);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "System";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // plotterOmgångar
            // 
            this.plotterOmgångar.BackColor = System.Drawing.Color.Transparent;
            this.plotterOmgångar.Location = new System.Drawing.Point(7, 7);
            this.plotterOmgångar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.plotterOmgångar.Name = "plotterOmgångar";
            this.plotterOmgångar.Size = new System.Drawing.Size(1147, 768);
            this.plotterOmgångar.TabIndex = 0;
            // 
            // plotterSystem
            // 
            this.plotterSystem.BackColor = System.Drawing.Color.Transparent;
            this.plotterSystem.Location = new System.Drawing.Point(7, 7);
            this.plotterSystem.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.plotterSystem.Name = "plotterSystem";
            this.plotterSystem.Size = new System.Drawing.Size(1147, 768);
            this.plotterSystem.TabIndex = 0;
            // 
            // StryktipsetStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1610, 833);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.KörAllaOmgångar);
            this.Controls.Add(this.SlumpaEnOmgång);
            this.Name = "StryktipsetStats";
            this.Text = "StryktipsetStats";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button SlumpaEnOmgång;
        private Button KörAllaOmgångar;
        private ListView listView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ListView listView1;
        private TabControl tabControl;
        private TabPage tabPage1;
        private ScottPlot.FormsPlot plotterOmgångar;
        private TabPage tabPage2;
        private ScottPlot.FormsPlot plotterSystem;
    }
}