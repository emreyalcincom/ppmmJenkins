using log4net.Config;
using Newtonsoft.Json;
using PPMM.Data.Model.Entities;
using PPMM.Data.Model.Enumerations;
using PPMM.Middleware.Core.App.Dtos.WorkOrders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Middleware.Core.App
{
    class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            if (!Environment.UserInteractive)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                    {
                        new WindowsService()
                    };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                MiddlewareInstance intance = new MiddlewareInstance();
                intance.StartWorking();
                Console.ReadKey();
            }
        }
    }
}
