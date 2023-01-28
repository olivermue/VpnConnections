namespace VpnConnections.Dialogs
{
    partial class ConfigurationDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationDialog));
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.splitButtonSave = new VpnConnections.Design.SplitButton();
            this.contextMenuStripSave = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemSaveInAppData = new System.Windows.Forms.ToolStripMenuItem();
            this.splitButtonActions = new VpnConnections.Design.SplitButton();
            this.contextMenuStripActions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemToggle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.panelBottom.SuspendLayout();
            this.contextMenuStripSave.SuspendLayout();
            this.contextMenuStripActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid
            // 
            resources.ApplyResources(this.propertyGrid, "propertyGrid");
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.splitButtonSave);
            this.panelBottom.Controls.Add(this.splitButtonActions);
            resources.ApplyResources(this.panelBottom, "panelBottom");
            this.panelBottom.Name = "panelBottom";
            // 
            // splitButtonSave
            // 
            resources.ApplyResources(this.splitButtonSave, "splitButtonSave");
            this.splitButtonSave.ContextMenuStrip = this.contextMenuStripSave;
            this.splitButtonSave.Name = "splitButtonSave";
            this.splitButtonSave.SplitMenuStrip = this.contextMenuStripSave;
            this.splitButtonSave.UseVisualStyleBackColor = true;
            this.splitButtonSave.Click += new System.EventHandler(this.OnButtonSaveClick);
            // 
            // contextMenuStripSave
            // 
            this.contextMenuStripSave.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.contextMenuStripSave.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSaveInAppData});
            this.contextMenuStripSave.Name = "contextMenuStripSave";
            this.contextMenuStripSave.ShowImageMargin = false;
            this.contextMenuStripSave.ShowItemToolTips = false;
            resources.ApplyResources(this.contextMenuStripSave, "contextMenuStripSave");
            // 
            // toolStripMenuItemSaveInAppData
            // 
            this.toolStripMenuItemSaveInAppData.Name = "toolStripMenuItemSaveInAppData";
            resources.ApplyResources(this.toolStripMenuItemSaveInAppData, "toolStripMenuItemSaveInAppData");
            this.toolStripMenuItemSaveInAppData.Click += new System.EventHandler(this.OnButtonSaveInAppDataClick);
            // 
            // splitButtonActions
            // 
            this.splitButtonActions.ContextMenuStrip = this.contextMenuStripActions;
            resources.ApplyResources(this.splitButtonActions, "splitButtonActions");
            this.splitButtonActions.Name = "splitButtonActions";
            this.splitButtonActions.SplitMenuStrip = this.contextMenuStripActions;
            this.splitButtonActions.UseVisualStyleBackColor = true;
            this.splitButtonActions.Click += new System.EventHandler(this.OnButtonCloseClick);
            // 
            // contextMenuStripActions
            // 
            this.contextMenuStripActions.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.contextMenuStripActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemConnect,
            this.toolStripMenuItemDisconnect,
            this.toolStripMenuItemToggle,
            this.toolStripSeparator,
            this.toolStripMenuItemQuit});
            this.contextMenuStripActions.Name = "contextMenuStrip";
            this.contextMenuStripActions.ShowImageMargin = false;
            this.contextMenuStripActions.ShowItemToolTips = false;
            resources.ApplyResources(this.contextMenuStripActions, "contextMenuStripActions");
            // 
            // toolStripMenuItemConnect
            // 
            this.toolStripMenuItemConnect.Name = "toolStripMenuItemConnect";
            resources.ApplyResources(this.toolStripMenuItemConnect, "toolStripMenuItemConnect");
            this.toolStripMenuItemConnect.Click += new System.EventHandler(this.OnButtonConnectClick);
            // 
            // toolStripMenuItemDisconnect
            // 
            this.toolStripMenuItemDisconnect.Name = "toolStripMenuItemDisconnect";
            resources.ApplyResources(this.toolStripMenuItemDisconnect, "toolStripMenuItemDisconnect");
            this.toolStripMenuItemDisconnect.Click += new System.EventHandler(this.OnButtonDisconnectClick);
            // 
            // toolStripMenuItemToggle
            // 
            this.toolStripMenuItemToggle.Name = "toolStripMenuItemToggle";
            resources.ApplyResources(this.toolStripMenuItemToggle, "toolStripMenuItemToggle");
            this.toolStripMenuItemToggle.Click += new System.EventHandler(this.OnButtonToggleClick);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            resources.ApplyResources(this.toolStripSeparator, "toolStripSeparator");
            // 
            // toolStripMenuItemQuit
            // 
            this.toolStripMenuItemQuit.Name = "toolStripMenuItemQuit";
            resources.ApplyResources(this.toolStripMenuItemQuit, "toolStripMenuItemQuit");
            this.toolStripMenuItemQuit.Click += new System.EventHandler(this.OnButtonQuitClick);
            // 
            // ConfigurationDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.panelBottom);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationDialog";
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.contextMenuStripSave.ResumeLayout(false);
            this.contextMenuStripActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PropertyGrid propertyGrid;
        private Panel panelBottom;
        private Design.SplitButton splitButtonActions;
        private ContextMenuStrip contextMenuStripActions;
        private ToolStripMenuItem toolStripMenuItemQuit;
        private ToolStripMenuItem toolStripMenuItemConnect;
        private ToolStripMenuItem toolStripMenuItemDisconnect;
        private ToolStripMenuItem toolStripMenuItemToggle;
        private ToolStripSeparator toolStripSeparator;
        private Design.SplitButton splitButtonSave;
        private ContextMenuStrip contextMenuStripSave;
        private ToolStripMenuItem toolStripMenuItemSaveInAppData;
    }
}