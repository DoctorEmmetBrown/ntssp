namespace FormationEditor
{
    partial class MainEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainEditorWindow));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.AddPathButton = new System.Windows.Forms.Button();
            this.RemovePathButton = new System.Windows.Forms.Button();
            this.Global = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.AnimateButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.ColorLabel = new System.Windows.Forms.Label();
            this.ColorSlider = new System.Windows.Forms.TrackBar();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.SizeSlider = new System.Windows.Forms.TrackBar();
            this.AddMonsterButton = new System.Windows.Forms.Button();
            this.ChangeMonsterButton = new System.Windows.Forms.Button();
            this.DeleteMonsterButton = new System.Windows.Forms.Button();
            this.MonstersListView = new TreeViewMS.TreeViewMS();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.SpeedLabel = new System.Windows.Forms.Label();
            this.DiffTimeLabel = new System.Windows.Forms.Label();
            this.EffectTypeLabel = new System.Windows.Forms.Label();
            this.SpeedSlider = new System.Windows.Forms.TrackBar();
            this.DiffTimeSlider = new System.Windows.Forms.TrackBar();
            this.EffectTypeComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.AddGroupButton = new System.Windows.Forms.Button();
            this.EditGroupButton = new System.Windows.Forms.Button();
            this.ChangeGroupButton = new System.Windows.Forms.Button();
            this.DeleteGroupButton = new System.Windows.Forms.Button();
            this.GroupsListView = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AssociateButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.AssociationTypeComboBox = new System.Windows.Forms.ComboBox();
            this.TimeBeforeLabel = new System.Windows.Forms.Label();
            this.TimeBeforeSlider = new System.Windows.Forms.TrackBar();
            this.AssociationGroupsListView = new System.Windows.Forms.TreeView();
            this.PathsListView = new System.Windows.Forms.TreeView();
            this.label6 = new System.Windows.Forms.Label();
            this.AssociationListView = new System.Windows.Forms.TreeView();
            this.Remove = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.GlobalUpButton = new System.Windows.Forms.Button();
            this.GlobalDownButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.UpCurrentOrderButton = new System.Windows.Forms.Button();
            this.DownCurrentOrderButton = new System.Windows.Forms.Button();
            this.GlobalGroupOrderListView = new System.Windows.Forms.TreeView();
            this.CurrentOrderListView = new System.Windows.Forms.TreeView();
            this.GameHandle = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.Global.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColorSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeSlider)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiffTimeSlider)).BeginInit();
            this.tableLayoutPanel7.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeBeforeSlider)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameHandle)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 600F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.GameHandle, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1057, 814);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint_1);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(603, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(451, 808);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(443, 782);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Paths";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.Global, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(437, 776);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 203);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Path";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.AddPathButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.RemovePathButton, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(425, 184);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // AddPathButton
            // 
            this.AddPathButton.AutoSize = true;
            this.AddPathButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AddPathButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.AddPathButton.Location = new System.Drawing.Point(3, 3);
            this.AddPathButton.Name = "AddPathButton";
            this.AddPathButton.Size = new System.Drawing.Size(419, 23);
            this.AddPathButton.TabIndex = 0;
            this.AddPathButton.Text = "Add Path";
            this.AddPathButton.UseVisualStyleBackColor = true;
            this.AddPathButton.Click += new System.EventHandler(this.AddPathButton_Click);
            // 
            // RemovePathButton
            // 
            this.RemovePathButton.AutoSize = true;
            this.RemovePathButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RemovePathButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.RemovePathButton.Location = new System.Drawing.Point(3, 95);
            this.RemovePathButton.Name = "RemovePathButton";
            this.RemovePathButton.Size = new System.Drawing.Size(419, 23);
            this.RemovePathButton.TabIndex = 1;
            this.RemovePathButton.Text = "Remove Path";
            this.RemovePathButton.UseVisualStyleBackColor = true;
            this.RemovePathButton.Click += new System.EventHandler(this.RemovePathButton_Click);
            // 
            // Global
            // 
            this.Global.Controls.Add(this.tableLayoutPanel3);
            this.Global.Dock = System.Windows.Forms.DockStyle.Top;
            this.Global.Location = new System.Drawing.Point(3, 391);
            this.Global.Name = "Global";
            this.Global.Size = new System.Drawing.Size(431, 237);
            this.Global.TabIndex = 1;
            this.Global.TabStop = false;
            this.Global.Text = "Global";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.AnimateButton, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.LoadButton, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.SaveButton, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(425, 218);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // AnimateButton
            // 
            this.AnimateButton.AutoSize = true;
            this.AnimateButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AnimateButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.AnimateButton.Location = new System.Drawing.Point(3, 3);
            this.AnimateButton.Name = "AnimateButton";
            this.AnimateButton.Size = new System.Drawing.Size(419, 23);
            this.AnimateButton.TabIndex = 0;
            this.AnimateButton.Text = "Animate";
            this.AnimateButton.UseVisualStyleBackColor = true;
            this.AnimateButton.Click += new System.EventHandler(this.AnimateButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.AutoSize = true;
            this.LoadButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.LoadButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.LoadButton.Location = new System.Drawing.Point(3, 85);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(419, 23);
            this.LoadButton.TabIndex = 1;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.AutoSize = true;
            this.SaveButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SaveButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.SaveButton.Location = new System.Drawing.Point(3, 167);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(419, 23);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(443, 782);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Formation";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel8);
            this.groupBox3.Location = new System.Drawing.Point(29, 383);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(378, 384);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Monsters";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.ColorLabel, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.ColorSlider, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.SizeLabel, 0, 3);
            this.tableLayoutPanel8.Controls.Add(this.SizeSlider, 0, 4);
            this.tableLayoutPanel8.Controls.Add(this.AddMonsterButton, 0, 5);
            this.tableLayoutPanel8.Controls.Add(this.ChangeMonsterButton, 0, 6);
            this.tableLayoutPanel8.Controls.Add(this.DeleteMonsterButton, 0, 7);
            this.tableLayoutPanel8.Controls.Add(this.MonstersListView, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 8;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.94574F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.05426F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(372, 365);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // ColorLabel
            // 
            this.ColorLabel.AutoSize = true;
            this.ColorLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ColorLabel.Location = new System.Drawing.Point(3, 106);
            this.ColorLabel.Name = "ColorLabel";
            this.ColorLabel.Size = new System.Drawing.Size(366, 13);
            this.ColorLabel.TabIndex = 1;
            this.ColorLabel.Text = "Color :1";
            this.ColorLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // ColorSlider
            // 
            this.ColorSlider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColorSlider.Location = new System.Drawing.Point(3, 130);
            this.ColorSlider.Maximum = 5;
            this.ColorSlider.Minimum = 1;
            this.ColorSlider.Name = "ColorSlider";
            this.ColorSlider.Size = new System.Drawing.Size(366, 49);
            this.ColorSlider.TabIndex = 2;
            this.ColorSlider.Value = 1;
            this.ColorSlider.Scroll += new System.EventHandler(this.ColorSlider_Scroll);
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SizeLabel.Location = new System.Drawing.Point(3, 182);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(366, 27);
            this.SizeLabel.TabIndex = 3;
            this.SizeLabel.Text = "Size :1";
            // 
            // SizeSlider
            // 
            this.SizeSlider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SizeSlider.Location = new System.Drawing.Point(3, 212);
            this.SizeSlider.Maximum = 5;
            this.SizeSlider.Minimum = 1;
            this.SizeSlider.Name = "SizeSlider";
            this.SizeSlider.Size = new System.Drawing.Size(366, 41);
            this.SizeSlider.TabIndex = 4;
            this.SizeSlider.Value = 1;
            this.SizeSlider.Scroll += new System.EventHandler(this.SizeSlider_Scroll);
            // 
            // AddMonsterButton
            // 
            this.AddMonsterButton.AutoSize = true;
            this.AddMonsterButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddMonsterButton.Location = new System.Drawing.Point(3, 259);
            this.AddMonsterButton.Name = "AddMonsterButton";
            this.AddMonsterButton.Size = new System.Drawing.Size(366, 22);
            this.AddMonsterButton.TabIndex = 5;
            this.AddMonsterButton.Text = "Add";
            this.AddMonsterButton.UseVisualStyleBackColor = true;
            this.AddMonsterButton.Click += new System.EventHandler(this.AddMonsterButton_Click);
            // 
            // ChangeMonsterButton
            // 
            this.ChangeMonsterButton.AutoSize = true;
            this.ChangeMonsterButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.ChangeMonsterButton.Location = new System.Drawing.Point(3, 287);
            this.ChangeMonsterButton.Name = "ChangeMonsterButton";
            this.ChangeMonsterButton.Size = new System.Drawing.Size(366, 23);
            this.ChangeMonsterButton.TabIndex = 6;
            this.ChangeMonsterButton.Text = "Change";
            this.ChangeMonsterButton.UseVisualStyleBackColor = true;
            this.ChangeMonsterButton.Click += new System.EventHandler(this.ChangeMonsterButton_Click);
            // 
            // DeleteMonsterButton
            // 
            this.DeleteMonsterButton.AutoSize = true;
            this.DeleteMonsterButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DeleteMonsterButton.Location = new System.Drawing.Point(3, 318);
            this.DeleteMonsterButton.Name = "DeleteMonsterButton";
            this.DeleteMonsterButton.Size = new System.Drawing.Size(366, 23);
            this.DeleteMonsterButton.TabIndex = 7;
            this.DeleteMonsterButton.Text = "Delete";
            this.DeleteMonsterButton.UseVisualStyleBackColor = true;
            this.DeleteMonsterButton.Click += new System.EventHandler(this.DeleteMonsterButton_Click);
            // 
            // MonstersListView
            // 
            this.MonstersListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MonstersListView.Location = new System.Drawing.Point(3, 3);
            this.MonstersListView.Name = "MonstersListView";
            this.MonstersListView.SelectedNodes = ((System.Collections.ArrayList)(resources.GetObject("MonstersListView.SelectedNodes")));
            this.MonstersListView.Size = new System.Drawing.Size(366, 100);
            this.MonstersListView.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel5);
            this.groupBox2.Location = new System.Drawing.Point(19, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(395, 357);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Groups";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel7, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.GroupsListView, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(389, 338);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tableLayoutPanel6.Controls.Add(this.SpeedLabel, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.DiffTimeLabel, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.EffectTypeLabel, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.SpeedSlider, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.DiffTimeSlider, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.EffectTypeComboBox, 2, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 111);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.62745F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.37255F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(383, 102);
            this.tableLayoutPanel6.TabIndex = 1;
            this.tableLayoutPanel6.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel6_Paint);
            // 
            // SpeedLabel
            // 
            this.SpeedLabel.AutoSize = true;
            this.SpeedLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SpeedLabel.Location = new System.Drawing.Point(3, 0);
            this.SpeedLabel.Name = "SpeedLabel";
            this.SpeedLabel.Size = new System.Drawing.Size(124, 13);
            this.SpeedLabel.TabIndex = 0;
            this.SpeedLabel.Text = "Speed: 0%";
            this.SpeedLabel.Click += new System.EventHandler(this.SpeedLabel_Click);
            // 
            // DiffTimeLabel
            // 
            this.DiffTimeLabel.AutoSize = true;
            this.DiffTimeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.DiffTimeLabel.Location = new System.Drawing.Point(133, 0);
            this.DiffTimeLabel.Name = "DiffTimeLabel";
            this.DiffTimeLabel.Size = new System.Drawing.Size(124, 13);
            this.DiffTimeLabel.TabIndex = 1;
            this.DiffTimeLabel.Text = "DiffTime: 0,0s";
            // 
            // EffectTypeLabel
            // 
            this.EffectTypeLabel.AutoSize = true;
            this.EffectTypeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.EffectTypeLabel.Location = new System.Drawing.Point(263, 0);
            this.EffectTypeLabel.Name = "EffectTypeLabel";
            this.EffectTypeLabel.Size = new System.Drawing.Size(117, 13);
            this.EffectTypeLabel.TabIndex = 2;
            this.EffectTypeLabel.Text = "Effect Type:";
            // 
            // SpeedSlider
            // 
            this.SpeedSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.SpeedSlider.Location = new System.Drawing.Point(3, 21);
            this.SpeedSlider.Maximum = 500;
            this.SpeedSlider.Name = "SpeedSlider";
            this.SpeedSlider.Size = new System.Drawing.Size(124, 45);
            this.SpeedSlider.TabIndex = 3;
            this.SpeedSlider.Scroll += new System.EventHandler(this.SpeedSlider_Scroll);
            // 
            // DiffTimeSlider
            // 
            this.DiffTimeSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.DiffTimeSlider.Location = new System.Drawing.Point(133, 21);
            this.DiffTimeSlider.Maximum = 5000;
            this.DiffTimeSlider.Name = "DiffTimeSlider";
            this.DiffTimeSlider.Size = new System.Drawing.Size(124, 45);
            this.DiffTimeSlider.TabIndex = 4;
            this.DiffTimeSlider.Scroll += new System.EventHandler(this.DiffTimeSlider_Scroll);
            // 
            // EffectTypeComboBox
            // 
            this.EffectTypeComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.EffectTypeComboBox.FormattingEnabled = true;
            this.EffectTypeComboBox.Items.AddRange(new object[] {
            "Normal",
            "Circle",
            "Switch",
            "Arc",
            "Rotate",
            "Fixed"});
            this.EffectTypeComboBox.Location = new System.Drawing.Point(263, 21);
            this.EffectTypeComboBox.Name = "EffectTypeComboBox";
            this.EffectTypeComboBox.Size = new System.Drawing.Size(117, 21);
            this.EffectTypeComboBox.TabIndex = 5;
            this.EffectTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.EffectTypeComboBox_SelectedIndexChanged);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.AddGroupButton, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.EditGroupButton, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.ChangeGroupButton, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.DeleteGroupButton, 0, 3);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 219);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 4;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.61905F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.38095F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(383, 116);
            this.tableLayoutPanel7.TabIndex = 2;
            // 
            // AddGroupButton
            // 
            this.AddGroupButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.AddGroupButton.Location = new System.Drawing.Point(3, 3);
            this.AddGroupButton.Name = "AddGroupButton";
            this.AddGroupButton.Size = new System.Drawing.Size(377, 21);
            this.AddGroupButton.TabIndex = 0;
            this.AddGroupButton.Text = "Add";
            this.AddGroupButton.UseVisualStyleBackColor = true;
            this.AddGroupButton.Click += new System.EventHandler(this.AddGroupButton_Click);
            // 
            // EditGroupButton
            // 
            this.EditGroupButton.AutoSize = true;
            this.EditGroupButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.EditGroupButton.Location = new System.Drawing.Point(3, 30);
            this.EditGroupButton.Name = "EditGroupButton";
            this.EditGroupButton.Size = new System.Drawing.Size(377, 23);
            this.EditGroupButton.TabIndex = 1;
            this.EditGroupButton.Text = "Edit";
            this.EditGroupButton.UseVisualStyleBackColor = true;
            this.EditGroupButton.Click += new System.EventHandler(this.EditGroupButton_Click);
            // 
            // ChangeGroupButton
            // 
            this.ChangeGroupButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.ChangeGroupButton.Location = new System.Drawing.Point(3, 59);
            this.ChangeGroupButton.Name = "ChangeGroupButton";
            this.ChangeGroupButton.Size = new System.Drawing.Size(377, 23);
            this.ChangeGroupButton.TabIndex = 2;
            this.ChangeGroupButton.Text = "Change";
            this.ChangeGroupButton.UseVisualStyleBackColor = true;
            this.ChangeGroupButton.Click += new System.EventHandler(this.ChangeGroupButton_Click);
            // 
            // DeleteGroupButton
            // 
            this.DeleteGroupButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DeleteGroupButton.Location = new System.Drawing.Point(3, 90);
            this.DeleteGroupButton.Name = "DeleteGroupButton";
            this.DeleteGroupButton.Size = new System.Drawing.Size(377, 23);
            this.DeleteGroupButton.TabIndex = 3;
            this.DeleteGroupButton.Text = "Delete";
            this.DeleteGroupButton.UseVisualStyleBackColor = true;
            this.DeleteGroupButton.Click += new System.EventHandler(this.DeleteGroupButton_Click);
            // 
            // GroupsListView
            // 
            this.GroupsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupsListView.Location = new System.Drawing.Point(3, 3);
            this.GroupsListView.Name = "GroupsListView";
            this.GroupsListView.Size = new System.Drawing.Size(383, 102);
            this.GroupsListView.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel9);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(443, 782);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Association";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel9.Controls.Add(this.AssociateButton, 0, 4);
            this.tableLayoutPanel9.Controls.Add(this.label3, 0, 5);
            this.tableLayoutPanel9.Controls.Add(this.AssociationTypeComboBox, 0, 6);
            this.tableLayoutPanel9.Controls.Add(this.TimeBeforeLabel, 0, 7);
            this.tableLayoutPanel9.Controls.Add(this.TimeBeforeSlider, 0, 8);
            this.tableLayoutPanel9.Controls.Add(this.AssociationGroupsListView, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.PathsListView, 0, 3);
            this.tableLayoutPanel9.Controls.Add(this.label6, 0, 9);
            this.tableLayoutPanel9.Controls.Add(this.AssociationListView, 0, 10);
            this.tableLayoutPanel9.Controls.Add(this.Remove, 0, 11);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 12;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.926407F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.07359F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 153F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 177F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(437, 776);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(431, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Groups";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(431, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Paths";
            // 
            // AssociateButton
            // 
            this.AssociateButton.AutoSize = true;
            this.AssociateButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.AssociateButton.Location = new System.Drawing.Point(3, 406);
            this.AssociateButton.Name = "AssociateButton";
            this.AssociateButton.Size = new System.Drawing.Size(431, 23);
            this.AssociateButton.TabIndex = 4;
            this.AssociateButton.Text = "Associate";
            this.AssociateButton.UseVisualStyleBackColor = true;
            this.AssociateButton.Click += new System.EventHandler(this.AssociateButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(3, 436);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(431, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Type";
            // 
            // AssociationTypeComboBox
            // 
            this.AssociationTypeComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.AssociationTypeComboBox.FormattingEnabled = true;
            this.AssociationTypeComboBox.Items.AddRange(new object[] {
            "Normal",
            "When Player is Found"});
            this.AssociationTypeComboBox.Location = new System.Drawing.Point(3, 458);
            this.AssociationTypeComboBox.Name = "AssociationTypeComboBox";
            this.AssociationTypeComboBox.Size = new System.Drawing.Size(431, 21);
            this.AssociationTypeComboBox.TabIndex = 6;
            this.AssociationTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // TimeBeforeLabel
            // 
            this.TimeBeforeLabel.AutoSize = true;
            this.TimeBeforeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TimeBeforeLabel.Location = new System.Drawing.Point(3, 486);
            this.TimeBeforeLabel.Name = "TimeBeforeLabel";
            this.TimeBeforeLabel.Size = new System.Drawing.Size(431, 13);
            this.TimeBeforeLabel.TabIndex = 7;
            this.TimeBeforeLabel.Text = "Time Before: 0.0s";
            // 
            // TimeBeforeSlider
            // 
            this.TimeBeforeSlider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TimeBeforeSlider.Location = new System.Drawing.Point(3, 510);
            this.TimeBeforeSlider.Maximum = 5000;
            this.TimeBeforeSlider.Name = "TimeBeforeSlider";
            this.TimeBeforeSlider.Size = new System.Drawing.Size(431, 38);
            this.TimeBeforeSlider.TabIndex = 8;
            this.TimeBeforeSlider.Scroll += new System.EventHandler(this.TimeBeforeSlider_Scroll);
            // 
            // AssociationGroupsListView
            // 
            this.AssociationGroupsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AssociationGroupsListView.Location = new System.Drawing.Point(3, 19);
            this.AssociationGroupsListView.Name = "AssociationGroupsListView";
            this.AssociationGroupsListView.Size = new System.Drawing.Size(431, 209);
            this.AssociationGroupsListView.TabIndex = 9;
            // 
            // PathsListView
            // 
            this.PathsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PathsListView.Location = new System.Drawing.Point(3, 253);
            this.PathsListView.Name = "PathsListView";
            this.PathsListView.Size = new System.Drawing.Size(431, 147);
            this.PathsListView.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(3, 551);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(431, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Associations";
            // 
            // AssociationListView
            // 
            this.AssociationListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AssociationListView.Location = new System.Drawing.Point(3, 572);
            this.AssociationListView.Name = "AssociationListView";
            this.AssociationListView.Size = new System.Drawing.Size(431, 171);
            this.AssociationListView.TabIndex = 12;
            // 
            // Remove
            // 
            this.Remove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Remove.Location = new System.Drawing.Point(3, 749);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(431, 24);
            this.Remove.TabIndex = 13;
            this.Remove.Text = "Remove Association";
            this.Remove.UseVisualStyleBackColor = true;
            this.Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tableLayoutPanel10);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(443, 782);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "TimeLine";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.GlobalUpButton, 0, 2);
            this.tableLayoutPanel10.Controls.Add(this.GlobalDownButton, 0, 3);
            this.tableLayoutPanel10.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel10.Controls.Add(this.UpCurrentOrderButton, 0, 6);
            this.tableLayoutPanel10.Controls.Add(this.DownCurrentOrderButton, 0, 7);
            this.tableLayoutPanel10.Controls.Add(this.GlobalGroupOrderListView, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.CurrentOrderListView, 0, 5);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 8;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.677165F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 97.32284F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(437, 776);
            this.tableLayoutPanel10.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(431, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Global Group Order:";
            // 
            // GlobalUpButton
            // 
            this.GlobalUpButton.AutoSize = true;
            this.GlobalUpButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.GlobalUpButton.Location = new System.Drawing.Point(3, 458);
            this.GlobalUpButton.Name = "GlobalUpButton";
            this.GlobalUpButton.Size = new System.Drawing.Size(431, 23);
            this.GlobalUpButton.TabIndex = 2;
            this.GlobalUpButton.Text = "Up";
            this.GlobalUpButton.UseVisualStyleBackColor = true;
            this.GlobalUpButton.Click += new System.EventHandler(this.GlobalUpButton_Click);
            // 
            // GlobalDownButton
            // 
            this.GlobalDownButton.AutoSize = true;
            this.GlobalDownButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.GlobalDownButton.Location = new System.Drawing.Point(3, 488);
            this.GlobalDownButton.Name = "GlobalDownButton";
            this.GlobalDownButton.Size = new System.Drawing.Size(431, 23);
            this.GlobalDownButton.TabIndex = 3;
            this.GlobalDownButton.Text = "Down";
            this.GlobalDownButton.UseVisualStyleBackColor = true;
            this.GlobalDownButton.Click += new System.EventHandler(this.GlobalDownButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(3, 516);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(431, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Time order for the current group:";
            // 
            // UpCurrentOrderButton
            // 
            this.UpCurrentOrderButton.AutoSize = true;
            this.UpCurrentOrderButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.UpCurrentOrderButton.Location = new System.Drawing.Point(3, 699);
            this.UpCurrentOrderButton.Name = "UpCurrentOrderButton";
            this.UpCurrentOrderButton.Size = new System.Drawing.Size(431, 24);
            this.UpCurrentOrderButton.TabIndex = 7;
            this.UpCurrentOrderButton.Text = "Up";
            this.UpCurrentOrderButton.UseVisualStyleBackColor = true;
            this.UpCurrentOrderButton.Click += new System.EventHandler(this.UpCurrentOrderButton_Click);
            // 
            // DownCurrentOrderButton
            // 
            this.DownCurrentOrderButton.AutoSize = true;
            this.DownCurrentOrderButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DownCurrentOrderButton.Location = new System.Drawing.Point(3, 732);
            this.DownCurrentOrderButton.Name = "DownCurrentOrderButton";
            this.DownCurrentOrderButton.Size = new System.Drawing.Size(431, 23);
            this.DownCurrentOrderButton.TabIndex = 8;
            this.DownCurrentOrderButton.Text = "Down";
            this.DownCurrentOrderButton.UseVisualStyleBackColor = true;
            this.DownCurrentOrderButton.Click += new System.EventHandler(this.DownCurrentOrderButton_Click);
            // 
            // GlobalGroupOrderListView
            // 
            this.GlobalGroupOrderListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GlobalGroupOrderListView.Location = new System.Drawing.Point(3, 15);
            this.GlobalGroupOrderListView.Name = "GlobalGroupOrderListView";
            this.GlobalGroupOrderListView.Size = new System.Drawing.Size(431, 437);
            this.GlobalGroupOrderListView.TabIndex = 9;
            this.GlobalGroupOrderListView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.GlobalGroupOrderListView_AfterSelect);
            // 
            // CurrentOrderListView
            // 
            this.CurrentOrderListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurrentOrderListView.Location = new System.Drawing.Point(3, 539);
            this.CurrentOrderListView.Name = "CurrentOrderListView";
            this.CurrentOrderListView.Size = new System.Drawing.Size(431, 154);
            this.CurrentOrderListView.TabIndex = 10;
            // 
            // GameHandle
            // 
            this.GameHandle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GameHandle.Location = new System.Drawing.Point(3, 3);
            this.GameHandle.MaximumSize = new System.Drawing.Size(600, 600);
            this.GameHandle.MinimumSize = new System.Drawing.Size(600, 600);
            this.GameHandle.Name = "GameHandle";
            this.GameHandle.Size = new System.Drawing.Size(600, 600);
            this.GameHandle.TabIndex = 1;
            this.GameHandle.TabStop = false;
            this.GameHandle.Click += new System.EventHandler(this.GameHandle_Click);
            // 
            // MainEditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 814);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(1073, 850);
            this.Name = "MainEditorWindow";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.Global.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColorSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeSlider)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiffTimeSlider)).EndInit();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeBeforeSlider)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameHandle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox GameHandle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button AddPathButton;
        private System.Windows.Forms.Button RemovePathButton;
        private System.Windows.Forms.GroupBox Global;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button AnimateButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label SpeedLabel;
        private System.Windows.Forms.Label DiffTimeLabel;
        private System.Windows.Forms.Label EffectTypeLabel;
        private System.Windows.Forms.TrackBar SpeedSlider;
        private System.Windows.Forms.TrackBar DiffTimeSlider;
        private System.Windows.Forms.ComboBox EffectTypeComboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Button AddGroupButton;
        private System.Windows.Forms.Button EditGroupButton;
        private System.Windows.Forms.Button ChangeGroupButton;
        private System.Windows.Forms.Button DeleteGroupButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button AssociateButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox AssociationTypeComboBox;
        private System.Windows.Forms.Label TimeBeforeLabel;
        private System.Windows.Forms.TrackBar TimeBeforeSlider;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button GlobalUpButton;
        private System.Windows.Forms.Button GlobalDownButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button UpCurrentOrderButton;
        private System.Windows.Forms.Button DownCurrentOrderButton;
        private System.Windows.Forms.TreeView GroupsListView;
        private System.Windows.Forms.TreeView AssociationGroupsListView;
        private System.Windows.Forms.TreeView PathsListView;
        private System.Windows.Forms.TreeView GlobalGroupOrderListView;
        private System.Windows.Forms.TreeView CurrentOrderListView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label ColorLabel;
        private System.Windows.Forms.TrackBar ColorSlider;
        private System.Windows.Forms.Label SizeLabel;
        private System.Windows.Forms.TrackBar SizeSlider;
        private System.Windows.Forms.Button AddMonsterButton;
        private System.Windows.Forms.Button ChangeMonsterButton;
        private System.Windows.Forms.Button DeleteMonsterButton;
        private TreeViewMS.TreeViewMS MonstersListView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TreeView AssociationListView;
        private System.Windows.Forms.Button Remove;

    }
}

