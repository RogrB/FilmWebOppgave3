using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FAQ;
using Grautbakken_Domene.Models;
using Grautbakken_Filmsjappe.Models;

namespace Grautbakken_Filmsjappe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StemmeController : Controller
    {
        private readonly FAQContext _context;

        public StemmeController(FAQContext context)
        {
            _context = context;
        }

        [HttpGet("[action]/{id}")]
        public JsonResult SPOpp(int id)
        {
            var db = new FAQDB(_context);
            bool resultat = db.TommelOppSpørsmål(id);

            return Json(resultat);
        }

        [HttpGet("[action]/{id}")]
        public JsonResult SPNed(int id)
        {
            var db = new FAQDB(_context);
            bool resultat = db.TommelNedSpørsmål(id);

            return Json(resultat);
        }

        [HttpGet("[action]/{id}")]
        public JsonResult SvarNed(int id)
        {
            var db = new FAQDB(_context);
            bool resultat = db.TommelNedSvar(id);

            return Json(resultat);
        }

        [HttpGet("[action]/{id}")]
        public JsonResult SvarOpp(int id)
        {
            var db = new FAQDB(_context);
            bool resultat = db.TommelOppSvar(id);

            return Json(resultat);
        }
    }
}