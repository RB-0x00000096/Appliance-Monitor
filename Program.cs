using System;

namespace ApplianceMonitor
{
    enum appliances { electricity, water };

    class Program
    {
        public static float electricUse, electricLimit, electricRate, waterUse, waterLimit, waterRate;

        static ConsoleKey keyConfirm = ConsoleKey.Enter;
        static ConsoleKey keyCancel = ConsoleKey.Escape;

        static DateTime timeNow;
        static DateTime timeLast;
        static TimeSpan timeDiff;

        static void StartUp() 
        {
            Console.WriteLine("Starting up...\n");

            electricUse = 1864.0f;
            waterUse = 0.0f;
            electricRate = 2.4f;
            waterRate = 0.0f;
        }

        static void SetLimit(float electric, float water) 
        {
            Console.WriteLine("Setting Appliance Limit...\n");

            electricLimit = electric;
            waterLimit = water;
        }

        static void UpdateTime() 
        {
            timeLast = timeNow;
            timeNow = DateTime.Now;
            timeDiff = timeNow - timeLast;

            electricUse += electricRate * (float)timeDiff.TotalSeconds;
            waterUse += waterRate * (float)timeDiff.TotalSeconds;
        }

        static void ElectricMenu() 
        {
            Console.Clear();

            UpdateTime();

            Console.WriteLine("[Electricity]\n");

            Console.Write("Electricity Used (kW/h): " + electricUse + "/" + electricLimit + "\n\n");

            Console.ReadKey();

        }

        static void WaterMenu() 
        {
            Console.Clear();

            UpdateTime();

            Console.WriteLine("[Water]\n");

            Console.Write("Water Used (L): " + waterUse + "/" + waterLimit + "\n\n");

            Console.ReadKey();
        }

        static void Message() 
        {
            
            if (electricUse >= electricLimit)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Alert! Electricity usage exceeds limit by " + (electricUse - electricLimit) + "kWh");
                Console.ResetColor();
                Console.WriteLine();
            }
            else if ((electricLimit - electricUse) <= 1000) 
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Warning! Electricity usage is to exceed limit in " + (electricLimit - electricUse) + "kWh");
                Console.ResetColor();
                Console.WriteLine();
            }

            if (waterUse >= waterLimit)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Alert! Water usage exceeds limit by " + (waterUse - waterLimit) + "L");
                Console.ResetColor();
                Console.WriteLine();
            }
            else if ((waterLimit - waterUse) <= 1000)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Warning! Water usage is to exceed limit in " + (waterLimit - waterUse) + "L");
                Console.ResetColor();
                Console.WriteLine();
            }

            

            

            Console.WriteLine();
        }

        static int PowerOff() 
        {
            Console.Clear();
            Console.WriteLine("Powering off.");
            return 0;
        }

        appliances x = new appliances();
        static void Main(string[] args)
        {


            StartUp();
            SetLimit(2000.0f, 2000.0f);
            

            int choice;
            int power = 1;

            do 
            {
                Console.Clear();

                timeNow = DateTime.Now;
                Console.WriteLine(timeNow);

                Message();

                choice = 0; 

                Console.WriteLine("What would you like to monitor? (Enter a number and press Enter)");

                Console.WriteLine("1. Electricity\n2. Water\n3. Power Off");

                    //choice = Console.ReadLine();

                switch (Console.ReadLine())
                {
                    case "1":

                        ElectricMenu();
                        break;

                    case "2":

                        WaterMenu();
                        break;

                    case "3":

                        power = PowerOff();
                        break;

                    default:
                        break;

                }
                
            }while(power == 1);

        }
    }
}
