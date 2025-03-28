using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Shared.Diagnostics
{
    public static class ApplicationDiagnostics
    {
        public static string ActivitySourceName = "Calculator.Tool.Diagnostic";
        public static ActivitySource ActivitySource = new(ActivitySourceName);
    }
}
