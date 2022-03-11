using Flunt.Notifications;

namespace WantApp.API.Endpoints;

public static class ProgramDetailExtensions
{
    public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications)
    {
        return notifications.GroupBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Message).ToArray());
    }
}
