using InventoryManager.DAL.Model;
using InventoryManager.DAL.RepositoryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryManager.Controllers
{
    public class ProductController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Index()
        {
            try
            {
                // Product manager is a layer, responsible to handle business logic
                ProductManager productManager = new ProductManager();
                return Ok(productManager.GetAll());
            }
            catch (Exception ex)
            {
                // Content is used to send api status properly if failed
                return Content(HttpStatusCode.InternalServerError, ex.InnerException);
            }
        }

        [HttpGet]
        public IHttpActionResult GetProductById(int id)
        {
            try
            {
                ProductManager productManager = new ProductManager();
                return Ok(productManager.GetById(id));
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.InternalServerError, ex.InnerException);
            }
        }

        [HttpPost]
        public IHttpActionResult Add(ProductModel productModel)
        {
            try
            {
                ProductManager productManager = new ProductManager();
                productManager.AddNew(productModel);

                if (productManager.validationSummary.Any())
                    return Content(HttpStatusCode.NoContent, productManager.validationSummary);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.InnerException);
            }
        }

        [HttpPut]
        public IHttpActionResult Put(ProductModel productModel)
        {
            try
            {
                ProductManager productManager = new ProductManager();
                productManager.Update(productModel);

                if (productManager.validationSummary.Any())
                    return Content(HttpStatusCode.NoContent, productManager.validationSummary);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.InnerException);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                ProductManager productManager = new ProductManager();
                productManager.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.InnerException);
            }
        }
    }
}
