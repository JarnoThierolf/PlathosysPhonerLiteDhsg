using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace PlathosysPhonerLite
{
    class PlathosysPhonerLiteContext : ApplicationContext
    {
        // Instances
        PhonerLiteController phonerLiteController;
        PlathosysController plathosysController;

        //Component declarations
        NotifyIcon notifyIcon;
        ContextMenuStrip contextMenuStrip;
        ToolStripMenuItem toolStripMenuItemClose;
        ToolStripMenuItem toolStripMenuItemName;
        ToolStripSeparator toolStripSeparator;

        public PlathosysPhonerLiteContext()
        {
            // Prevent application from running twice
            Process[] result = Process.GetProcessesByName("PlathosysPhonerLite");
            if (result.Length > 1)
            {
                MessageBox.Show("There is already an instance of \"PlathosysPhonerLite\" running.", "Information");
                Environment.Exit(0);
            }

            phonerLiteController = new PhonerLiteController();
            plathosysController = new PlathosysController();

            plathosysController.HookChanged += phonerLiteController.OnHookChanged;
            phonerLiteController.IncomingCall += plathosysController.OnIncomingCall;
            phonerLiteController.DisconnectingCall += plathosysController.OnDisconnectingCall;
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // create instances
            notifyIcon = new NotifyIcon();
            contextMenuStrip = new ContextMenuStrip();
            toolStripMenuItemClose = new ToolStripMenuItem();
            toolStripMenuItemName = new ToolStripMenuItem();
            toolStripSeparator = new ToolStripSeparator();


            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            notifyIcon.Icon = Properties.Resources.Icon;
            notifyIcon.Text = "PlathosysPhonerLite 1.0";
            notifyIcon.Visible = true;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] {
            toolStripMenuItemName,
            toolStripSeparator,
            toolStripMenuItemClose});
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(219, 76);
            // 
            // toolStripMenuItemClose
            // 
            this.toolStripMenuItemClose.Name = "toolStripMenuItemClose";
            this.toolStripMenuItemClose.Size = new Size(218, 22);
            this.toolStripMenuItemClose.Text = "Quit";
            this.toolStripMenuItemClose.Click += new EventHandler(this.toolStripMenuItemClose_Click);
            // 
            // toolStripMenuItemName
            // 
            this.toolStripMenuItemName.Name = "toolStripMenuItemName";
            this.toolStripMenuItemName.Size = new Size(218, 22);
            this.toolStripMenuItemName.Text = "PlathosysPhonerLite 1.0";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new Size(215, 6);
        }

        /// <summary>
        /// Close Application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            //Cleanup so that the icon will be removed when the application is closed
            notifyIcon.Dispose();
        }
    }
}
