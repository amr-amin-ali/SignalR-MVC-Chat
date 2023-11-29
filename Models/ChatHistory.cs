namespace SignalR_Chat.Models;

using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations.Schema;

public class ChatHistory
{
    public int Id { get; set; }
    public string Message { get; set; }

    [ForeignKey(nameof(Id))]
    public string UserId { get; set; }
    public IdentityUser User { get; set; }
    public ICollection<IdentityUser> Users { get; set; }
}
