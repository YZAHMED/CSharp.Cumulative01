using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cumulative01.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Cumulative01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TeacherAPIController : ControllerBase
    {


        // This is a private variable that will hold the connection to the database after being assigned in the constructor.
        private readonly SchoolDbContext _context;


        //This is the constructor that will assign the connection to the database to the private variable.s
        public TeacherAPIController(SchoolDbContext context)
        {
            // here the connection to the database is assigned to the private variable from the model DxContext.
            _context = context;
        }
        //Here we are setting up the API method to receveive a GET request to the endpoint /api/Teacher
        [HttpGet(template:"Teacher")]

        //this is the method that will call the database and return a list of teachers.


        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>
        /// GET api/Teacher -> [{"TeacherId":1,"TeacherFirstName":"Alexendar","TeacherLastName":"Doe",...},..]
        /// </example>
        /// <returns>
        /// A list of Teacher objects containing ID, Name, EmployeeID, HireDate, and Salary
        /// </returns>



        public List<Teacher> ListTeacherNames()
        {
            //This is the list of a list type of Teacher that will hold the teachers instances in as objects in the list.
            List<Teacher> teachers = new List<Teacher>();


            //MySqlConnection is a data type that here is used to hold in the Connection variable the connection to the database.
            MySqlConnection Connection = _context.GetConnection();


            //Here the connection to the database is being opened using the Open() method.
            Connection.Open();

            Debug.WriteLine("DbConnected");

            //This is the SQL query that will be executed on the database to get the teachers.
            string SQLQuery = "SELECT * FROM teachers";


            //MySqlCommand is a data type that here is used to hold in the Command variable the command that will be executed on the database.
            MySqlCommand Command = Connection.CreateCommand();

            //here the command text is set to the SQL query that will be executed on the database.
            Command.CommandText = SQLQuery;

            //MySqlDataReader is a data type that here is used to hold in the DataReader variable the dataset that will be read from the database.
            MySqlDataReader DataReader = Command.ExecuteReader();


            //This is a while loop that will read the data from the database and assign it to the teachers list.
            while (DataReader.Read())
            {
             
                int TeacherId = Convert.ToInt32(DataReader["teacherid"]);
                string TeacherFName = DataReader["teacherfname"].ToString();
                string TeacherLName = DataReader["teacherlname"].ToString();
                string EmployeeID = DataReader["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(DataReader["hiredate"]);
                double Salary = Convert.ToDouble(DataReader["salary"]);

                Teacher newTeacher = new Teacher();
                newTeacher.TeacherId = TeacherId;
                newTeacher.TeacherFirstName = TeacherFName;
                newTeacher.TeacherLastName = TeacherLName;
                newTeacher.EmployeeID = EmployeeID;
                newTeacher.HireDate = HireDate;
                newTeacher.Salary = Salary;
                //Here the new teacher object is added to the teachers list.
                teachers.Add(newTeacher);


            }
            //Here the connection to the database is closed.
            Connection.Close();


            //Here the list of teachers is returned to the client.
            return teachers;
        }



        /// <summary>
        /// Finds a teacher by their ID
        /// </summary>
        /// <example>
        /// GET api/FindTeacher/1 -> {"TeacherId":1,"TeacherFirstName":"Alexendar","TeacherLastName":"Doe",...}
        /// </example>
        /// <param name="id">The ID of the teacher</param>
        /// <returns>
        /// A Teacher object containing ID, Name, EmployeeID, HireDate, and Salary
        /// </returns>




        //This is the API method that will receive a GET request to the endpoint /api/FindTeacher/{id}
        [HttpGet]
        [Route(template: "FindTeacher/{id}")]

        //This is a method that calls the database and return a teacher object by teacher's ID.
        public Teacher FindTeacher(int id)
        {
            //Teacher is a data type that here is used to hold in the teacher variable the teacher object that will be returned to the client.
            Teacher teacher = new Teacher();
            //MySqlConnection is a data type that here is used to hold in the Connection variable the connection to the database.
            MySqlConnection Connection = _context.GetConnection();
            //Here the connection to the database is opened.
            Connection.Open();

            string SQL = "Select * FROM teachers Where Teacherid = "+id.ToString();

            MySqlCommand Command = Connection.CreateCommand();

            Command.CommandText = SQL;

            MySqlDataReader DataReader = Command.ExecuteReader();


            while (DataReader.Read())
            {
                int TeacherId = Convert.ToInt32(DataReader["teacherid"]);
                string TeacherFName = DataReader["teacherfname"].ToString();
                string TeacherLName = DataReader["teacherlname"].ToString();
                string EmployeeID = DataReader["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(DataReader["hiredate"]);
                double Salary = Convert.ToDouble(DataReader["salary"]);

                teacher.TeacherId = TeacherId;
                teacher.TeacherFirstName = TeacherFName;
                teacher.TeacherLastName = TeacherLName;
                teacher.EmployeeID = EmployeeID;
                teacher.HireDate = HireDate;
                teacher.Salary = Salary;
            }

            Connection.Close(); 


            return teacher;
        }


        /// <summary>
        /// Finds teachers hired within a specified date range
        /// </summary>
        /// <example>
        /// GET api/findbydate?Start=2020-01-01&End=2022-12-31 -> [{"TeacherId":1,"TeacherFirstName":"Alexendar","TeacherLastName":"Doe",...},..]
        /// </example>
        /// <param name="Start">Start date in YYYY-MM-DD format</param>
        /// <param name="End">End date in YYYY-MM-DD format</param>
        /// <returns>
        /// A list of Teacher objects hired within the given date range
        /// </returns>




        [HttpGet]
        [Route(template: "findbydate")]
        public List<Teacher> FindByHireDate([FromQuery] string Start, [FromQuery] string End)
        {

            DateTime startDate = DateTime.Parse(Start);
            DateTime endDate = DateTime.Parse(End);

            List<Teacher> teachers = new List<Teacher>();

            MySqlConnection Connection = _context.GetConnection();

            Connection.Open();

            string SQL = "SELECT * FROM `teachers` WHERE teachers.hiredate BETWEEN '"
                  + startDate.ToString("yyyy-MM-dd") + "' AND '"
                  + endDate.ToString("yyyy-MM-dd") + "'";

            MySqlCommand Command = Connection.CreateCommand();

            Command.CommandText = SQL;

            MySqlDataReader DataReader = Command.ExecuteReader();

            while (DataReader.Read())
            {
                int TeacherId = Convert.ToInt32(DataReader["teacherid"]);
                string TeacherFName = DataReader["teacherfname"].ToString();
                string TeacherLName = DataReader["teacherlname"].ToString();
                string EmployeeID = DataReader["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(DataReader["hiredate"]);
                double Salary = Convert.ToDouble(DataReader["salary"]);

                Teacher newTeacher = new Teacher();
                newTeacher.TeacherId = TeacherId;
                newTeacher.TeacherFirstName = TeacherFName;
                newTeacher.TeacherLastName = TeacherLName;
                newTeacher.EmployeeID = EmployeeID;
                newTeacher.HireDate = HireDate;
                newTeacher.Salary = Salary;
                //Here the new teacher object is added to the teachers list.
                teachers.Add(newTeacher);
            }

            return teachers;

        }





    }
}
