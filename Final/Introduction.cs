﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Final
{
    public partial class Introduction : Form
    {
        public Introduction()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void btnQuanLy_Click(object sender, EventArgs e)
        {
            QuanLyTruong Ql = new QuanLyTruong();
            this.Visible = false;
            Ql.ShowDialog();
        }

        private void btnHuongDan_Click(object sender, EventArgs e)
        {
            Introduction intro = new Introduction();
            this.Visible = false;
            intro.Show();
        }


        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            Home Hm = new Home();
            this.Visible = false;
            Hm.ShowDialog();
        }
    }
}
