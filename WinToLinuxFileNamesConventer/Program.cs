using System;
using System.Collections.Generic;
using System.IO;

namespace WinToLinuxFileNamesConventer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Доброго дня!");

            Console.WriteLine("Введiть директорiю");
            string dir = Console.ReadLine();
            DirSearch(@dir);

            Console.WriteLine();
            foreach (var item in FilesFound)
            {
                if (Path.GetFileName(item).Length > 100)
                {
                    Console.WriteLine(Path.GetFileName(item));
                    Console.WriteLine(Path.GetFileName(item).Length);
                    Console.WriteLine();

                    LongFilesFound.Add(item);
                }
            }
            Console.WriteLine("Переiменовувать файли? Y/N");
            var answer = Console.ReadLine();
            if (answer == "Y" || answer == "y") 
            {
                foreach (var item in LongFilesFound)
                {
                    Rename(item);
                }

                Console.WriteLine("Файли переiменовано.");
            }
            else
            {
                Console.WriteLine("До побачення.");
            }

            Console.ReadKey();
        }

        static void DirSearch(string sDir)
        {
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    FilesFound.Add(f);
                }

                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        FilesFound.Add(f);
                    }
                    DirSearch(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        static void Rename(string path)
        {
            string oldFileName = Path.GetFileNameWithoutExtension(path);
            string newFileName = Path.GetFileNameWithoutExtension(path).Substring(0, 97);
            newFileName += random.Next(99).ToString();

            string newPath = path.Replace(oldFileName, newFileName);

            System.IO.File.Move(path, newPath);

            Console.WriteLine($"Переiменовано {oldFileName} на {newFileName}");
        }

        private static List<string> FilesFound = new List<string>();

        private static List<string> LongFilesFound = new List<string>();

        private static Random random = new Random();

    }
}
