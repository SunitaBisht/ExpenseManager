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

        public async Task<IEnumerable<ExpenseDto>> GetAllExpenses()
        {
            List<ExpenseDto> expenses = new List<ExpenseDto>();
            IEnumerable<ExpenseEntityDesign> result=  await _expenseRepository.GetAllExpense();
            foreach (ExpenseEntityDesign item in result)
            {
                ExpenseDto expense = new ExpenseDto();
                expense.Name=item.Name;
                expense.Amount = item.Amount;
                expense.Description = item.Description;
                expense.ExpenseDate = item.ExpenseDate;

                expenses.Add(expense);
            }
            return expenses;
        }

        public async Task<ExpenseDto> GetCategoryById(int Id)
        {
            throw new NotImplementedException();
        }

        //public async Task<int> UpdateExpense(ExpenseDto expense)
        //{
        //    ExpenseEntityDesign entity = await _expenseRepository.UpdateExpense(expense.Id);

        //    //Category xCategory = await _categoryRepository.GetCategoryById(category.Id);

        //    //if (xCategory == null)
        //    //    throw new UserFriendlyException($"{category.Id} is not valid {typeof(Category)} identifier.");

        //    //xCategory.Name = category.Name;
        //    //xCategory.Description = category.Description;
        //    //xCategory.IsActive = true;

        //    //return await _categoryRepository.UpdateCategory(xCategory);
        //}
    }
}
