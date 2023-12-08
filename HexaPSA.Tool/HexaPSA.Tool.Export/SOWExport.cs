using ClosedXML.Excel;
using HexaPSA.Tool.Application.Contracts.Project;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Export
{
    public class SOW : ExportBase
    {
        public SOW(ProjectResponse project) : base(project)
        {
            this.Project = project;
        }
        public override MemoryStream Execute() { throw new NotImplementedException(); }
    }

}
