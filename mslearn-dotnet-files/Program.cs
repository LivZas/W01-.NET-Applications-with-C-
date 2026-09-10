using Newtonsoft.Json;
using System.Globalization;
using System.Text;
var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");

var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);   

var salesFiles = FindFiles(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles);

void GenerateSalesReport(IEnumerable<string> salesFiles, double salesTotal, string salesTotalDir)
{
    
    var report = new StringBuilder();

    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($"Total Sales: {salesTotal.ToString("C2", CultureInfo.GetCultureInfo("en-US"))}");
    report.AppendLine();
    report.AppendLine("Details:");

    foreach (var file in salesFiles)

    {
        
        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        var fileName = Path.GetFileName(file);
        var total = data?.Total ?? 0;

        report.AppendLine($" {fileName}: {total.ToString("C2", CultureInfo.GetCultureInfo("en-US"))}");
    }

    File.WriteAllText(
        Path.Combine(salesTotalDir, "totals.txt"),
        report.ToString()
    );
}

GenerateSalesReport(salesFiles, salesTotal, salesTotalDir);

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        if (extension == ".json" && Path.GetFileName(file) == "sales.json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;
    
    foreach (var file in salesFiles)
    {      
        string salesJson = File.ReadAllText(file);
    
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
    
        salesTotal += data?.Total ?? 0;
    }
    
    return salesTotal;
}

record SalesData (double Total);