using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utility {
    public class CsvParser 
    {
        private readonly StreamReader input;
        private readonly char delimiter;

        public CsvParser(StreamReader input, char delimiter) {
            this.input = input;
            this.delimiter = delimiter;
        }

        public string[] ReadNextLine() {
            var line = input.ReadLine();
            return line.Split(delimiter);
        }
    }
}
