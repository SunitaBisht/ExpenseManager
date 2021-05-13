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
            xsqlcommand.Parameters.AddWithValue("@createdon", DateTime.Now);

            await xSqlConnection.OpenAsync();
            int IsInserted = await xsqlcommand.ExecuteNonQueryAsync();
            xSqlConnection.Close();
            return IsInserted;
        }

        public async Task<IEnumerable<ExpenseEntityDesign>> GetAllExpense()
        {
            List<ExpenseEntityDesign> expenseList = new List<ExpenseEntityDesign>();
            string cmdText = "Select * from TblExpense ";
            SqlCommand sqlcommand = new SqlCommand(cmdText, xSqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcommand);
            DataTable dataTable = new DataTable();
            await xSqlConnection.OpenAsync();
            sqlDataAdapter.Fill(dataTable);
            xSqlConnection.Close();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                ExpenseEntityDesign expense = new ExpenseEntityDesign();

                expense.Id = Convert.ToInt32(dataRow["Id"]);
                expense.Name = dataRow["Name"].ToString();
                expense.Amount = Convert.ToDecimal(dataRow["Amount"]);
                expense.Description = dataRow["Description"].ToString();
                expense.ExpenseDate = Convert.ToDateTime(dataRow["ExpenseDate"]);
                expense.CreatedOn = Convert.ToDateTime(dataRow["CreatedOn"]);
                expense.LastUpdatedOn = Convert.ToDateTime(dataRow["LastUpdatedOn"]);

                expenseList.Add(expense);
            }
            return expenseList;
        }

        public Task<ExpenseEntityDesign> GetExpenseById()
        {
            throw new NotImplementedException();
        }

        public async Task<ExpenseEntityDesign> GetExpenseById(int Id)
        {
            ExpenseEntityDesign entity = null;
            if (Id == 0)
                throw new ArgumentNullException();

            string cmdText = "SELECT * FROM TblExpense WHERE Id=@Id";
            SqlCommand xsqlcommand = new SqlCommand(cmdText, xSqlConnection);
            xsqlcommand = new SqlCommand(cmdText, xSqlConnection);

            xsqlcommand.Parameters.AddWithValue("@Id", Id);

            await xSqlConnection.OpenAsync();
            SqlDataReader xSqlDataReader = await xsqlcommand.ExecuteReaderAsync();
            while (xSqlDataReader.HasRows && await xSqlDataReader.ReadAsync())
            {
                entity = new ExpenseEntityDesign
                {
                    Id = Convert.ToInt32(xSqlDataReader["Id"]),
                    Name = xSqlDataReader["Name"].ToString(),
                    Amount = Convert.ToDecimal(xSqlDataReader["Amount"]),
                    Description = xSqlDataReader["Description"].ToString(),
                    ExpenseDate = Convert.ToDateTime(xSqlDataReader["ExpenseDate"]),
                    CreatedOn = Convert.ToDateTime(xSqlDataReader["CreatedOn"]),
                    // LastUpdatedOn = Convert.ToDateTime(xSqlDataReader["LastUpdatedOn"])

                };
                if (xSqlDataReader["LastUpdatedOn"] != DBNull.Value)
                {
                    entity.LastUpdatedOn = Convert.ToDateTime(xSqlDataReader["LastUpdatedOn"]);
                }
            }
            return entity;
        }


        public async Task<int> UpdateExpense(ExpenseEntityDesign xExpense)
        {
            if (xExpense == null)
            {
                throw new ArgumentNullException("No Expense Available");
            }

            string cmdText = "UPDATE TblExpense SET Name= @name,Amount=@amount, Description= @description,ExpenseDate=@expensedate,IsDeleted=@isdeleted, LastUpdatedOn = @lastupdatedon WHERE Id =@Id";
            SqlCommand xsqlcommand = new SqlCommand(cmdText, xSqlConnection);
            xsqlcommand = new SqlCommand(cmdText, xSqlConnection);

            xsqlcommand.Parameters.AddWithValue("@Id", xExpense.Id);
            xsqlcommand.Parameters.AddWithValue("@name", xExpense.Name);
            xsqlcommand.Parameters.AddWithValue("@amount", xExpense.Amount);
            xsqlcommand.Parameters.AddWithValue("@description", xExpense.Description);
            xsqlcommand.Parameters.AddWithValue("@expensedate", xExpense.ExpenseDate);
            xsqlcommand.Parameters.AddWithValue("@isdeleted", false);
            xsqlcommand.Parameters.AddWithValue("@lastupdatedon", xExpense.LastUpdatedOn);

            await xSqlConnection.OpenAsync();
            int isUpdated = await xsqlcommand.ExecuteNonQueryAsync();
            xSqlConnection.Close();
            return isUpdated;
        }
    }
}