using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEmployeesApp.Classes;
using MVCEmployeesApp.Models;

namespace MVCEmployeesApp.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/

        [HttpGet]
        public ActionResult Index(int id = 0, String type = null)
        {
            Employee emp = new Employee();
            TrnEmployees trn = new TrnEmployees();

            if (id > 0 && !String.IsNullOrWhiteSpace(type))
            {
                try
                {
                    if (type.ToLower().Equals("d"))
                    {
                        emp.Id = id;
                        emp.StmtType = "Delete";
                        emp.Name = "";
                        emp.Designation = "";
                        emp.Department = "";
                        emp.buttonText = "Add";
                        emp = trn.EmployeesCRUDOperation(emp);

                        if (emp.Message.Equals("success"))
                            emp.Message = "Employee deleted successfully.";

                        emp.lstEmployees = trn.GetEmployees(0).lstEmployees;
                        emp.buttonText = "Add";
                        emp.StmtType = "Insert";
                    }
                    else if (type.ToLower().Equals("e"))
                    {
                        emp = trn.GetEmployees(id);
                        emp.StmtType = "Update";
                        emp.buttonText = "Update";
                        emp.Message = "";
                        emp.lstEmployees = trn.GetEmployees(0).lstEmployees;
                    }
                }
                catch (Exception ex)
                {
                    emp.lstEmployees = null;
                    emp.buttonText = "Add";
                    emp.StmtType = "Insert";
                    emp.Message = ex.Message.ToString();
                }
            }
            else
            {
                emp.Message = "";
                emp.StmtType = "Insert";
                emp.buttonText = "Add";
                emp.lstEmployees = trn.GetEmployees(0).lstEmployees;
            }

            if (emp.lstEmployees == null || emp.lstEmployees.Count == 0)
            {
                emp.lstEmployees.Add(new Employees
                {
                    Name = "",
                    Id = 0,
                    Designation = "Error",
                    Department = "Error",
                    lstEmployees = null

                });
            }
            return View(emp);
        }

        [HttpPost]
        public ActionResult Index(Employee emp)
        {
            TrnEmployees trn = new TrnEmployees();

            if (ModelState.IsValid)
            {
                Employee e = trn.EmployeesCRUDOperation(emp);
                if (!String.IsNullOrWhiteSpace(e.Message) && e.Message.Equals("success"))
                {
                    var c = trn.GetEmployees(0);
                    if (c != null)
                    {
                        e.lstEmployees = c.lstEmployees;
                    }
                    e.buttonText = "Add";
                    if (e.Message.ToLower().Equals("success"))
                    {
                        e.Message = "Employee saved successfully";
                    }
                    e.StmtType = "Insert";
                }
                else
                {
                    e.Department = emp.Department;
                    e.Designation = emp.Designation;
                    e.Id = emp.Id;
                    e.Name = emp.Name;
                    e.StmtType = emp.StmtType;
                    e.Message = emp.Message;
                    e.buttonText = "Update";
                }
                return View(e);
            }
            else
            {
                var c = trn.GetEmployees(0);
                if (c != null)
                {
                    emp.lstEmployees = c.lstEmployees;
                }
                emp.buttonText = "Add";
                emp.StmtType = "Insert";
                return View(emp);
            }

        }

    }
}
