using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBLab;

namespace DBLabs
{
    public partial class AddStudentControl : UserControl
    {
        private DBConnection dbconn;

        public AddStudentControl()
        {
            /*
             * Constructor the control
             * 
             * You DONT need to edit this constructor.
             * 
             */
            InitializeComponent();
        }

        public void AddStudentControlSettings(ref DBConnection dbconn)
        {
            /*
             * Since UserControls cannot take arguments to the constructor 
             * this function is called after the constructor to perform this.
             * 
             * You DONT need to edit this function.
             * 
             */
            this.dbconn = dbconn; 
        }

        private void LoadAddStudentControl(object sender, EventArgs e)
        {
            StudentTyp.DataSource = dbconn.StudentTypeTable(); ;
            StudentTyp.DisplayMember = "StudentType";
            StudentTyp.ValueMember = "StudentType";

            PhoneType.DataSource = dbconn.PhoneTypeTable(); ;
            PhoneType.DisplayMember = "PhoneType";
            PhoneType.ValueMember = "PhoneType";
            /*
             * This function contains all code that needs to be executed when the control is first loaded
             * 
             * You need to edit this code. 
             * Example: Population of Comboboxes and gridviews etc.
             * 
             */
        }
        public void ResetAddStudentControl()
        {
            StudentId.Text = String.Empty;
            FirstName.Text = String.Empty;
            LastName.Text = String.Empty;
            Gender.Text = String.Empty;
            StreetAdress.Text = String.Empty;
            ZipCode.Text = String.Empty;
            City.Text = String.Empty;
            Country.Text = String.Empty;
            StudentTyp.SelectedIndex = 0;
            //BirthDate.Value = DateTime.Now;
            

            ResetAddPhone();

            PhoneList.Items.Clear();

            /*
             * This function contains all code that needs to be executed when the control is reloaded
             * 
             * You need to edit this code. 
             * Example: Emptying textboxes and gridviews
             * 
             */
        }

        public void ResetAddPhone()
        {
            PhoneNumbers.Text = String.Empty;
            PhoneType.SelectedIndex = 0;
        }

        private void ButtonAddStudent_Click(object sender, EventArgs e)
        {
            var student = new Student();

            student.StudentID = StudentId.Text;
            student.FirstName = FirstName.Text;
            student.LastName = LastName.Text;
            student.Gender = Gender.Text;
            student.City = City.Text;
            student.Country = Country.Text;
            int number;
            bool result = Int32.TryParse(ZipCode.Text, out number);
            if (result)
            {
                student.ZipCode = number;
            }
            else
            {
                            if (ZipCode.Text == null) ZipCode.Text = null; 
                MessageBox.Show("Only integers for ZipCode", "Please try again", MessageBoxButtons.OK);
                return;
            }
            student.ZipCode = Int32.Parse(ZipCode.Text);
            student.BirthDate = BirthDate.Text;
            student.StudentType = StudentTyp.Text;
            student.StreetAdress = StreetAdress.Text;

            

            if (dbconn.AddStudent(student) == 1)
            {
                foreach (Phone item in PhoneList.Items)
                {
                    dbconn.AddPhone(item);
                }
                ResetAddStudentControl();
            }
        }

        private void ButtonPhoneNumber_Click(object sender, EventArgs e)
        {
            var phone = new Phone(PhoneNumbers.Text,PhoneType.Text, StudentId.Text);

            PhoneList.Items.Add(phone);

            ResetAddPhone();
        }
    }
}
