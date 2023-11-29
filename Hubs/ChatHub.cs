namespace SignalR_Chat.Hubs;

using Microsoft.AspNetCore.SignalR;

using SignalR_Chat.Models;

using System.Collections.Concurrent;
using System.Security.Claims;

public class ChatHub : Hub, IChatHub
{
    /// <summary>
    /// Dictionary to hold connected users ConnectionIds.
    /// </summary>
    /// <remarks>
    /// ConnectionIds are needed to add user to group and also to keep track of opened tabs for the same user.
    /// </remarks>
    public static ConcurrentDictionary<string, List<string>> ConnectedUsers = new ConcurrentDictionary<string, List<string>>();

    /// <summary> Sends message to specific user by user id. </summary>
    /// <param name="message">The message that will be sent to the recipient.</param>
    /// <param name="recipientUserId">ID of the user that the should receive the message.</param>
    /// <returns>Task</returns>
    public async Task SendTo(string recipientUserId, ChatResponse message)
    {
        string senderUserId = Context.UserIdentifier!;
        var groupManager = new ChatGroupsManager();
        string groupName = groupManager.GenerateOneToOneGroupName(recipientUserId, senderUserId);

        //Get sender connection ids
        List<string> senderUserConnectionIds;
        ChatHub.ConnectedUsers.TryGetValue(senderUserId!, out senderUserConnectionIds!);

        //Add sender connection ids to tge group
        foreach (var connectionId in senderUserConnectionIds)
        {
            await Groups.AddToGroupAsync(connectionId, groupName);
        }

        //Get recipient connection ids
        List<string> recipientUserConnectionIds;
        ChatHub.ConnectedUsers.TryGetValue(recipientUserId!, out recipientUserConnectionIds!);

        //Add recipient connection ids to tge group
        foreach (var connectionId in recipientUserConnectionIds)
        {
            await Groups.AddToGroupAsync(connectionId, groupName);
        }

        await this.SendToGroup(groupName, message);
    }

    /// <summary> Sends message to all connected clients  </summary>
    /// <param name="message"></param>
    /// <returns>Task</returns>
    public async Task SendToAllClients(ChatResponse message)
    {
        await Clients.All.SendAsync("ClientReceiver", message);
    }

    /// <summary> Sends message to the caller client. </summary>
    /// <param name="message">The message that will be sent to the caller user.</param>
    /// <returns>Task</returns>
    public async Task SendToCaller(ChatResponse message)
    {
        await Clients.Caller.SendAsync("ClientReceiver", message);
    }

    /// <summary> Sends message to all connected clients except the client who sent it. </summary>
    /// <param name="message">The message that will be sent to all users except who sent it.</param>
    /// <returns>Task</returns>
    public async Task SendToAllClientsButCaller(ChatResponse message)
    {
        await Clients.Others.SendAsync("ClientReceiver", message);
    }

    /// <summary> Sends message to all users except specific user. </summary>
    /// <param name="message">The message that will be sent to the all other participants.</param>
    /// <param name="excludedUserId">ID of the user that the should not receive the message.</param>
    /// <returns>Task</returns>
    public async Task SendToAllClientsExcept(string excludedUserId, ChatResponse message)
    {
        await Clients.AllExcept(excludedUserId).SendAsync("ClientReceiver", message);
    }

    /// <summary> Sends message to specific group of users. </summary>
    /// <param name="message">The message that will be sent to the group</param>
    /// <param name="groupName">Name of the group that will receive the message.</param>
    /// <returns>Task</returns>
    public async Task SendToGroup(string groupName, ChatResponse message)
    {
        await Clients.Group(groupName).SendAsync("ClientReceiver", message);
    }

    /// <summary> Add caller to specific group. </summary>
    /// <param name="groupName">Name of the group that the user should join.</param>
    /// <returns>Task</returns>
    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    /// <summary> Remove caller to specific group. </summary>
    /// <param name="groupName">Name of the group that the user should leave.</param>
    /// <returns>Task</returns>
    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }

    public override Task OnConnectedAsync()
    {
        // Get the user ID from the ClaimsPrincipal
        var userId = Context.User!.FindFirstValue(ClaimTypes.NameIdentifier);

        // Try to get a List of existing user connections from the cache
        List<string> existingUserConnectionIds;

        ConnectedUsers.TryGetValue(userId!, out existingUserConnectionIds!);

        // happens on the very first connection from the user
        existingUserConnectionIds ??= new List<string>();

        // First add to a List of existing user connections (i.e. multiple web browser tabs)
        existingUserConnectionIds.Add(Context.ConnectionId);

        // Add to the global dictionary of connected users
        ConnectedUsers.TryAdd(userId!, existingUserConnectionIds);

        return base.OnConnectedAsync();
    }
}