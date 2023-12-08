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
    public class ExportBase : IExport
    {
        public ProjectResponse Project { get; set; }
        public Dictionary<int,XLColor> Colors { get; set; }
        public ExportBase(ProjectResponse project)
        {
            this.Project = project;
            this.SetColors();
        }
        public void SetColors()
        {
            this.Colors = new Dictionary<int, XLColor>();
            this.Colors.Add(0, XLColor.Green);
            this.Colors.Add(1, XLColor.SkyBlue);
            this.Colors.Add(2, XLColor.LightBrown);
            this.Colors.Add(3, XLColor.Orange);
            this.Colors.Add(4, XLColor.LightCornflowerBlue);
            this.Colors.Add(5, XLColor.Yellow);
            this.Colors.Add(6, XLColor.YellowGreen);
            this.Colors.Add(7, XLColor.LightCoral);
            this.Colors.Add(8, XLColor.LightPastelPurple);
            this.Colors.Add(9, XLColor.GreenYellow);
        }
        public XLColor GetColor(int key)
        {
            if(this.Colors.ContainsKey(key)){
                return this.Colors[key];
            }
            return this.Colors[1];
        }
        public virtual MemoryStream Execute() { throw new NotImplementedException(); }
    }
}
