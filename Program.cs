using System;

namespace ApplianceMonitor
{
    enum appliances { electricity, water };
    enum message_type {INFO, WARNING, ALERT};
    enum message {ELECTRICITYLIMIT_SOON, ELECTRICITYLIMIT_OVERDUE, WATERLIMIT_SOON, WATERLIMIT_OVERDUE};

    class Program
    {
        public static float electricUse, electricLimit, electricRate, waterUse, waterLimit, waterRate;

        static ConsoleKey keyConfirm = ConsoleKey.Enter;
        static ConsoleKey keyCancel = ConsoleKey.Escape;

        static ConsoleKeyInfo keyInput;

        static DateTime timeNow;
        static DateTime timeLast;
        static TimeSpan timeDiff;

        static message msg = new message();

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

        static void UsageMenu() 
        {
            Console.Clear();

            UpdateTime();

            Console.WriteLine("[Usage]\n");

            Console.Write("Electricity Used (kW/h): " + electricUse + "/" + electricLimit + "\n\n");
            Console.Write("Water Used (L): " + waterUse + "/" + waterLimit + "\n\n");

            Console.ReadKey();

        }

        static void SettingMenu() 
        {
            Console.WriteLine("[Setting]");

            Console.Write("1. Set Electricity Limit\n" +
                          "2. Set Water Limit\n" +
                          "3. Back to Main Menu\n\n" +
                          "Enter a number and press [ENTER]\n\n");

            keyInput = Console.ReadKey(true);

            switch (keyInput.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.Write("Set Electricity Limit: ");
                    //electricLimit = Console.ReadLine();

                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:

                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:

                    break;

                default:
                    break;

            }
        }

        static void CheckUsage() 
        {
            if (electricUse >= electricLimit)
            {
                Message(message.ELECTRICITYLIMIT_OVERDUE, message_type.ALERT);
            }
            else if ((electricLimit - electricUse) <= 1000) 
            {
                Message(message.ELECTRICITYLIMIT_SOON, message_type.WARNING);
            }

            if (waterUse >= waterLimit)
            {
                Message(message.WATERLIMIT_OVERDUE, message_type.ALERT);
            }
            else if ((waterLimit - waterUse) <= 1000)
            {
                Message(message.WATERLIMIT_SOON, message_type.WARNING);
            }
        }

        static void Message(message msg, message_type msgType) 
        {
            if (msgType == message_type.ALERT)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("ALERT:");
            }
            else if (msgType == message_type.WARNING) 
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("WARNING:");
            }

            Console.ResetColor();
            Console.Write(" ");

            switch (msg) 
            {
                case message.ELECTRICITYLIMIT_OVERDUE:
                    Console.Write("Electricity usage exceeds limit by " + (electricUse - electricLimit) + "kWh");
                    break;

                case message.ELECTRICITYLIMIT_SOON:
                    Console.Write("Electricity usage is to exceed limit in " + (electricLimit - electricUse) + "kWh");
                    break;

                case message.WATERLIMIT_OVERDUE:
                    Console.Write("Water usage exceeds limit by " + (waterUse - waterLimit) + "L");
                    break;

                case message.WATERLIMIT_SOON:
                    Console.Write("Water usage is to exceed limit in " + (waterLimit - waterUse) + "L");
                    break;
            }

            Console.Write("\n\n");
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
            
            int power = 1;

            do 
            {
                Console.Clear();

                timeNow = DateTime.Now;
                Console.WriteLine(timeNow);

                CheckUsage();

                Console.WriteLine("What would you like to monitor? (Enter a number and press Enter)");

                Console.WriteLine("1. Check Usage\n2. Water\n3. Power Off");

                keyInput = Console.ReadKey(true);

                switch (keyInput.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        
                        UsageMenu();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        
                        SettingMenu();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:

                        power = PowerOff();
                        break;

                    default:
                        break;

                }
                
            }while(power == 1);

        }
    }
}
