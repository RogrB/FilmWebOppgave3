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
    public class SvarController : Controller
    {
        private readonly FAQContext _context;

        public SvarController(FAQContext context)
        {
            _context = context;
        }

        [HttpPost("{id}")]
        public JsonResult Post(int id, [FromBody]svar innSvar)
        {
            if (ModelState.IsValid)
            {
                var db = new FAQDB(_context);
                bool OK = db.SkrivSvar(id, innSvar);
                if (OK)
                {
                    return Json("OK");

                }
            }
            return Json("Kunne ikke lagre spørsmålet i DB");
        }

    }
}