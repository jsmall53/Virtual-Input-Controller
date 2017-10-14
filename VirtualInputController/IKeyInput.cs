using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualInputController {
    interface IKeyInput {
        void TapKey();
        void HoldKey();
        void ReleaseKey();
    }
}
