using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWave;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("No File");
                Console.Read();
                return;
            }

            string path = args[0];

            try
            {
                WavFileUtils.TrimWavFile(path, $"{path}_converted.wav", TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(20));
                Console.WriteLine("Done");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

            }

            Console.Read();
            
        }
    }
}
