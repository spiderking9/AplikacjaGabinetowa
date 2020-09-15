using ServiceReference1;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml.Linq;

namespace pobierzOswiadczenie
{
    class Program
    {
        public static string Login { get; set; } = "Aleksander";
        public static string Podpis { get; set; } = "Marecik";
        public static string Token { get; set; } = "CK-846df571-0150-1000-84c8-289e43b5bf78";
        public static XElement pobierzOswiadczenie()
        {
            XElement xml = new XElement("Oświadczenie",
                    new XElement("Tresc",$"Logowanie Lekarza przez system zewnętrzny({Login} {Podpis})"),
                    new XElement("Data", DateTime.Now.ToString("yyyy-MM-dd")),
                    new XElement("Czas", DateTime.Now.ToString("hh:mm:ss")),
                    new XElement("Token", Token)
             );

            return xml;
        }
        public static XElement AlgorytmPodzialuZaswiadczen(DateTime dataWystawienia, DateTime niezdolnyOd, DateTime niezdolnyDo, DateTime szpitalOd, DateTime szpitalDo)
        {
            DateTime niezdolnoscWsteczneDo, niezdolnoscBiezaceOd;

            if (dataWystawienia >= szpitalOd) niezdolnoscWsteczneDo = szpitalOd.AddDays(-4);
            else niezdolnoscWsteczneDo = dataWystawienia.AddDays(-4);

            niezdolnoscBiezaceOd = niezdolnoscWsteczneDo.AddDays(1);


            XElement xml =
                new XElement("RodzajZaswiadczenia",
                    new XElement("Wsteczne",
                        new XElement("NiezdolnoscDoPracyOd", niezdolnyOd.ToString("dd.M.yyyy")),
                        new XElement("NiezdolnoDoPracyDo", niezdolnoscWsteczneDo.ToString("dd.M.yyyy")),
                        new XElement("PobytWSzpitaluOd"),
                        new XElement("PobytWSzpitaluDo")),
                    new XElement("Biezace",
                        new XElement("NiezdolnoscDoPracyOd", niezdolnoscBiezaceOd.ToString("dd.M.yyyy")),
                        new XElement("NiezdolnoDoPracyDo", niezdolnyDo.ToString("dd.M.yyyy")),
                        new XElement("PobytWSzpitaluOd", szpitalOd.ToString("dd.M.yyyy")),
                        new XElement("PobytWSzpitaluDo", szpitalDo.ToString("dd.M.yyyy"))));

            return xml;

        }
        static void Main(string[] args)
        {
            Console.WriteLine(AlgorytmPodzialuZaswiadczen(new DateTime(2020,11,01), new DateTime(2020, 10, 10), new DateTime(2020, 11, 10), new DateTime(2020, 10, 20), new DateTime(2020, 11, 05)));
            Console.WriteLine();
            Console.WriteLine(AlgorytmPodzialuZaswiadczen(new DateTime(2020,11,01), new DateTime(2020, 10, 10), new DateTime(2020, 11, 10), new DateTime(2020, 11, 02), new DateTime(2020, 11, 05)));
            Console.WriteLine();
            Console.WriteLine(pobierzOswiadczenie());

            Console.ReadKey();
        }
    }
}
