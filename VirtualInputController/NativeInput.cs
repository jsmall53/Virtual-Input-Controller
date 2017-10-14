using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace VirtualInputController {
    class NativeInput {
        [DllImport("User32.dll")]
        public static extern UInt32 SendInput(UInt32 numInputs, INPUT[] inputs, Int32 size);

        internal struct INPUT {
            public UInt32 type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSEKEYBDHARDWAREINPUT {
            [FieldOffset(0)]
            public MOUSEINPUT mouse;

            [FieldOffset(0)]
            public KEYBDINPUT keyboard;

            [FieldOffset(0)]
            public HARDWAREINPUT hardware;
        }
#pragma warning disable CS0649
        internal struct MOUSEINPUT {
            public Int32 x;
            public Int32 y;
            public UInt32 MouseData;
            public UInt32 Flags;
            public UInt32 Time;
            public IntPtr ExtraInfo;
        }

        internal struct KEYBDINPUT {
            public UInt16 VirtualKey;
            public UInt16 ScanCode;
            public UInt32 Flags;
            public UInt32 Time;
            public IntPtr ExtraInfo;
        }

        internal struct HARDWAREINPUT {
            public UInt32 Msg;
            public UInt16 ParamL;
            public UInt16 ParamH;
        }
#pragma warning restore CS0649
    }
}
