using System.Globalization;

namespace Source.Data
{
    public static class DataLoader
    {
        public static (List<double[]> Inputs, List<double> Targets) LoadCsv(string filePath)
        {
            // Skip the header row
            var lines = File.ReadAllLines(filePath).Skip(1);

            var inputs = new List<double[]>();
            var targets = new List<double>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');

                // We expect at least 9 columns based on your structure.
                // 0:PROTEIN, 1:FAT, 2:CARBS, 3:ENERGY, 4:FIBER, 5:SAT_FAT, 6:SUGARS
                // 7:NUTRI_SCORE (IGNORED)
                // 8:CLASSIFICATION (TARGET)

                if (parts.Length < 9) continue;

                // 1. Parse Inputs (Indices 0 to 6) -> Dimension = 7
                double[] rowInput = new double[7];
                for (int i = 0; i < 7; i++)
                {
                    rowInput[i] = double.Parse(parts[i], CultureInfo.InvariantCulture);
                }

                // 2. Parse Target (Index 8) -> Skip Index 7 entirely
                double target = double.Parse(parts[8], CultureInfo.InvariantCulture);

                inputs.Add(rowInput);
                targets.Add(target);
            }

            return (inputs, targets);
        }
    }
}