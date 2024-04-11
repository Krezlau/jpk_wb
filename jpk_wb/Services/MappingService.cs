using jpk_wb.Data;

namespace jpk_wb.Services;

public static class MappingService
{
    public static CompanyInfo ToEntity(this CompanyInfoDTO data)
    {
        return new CompanyInfo()
        {
            NIP = data.NIP,
            PelnaNazwa = data.PelnaNazwa,
            REGON = data.REGON,
            KodKraju = data.KodKraju,
            Wojewodztwo = data.Wojewodztwo,
            Powiat = data.Powiat,
            Gmina = data.Gmina,
            Ulica = data.Ulica,
            NrDomu = data.NrDomu,
            NrLokalu = data.NrLokalu,
            Miejscowosc = data.Miejscowosc,
            KodPocztowy = data.KodPocztowy,
            Poczta = data.Poczta
        };
    }

    public static BankStatement ToEntity(this BankStatementDTO data, Guid companyId)
    {
        return new BankStatement()
        {
            NumerRachunku = data.NumerRachunku,
            Transakcje = data.Transakcje.Select(t => t.ToEntity()).ToList(),
            InformacjePodmiotuId = companyId
        };
    }

    public static Transaction ToEntity(this TransactionDTO data)
    {
        return new Transaction()
        {
            DataOperacji = data.DataOperacji,
            NazwaPodmiotu = data.NazwaPodmiotu,
            OpisOperacji = data.OpisOperacji,
            KwotaOperacji = data.KwotaOperacji,
            SaldoOperacji = data.SaldoOperacji,
            SymbolWaluty = data.SymbolWaluty
        };
    }
}