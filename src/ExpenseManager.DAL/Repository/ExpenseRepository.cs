using ExpenseManager.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExpenseManager.DAL.Repository
{
    public class ExpenseRepository
    {
        readonly SqlConnection xSqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        public async Task<int> Add(ExpenseEntityDesign xExpense)
        {
            string cmdText = "INSERT INTO TblExpense(Name,Amount,Description,ExpenseDate,IsDeleted,CreatedOn) VALUES(@name,@amount,@description,@expensedate,@isdeleted,@createdon)";
            SqlCommand xsqlcommand = new SqlCommand(cmdText, xSqlConnection);
            xsqlcommand = new SqlCommand(cmdText, xSqlConnection);

            xsqlcommand.Parameters.AddWithValue("@name", xExpense.Name);
            xsqlcommand.Parameters.AddWithValue("@amount", xExpense.Amount);
            xsqlcommand.Parameters.AddWithValue("@description", xExpense.Description);
            xsqlcommand.Parameters.AddWithValue("@expensedate", xExpense.ExpenseDate);
            xsqlcommand.Parameters.AddWithValue("@isdeleted", false);
            xsqlcommand.Parameters.AddWithValue("@createdon",DateTime.Now);


            await xSqlConnection.OpenAsync();
            int IsInserted = await xsqlcommand.ExecuteNonQueryAsync();
            xSqlConnection.Close();
            return IsInserted;
        }
    }
}
