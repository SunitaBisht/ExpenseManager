using ExpenseManager.BLL.DataTransferObject;
using ExpenseManager.DAL.Entity;
using ExpenseManager.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.BLL
{
    public class ExpenseService
    {
        private ExpenseRepository _expenseRepository;

        public ExpenseService()
        {
            _expenseRepository = new ExpenseRepository();
        }

        public async Task<int> SaveExpense(ExpenseDto expense)
        {
            if (expense is null)
            {
                throw new Exception("Expense Required.");
            }

            ExpenseEntityDesign entity = new ExpenseEntityDesign
            {
                Name = expense.Name,
                Description = expense.Description,
                Amount = expense.Amount,
                ExpenseDate = expense.ExpenseDate
            };

            return await _expenseRepository.Add(entity);
        }
    }
}
