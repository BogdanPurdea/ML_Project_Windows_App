using System.Globalization;

namespace Source.Data
{
    public static class DataLoader
    {
        ///<summary>
        /// Loads CSV data dynamically based on a provided schema string.
        ///</summary>
        /// <param name="filePath">Path to the CSV file.</param>
        /// <param name="schema">Semicolon-separated columns to use (e.g., "PROTEIN;FAT;CLASSIFICATION"). Last item is always the Target.</param>
        public static (List<double[]> Inputs, List<double> Targets) LoadCsv(string filePath, string schema)
        {
            var lines = File.ReadAllLines(filePath);

            if (lines.Length < 2)
                throw new Exception("CSV file is empty or missing data.");

            // 1. Parse the CSV Header to map Column Names -> Index
            //    e.g. { "PROTEIN": 0, "TOTAL_FAT": 1, ... }
            var headerLine = lines[0];
            var headerParts = headerLine.Split(';')
                                        .Select(h => h.Trim().ToUpperInvariant()) // Normalize to UPPER keys
                                        .ToArray();

            var csvColumnMap = new Dictionary<string, int>();
            for (int i = 0; i < headerParts.Length; i++)
            {
                // Safety check for duplicate headers
                if (!csvColumnMap.ContainsKey(headerParts[i]))
                {
                    csvColumnMap.Add(headerParts[i], i);
                }
            }

            // 2. Parse the User's Schema
            //    e.g. "PROTEIN;TOTAL_FAT;CLASSIFICATION"
            var schemaParts = schema.Split(';')
                                    .Select(s => s.Trim().ToUpperInvariant())
                                    .Where(s => !string.IsNullOrEmpty(s))
                                    .ToArray();

            if (schemaParts.Length < 2)
                throw new Exception("Schema must contain at least one Input feature and one Target (e.g., 'PROTEIN;CLASS').");

            // The LAST item in the schema is strictly the TARGET.
            string targetColumn = schemaParts.Last();

            // All PRECEDING items are INPUT features.
            string[] featureColumns = schemaParts.Take(schemaParts.Length - 1).ToArray();

            // 3. Validate that Schema columns exist in the CSV
            if (!csvColumnMap.ContainsKey(targetColumn))
                throw new Exception($"Target column '{targetColumn}' not found in CSV header.");

            foreach (var col in featureColumns)
            {
                if (!csvColumnMap.ContainsKey(col))
                    throw new Exception($"Feature column '{col}' not found in CSV header.");
            }

            // 4. Extract Data
            var inputs = new List<double[]>();
            var targets = new List<double>();

            // Skip header (start at index 1)
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;

                var rowParts = line.Split(';');

                // Parse Inputs
                double[] rowInput = new double[featureColumns.Length];
                for (int j = 0; j < featureColumns.Length; j++)
                {
                    string colName = featureColumns[j];
                    int csvIndex = csvColumnMap[colName];

                    if (csvIndex >= rowParts.Length) continue; // Safety for malformed lines

                    if (double.TryParse(rowParts[csvIndex], NumberStyles.Any, CultureInfo.InvariantCulture, out double val))
                    {
                        rowInput[j] = val;
                    }
                    else
                    {
                        rowInput[j] = 0.0; // Handle missing/bad data
                    }
                }

                // Parse Target
                int targetIndex = csvColumnMap[targetColumn];
                if (targetIndex < rowParts.Length &&
                    double.TryParse(rowParts[targetIndex], NumberStyles.Any, CultureInfo.InvariantCulture, out double targetVal))
                {
                    targets.Add(targetVal);
                    inputs.Add(rowInput);
                }
            }

            return (inputs, targets);
        }
    }
}