
using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Drawing;
using System.Linq;

namespace ExportSample.Module.Web.Controllers
{
    public class GeneralListViewController : ViewController<ListView>
    {
      
        private ExportController exportController;
        protected override void OnActivated()
        {
            base.OnActivated();

            exportController = Frame.GetController<ExportController>();

            if (exportController != null)
            {
                exportController.ExportAction.Executing += ExportAction_Executing;
            }

            View.SelectionChanged += View_SelectionChanged;
        }

        private void ExportAction_Executing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SingleChoiceAction ea = sender as SingleChoiceAction;
            if (ea != null)
            {
                var easc = ea.SelectionContext as ListView;
                var ve = View.Editor as ASPxGridListEditor;

                //if (easc != null && ve != null && ve.Grid.Selection.Count > 2)
                if (easc != null && ve != null && ve.Grid.VisibleRowCount > 2)                
                {
                    Application.ShowViewStrategy.ShowMessage(new MessageOptions
                    {
                        Duration = 3000,
                        Message = "A maximum of 2 data rows can be exported at a time. Please use filters to reduce the number of data rows.",
                        Type = InformationType.Warning
                    });
                    e.Cancel = true;
                }
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            View.SelectionChanged -= View_SelectionChanged;
           

        }

        private void View_SelectionChanged(object sender, EventArgs e)
        {
            var lv = (ListView)sender;
            if (lv.SelectedObjects.Count > 2)
            {
                Application.ShowViewStrategy.ShowMessage(new MessageOptions
                {
                    Duration = 3000,
                    Message = "A maximum of 2 data rows can be selected at a time. Please use filters to reduce the number of data rows.",
                    Type = InformationType.Warning
                });

                ((ASPxGridListEditor)lv.Editor).Grid.Selection.UnselectAll();
            }
        }

        protected override void OnViewControlsCreated()
        {

            base.OnViewControlsCreated();
            
        }
       
    }
}
