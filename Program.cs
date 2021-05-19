using System;
using System.Diagnostics;
using System.Security.Principal;

namespace AudioDG_Patcher
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!IsUserAdministrator())
            {
                Console.WriteLine("You must start this program as Administrator");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            } else
            {
                Process[] processes = Process.GetProcessesByName("audiodg");
                Process audiodg = processes[0];

                if (processes.Length == 0)
                {
                    Console.WriteLine("Cannot find AudioDG.exe process");
                }
                else
                {
                    for (int i = 0; i < processes.Length; ++i)
                    {
                        SetAffinity(processes[i], 1);
                    }
                }
            }

        }

        static void SetAffinity(Process process, int core)
        {
            process.ProcessorAffinity = (IntPtr)core;
        }

        static bool IsUserAdministrator()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}