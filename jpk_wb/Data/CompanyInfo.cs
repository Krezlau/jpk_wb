using System.ComponentModel.DataAnnotations;

namespace jpk_wb.Data;

public class CompanyInfo
{
    [Key]
    public Guid Id { get; set; }

    # region IdentyfikatorPodmiotu
    
    [MaxLength(10)]
    public string NIP { get; set; } = string.Empty;
    
    [MaxLength(250)]
    public string PelnaNazwa { get; set; } = string.Empty;
    
    [MaxLength(9)]
    public string REGON { get; set; } = string.Empty;
    
    #endregion
    
    #region AdresPodmiotu
    
    [MaxLength(2)]
    public string KodKraju { get; set; } = string.Empty;
    
    [MaxLength(250)]
    public string Wojewodztwo { get; set; } = string.Empty;
    
    [MaxLength(250)]
    public string Powiat { get; set; } = string.Empty;
    
    [MaxLength(250)]
    public string Gmina { get; set; } = string.Empty;
    
    [MaxLength(250)]
    public string Ulica { get; set; } = string.Empty;
    
    [MaxLength(250)]
    public string NrDomu { get; set; } = string.Empty;
    
    [MaxLength(250)]
    public string NrLokalu { get; set; } = string.Empty;
    
    [MaxLength(250)]
    public string Miejscowosc { get; set; } = string.Empty;
    
    [MaxLength(6)]
    public string KodPocztowy { get; set; } = string.Empty;
    
    [MaxLength(250)]
    public string Poczta { get; set; } = string.Empty;
    
    #endregion
}