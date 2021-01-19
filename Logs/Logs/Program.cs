using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs
{
    public interface ILogger
    {
        void Log(string text);
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string text)
        {
            Console.WriteLine("Affichage des logs console : ");
            Console.WriteLine(text);
        }
    }

    public class FileLogger : ILogger
    {
        public string FilePath;

        public void Log(string text)
        {
            File.WriteAllText(FilePath, text);
        }
    }

    public class DBLogger : ILogger
    {
        public void Log(string text)
        {
            Console.WriteLine("Aucune base de données, veuillez réessayer avec une autre alternative");
        }
    }

    public interface ILoggerFactory
    {
        ILogger Create(int loggingOption);
    }

    public class LoggerFactory : ILoggerFactory
    {
        public ILogger Create(int loggingOption)
        {
            ILogger logger = null;

            switch (loggingOption)
            {
                case 1:
                    logger = new ConsoleLogger();
                    break;
                case 2:
                    // Répertoire à changer ou créer pour tester
                    logger = new FileLogger { FilePath = "D:\\Cours\\Design_pattern\\log.txt" };
                    break;
                default:
                    logger = new DBLogger();
                    break;
            }

            return logger;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 : Logs Console");
            Console.WriteLine("2 : Logs Fichier");
            Console.WriteLine("3 : Logs Base de données");

            int loggingOption = int.Parse(Console.ReadLine());

            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger logger = loggerFactory.Create(loggingOption);

            Console.WriteLine("Ecrivez une phrase à logger :");
            string textToLog = Console.ReadLine();

            logger.Log(textToLog);
            Console.WriteLine("Appuyez sur un touche pour valider...");
            Console.ReadKey();
        }
    }
}
