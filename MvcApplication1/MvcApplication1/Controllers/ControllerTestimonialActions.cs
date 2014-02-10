using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMobileLogic;
using IMobileDataModel;

namespace MvcApplication1.Controllers
{
    public partial class HomeController
    {

        public ActionResult Testimonial(String id)
        {
            Testimonial testimonial = logic.getLastTestimonial(logic.getHouse(id));

            return PartialView("NewTestimonial", testimonial);
        }

        [HttpPost]
        [ActionName("Testimonial")]
        public ActionResult addTestimonial(String id, String stars, String description)
        {
            Testimonial test = new Testimonial();
            if (description != null)
            {
                test.setTestimonial(charValueToDecimal(stars[STARS_VALUE]), description, null);
                Estate house = logic.getHouse(id);
                logic.addTestimonial(house, test);
                return RedirectToAction("Testimonial/" + id);
            }
            return HouseX(id);
        }

        private int charValueToDecimal(int value)
        {
            return value - 48;
        }

    }
}