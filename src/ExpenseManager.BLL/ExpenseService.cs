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
            IEnumerable<ExpenseEntityDesign> result = await _expenseRepository.GetAllExpense();
            foreach (ExpenseEntityDesign item in result)
            {
                ExpenseDto expense = new ExpenseDto();
                expense.Name = item.Name;
                expense.Amount = item.Amount;
                expense.Description = item.Description;
                expense.ExpenseDate = item.ExpenseDate;

                expenses.Add(expense);
            }
            return expenses;
        }

        public async Task<ExpenseDto> GetExpenseById(int Id)
        {
            if (Id == default)
                throw new ArgumentException($"{Id} is not a vaild identifier.");

            ExpenseEntityDesign expenseEntity = await _expenseRepository.GetExpenseById(Id);

            ExpenseDto expense = new ExpenseDto();

            if (expenseEntity.Name == null && expenseEntity.Amount == 0)
            {
                throw new Exception("Invalid Id");
            }

            expense.Name = expenseEntity.Name;
            expense.Amount = expenseEntity.Amount;
            expense.Description = expenseEntity.Description;
            expense.ExpenseDate = expenseEntity.ExpenseDate;

            return expense;
        }

        public async Task<int> UpdateExpense(EditExpenseDto expense)
        {
            //if (expense is null)
            //{
            //    throw new Exception("Expense Required.");
            //}

            ExpenseEntityDesign expenseEntity = await _expenseRepository.GetExpenseById(expense.Id);

            if (expense.Name == null && expense.Amount == 0)
            {
                throw new Exception("Invalid Id");
            }

            expenseEntity.Name = expense.Name;
            expenseEntity.Amount = expense.Amount;
            expenseEntity.Description = expense.Description;
            expenseEntity.ExpenseDate = expense.ExpenseDate;

            return await _expenseRepository.UpdateExpense(expenseEntity);
        }

        public async Task<bool> DeleteExpenseById(int Id, bool isHardDelete)
        {
            if (Id == default)
                throw new ArgumentException($"{Id} is no a valid identifier.");

            bool response = await _expenseRepository.RemoveExpense(Id, isHardDelete);
            return response;
        }
    }
}
