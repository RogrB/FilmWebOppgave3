using Grautbakken_Domene.Models;
using Grautbakken_Filmsjappe.Models;
using Microsoft.EntityFrameworkCore;
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
                    sp = new List<spørsmål>(),
                    underkategorier = new List<underkategori>()
                };
                if(kategori.sp != null)
                foreach (var spørsmål in kategori.sp)
                {
                    spørsmål nyttSpørsmål = new spørsmål()
                    {
                        id = spørsmål.id,
                        poeng = spørsmål.poeng,
                        antallStemmer = spørsmål.antallStemmer,
                        sp = spørsmål.sp
                    };
                    nyKategori.sp.Add(nyttSpørsmål);
                }
                if(kategori.underkategorier != null)
                foreach (var underkategori in kategori.underkategorier)
                {
                    underkategori nyUnderKategori = new underkategori()
                    {
                        id = underkategori.id,
                        navn = underkategori.navn,
                        sp = new List<spørsmål>()
                    };
                    if(underkategori.sp != null)
                    foreach (var sp in underkategori.sp)
                    {
                        spørsmål nyttSpørsmål = new spørsmål()
                        {
                            id = sp.id,
                            sp = sp.sp,
                            antallStemmer = sp.antallStemmer,
                            poeng = sp.poeng
                        };
                        nyUnderKategori.sp.Add(nyttSpørsmål);
                    }
                    nyKategori.underkategorier.Add(nyUnderKategori);
                }
                utKategorier.Add(nyKategori);
            }
            
            return utKategorier;
        }

        public kategori HentKategori(int id)
        {
            var funnetKategori = _context.Kategorier.
                Include(s => s.sp).
                Include(u => u.underkategorier).
                FirstOrDefault(f => f.id == id);
            if (funnetKategori != null)
            {
                var utKategori = new kategori()
                {
                    id = funnetKategori.id,
                    navn = funnetKategori.navn,
                    sp = new List<spørsmål>(),
                    underkategorier = new List<underkategori>()
                };
                if(funnetKategori.sp != null)
                foreach (var spørsmål in funnetKategori.sp)
                {
                    spørsmål nyttSpørsmål = new spørsmål()
                    {
                        id = spørsmål.id,
                        sp = spørsmål.sp,
                        antallStemmer = spørsmål.antallStemmer,
                        poeng = spørsmål.poeng
                    };
                    utKategori.sp.Add(nyttSpørsmål);
                }
                if(funnetKategori.underkategorier != null)
                foreach (var underKategori in funnetKategori.underkategorier)
                {
                    underkategori nyUnderKategori = new underkategori()
                    {
                        id = underKategori.id,
                        navn = underKategori.navn,
                        sp = new List<spørsmål>()
                    };
                    if(underKategori.sp != null)
                    foreach (var sp in underKategori.sp)
                    {
                        spørsmål nyttSpørsmål = new spørsmål()
                        {
                            id = sp.id,
                            poeng = sp.poeng,
                            antallStemmer = sp.antallStemmer,
                            sp = sp.sp
                        };
                        nyUnderKategori.sp.Add(nyttSpørsmål);
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
                sp = new List<DBSpørsmål>(),
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
            var endreKategori = _context.Kategorier.FirstOrDefault(k => k.id == innKategori.id);
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
            var kategori = _context.Kategorier.FirstOrDefault(k => k.id == kategoriID);
            if (kategori == null)
            {
                return false;
            }
            DBSpørsmål spørsmål = new DBSpørsmål()
            {
                sp = innSpørsmål.sp,
                poeng = 0,
                antallStemmer = 0,
                svar = new List<DBSvar>(),
            };
            try
            {
                if(kategori.sp == null)
                {
                    kategori.sp = new List<DBSpørsmål>();
                }
                kategori.sp.Add(spørsmål);
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
            var kategori = _context.UnderKategorier.FirstOrDefault(k => k.id == kategoriID);
            if (kategori == null)
            {
                return false;
            }
            DBSpørsmål spørsmål = new DBSpørsmål()
            {
                sp = innSpørsmål.sp,
                poeng = 0,
                antallStemmer = 0
            };
            kategori.sp.Add(spørsmål);
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
            var kategori = _context.Kategorier.FirstOrDefault(k => k.id == id);
            if((kategori != null) && (kategori.sp != null))
            foreach (var spørsmål in kategori.sp)
            {
                spørsmål nyttSpørsmål = new spørsmål()
                {
                    id = spørsmål.id,
                    poeng = spørsmål.poeng,
                    antallStemmer = spørsmål.antallStemmer,
                    sp = spørsmål.sp
                };
                alleSpørsmål.Add(nyttSpørsmål);
            }

            return alleSpørsmål;
        }

        public spørsmål HentEttSpørsmål(int id)
        {
            var funnetSpørsmål = _context.Spørsmål.FirstOrDefault(s => s.id == id);
            spørsmål utSpørsmål = new spørsmål()
            {
                id = funnetSpørsmål.id,
                sp = funnetSpørsmål.sp,
                poeng = funnetSpørsmål.poeng,
                antallStemmer = funnetSpørsmål.antallStemmer,
                svar = new List<svar>()
            };
            if(funnetSpørsmål.svar != null)
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
            var spørsmål = _context.Spørsmål.FirstOrDefault(s => s.id == id);
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
            var spørsmål = _context.Spørsmål.FirstOrDefault(s => s.id == id);
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
            var svar = _context.Svar.FirstOrDefault(s => s.id == id);
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
            var svar = _context.Svar.FirstOrDefault(s => s.id == id);
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

