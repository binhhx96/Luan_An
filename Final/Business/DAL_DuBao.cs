﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Final.ConnectDB;

namespace FinalProject
{
    class DAL_DuBao : DBConnect
    {
        public DataTable getDuBaoCung(int nam)
        {
            _conn.Open();
            SqlDataAdapter da;
            DataTable dtTruong = new DataTable();
            string sql = "SELECT * from dubaocung where dubaocung.Nam=" + nam;            
            string sql_1 = "SELECT cosodaotao.MaTruong as ma_truong,TenTruong,DiaChi FROM cosodaotao";
            string sql_2 = "SELECT * from TuyenSinh where TuyenSinh.Nam=" + nam;
            string sql_3 = "SELECT cosodaotao.MaTruong as ma_truong,TenTruong,DiaChi,dubaocung.Nam as Nam,dubaocung.DuBaoTuyenSinh as du_bao_tuyen_sinh, dubaocung.TiLeDo as ti_le_do, dubaocung.SoLaoDong as so_lao_dong FROM cosodaotao Inner Join dubaocung on dubaocung.MaTruong = cosodaotao.MaTruong AND dubaocung.Nam=" + nam;
            string sql_4 = "SELECT cosodaotao.MaTruong as ma_truong,TenTruong,DiaChi,TuyenSinh.Nam as Nam,TuyenSinh.ChiTieu as du_bao_tuyen_sinh FROM cosodaotao Inner Join TuyenSinh on TuyenSinh.MaTruong = cosodaotao.MaTruong AND TuyenSinh.Nam=" + nam;            
            SqlCommand cmd = new SqlCommand(sql, _conn);
            SqlDataReader data = cmd.ExecuteReader();
            if (data.HasRows){
                data.Close();                                                    
                da = new SqlDataAdapter(sql_3, _conn);
                da.Fill(dtTruong);                                                                   
            }else{
                data.Close();
                SqlCommand cmd_1 = new SqlCommand(sql_2, _conn);
                SqlDataReader data_1 = cmd_1.ExecuteReader();
                if (data_1.HasRows)
                {
                    data_1.Close();                                       
                    da = new SqlDataAdapter(sql_4, _conn);
                    da.Fill(dtTruong);              
                }
                else 
                {
                    data_1.Close();
                    dtTruong.Columns.Add("ma_truong", typeof(string));
                    dtTruong.Columns.Add("TenTruong", typeof(string));
                    dtTruong.Columns.Add("DiaChi", typeof(string));
                    dtTruong.Columns.Add("Nam", typeof(int));
                    dtTruong.Columns.Add("du_bao_tuyen_sinh", typeof(int));

                    SqlCommand cmd_2 = new SqlCommand(sql_1, _conn);
                    SqlDataReader data_2 = cmd_2.ExecuteReader();

                    while (data_2.Read())
                    {
                        dtTruong.Rows.Add(data_2[0], data_2[1], data_2[2], nam, 0);
                    }                    
                }                
            }
           
            _conn.Close();           
            return dtTruong;
        }
        public void addDuBaoCung(DTO_DuBao dubao) {
            
            try
            {
                // Ket noi
                _conn.Open();

                // Query string
                string SQL = string.Format("INSERT INTO dubaocung(MaTruong, Nam,DuBaoTuyenSinh,TiLeDo,SoLaoDong) VALUES ('{0}','{1}','{2}','{3}','{4}')", dubao.MA_TRUONG,dubao.NAM,dubao.DU_BAO_TUYEN_SINH,dubao.TI_LE_DO,dubao.SO_LAO_DONG);                
                // Command    
                Console.WriteLine(SQL);
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                // Query và kiểm tra 
                cmd.ExecuteNonQuery();                   
            }
            catch (Exception e)
            {
                MessageBox.Show("Có lỗi trong khi cập nhật dự báo cung!");
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }            
        }
        public void deleteDuBaoCung(int nam)
        {
            try
            {
                // Ket noi
                _conn.Open();

                // Query string
                string SQL = string.Format("DELETE from dubaocung where dubaocung.Nam=" + nam);
                Console.WriteLine(SQL);
                // Command                
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                // Query và kiểm tra 
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Có lỗi trong khi xóa dự báo cung!");
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
        }
        public void updateDuBaoCung(DTO_DuBao db)
        {
            try
            {
                // Ket noi
                _conn.Open();

                // Query string
                string SQL = string.Format("update dubaocung set SoLaoDong='{0}', DuBaoTuyenSinh='{1}',TiLeDo='{2}' where dubaocung.Nam='{3}' AND dubaocung.MaTruong='{4}'", db.SO_LAO_DONG, db.DU_BAO_TUYEN_SINH,db.TI_LE_DO,db.NAM,db.MA_TRUONG);
                Console.WriteLine(SQL);
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                // Query và kiểm tra 
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Có lỗi trong khi update dữ liệu tuyển sinh!");
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
        }
        public List<int> getNamDuBaoCung() {
            List<int> ls_nam = new List<int>();
            _conn.Open();
            string SQL = string.Format("SELECT DISTINCT Nam from dubaocung");
            Console.WriteLine(SQL);
            // Command
            SqlCommand cmd = new SqlCommand(SQL, _conn);
            SqlDataReader data = cmd.ExecuteReader();            

            while (data.Read())
            {
                int nam = Int32.Parse(data[0].ToString());
                ls_nam.Add(nam);
            }

            _conn.Close();
            return ls_nam;
        }

        public bool del_Truong_DuBaoCung(string ma_truong)
        {
            try
            {
                // Ket noi
                _conn.Open();

                Boolean check = false;
                // Query string
                string SQL = string.Format("select * FROM dubaocung WHERE dubaocung.MaTruong='" + ma_truong + "'");
                // Command                
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                // Query và kiểm tra
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Có lỗi trong khi xóa!");
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
            return false;
        }
        private int bptoithieu(List<DTO_CT> listCt, int n, int nam)
        {
            double X = 0, Y = 0, XY = 0, XX = 0;

            for (int j = 0; j < n; j++)
            {
                X += listCt[j].NAM;
                Y += listCt[j].CHITIEU;
                XX += listCt[j].NAM * listCt[j].NAM;
                XY += listCt[j].NAM * listCt[j].CHITIEU;
            }

            double X1 = X / n, Y1 = Y / n, XX1 = XX / n, XY1 = XY / n;

            double b1 = (XY1 - X1 * Y1) / (XX1 - X1 * X1);
            double b2 = Y1 - X1 * b1;

            int dubao = (int)(nam * b1 + b2);
            if (dubao < 0)
                return 0;
            return dubao;
        }

        public bool updateTruong_Dubaocung(string matruong)
        {
            _conn.Open();
            string SQL = string.Format("Select Nam,ChiTieu from TuyenSinh where ChiTieu > 0 AND MaTruong = '" + matruong + "'");
            Console.WriteLine(SQL);
            // Command
            SqlCommand cmd = new SqlCommand(SQL, _conn);
            SqlDataReader data = cmd.ExecuteReader();
            List<DTO_CT> listCt = new List<DTO_CT>();

            while (data.Read())
            {
                DTO_CT ct = new DTO_CT(Int32.Parse(data[0].ToString()), Int32.Parse(data[1].ToString()));
                listCt.Add(ct);
            }
            _conn.Close();

            int n = listCt.Count;
            if (n != 0)
            {
                try
                {
                    _conn.Open();
                    string SQL_1 = string.Format("Select TuyenSinh.MaTruong,TiLeDo,SoLaoDong,TuyenSinh.Nam as ts_nam,TuyenSinh.ChiTieu as ts_chitieu,dubaocung.Nam as dbc_nam from dubaocung Inner Join TuyenSinh on TuyenSinh.MaTruong = dubaocung.MaTruong where dubaocung.MaTruong = '" + matruong + "'");
                    Console.WriteLine(SQL_1);
                    // Command
                    SqlCommand cmd_1 = new SqlCommand(SQL_1, _conn);
                    SqlDataReader data_1 = cmd_1.ExecuteReader();
                    while (data_1.Read())
                    {
                        int dbc_nam = Int32.Parse(data_1[5].ToString());
                        int ts_nam = Int32.Parse(data_1[3].ToString());
                        double tile = Convert.ToDouble(data_1[1].ToString());
                        int chitieu = 0;
                        if (dbc_nam > 2017)
                        {
                            chitieu = bptoithieu(listCt, n, dbc_nam);
                            int dubao = (int)(chitieu * tile) / 100;
                            DTO_DuBao db = new DTO_DuBao(matruong, (float)tile, dubao, dbc_nam, chitieu);
                            DAL_DuBao dal_db = new DAL_DuBao();
                            dal_db.updateDuBaoCung(db);
                        }
                        else
                        {
                            if (dbc_nam == ts_nam)
                            {
                                chitieu = Int32.Parse(data_1[4].ToString());
                                int dubao = (int)(chitieu * tile) / 100;
                                DTO_DuBao db = new DTO_DuBao(matruong, (float)tile, dubao, dbc_nam, chitieu);
                                DAL_DuBao dal_db = new DAL_DuBao();
                                dal_db.updateDuBaoCung(db);
                            }
                        }
                    }
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Có lỗi trong khi update!");
                }
                finally
                {
                    // Dong ket noi
                    _conn.Close();
                }
            }
            return false;
        }        
    }
}
