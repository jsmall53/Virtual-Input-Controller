using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.Threading;

namespace VirtualInputController {
    class SharedMemory {
        //since this will just be reading the data in the file, don't create it here in the end. only creating for test purposes.
        public SharedMemory(string mmfName) {
            _mapFile = MemoryMappedFile.CreateOrOpen(mmfName, 128);
            _value = 0;
        }

        public void ReadSharedMemory() {
            using (_readStream = _mapFile.CreateViewStream()) {
                BinaryReader reader = new BinaryReader(_readStream);
                //char[] testData = new char[1024];
                bool testData = false;
                while (true) {
                    try {
                        testData = reader.ReadBoolean();
                    } catch {
                        testData = false;
                    }
                    if (testData == true) {
                        Console.WriteLine("Received Data!!!");
                        break;
                    }
                }
            }
        }

        public uint PollMapFile() {
            using (_accessor = _mapFile.CreateViewAccessor(0, 128)) {
                //Mutex mutex = Mutex.OpenExisting("testMutex");
                //mutex.WaitOne();
                _value = _accessor.ReadUInt32(0);
                //mutex.ReleaseMutex();
            }

            return _value;
        }

        public MemoryMappedFile MapFile { get => _mapFile; set {; } }

        uint _value;
        MemoryMappedFile _mapFile;
        MemoryMappedViewStream _readStream;
        MemoryMappedViewAccessor _accessor;

        
    }
}
