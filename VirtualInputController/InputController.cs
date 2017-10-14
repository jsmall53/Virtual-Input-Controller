using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualInputController {
    class InputController {

        public InputController() {
            _currentStates = 0;
            _newState = 0;
            //_rawInputs = new TestInputs();
            _rawInputs = InputMap.Load("InputMap.xml");
            //hardcode state for testing;
            //_currentStates = 0x2A;
        }

        public void Update() {
            //first take the difference between the new and current states
            GameInput updateInput;
            _updateState = _currentStates ^ _newState;
            _currentStates = _newState;

            if (_updateState != 0) {
                uint mask = 0x01;
                for (uint i = 0; i < _rawInputs.Count(); i++) {
                    if ((_updateState & mask) != 0) {
                        if (_rawInputs.TryGetValue((i + 1), out updateInput)) {
                            updateInput.XORInput();
                        } else {
                            //Console.WriteLine("Input Mapping Error, unable to find input at key {0}", i+1);
                            throw new System.ArgumentOutOfRangeException("Input Mapping Error, unable to find input at key {0}", (i+1).ToString());
                        }
                    }
                    mask = mask << 1;
                }
            }
        }

        public uint NewState {
            get {return _newState; }
            set { _newState = value; }
        }
        //Bits corresponding to individual inputs.
        //Managing states:  _inputStates keeps the master list of the state of each input
        //                  _newState is the fully managed list of input states from the communicating process
        //                  -_updateState will be the container for which inputs to "XOR". A '1' in this case means 'flip the input state of this key'. It just shows the difference between current and new states
        private uint _currentStates;
        private uint _newState;
        private uint _updateState;

        //private TestInputs _rawInputs;
        private Dictionary<uint, GameInput> _rawInputs;
    }
}
