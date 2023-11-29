namespace SignalR_Chat.Hubs;

public interface IChatGroupsManager
{
    IEnumerable<string> GetChatGroups();
    string GenerateOneToOneGroupName(string firstMemberId, string secondMemberId);
    bool IsUniqueOneToOneChatGroup(string groupName);
    bool IsUniqueOneToManyChatGroup(string groupName);
}
