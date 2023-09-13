using System.ComponentModel.DataAnnotations;

namespace Studly.Entities;

public class Comment
{
    [Key]
    public int CommentId { get; set; }
    [Required]
    public string CommentText { get; set; } = string.Empty;

    public DateTime CommentDate { get; set; }

    public int UserId { get; set; }
    public Customer? Customer { get; set; }

    public int TaskId { get; set; }
    public Challenge? Task { get; set; }
}