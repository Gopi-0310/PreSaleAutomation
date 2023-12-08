using ClosedXML.Excel;
using HexaPSA.Tool.Application.Contracts.Project;
using System.Data;


namespace HexaPSA.Tool.Export
{


    public class LOE : ExportBase
    {
        Dictionary<int, int> WeekCell = new Dictionary<int, int>();
        double allSectionTotalHours = 0;
        double allSectionTotalRates = 0;
        int currentRow = 2;
        public LOE(ProjectResponse project) : base(project)
        {
            this.Project = project;
        }
        public override MemoryStream Execute()
        {
            MemoryStream stream = new MemoryStream();

            using (XLWorkbook wb = new XLWorkbook())
            {

                CostSummartSheet(wb);
                LOESheet(wb);
                ResourceRateCardSheet(wb);

                wb.SaveAs(stream);

            }


            return stream;
        }

        #region  1.Cost Summary Sheet Preparation
        private void CostSummartSheet(XLWorkbook wb)
        {
            DataSet ds = new DataSet();
            DataTable costResource = Project.CostSummary.CostByResourceList.ToDataTable();
            costResource.TableName = Application.Utilities.Constants.CostbyResource;
            DataTable costByWorkStreams = Project.CostSummary.CostByWorkStreamList.ToDataTable();
            costByWorkStreams.TableName = Application.Utilities.Constants.CostbyWorkstream;
            ds.Tables.Add(costResource);
            ds.Tables.Add(costByWorkStreams);

            int currentRow = 1;//counter to keep track of what row we're on
            IXLWorksheet worksheet = wb.AddWorksheet(sheetName: "1.Cost Summary");

            try
            {
                foreach (DataTable table in ds.Tables)
                {
                    //Use the table name, and add it to the first cell above the table before insert
                    worksheet.Cell(currentRow, 1).Value = table.TableName;
                    worksheet.Range(worksheet.Cell(currentRow, 1), worksheet.Cell(currentRow, table.Columns.Count)).Merge();
                    worksheet.Cell(currentRow, 1).Style.Font.FontSize = 11;
                    worksheet.Cell(currentRow, 1).Style.Font.FontName = "Calibri";
                    worksheet.Cell(currentRow, 1).Style.Fill.BackgroundColor = XLColor.AshGrey;
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(currentRow, 1).Style.Font.SetBold();
                    currentRow++;
                    //now that the title is inserted and formatted, insert table
                    worksheet.Cell(currentRow, 1).InsertTable(table);
                    currentRow += table.Rows.Count + 2;

                    // Calculate and insert subtotals for hours and fees
                    int hoursColumnIndex = -1;
                    int feesColumnIndex = -1;

                    // Find the columns for hours and fees
                    for (int col = 1; col <= table.Columns.Count; col++)
                    {
                        if (table.Columns[col - 1].ColumnName.Equals("Hours", StringComparison.OrdinalIgnoreCase))
                        {
                            hoursColumnIndex = col;
                        }
                        else if (table.Columns[col - 1].ColumnName.Equals("Fees", StringComparison.OrdinalIgnoreCase))
                        {
                            feesColumnIndex = col;
                        }
                    }

                    // If both hours and fees columns are found, calculate and insert subtotals
                    if (hoursColumnIndex != -1 && feesColumnIndex != -1)
                    {
                        decimal totalHours = 0;
                        decimal totalFees = 0;

                        // Calculate subtotals
                        foreach (DataRow row in table.Rows)
                        {
                            totalHours += Convert.ToDecimal(row[hoursColumnIndex - 1]);
                            totalFees += Convert.ToDecimal(row[feesColumnIndex - 1]);
                        }

                        // Insert subtotals in the same row


                        switch (table.TableName)
                        {
                            case Application.Utilities.Constants.CostbyResource:
                                worksheet.Cell(currentRow, hoursColumnIndex - 2).SetStyle(XLColor.AshGrey, XLAlignmentHorizontalValues.Center, IsNumber: true).Value = "Total";
                                worksheet.Cell(currentRow, 1).SetStyle(XLColor.AshGrey).Value = "";
                                worksheet.Cell(currentRow, 3).SetStyle(XLColor.AshGrey).Value = "";
                                worksheet.Cell(currentRow, 5).SetStyle(XLColor.AshGrey).Value = "";
                                worksheet.Cell(currentRow, 6).SetStyle(XLColor.AshGrey).Value = "";
                                break;
                            case Application.Utilities.Constants.CostbyWorkstream:
                                worksheet.Cell(currentRow, hoursColumnIndex - 1).SetStyle(XLColor.AshGrey, XLAlignmentHorizontalValues.Center, IsNumber: true).Value = "Total";
                                break;
                            default:
                                // Handle logic for other tables
                                break;
                        }
                        worksheet.Cell(currentRow, hoursColumnIndex).SetStyle(XLColor.AshGrey, XLAlignmentHorizontalValues.Center, IsNumber: true).Value = totalHours;
                        worksheet.Cell(currentRow, feesColumnIndex).SetStyle(XLColor.AshGrey, XLAlignmentHorizontalValues.Right, IsNumber: true).Value = $"${totalFees.ToString("0.00")}";

                        currentRow++;
                    }
                    currentRow += table.Rows.Count + 3;
                }
            }
            catch (Exception ex)
            {

            }
            //optional for adjusting columns to their contents and setting wrap text
            var cols = worksheet.Columns();
            cols.AdjustToContents();
            foreach (var a in cols)
            {//set mas width to 50
                a.Width = a.Width > 50 ? 50 : a.Width;
            }
            cols.Style.Alignment.WrapText = true;

        }

