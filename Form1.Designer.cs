namespace blendovideoknife
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            label1 = new Label();
            textBox_padding = new TextBox();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            contextMenuStrip_datagrid = new ContextMenuStrip(components);
            toolStripMenuItem2 = new ToolStripMenuItem();
            checkBox1 = new CheckBox();
            listBox1 = new ListBox();
            contextMenuStrip_listbox = new ContextMenuStrip(components);
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            linkLabel1 = new LinkLabel();
            splitContainer1 = new SplitContainer();
            copyAllEntriesToClipboardToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            contextMenuStrip_datagrid.SuspendLayout();
            contextMenuStrip_listbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(560, 30);
            label1.TabIndex = 0;
            label1.Text = "Fill information, then drag video file into this window.";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBox_padding
            // 
            textBox_padding.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_padding.Location = new Point(153, 46);
            textBox_padding.Name = "textBox_padding";
            textBox_padding.Size = new Size(55, 25);
            textBox_padding.TabIndex = 1;
            textBox_padding.Text = "2";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 51);
            label2.Name = "label2";
            label2.Size = new Size(135, 15);
            label2.TabIndex = 2;
            label2.Text = "Padding time (seconds):";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
            dataGridView1.ContextMenuStrip = contextMenuStrip_datagrid;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Size = new Size(554, 497);
            dataGridView1.TabIndex = 3;
            // 
            // Column1
            // 
            Column1.HeaderText = "Start time";
            Column1.Name = "Column1";
            Column1.Width = 90;
            // 
            // Column2
            // 
            Column2.HeaderText = "End time";
            Column2.Name = "Column2";
            Column2.Width = 90;
            // 
            // Column3
            // 
            Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column3.HeaderText = "New filename";
            Column3.Name = "Column3";
            // 
            // contextMenuStrip_datagrid
            // 
            contextMenuStrip_datagrid.Items.AddRange(new ToolStripItem[] { copyAllEntriesToClipboardToolStripMenuItem, toolStripMenuItem2 });
            contextMenuStrip_datagrid.Name = "contextMenuStrip_datagrid";
            contextMenuStrip_datagrid.Size = new Size(223, 70);
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(222, 22);
            toolStripMenuItem2.Text = "Clear all entries";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // checkBox1
            // 
            checkBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(389, 52);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(183, 19);
            checkBox1.TabIndex = 4;
            checkBox1.Text = "Auto-populate next start time";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBox1.ContextMenuStrip = contextMenuStrip_listbox;
            listBox1.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(3, 3);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(554, 154);
            listBox1.TabIndex = 5;
            // 
            // contextMenuStrip_listbox
            // 
            contextMenuStrip_listbox.Items.AddRange(new ToolStripItem[] { toolStripMenuItem3, toolStripMenuItem1 });
            contextMenuStrip_listbox.Name = "contextMenuStrip_listbox";
            contextMenuStrip_listbox.Size = new Size(190, 48);
            contextMenuStrip_listbox.Opening += contextMenuStrip_listbox_Opening;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(189, 22);
            toolStripMenuItem3.Text = "Copy log to clipboard";
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(189, 22);
            toolStripMenuItem1.Text = "Clear log";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel1.Location = new Point(544, 9);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(25, 21);
            linkLabel1.TabIndex = 6;
            linkLabel1.TabStop = true;
            linkLabel1.Text = " ? ";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(12, 77);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(listBox1);
            splitContainer1.Size = new Size(560, 672);
            splitContainer1.SplitterDistance = 503;
            splitContainer1.TabIndex = 7;
            // 
            // copyAllEntriesToClipboardToolStripMenuItem
            // 
            copyAllEntriesToClipboardToolStripMenuItem.Name = "copyAllEntriesToClipboardToolStripMenuItem";
            copyAllEntriesToClipboardToolStripMenuItem.Size = new Size(222, 22);
            copyAllEntriesToClipboardToolStripMenuItem.Text = "Copy all entries to clipboard";
            copyAllEntriesToClipboardToolStripMenuItem.Click += copyAllEntriesToClipboardToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 761);
            Controls.Add(splitContainer1);
            Controls.Add(linkLabel1);
            Controls.Add(checkBox1);
            Controls.Add(label2);
            Controls.Add(textBox_padding);
            Controls.Add(label1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Blendo Video Knife";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            contextMenuStrip_datagrid.ResumeLayout(false);
            contextMenuStrip_listbox.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private CheckBox checkBox1;
        private ListBox listBox1;
        private LinkLabel linkLabel1;
        private TextBox textBox_padding;
        private SplitContainer splitContainer1;
        private ContextMenuStrip contextMenuStrip_listbox;
        private ToolStripMenuItem toolStripMenuItem1;
        private ContextMenuStrip contextMenuStrip_datagrid;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem copyAllEntriesToClipboardToolStripMenuItem;
    }
}
