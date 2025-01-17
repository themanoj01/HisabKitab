using HisabKitab.Abstraction;
using HisabKitab.Model;
using HisabKitab.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HisabKitab.Services
{
    public class TagService : TagBase, ITagService
    {
        private List<Tags> tags;
        public TagService()
        {
            var directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "[]");
            }
            tags = LoadTags();
        }

        public void AddTag(Tags tag)
        {
            if (tags.Any(t => t.TagName.Equals(tag.TagName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("A tag with the same name already exists.");
            }

            tags.Add(tag);
            SaveTags(tags);

        }

        public List<Tags> GetAllTags()
        {
            return LoadTags();
        }

        public void RemoveTag(Guid id)
        {
            var tagToRemove = tags.FirstOrDefault(t => t.TagId == id);
            if (tagToRemove == null)
            {
                throw new KeyNotFoundException("Tag not found.");
            }

            tags.Remove(tagToRemove);
            SaveTags(tags);
        }
    }
}
