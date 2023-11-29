namespace SignalR_Chat.Models;

public class ChatResponse
{
    /// <summary> To help front end to separate chat windows from each other </summary>
    /// <remarks>Used as identifier to group related messages to each other</remarks>
    public string? GroupId { get; set; }

    /// <summary> Id of the sender </summary>
    public string? FromId { get; set; }

    /// <summary> Id of the receiver </summary>
    public string? ToId { get; set; }

    public string Message { get; set; } = string.Empty;
}
