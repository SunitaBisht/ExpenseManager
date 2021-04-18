﻿using ExpenseManager.API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExpenseManager.API.Controllers
{

    [RoutePrefix("api/group")]
    public class GroupAPIController : ApiController
    {
        SqlConnection sqlconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        [Route("get-all")]
        [HttpGet]
        public IEnumerable<ExpenseViewModel> GetAllGroups()
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
            return expenseList;
        }

        [Route("get/{id}")]
        [HttpGet]
        public IHttpActionResult GetGroupById(int id)
        {
            ExpenseViewModel expense = new ExpenseViewModel();
            string cmdText = "Select * from TblExpense where id=@id";
            SqlCommand xsqlcommand = new SqlCommand(cmdText, sqlconnection);
            xsqlcommand.Parameters.AddWithValue("@id", id);
            sqlconnection.Open();
            SqlDataReader dr = xsqlcommand.ExecuteReader();

            while (dr.Read())
            {
                expense.Id = Convert.ToInt32(dr["id"]);
                expense.Name = dr["Name"].ToString();
                expense.Amount = Convert.ToDecimal(dr["Amount"]);
                expense.Description = dr["Description"].ToString();
                expense.ExpenseDate = Convert.ToDateTime(dr["ExpenseDate"]);
            }
            sqlconnection.Close();
            return Ok(expense);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult CreateExpense(ExpenseViewModel expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string cmdText = "INSERT INTO TblExpense(Name,Amount,Description,ExpenseDate) VALUES(@name,@amount,@description,@expensedate)";
            SqlCommand xsqlcommand = new SqlCommand(cmdText, sqlconnection);

            xsqlcommand.Parameters.AddWithValue("@name", expense.Name);
            xsqlcommand.Parameters.AddWithValue("@amount", expense.Amount);
            xsqlcommand.Parameters.AddWithValue("@description", expense.Description);
            xsqlcommand.Parameters.AddWithValue("@expensedate", expense.ExpenseDate);

            sqlconnection.Open();
            int affectedRows = xsqlcommand.ExecuteNonQuery();
            sqlconnection.Close();

            return Ok(affectedRows);
        }
    }
}
