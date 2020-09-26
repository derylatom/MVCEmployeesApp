using MVCEmployeesApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MVCEmployeesApp.Classes
{
    public class TrnEmployees
    {

        SqlDataAdapter adapter;
        DataTable dt;
        SqlCommand cmd;

        public Employee EmployeesCRUDOperation(Employee employee)
        {
            Employee result = new Employee();
            try
            {
                var connection = sqlConnection();
                connection.Open();
                cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Employees";
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Designation", employee.Designation);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@StmtType", employee.StmtType);
                cmd.Parameters.AddWithValue("@Id", employee.Id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result.Message = "success";
                    result.Id = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close();
                cmd.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                result.Message= "Error:" + ex.Message.ToString();
            }
            return result;
        }

        public SqlConnection sqlConnection()
        {
            return (new SqlConnection(@"Data Source=192.168.0.192;Initial Catalog=ESource;User ID=sa;Password=789"));
        }

        public Employee GetEmployees(int id)
        {
            try
            {
                var connection = sqlConnection();
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "get_Employees";
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Employee emp = new Employee();
                emp.lstEmployees = new List<Employees>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (id == 0)
                        {
                            Employees em = new Employees();
                            em.Id = Convert.ToInt32(row["Id"].ToString());
                            em.Name = row["Name"].ToString();
                            em.Designation = row["Designation"].ToString();
                            em.Department = row["Department"].ToString();
                            emp.lstEmployees.Add(em);
                        }
                        else
                        {
                            emp.Name = row["Name"].ToString();
                            emp.Designation = row["Designation"].ToString();
                            emp.Department = row["Department"].ToString();
                            emp.Id = Convert.ToInt32(row["Id"].ToString());
                        }
                    }
                }
                return emp;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
