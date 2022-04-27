using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace sudoku
{
    public partial class frmErrors : Form
    {

        public objError err;

        //  constructor

        public frmErrors()
        {
            InitializeComponent();
        }

        //  on load set up list view

        private void frmErrors_Load(object sender, EventArgs e)
        {
            lvData.Columns.Clear();
            lvData.Columns.Add("Item", 150, HorizontalAlignment.Left);
            lvData.Columns.Add("Description", 300, HorizontalAlignment.Left);
        }

        //  on selection, return error

        private void lvData_ItemActivate(object sender, EventArgs e)
        {
            err = (objError)lvData.SelectedItems[0].Tag;
            DialogResult = DialogResult.OK;
        }

        //  close form

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        //  load the error list

        public void LoadData(List<objError> errorList)
        {
            ListViewItem lvItem;

            foreach (objError itm in errorList)
            {
                lvItem = new ListViewItem(itm.Dimension + " " + itm.n.ToString());
                lvItem.SubItems.Add(itm.Items);
                lvItem.Tag = itm;
                lvData.Items.Add(lvItem);
            }
        }


    }
}
