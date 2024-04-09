using System.ComponentModel.DataAnnotations;

namespace jpk_wb.Data;

public class CompanyInfo
{
    [Key]
    public Guid Id { get; set; }

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