using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using IMobileDataModel;

namespace Utils
{
    public class ConverterUtils
    {
        public static MvcHtmlString ConvertToJson(PageMap p)
        {
            string s = JsonConvert.SerializeObject(p);
            MvcHtmlString x = MvcHtmlString.Create(s);
            return x;
        }

        public static MvcHtmlString ConvertToJson(Estate e)
        {
            string s = JsonConvert.SerializeObject(e);
            MvcHtmlString x = MvcHtmlString.Create(s);
            return x;
        }
    }
}