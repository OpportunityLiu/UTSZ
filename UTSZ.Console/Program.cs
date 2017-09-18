using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTSZ.Client;

namespace UTSZ.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                try
                {
                    Client.Client.DisconnentAsync().Wait();
                    System.Console.WriteLine("网络已断开");
                }
                catch (AggregateException ex)
                {
                    System.Console.Error.WriteLine(ex.InnerException.Message);
                }
            }
            else
            {
                try
                {
                    Client.Client.ConnentAsync(args[0], args[1]).Wait();
                    System.Console.WriteLine("网络已连接");
                }
                catch (AggregateException ex)
                {
                    System.Console.Error.WriteLine(ex.InnerException.Message);
                }
            }
        }
    }
}
