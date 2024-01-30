using System;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main()
    {
        string filePath = "./Companies.json";

        if (File.Exists(filePath))
        {
            try
            {
              string jsonString = File.ReadAllText(filePath);

              // Deserialize the JSON string into an object
              CompanyList companyList = JsonSerializer.Deserialize<CompanyList>(jsonString);

              string tableHtml = @"
              <table style='border-collapse: collapse; width: 100%; text-align: center;'>
                  <thead>
                      <tr>
                          <th style='border: 1px solid #dddddd; padding: 8px;'>Name</th>
                          <th style='border: 1px solid #dddddd; padding: 8px;'>Ticker</th>
                          <th style='border: 1px solid #dddddd; padding: 8px;'>Price</th>
                          <th style='border: 1px solid #dddddd; padding: 8px;'>Delta</th>
                      </tr>
                  </thead>
                  <tbody>";

              foreach (Company company in companyList.companies)
              {
                  tableHtml += @$"
                      <tr>
                          <td style='border: 1px solid #dddddd; padding: 8px;'>{company.name}</td>
                          <td style='border: 1px solid #dddddd; padding: 8px;'>{company.ticker}</td>
                          <td style='border: 1px solid #dddddd; padding: 8px;'>{company.price}</td>
                          <td style='border: 1px solid #dddddd; padding: 8px;'>{company.delta}</td>
                      </tr>";
              }

              tableHtml += @"
                  </tbody>
              </table>";

              File.WriteAllText("./index.html", tableHtml);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading or deserializing JSON file: {ex.Message}");
            }

        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }
}

public class CompanyList
{
  public List<Company> companies { get; set; }   
}

public class Company
{
  public string name { get; set; }
  public string ticker { get; set; }
  public decimal price { get; set; }
  public decimal delta { get; set; }
}
