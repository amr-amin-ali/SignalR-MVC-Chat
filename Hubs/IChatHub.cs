namespace SignalR_Chat.Hubs;

using SignalR_Chat.Models;

public interface IChatHub
{
    //broadcast
    Task SendToAllClients(ChatResponse message);
    Task SendTo(string recepientId, ChatResponse message);
    Task SendToAllClientsExcept(string excludedUserId, ChatResponse message);
    Task SendToCaller(ChatResponse message);
    Task SendToAllClientsButCaller(ChatResponse message);
    Task SendToGroup(string groupName, ChatResponse message);
    Task JoinGroup(string groupName);
    Task LeaveGroup(string groupName);
}
