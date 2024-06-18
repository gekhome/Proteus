using Proteus.DAL;
using Proteus.Models;

namespace Proteus.Reports.Archive
{
    partial class _TeacherBebeosiExamsTotal
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.TEACHER_IDGroupFooter = new Telerik.Reporting.GroupFooterSection();
            this.tEACHER_IDGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.Ï«Õ¡”_≈‘œ”DataTextBox = new Telerik.Reporting.TextBox();
            this.fieldTotalHours = new Telerik.Reporting.TextBox();
            this.txtHoursWord = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TEACHER_IDGroupFooter
            // 
            this.TEACHER_IDGroupFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.29999950528144836D);
            this.TEACHER_IDGroupFooter.Name = "TEACHER_IDGroupFooter";
            this.TEACHER_IDGroupFooter.Style.Visible = false;
            // 
            // tEACHER_IDGroupHeaderSection
            // 
            this.tEACHER_IDGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.70009994506835938D);
            this.tEACHER_IDGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox14});
            this.tEACHER_IDGroupHeaderSection.Name = "tEACHER_IDGroupHeaderSection";
            this.tEACHER_IDGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // textBox14
            // 
            this.textBox14.CanGrow = true;
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.235416412353516D), Telerik.Reporting.Drawing.Unit.Cm(0.69999986886978149D));
            this.textBox14.Style.Font.Bold = true;
            this.textBox14.Style.Font.Name = "Verdana";
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox14.StyleName = "Caption";
            this.textBox14.Value = "øÒÂÚ ≈ÈÛÁ„ﬁÛÂ˘Ì/≈ÈÙÁÒﬁÛÂ˘Ì";
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.29999980330467224D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.59989988803863525D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox,
            this.textBox15,
            this.textBox16});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox
            // 
            this.ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox.CanGrow = true;
            this.ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052922315895557404D), Telerik.Reporting.Drawing.Unit.Cm(0.00019863128545694053D));
            this.ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox.Name = "ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox";
            this.ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.347076416015625D), Telerik.Reporting.Drawing.Unit.Cm(0.59970134496688843D));
            this.ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox.Style.Font.Bold = true;
            this.ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox.Style.Font.Name = "Tahoma";
            this.ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox.StyleName = "Caption";
            this.ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox.Value = "–≈—…√—¡÷«";
            // 
            // textBox15
            // 
            this.textBox15.CanGrow = true;
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.4001989364624023D), Telerik.Reporting.Drawing.Unit.Cm(0.00019863128545694053D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.6074347496032715D), Telerik.Reporting.Drawing.Unit.Cm(0.59970134496688843D));
            this.textBox15.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox15.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox15.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox15.Style.Font.Bold = true;
            this.textBox15.Style.Font.Name = "Tahoma";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox15.StyleName = "Caption";
            this.textBox15.Value = "Ÿ—≈” (ÔÎÔ„Ò‹ˆ˘Ú)";
            // 
            // textBox16
            // 
            this.textBox16.CanGrow = true;
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.007833480834961D), Telerik.Reporting.Drawing.Unit.Cm(0.00019863128545694053D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2804007530212402D), Telerik.Reporting.Drawing.Unit.Cm(0.59970134496688843D));
            this.textBox16.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox16.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox16.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox16.Style.Font.Bold = true;
            this.textBox16.Style.Font.Name = "Tahoma";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox16.StyleName = "Caption";
            this.textBox16.Value = "Ÿ—≈” (·ÒÈËÏÁÙÈÍ˛Ú)";
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "Proteus.Properties.Settings.DBConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.SelectCommand = "SELECT       …≈ , TEACHER_ID, LESSON_DESC, Ÿ—≈”_≈≈\r\nFROM            _abp≈ –¡…ƒ≈’‘" +
    "«”_œÀ… œ_≈…”«√«”«";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.39999613165855408D);
            this.pageFooter.Name = "pageFooter";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60000050067901611D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.Ï«Õ¡”_≈‘œ”DataTextBox,
            this.fieldTotalHours,
            this.txtHoursWord});
            this.detail.KeepTogether = false;
            this.detail.Name = "detail";
            this.detail.ItemDataBound += new System.EventHandler(this.detail_ItemDataBound);
            // 
            // Ï«Õ¡”_≈‘œ”DataTextBox
            // 
            this.Ï«Õ¡”_≈‘œ”DataTextBox.CanGrow = true;
            this.Ï«Õ¡”_≈‘œ”DataTextBox.CanShrink = false;
            this.Ï«Õ¡”_≈‘œ”DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.0529198944568634D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.Ï«Õ¡”_≈‘œ”DataTextBox.Name = "Ï«Õ¡”_≈‘œ”DataTextBox";
            this.Ï«Õ¡”_≈‘œ”DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.34708023071289D), Telerik.Reporting.Drawing.Unit.Cm(0.60000050067901611D));
            this.Ï«Õ¡”_≈‘œ”DataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.Ï«Õ¡”_≈‘œ”DataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.Ï«Õ¡”_≈‘œ”DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.Ï«Õ¡”_≈‘œ”DataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.Ï«Õ¡”_≈‘œ”DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Ï«Õ¡”_≈‘œ”DataTextBox.StyleName = "Data";
            this.Ï«Õ¡”_≈‘œ”DataTextBox.Value = "= Fields.LESSON_DESC";
            // 
            // fieldTotalHours
            // 
            this.fieldTotalHours.CanGrow = true;
            this.fieldTotalHours.CanShrink = false;
            this.fieldTotalHours.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.007833480834961D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.fieldTotalHours.Name = "fieldTotalHours";
            this.fieldTotalHours.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2804982662200928D), Telerik.Reporting.Drawing.Unit.Cm(0.59980028867721558D));
            this.fieldTotalHours.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.fieldTotalHours.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.fieldTotalHours.Style.Font.Bold = true;
            this.fieldTotalHours.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.fieldTotalHours.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.fieldTotalHours.StyleName = "Data";
            this.fieldTotalHours.Value = "= Fields.Ÿ—≈”_≈≈";
            // 
            // txtHoursWord
            // 
            this.txtHoursWord.CanGrow = true;
            this.txtHoursWord.CanShrink = false;
            this.txtHoursWord.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.4001998901367188D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.txtHoursWord.Name = "txtHoursWord";
            this.txtHoursWord.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.6074328422546387D), Telerik.Reporting.Drawing.Unit.Cm(0.59980028867721558D));
            this.txtHoursWord.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtHoursWord.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.txtHoursWord.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.txtHoursWord.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtHoursWord.StyleName = "Data";
            this.txtHoursWord.Value = "";
            // 
            // _TeacherBebeosiExamsTotal
            // 
            this.DataSource = this.sqlDataSource1;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.TEACHER_ID", Telerik.Reporting.FilterOperator.Equal, "=Parameters.teacherID.Value"));
            group1.GroupFooter = this.TEACHER_IDGroupFooter;
            group1.GroupHeader = this.tEACHER_IDGroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.TEACHER_ID"));
            group1.Name = "tEACHER_IDGroup";
            group2.GroupFooter = this.labelsGroupFooterSection;
            group2.GroupHeader = this.labelsGroupHeaderSection;
            group2.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.tEACHER_IDGroupHeaderSection,
            this.TEACHER_IDGroupFooter,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageFooter,
            this.detail});
            this.Name = "_TeacherBebeosiExamsTotal";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(15D), Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(15D), Telerik.Reporting.Drawing.Unit.Mm(10D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlDataSource1;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.TEACHER_ID";
            reportParameter1.AvailableValues.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.TEACHER_ID", Telerik.Reporting.SortDirection.Asc));
            reportParameter1.AvailableValues.ValueMember = "= Fields.TEACHER_ID";
            reportParameter1.Name = "teacherID";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            this.ReportParameters.Add(reportParameter1);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule1.Style.Color = System.Drawing.Color.Black;
            styleRule1.Style.Font.Bold = true;
            styleRule1.Style.Font.Italic = false;
            styleRule1.Style.Font.Name = "Tahoma";
            styleRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule1.Style.Font.Strikeout = false;
            styleRule1.Style.Font.Underline = false;
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(18.341251373291016D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.GroupHeaderSection tEACHER_IDGroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection TEACHER_IDGroupFooter;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox ÔÕœÃ¡‘≈–ŸÕ’ÃœCaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox Ï«Õ¡”_≈‘œ”DataTextBox;
        private Telerik.Reporting.TextBox fieldTotalHours;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox txtHoursWord;
        private Telerik.Reporting.TextBox textBox14;

    }
}