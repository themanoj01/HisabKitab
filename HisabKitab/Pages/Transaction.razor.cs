using HisabKitab.Services;
using HisabKitab.Model;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace HisabKitab.Pages
{
    public partial class Transaction
    {
        private Transactions transaction = new Transactions();
        private List<Transactions> transactions = new List<Transactions>();
        private string Message { get; set; } = string.Empty;
        private bool IsSuccess { get; set; }

        private string searchQuery = string.Empty;
        private DateTime? startDate;
        private DateTime? endDate;
        private TransactionType? selectedType;
        private DefaultTags? selectedTag;

        private List<Transactions> pagedTransactions = new();
        private int currentPage = 1;
        private int pageSize = 5; 
        private int totalPages = 1;
        private bool IsPreviousDisabled => currentPage == 1;
        private bool IsNextDisabled => currentPage == totalPages;

        private Transactions editTransaction;
        private Transactions transactionToDelete;

        private bool isEditModalVisible = false;
        private bool isDeleteModalVisible = false;

        
    
    protected override void OnInitialized()
        {
            transactions = TransactionService.GetAllTransactions();
            UpdatePagination();
        }

        private void UpdatePagination()
    {
        totalPages = (int)Math.Ceiling(transactions.Count / (double)pageSize);
        pagedTransactions = transactions
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

        private void PreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdatePagination();
            }
        }

        private void NextPage()
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                UpdatePagination();
            }
        }
        private void ChangePage(int pageNumber)
        {
            if (pageNumber >= 1 && pageNumber <= totalPages)
            {
                currentPage = pageNumber;
                UpdatePagination();
                StateHasChanged();
            }
        }

        private void HandleTransactionSubmit()
        {
            try
            {
                if (transaction.Type == TransactionType.Debit)
                {
                    if (!TransactionService.HasSufficientBalance(transaction.Amount))
                    {
                        IsSuccess = false;
                        Message = "Not sufficient balance to process this transaction.";
                        return;
                    }
                }
                TransactionService.AddTransaction(transaction);
                IsSuccess = true;
                Message = "Transaction done successfully!";
                transaction = new Transactions();
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                Message = "Transaction failed! Error: " + ex.Message;
            }
        }

        private void ApplyFilters()
        {
            try
            {
                var filteredTransactions = TransactionService.GetAllTransactions();

                if (selectedType.HasValue)
                    filteredTransactions = filteredTransactions.Where(txn => txn.Type == selectedType).ToList();

                if (selectedTag.HasValue)
                    filteredTransactions = filteredTransactions.Where(txn => txn.DefaultTags == selectedTag).ToList();

                if (startDate.HasValue && endDate.HasValue)
                    filteredTransactions = filteredTransactions.Where(txn => txn.Date >= startDate && txn.Date <= endDate).ToList();

                transactions = filteredTransactions;

            }
            catch(Exception ex)
            {
                Message = "Error applying filters:" +ex.Message;
                IsSuccess = false;
            }
            UpdatePagination();
        }

        private void SearchTransactions()
        {
            try
            {
                var searchedTransactions = TransactionService.GetAllTransactions() ?? new List<Transactions>();

                if (!string.IsNullOrWhiteSpace(searchQuery))
                {
                    searchedTransactions = searchedTransactions
                        .Where(txn => txn.Title != null && txn.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                transactions = searchedTransactions;
                Console.WriteLine($"Found {transactions.Count} transactions matching the search query.");
            }
            catch (Exception ex)
            {
                Message = "Error during search: " + ex.Message;
                IsSuccess = false;
            }

            UpdatePagination();
        }


        private void SortByDate(string order)
        {
            if (order == "asc")
            {
                transactions = transactions.OrderBy(txn => txn.Date).ToList();
            }
            else
            {
                transactions = transactions.OrderByDescending(txn => txn.Date).ToList();
            }
            UpdatePagination();
        }
        private void OpenEditModal(Transactions txn)
        {
            editTransaction = new Transactions
            {
                TransactionId = txn.TransactionId,  
                Title = txn.Title,
                Amount = txn.Amount,
                Type = txn.Type,
                Notes = txn.Notes
            };
            isEditModalVisible = true;
        }

        private void CloseEditModal()
        {
            isEditModalVisible = false;
        }

        private void HandleEditTransactionSubmit()
        {
            var transactionToUpdate = transactions.FirstOrDefault(t => t.TransactionId == editTransaction.TransactionId);
            if (transactionToUpdate != null)
            {
                transactionToUpdate.Title = editTransaction.Title;
                transactionToUpdate.Amount = editTransaction.Amount;
                transactionToUpdate.Type = editTransaction.Type;
                transactionToUpdate.Notes = editTransaction.Notes;
                Message = "Transaction updated successfully!";
                IsSuccess = true;
            }
            isEditModalVisible = false;
        }

        private void OpenDeleteModal(Transactions txn)
        {
            transactionToDelete = txn;
            isDeleteModalVisible = true;
        }

        private void CloseDeleteModal()
        {
            isDeleteModalVisible = false;
        }

        private void DeleteTransaction()
        {
            if (transactionToDelete != null)
            {
                transactions.Remove(transactionToDelete);
                Message = "Transaction deleted successfully!";
                IsSuccess = true;
            }
            isDeleteModalVisible = false;
        }

    }
}
