using System.Text.Json.Serialization;

namespace PPSDotNetInternshipTraining.HighChart.Models
{
    public class ChartDataModel
    {
        // Maps the C# "Name" to the JS "name"
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("y")]
        public double Y { get; set; }

        // Using bool? (nullable) because not every item has "sliced"
        [JsonPropertyName("sliced")]
        public bool? Sliced { get; set; }

        [JsonPropertyName("selected")]
        public bool? Selected { get; set; }
    }
}