using System.Xml.Linq;
using System.Xml.Serialization;
using jpk_wb.Data;

namespace jpk_wb.Services;

public static class XmlCreatorService
{
    private static readonly XNamespace tns = "http://jpk.mf.gov.pl/wzor/2019/09/27/09271/";
    private static readonly XNamespace etd = "http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2018/08/24/eD/DefinicjeTypy/";
    private static readonly XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
    private static readonly string schemaLocation = "http://jpk.mf.gov.pl/wzor/2019/09/27/09271/schema.xsd";
    
    public static XElement ConstructXml(BankStatement data)
    {
        var jpk = new XElement(tns + "jpk",
            new XAttribute(XNamespace.Xmlns + "tns", tns),
            new XAttribute(XNamespace.Xmlns + "etd", etd),
            new XAttribute(XNamespace.Xmlns + "xsi", xsi),
            new XAttribute(xsi + "schemaLocation", schemaLocation)
            );
        
        jpk.Add(ConstructHeader(data));
        jpk.Add(ConstructPodmiot(data));
        jpk.Add(ConstructNumerRachunku(data));
        jpk.Add(ConstructSalda(data));
        jpk.Add(ConstructTransakcje(data));
        jpk.Add(ConstructWyciagKontrolny(data));

        return jpk;
    }

    private static XElement ConstructHeader(BankStatement data)
    {
        var pierwszyPrzelew = data.Transakcje.MinBy(x => x.DataOperacji)!.DataOperacji;
        var ostatniPrzelew = data.Transakcje.MaxBy(x => x.DataOperacji)!.DataOperacji;
        var dataOd = new DateTime(pierwszyPrzelew.Year, pierwszyPrzelew.Month, 1);
        var dataDo = new DateTime(ostatniPrzelew.Year, ostatniPrzelew.AddMonths(1).Month, 1).AddDays(-1);
        
        var root = ConstructRoot("Naglowek");
        root.Add(new XElement(tns + "KodFormularza",
            "JPK_WB",
            new XAttribute("wersjaSchemy", "1-0"),
            new XAttribute("kodSystemowy", "JPK_WB (3)")));
        
        root.Add(new XElement("WariantFormularza", "3"));
        root.Add(new XElement("CelZlozenia", "1"));
        root.Add(new XElement("DataWytworzeniaJPK", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")));
        root.Add(new XElement("DataOd", dataOd.ToString("yyyy-MM-dd")));
        root.Add(new XElement("DataDo", dataDo.ToString("yyyy-MM-dd")));
        root.Add(new XElement("DomyslnyKodWaluty", "PLN"));
        root.Add(new XElement("KodUrzedu", "0202"));
        
        return root;
    }
    
    private static XElement ConstructPodmiot(BankStatement data)
    {
        var root = ConstructRoot("Podmiot1");
        
        var identyfikator = ConstructRoot("IdentyfikatorPodmiotu");
        identyfikator.Add(new XElement("etd" + "NIP", data.InformacjePodmiotu!.NIP));
        identyfikator.Add(new XElement("etd" + "PelnaNazwa", data.InformacjePodmiotu!.PelnaNazwa));
        identyfikator.Add(new XElement("etd" + "REGON", data.InformacjePodmiotu!.REGON));
        
        root.Add(identyfikator);
        
        var adresPodmiotu = ConstructRoot("AdresPodmiotu");
        adresPodmiotu.Add(new XElement("etd" + "KodKraju", "PL"));
        adresPodmiotu.Add(new XElement("etd" + "Wojewodztwo", data.InformacjePodmiotu!.Wojewodztwo));
        adresPodmiotu.Add(new XElement("etd" + "Powiat", data.InformacjePodmiotu!.Powiat));
        adresPodmiotu.Add(new XElement("etd" + "Gmina", data.InformacjePodmiotu!.Gmina));
        adresPodmiotu.Add(new XElement("etd" + "Ulica", data.InformacjePodmiotu!.Ulica));
        adresPodmiotu.Add(new XElement("etd" + "NrDomu", data.InformacjePodmiotu!.NrDomu));
        adresPodmiotu.Add(new XElement("etd" + "NrLokalu", data.InformacjePodmiotu!.NrLokalu));
        adresPodmiotu.Add(new XElement("etd" + "Miejscowosc", data.InformacjePodmiotu!.Miejscowosc));
        adresPodmiotu.Add(new XElement("etd" + "KodPocztowy", data.InformacjePodmiotu!.KodPocztowy));
        
        root.Add(adresPodmiotu);

        return root;
    }
    
    private static XElement ConstructNumerRachunku(BankStatement data)
    {
        var root = ConstructRoot("NumerRachunku");
        root.Add(new XElement("etd" + "NumerRachunku", data.NumerRachunku));
        return root;
    }
    
    private static XElement ConstructSalda(BankStatement data)
    {
        var operacje = data.Transakcje.OrderBy(x => x.DataOperacji).ToList();
        var saldoPoczatkowe = operacje.First().SaldoOperacji - operacje.First().KwotaOperacji;
        var saldoKoncowe = operacje.Last().SaldoOperacji;
        
        var root = ConstructRoot("Salda");
        root.Add(new XElement(tns + "SaldoPoczatkowe", saldoPoczatkowe));
        root.Add(new XElement(tns + "SaldoKoncowe", saldoKoncowe));
        return root;
    }
    
    private static List<XElement> ConstructTransakcje(BankStatement data)
    {
        var transakcje = data.Transakcje.OrderBy(x => x.DataOperacji).ToList();

        return transakcje.Select((t, i) =>
        {
            var root = ConstructRoot("WyciagWiersz");
            
            root.Add(new XElement(tns + "NumerWiersza", i + 1));
            root.Add(new XElement(tns + "DataOperacji", t.DataOperacji.ToString("yyyy-MM-dd")));
            root.Add(new XElement(tns + "NazwaPodmiotu", t.NazwaPodmiotu));
            root.Add(new XElement(tns + "OpisOperacji", t.OpisOperacji));
            root.Add(new XElement(tns + "KwotaOperacji", t.KwotaOperacji));
            root.Add(new XElement(tns + "SaldoOperacji", t.SaldoOperacji));

            return root;
        }).ToList();
    }
    
    private static XElement ConstructWyciagKontrolny(BankStatement data)
    {
        var root = ConstructRoot("WyciagCtrl");
        
        root.Add(new XElement(tns + "LiczbaWierszy", data.Transakcje.Count));
        root.Add(new XElement(tns + "SumaObciazen", -1 * data.Transakcje.Where(x => x.KwotaOperacji < 0).Sum(x => x.KwotaOperacji)));
        root.Add(new XElement(tns + "SumaUznan", data.Transakcje.Where(x => x.KwotaOperacji > 0).Sum(x => x.KwotaOperacji)));
        
        return root;
    }
    
    private static XElement ConstructRoot(string name, XmlTypeAttribute? xmlType = null)
    {
        return new XElement(tns + name, 
            new XAttribute(XNamespace.Xmlns + "tns", tns),
            new XAttribute(XNamespace.Xmlns + "etd", etd),
            xmlType is not null ? new XAttribute(xsi + "type", xmlType.TypeName) : null
            );
    }
}