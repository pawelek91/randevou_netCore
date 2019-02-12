using System.Collections;
using System.Collections.Generic;

namespace BusinessServices.MessageService
{
    public interface IMessagesService
    {
        int SendMessage(int senderId, int receiverId, string content);
        MessageDto GetMessage(int id);
        IEnumerable<MessageDto> GetConversationBetweenUsers(int user1Id, int user2Id);
        IEnumerable<int> GetUserConversationsSpeakers(int userId);
        IEnumerable<LastMessagesFromConversationsDto> GetConversationsLastMessages(int userId);
        void MarkMessageRead(int messageId, int ownerId);
        void MarkMessageUnread(int messageId, int ownerId);
    }
}