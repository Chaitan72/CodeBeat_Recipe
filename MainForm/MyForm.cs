using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeBeat_Recipe;

namespace MainForm
{
    public partial class MyForm : Form
    {
        public MyForm()
        {
            InitializeComponent();
        }

        private void MyForm_Load(object sender, EventArgs e)
        {
            DbManage data = new DbManage();
            data.make_connection();

            List<string> rName = new List<string>();
            List<string> uName = new List<string>();

            data.SelectMultiple("SELECT DISTINCT CreatedBy FROM span_db.model", ref uName);
            data.SelectMultiple("SELECT Name From span_db.model", ref rName);

            for (int i = 0; i < uName.Count(); i++)
            {
                this.CB_userName.Items.Add(uName[i]);
            }
            this.CB_userName.SelectedItem = null;
            this.CB_userName.SelectedText = "All Users";

            int c = 0;
            for (int i = 0; i < rName.Count(); i++)
            {
                this.LB_recipeName.Items.Add(rName[i]);
                c++;
            }
        }

        private void CB_userName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int ia = 0;
            string curItem = this.CB_userName.GetItemText(this.CB_userName.SelectedItem);

            this.LB_recipeName.Items.Clear();

            DbManage d1 = new DbManage();
            d1.make_connection();
            List<string> rName = new List<string>();
            d1.SelectMultiple("SELECT Name FROM span_db.model WHERE CreatedBy = '" + curItem + "'", ref rName);
            for (int i = 0; i < rName.Count(); i++)
            {
                this.LB_recipeName.Items.Add(rName[i]);
            }

            int k = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string rname = this.LB_recipeName.GetItemText(this.LB_recipeName.SelectedItem);

            Recipe_Main r = new Recipe_Main(rname);
            r.MakeDocument();
            this.documentViewer1.DocumentSource = r;

            //   doRecipe_Main("")
        }
    }
}
