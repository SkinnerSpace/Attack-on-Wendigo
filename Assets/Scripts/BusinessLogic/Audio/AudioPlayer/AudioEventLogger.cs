public static class AudioEventLogger
{
    private static int totalCount;

    public static string LogCreationAndGetName(FMODUnity.EventReference audioReference)
    {
        totalCount += 1;
        string[] words = audioReference.ToString().Split('/');
        string eventName = words[words.Length - 1] + totalCount;

        if (MustUpdateTable())
        {
            AudioEventsTable.Instance.AddEvent(eventName);
        }

        return eventName;
    }

    public static void LogDestruction(string eventName)
    {
        if (MustUpdateTable())
        {
            AudioEventsTable.Instance.RemoveEvent(eventName);
        }
    }

    private static bool MustUpdateTable()
    {
        return AudioEventsTable.Instance != null &&
               AudioEventsTable.Instance.IsActive;
    }
}