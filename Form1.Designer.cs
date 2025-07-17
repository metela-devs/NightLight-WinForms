namespace NightLight;

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
        this.components = new System.ComponentModel.Container();
        this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toggleFilterButton = new System.Windows.Forms.Button();
        this.copyrightLabel = new System.Windows.Forms.LinkLabel();
        this.themeToggleButton = new System.Windows.Forms.Button();
        this.titleBarPanel = new System.Windows.Forms.Panel();
        this.closeButton = new System.Windows.Forms.Button();
        this.titleLabel = new System.Windows.Forms.Label();
        this.contextMenuStrip1.SuspendLayout();
        this.titleBarPanel.SuspendLayout();
        this.SuspendLayout();
        // 
        // notifyIcon1
        // 
        this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
        this.notifyIcon1.Text = "Night Light";
        this.notifyIcon1.Visible = true;
        this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
        // 
        // contextMenuStrip1
        // 
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem});
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.Size = new System.Drawing.Size(104, 48);
        // 
        // showToolStripMenuItem
        // 
        this.showToolStripMenuItem.Name = "showToolStripMenuItem";
        this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
        this.showToolStripMenuItem.Text = "Show";
        this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
        // 
        // exitToolStripMenuItem
        // 
        this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
        this.exitToolStripMenuItem.Text = "Exit";
        this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
        // 
        // toggleFilterButton
        // 
        this.toggleFilterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.toggleFilterButton.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.toggleFilterButton.Location = new System.Drawing.Point(50, 70);
        this.toggleFilterButton.Name = "toggleFilterButton";
        this.toggleFilterButton.Size = new System.Drawing.Size(200, 50);
        this.toggleFilterButton.TabIndex = 0;
        this.toggleFilterButton.Text = "Turn On Filter";
        this.toggleFilterButton.UseVisualStyleBackColor = true;
        this.toggleFilterButton.Click += new System.EventHandler(this.toggleFilterButton_Click);
        // 
        // copyrightLabel
        // 
        this.copyrightLabel.ActiveLinkColor = System.Drawing.Color.DodgerBlue;
        this.copyrightLabel.AutoSize = true;
        this.copyrightLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.copyrightLabel.LinkColor = System.Drawing.Color.Gray;
        this.copyrightLabel.Location = new System.Drawing.Point(110, 175);
        this.copyrightLabel.Name = "copyrightLabel";
        this.copyrightLabel.Size = new System.Drawing.Size(76, 15);
        this.copyrightLabel.TabIndex = 1;
        this.copyrightLabel.TabStop = true;
        this.copyrightLabel.Text = "Metela devs";
        this.copyrightLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.copyrightLabel_LinkClicked);
        // 
        // themeToggleButton
        // 
        this.themeToggleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.themeToggleButton.Location = new System.Drawing.Point(12, 168);
        this.themeToggleButton.Name = "themeToggleButton";
        this.themeToggleButton.Size = new System.Drawing.Size(75, 23);
        this.themeToggleButton.TabIndex = 2;
        this.themeToggleButton.Text = "Theme";
        this.themeToggleButton.UseVisualStyleBackColor = true;
        this.themeToggleButton.Click += new System.EventHandler(this.themeToggleButton_Click);
        // 
        // titleBarPanel
        // 
        this.titleBarPanel.Controls.Add(this.closeButton);
        this.titleBarPanel.Controls.Add(this.titleLabel);
        this.titleBarPanel.Dock = System.Windows.Forms.DockStyle.Top;
        this.titleBarPanel.Location = new System.Drawing.Point(0, 0);
        this.titleBarPanel.Name = "titleBarPanel";
        this.titleBarPanel.Size = new System.Drawing.Size(300, 40);
        this.titleBarPanel.TabIndex = 3;
        this.titleBarPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleBarPanel_MouseDown);
        // 
        // closeButton
        // 
        this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.closeButton.FlatAppearance.BorderSize = 0;
        this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.closeButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.closeButton.Location = new System.Drawing.Point(260, 0);
        this.closeButton.Name = "closeButton";
        this.closeButton.Size = new System.Drawing.Size(40, 40);
        this.closeButton.TabIndex = 1;
        this.closeButton.Text = "X";
        this.closeButton.UseVisualStyleBackColor = true;
        this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
        // 
        // titleLabel
        // 
        this.titleLabel.AutoSize = true;
        this.titleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.titleLabel.Location = new System.Drawing.Point(12, 10);
        this.titleLabel.Name = "titleLabel";
        this.titleLabel.Size = new System.Drawing.Size(78, 19);
        this.titleLabel.TabIndex = 0;
        this.titleLabel.Text = "Night Light";
        this.titleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleBarPanel_MouseDown);
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(300, 200);
        this.Controls.Add(this.titleBarPanel);
        this.Controls.Add(this.themeToggleButton);
        this.Controls.Add(this.copyrightLabel);
        this.Controls.Add(this.toggleFilterButton);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Night Light";
        this.Load += new System.EventHandler(this.Form1_Load);
        this.contextMenuStrip1.ResumeLayout(false);
        this.titleBarPanel.ResumeLayout(false);
        this.titleBarPanel.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Button toggleFilterButton;
    private System.Windows.Forms.LinkLabel copyrightLabel;
    private System.Windows.Forms.NotifyIcon notifyIcon1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.Button themeToggleButton;
    private System.Windows.Forms.Panel titleBarPanel;
    private System.Windows.Forms.Button closeButton;
    private System.Windows.Forms.Label titleLabel;
}
