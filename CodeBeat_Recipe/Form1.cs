using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeBeat_Recipe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Recipe_Main rm = new Recipe_Main("testing");
            rm.CreateDocument();

            documentViewer1.DocumentSource = rm;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
