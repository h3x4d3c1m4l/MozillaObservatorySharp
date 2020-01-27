using System;

namespace MozillaObservatory.Cli
{
    class Program
    {
        // TODO: make a nice Cli for testing
        static void Main(string[] args)
        {
            var url = "www.mozilla.com";
            using (var moClient = new ObservatoryClient())
            {
                var test1 = moClient.Analyze(url, false, false).Result;
                var test2 = moClient.GetScanResults(test1.ScanId).Result;
                Debugger.Break();
            }
        }
    }
}
