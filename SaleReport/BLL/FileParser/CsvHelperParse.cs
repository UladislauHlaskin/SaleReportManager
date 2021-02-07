using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace SaleReport.BLL.FileParser
{
    public class CsvHelperParse
    {
        public static IEnumerable<CsvFileRecord> LibParse(string path)
        {
            IEnumerable<CsvFileRecord> records;
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {
                records = csv.GetRecords<CsvFileRecord>();
            }
            return records;
        }
    }
}
