using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using SaleReport.Model;

namespace SaleReport.BLL.FileParser
{
    class FileParser
    {
        string _fullPath;
        string _fileName;
        SaleContext _context;

        public FileParser(string fullPath, SaleContext context)
        {
            _fullPath = fullPath;
            _fileName = System.IO.Path.GetFileName(fullPath);
            _context = context;
        }

        public File Parse()
        {
            if (!ParseFileName(_fileName))
            {
                return null;
            }

            File file = new File();
            file.Date = GetFileDate(_fileName);
            file.Manager = GetManager(GetManagerName(_fileName));
            file.Record = GetRecords(ReadLines(_fullPath));
            return file;
        }

        private bool ParseFileName(string fileName)
        {
            Regex regex = new Regex(@"^[a-zA-Z]+_[0-9]{8}.csv$", RegexOptions.IgnoreCase); //SeconName_DDMMYYYY.csv
            if (regex.IsMatch(fileName))
            {
                string noManagerName = fileName.Split('_')[1];
                string dateDigitsOnly = noManagerName.Split('.')[0];
                if(!ParseDate(dateDigitsOnly))
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ParseDate(string ddMMyyyy)
        {
            DateTime d;
            return DateTime.TryParseExact(ddMMyyyy, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
        }

        private string GetManagerName(string fineName)
        {
            return fineName.Split('_')[0];
        }

        private DateTime GetFileDate(string fileName)
        {
            string noManagerName = fileName.Split('_')[1];
            string dateDigitsOnly = noManagerName.Split('.')[0];
            return DateTime.ParseExact(dateDigitsOnly, "ddMMyyyy", CultureInfo.InvariantCulture);
        }

        private ICollection<string> ReadLines(string fullName)
        {
            ICollection<string> lines = new List<string>();
            string line;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(fullName))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }

        private ICollection<Record> GetRecords(ICollection<string> lines)
        {
            ICollection<Record> records = new List<Record>();
            foreach (var line in lines)
            {
                records.Add(LineParser.Parse(line, _context));
            }
            return records;
        }

        private Manager GetManager(string name)
        {
            Manager m;
            if (_context.ManagerExists(mn => mn.Name == name))
            {
                m = _context.GetManager(mn => mn.Name == name);
            }
            else
            {
                m = new Manager() { Name = name };
                _context.Managers.Add(m);
            }
            return m;
        }

    }
}
