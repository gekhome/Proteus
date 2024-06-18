namespace Proteus.Reports.Students
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for StudentAtomikoDeltio.
    /// </summary>
    public partial class FoitisiDeltio : Telerik.Reporting.Report
    {
        public FoitisiDeltio()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        private void TermGroupHeader_ItemDataBound(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.GroupSection group = (Telerik.Reporting.Processing.GroupSection)(sender as Telerik.Reporting.Processing.GroupSection);
            Telerik.Reporting.Processing.TextBox text_term = (Telerik.Reporting.Processing.TextBox)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(group, "txtTermID");
            Telerik.Reporting.Processing.TextBox text_exodos = (Telerik.Reporting.Processing.TextBox)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(group, "txtExodosType");
            Telerik.Reporting.Processing.TextBox text_status = (Telerik.Reporting.Processing.TextBox)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(group, "txtStatus");
            Telerik.Reporting.Processing.TextBox text_egrafi = (Telerik.Reporting.Processing.TextBox)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(group, "txtEgrafiType");

            // txtStatus ÂﬂÌ·È ÙÔ Â‰ﬂÔ ÷œ…‘«”« - „È· ·ÔÙı˜ﬂ· ›˜ÂÈ ÙÈÏ›Ú 3 (¡ÌÂ·ÒÍﬁÚ) ﬁ 6 (≈·Ì··Ò·ÍÔÎÔ˝ËÁÛÁ)

            //if (text_egrafi.Value.ToString() == "1") this.txtApotelesma.Value = "¡ÔÙ›ÎÂÛÏ· : ≈–…‘’◊…¡";
            //else if (text_egrafi.Value.ToString() == "2") this.txtApotelesma.Value = "¡ÔÙ›ÎÂÛÏ· : ≈–¡Õ¡À«ÿ« ≈Œ¡Ã«Õœ’";

            if (text_status.Value.ToString() == "3") this.txtApotelesma.Value = "¡ÔÙ›ÎÂÛÏ· : ≈–¡Õ¡À«ÿ« ≈Œ¡Ã«Õœ’";
            else if (text_status.Value.ToString() == "6" || text_status.Value.ToString() == "7") 
                this.txtApotelesma.Value = "¡ÔÙ›ÎÂÛÏ· : ≈–¡Õ¡–¡—¡ œÀœ’»«”« Ã¡»«Ã¡‘ŸÕ Ã≈ ¡Õ≈–¡— ≈…¡";
            else this.txtApotelesma.Value = "¡ÔÙ›ÎÂÛÏ· : ≈–…‘’◊«” –¡—¡ œÀœ’»«”«";

            if (text_exodos.Value.ToString() == "2")
            {
                this.txtResult.Value = "¡–œ÷œ…‘«”≈";
            }
            else
            {
                //this.txtResult.Value = "TESTING VALUES:" + " term:" + text_term.Value.ToString() + " exodos:" + text_exodos.Value.ToString() + " status:" + text_status.Value.ToString();
                if (text_term.Value.ToString() == "4")
                {
                    if (text_status.Value.ToString() == "3" || text_status.Value.ToString() == "7") this.txtResult.Value = "¡Õ≈–¡— «” ÷œ…‘«”«";
                    else if (text_status.Value.ToString() == "6") this.txtResult.Value = "≈–¡— «” ÷œ…‘«”«";
                    else this.txtResult.Value = " ¡ÕœÕ… « –¡—¡ œÀœ’»«”«";
                }
                else if (text_term.Value.ToString() == "1")
                {
                    if (text_status.Value.ToString() == "3") this.txtResult.Value = "ƒ≈Õ ¡–œ ‘¡ ƒ… ¡…ŸÃ¡ ≈√√—¡÷«” ”‘œ ¬' ≈Œ¡Ã«Õœ";
                    else this.txtResult.Value = "¡–œ ‘¡ ƒ… ¡…ŸÃ¡ ≈√√—¡÷«” ”‘œ ¬' ≈Œ¡Ã«Õœ";
                }
                else if (text_term.Value.ToString() == "2")
                {
                    if (text_status.Value.ToString() == "3") this.txtResult.Value = "ƒ≈Õ ¡–œ ‘¡ ƒ… ¡…ŸÃ¡ ≈√√—¡÷«” ”‘œ √' ≈Œ¡Ã«Õœ";
                    else this.txtResult.Value = "¡–œ ‘¡ ƒ… ¡…ŸÃ¡ ≈√√—¡÷«” ”‘œ √' ≈Œ¡Ã«Õœ";
                }
                else if (text_term.Value.ToString() == "3")
                {
                    if (text_status.Value.ToString() == "3") this.txtResult.Value = "ƒ≈Õ ¡–œ ‘¡ ƒ… ¡…ŸÃ¡ ≈√√—¡÷«” ”‘œ ƒ' ≈Œ¡Ã«Õœ";
                    else this.txtResult.Value = "¡–œ ‘¡ ƒ… ¡…ŸÃ¡ ≈√√—¡÷«” ”‘œ ƒ' ≈Œ¡Ã«Õœ";
                }
            }
        }

        private void subReport2_ItemDataBound(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.SubReport subReport = (Telerik.Reporting.Processing.SubReport)sender;
            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)subReport.InnerReport;

            var items = Telerik.Reporting.Processing.ElementTreeHelper.FindChildByName(report, "detail", true).Length;
            if (!(items > 0))
            {
                this.txtApalagiText.Visible = true;
                this.txtApalagiText.Value = " ¡Ã…¡ ¡–¡ÀÀ¡√«";
            }
        }

        private void subReport3_ItemDataBound(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.SubReport subReport = (Telerik.Reporting.Processing.SubReport)sender;
            Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)subReport.InnerReport;

            this.txtPraktikiNoData.Visible = false;
            var items = Telerik.Reporting.Processing.ElementTreeHelper.FindChildByName(report, "detail", true).Length;
            if (items == 0)
            {
                this.txtPraktikiNoData.Visible = true;
                this.txtPraktikiNoData.Value = "*** ƒ≈Õ ’–¡—◊œ’Õ ”‘œ…◊≈…¡ –—¡ ‘… «” ¡” «”«” ***";
            }
        }

    }
}