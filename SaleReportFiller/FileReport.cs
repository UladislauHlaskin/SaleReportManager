using System;
using System.Collections.Generic;
using System.IO;

namespace SaleReportFiller
{
    class FileReport
    {
        public string FineName { get; }
        public string Path { get; }
        private ICollection<FileRecord> _records;

        public FileReport(string directoryPath, string managerName, DateTime date)
        {
            FineName = $"{managerName}_{date.ToString("dd")}{date.ToString("MM")}{date.ToString("yyyy")}.csv";
            Path = $"{directoryPath}\\{FineName}";
            _records = new List<FileRecord>();
        }

        public void AddRecord(FileRecord record)
        {
            _records.Add(record);
        }

        public void WriteResult()
        {
            using (var writer = new StreamWriter(Path, false))
            {
                foreach (var r in _records)
                {
                    writer.WriteLine(r.ToString());
                }
            }
        }

    }
}
