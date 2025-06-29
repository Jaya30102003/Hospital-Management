using Notifications.Model;


namespace  Notifications.Helper;
public delegate void NotificationHandler(string message);
public delegate void UserNotification(string user, string message);

public class NotificationHelper
{
    public event NotificationHandler OnNotify;
    public event UserNotification OnUser;

    public void Notify(string message) => OnNotify?.Invoke(message);
    public void User(string user, string message) => OnUser?.Invoke(user, message);

    public static void SendEmail(string message) =>
        Console.WriteLine($"[System Email]: {message}");

    public static void ConfirmationEmail(string user, string message) =>
        Console.WriteLine($"[To {user}]: {message}");
}
