﻿using System.Diagnostics;

namespace MagicAPI.Logging
{
    public class Logging : ILogging
    {
        public void Log(string message, string type)
        {
            if (type == "error")
            {
                Debug.WriteLine("ERROR - " + message);
                Console.WriteLine("ERROR - " + message);
            }
            else
            {
                Debug.WriteLine(message);
                Console.WriteLine(message);
            }
        }
    }
}
