using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIZZAORDERING.Models;

public class Coupans
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public string Description { get; set; }

    public string DiscountType { get; set; } 

    public decimal DiscountValue { get; set; }
    
    public decimal MinOrderAmount { get; set; } = 0;
    
    public decimal? MaxDiscountAmount { get; set; }

    public int? UsageLimit { get; set; }

    public int UsageCount { get; set; } = 0;

    public int PerUserLimit { get; set; } = 1;
    
    public DateTime StartsAt { get; set; }
    
    public DateTime ExpiresAt { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
}