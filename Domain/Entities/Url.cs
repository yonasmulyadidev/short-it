using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

[Table("Url")]
public sealed class Url : BaseEntity
{
    public required string ShortUrl { get; set; }

    public required string LongUrl { get; set; }
}