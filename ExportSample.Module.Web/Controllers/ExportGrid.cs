using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Web;
using DevExpress.Xpo;
using DevExpress.XtraPrinting;
using DevExpress.Web.Internal;
using DevExpress.XtraReports.UI;
using ExportSample.Module.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.Web.Data;
using ExportSample.Module.BusinessObjects;
using System.IO;
using DevExpress.ExpressApp.Web.SystemModule;
using System.Web;

namespace ExportSample.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ExportGrid : ViewController
    {
        public ExportGrid()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }

        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void Export_Execute(object sender, SimpleActionExecuteEventArgs e)
        {            
            Session session = ((XPObjectSpace)ObjectSpace).Session;

            ASPxGridListEditor listEditor = ((ListView)View).Editor as ASPxGridListEditor;
            if (listEditor != null)
            {
                ASPxGridView gridView = listEditor.Grid;
                ReadOnlyGridColumnCollection<GridViewColumn> visiblecolumns = gridView.VisibleColumns;
              
                IList<string> propertyNames = new List<string>();
                IList<Type> propertyTypes = new List<Type>();
                foreach (GridViewColumn column in visiblecolumns)
                {
                    if (!String.IsNullOrWhiteSpace(column.Caption))
                    {
                        if (column is GridViewDataColumn)
                            propertyNames.Add(((IWebColumnInfo)column).FieldName);                       
                            propertyTypes.Add(((IFilterablePropertyInfo)column).PropertyType);
                    }
                    
                }

                //string queryString = "Select ";
                //foreach (var detail in propertyNames)
                //{                    
                //    queryString += " " + detail;
                //    if(detail != propertyNames.Last()) { queryString += ","; }
                //}
                //queryString += " from Customer";

                //var queryResult = session.ExecuteQuery(queryString);

                //XPDataView dataView = new XPDataView(session.Dictionary, propertyNames, propertyTypes);
                //dataView.LoadData(queryResult);

                string fields = String.Empty;
                foreach (var detail in propertyNames)
                {
                    fields += detail;
                    if (detail != propertyNames.Last()) { fields += ";"; }
                }             

                var dataView = ObjectSpace.CreateDataView(typeof(Customer), fields, null, null);

                // Report
                XtraReport report = new XtraReport();

                report.DataSource = dataView;

                ReportHelper.CreateReport(report, propertyNames.ToArray());
                report.Name = "CustomerReport";
                report.CreateDocument();

                // Export
                XlsxExportOptions xlsxExportOptions = report.ExportOptions.Xlsx;
                xlsxExportOptions.SheetName = "Customer Report";
                //report.ExportToXlsx(@"c:\temp\file.xlsx", xlsxExportOptions);

                
                using (MemoryStream stream = new MemoryStream())
                {
                    //string fileName = "Customer.xlsx";
                   // report.ExportToXlsx(stream, xlsxExportOptions);
                    //stream.Seek(0, System.IO.SeekOrigin.Begin);
                    HttpResponse response = HttpContext.Current.Response;
                    response.Clear();
                    HttpContext.Current.Response.ContentType = MimeMapping.GetMimeMapping("Customer.xlsx");
                    string contentHeader = string.Format("attachment; filename={0}", "Customer.xlsx");
                    response.AddHeader("Content-Disposition", contentHeader);
                    report.ExportToXlsx(response.OutputStream, xlsxExportOptions);
                    response.Flush();
                    response.End();
                }

            }
        }
     
    }
}

//ReportHelper.CreateReport(report, new string[] { "FirstName", "LastName", "Gender", "DateOfBirth" });
//string queryString = "Select FirstName, LastName, Gender, DateOfBirth from Customer";
//IList<string> propertyNames = new List<string>() { "FirstName", "LastName", "Gender", "DateOfBirth" };
//IList<Type> propertyTypes = new List<Type>()
//{
//    typeof(string),
//    typeof(string),
//    typeof(string),
//    typeof(DateTime)
//};