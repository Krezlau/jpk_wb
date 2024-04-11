﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace jpk_wb.Data;

public class BankStatement
{
    [Key]
    public Guid Id { get; set; }
    
    public string NumerRachunku { get; set; } = string.Empty;
    
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual List<Transaction> Transakcje { get; set; } = new();
    
    public Guid InformacjePodmiotuId { get; set; }
    
    public virtual CompanyInfo InformacjePodmiotu { get; set; }
}