        #endregion

        #region  2.LOE Sheet Preparation
        private void LOESheet(XLWorkbook workbook)
        {

            IXLWorksheet worksheet = workbook.Worksheets.Add("2.LOE");

            SetLOEHeader(worksheet);

            SetLOEBody(worksheet);

            SetLOEFooter(worksheet);


            //optional for adjusting columns to their contents and setting wrap text
            var cols = worksheet.Columns();
            cols.AdjustToContents();
            foreach (var a in cols)
            {//set mas width to 50
                a.Width = a.Width > 50 ? 50 : a.Width;
            }
            cols.Style.Alignment.WrapText = true;

        }

        private void SetLOEHeader(IXLWorksheet worksheet)
        {
            worksheet.Cell(1, 1).SetStyle(XLColor.AshGrey).Value = "Workstream";

            worksheet.Cell(1, 2).SetStyle(XLColor.AshGrey).Value = "Activity";

            worksheet.Cell(1, 3).SetStyle(XLColor.AshGrey).Value = "Role";

            worksheet.Cell(1, 4).SetStyle(XLColor.AshGrey).Value = "Role Description";


            var columnLength = 5;
            foreach (var week in Project.CostSummary.Weeks)
            {
                worksheet.Cell(1, columnLength).SetStyle(XLColor.AshGrey).Value = "Week " + week;

                WeekCell.Add(week, columnLength);
                columnLength += 1;
            }
            worksheet.Cell(1, columnLength).SetStyle(XLColor.AshGrey).Value = "Total Hours";

            worksheet.Cell(1, columnLength + 1).SetStyle(XLColor.AshGrey).Value = "Rate($)";

            columnLength = columnLength + 2;
            worksheet.Cell(1, columnLength).SetStyle(XLColor.AshGrey).Value = "Total";
        }

        private void SetLOEBody(IXLWorksheet worksheet)
        {
            currentRow = 2;


            for (int workStreamIndex = 1; workStreamIndex <= Project.WorkStreamResponses.Count; workStreamIndex++)
            {
                var wrkActivities = Project.WorkStreamResponses[workStreamIndex - 1].WorkStreamActivityList;
                var color = this.GetColor(workStreamIndex - 1);
                worksheet.Cell(currentRow, 1).SetStyle(color).Value = Project.WorkStreamResponses[workStreamIndex - 1].Name;
                worksheet.Cell(currentRow, 2).SetStyle(color).Value = "";
                worksheet.Cell(currentRow, 3).SetStyle(color).Value = "";
                worksheet.Cell(currentRow, 4).SetStyle(color).Value = "";

                var iColumnLength = 5;
                foreach (var week in Project.CostSummary.Weeks)
                {
                    worksheet.Cell(currentRow, iColumnLength).SetStyle(color).Value = "";

                    iColumnLength += 1;
                }
                double totalHours = wrkActivities.Select(h => h.Hours).Sum();
                double totalRate = wrkActivities.Select(h => h.Rate).Sum();

                worksheet.Cell(currentRow, iColumnLength).SetStyle(color, XLAlignmentHorizontalValues.Center, IsNumber: true).Value = totalHours;
                allSectionTotalHours += totalHours;
                worksheet.Cell(currentRow, iColumnLength + 1).SetStyle(color, XLAlignmentHorizontalValues.Center, IsNumber: true).Value = totalRate;
                iColumnLength = iColumnLength + 2;


                double totalRateCalc = 0;
                for (int tIndex = 1; tIndex <= wrkActivities.Count; tIndex++)
                {
                    double hours = 0;
                    foreach (KeyValuePair<int, int> wk in WeekCell)
                    {

                        if (wk.Key == wrkActivities[tIndex - 1].Week)
                        {
                            hours += wrkActivities[tIndex - 1].Hours;

                        }


                    }
                    var hrsRate = hours * wrkActivities[tIndex - 1].Rate;
                    totalRateCalc += hrsRate;
                    // hours* wrkActivities[insideIndex - 1].Rate;
                }
                worksheet.Cell(currentRow, iColumnLength).SetStyle(color, XLAlignmentHorizontalValues.Center, IsNumber: true).Value = totalRateCalc;
                //allSectionTotalRates += (totalHours * totalRate);
                allSectionTotalRates += totalRateCalc;

                var workStreamActivityId = Project.WorkStreamResponses[workStreamIndex - 1].Id;
                var insideColumnLength = 0;

                for (int insideIndex = 1; insideIndex <= wrkActivities.Count; insideIndex++)
                {
                    currentRow = currentRow + 1;
                    worksheet.Cell(currentRow, 1).SetStyle(XLColor.White, XLAlignmentHorizontalValues.Left).Value = string.Empty;
                    worksheet.Cell(currentRow, 2).SetStyle(XLColor.White, XLAlignmentHorizontalValues.Left).Value = wrkActivities[insideIndex - 1].Activity;
                    worksheet.Cell(currentRow, 3).SetStyle(XLColor.White, XLAlignmentHorizontalValues.Left).Value = wrkActivities[insideIndex - 1].Role;
                    worksheet.Cell(currentRow, 4).SetStyle(XLColor.White, XLAlignmentHorizontalValues.Left).Value = wrkActivities[insideIndex - 1].RoleDescription;

                    insideColumnLength = 5;
                    double hours = 0;
                    foreach (KeyValuePair<int, int> wk in WeekCell)
                    {

                        if (wk.Key == wrkActivities[insideIndex - 1].Week)
                        {
                            hours += wrkActivities[insideIndex - 1].Hours;
                            worksheet.Cell(currentRow, insideColumnLength).SetStyle(XLColor.White, XLAlignmentHorizontalValues.Center, IsNumber: true).Value = wrkActivities[insideIndex - 1].Hours;
                        }
                        else
                        {
                            worksheet.Cell(currentRow, insideColumnLength).SetStyle(XLColor.White, XLAlignmentHorizontalValues.Left).Value = "";
                        }
                        insideColumnLength += 1;
                    }

                    worksheet.Cell(currentRow, insideColumnLength).SetStyle(XLColor.White, XLAlignmentHorizontalValues.Center, IsNumber: true).Value = hours;
                    worksheet.Cell(currentRow, insideColumnLength + 1).SetStyle(XLColor.White, XLAlignmentHorizontalValues.Center, IsNumber: true).Value = wrkActivities[insideIndex - 1].Rate;
                    insideColumnLength = insideColumnLength + 2;
                    worksheet.Cell(currentRow, insideColumnLength).SetStyle(XLColor.White, XLAlignmentHorizontalValues.Center, IsNumber: true).Value = hours * wrkActivities[insideIndex - 1].Rate;


                }

                currentRow = currentRow + 1;

            }
        }

