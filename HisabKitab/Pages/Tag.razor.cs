using HisabKitab.Model;
using HisabKitab.Services;
namespace HisabKitab.Pages
{
    public partial class Tag
    {
        private Tags newTag = new Tags();
        private Tags tagToDelete = new Tags();
        private List<Tags> tags = new List<Tags>();
        private string Message {  get; set; }
        private bool IsSuccess {  get; set; }
        private bool IsDeleteModalVisible = false;
        protected override void OnInitialized()
        {
            tags = TagService.GetAllTags();
        }
        private void HandleTagSubmit()
        {
            try
            {
                newTag.TagId = Guid.NewGuid();
                TagService.AddTag(newTag);
                tags.Add(newTag); 
                newTag = new Tags(); 
                IsSuccess = true;
                Message = "Tag added successfully!";
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                Message = "Error encountered: " + ex.Message;
            }
        }
        private void OpenDeleteModal(Tags tagToDelete)
        {
            this.tagToDelete = tagToDelete;
            IsDeleteModalVisible = true;
        }

        private void CloseDeleteModal()
        {
            IsDeleteModalVisible = false;
        }
        private void DeleteTag()
        {
            try
            {
                if (tagToDelete != null)
                {
                    TagService.RemoveTag(tagToDelete.TagId);
                    tags.Remove(tagToDelete); 
                    Message = "Tag deleted successfully!";
                    IsSuccess = true;
                }
                IsDeleteModalVisible = false;
            }
            catch (Exception ex)
            {
                Message = "Error encountered: " + ex.Message;
            }
        }

    }
}
