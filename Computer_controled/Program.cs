using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Alnudaar2.Server.Data;
using Alnudaar2.Server.Models;
using Computer_Control.Servieces.ScreenTimeService;

namespace Computer_control
{
    public class Program
    {
        public static void Main()
        {
            ServiceBase.Run(new ScreenTimeService());
        }
    }
}
