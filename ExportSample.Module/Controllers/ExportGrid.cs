using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.XtraReports.UI;
using ExportSample.Module.Helpers;
using System;
using System.Linq;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp.Xpo;
using ExportSample.Module.BusinessObjects;
using DevExpress.XtraPrinting;
using System.Collections.Generic;

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
            // <<<<<<<o>>>>>>
            Session session = ((XPObjectSpace)ObjectSpace).Session;


            // Data from XAF
            string queryString = "Select FirstName, LastName, Gender, DateOfBirth from Customer";
            var queryResult = session.ExecuteQuery(queryString);

            IList<string> propertyNames = new List<string>() { "FirstName", "LastName", "Gender", "DateOfBirth" };
            IList<Type> propertyTypes = new List<Type>()
            {
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(DateTime)
            };
            XPDataView dataView = new XPDataView(session.Dictionary, propertyNames, propertyTypes);
            dataView.LoadData(queryResult);


            // Report
            XtraReport report = new XtraReport();

            report.DataSource = dataView;
            ReportHelper.CreateReport(report, new string[] { "FirstName", "LastName", "Gender", "DateOfBirth" });
            report.Name = "CustomerReport";
            report.CreateDocument();

            // Export
            XlsxExportOptions xlsxExportOptions = report.ExportOptions.Xlsx;
            xlsxExportOptions.SheetName = "Customer Report";
            report.ExportToXlsx(@"c:\temp\file.xlsx", xlsxExportOptions);
        }
    }
}
