using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Panappta {
    public partial class fMain : Form {
        public fMain() {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e) {
            listView1.Items.Add("Test");
        }
    }
}
