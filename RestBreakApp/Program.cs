﻿using RestBreakService;
using System;
using Topshelf;

namespace RestBreakApp
{
    class Program
    {        
        const int DURATION = 20;
        static void Main(string[] args)
        {
            int duration = DURATION;
            if (args.Length > 0)
            {
                int.TryParse(args[0], out duration);
                duration *= 60000;
            }

            var exitCode = HostFactory.Run(x =>
            {
                x.Service<HeartBeat>(s =>
                {
                    s.ConstructUsing(heartbeat => new HeartBeat());
                    s.WhenStarted(heartbeat => heartbeat.Start());
                    s.WhenStopped(heartbeat => heartbeat.Stop());
                });

                x.RunAsLocalSystem();

                x.SetServiceName("RestBreakService");
                x.SetDisplayName("Rest Break Service");
                x.SetDescription("Service to take breaks from the screen");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}