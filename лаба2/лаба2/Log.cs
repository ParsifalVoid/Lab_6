using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace лаба2
{
    internal class Log
    {
        List<String> logs = new List<String>();
        String path;

        public Log()
        {

            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            this.path = $"mylogs-{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}.log";
            var file = File.Create(docPath + "/" + path);
            file.Flush();
            file.Close();
        }

        public void clear()
        {
            this.logs.Clear();
        }

        private void store()
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, path), true))
            {
                foreach(String log in logs)
                {
                    outputFile.WriteLine(log);
                }
            }
        }

        private void log(String type, String data) 
        {
            this.logs.Add(String.Format("[{0} - {1}]=>Добавлена запись {2}", type, $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}", data));
            this.store();
        }

        public void info(String data)
        {
            this.log("info", data);
        }
        public void error(String data)
        {
            this.log("error", data);
        }

        public void warning(String data)
        {
            this.log("warning", data);
        }

        public void printLogs()
        {
            if (logs.Count == 0)
            {
                Console.WriteLine("В логи еще ничего не добавлено!");
                return;
            }

            foreach (String log in logs)
            {
                Console.WriteLine(log);
            }
        }
    }
}
