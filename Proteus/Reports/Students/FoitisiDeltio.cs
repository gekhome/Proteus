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

            // txtStatus ����� �� ����� ������� - ��� �������� ���� ����� 3 (���������) � 6 (������������������)

            //if (text_egrafi.Value.ToString() == "1") this.txtApotelesma.Value = "���������� : ��������";
            //else if (text_egrafi.Value.ToString() == "2") this.txtApotelesma.Value = "���������� : ��������� ��������";

            if (text_status.Value.ToString() == "3") this.txtApotelesma.Value = "���������� : ��������� ��������";
            else if (text_status.Value.ToString() == "6" || text_status.Value.ToString() == "7") 
                this.txtApotelesma.Value = "���������� : ������������������ ��������� �� ����������";
            else this.txtApotelesma.Value = "���������� : �������� �������������";

            if (text_exodos.Value.ToString() == "2")
            {
                this.txtResult.Value = "����������";
            }
            else
            {
                //this.txtResult.Value = "TESTING VALUES:" + " term:" + text_term.Value.ToString() + " exodos:" + text_exodos.Value.ToString() + " status:" + text_status.Value.ToString();
                if (text_term.Value.ToString() == "4")
                {
                    if (text_status.Value.ToString() == "3" || text_status.Value.ToString() == "7") this.txtResult.Value = "��������� �������";
                    else if (text_status.Value.ToString() == "6") this.txtResult.Value = "������� �������";
                    else this.txtResult.Value = "�������� �������������";
                }
                else if (text_term.Value.ToString() == "1")
                {
                    if (text_status.Value.ToString() == "3") this.txtResult.Value = "��� ������ �������� �������� ��� �' �������";
                    else this.txtResult.Value = "������ �������� �������� ��� �' �������";
                }
                else if (text_term.Value.ToString() == "2")
                {
                    if (text_status.Value.ToString() == "3") this.txtResult.Value = "��� ������ �������� �������� ��� �' �������";
                    else this.txtResult.Value = "������ �������� �������� ��� �' �������";
                }
                else if (text_term.Value.ToString() == "3")
                {
                    if (text_status.Value.ToString() == "3") this.txtResult.Value = "��� ������ �������� �������� ��� �' �������";
                    else this.txtResult.Value = "������ �������� �������� ��� �' �������";
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
                this.txtApalagiText.Value = "����� ��������";
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
                this.txtPraktikiNoData.Value = "*** ��� �������� �������� ��������� ������� ***";
            }
        }

    }
}