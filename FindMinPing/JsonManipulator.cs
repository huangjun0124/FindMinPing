using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FindMinPing
{
    class JsonManipulator
    {
        private string _fileName;
        private JObject _JsonObject;

        public JsonManipulator(string fileName)
        {
            _fileName = fileName;
            ReadFile();

        }

        private void ReadFile()
        {
            string text = System.IO.File.ReadAllText(_fileName);
            _JsonObject = JObject.Parse(text);
            _JsonObject["configs"] = new JArray();
        }

        public void AddConfig(SS_GUI_Config config)
        {
            JArray configs = _JsonObject["configs"] as JArray;
            configs.Add(JObject.FromObject(config));
        }

        public void WriteJsonToFile(string fileName="")
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = _fileName;
            }
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter _streamWriter = new StreamWriter(fs, Encoding.UTF8))
                {
                    _streamWriter.Write(_JsonObject.ToString());
                    _streamWriter.Flush();
                }
            }
        }
    }
}
