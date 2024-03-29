﻿using Grautbakken_Domene.Models;
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

        public List<DBKategori> HentAlleKategorier()
        {
            List<DBKategori> alleKategorier = _context.Kategorier.ToList();
            
            return alleKategorier;
        }

        public DBKategori HentKategori(int id)
        {
            var utKategori = _context.Kategorier.
                Include(s => s.sp).
                    ThenInclude(sv => sv.svar).
                FirstOrDefault(k => k.id == id);
            if(utKategori != null)
            {
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

        public DBSpørsmål HentEttSpørsmål(int id)
        {
            var spørsmål = _context.Spørsmål.
                Include(v => v.svar).
                FirstOrDefault(s => s.id == id);
            if(spørsmål != null)
            {
                spørsmål.svar = spørsmål.svar.OrderByDescending(f => f.poeng).ToList();
                return spørsmål;
            }

            return null;
        }

        public bool SkrivSvar(int spørsmålsID, svar innSvar)
        {
            var spørsmål = _context.Spørsmål.FirstOrDefault(s => s.id == spørsmålsID);
            if (spørsmål == null)
            {
                return false;
            }
            DBSvar nyttSvar = new DBSvar()
            {
                svar = innSvar.Svar,
                antallStemmer = 0,
                poeng = 0
            };
            if(spørsmål.svar == null)
            {
                spørsmål.svar = new List<DBSvar>();
            }
            spørsmål.svar.Add(nyttSvar);
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

        // Metode som returnerer søkeforslag basert på input fra brukeren
        public List<Søkeresultat> HentSøkeforslag(string input)
        {
            List<Søkeresultat> forslag = new List<Søkeresultat>();
            var alleSpørsmål = _context.Spørsmål.Where(s => s.sp.Contains(input));
            if (alleSpørsmål != null)
            {
                foreach(var spørsmål in alleSpørsmål)
                {
                    Søkeresultat resultat = new Søkeresultat()
                    {
                        id = spørsmål.id,
                        sp = spørsmål.sp
                    };
                    forslag.Add(resultat);
                }
                return forslag;
            }
            return null;
        }

        // Metode som henter de 5 spørsmålene som har høyest poeng
        public List<DBSpørsmål> HentTopSpørsmål()
        {
            List<DBSpørsmål> spørsmål = _context.Spørsmål.OrderByDescending(f => f.poeng).Take(5).ToList();

            return spørsmål;
        }

    }
}

