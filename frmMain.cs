using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Panappta {
    public partial class frmMain : Form {
        #region Global Variables
        XmlParser xml = new XmlParser();
        List<string> IgnoredAlerts = new List<string>();
        DateTime LastAlert;
        #endregion

        #region Form Events
        public frmMain() {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e) {
            fMain frMain = new fMain();
            frMain.Show();
            this.Text = string.Format("Panappta - {0} beta by Thomas Mack", Application.ProductVersion);
            lblLast.Text = string.Format("Hello, {0}!", Environment.UserName);
            LastAlert = DateTime.Now.AddDays(-1); // Set our last alert to never..
            DoTick();
        }

        private void frmMain_Resize(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Minimized)
                this.Hide();
        }
        #endregion

        #region Parsing/Worker
        void bWorker_DoWork(object sender, DoWorkEventArgs e) {
            // Create a new parser.

            xml = new XmlParser();
            xml.Read(); // Read the data.
        }

        void bWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            lblLast.Text = "Parsing...";
            if (xml.OutageItems.Count > 0) // Only call if the count is great than 0.
                ProcessResults();

            tbProgress.Visible = false;

        }

        void ProcessResults() {
            string strTmp = null, strUrl = null, strType = null, strMsg = null;
            bool bStop = false;
            ArrayList Tabs = new ArrayList();

            tbOutages.TabPages.Clear(); // Clear our tab pages.

            if (xml.OutageItems.Count > 0)
                nSystem.Text = string.Format("There are {0} active alarm(s)", xml.OutageItems.Count - 1);

            // Check if the alarm is ignored (don't display in status tray)
            for (int i = 0; i < xml.OutageItems.Count - 1; i++) { // Loop through the outageitems list
                bStop = false;
                if (IgnoredAlerts.Count > 0) { // Are we ignoring anything?
                    for (int b = 0; b < IgnoredAlerts.Count; b++) { // Loop through the ignored alerts list
                        if (xml.OutageItems[i].Title.Equals(IgnoredAlerts[b])) // Check if the alert equals
                            bStop = true; // It does, set to true

                        if (bStop) // Break the second loop
                            break;
                    }
                }

                // Generate our strTmp. This is filled in our text box.
                strTmp = string.Format("An outage has been confirmed for {0}{6}This started at {3}{6}The last update was at {4}{6}The server has been alarming for {5}{6}{7}{6}{6}{2}",
                    xml.OutageItems[i].FullTitle, // 0
                    xml.OutageItems[i].Type,  // 1
                    xml.OutageItems[i].URL,   // 2
                    xml.OutageItems[i].StartDate, // 3
                    xml.OutageItems[i].LastUpdate, // 4
                    xml.OutageItems[i].DownTime,// 5
                    Environment.NewLine, // 6
                    xml.OutageItems[i].Summary); // 7

                // Build our tray alert if it's not ignored.
                if (!bStop) {
                    strMsg += string.Format("{1} {0} is alarming{6}",
                        xml.OutageItems[i].Title, // 0
                        xml.OutageItems[i].Type.ToString().Replace("_", " "),  // 1
                        xml.OutageItems[i].URL,   // 2
                        xml.OutageItems[i].StartDate, // 3
                        xml.OutageItems[i].LastUpdate, // 4
                        xml.OutageItems[i].DownTime,// 5
                        Environment.NewLine); // 6
                }

                strUrl = xml.OutageItems[i].URL;
                strType = xml.OutageItems[i].Type.ToString();

                // Generate our tab and our textbox.
                TabPage tmp = new TabPage(xml.OutageItems[i].Title);
                RichTextBox txt = new RichTextBox();
                txt.Dock = DockStyle.Fill;
                txt.BorderStyle = BorderStyle.None;
                txt.ReadOnly = true;
                txt.DetectUrls = true;
                txt.LinkClicked += txt_LinkClicked;
                txt.Text = strTmp;
                tmp.Controls.Add(txt);
                Tabs.Add(tmp);
            }

            // Loop through our tab list and add them.
            foreach (TabPage t in Tabs)
                tbOutages.TabPages.Add(t);

            // Process our alerts.
            DoAlert(nSystem.Text, strMsg);
            cmdUpdate.Enabled = true; // Enable our update button now that the worker is completed.

            UpdateButtons(); // Update our buttons.
        }

        /// <summary>
        /// Displays an alert in the system tray.
        /// </summary>
        /// <param name="strTitle">The system alert title.</param>
        /// <param name="strMsg">The system alert message.</param>
        void DoAlert(string strTitle, string strMsg) {
            if (Convert.ToInt32(DateTime.Now.Subtract(LastAlert).TotalMinutes) > nAlertTime.Value) { // Check if we exceeded our alert time limit
                if (!string.IsNullOrEmpty(strMsg)) { // Double check to make sure strMsg is not empty
                    nSystem.ShowBalloonTip(5000, strTitle, strMsg, ToolTipIcon.Info); // Display our ballon
                    LastAlert = DateTime.Now; // Set the last alert

                }
            }
            lblLast.Text = string.Format("Last alert occurred at {0}.", LastAlert);
        }
        #endregion

        #region OnClick/Form Events
        void txt_LinkClicked(object sender, LinkClickedEventArgs e) {
            System.Diagnostics.Process.Start(e.LinkText); // Execute the link in default browser
        }

        private void nSystem_MouseDoubleClick(object sender, MouseEventArgs e) {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void tbOutages_Click(object sender, EventArgs e) {
            UpdateButtons(); // Update button status.
        }

        private void cmdShow_Click(object sender, EventArgs e) {
            RemoveIgnoredServer(tbOutages.SelectedTab.Text);
            UpdateButtons(); // Update button status.
        }

        private void cmdHide_Click(object sender, EventArgs e) {
            AddIgnoredServer(tbOutages.SelectedTab.Text);
            UpdateButtons(); // Update button status.
        }


        private void cmdUpdate_Click(object sender, EventArgs e) {
            DoTick();
        }
        #endregion

        #region Timer Events
        private void nTime_ValueChanged(object sender, EventArgs e) {
            tmUpdate.Interval = Convert.ToInt32(nTime.Value * 1000);
        }

        private void tmUpdate_Tick(object sender, EventArgs e) {
            DoTick();
        }

        void DoTick() {
            lblLast.Text = "Contacting Panopta...";
            tbProgress.Visible = true;
            UpdateButtons(); // Update button status.
            BackgroundWorker bWorker = new BackgroundWorker(); // Create our worker process to prevent UI hang.
            bWorker.DoWork += bWorker_DoWork;
            bWorker.RunWorkerCompleted += bWorker_RunWorkerCompleted;
            bWorker.RunWorkerAsync();
            cmdUpdate.Enabled = false;

        }

        void UpdateButtons() {
            if (tbOutages.SelectedTab == null) { // If the selected tab is null, we have no tabs, disable buttons.
                cmdHide.Enabled = false;
                cmdShow.Enabled = false;
            } else {
                if (ItemExists(tbOutages.SelectedTab.Text)) { // The item exists, we need to disable the hide button.
                    cmdHide.Enabled = false;
                    cmdShow.Enabled = true;
                } else { // The item does not exist, we need to disable the show button.
                    cmdHide.Enabled = true;
                    cmdShow.Enabled = false;
                }
            }

            // Just for extra measures. 
            if (tbOutages.TabPages.Count == 0) {
                cmdHide.Enabled = false;
                cmdShow.Enabled = false;
            }

        }
        #endregion

        #region List Management
        /// <summary>
        /// Check if variable exists inside of the list.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        bool ItemExists(string Name) {
            bool bExists = false;
            int index = -1;

            // Grab the index of the file if it exists.
            index = IgnoredAlerts.FindIndex(delegate(string t) {
                return t.Equals(Name);
            });


            if (index >= 0) // If the index is great than -1, it exists.
                bExists = true;
            else
                bExists = false;

            return bExists;
        }

        /// <summary>
        /// Returns the index of specified variable.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        int GetIndexOfItem(string Name) {
            return IgnoredAlerts.FindIndex(delegate(string t) {
                return t.Equals(Name);
            });
        }

        /// <summary>
        /// Adds specified item to the ignored list.
        /// </summary>
        /// <param name="Name"></param>
        void AddIgnoredServer(string Name) {
            IgnoredAlerts.Add(Name);
        }

        /// <summary>
        /// Removes specified item from ignored list.
        /// </summary>
        /// <param name="Name"></param>
        void RemoveIgnoredServer(string Name) {
            int index = GetIndexOfItem(Name);

            if (index >= 0)
                IgnoredAlerts.RemoveAt(index);
        }
        #endregion

    }
}
