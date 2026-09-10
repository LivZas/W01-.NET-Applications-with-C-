# Assignment Week 01:

## 1. Evidence:

### GET:

```

HTTP/1.1 200 OK
Connection: close
Content-Type: application/json; charset=utf-8
Date: Tue, 08 Sep 2026 21:32:25 GMT
Server: Kestrel
Transfer-Encoding: chunked

[
  {
    "id": 1,
    "name": "Classic Italian",
    "isGlutenFree": false
  },
  {
    "id": 2,
    "name": "Veggie",
    "isGlutenFree": true
  },
  {
    "id": 3,
    "name": "Pepperoni",
    "isGlutenFree": false
  }
]

```


## *Note:* I added the pepperoni pizza as the additional record. It's in PizzaService.cs.

```

new Pizza { Id = 3, Name = "Pepperoni", IsGlutenFree = false}

```

### POST:

```

HTTP/1.1 201 Created
Connection: close
Content-Type: application/json; charset=utf-8
Date: Thu, 10 Sep 2026 17:08:52 GMT
Server: Kestrel
Location: http://localhost:5003/Pizza/4
Transfer-Encoding: chunked

{
  "id": 4,
  "name": "Hawaii",
  "isGlutenFree": false
}

```

### PUT:

```

HTTP/1.1 204 No Content
Connection: close
Date: Thu, 10 Sep 2026 17:10:05 GMT
Server: Kestrel

```

### GET by Id:

```

HTTP/1.1 200 OK
Connection: close
Content-Type: application/json; charset=utf-8
Date: Thu, 10 Sep 2026 17:10:37 GMT
Server: Kestrel
Transfer-Encoding: chunked

{
  "id": 4,
  "name": "Hawaiian",
  "isGlutenFree": false
}

```

### DELETE:

```

HTTP/1.1 204 No Content
Connection: close
Date: Thu, 10 Sep 2026 17:10:53 GMT
Server: Kestrel

```

## 2. Sales Summary Function:

```

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

```

## The output:

```
Sales Summary
----------------------------
Total Sales: $2,012.20

Details:
 sales.json: $88.88
 sales.json: $501.22
 sales.json: $1,234.22
 sales.json: $99.00
 sales.json: $88.88
 ```