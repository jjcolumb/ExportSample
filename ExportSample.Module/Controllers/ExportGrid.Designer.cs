
namespace ExportSample.Module.Controllers
{
    partial class ExportGrid
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Export = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // Export
            // 
            this.Export.Caption = "Export";
            this.Export.Category = "Export";
            this.Export.ConfirmationMessage = null;
            this.Export.Id = "23fc6d5a-bd4f-449d-b530-1f8bb0266ebf";
            this.Export.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Caption;
            this.Export.ToolTip = null;
            this.Export.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.Export_Execute);
            // 
            // ExportGrid
            // 
            this.Actions.Add(this.Export);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction Export;
    }
}
