using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Export
{
    public interface IExport
    {
        public MemoryStream Execute();
    }
}
