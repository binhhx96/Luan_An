﻿using FinalProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Final
{
    public partial class AddStudent : Form
    {
        DAL_SinhVien dal_sv = new DAL_SinhVien();
        public AddStudent()
        {
            InitializeComponent();
            GUI_getDataForCombobox();
        }

        public void GUI_getDataForCombobox()
        {
            cbbKhoa.ValueMember = "id";
            cbbKhoa.DisplayMember = "ma_khoa_hoc";
            cbbKhoa.DataSource = dal_sv.getDataForSelectBox("khoa_hoc");

            cbbNganh.ValueMember = "id";
            cbbNganh.DisplayMember = "TenChuyenNganh";
            cbbNganh.DataSource = dal_sv.getDataForSelectBox("nganh_dao_tao");

            cbbNghe.ValueMember = "id";
            cbbNghe.DisplayMember = "ten_nganh";
            cbbNghe.DataSource = dal_sv.getDataForSelectBox("nganh_nghe");

            cbbCoQuan.ValueMember = "id";
            cbbCoQuan.DisplayMember = "TenCoQuan";
            cbbCoQuan.DataSource = dal_sv.getDataForSelectBox("co_quan");
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            QuanLySV qlsv = new QuanLySV();
            qlsv.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtHoTen.Text != "" && txtMaSV.Text != "")
            {
                //Nganh
                DataRowView drvNganh = cbbNganh.SelectedItem as DataRowView;
                string nganh = string.Empty;

                if (drvNganh != null)
                {
                    nganh = drvNganh.Row["TenChuyenNganh"] as string;
                }

                //Nghe
                DataRowView drvNghe = cbbNghe.SelectedItem as DataRowView;
                string nghe = string.Empty;

                if (drvNghe != null)
                {
                    nghe = drvNghe.Row["ma_nganh_nghe"] as string;
                }

                //Dan toc
                string dantoc = cbbDanToc.SelectedItem.ToString();
                //Hoc luc
                string hocluc = cbbHocLuc.SelectedItem.ToString();

                //Gioi tinh
                string gioitinh = cbbGioiTinh.SelectedItem.ToString();

                //Quê quán
                string quequan = txtQueQuan.Text;

                //Cơ quan
                DataRowView drvCoquan = cbbCoQuan.SelectedItem as DataRowView;
                string coquan = string.Empty;

                if (drvCoquan != null)
                {
                    coquan = drvCoquan.Row["TenCoQuan"] as string;
                }

                //Khoa
                DataRowView drvKhoa = cbbKhoa.SelectedItem as DataRowView;
                string khoahoc = string.Empty;
    
                if (drvKhoa != null)
                {
                    khoahoc = drvKhoa.Row["ma_khoa_hoc"] as string;
                }
                // Tạo DTo
                DTO_SinhVien sv = new DTO_SinhVien(txtMaSV.Text, txtHoTen.Text, dateNgaysinh.Text, gioitinh, dantoc, quequan, khoahoc, hocluc, nganh, nghe, coquan);
                // Them
                if (dal_sv.themSinhVien(sv))
                {
                    MessageBox.Show("Thêm thành công");
                    this.Visible = false;
                    QuanLySV qlsv = new QuanLySV();
                    qlsv.Show();
                }
                else
                {
                    MessageBox.Show("Thêm ko thành công");
                }
            }
            else
            {
                MessageBox.Show("Xin hãy nhập đầy đủ");
            }
        }

      
    }
}
