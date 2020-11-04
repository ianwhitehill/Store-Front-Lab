using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab.DATA.EF;
using StoreFrontLab.MVC.UI.Utilities;
using StoreFrontLab.MVC.UI.Models;

namespace StoreFrontLab.MVC.UI.Controllers
{
    public class CarsController : Controller
    {
        private MovieCarEntities db = new MovieCarEntities();

        #region AddToCart ()
        public ActionResult AddToCart(int qty, string VIN)
        {
            ///check of QTY before adding to cart
            if (qty <= 0)
            {
                return RedirectToAction("Details", new { id = VIN });
            }
            //Create an empty cart (local version)- dictionary collection 
            Dictionary<string, ShoppingCartViewModel> shoppingCart = null;

            //Check the session cart (global)
            //add stuff from it to the local version 
            if (Session["cart"] != null)
            {
                //session lasts 20 min of inactive or until Browser closure
                //sesion defualt for tiem can be updated in the root web.config (<system.web>)
                shoppingCart = (Dictionary<string, ShoppingCartViewModel>)Session["cart"];
            }
            //else cart is empty - create new dictionary 
            else
            {
                shoppingCart = new Dictionary<string, ShoppingCartViewModel>();
            }
            //get the product being added using the pk to retrive from bd 
            Car product = db.Cars.Where(b => b.VIN == VIN).FirstOrDefault();
            //if the product is null (drop them back at books index)
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            //if product has value - then process it 
            else
            {
                //create the shoppingCartViewModel 
                ShoppingCartViewModel item = new ShoppingCartViewModel(qty, product);

                //if the cart already has a product with that id increase the qty 
                if (shoppingCart.ContainsKey(product.VIN))
                {
                    shoppingCart[product.VIN].Qty += qty;
                }
                //else add to cart 
                else
                {
                    shoppingCart.Add(product.VIN, item);
                }
                //update the session cart (gobel) with new info
                Session["cart"] = shoppingCart;
            }
            //Send the user to shopppoing cart landing page 
            return RedirectToAction("Index", "ShoppingCart");
        }
        #endregion

        // GET: Cars
        public ActionResult Index()
        {
            var cars = db.Cars.Include(c => c.FuelType).Include(c => c.Make).Include(c => c.Movie).Include(c => c.Status);
            return View(cars.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            ViewBag.FuelTypeID = new SelectList(db.FuelTypes, "FuelTypeID", "FuelTypes");
            ViewBag.MakeID = new SelectList(db.Makes, "MakeID", "Makes");
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieTitle");
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "StatusType");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VIN,Year,MakeID,MovieID,DailyCost,FuelTypeID,StatusID,Description,IsAutomatic,Image")] Car car, HttpPostedFileBase carPic)
        {
            if (ModelState.IsValid)
            {
                #region File Upload - Using the Image Service
                //use a default img if not provideed when the record is added - noImage.png
                string imgName = "noimage.png";
                //branch to check inout type file is not null
                if (carPic != null)
                {
                    //retrieve the image from HPFB and assign to our image variable 
                    imgName = carPic.FileName;
                    //declare and assign the extension
                    string ext = imgName.Substring(imgName.LastIndexOf("."));

                    //create a list of valid extensions 
                    string[] goodExts = { ".jpeg", ".gif", ".png", ".jpg" };

                    //check the extensions agaist the list and ckeck that img is  4mg or less
                    //if good then do the following
                    if (goodExts.Contains(ext.ToLower()) && (carPic.ContentLength <= 4194304))//4mb in bytes
                    {
                        //rename the file using a GUID and concatonate with the extension.
                        imgName = Guid.NewGuid() + ext.ToLower();


                        //RESIZE IMAGE UTILITY
                        //path to save img 
                        string savePath = Server.MapPath("~/Content/carpics/");

                        //img value for the converted img 
                        Image convertedImage = Image.FromStream(carPic.InputStream);

                        //max img size 
                        int maxImageSize = 500;

                        //max thumb nail size
                        int maxThumbSize = 100;

                        //Call the imageService.ResizeImage() in Utilities Folder
                        ImageService.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);
                    }
                    else
                    {
                        //If the ext is not valid of the file size is too large - defualt bacl to default img
                        imgName = "noimage.png";
                    }
                }
                //No matter what - add imgName value to the bookImage prop of the Book Object

                car.Image = imgName;
                #endregion

                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FuelTypeID = new SelectList(db.FuelTypes, "FuelTypeID", "FuelTypes", car.FuelTypeID);
            ViewBag.MakeID = new SelectList(db.Makes, "MakeID", "Makes", car.MakeID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieTitle", car.MovieID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "StatusType", car.StatusID);
            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.FuelTypeID = new SelectList(db.FuelTypes, "FuelTypeID", "FuelTypes", car.FuelTypeID);
            ViewBag.MakeID = new SelectList(db.Makes, "MakeID", "Makes", car.MakeID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieTitle", car.MovieID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "StatusType", car.StatusID);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VIN,Year,MakeID,MovieID,DailyCost,FuelTypeID,StatusID,Description,IsAutomatic,Image")] Car car, HttpPostedFileBase carPic)
        {
            if (ModelState.IsValid)
            {
                #region File Upload - Using the Image Service
                //use a default img if not provideed when the record is added - noImage.png
                
                //branch to check inout type file is not null
                if (carPic != null)
                {
                    //retrieve the image from HPFB and assign to our image variable 
                    string imgName = carPic.FileName;
                    //declare and assign the extension
                    string ext = imgName.Substring(imgName.LastIndexOf("."));

                    //create a list of valid extensions 
                    string[] goodExts = { ".jpeg", ".gif", ".png", ".jpg" };

                    //check the extensions agaist the list and ckeck that img is  4mg or less
                    //if good then do the following
                    if (goodExts.Contains(ext.ToLower()) && (carPic.ContentLength <= 4194304))//4mb in bytes
                    {
                        //rename the file using a GUID and concatonate with the extension.
                        imgName = Guid.NewGuid() + ext.ToLower();

                        //RESIZE IMAGE UTILITY
                        //path to save img 
                        string savePath = Server.MapPath("~/Content/carpics/");

                        //img value for the converted img 
                        Image convertedImage = Image.FromStream(carPic.InputStream);

                        //max img size 
                        int maxImageSize = 500;

                        //max thumb nail size
                        int maxThumbSize = 100;

                        //Call the imageService.ResizeImage() in Utilities Folder
                        ImageService.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);
                        car.Image = imgName;
                    }

                }
                #endregion
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FuelTypeID = new SelectList(db.FuelTypes, "FuelTypeID", "FuelTypes", car.FuelTypeID);
            ViewBag.MakeID = new SelectList(db.Makes, "MakeID", "Makes", car.MakeID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieTitle", car.MovieID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "StatusType", car.StatusID);
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
