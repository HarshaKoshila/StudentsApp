﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsApp
{
    public partial class BackupForm : Form
    {
        private SqlConnection conn;
        private SqlCommand command;
        private SqlDataReader reader;
        string sql = "";
        string connectionString = "";
        public BackupForm()
        {
            InitializeComponent(); 
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                connectionString = "Data Source =" + textBox1.Text + ";Integrated Security=True";
                conn = new SqlConnection(connectionString);
                conn.Open();
                MessageBox.Show("Successfully Connected");
                //ql = "EXEC sp_databases";
                sql = "SELECT * FROM sys.databases d WHERE d.database_id>4";
                command = new SqlCommand(sql, conn);
                reader = command.ExecuteReader();
                comboBox1.Items.Clear();
                while (reader.Read())

                {
                    comboBox1.Items.Add(reader[0].ToString());

                }
                reader.Dispose();
                conn.Close();
                conn.Dispose();

                textBox1.Enabled = false;
                btnConnect.Enabled = false;
                BtnDisconnect.Enabled = true;
                btnBackup.Enabled = true;
                btnRestore.Enabled = true;

                comboBox1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            comboBox1.Enabled = false;
            btnRestore.Enabled = false;
            btnBackup.Enabled = false;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = dlg.SelectedPath;
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text.CompareTo("") == 0)
                {
                    MessageBox.Show("please select a Database.");
                    return;
                }
                conn = new SqlConnection(connectionString);
                conn.Open();
                sql = "BACKUP DATABASE  " + comboBox1.Text + " TO DISK = '" + textBox4.Text + "\\" + comboBox1.Text + "-" + DateTime.Now.Ticks.ToString() + ".bak'";
                command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                MessageBox.Show("Successfully Database Backup Completed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDBBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Backup Files(*.bak)|*.bak|All Files(*.*)|*.*";
            dlg.FilterIndex = 0;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = dlg.FileName;
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text.CompareTo("") == 0)
                {
                    MessageBox.Show("please select a Database.");
                    return;
                }
                conn = new SqlConnection(connectionString);
                conn.Open();
                sql = "Alter Database  " + comboBox1.Text + " set SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                sql += "Restore Database " + comboBox1.Text + " FROM disk = '" + textBox5.Text + "' WITH REPLACE;";
                command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                MessageBox.Show("Successfully Restore Database.");

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGood_Click(object sender, EventArgs e)
        {
            this.Hide();
            GdCondctForm1 ob = new GdCondctForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            OfficerForm ob = new OfficerForm();
            ob.ShowDialog();
            this.Close();
        }

        private void btnChrgrSheet_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChrgSheetForm1 ob = new ChrgSheetForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnBlackbook_Click(object sender, EventArgs e)
        {
            this.Hide();
            BlackBkForm1 ob = new BlackBkForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void BtnIncDiary_Click(object sender, EventArgs e)
        {
            this.Hide();
            InciDryForm1 ob = new InciDryForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm ob = new LoginForm();
            ob.ShowDialog();
            this.Close();
        }

        private void btnWork_Click(object sender, EventArgs e)
        {
            this.Hide();
            WorkAssForm1 ob = new WorkAssForm1();
            ob.ShowDialog();
            this.Close();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {

        }
    }
}
