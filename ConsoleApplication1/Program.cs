using SIS.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            APD detector = new SIS.Hardware.APD("Dev1", "Ctr0", 100, "Ctr2InternalOutput", "Ctr1", "PFI15", false);
            //Ctr2
            detector.SetupAPDCountAndTiming(100, 100000);
            detector.StartAPDAcquisition();

            uint[] data;

            while (true)
            {
                data = detector.Read();
                foreach (uint test in data)
                {
                    Console.WriteLine(test.ToString());
                    Thread.Sleep(50);
                }
            }

        }
    }
}
