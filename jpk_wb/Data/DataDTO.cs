using System.ComponentModel.DataAnnotations;

namespace jpk_wb.Data;

public class DataDTO
{
    [Required]
    public CompanyInfoDTO InformacjePodmiotu { get; set; } = new();
    
    [Required]
    public BankStatementDTO WyciagBankowy { get; set; } = new();
}

public class BankStatementDTO
{
    [Required]
    [RegularExpression("[0-9]{26}")]
    public string NumerRachunku { get; set; } = string.Empty;
    
    [Required]
    [MinLength(1)]
    public List<TransactionDTO> Transakcje { get; set; } = new();
}

public class TransactionDTO
{
    [Required]
    public DateTime DataOperacji { get; set; }
    
    [Required]
    [MinLength(1)]
    public string NazwaPodmiotu { get; set; } = string.Empty;
    
    [Required]
    [MinLength(1)]
    public string OpisOperacji { get; set; } = string.Empty;
    
    [Required]
    public decimal KwotaOperacji { get; set; }
    
    [Required]
    public decimal SaldoOperacji { get; set; }
    
    public string SymbolWaluty { get; set; } = string.Empty;
}

public class CompanyInfoDTO
{
    # region IdentyfikatorPodmiotu
    
    [Required]
    [RegularExpression("[0-9]{10}")]
    public string NIP { get; set; } = string.Empty;
    
    [Required]
    [MinLength(1)]
    public string PelnaNazwa { get; set; } = string.Empty;
    
    [Required]
    [RegularExpression("[0-9]{9}")]
    public string REGON { get; set; } = string.Empty;
    
    #endregion
    
    #region AdresPodmiotu
    
    [Required]
    [RegularExpression("[A-Z]{2}")]
    public string KodKraju { get; set; } = string.Empty;
    
    [Required]
    [MinLength(1)]
    public string Wojewodztwo { get; set; } = string.Empty;
    
    [Required]
    [MinLength(1)]
    public string Powiat { get; set; } = string.Empty;
    
    [Required]
    [MinLength(1)]
    public string Gmina { get; set; } = string.Empty;
    
    [Required]
    [MinLength(1)]
    public string Ulica { get; set; } = string.Empty;
    
    [Required]
    [MinLength(1)]
    public string NrDomu { get; set; } = string.Empty;
    
    public string NrLokalu { get; set; } = string.Empty;
    
    [Required]
    [MinLength(1)]
    public string Miejscowosc { get; set; } = string.Empty;
    
    [Required]
    [RegularExpression("[0-9]{2}-[0-9]{3}")]
    public string KodPocztowy { get; set; } = string.Empty;
    
    [Required]
    [MinLength(1)]
    public string Poczta { get; set; } = string.Empty;
    
    #endregion
}