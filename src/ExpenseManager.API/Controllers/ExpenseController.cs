using ExpenseManager.BLL;
using ExpenseManager.BLL.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExpenseManager.API.Controllers
{
    [RoutePrefix("api/expense")]
    public class ExpenseController : ApiController
    {
        private ExpenseService _expenseService;    //declaration
        public ExpenseController()
        {
            _expenseService = new ExpenseService();    //Initialization
        }

        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> Add(ExpenseDto expenseRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int isAdded = await _expenseService.SaveExpense(expenseRequest);
            ApiResponse res = new ApiResponse();
            res.Message = "Expense Added Successfully";
            return Ok(isAdded);
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IHttpActionResult> GetAllExpense()
        {
            IEnumerable<ExpenseDto> results = await _expenseService.GetAllExpenses();
            return Ok(results);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IHttpActionResult> GetExpenseById(int Id)
        {
            ExpenseDto expense = await _expenseService.GetExpenseById(Id);
            return Ok(expense);
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IHttpActionResult> Edit(EditExpenseDto expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int isUpdated = await _expenseService.UpdateExpense(expense);
            ApiResponse res = new ApiResponse();
            res.Message = "Expense Update Successfully";
            return Ok(isUpdated);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IHttpActionResult> DeleteCategory(int Id, bool IsHardDelete = false)
        {
            if (Id == 0)
            {
                return BadRequest(ModelState);
            }
                

            bool response = await _expenseService.DeleteExpenseById(Id, IsHardDelete);
            return Ok("Deleted Successfully");
        }
    }
}