using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Wpf
{
    public static class ApplicationState
    {
        // "Sqlite" или "SqlServer"
        public static string Provider { get; set; } = "Sqlite";
    }
}