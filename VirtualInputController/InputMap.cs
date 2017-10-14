using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace VirtualInputController {
    class InputMap {
        public InputMap() {

        }

        public static Dictionary<uint, GameInput> Load(string filePath) {

            uint position;
            List<GameInput> inputList = new List<GameInput>();
            Dictionary<uint, GameInput> inputDict = new Dictionary<uint, GameInput>();

            XmlDocument inputMap = new XmlDocument();

            try {
                inputMap.Load(filePath);
            } catch (Exception ex) {
                Console.WriteLine("Error opening input map file {0}", filePath);
                throw ex;
            }

            XmlNodeList nodeList = inputMap.GetElementsByTagName("Input");
            foreach (XmlNode node in nodeList) {
                GameInput input = CreateInputItem(node, out position);
                if (position != 0 && !inputDict.ContainsKey(position)) {
                    inputDict.Add(position, input);
                } else {
                    Console.WriteLine("Failed to add input {0}", input.ToString());
                }
            }
            
            return inputDict;
        }

        private static GameInput CreateInputItem(XmlNode inputNode, out uint position) {
            GameInput input = null;
            XmlNode node;
            UInt16 scanCode = 0;
            string description = "";
            string inputType;
            position = 0;

            node = inputNode.FirstChild;
            //grab the position
            while (node != null) {
                if (node.Name == "Position") {
                    position = (uint)UInt32.Parse(node.InnerText);
                } else if (node.Name == "ScanCode") {
                    scanCode = UInt16.Parse(node.InnerText);
                } else if (node.Name == "Description") {
                    description = node.InnerText;
                } else if (node.Name == "InputType") {
                    inputType = node.InnerText;
                }
                node = node.NextSibling;
            }

            input = new GameInput(description, scanCode);

            return input;
        }
    }
}
