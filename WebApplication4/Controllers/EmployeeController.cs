using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class EmployeeController : Controller
    {
        string connectionString = "server=sql6.freesqldatabase.com;port=3306;user=sql6641563;password=2NvV3JRVfy;database=sql6641563";
        public ActionResult Create()
        {
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee, HttpPostedFileBase photoFile,List<EducationDetails> educations)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return View("Index");
                }
                if (photoFile != null && photoFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(photoFile.InputStream))
                    {
                        employee.ProfileImage = binaryReader.ReadBytes(photoFile.ContentLength);
                    }
                }


                using (var connection = new MySqlConnection(connectionString)) // Make sure to use MySqlConnection for MySQL
                {
                    connection.Open();

                    var insertQuery = "INSERT INTO Employee (FirstName, LastName, DateOfBirth, Gender, ContactNumber, Email,ProfileImage) " +
                                      "VALUES (@EmployeeCode, @FirstName, @LastName, @DateOfBirth, @Gender, @ContactNumber, @Email,ProfileImage)";

                    using (var command = new MySqlCommand(insertQuery, connection)) // Use MySqlCommand for MySQL
                    {

                        command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        command.Parameters.AddWithValue("@LastName", employee.LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                        command.Parameters.AddWithValue("@Gender", employee.Gender);
                        command.Parameters.AddWithValue("@ContactNumber", employee.ContactNumber);
                        command.Parameters.AddWithValue("@Email", employee.Email);
                        command.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);


                        command.ExecuteNonQuery();
                    }
                    foreach (var education in educations)
                    {
                        var insertEducationQuery = "INSERT INTO Education (EmployeeId, Degree, PassingYear, Percentage) " +
                                                   "VALUES (LAST_INSERT_ID(), @Degree, @PassingYear, @Percentage)";

                        using (var educationCommand = new MySqlCommand(insertEducationQuery, connection))
                        {
                            educationCommand.Parameters.AddWithValue("@Degree", education.Degree);
                            educationCommand.Parameters.AddWithValue("@PassingYear", education.PassingYear);
                            educationCommand.Parameters.AddWithValue("@Percentage", education.Percentage);

                            educationCommand.ExecuteNonQuery();
                        }
                    }
                }
                // Show success message
                TempData["Message"] = "Employee created successfully.";
            }
            catch (Exception ex)
            {
                // Show error message
                TempData["Error"] = "An error occurred while creating the employee: " + ex.Message;
            }

            return RedirectToAction("Index");

        }


    }
}
