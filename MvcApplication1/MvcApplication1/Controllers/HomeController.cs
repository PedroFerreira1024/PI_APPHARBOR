using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMobileLogic;
using IMobileDataModel;
using System.IO;

namespace MvcApplication1.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly int LAST_INSERTED_NUM = 5,
            STARS_VALUE = 0,
            MAX_NUMBER_PER_PAGE = 4;
        public ILogic<PageMap,Estate,Testimonial> logic = new ServerLogic();

        //
        //GET /Index
        public ActionResult Index()
        {
            IEnumerable<Estate> lastInsertedHouses = logic.getLastInserted(LAST_INSERTED_NUM);

            return View("Index",lastInsertedHouses);
        }

        //
        //GET /Houses
        public ActionResult Houses(String page)
        {

            PageMap pageM;
            if (page == null)
                pageM = logic.getAllHouses(MAX_NUMBER_PER_PAGE);
            else
            {
                pageM = logic.getAllHouses(MAX_NUMBER_PER_PAGE);
                pageM = logic.setCurrentPage(Convert.ToInt32(page));
                return PartialView("HousesPage",pageM);
            }
            
            return View("Houses", pageM);
        }

        //
        //GET /Search
        public ActionResult Search(String Name, String Location, String PriceLow, String PriceHigh, String Capacity, String AvailFrom, String AvailTo, String page)
        {
            

            PageMap pageM = logic.searchAndGetHouses(MAX_NUMBER_PER_PAGE,Name, Location, PriceLow, PriceHigh, AvailFrom, AvailTo,Capacity);
            if(page==null)
            {
                pageM.setCurrentPage(1);
                return View("Houses", pageM);
            }
            pageM.setCurrentPage(Convert.ToInt32(page));
            return PartialView("HousesPage", pageM);
        }

        //
        //POST /Houses
        [HttpPost]
        [ActionName("AddHouse")]
        public ActionResult HousesPost(HttpPostedFileBase mainPhoto, List<HttpPostedFileBase> otherPhotoList)
        {
            Estate estate = new Estate();

            if (TryUpdateModel(estate))
            {
                logic.insertHouse(estate, mainPhoto, otherPhotoList, Server.MapPath("~/Photos/"+estate.nameShort+"/"));
                return RedirectToAction("HouseX/"+estate.nameShort);
            }
            return View("AddHouse",estate);
        }

        //
        //GET /AddHouse
        public ActionResult AddHouse()
        {
            return View();
        }

        //
        //GET /Houses/id
        public ActionResult HouseX(String id)
        {
            Estate house = logic.getHouse(id);
            return View(house);
        }

        //
        //GET /About
        public ActionResult About()
        {
            return View();
        }

    }
}
