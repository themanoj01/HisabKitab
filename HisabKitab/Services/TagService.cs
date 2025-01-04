using HisabKitab.Model;

namespace HisabKitab.Services
{
    public class TagService
    {
        private readonly JsonStorageService<Tags> _storage;

        public TagService()
        {
            _storage = new JsonStorageService<Tags>("tags.json");
        }

        // Add a new tag
        public void AddTag(Tags tag)
        {
            _storage.Add(tag);
        }

        // Get all tags
        public List<Tags> GetTags()
        {
            return _storage.GetAll();
        }

        // Delete a tag by its ID
        public void DeleteTag(Guid id)
        {
            _storage.Delete(id, tag => tag.TagId);
        }
    }
}
