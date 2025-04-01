using Cumulative01.Models;
using Microsoft.AspNetCore.Mvc;

//This is the controller that will handle the requests from the teacher page.
namespace Cumulative01.Controllers
{
    //This is the route that will be used to access the controller.
    public class TeacherPageController : Controller
    {

        //this is the private variable that will hold the connection to the API controller.
        private readonly TeacherAPIController _api;


        //This is the constructor that will assign the connection to the API controller to the private variable.
        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }

        //This is the method that will be called when the user wants to see the list of teachers.

        /// <summary>
        /// Returns a list of Teachers to be displayed on the page
        /// </summary>
        /// <example>
        /// GET TeacherPage/List -> [{"TeacherId":1,"TeacherFirstName":"Alexendar","TeacherLastName":"Doe",...},..]
        /// </example>
        /// <returns>
        /// A list of Teacher objects containing ID, Name, EmployeeID, HireDate, and Salary
        /// </returns>

        public IActionResult List()
        {

            //This is the list of teachers that will hold the teachers instances in as objects in the list.
            List<Teacher> Teach = _api.ListTeacherNames();
            //This is the view that will be returned to the user with the list of teachers.
            return View(Teach);
        }

        //This is the method that will be called when the user wants to see the details of a teacher by its ID.

        /// <summary>
        /// Returns details of a specific teacher based on the given ID
        /// </summary>
        /// <example>
        /// GET TeacherPage/Show/1 -> {"TeacherId":1,"TeacherFirstName":"Alexendar","TeacherLastName":"Doe",...}
        /// </example>
        /// <param name="Id">The ID of the teacher</param>
        /// <returns>
        /// A Teacher object containing ID, Name, EmployeeID, HireDate, and Salary
        /// </returns>

        public IActionResult Show(int Id)
        {
            //This is the Teacher Class in the model that will hold the teacher instance in as an object.
            Teacher teach1 = _api.FindTeacher(Id);
            //This is the view that will be returned to the user with the details of the teacher.
            return View(teach1);
        }
    }
}
