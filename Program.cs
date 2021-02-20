using System;
using System.IO;
using NLog.Web;

namespace MovieLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\nlog.config";
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();

            string contents = File.ReadAllText("C:\\Users\\megun\\Desktop\\College\\.NetDatabase\\MovieLibrary\\movies.csv");
            string moviePath = Directory.GetCurrentDirectory() + "\\movies.csv";
            
        
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Add a new movie");
            Console.WriteLine("2. Read existing movies");
            Console.WriteLine("Enter anything else to quit.");
            string resp = Console.ReadLine();
            logger.Info("Option Chose: {resp}", resp);
            
            if (resp == "1")
            {
                using (FileStream movieFile = new FileStream(moviePath, FileMode.Append, FileAccess.Write))
                {
                    Console.WriteLine("Enter the movielensId number:");
                    string id = Console.ReadLine();
                    Console.WriteLine("Enter the movie title:");
                    string title = Console.ReadLine();
                    Console.WriteLine("Enter the movies genre:");
                    string genre = Console.ReadLine();

                    if (contents.Contains(id) == false || contents.Contains(title) == false)
                    {
                            
                        using (StreamWriter newMovie = new StreamWriter(movieFile))
                        {
                            newMovie.WriteLine("{0},{1},{2}", id, title, genre);
                            logger.Info("New movie added.");
                            logger.Info("Program closed.");
                                
                        }
                    }
                    else
                    {
                        Console.WriteLine("This movie already exists.");
                        logger.Info("Program closed.");
                    }
                }
            }
            
            else if (resp == "2")
            {
                using(StreamReader sw = new StreamReader(moviePath))
                {
                    while(!sw.EndOfStream)
                    {
                        string line = sw.ReadLine();
                        Console.WriteLine(line);
                    }
                    logger.Info("Program closed.");
                    sw.Close();
                }      
            }
        }
    }
}