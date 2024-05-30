using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using SanaSDB3.Models;

namespace SanaSDB3.Repositories.XMLRepositories
{
    public class XMLTasksRepository : ITasksRepository
    {
        private readonly string _filePath;

        public XMLTasksRepository(IConfiguration configuration)
        {
            _filePath = configuration.GetConnectionString("XMLConnection") + "TasksStorage.xml";
        }

        public async Task Create(Tasks task)
        {
            var xdoc = await LoadDocumentAsync();
            var newId = xdoc.Root.Elements("Task").Any() ? xdoc.Root.Elements("Task").Max(x => (int)x.Element("Id")) + 1 : 1;

            var newTask = new XElement("Task",
                new XElement("Id", newId),
                new XElement("Name", task.Name),
                new XElement("Completed", task.Completed),
                new XElement("DueDate", task.DueDate),
                new XElement("CategoryId", task.CategoryId.HasValue ? task.CategoryId.ToString() : string.Empty)
            );

            xdoc.Root.Add(newTask);
            await SaveDocumentAsync(xdoc);
        }

        public async Task<Tasks> GetById(int id)
        {
            var xdoc = await LoadDocumentAsync();
            var element = xdoc.Root.Elements("Task").FirstOrDefault(x => (int)x.Element("Id") == id);
            if (element == null) return null;

            return new Tasks
            {
                Id = (int)element.Element("Id"),
                Name = (string)element.Element("Name"),
                Completed = (bool)element.Element("Completed"),
                DueDate = (DateTime)element.Element("DueDate"),
                CategoryId = !string.IsNullOrEmpty((string)element.Element("CategoryId")) ? (int?)int.Parse((string)element.Element("CategoryId")) : null
            };
        }

        public async Task Update(Tasks task)
        {
            var xdoc = await LoadDocumentAsync();
            var element = xdoc.Root.Elements("Task").FirstOrDefault(x => (int)x.Element("Id") == task.Id);
            if (element != null)
            {
                element.SetElementValue("Name", task.Name);
                element.SetElementValue("Completed", task.Completed);
                element.SetElementValue("DueDate", task.DueDate);
                element.SetElementValue("CategoryId", task.CategoryId.HasValue ? task.CategoryId.ToString() : string.Empty);
                await SaveDocumentAsync(xdoc);
            }
        }

        public async Task DeleteById(int id)
        {
            var xdoc = await LoadDocumentAsync();
            var element = xdoc.Root.Elements("Task").FirstOrDefault(x => (int)x.Element("Id") == id);
            if (element != null)
            {
                element.Remove();
                await SaveDocumentAsync(xdoc);
            }
        }

        public async Task<IEnumerable<Tasks>> GetAll()
        {
            var xdoc = await LoadDocumentAsync();
            return xdoc.Root.Elements("Task").Select(x => new Tasks
            {
                Id = (int)x.Element("Id"),
                Name = (string)x.Element("Name"),
                Completed = (bool)x.Element("Completed"),
                DueDate = (DateTime)x.Element("DueDate"),
                CategoryId = !string.IsNullOrEmpty((string)x.Element("CategoryId")) ? (int?)int.Parse((string)x.Element("CategoryId")) : null
            });
        }

        private async Task<XDocument> LoadDocumentAsync()
        {
            if (!File.Exists(_filePath))
            {
                var xdoc = new XDocument(new XElement("Tasks"));
                await SaveDocumentAsync(xdoc);
            }

            using var stream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Read);
            return await Task.Run(() => XDocument.Load(stream));
        }

        private async Task SaveDocumentAsync(XDocument xdoc)
        {
            using var stream = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
            await Task.Run(() => xdoc.Save(stream));
        }
    }
}
