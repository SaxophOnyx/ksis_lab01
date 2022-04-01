using System;
using System.Collections.Generic;

namespace TracertApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Ошибка: недостаточно параметров");
                return;
            }

            string ip = args[0];

            Tracert tr = new Tracert(ip);
            List<TracertFrame> res = tr.Run();

            Console.WriteLine("Трассировка маршрута к {0}: ", ip);
            Console.WriteLine();

            foreach (TracertFrame fr in res)
            {
                Console.Write("{0, 3}:", fr.HopNumber);
                foreach (int a in fr.Attempts)
                    if (a != -1)
                        Console.Write("{0, 8}", a.ToString() + " мс");
                    else
                        Console.Write("{0, 8}", "***");
                if (!fr.Success)
                    Console.WriteLine("    Превышено время ожидания ответа");
                else
                    Console.WriteLine("    {0, 22}", fr.RemoteIP.PadRight(22));
            }

            Console.WriteLine();

            if ((res[res.Count - 1].Success == true) && (res[res.Count - 1].RemoteIP.Contains(ip)))
                Console.WriteLine("Трассировка успешно завершена");
            else
                Console.WriteLine("Ошибка: трассировка не была завершена");

            Console.ReadKey();
        }
    }
}
