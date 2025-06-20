namespace AT_TourManager.Data.Models
{
    public delegate void LogDelegate(string message);
    public class Logger
    {
        public static Action<string> Mensagem;

        public static void LogToConsole(string message)
        {
            Console.WriteLine($"Log Console: {DateTime.Now} = {message}");
        }

        public static void LogToFile(string message)
        {
            string filePath = "log.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"Log Txt: {DateTime.Now} = {message}");
            }
        }

        public static void LogToMemory(string message)
        {
            Console.WriteLine($"Log Memória: {DateTime.Now} = {message}");
        }

        public static void MultipleLoggers(bool logToConsole, bool logToFile, bool logToMemory)
        {
            Mensagem = null; 
            if (logToConsole)
            {
                Mensagem += LogToConsole;
            }
            if (logToFile)
            {
                Mensagem += LogToFile;
            }
            if (logToMemory)
            {
                Mensagem += LogToMemory;
            }
        }

    }
}
