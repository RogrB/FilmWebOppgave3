using Grautbakken_Filmsjappe.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grautbakken_Filmsjappe
{
    // Klasse som seeder data til DB
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<FAQContext>();
            context.Database.EnsureCreated();
            if (!context.Kategorier.Any())
            {
                List<DBKategori> kategorier = hentKategorier();
                foreach (var kategori in kategorier)
                {
                    context.Kategorier.Add(kategori);
                }
                context.SaveChanges();
            }
        }

        private static List<DBKategori> hentKategorier()
        {
            List<DBKategori> kategorier = new List<DBKategori>();
            var kategori1 = new DBKategori() { navn = "Betaling", sp = hentSpørsmål1() };
            var kategori2 = new DBKategori() { navn = "Filmer", sp = hentSpørsmål2() };
            var kategori3 = new DBKategori() { navn = "Skuespillere", sp = hentSpørsmål3() };
            var kategori4 = new DBKategori() { navn = "Registrering", sp = hentSpørsmål4() };
            kategorier.Add(kategori1);
            kategorier.Add(kategori2);
            kategorier.Add(kategori3);
            kategorier.Add(kategori4);
            return kategorier;
        }

        private static List<DBSpørsmål> hentSpørsmål1()
        {
            List<DBSpørsmål> alleSpørsmål = new List<DBSpørsmål>();
            DBSpørsmål spørsmål1 = new DBSpørsmål() { sp = "Kan man betale med kredittkort?", antallStemmer = 6, poeng = 2, svar = hentSvar01() };
            DBSpørsmål spørsmål2 = new DBSpørsmål() { sp = "Hvor kan man se priser?", antallStemmer = 7, poeng = 3, svar = hentSvar02() };
            DBSpørsmål spørsmål3 = new DBSpørsmål() { sp = "Går det an å få avslag i pris?", antallStemmer = 4, poeng = 2, svar = hentSvar03() };
            DBSpørsmål spørsmål4 = new DBSpørsmål() { sp = "Hvor ofte må man betale??", antallStemmer = 4, poeng = 2, svar = hentSvar10() };
            alleSpørsmål.Add(spørsmål1);
            alleSpørsmål.Add(spørsmål2);
            alleSpørsmål.Add(spørsmål3);
            return alleSpørsmål;
        }

        private static List<DBSvar> hentSvar01()
        {
            List<DBSvar> alleSvar = new List<DBSvar>();
            DBSvar svar01 = new DBSvar() { svar = "Ja, man må oppgi kredittkortinformasjon når man registrerer seg", poeng = 3, antallStemmer = 3 };
            DBSvar svar02 = new DBSvar() { svar = "Jada, det går fint", poeng = 2, antallStemmer = 5 };
            alleSvar.Add(svar01);
            alleSvar.Add(svar02);
            return alleSvar;
        }

        private static List<DBSvar> hentSvar02()
        {
            List<DBSvar> alleSvar = new List<DBSvar>();
            DBSvar svar01 = new DBSvar() { svar = "Pris vises for hver film når man trykker på den", poeng = 4, antallStemmer = 4 };
            DBSvar svar02 = new DBSvar() { svar = "Man kan sortere filmene etter pris", poeng = 3, antallStemmer = 5 };
            DBSvar svar03 = new DBSvar() { svar = "Det er alt for dyre filmer", poeng = -3, antallStemmer = 5 };
            alleSvar.Add(svar01);
            alleSvar.Add(svar02);
            alleSvar.Add(svar03);
            return alleSvar;
        }

        private static List<DBSvar> hentSvar03()
        {
            List<DBSvar> alleSvar = new List<DBSvar>();
            DBSvar svar01 = new DBSvar() { svar = "Nei, priser er faste", poeng = -4, antallStemmer = 6 };
            DBSvar svar02 = new DBSvar() { svar = "De har kampanjer med avslag i pris innimellom", poeng = 3, antallStemmer = 5 };
            alleSvar.Add(svar01);
            alleSvar.Add(svar02);
            return alleSvar;
        }

        private static List<DBSvar> hentSvar10()
        {
            List<DBSvar> alleSvar = new List<DBSvar>();
            DBSvar svar01 = new DBSvar() { svar = "Man betaler for hver film man ser", poeng = 4, antallStemmer = 6 };
            alleSvar.Add(svar01);
            return alleSvar;
        }

        private static List<DBSpørsmål> hentSpørsmål2()
        {
            List<DBSpørsmål> alleSpørsmål = new List<DBSpørsmål>();
            DBSpørsmål spørsmål1 = new DBSpørsmål() { sp = "Hvilke filmkategorier finnes?", antallStemmer = 4, poeng = 1, svar = hentSvar04() };
            DBSpørsmål spørsmål2 = new DBSpørsmål() { sp = "Hvordan kan man se hvilken film som er best?", antallStemmer = 3, poeng = 1, svar = hentSvar05() };
            DBSpørsmål spørsmål3 = new DBSpørsmål() { sp = "Hvor mange filmer er det på denne siden?", antallStemmer = 5, poeng = 2, svar = hentSvar06() };
            alleSpørsmål.Add(spørsmål1);
            alleSpørsmål.Add(spørsmål2);
            alleSpørsmål.Add(spørsmål3);
            return alleSpørsmål;
        }

        private static List<DBSvar> hentSvar04()
        {
            List<DBSvar> alleSvar = new List<DBSvar>();
            DBSvar svar01 = new DBSvar() { svar = "Det finnes en egen kategoriside med oversikt", poeng = 4, antallStemmer = 4 };
            DBSvar svar02 = new DBSvar() { svar = "Du kan se kategoriene under hver film", poeng = 2, antallStemmer = 5 };
            alleSvar.Add(svar01);
            alleSvar.Add(svar02);
            return alleSvar;
        }

        private static List<DBSvar> hentSvar05()
        {
            List<DBSvar> alleSvar = new List<DBSvar>();
            DBSvar svar01 = new DBSvar() { svar = "Man kan stemme på film, og sortere filmer etter antall poeng", poeng = 4, antallStemmer = 6 };
            DBSvar svar02 = new DBSvar() { svar = "Sci-Fi filmene er best", poeng = 1, antallStemmer = 7 };
            alleSvar.Add(svar01);
            alleSvar.Add(svar02);
            return alleSvar;
        }

        private static List<DBSvar> hentSvar06()
        {
            List<DBSvar> alleSvar = new List<DBSvar>();
            DBSvar svar01 = new DBSvar() { svar = "Mange", poeng = 0, antallStemmer = 6 };
            DBSvar svar02 = new DBSvar() { svar = "Det blir lagt til nye filmer hele tiden", poeng = 5, antallStemmer = 7 };
            DBSvar svar03 = new DBSvar() { svar = "Ikke nok..", poeng = 2, antallStemmer = 6 };
            alleSvar.Add(svar01);
            alleSvar.Add(svar02);
            alleSvar.Add(svar03);
            return alleSvar;
        }

        private static List<DBSpørsmål> hentSpørsmål3()
        {
            List<DBSpørsmål> alleSpørsmål = new List<DBSpørsmål>();
            DBSpørsmål spørsmål1 = new DBSpørsmål() { sp = "Hvordan kan man se hvilke filmer en skuespiller har vært med i?", antallStemmer = 4, poeng = 2, svar = hentSvar07() };
            DBSpørsmål spørsmål2 = new DBSpørsmål() { sp = "Hvor kan man se alderen til en skuespiller?", antallStemmer = 3, poeng = 1, svar = hentSvar08() };
            alleSpørsmål.Add(spørsmål1);
            alleSpørsmål.Add(spørsmål2);
            return alleSpørsmål;
        }

        private static List<DBSvar> hentSvar07()
        {
            List<DBSvar> alleSvar = new List<DBSvar>();
            DBSvar svar01 = new DBSvar() { svar = "Det vises en oversikt under hver enkelte skuespiller når du går inn på siden dems", poeng = 4, antallStemmer = 6 };
            DBSvar svar02 = new DBSvar() { svar = "Trykk på en skuespiller", poeng = 2, antallStemmer = 7 };
            alleSvar.Add(svar01);
            alleSvar.Add(svar02);
            return alleSvar;
        }

        private static List<DBSvar> hentSvar08()
        {
            List<DBSvar> alleSvar = new List<DBSvar>();
            DBSvar svar01 = new DBSvar() { svar = "Skuespillerinformasjon vises på skuespillerens side", poeng = 4, antallStemmer = 6 };
            DBSvar svar02 = new DBSvar() { svar = "Inne på skuespillersiden", poeng = 2, antallStemmer = 7 };
            alleSvar.Add(svar01);
            alleSvar.Add(svar02);
            return alleSvar;
        }

        private static List<DBSpørsmål> hentSpørsmål4()
        {
            List<DBSpørsmål> alleSpørsmål = new List<DBSpørsmål>();
            DBSpørsmål spørsmål1 = new DBSpørsmål() { sp = "Hvordan registrerer man seg?", antallStemmer = 4, poeng = 2, svar = hentSvar09() };
            DBSpørsmål spørsmål2 = new DBSpørsmål() { sp = "Hvorfor må jeg oppgi kredittkortinformasjon?", antallStemmer = 3, poeng = 1, svar = hentSvar11() };
            alleSpørsmål.Add(spørsmål1);
            alleSpørsmål.Add(spørsmål2);
            return alleSpørsmål;
        }

        private static List<DBSvar> hentSvar09()
        {
            List<DBSvar> alleSvar = new List<DBSvar>();
            DBSvar svar01 = new DBSvar() { svar = "Man trykker på registrer linken og fyller ut informasjonen som trengs", poeng = 4, antallStemmer = 6 };
            DBSvar svar02 = new DBSvar() { svar = "På registreringssiden..", poeng = -2, antallStemmer = 4 };
            alleSvar.Add(svar01);
            alleSvar.Add(svar02);
            return alleSvar;
        }

        private static List<DBSvar> hentSvar11()
        {
            List<DBSvar> alleSvar = new List<DBSvar>();
            DBSvar svar01 = new DBSvar() { svar = "Man oppgir kredittkort ved registrering som del av valideringsprosessen", poeng = 4, antallStemmer = 6 };
            DBSvar svar02 = new DBSvar() { svar = "For å kunne betale", poeng = -2, antallStemmer = 4 };
            alleSvar.Add(svar01);
            alleSvar.Add(svar02);
            return alleSvar;
        }

    }
}
