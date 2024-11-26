using System.ComponentModel.DataAnnotations;

namespace Studly.DAL.Entities;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}