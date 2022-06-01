namespace Traindispetcher
{
    public class Authorization
    {
        public static int logUser { get; set; }
        public int LogCheck(string logText, string pswText)
        {
            logUser = 0;
            if ((logText == "admin") && (pswText == "123"))
            {
                logUser = 2;
            }
            return logUser;
        }
    }
}