        private void SetLOEFooter(IXLWorksheet worksheet)
        {
            //Last Row
            currentRow += 1;
            worksheet.Cell(currentRow, 1).SetStyle(XLColor.CoolGrey).Value = "Totals";
            worksheet.Cell(currentRow, 2).SetStyle(XLColor.CoolGrey).Value = "";
            worksheet.Cell(currentRow, 3).SetStyle(XLColor.CoolGrey).Value = "";
            worksheet.Cell(currentRow, 4).SetStyle(XLColor.CoolGrey).Value = "";

            var iLastColumnLength = 5;
            foreach (var week in Project.CostSummary.Weeks)
            {
                worksheet.Cell(currentRow, iLastColumnLength).SetStyle(XLColor.CoolGrey).Value = "";

                iLastColumnLength += 1;
            }


            worksheet.Cell(currentRow, iLastColumnLength).SetStyle(XLColor.CoolGrey, XLAlignmentHorizontalValues.Center, IsNumber: true).Value = allSectionTotalHours;
            worksheet.Cell(currentRow, iLastColumnLength + 1).SetStyle(XLColor.CoolGrey).Value = "";
            iLastColumnLength = iLastColumnLength + 2;
            worksheet.Cell(currentRow, iLastColumnLength).SetStyle(XLColor.CoolGrey, XLAlignmentHorizontalValues.Center, IsNumber: true).Value = allSectionTotalRates;
        }

        #endregion

        #region   3.Resource Rate Card Sheet Preparation
        private void ResourceRateCardSheet(XLWorkbook wb)
        {
            IXLWorksheet worksheet = wb.AddWorksheet(sheetName: "3.Resource Rate Card");
            DataTable rrList = Project.ResourceRateList.ToDataTable();
            worksheet.Cell(1, 1).InsertTable(rrList);

            var cols = worksheet.Columns();
            cols.AdjustToContents();
            foreach (var a in cols)
            {//set mas width to 50
                a.Width = a.Width > 50 ? 50 : a.Width;
            }
            cols.Style.Alignment.WrapText = true;
        }
        #endregion

    }
    public static class CellExtension
    {
        public static IXLCell SetStyle(this IXLCell cell, XLColor xlColor, XLAlignmentHorizontalValues xlV = XLAlignmentHorizontalValues.Center, int fontSize = 11, string fontName = "Calibri", bool IsNumber = false)
        {

            cell.Style.Font.FontSize = 11;
            cell.Style.Font.FontName = "Calibri";
            cell.Style.Fill.BackgroundColor = xlColor;
            cell.Style.Alignment.SetHorizontal(xlV);

            cell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            cell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            if (xlV == XLAlignmentHorizontalValues.Center && !IsNumber)
                cell.Style.Font.SetBold();
            return cell;
        }
    }
}
