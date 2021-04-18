using ExpenseManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseManager.Web.Controllers
{
    [RoutePrefix("Expense")]
    public class ExpenseReportController : Controller
    {
        SqlConnection sqlconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        // GET: ExpenseReport
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Save")]
        public void Insert(ExpenseViewModel expense)
        {
            string cmdText = "INSERT INTO TblExpense(Name,Amount,Description,ExpenseDate) VALUES(@name,@amount,@description,@expensedate)";
            SqlCommand xsqlcommand = new SqlCommand(cmdText, sqlconnection);

            xsqlcommand.Parameters.AddWithValue("@name", expense.Name);
            xsqlcommand.Parameters.AddWithValue("@amount", expense.Amount);
            xsqlcommand.Parameters.AddWithValue("@description", expense.Description);
            xsqlcommand.Parameters.AddWithValue("@expensedate", expense.ExpenseDate);

            sqlconnection.Open();
            xsqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
        }

        [HttpGet]
        public ActionResult List()
        {
            List<ExpenseViewModel> expenseList = new List<ExpenseViewModel>();
            string cmdText = "Select * from TblExpense";
            SqlCommand sqlcommand = new SqlCommand(cmdText, sqlconnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcommand);
            DataTable dataTable = new DataTable();
            sqlconnection.Open();
            sqlDataAdapter.Fill(dataTable);
            sqlconnection.Close();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                ExpenseViewModel expense = new ExpenseViewModel();

                expense.Id = Convert.ToInt32(dataRow["Id"]);
                expense.Name = dataRow["Name"].ToString();
                expense.Amount = Convert.ToDecimal(dataRow["Amount"]);
                expense.Description = dataRow["Description"].ToString();
                expense.ExpenseDate = Convert.ToDateTime(dataRow["ExpenseDate"]);

                expenseList.Add(expense);
            }
            return View(expenseList);
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            ExpenseViewModel expense = new ExpenseViewModel();
            string cmdText = "Select * from TblExpense where Id=@Id";
            SqlCommand xsqlcommand = new SqlCommand(cmdText, sqlconnection);
            xsqlcommand.Parameters.AddWithValue("@Id", Id);
            sqlconnection.Open();
            SqlDataReader dr = xsqlcommand.ExecuteReader();

            while (dr.Read())
            {
                expense.Id = Convert.ToInt32(dr["Id"]);
                expense.Name = dr["Name"].ToString();
                expense.Amount = Convert.ToDecimal(dr["Amount"]);
                expense.Description = dr["Description"].ToString();
                expense.ExpenseDate = Convert.ToDateTime(dr["ExpenseDate"]);
            }
            sqlconnection.Close();
            return View("Edit", expense);
        }

        [HttpPost]
        [Route("Edit")]
        public ActionResult Edit(int id, ExpenseViewModel expense)
        {
            string cmdText = "Update TblExpense set Name=@name,Amount=@amount,Description=@description,ExpenseDate=expensedate Where id=@id"; 
            SqlCommand xsqlcommand = new SqlCommand(cmdText, sqlconnection);

            xsqlcommand.Parameters.AddWithValue("@name", expense.Name);
            xsqlcommand.Parameters.AddWithValue("@amount", expense.Amount);
            xsqlcommand.Parameters.AddWithValue("@description", expense.Description);
            xsqlcommand.Parameters.AddWithValue("@expensedate", expense.ExpenseDate);
            xsqlcommand.Parameters.AddWithValue("@id", id);
            sqlconnection.Open();
            xsqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            ExpenseViewModel expense = new ExpenseViewModel();
            string cmdText = "Select * from TblExpense where Id=@Id";
            SqlCommand xsqlcommand = new SqlCommand(cmdText, sqlconnection);
            xsqlcommand.Parameters.AddWithValue("@Id", Id);
            sqlconnection.Open();
            SqlDataReader dr = xsqlcommand.ExecuteReader();

            while (dr.Read())
            {
                expense.Id = Convert.ToInt32(dr["Id"]);
                expense.Name = dr["Name"].ToString();
                expense.Amount = Convert.ToDecimal(dr["Amount"]);
                expense.Description = dr["Description"].ToString();
                expense.ExpenseDate = Convert.ToDateTime(dr["ExpenseDate"]);
            }
            sqlconnection.Close();
            return View("Delete", expense);
        }


        [HttpPost]
        [Route("Delete")]
        public ActionResult Delete(int id)
        {
            string cmdText = "Delete from TblExpense where id=@id";
            SqlCommand xsqlcommand = new SqlCommand(cmdText, sqlconnection);

            xsqlcommand.Parameters.AddWithValue("@id", id);

            sqlconnection.Open();
            xsqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
            return RedirectToAction("List");
        }
    }
}
