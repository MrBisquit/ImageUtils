using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUtils.Utils
{
    public static class LengthUtils
    {
        public static string BytesToFormatted(long bytes)
        {
            // Bing AI

            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int index = 0;
            double size = bytes;

            while (size >= 1024 && index < suffixes.Length - 1)
            {
                size /= 1024;
                index++;
            }

            return $"{size:F2} {suffixes[index]}";
        }
    }
}
