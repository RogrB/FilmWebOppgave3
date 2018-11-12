using Grautbakken_Domene.Models;
using Grautbakken_Filmsjappe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAQ
{
    public class FAQDB
    {
        private readonly FAQContext _context;
        public FAQDB(FAQContext context)
        {
            _context = context;
        }

        public List<kategori> HentAlleKategorier()
        {
            List<DBKategori> alleKategorier = _context.Kategorier.ToList();
            List<kategori> utKategorier = new List<kategori>();
            foreach(DBKategori kategori in alleKategorier)
            {
                kategori nyKategori = new kategori()
                {
                    id = kategori.id,
                    navn = kategori.navn,
                    spørsmål = new List<spørsmål>(),
                    underkategorier = new List<underkategori>()
                };
                if(kategori.spørsmål != null)
                foreach (var spørsmål in kategori.spørsmål)
                {
                    spørsmål nyttSpørsmål = new spørsmål()
                    {
                        id = spørsmål.id,
                        poeng = spørsmål.poeng,
                        antallStemmer = spørsmål.antallStemmer,
                        Spørsmål = spørsmål.spørsmål
                    };
                    nyKategori.spørsmål.Add(nyttSpørsmål);
                }
                if(kategori.underkategorier != null)
                foreach (var underkategori in kategori.underkategorier)
                {
                    underkategori nyUnderKategori = new underkategori()
                    {
                        id = underkategori.id,
                        navn = underkategori.navn,
                        spørsmål = new List<spørsmål>()
                    };
                    if(underkategori.spørsmål != null)
                    foreach (var sp in underkategori.spørsmål)
                    {
                        spørsmål nyttSpørsmål = new spørsmål()
                        {
                            id = sp.id,
                            Spørsmål = sp.spørsmål,
                            antallStemmer = sp.antallStemmer,
                            poeng = sp.poeng
                        };
                        nyUnderKategori.spørsmål.Add(nyttSpørsmål);
                    }
                    nyKategori.underkategorier.Add(nyUnderKategori);
                }
                utKategorier.Add(nyKategori);
            }
            
            return utKategorier;
        }

        public kategori HentKategori(int id)
        {
            var funnetKategori = _context.Kategorier.FirstOrDefault(f => f.id == id);
            if (funnetKategori != null)
            {


                var utKategori = new kategori()
                {
                    id = funnetKategori.id,
                    navn = funnetKategori.navn,
                    spørsmål = new List<spørsmål>(),
                    underkategorier = new List<underkategori>()
                };
                foreach (var spørsmål in funnetKategori.spørsmål)
                {
                    spørsmål nyttSpørsmål = new spørsmål()
                    {
                        id = spørsmål.id,
                        Spørsmål = spørsmål.spørsmål,
                        antallStemmer = spørsmål.antallStemmer,
                        poeng = spørsmål.poeng
                    };
                    utKategori.spørsmål.Add(nyttSpørsmål);
                }
                foreach (var underKategori in funnetKategori.underkategorier)
                {
                    underkategori nyUnderKategori = new underkategori()
                    {
                        id = underKategori.id,
                        navn = underKategori.navn,
                        spørsmål = new List<spørsmål>()
                    };
                    foreach (var sp in underKategori.spørsmål)
                    {
                        spørsmål nyttSpørsmål = new spørsmål()
                        {
                            id = sp.id,
                            poeng = sp.poeng,
                            antallStemmer = sp.antallStemmer,
                            Spørsmål = sp.spørsmål
                        };
                        nyUnderKategori.spørsmål.Add(nyttSpørsmål);
                    }

                    utKategori.underkategorier.Add(nyUnderKategori);
                }

                return utKategori;
            }
            return null;
        }

        public bool LagreKategori(kategori innKategori)
        {
            var nyKategori = new DBKategori
            {
                navn = innKategori.navn,
                spørsmål = new List<DBSpørsmål>(),
                underkategorier = new List<DBUnderKategori>()
            };
            try
            {
                _context.Kategorier.Add(nyKategori);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool EndreKategori(kategori innKategori)
        {
            var endreKategori = _context.Kategorier.Find(innKategori.id);
            if (endreKategori == null)
            {
                return false;
            }
            endreKategori.navn = innKategori.navn;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool LeggSpørsmålIKategori(int kategoriID, spørsmål innSpørsmål)
        {
            var kategori = _context.Kategorier.Find(kategoriID);
            if (kategori == null)
            {
                return false;
            }
            DBSpørsmål spørsmål = new DBSpørsmål()
            {
                spørsmål = innSpørsmål.Spørsmål,
                poeng = 0,
                antallStemmer = 0
            };
            kategori.spørsmål.Add(spørsmål);
            try
            {
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

        public bool LeggSpørsmålIUnderKategori(int kategoriID, spørsmål innSpørsmål)
        {
            var kategori = _context.UnderKategorier.Find(kategoriID);
            if (kategori == null)
            {
                return false;
            }
            DBSpørsmål spørsmål = new DBSpørsmål()
            {
                spørsmål = innSpørsmål.Spørsmål,
                poeng = 0,
                antallStemmer = 0
            };
            kategori.spørsmål.Add(spørsmål);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public List<spørsmål> HentSpørsmålFraKategori(int id)
        {
            List<spørsmål> alleSpørsmål = new List<spørsmål>();
            var kategori = _context.Kategorier.Find(id);
            foreach (var spørsmål in kategori.spørsmål)
            {
                spørsmål nyttSpørsmål = new spørsmål()
                {
                    id = spørsmål.id,
                    poeng = spørsmål.poeng,
                    antallStemmer = spørsmål.antallStemmer,
                    Spørsmål = spørsmål.spørsmål
                };
                alleSpørsmål.Add(nyttSpørsmål);
            }

            return alleSpørsmål;
        }

        public spørsmål HentEttSpørsmål(int id)
        {
            var funnetSpørsmål = _context.Spørsmål.Find(id);
            spørsmål utSpørsmål = new spørsmål()
            {
                id = funnetSpørsmål.id,
                Spørsmål = funnetSpørsmål.spørsmål,
                poeng = funnetSpørsmål.poeng,
                antallStemmer = funnetSpørsmål.antallStemmer,
                svar = new List<svar>()
            };
            foreach (var svar in funnetSpørsmål.svar)
            {
                svar nyttSvar = new svar()
                {
                    id = svar.id,
                    Svar = svar.svar,
                    poeng = svar.poeng,
                    antallStemmer = svar.antallStemmer
                };
                utSpørsmål.svar.Add(nyttSvar);
            }

            return utSpørsmål;
        }

        public bool TommelOppSpørsmål(int id)
        {
            var spørsmål = _context.Spørsmål.Find(id);
            if (spørsmål == null)
            {
                return false;
            }
            spørsmål.poeng++;
            spørsmål.antallStemmer++;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool TommelNedSpørsmål(int id)
        {
            var spørsmål = _context.Spørsmål.Find(id);
            if (spørsmål == null)
            {
                return false;
            }
            spørsmål.poeng--;
            spørsmål.antallStemmer++;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool TommelOppSvar(int id)
        {
            var svar = _context.Svar.Find(id);
            if (svar == null)
            {
                return false;
            }
            svar.poeng++;
            svar.antallStemmer++;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool TommelNedSvar(int id)
        {
            var svar = _context.Svar.Find(id);
            if (svar == null)
            {
                return false;
            }
            svar.poeng--;
            svar.antallStemmer++;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

    }
}

