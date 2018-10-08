namespace BusinessServices.MessageService
{
    public interface IMessagesService
    {
        int SendMessage(int senderId, int receiverId, string content);
    }
}