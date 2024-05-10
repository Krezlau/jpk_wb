using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace jpk_wb.Data;

public class BankStatement
{
    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(26)]
    public string NumerRachunku { get; set; } = string.Empty;
    
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual List<Transaction> Transakcje { get; set; } = new();
    
    public Guid InformacjePodmiotuId { get; set; }
    
    [ForeignKey(nameof(InformacjePodmiotuId))]
    public virtual CompanyInfo? InformacjePodmiotu { get; set; }
}