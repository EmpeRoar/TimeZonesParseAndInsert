using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace TZ.Parser
{
    class TZone
    {
        public string ZName { get; set; }
        public string ZTime { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Parser!");

            var dirPath = Assembly.GetExecutingAssembly().Location;
            dirPath = Path.GetDirectoryName(dirPath);
            var path = dirPath + @"\";
            string line;
            int counter = 0;

            List<TZone> timeZones = new List<TZone>();
            StreamReader file = new StreamReader(path + "timezones.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] words = line.Split("##");
                var time = words[1];
                time = time.Substring(1,9);

                var name = words[0];
                name = name.Substring(1,name.Length-2);
                timeZones.Add(new TZone()
                {
                     ZTime = time,
                     ZName = name
                });
                counter++;
            }

            foreach(var t in timeZones)
            {
                //Console.WriteLine($"{t.ZTime} : {t.ZName}");
                Console.WriteLine($"INSERT INTO [dbo].[CTRL_Timezones] ([Name],[Time]) VALUES('{t.ZName}','{t.ZTime}');");
            }

            file.Close();


            Console.ReadLine();
        }
    }
}
