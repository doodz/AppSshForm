namespace Doods.StdFramework.Interfaces
{
    public interface INotificationsHelper
    {
        void PublishNotification(string title, string message);
        void PublishNotification(string title, string message, string filePath);
    }
}