namespace FormationEditor
{
    partial class GroupEditor
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
            this.GameHandle = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.EnemiesListView = new System.Windows.Forms.TreeView();
            this.Up = new System.Windows.Forms.Button();
            this.Down = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GameHandle)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GameHandle
            // 
            this.GameHandle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GameHandle.Location = new System.Drawing.Point(0, 0);
            this.GameHandle.MaximumSize = new System.Drawing.Size(600, 600);
            this.GameHandle.MinimumSize = new System.Drawing.Size(600, 600);
            this.GameHandle.Name = "GameHandle";
            this.GameHandle.Size = new System.Drawing.Size(600, 600);
            this.GameHandle.TabIndex = 0;
            this.GameHandle.TabStop = false;
            this.GameHandle.Click += new System.EventHandler(this.GameHandle_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.EnemiesListView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Up, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.Down, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(606, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.544933F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.45507F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 271F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(223, 564);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enemies Order:";
            // 
            // EnemiesListView
            // 
            this.EnemiesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EnemiesListView.Location = new System.Drawing.Point(3, 17);
            this.EnemiesListView.Name = "EnemiesListView";
            this.EnemiesListView.Size = new System.Drawing.Size(217, 239);
            this.EnemiesListView.TabIndex = 1;
            // 
            // Up
            // 
            this.Up.AutoSize = true;
            this.Up.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Up.Dock = System.Windows.Forms.DockStyle.Top;
            this.Up.Location = new System.Drawing.Point(3, 262);
            this.Up.Name = "Up";
            this.Up.Size = new System.Drawing.Size(217, 23);
            this.Up.TabIndex = 2;
            this.Up.Text = "Up";
            this.Up.UseVisualStyleBackColor = true;
            this.Up.Click += new System.EventHandler(this.Up_Click);
            // 
            // Down
            // 
            this.Down.AutoSize = true;
            this.Down.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Down.Dock = System.Windows.Forms.DockStyle.Top;
            this.Down.Location = new System.Drawing.Point(3, 295);
            this.Down.Name = "Down";
            this.Down.Size = new System.Drawing.Size(217, 23);
            this.Down.TabIndex = 3;
            this.Down.Text = "Down";
            this.Down.UseVisualStyleBackColor = true;
            this.Down.Click += new System.EventHandler(this.Down_Click);
            // 
            // GroupEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 564);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.GameHandle);
            this.MaximumSize = new System.Drawing.Size(845, 600);
            this.MinimumSize = new System.Drawing.Size(845, 600);
            this.Name = "GroupEditor";
            this.Text = "GroupEditor";
            this.Load += new System.EventHandler(this.GroupEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GameHandle)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox GameHandle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView EnemiesListView;
        private System.Windows.Forms.Button Up;
        private System.Windows.Forms.Button Down;
    }
}