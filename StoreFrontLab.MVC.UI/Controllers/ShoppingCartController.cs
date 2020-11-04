using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab.DATA.EF;
using StoreFrontLab.MVC.UI.Models;

namespace StoreFrontLab.MVC.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            //create a local version of the shopping cart (dictionary) from the session version
            var shoppingCart = (Dictionary<string, ShoppingCartViewModel>)Session["cart"];
            //if the value is null 
            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                shoppingCart = new Dictionary<string, ShoppingCartViewModel>();
                ViewBag.Message = "There are no Cars booked";
            }
            //null the meesage 
            else
            {
                ViewBag.Message = null;
            }

            //wether is full or empty we return the dictionary bake to the view 
            return View(shoppingCart);

            //When you add the view make sure you select the List Template, the ViewModel for the Model and CLEAR OUT the            //Connection box, leave the last 2 options checked in the add view dialog and click add.

        }

        [HttpPost]
        public ActionResult UpdateCart(string VIN, int qty)
        {
            #region care for 0 
            //if they zero out the qty inthe car 
            if (qty <= 0)
            {
                RemoveFromCart(VIN);
                return RedirectToAction("Index");
            }
            #endregion


            //reteive the cart from session and add to local dictionary 
            Dictionary<string, ShoppingCartViewModel> shoppingCart = (Dictionary<string, ShoppingCartViewModel>)Session["cart"];

            // up date qty in the dictionary 
            shoppingCart[VIN].Qty = qty;
            //return dic cart to session 
            Session["cart"] = shoppingCart;
            //if logic to stop someone deleteing or removing from cart 
            if (shoppingCart.Count == 0)
            {
                ViewBag.Message = "There are no Cars booked";
            }
            //return to action to rerun code in tha action result of index 
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(string id)
        {
            //cart out of session into dic
            Dictionary<string, ShoppingCartViewModel> shoppingCart = (Dictionary<string, ShoppingCartViewModel>)Session["cart"];
            //call the remove() fo rdictionary calss
            shoppingCart.Remove(id);
            
            if (shoppingCart.Count == 0)
            {
                Session["cart"] = null;
            }
            return RedirectToAction("Index");
        }
    }
}