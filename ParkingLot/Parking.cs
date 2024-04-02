using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ParkingLot
{
    public partial class Parking : Form
    {
        public Parking()
        {
            InitializeComponent();
            Con = new Functions();
            GetCars();
            GetPlaces();
            ShowBooking();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        Functions Con;
        private void GetCars()
        {
            string Query = "select * from CarsTbl";
            CarsCb.ValueMember = Con.GetData(Query).Columns["CNum"].ToString();
            CarsCb.DisplayMember = Con.GetData(Query).Columns["PNumber"].ToString();
            CarsCb.DataSource = Con.GetData(Query);
        }

        private void GetPlaces()
        {
            string PSt = "Free";
            string Query = "select * from PlaceTbl where PStatus = '{0}'";
            Query = string.Format(Query, PSt);
            PlaceCb.ValueMember = Con.GetData(Query).Columns["PlNum"].ToString();
            PlaceCb.DisplayMember = Con.GetData(Query).Columns["Pposition"].ToString();
            PlaceCb.DataSource = Con.GetData(Query);
        }

        private void ShowBooking()
        {
            string Query = "select * from ParkingTbl";
            ParkingDGV.DataSource = Con.GetData(Query);
        }
        private void UpdateStatus()
        {
            try
            {
                string PSt = "Booked";
                string Place = PlaceCb.SelectedValue.ToString();
                string Query = "update PlaceTbl set PStatus='{0}' where PLNum = {1}";
                Query = string.Format(Query,PSt, Place);
                Con.SetData(Query);
                MessageBox.Show("Place Updated!!!");
                //ShowPlaces();
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void BookBtn_Click(object sender, EventArgs e)
        {
            if(CarsCb.SelectedIndex == -1 || PlaceCb.SelectedIndex == -1 || AmountTb.Text =="" || DurationTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                try
                {

                    string Car = CarsCb.SelectedValue.ToString();
                    string Duration = DurationTb.Text;
                    int Amount = Convert.ToInt32(AmountTb.Text);
                    string Place = PlaceCb.SelectedValue.ToString();
                    string Query = "insert into ParkingTbl values ('{0}','{1}',{2},{3},'{4}')";
                    Query = string.Format(Query, Car, System.DateTime.Today.ToString(), Duration, Amount, Place);
                    Con.SetData(Query);
                    MessageBox.Show("Place Booked!!!");
                    ShowBooking();
                    UpdateStatus();
                }

                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Cars Obj = new Cars();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Places Obj = new Places();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }
    }
}
