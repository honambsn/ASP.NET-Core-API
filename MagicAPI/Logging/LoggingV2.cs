using System.Diagnostics;

namespace MagicAPI.Logging
{
    public class LoggingV2 : ILogging
    {
        public void Log (string message, string type)
        {
            if (type == "error")
            {
                Debug.WriteLine("ERROR - " + message);
                Console.WriteLine("ERROR - " + message);
            }
            else
            {
                //Debug.WriteLine(message);
                //Console.WriteLine(message);

                if (type == "warning")
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("ERROR - " + message);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.WriteLine(message);
                }
            }
        }
    }
}
