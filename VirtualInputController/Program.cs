using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace VirtualInputController {
    class Program {
        [DllImport("User32.dll")]
        public static extern IntPtr GetForegroundWindow();

        static void Main(string[] args) {
            IntPtr hWndTekken = IntPtr.Zero;
            Process[] process = new Process[1]; //there should only be 1 highlander
            process = Process.GetProcessesByName("TekkenGame-Win64-Shipping");

            try {
                hWndTekken = process[0].MainWindowHandle;
            } catch (Exception ex) {
                Console.WriteLine("Could not find Tekken Process");
            }

            //TestInputs test = new TestInputs();
            InputController _inputs = new InputController();
            uint input = 0;
            SharedMemory sm = new SharedMemory("Xbox Controller");

            while (true) {
                
                if (hWndTekken == GetForegroundWindow()) {
                    input = sm.PollMapFile();
                    _inputs.NewState = input;
                    try {
                        _inputs.Update();
                    } catch (System.ArgumentOutOfRangeException ex) {
                        Console.WriteLine(ex.ToString());
                        break;
                    }
                }
            }

            Console.WriteLine("Input mapping error has occurred, press any key to exit...");
            Console.ReadLine();

        }

    }
}
