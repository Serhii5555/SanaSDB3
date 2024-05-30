using SanaSDB3.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using SanaSDB3.Models.ViewModels;

namespace SanaSDB3.Repositories.XMLRepositories
{
    public class XMLCategoriesRepository : ICategoriesRepository
    {
        private readonly string _filePath;

        public XMLCategoriesRepository(IConfiguration configuration)
        {
            _filePath = configuration.GetConnectionString("XMLConnection") + "CategoriesStorage.xml";
        }

        public async Task Create(Categories category)
        {
            var xdoc = await LoadDocumentAsync();
            var newId = xdoc.Root.Elements("Category").Any() ? xdoc.Root.Elements("Category").Max(x => (int)x.Element("Id")) + 1 : 1;

            var newCategory = new XElement("Category",
                new XElement("Id", newId),
                new XElement("Name", category.Name)
            );

            xdoc.Root.Add(newCategory);
            await SaveDocumentAsync(xdoc);
        }

        public async Task DeleteById(int id)
        {
            var xdoc = await LoadDocumentAsync();
            var element = xdoc.Root.Elements("Category").FirstOrDefault(x => (int)x.Element("Id") == id);
            if (element != null)
            {
                element.Remove();
                await SaveDocumentAsync(xdoc);
            }
        }

        public async Task<IEnumerable<Categories>> GetAll()
        {
            var xdoc = await LoadDocumentAsync();
            return xdoc.Root.Elements("Category").Select(x => new Categories
            {
                Id = (int)x.Element("Id"),
                Name = (string)x.Element("Name")
            });
        }

        public async Task<Categories> GetById(int id)
        {
            var xdoc = await LoadDocumentAsync();
            var element = xdoc.Root.Elements("Category").FirstOrDefault(x => (int)x.Element("Id") == id);
            if (element == null) return null;

            return new Categories
            {
                Id = (int)element.Element("Id"),
                Name = (string)element.Element("Name")
            };
        }

        public async Task Update(Categories category)
        {
            var xdoc = await LoadDocumentAsync();
            var element = xdoc.Root.Elements("Category").FirstOrDefault(x => (int)x.Element("Id") == category.Id);
            if (element != null)
            {
                element.SetElementValue("Name", category.Name);
                await SaveDocumentAsync(xdoc);
            }
        }

        private async Task<XDocument> LoadDocumentAsync()
        {
            if (!File.Exists(_filePath))
            {
                var xdoc = new XDocument(new XElement("Categories"));
                await SaveDocumentAsync(xdoc);
            }

            using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
            return await Task.Run(() => XDocument.Load(stream));
        }

        private async Task SaveDocumentAsync(XDocument xdoc)
        {
            using var stream = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
            await Task.Run(() => xdoc.Save(stream));
        }
    }
}
