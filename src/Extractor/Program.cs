using System;

namespace Extractor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var wikifolioGuid = new Guid("01DB9DB8-DC41-4FEF-8172-001F101573F9");
            wikifolioGuid = new Guid("FF48D396-7D1D-4E3C-A9D2-0414173BC23F");
            wikifolioGuid = new Guid("2F5842FC-977C-4E62-9028-2D3A8D00F9C1");
            var userGuid = new Guid("B6AB34AA-9587-4F1E-A54C-2DCF23764FCE");
            new WikifolioExtractor(wikifolioGuid, userGuid, true, true).WriteInserts(Console.Out);
        }
    }
}
