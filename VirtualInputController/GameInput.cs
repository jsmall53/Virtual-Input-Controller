using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace VirtualInputController {
    class GameInput : IKeyInput {

        public GameInput(string description, UInt16 keyCode) {
            _input = new VirtualInputController.NativeInput.INPUT();
            _inputArray = new NativeInput.INPUT[1];

            _input.type = 1; //hardcode keyboard inputs for now
            _input.Data.keyboard.ScanCode = keyCode;
            _input.Data.keyboard.VirtualKey = 0;
            _input.Data.keyboard.Time = 0;
            _input.Data.keyboard.ExtraInfo = IntPtr.Zero;

            _description = description;
            //since the state of the key is essentially unknown on startup
            _isPressed = false;
            ReleaseKey();
            
        }

        public void HoldKey() {
            _input.Data.keyboard.Flags = PRESSKEY;
            SendInput(_input);
            _isPressed = true;
        }

        public void ReleaseKey() {
            _input.Data.keyboard.Flags = RELEASEKEY;
            SendInput(_input);
            _isPressed = false;
        }

        public void TapKey() {
            HoldKey();
            System.Threading.Thread.Sleep(17); //TODO: This is a Tekken ONLY implementation 1 frame is 16.67ms
            ReleaseKey();
        }

        public void XORInput() {
            if (IsPressed) {
                ReleaseKey();
            } else {
                HoldKey();
            }
        }


        private void SendInput(NativeInput.INPUT input) {
            _inputArray[0] = input;
            NativeInput.SendInput(1, _inputArray, Marshal.SizeOf(input));
        }

        public override string ToString() {
            return _description;
        }

        public bool IsPressed {
            get { return _isPressed; }
            set {; }
        }

        public UInt16 Value {
            get { return _input.Data.keyboard.ScanCode; }
            set {; }
        }

        public string Name {
            get { return _description; }
        }

        public NativeInput.INPUT[] _inputArray;
        private string _description;
        private NativeInput.INPUT _input;
        private const UInt32 PRESSKEY = 0x0008;
        private const UInt32 RELEASEKEY = 0x0008 | 0x0002;
        private bool _isPressed;
    }
}
