using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Grautbakken_Filmsjappe.Models
{
    public class DBSpørsmål
    {
        [Key]
        public int id { get; set; }
        public string sp { get; set; }
        public int poeng { get; set; }
        public int antallStemmer { get; set; }
        public virtual List<DBSvar> svar { get; set; }
    }

    public class DBSvar
    {
        public int id { get; set; }
        public string svar { get; set; }
        public int poeng { get; set; }
        public int antallStemmer { get; set; }
    }

    public class DBKategori
    {
        [Key]
        public int id { get; set; }
        public string navn { get; set; }
        public virtual List<DBSpørsmål> sp { get; set; }
    }

    public class FAQContext : DbContext
    {
        public FAQContext(DbContextOptions<FAQContext> options)
        : base(options) { }


        public DbSet<DBSpørsmål> Spørsmål { get; set; }
        public DbSet<DBKategori> Kategorier { get; set; }
        public DbSet<DBSvar> Svar { get; set; }
    }


}