using Newtonsoft.Json;
using RapidDoc.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RapidDoc.Controllers
{
    public class CustomController : BasicController
    {
        private readonly IEmplService _EmplService;
        private readonly ISystemService _SystemService;

        public CustomController(IEmplService emplService, ISystemService systemService)
        {
            _EmplService = emplService;
            _SystemService = systemService;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult JsonEmpl()
        {
            var jsondata = _EmplService.GetJsonEmpl();
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult JsonBase1C77()
        {
            List<string> jsondata = new List<string>();
            jsondata.Add("Altynavto_2004-2005");
            jsondata.Add("Altynavto_old");
            jsondata.Add("ATK2010");
            jsondata.Add("Goldservice2002-2003");
            jsondata.Add("Goldservice2004-2005");
            jsondata.Add("vg_2005_db");
            jsondata.Add("vg_2006");
            jsondata.Add("vg_2008");
            jsondata.Add("vgok_2005");
            jsondata.Add("vgok_2009");
            jsondata.Add("vgok_zp_2005");
            jsondata.Add("Zarp2005");
            jsondata.Add("СП");
            jsondata.Add("СП_2005");
            jsondata.Add("СП_2004");
            jsondata.Add("СП_2003");
            jsondata.Add("NarTab_2005");
            jsondata.Add("NarTab_2006");
            jsondata.Add("NarTab_2007");
            jsondata.Add("NarTab_2009");
            jsondata.Add("NarTab_2010");
            jsondata.Add("NarTab_2011");
            jsondata.Add("NarTab_АМУ");
            jsondata.Add("Base");
            jsondata.Add("Base_2003");
            jsondata.Add("Base_2005");

            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
	}
}