using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Code_Home_Work_3_6
{
    public class MyData
    {
        public int N { get; set; }
    }

    public class Logger
    {
        private readonly string _logFileName;

        private readonly string _backupFolderName;

        private readonly int _backupAfterNLogs;

        private  int _countLog;

        private int _countBackupLog = 10;

        public event EventHandler BackupEvent;

        public Logger() 
        {
            _logFileName = "log.txt";
            _backupFolderName = "Backup";

            string jsonString = File.ReadAllText("Mydata.json");
            MyData myData = JsonSerializer.Deserialize<MyData>(jsonString);

            _backupAfterNLogs = myData.N;
        }

        public  async Task LogAsync(string message)
        {
            File.Create(_logFileName).Close();

                using var writer = new StreamWriter(_logFileName);
                await writer.WriteLineAsync($"{DateTime.Now:yyyy-MM-dd-HH-mm-ss}: {message}");
        }

        public async Task BackupAsync()
        {
            Directory.CreateDirectory(_backupFolderName);

            var backupFileName = $"{DateTime.Now.ToString("hh.hh.ss dd.MM.yyyy") + ".txt"}";
            var backupFilePath = Path.Combine(_backupFolderName, backupFileName);

                using (StreamReader reader = new StreamReader(_logFileName))
                {
                    using (StreamWriter writer = new StreamWriter(backupFilePath))
                    {
                        string line;
                        while ((line = await reader.ReadLineAsync()) != null)
                        {
                            for (int i = 0; i < _countBackupLog; i++)
                            {
                                await writer.WriteLineAsync(line);
                            }
                        }
                    }
                };

            _countBackupLog += _countBackupLog;
        }

    }
}
