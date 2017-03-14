using System.Drawing;
using System.IO;
using System.Text;

namespace ImageToTableBuilder
{
    public class Program
    {
        public static void Main()
        {
            var colorMatrix = GetBitMapColorMatrix("3.jpg");

            GenerateHTMLFile(colorMatrix, "output.html");
        }

        public static Color[,] GetBitMapColorMatrix(string bitmapFilePath)
        {
            Bitmap bitmap = new Bitmap(bitmapFilePath);

            int height = bitmap.Height;
            int width = bitmap.Width;

            Color[,] colorMatrix = new Color[height, width];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    colorMatrix[row, col] = bitmap.GetPixel(col, row);
                }
            }

            return colorMatrix;
        }

        public static void GenerateHTMLFile(Color[,] colorMatrix, string outputPath)
        {
            var sb = new StringBuilder();

            Color currentColor;

            sb.AppendLine("<html>");
            sb.AppendLine("<body>");
            sb.AppendLine("<table border=\"0\" cellpadding=\"0\" cellspacing=\"1\" style=\"border-collapse: collapse\">");

            for (int row = 0; row < colorMatrix.GetLength(0); row++)
            {
                sb.AppendLine("<tr height=\"1\">");

                for (int col = 0; col < colorMatrix.GetLength(1); col++)
                {
                    currentColor = colorMatrix[row, col];

                    sb.AppendLine($"<td width=\"1\" style=\"background-color: rgba({currentColor.R}, {currentColor.G}, {currentColor.B}, {currentColor.A})\">");
                    sb.AppendLine("</td>");
                }

                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            using (var sw = new StreamWriter(outputPath))
            {
                sw.WriteLine(sb.ToString());
            }
        }
    }
}
