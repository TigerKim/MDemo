using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.ConCrete;
using DomainModel.Abstract;


namespace WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsRepository productsRepository;
        public ProductsController ()
        {
            productsRepository = new FakeProductsRepository();
        }

        public ActionResult List()
        {
            return View(productsRepository.Products.ToList());
        }

        

    }
}
