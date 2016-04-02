//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul Simpson
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceProcess;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Main entry point for the service component which runs in the background.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                LabinatorService service = new LabinatorService();
                service.TestStartupAndStop(args);
            }
            else
            {
                System.ServiceProcess.ServiceBase.Run(new LabinatorService());
            }
        }
    }
}