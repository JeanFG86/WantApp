namespace WantApp.API.Endpoints;

public static class ProgramDetailExtensions
{
    public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications)
    {
        return notifications.GroupBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Message).ToArray());
    }

    public static Dictionary<string, string[]> ConvertToProblemDetails(this IEnumerable<IdentityError> error)
    {
        var dictionary = new Dictionary<string, string[]>();
        dictionary.Add("Error", error.Select(e => e.Description).ToArray());

        return dictionary;
    }
}
