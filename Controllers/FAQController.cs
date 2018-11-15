using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FAQ;
using Grautbakken_Domene.Models;
using Grautbakken_Filmsjappe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grautbakken_Filmsjappe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : Controller
    {
        private readonly FAQContext _context;

        public FAQController(FAQContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var db = new FAQDB(_context);
            var utSpørsmål = db.HentEttSpørsmål(id);

            return Json(utSpørsmål);
        }

        [HttpPost("{id}")]
        public JsonResult Post(int id, [FromBody]spørsmål innSpørsmål)
        {
            if (ModelState.IsValid)
            {
                var db = new FAQDB(_context);
                bool OK = db.LeggSpørsmålIKategori(id, innSpørsmål);
                if (OK)
                {
                    return Json("OK");

                }
            }
            return Json("Kunne ikke lagre spørsmålet i DB");
        }

        [HttpGet("[action]/{streng}")]
        public JsonResult HentForslag(string streng)
        {
            var db = new FAQDB(_context);
            var resultat = db.HentSøkeforslag(streng);

            return Json(resultat);
        }

    }
}
