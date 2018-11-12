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
        public virtual List<DBUnderKategori> underkategorier { get; set; }
    }

    public class DBUnderKategori
    {
        [Key]
        public int id { get; set; }
        public string navn { get; set; }
        public virtual List<DBSpørsmål> sp { get; set; }
    }

    public class Kunde
    {
        [Key]
        public int id { get; set; }
        public string fornavn { get; set; }
        public string etternavn { get; set; }
        public string adresse { get; set; }
        public string postnr { get; set; }

        public virtual Poststed poststed { get; set; }
    }

    public class Poststed
    {
        [Key]
        public string postnr { get; set; }
        public string poststed { get; set; }

        public virtual List<Kunde> kunder { get; set; }
    }

    public class FAQContext : DbContext
    {
        public FAQContext(DbContextOptions<FAQContext> options)
        : base(options) { }

        public DbSet<Kunde> Kunder { get; set; }
        public DbSet<Poststed> Poststeder { get; set; }
        public DbSet<DBSpørsmål> Spørsmål { get; set; }
        public DbSet<DBKategori> Kategorier { get; set; }
        public DbSet<DBUnderKategori> UnderKategorier { get; set; }
        public DbSet<DBSvar> Svar { get; set; }
    }


}