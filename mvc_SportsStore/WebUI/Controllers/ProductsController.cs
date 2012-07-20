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
        //private string connectionString = @"Server=.\SQLExpress;Database=SportsStore;Trusted_Connection=yes;";

        private IProductsRepository productsRepository;
        //public ProductsController()
        //{
        //    productsRepository = new FakeProductsRepository();
        //    productsRepository = new SqlProductsRepository(connectionString); // 140p
        //}

        // 144page
        public ProductsController (IProductsRepository productsRepository)
        {
            //productsRepository = new FakeProductsRepository();
            //productsRepository = new SqlProductsRepository(connectionString); // 140p
            this.productsRepository = productsRepository;
        }


        public ActionResult List()
        {
            return View(productsRepository.Products.ToList());
        }

        public ActionResult List(int page)
        {
            int PageSize = 2;

            int numProducts = productsRepository.Products.Count();
            ViewData["TotalPages"] = (int)Math.Ceiling((double)numProducts / PageSize);
            ViewData["CurrentPage"] = page;

            return View(productsRepository.Products.Skip((page - 1) * PageSize).Take(PageSize).ToList());
        }

        

    }
}
