using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Home_Work_3_6
{
    internal class Starter
    {
        private readonly Logger _logger;

        public Starter(Logger logger)
        {
            _logger = logger;
            _logger.BackupEvent += Logger_BackupEvent;
        }

        public  async Task Start() 
        {
            for (int i = 0; i < 50; i++)
            {
               await _logger.LogAsync($"Log: {i}");
            }
        }

        private async void Logger_BackupEvent(object sender, EventArgs e)
        {
            await _logger.BackupAsync();
        }
    }
}
