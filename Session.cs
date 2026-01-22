namespace C__project
{
    public static class Session
    {
        // Logged in user id
        public static string UserId { get; set; }

        // Optional (future use)
        public static bool IsEmployee { get; set; }
        public static string CreatedBy { get; set; }

        public static void Clear()
        {
            UserId = null;
            IsEmployee = false;
            CreatedBy = null;
        }
    }
}
