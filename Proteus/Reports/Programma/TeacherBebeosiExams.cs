namespace Proteus.Reports.Programma
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Telerik.Reporting.Processing;
    using Proteus.BPM;
    using Proteus.DAL;
    using Proteus.Models;
    using System.Linq;

    /// <summary>
    /// Summary description for TeacherBebeosi1.
    /// </summary>
    public partial class TeacherBebeosiExams : Telerik.Reporting.Report
    {
        private ProteusDBEntities db = new ProteusDBEntities();

        public TeacherBebeosiExams()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            //this.txtHoursWord.Value = HumanFriendlyInteger.IntegerToWritten(135).ToString(); // this verifies

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        private void detail_ItemDataBound(object sender, System.EventArgs e)
        {
            Telerik.Reporting.Processing.DetailSection detail = (Telerik.Reporting.Processing.DetailSection)(sender as Telerik.Reporting.Processing.DetailSection);
            Telerik.Reporting.Processing.TextBox text1 = (Telerik.Reporting.Processing.TextBox)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(detail, "fieldTotalHours");
            Telerik.Reporting.Processing.TextBox text2 = (Telerik.Reporting.Processing.TextBox)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(detail, "txtHoursWord");

            int hours;
            string HoursWord;
            string strHours = text1.Value.ToString();
            bool res = int.TryParse(strHours, out hours);
            HoursWord = HumanFriendlyInteger.IntegerToWritten(hours).ToString();

            text2.Value = HoursWord;
        }
    }
}