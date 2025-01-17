using HisabKitab.Model;
using HisabKitab.Services;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Transactions;
using System.Xml.Serialization;

namespace HisabKitab.Pages
{
    public partial class Debt
    {
        private Debts debt = new Debts();
        private Debts editDebt = new Debts();
        private Debts debtToDelete = new Debts();
        private List<Debts> debts = new List<Debts>();
        private List<Debts> pagedDebts = new List<Debts>();
        private string Message { get; set; } = string.Empty;
        private bool IsCleared {  get; set; }
        private bool IsSuccess { get; set; }
        private bool isEditModalVisible = false;
        private bool isDeleteModalVisible = false;
        private int currentPage = 1;
        private int pageSize = 5;
        private int totalPages = 1;

        private bool IsPreviousDisabled => currentPage == 1;
        private bool IsNextDisabled => currentPage == totalPages;

        protected override void OnInitialized()
        {
            debts = DebtService.GetAllDebts().ToList();
            SortByDate("desc");
            UpdatePagination();
        }

        private void UpdatePagination()
        {
            totalPages = (int)Math.Ceiling(debts.Count / (double)pageSize);
            pagedDebts = debts
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        private void HandleDebtSubmit()
        {
            try
            {
                DebtService.AddDebt(debt);
                debt = new Debts();
                IsSuccess = true;
                Message = "Debt added successfully!";
                Nav.Refresh();
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                Message = "Error encountered: " + ex.Message;
            }
        }

        private void ClearDebt(Guid id)
        {
            try
            {
                DebtService.ClearDebt(id);
                Message = "Debt cleared successfully!";
                IsSuccess = true;
                Nav.Refresh();
            }
            catch (Exception ex)
            {
                Message = "Error encountered: " + ex.Message;
                IsSuccess = false;
            }
        }
        private void HandleEditDebtSubmit()
        {
            try
            {
                var debtToUpdate = debts.FirstOrDefault(d => d.DebtId == editDebt.DebtId);
                if (debtToUpdate != null)
                    {
                        debtToUpdate.Source = editDebt.Source;
                        debtToUpdate.DueDate = editDebt.DueDate;
                        debtToUpdate.Amount = editDebt.Amount;
                        debtToUpdate.Notes = editDebt.Notes;
                        Message = "Debt updated successfully!";
                        IsSuccess = true;
                    }

                    isEditModalVisible = false;
                Nav.Refresh();
            }
            
            catch (Exception ex)
            {
                Message = "Error encountered: " + ex.Message;
                IsSuccess = false;
            }
        }

        private void OpenEditModal(Debts debtToEdit)
        {
            editDebt = new Debts
            {
                DebtId = debtToEdit.DebtId,
                Amount = debtToEdit.Amount,
                Source = debtToEdit.Source,
                DueDate = debtToEdit.DueDate,
                Notes = debtToEdit.Notes
            };
            isEditModalVisible = true;
        }

        private void CloseEditModal()
        {
            isEditModalVisible = false;
        }

        private void OpenDeleteModal(Debts debtToDelete)
        {
            this.debtToDelete = debtToDelete;
            isDeleteModalVisible = true;
        }

        private void CloseDeleteModal()
        {
            isDeleteModalVisible = false;
        }

        private void DeleteDebt()
        {
            try
            {
                if (debtToDelete != null)
                {
                    DebtService.DeleteDebt(debtToDelete.DebtId);  
                    debts.Remove(debtToDelete);
                    Message = "Debt deleted successfully!";
                    IsSuccess = true;
                }
                isDeleteModalVisible = false;
                Nav.Refresh();
            }
            catch (Exception ex)
            {
                Message = "Error encountered: " + ex.Message;
            }
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
        private void SortByDate(string order)
        {
            if (order == "asc")
            {
                debts = debts.OrderBy(txn => txn.TakenDate).ToList();
            }
            else
            {
                debts = debts.OrderByDescending(txn => txn.TakenDate).ToList();
            }
            UpdatePagination();
        }
    }
}
