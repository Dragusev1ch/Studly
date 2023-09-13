using System.ComponentModel.DataAnnotations;

namespace Studly.Entities;

public class TaskLabel
{
    [Key]
    public int TaskLabelId { get; set; }
    public int TaskId { get; set; }
    public Task? Task { get; set; }
    public int LabelId { get; set; }
    public Label? Label { get; set; }
}