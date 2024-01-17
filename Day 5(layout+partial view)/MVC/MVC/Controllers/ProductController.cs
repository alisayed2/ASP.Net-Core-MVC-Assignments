using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult showAll()
        {
            var products = ProductBL.getProducts();
            return View("ShowData",products);
        }

        public ActionResult productDetails(int id)
        {
            return View("productDetails",ProductBL.getProductById(id));
        }
    }
}
