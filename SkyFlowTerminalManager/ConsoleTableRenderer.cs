using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyFlowTerminalManager
{
    public static class ConsoleTableRenderer
    {
        public static void RenderTable<T>(IEnumerable<T> data, string title = "")
        {
            if (data == null || !data.Any())
            {
                Console.WriteLine("No data to display.");
                return;
            }

            var properties = typeof(T).GetProperties();
            var headers = properties.Select(p => p.Name).ToList();

            // Calculate column widths
            var columnWidths = headers.Select(h => h.Length).ToArray();
            var rows = new List<string[]>();

            foreach (var item in data)
            {
                var rowValues = properties.Select(p => p.GetValue(item)?.ToString() ?? "").ToArray();
                rows.Add(rowValues);
                for (int i = 0; i < rowValues.Length; i++)
                {
                    if (rowValues[i].Length > columnWidths[i])
                    {
                        columnWidths[i] = rowValues[i].Length;
                    }
                }
            }

            // Adjust for padding
            for (int i = 0; i < columnWidths.Length; i++)
            {
                columnWidths[i] += 2; // Add padding
            }

            // Draw top border
            DrawLine(columnWidths);

            // Draw title if provided
            if (!string.IsNullOrEmpty(title))
            {
                Console.WriteLine($"| {title.PadRight(columnWidths.Sum() + headers.Count - 1 - title.Length)} |");
                DrawLine(columnWidths);
            }

            // Draw headers
            Console.Write("|");
            for (int i = 0; i < headers.Count; i++)
            {
                Console.Write($" {headers[i].PadRight(columnWidths[i] - 1)}|");
            }
            Console.WriteLine();

            // Draw header-data separator
            DrawLine(columnWidths);

            // Draw rows
            foreach (var row in rows)
            {
                Console.Write("|");
                for (int i = 0; i < row.Length; i++)
                {
                    Console.Write($" {row[i].PadRight(columnWidths[i] - 1)}|");
                }
                Console.WriteLine();
            }

            // Draw bottom border
            DrawLine(columnWidths);
        }

        private static void DrawLine(int[] columnWidths)
        {
            Console.Write("+");
            foreach (var width in columnWidths)
            {
                Console.Write(new string('-', width));
                Console.Write("+");
            }
            Console.WriteLine();
        }
    }
}
