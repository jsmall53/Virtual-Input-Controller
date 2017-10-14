using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualInputController {
    //raw key values
    class Keys {
        public const UInt16 LEFT    = 203;
        public const UInt16 RIGHT   = 0xCD;
        public const UInt16 UP      = 0xC8;
        public const UInt16 DOWN    = 0xD0;
        public const UInt16 X       = 0x47;
        public const UInt16 Y       = 0x48;
        public const UInt16 A       = 0x4B;
        public const UInt16 B       = 0x4C;
        public const UInt16 START   = 199;
        public const UInt16 SELECT  = 210;
        public const UInt16 LB      = 73;
        public const UInt16 RB      = 77;
        public const UInt16 LT      = 74;
        public const UInt16 RT      = 78;
    }

    class InputActions {

        public InputActions() {
            _input = new VirtualInputController.NativeInput.INPUT();

            _input.type = 1; //hardcode keyboard inputs for now
            _input.Data.keyboard.VirtualKey = 0;
            _input.Data.keyboard.Time = 0;
            _input.Data.keyboard.ExtraInfo = IntPtr.Zero;
        }

        public void TapKey(UInt16 keyValue) {

        }

        private NativeInput.INPUT _input;
    }
}
