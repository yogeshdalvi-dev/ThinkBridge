using InventoryManager.DAL.Data;
using InventoryManager.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.DAL.RepositoryManager
{
    // Product manager is responsible to all the business logic
    // It creates abstract layer between application and data
    // I have used ProductModel more like ViewModel to keep db classess safe

    public class ProductManager
    {
        // UNit of work is used to save changes at once
        // Also to get repo instance
        private readonly UnitOfWork _unitOfWork;

        // Validation summary
        public List<string> validationSummary;

        public ProductManager()
        {
            _unitOfWork = new UnitOfWork();
            validationSummary = new List<string>();
        }

        public IEnumerable<ProductModel> GetAll()
        {

            var dbProducts = _unitOfWork.GetRepoInstance<Product>().GetAll();

            return from product in dbProducts
                   select new ProductModel
                   {
                       ProductId = product.ProductId,
                       ProductName = product.Name,
                       Price = product.Price,
                       Description = product.Description
                   };

        }

        public void AddNew(ProductModel productModel)
        {
            if (!TryValidate(productModel)) return;

            var customerRepo = _unitOfWork.GetRepoInstance<Product>();

            var product = new Product()
            {
                Name = productModel.ProductName,
                Description = productModel.Description,
                Price = productModel.Price,
                CreationDate = DateTime.Now
            };

            customerRepo.AddNew(product);

            _unitOfWork.SaveChanges();
        }

        public ProductModel GetById(int id)
        {
            var dbProduct = _unitOfWork.GetRepoInstance<Product>().GetById(id);

            return new ProductModel()
            {
                ProductId = dbProduct.ProductId,
                ProductName = dbProduct.Name,
                Description = dbProduct.Description,
                Price = dbProduct.Price
            };
        }

        public void Update(ProductModel productModel)
        {
            if (!TryValidate(productModel)) return;

            var customerRepo = _unitOfWork.GetRepoInstance<Product>();
            var dbProduct = customerRepo.GetById(productModel.ProductId);

            dbProduct.Name = productModel.ProductName;
            dbProduct.Price = productModel.Price;
            dbProduct.Description = productModel.Description;
            dbProduct.CreationDate = DateTime.Now;

            _unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {

            var customerRepo = _unitOfWork.GetRepoInstance<Product>();
            var dbProduct = customerRepo.GetById(id);

            customerRepo.Delete(dbProduct);

            _unitOfWork.SaveChanges();
        }

        private bool TryValidate(ProductModel productModel)
        {
            if (productModel.ProductName == string.Empty)
                validationSummary.Add("Product name is empty");

            if (productModel.Price < 1)
                validationSummary.Add("Product price is not valid");

            if (validationSummary.Any())
                return false;
            else
                return true;
        }

    }
}
