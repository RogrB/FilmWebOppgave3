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
    public class KategoriController : Controller
    {
        private readonly FAQContext _context;

        public KategoriController(FAQContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var db = new FAQDB(_context);
            List<DBKategori> alleKategorier = db.HentAlleKategorier();

            return Json(alleKategorier);
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var db = new FAQDB(_context);
            var kategori = db.HentKategori(id);


            return Json(kategori);
        }

        [HttpPost]
        public JsonResult Post([FromBody]kategori innKategori)
        {
            if (ModelState.IsValid)
            {
                var db = new FAQDB(_context);
                bool OK = db.LagreKategori(innKategori);
                if (OK)
                {
                    return Json("OK");

                }
            }
            return Json("Kunne ikke sette inn kategorien i DB");
        }
    }
}
