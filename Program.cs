using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
class Program
{
    static void Main()
    {
        var data = new List<CountryCity>
        {
            new CountryCity("India", "Delhi"),
            new CountryCity("India", "Bangalore"),
            new CountryCity("India", "Chennai"),
            new CountryCity("India", "Trivandrum"),
            new CountryCity("India", "Goa"),
            new CountryCity("USA", "New York"),
            new CountryCity("USA", "Seattle"),
            new CountryCity("USA", "San Diego"),
            new CountryCity("USA", "California"),
            new CountryCity("Canada", "Montreal"),
            new CountryCity("Canada", "Toronto"),
            new CountryCity("Canada", "Vancouver"),
            new CountryCity("Canada", "Ottawa")
        };

        var result = TransformData(data);
        PrintTable(result);
    }

    public class CountryCity
    {
        public string Country { get; set; }
        public string City { get; set; }

        public CountryCity(string country, string city)
        {
            Country = country;
            City = city;
        }
    }

    static Dictionary<string, List<string>> TransformData(List<CountryCity> data)
    {
        var result = new Dictionary<string, List<string>>();

        foreach (var group in data.GroupBy(x => x.Country))
        {
            var cities = group.Select(x => x.City).ToList();
            var currentRow = new List<string>();
            var currentCell = new StringBuilder();
            int currentLength = 0;

            foreach (var city in cities)
            {
                if (currentLength + city.Length > 15)
                {
                    currentRow.Add(currentCell.ToString());
                    currentCell.Clear();
                    currentCell.Append(city);
                    currentLength = city.Length;
                }
                else
                {
                    if (currentCell.Length > 0)
                    {
                        currentCell.Append(", ");
                        currentLength += 2; 
                    }
                    currentCell.Append(city);
                    currentLength += city.Length;
                }
            }

            if (currentCell.Length > 0)
            {
                currentRow.Add(currentCell.ToString());
            }

            result.Add(group.Key, currentRow);
        }

        return result;
    }

    static void PrintTable(Dictionary<string, List<string>> result)
    {
        int maxColumns = result.Values.Max(row => row.Count);

        Console.Write("Country".PadRight(20));
        for (int i = 1; i <= maxColumns; i++)
        {
            Console.Write($"Column {i}".PadRight(20));
        }
        Console.WriteLine();

        Console.WriteLine(new string('-', 20 + maxColumns * 20));

        foreach (var country in result)
        {
            Console.Write(country.Key.PadRight(20));
            foreach (var cell in country.Value)
            {
                Console.Write(cell.PadRight(20));
            }
            Console.WriteLine();
        }
    }
}
