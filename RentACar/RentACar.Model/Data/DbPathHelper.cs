using System;
using System.IO;

namespace RentACar.Model.Data
{
    public static class DbPathHelper
    {
        public static string GetSharedSqlitePath()
        {
            // Търси нагоре от BaseDirectory докато намери .sln файл
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            while (dir != null)
            {
                if (dir.GetFiles("*.sln").Length > 0)
                {
                    // Намерихме solution root-а
                    var dataDir = Path.Combine(dir.FullName, "SharedData");
                    Directory.CreateDirectory(dataDir);
                    return Path.Combine(dataDir, "rentacar.db");
                }
                dir = dir.Parent;
            }

            // Fallback ако не намери .sln
            var fallbackDir = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "SharedData");
            Directory.CreateDirectory(fallbackDir);
            return Path.Combine(fallbackDir, "rentacar.db");
        }
    }
}