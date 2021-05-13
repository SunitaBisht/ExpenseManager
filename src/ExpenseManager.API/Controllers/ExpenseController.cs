﻿using ExpenseManager.BLL;
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
            return Ok(isAdded);
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IHttpActionResult> GetAllExpense()
        {
            IEnumerable<ExpenseDto> results = await _expenseService.GetAllExpenses();
            return Ok(results);
            // IEnumerable<ExpenseDto> result = await _expenseService.GetAllExpenses();
          
            //if (result == null)
            //{
            //    return BadRequest(ModelState);
            //}
            //return Ok(result);
        }

       
    }
}