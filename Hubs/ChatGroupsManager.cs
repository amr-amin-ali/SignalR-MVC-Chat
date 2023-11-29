namespace SignalR_Chat.Hubs;

using System.Collections.Generic;

public class ChatGroupsManager : IChatGroupsManager
{
    private static readonly List<string> _groupsDatabase = [];
    public IEnumerable<string> GetChatGroups()
    {
        // TO DO
        //- Get chat groups names from existing database

        string[] groups = [];
        return groups;
    }

    public bool IsUniqueOneToOneChatGroup(string groupName)
    {
        //Temp dummy response represents database
        #region Dummy Implementation -> To be deleted
        return true;
        #endregion

        //// TO DO
        ////-- Implement database checking
        ////---------------------------------------------
        //bool existInOneToOneDatabase = false;
        //if (existInOneToOneDatabase)
        //{
        //    return false;
        //}

        //return true;
    }

    public bool IsUniqueOneToManyChatGroup(string groupName)
    {
        // TO DO
        //-- Implement database checking
        //---------------------------------------------
        bool existInOneToManyDatabase = false;
        if (existInOneToManyDatabase)
        {
            return false;
        }

        return true;
    }

    public string GenerateOneToOneGroupName(string firstMemberId, string secondMemberId)
    {
        string newOneToOneGroupName = string.Empty;
        int result = string.Compare(firstMemberId, secondMemberId, StringComparison.OrdinalIgnoreCase);
        if (result < 0)
        {
            newOneToOneGroupName = firstMemberId + secondMemberId;
        }
        else
        {
            newOneToOneGroupName = secondMemberId + firstMemberId;
        }
        // Dummy implementation instead of database -> remove after database implementation
        #region Dummy Implementation -> To be deleted
        bool isUnique = this.IsUniqueOneToOneChatGroup(newOneToOneGroupName);
        if (isUnique)
        {
            _groupsDatabase.Add(newOneToOneGroupName);
        }
        #endregion
        // //////////////////////////////////////////////////////////////////////////

        return newOneToOneGroupName;
    }
}
