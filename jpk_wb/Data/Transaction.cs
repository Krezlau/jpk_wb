using System.ComponentModel.DataAnnotations;

namespace jpk_wb.Data;

public class Transaction
{
    [Key]
    public Guid Id { get; set; }
    
    public DateTime DataOperacji { get; set; }
    
    public string NazwaPodmiotu { get; set; } = string.Empty;
    
    public string OpisOperacji { get; set; } = string.Empty;
    
    public decimal KwotaOperacji { get; set; }
    
    public decimal SaldoOperacji { get; set; }
    
    public string SymbolWaluty { get; set; } = string.Empty;
}