using System.ComponentModel.DataAnnotations;

namespace Domain.Common;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    
    public DateTime DateCreated { get; set; } = DateTime.Now;

    public DateTime DateModified { get; set; } = DateTime.Now;
}