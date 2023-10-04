using System.ComponentModel.DataAnnotations;
using SQLite;


namespace Studly.Entities.Base;

public class BaseEntity
{
    [PrimaryKey,AutoIncrement]
    public int Id { get; set; }
}