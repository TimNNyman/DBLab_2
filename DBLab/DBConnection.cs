using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DBLab;

namespace DBLabs
{
    public class DBConnection : DBLabsDLL.DBConnectionBase
    {
       
        ///*
        // * The constructor
        // */
        public DBConnection()
        {
            
        }

        private SqlConnection myConnection;

        /*
         * The function to logon to the database
         * 
         * Parameters:
         *              username    The userid used to login to SQL Server
         *              password    The password for the userid
         *              
         * Return value:
         *              true    successful login
         *              false   Error
         */
        public override bool login(string username, string password)
        {
            string connectionString = "Data Source=www4.idt.mdh.se;" + "Initial Catalog=DVA234_2018_C1_db;" + "User Id=" + username + ";" +
            "Password=" + password + ";";

            myConnection = new SqlConnection(connectionString);

            try
            {
                myConnection.Open();
                myConnection.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            
            return true;
        }

        
        /*
         --------------------------------------------------------------------------------------------
         IMPLEMENTATION TO BE USED IN LAB 2. 
         --------------------------------------------------------------------------------------------
        */
        public int AddStudent(Student student)
        {
            myConnection.Open();

            SqlCommand cmnd = new SqlCommand("[addStudent]", myConnection);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.Add("@StudentID", SqlDbType.VarChar, 11).Value = student.StudentID;
            cmnd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = student.FirstName;
            cmnd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = student.LastName;
            cmnd.Parameters.Add("@Gender", SqlDbType.VarChar, 1).Value = student.Gender;
            cmnd.Parameters.Add("@StreetAdress", SqlDbType.VarChar, 50).Value = student.StreetAdress;
            cmnd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 50).Value = student.ZipCode;
            cmnd.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = student.City;
            cmnd.Parameters.Add("@Country", SqlDbType.VarChar, 50).Value = student.Country;
            cmnd.Parameters.Add("@Birthdate", SqlDbType.Date).Value = student.BirthDate;
            cmnd.Parameters.Add("@StudentType", SqlDbType.VarChar, 50).Value = student.StudentType;

            for (int i = 0; i < cmnd.Parameters.Count; i++)
            {
                if (cmnd.Parameters[i].Value == null)
                {
                    cmnd.Parameters[i].Value = DBNull.Value;
                }
            }

            StringBuilder errorMessages = new StringBuilder();
            int result = 0;
            try
            {
                result = cmnd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
            
            myConnection.Close();

            return result;
        }

        public void AddPhone(Phone phone)
        {
            myConnection.Open();

            SqlCommand cmnd = new SqlCommand("[addStudentPhoneNo]", myConnection);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.Add("@StudentID", SqlDbType.VarChar, 11).Value = phone.StudentID;
            cmnd.Parameters.Add("@PhoneNmbr", SqlDbType.VarChar, 50).Value = phone.PhoneNumber;
            cmnd.Parameters.Add("@PhoneType", SqlDbType.VarChar, 50).Value = phone.PhoneType;

            try
            {
                cmnd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }

            myConnection.Close();
        }

        public DataTable PhoneTypeTable()
        {
            var phoneTypeQuery = "SELECT * FROM PhoneType";

            myConnection.Open();
            
            DataTable dtPhoneType = new DataTable();
            
            SqlDataAdapter da = new SqlDataAdapter(phoneTypeQuery, myConnection);

            da.Fill(dtPhoneType);

            myConnection.Close();

            return dtPhoneType;
        }

        public DataTable StudentTypeTable()
        {
            var studentTypeQuery = "SELECT * FROM StudentType";
            myConnection.Open();
            SqlDataAdapter da = new SqlDataAdapter(studentTypeQuery, myConnection);
            DataTable dtStudentType = new DataTable();
            da.Fill(dtStudentType);
            myConnection.Close();
            return dtStudentType;
        }
        /*
         --------------------------------------------------------------------------------------------
         STUB IMPLEMENTATIONS TO BE USED IN LAB 3. 
         --------------------------------------------------------------------------------------------
        */


        /********************************************************************************************
         * DATABASE UPDATING METHODS
         *******************************************************************************************/

        /*
         * Add a prerequisite for a course
         * 
         * Parameters:
         *              cc          CourseCode of the course on which to add a prerequisite
         *              preReqcc    CourseCode of the course that is the prerequisite
         *              
         * Return value:
         *              1           Prerequisite added
         *              Any other   Error
         */
        public override int addPreReq(string cc, string preReqcc)
        {
            myConnection.Open();

            SqlCommand cmnd = new SqlCommand("[addPreReq]", myConnection);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.Add("@Kurs", SqlDbType.VarChar, 50).Value = cc;
            cmnd.Parameters.Add("@Krav", SqlDbType.VarChar, 50).Value = preReqcc;

            int result = 0;
            try
            {
                result = cmnd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }

            myConnection.Close();

            return result;
        }

        /*
         * Add a course instance for a course
         * 
         * Parameters:
         *              cc          CourseCode of the course on which to add a course instance
         *              year        The year for the course instance
         *              period      The period for the course instance
         *              
         * Return value:
         *              1           Course instance added
         *              Any other   Error
         */
        public override int addInstance(string cc, int year, int period)
        {
            myConnection.Open();

            SqlCommand cmnd = new SqlCommand("[addInstance]", myConnection);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.Add("@LäsÅr", SqlDbType.Int).Value = year;
            cmnd.Parameters.Add("@Period", SqlDbType.Int).Value = period;
            cmnd.Parameters.Add("@KursKod", SqlDbType.VarChar, 50).Value = cc;

            int result = 0;
            try
            {
                result = cmnd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }

            myConnection.Close();

            return result;
        }

        /*
         * Add a teacher staffing for a course
         * 
         * Parameters:
         *              pnr         "Personnummer" for the teacher to staff
         *              cc          CourseCode of the course on which to add a teacher
         *              year        The year for the course instance
         *              period      The period for the course instance
         *              hours       The number of hours to staff the teacher
         *              
         * Return value:
         *              1           Teacher staffing added
         *              Any other   Error
         */
        public override int addStaff(string pnr, string cc, int year, int period, int hours)
        {
            myConnection.Open();

            SqlCommand cmnd = new SqlCommand("[addStaff]", myConnection);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.Add("@PrNmbr", SqlDbType.VarChar, 11).Value = pnr;
            cmnd.Parameters.Add("@KursKod", SqlDbType.VarChar, 50).Value = cc;
            cmnd.Parameters.Add("@År", SqlDbType.Int).Value = year;
            cmnd.Parameters.Add("@Period", SqlDbType.Int).Value = period;
            cmnd.Parameters.Add("@Timmar", SqlDbType.Int).Value = hours;

            int result = 0;
            try
            {
                result = cmnd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }

            myConnection.Close();

            return result;
        }

        /*
         * Add a labassistant staffing for a course
         * 
         * Parameters:
         *              studid      StudentID for the student to staff
         *              cc          CourseCode of the course on which to add a labassistant
         *              year        The year for the course instance
         *              period      The period for the course instance
         *              hours       The number of hours to staff the student
         *              
         * Return value:
         *              1           Labassistant staffing added
         *              Any other   Error
         */
        public override int addLabass(string studid, string cc, int year, int period, int hours, int salary)
        {
            myConnection.Open();

            SqlCommand cmnd = new SqlCommand("[addLabass]", myConnection);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.Add("@StudentID", SqlDbType.VarChar, 11).Value = studid;
            cmnd.Parameters.Add("@KursKod", SqlDbType.VarChar, 50).Value = cc;
            cmnd.Parameters.Add("@LäsÅr", SqlDbType.Int).Value = year;
            cmnd.Parameters.Add("@Period", SqlDbType.Int).Value = period;
            cmnd.Parameters.Add("@Timmar", SqlDbType.Int).Value = hours;
            cmnd.Parameters.Add("@TimLön", SqlDbType.Int).Value = salary;

            int result = 0;
            try
            {
                result = cmnd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }

            myConnection.Close();

            return result;
        }


        /*
         * Add a new course
         * 
         * Parameters:
         *              cc          CourseCode of the course on which to add a labassistant
         *              name        The name of the course
         *              credits     The number of credits for the course
         *              responsible The "personnummer" of the course responsible staff
         *              
         * Return value:
         *              1           Course added
         *              Any other   Error
         */
        public override int addCourse(string cc, string name, double credits, string responsible)
        {
            myConnection.Open();

            SqlCommand cmnd = new SqlCommand("[addCourse]", myConnection);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.Add("@KursKod", SqlDbType.VarChar, 50).Value = cc;
            cmnd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = name;
            cmnd.Parameters.Add("@Credit", SqlDbType.Float).Value = credits;
            cmnd.Parameters.Add("@Responsible", SqlDbType.VarChar, 50).Value = responsible;
            

            int result = 0;
            try
            {
                result = cmnd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }

            myConnection.Close();

            return result;
        }


        /********************************************************************************************
         * DATABASE QUERYING METHODS
         *******************************************************************************************/

        /*
         * Get student data for all students
         * 
         * Parameters
         *              None
         * 
         * Return value:
         *              DataTable with the following columns:
         *                  StudentID       VARCHAR     StudentID for Students
         *                  FirstName       VARCHAR     Students First Name
         *                  LastName        VARCHAR     Students Last Name
         *                  Gender          VARCHAR     Students Gender
         *                  StreetAdress    VARCHAR     Students StreetAdress
         *                  ZipCode         VARCHAR     Students "PostNummer"
         *                  BirthDate       DATETIME    Students BirthDate
         *                  StudentType     VARCHAR     Student type (Program Student, Exchange Student etc)
         *                  City            VARCHAR     Students City
         *                  Country         VARCHAR     Students Country
         *                  program         VARCHAR     Name of the program the student is enrolled to
         *                  PgmStartYear    INTEGER     Year the student enrolled to the program
         *                  credits         FLOAT       The number of credits that the student has completed
         */
        public override DataTable getStudentData()
        {
            myConnection.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM getStudents", myConnection);

    
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("Gender");
            dt.Columns.Add("Streetadress");
            dt.Columns.Add("ZipCode");
            dt.Columns.Add("Birthdate");
            dt.Columns.Add("StudentType");
            dt.Columns.Add("City");
            dt.Columns.Add("Country");
            dt.Columns.Add("program");
            dt.Columns.Add("PgmStartYear");
            dt.Columns.Add("credits");

            da.Fill(dt);

            myConnection.Close();

            return dt;
        }

        /*
         * Get list of staff
         * 
         * Parameters
         *              None
         *
         * Return value
         *              DataTable with the following columns:
         *                  pnr             VARCHAR     "Personnummer" for the staff
         *                  fullname        VARCHAR     Full name (First name and Last Name) of the staff.
         */
        public override DataTable getStaff()
        {
            myConnection.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM getStaff", myConnection);

            
            DataTable dt = new DataTable();

            dt.Columns.Add("pnr");
            dt.Columns.Add("fullname");

            da.Fill(dt);

            myConnection.Close();

            return dt;
        }

        /*
         * Get list of Potential Labasses (i.e. students)
         * 
         * Parameters
         *              None
         *
         * Return value
         *              DataTable with the following columns:
         *                  StudentID       VARCHAR     StudentID for all students
         *                  fullname        VARCHAR     Full name (First name and Last Name) of the students.
         */
        public override DataTable getLabasses()
        {
            myConnection.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM getLabasses", myConnection);


            DataTable dt = new DataTable();

            dt.Columns.Add("StudentID");
            dt.Columns.Add("fullname");

            da.Fill(dt);

            myConnection.Close();

            return dt;
            
        }

        /*
         * Get course data
         * 
         * Parameters
         *              None
         * 
         * Return value
         *              DataTable with the following columns:
         *                  coursecode      VARCHAR     Course Code
         *                  name            VARCHAR     Name of the Course
         *                  credits         FLOAT       Credits of the course
         *                  courseresponsible VARCHAR   "Personnummer" for the course responsible teacher
         */
        public override DataTable getCourses()
        {
            myConnection.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM getCourses", myConnection);


            DataTable dt = new DataTable();

            dt.Columns.Add("coursecode");
            dt.Columns.Add("name");
            dt.Columns.Add("credits");
            dt.Columns.Add("courseresponsible");
            

            da.Fill(dt);

            myConnection.Close();

            return dt;
        }
        /*
         * Returns the salary costs for a course instance based on the teacher and lab assistent staffing.
         * 
         * Parameters:
         *              cc          CourseCode to the course to calculate the cost
         *              year        The year for the course instance
         *              period      The period for the course instance
         *              
         * Return value:
         *              integer     The cost in currency (SEK)
         */
        public override int getCourseCost(string cc, int year, int period)
        {

            myConnection.Open();


            SqlParameter returnValue = new SqlParameter("@T", SqlDbType.Int);
            returnValue.Direction = ParameterDirection.ReturnValue;
            SqlCommand cmnd = new SqlCommand("[dbo].[getCourseCost]", myConnection);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.Add("@KursKod", SqlDbType.VarChar, 50).Value = cc;
            cmnd.Parameters.Add("@LäsÅr", SqlDbType.Int).Value = year;
            cmnd.Parameters.Add("@Period", SqlDbType.Int).Value = period;

            cmnd.Parameters.Add(returnValue);

            cmnd.ExecuteNonQuery();

            int result = (int)cmnd.Parameters["@T"].Value;

            myConnection.Close();

            return result;
        }

        /*
         * Returns the staffed persons (both teachers and lab assistants) for a course instance
         * 
         * Parameters:
         *              cc          CourseCode to the course to show staffing for
         *              year        The year for the course instance
         *              period      The period for the course instance
         *              
         * Return value:
         *              DataTable with the relevant information
         *                  The table should show name, number of hours, the Task in the course and the hourly salary
         */
        public override DataTable getCourseStaffing(string cc, string year, string period)
        {
            myConnection.Open();

            SqlCommand cmnd = new SqlCommand("[getCourseStaffing]", myConnection);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.Add("@KursKod", SqlDbType.VarChar, 50).Value = cc;
            cmnd.Parameters.Add("@LäsÅr", SqlDbType.Int).Value = year;
            cmnd.Parameters.Add("@Period", SqlDbType.Int).Value = period;

            DataTable dt = new DataTable();

            dt.Load(cmnd.ExecuteReader());

            myConnection.Close();
            return dt;

            
            //Dummy code - Remove!
            

        }

        /*
         * Returns the student course transcript ("Ladokudrag")
         * 
         * Parameters:
         *              studId      StudentID for student to show transcript for
         *              
         * Return value:
         *              DataTable with the relevant information
         *                  See lab-instructions for more information about this DataTable
         */
        public override DataTable getStudentRecord(string studId)
        {
            myConnection.Open();

            SqlCommand cmnd = new SqlCommand("[getStudentRecord]", myConnection);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.Add("@StudentID", SqlDbType.VarChar, 11).Value = studId;

            DataTable dt = new DataTable();
            dt.Load(cmnd.ExecuteReader());

            myConnection.Close();
            return dt;
        }

        /*
         * Returns the a list of all courses that are prerequisites to a course.
         * 
         * Parameters:
         *              cc      Course Code for the course to list prerequisites
         *              
         * Return value:
         *              DataTable with the relevant information
         *                  The Table should show at least coursecode and course name for all prerequisite courses
         */
        public override DataTable getPreReqs(string cc)
        {
            myConnection.Open();

            SqlCommand cmnd = new SqlCommand("[getPreReqs]", myConnection);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.Add("@kurs", SqlDbType.VarChar, 50).Value = cc;

            DataTable dt = new DataTable();
            dt.Columns.Add("Course Code");
            dt.Columns.Add("Course Name");
            dt.Load(cmnd.ExecuteReader());

            myConnection.Close();
            return dt;
        }


        /*
         * Get course instances for a course
         * 
         * Parameters
         *              cc      Course Code for the course to list course instances
         * 
         * Return value
         *              DataTable with the following columns:
         *                  year            INTEGER     The year of the course instance
         *                  period          INTEGER     The period of the course instance
         *                  instance        VARCHAR     The "Display text" for the instance, e.g. year(period) or similar
         */
        public override DataTable getInstances(string cc)
        {
            myConnection.Open();

            SqlCommand cmnd = new SqlCommand("[getInstances]", myConnection);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.Add("@kurs", SqlDbType.VarChar, 50).Value = cc;

            DataTable dt = new DataTable();
            dt.Load(cmnd.ExecuteReader());

             myConnection.Close();

            return dt;
        }

        /*
        * Get list of telephone numbers for a student
        * 
        * Parameters
        *              studId      StudentID for the student
        * 
        * Return value
        *              DataTable with the following columns:
        *                  Type            VARCHAR     The type of telephone number (e.g., Home, Work, Cell etc.)
        *                  Number          VARCHAR     The telephone number
        */
        public override DataTable getStudentPhoneNumbers(string studId)
        {
            myConnection.Open();

            SqlCommand cmnd = new SqlCommand("[getStudentPhoneNumbers]", myConnection);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.Add("@StudentID", SqlDbType.VarChar, 11).Value = studId;

            DataTable dt = new DataTable();
            dt.Columns.Add("Type");
            dt.Columns.Add("Number");
            dt.Load(cmnd.ExecuteReader());

            myConnection.Close();

            return dt;
            
        }

        /*
        --------------------------------------------------------------------------------------------
         STUB IMPLEMENTATIONS TO BE USED IN LAB 4. 
        --------------------------------------------------------------------------------------------
        */


        /*
        * Get list years which have course instances
        * 
        * Parameters
        *              None      
        * 
        * Return value
        *              DataTable with the following column:
        *                  Year            INTEGER     A unique (no duplicates) list of all years which has course instances
        */
        public override DataTable getStaffingYears()
        {
            //Dummy code - Remove!
            //Please note that you do not use DataTables like this at all when you are using a database!!
            DataTable dt = new DataTable();
            dt.Columns.Add("Year");
            dt.Rows.Add(2000);
            return dt;
        }

        /*
        * Get a matrix of all staffing for a year
        * 
        * Parameters
        *              year     The year to show staffings for      
        * 
        * Return value
        *              DataTable with suitable format
        *                  For more information about the format, see Lab instructions for lab 4
        */
        public override DataTable getStaffingGrid(string year)
        {
            //Dummy code - Remove!
            //Please note that you do not use DataTables like this at all when you are using a database!!
            DataTable dt = new DataTable();
            dt.Columns.Add("StaffingGrid");
            dt.Rows.Add("All will be revealed in lab 4.. :)");
            return dt;
        }
    }
}
