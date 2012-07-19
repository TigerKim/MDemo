using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Abstract;


namespace WebUI.Controllers
{
    public class ProductsController : Controller
    {
        //
        // GET: /Products/
        private IProductRepository productRepository;
        public ProductsController ()
        {
            productRepository = new FakeProductRepository();
        }

        public ActionResult List()
        {
            return View(productRepository.Products.ToList());
        }

        

    }
}
