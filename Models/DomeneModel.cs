using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grautbakken_Domene.Models
{

    public class spørsmål
    {
        public int id { get; set; }
        [Required]
        [RegularExpression("^[a-zA-ZøæåØÆÅ\\-. ?]{1,100}$")]
        public string sp { get; set; }
        public int poeng { get; set; }
        public int antallStemmer { get; set; }
        public List<svar> svar { get; set; }
    }

    public class svar
    {
        public int id { get; set; }
        [Required]
        [RegularExpression("^[a-zA-ZøæåØÆÅ\\-. ?]{1,100}$")]
        public string Svar { get; set; }
        public int poeng { get; set; }
        public int antallStemmer { get; set; }
    }

    public class kategori
    {
        public int id { get; set; }
        [Required]
        [RegularExpression("^[a-zæøåA-ZÆØÅ. \\-]{2,30}$")]
        public string navn { get; set; }
        public List<spørsmål> sp { get; set; }
    }

    public class Søkeresultat
    {
        public int id { get; set; }
        public string sp { get; set; }
    }
}