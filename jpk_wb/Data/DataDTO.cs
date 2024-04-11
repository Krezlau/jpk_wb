namespace jpk_wb.Data;

public class DataDTO
{
    public CompanyInfoDTO InformacjePodmiotu { get; set; } = new();
    
    public BankStatementDTO WyciagBankowy { get; set; } = new();
}

public class BankStatementDTO
{
    public string NumerRachunku { get; set; } = string.Empty;
    
    public List<TransactionDTO> Transakcje { get; set; } = new();
}

public class TransactionDTO
{
    public DateTime DataOperacji { get; set; }
    
    public string NazwaPodmiotu { get; set; } = string.Empty;
    
    public string OpisOperacji { get; set; } = string.Empty;
    
    public decimal KwotaOperacji { get; set; }
    
    public decimal SaldoOperacji { get; set; }
    
    public string SymbolWaluty { get; set; } = string.Empty;
}

public class CompanyInfoDTO
{
    # region IdentyfikatorPodmiotu
    
    public string NIP { get; set; } = string.Empty;
    
    public string PelnaNazwa { get; set; } = string.Empty;
    
    public string REGON { get; set; } = string.Empty;
    
    #endregion
    
    #region AdresPodmiotu
    
    public string KodKraju { get; set; } = string.Empty;
    
    public string Wojewodztwo { get; set; } = string.Empty;
    
    public string Powiat { get; set; } = string.Empty;
    
    public string Gmina { get; set; } = string.Empty;
    
    public string Ulica { get; set; } = string.Empty;
    
    public string NrDomu { get; set; } = string.Empty;
    
    public string NrLokalu { get; set; } = string.Empty;
    
    public string Miejscowosc { get; set; } = string.Empty;
    
    public string KodPocztowy { get; set; } = string.Empty;
    
    public string Poczta { get; set; } = string.Empty;
    
    #endregion
}