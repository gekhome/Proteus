using Proteus.DAL;
using Proteus.Models;

namespace Proteus.Reports.Programma
{
    partial class TeacherBebeosiLessons
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
            Telerik.Reporting.Group group3 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.�������_����GroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.�������_����GroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.TEACHER_IDGroupFooter = new Telerik.Reporting.GroupFooterSection();
            this.tEACHER_IDGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.txtSumHours = new Telerik.Reporting.TextBox();
            this.txtTotalHoursWord = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.�������������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.sqlDataSourceSchoolYears = new Telerik.Reporting.SqlDataSource();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.�����_����DataTextBox = new Telerik.Reporting.TextBox();
            this.fieldTotalHours = new Telerik.Reporting.TextBox();
            this.txtHoursWord = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // �������_����GroupFooterSection
            // 
            this.�������_����GroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.30000105500221252D);
            this.�������_����GroupFooterSection.Name = "�������_����GroupFooterSection";
            // 
            // �������_����GroupHeaderSection
            // 
            this.�������_����GroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.30000001192092896D);
            this.�������_����GroupHeaderSection.Name = "�������_����GroupHeaderSection";
            this.�������_����GroupHeaderSection.PrintOnEveryPage = false;
            this.�������_����GroupHeaderSection.Style.Visible = false;
            // 
            // TEACHER_IDGroupFooter
            // 
            this.TEACHER_IDGroupFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.29999950528144836D);
            this.TEACHER_IDGroupFooter.Name = "TEACHER_IDGroupFooter";
            this.TEACHER_IDGroupFooter.Style.Visible = false;
            // 
            // tEACHER_IDGroupHeaderSection
            // 
            this.tEACHER_IDGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.89999961853027344D);
            this.tEACHER_IDGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox14});
            this.tEACHER_IDGroupHeaderSection.Name = "tEACHER_IDGroupHeaderSection";
            this.tEACHER_IDGroupHeaderSection.PrintOnEveryPage = false;
            // 
            // textBox14
            // 
            this.textBox14.CanGrow = true;
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.235416412353516D), Telerik.Reporting.Drawing.Unit.Cm(0.89999794960021973D));
            this.textBox14.Style.Font.Name = "Verdana";
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox14.StyleName = "Caption";
            this.textBox14.Value = "�� ���� ��� ����������������� ��� ������ ���������� �� ����:";
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D);
            this.labelsGroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtSumHours,
            this.txtTotalHoursWord,
            this.textBox17});
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = true;
            // 
            // txtSumHours
            // 
            this.txtSumHours.CanGrow = true;
            this.txtSumHours.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.007833480834961D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.txtSumHours.Name = "txtSumHours";
            this.txtSumHours.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2804994583129883D), Telerik.Reporting.Drawing.Unit.Cm(0.59979987144470215D));
            this.txtSumHours.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.txtSumHours.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtSumHours.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.txtSumHours.Style.Font.Bold = true;
            this.txtSumHours.Style.Font.Name = "Verdana";
            this.txtSumHours.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.txtSumHours.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtSumHours.StyleName = "Caption";
            this.txtSumHours.Value = "= Sum(Fields.����)";
            this.txtSumHours.ItemDataBound += new System.EventHandler(this.txtSumHours_ItemDataBound);
            // 
            // txtTotalHoursWord
            // 
            this.txtTotalHoursWord.CanGrow = true;
            this.txtTotalHoursWord.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.700000762939453D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.txtTotalHoursWord.Name = "txtTotalHoursWord";
            this.txtTotalHoursWord.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.3076324462890625D), Telerik.Reporting.Drawing.Unit.Cm(0.59979987144470215D));
            this.txtTotalHoursWord.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.txtTotalHoursWord.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtTotalHoursWord.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.txtTotalHoursWord.Style.Font.Bold = true;
            this.txtTotalHoursWord.Style.Font.Name = "Verdana";
            this.txtTotalHoursWord.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.txtTotalHoursWord.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtTotalHoursWord.StyleName = "Data";
            this.txtTotalHoursWord.Value = "";
            // 
            // textBox17
            // 
            this.textBox17.CanGrow = true;
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.0529198944568634D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.646880149841309D), Telerik.Reporting.Drawing.Unit.Cm(0.59979987144470215D));
            this.textBox17.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox17.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox17.Style.Font.Bold = true;
            this.textBox17.Style.Font.Name = "Verdana";
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox17.StyleName = "Caption";
            this.textBox17.Value = "������";
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.599998414516449D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.�������������CaptionTextBox,
            this.textBox15,
            this.textBox16});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // �������������CaptionTextBox
            // 
            this.�������������CaptionTextBox.CanGrow = true;
            this.�������������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052922315895557404D), Telerik.Reporting.Drawing.Unit.Cm(0.00019863128545694053D));
            this.�������������CaptionTextBox.Name = "�������������CaptionTextBox";
            this.�������������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.646876335144043D), Telerik.Reporting.Drawing.Unit.Cm(0.59979987144470215D));
            this.�������������CaptionTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.�������������CaptionTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.�������������CaptionTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.�������������CaptionTextBox.Style.Font.Bold = true;
            this.�������������CaptionTextBox.Style.Font.Name = "Tahoma";
            this.�������������CaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.�������������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.�������������CaptionTextBox.StyleName = "Caption";
            this.�������������CaptionTextBox.Value = "������";
            // 
            // textBox15
            // 
            this.textBox15.CanGrow = true;
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.699999809265137D), Telerik.Reporting.Drawing.Unit.Cm(0.00019863128545694053D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.3076343536376953D), Telerik.Reporting.Drawing.Unit.Cm(0.59979987144470215D));
            this.textBox15.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox15.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox15.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox15.Style.Font.Bold = true;
            this.textBox15.Style.Font.Name = "Tahoma";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox15.StyleName = "Caption";
            this.textBox15.Value = "���� (���������)";
            // 
            // textBox16
            // 
            this.textBox16.CanGrow = true;
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.007833480834961D), Telerik.Reporting.Drawing.Unit.Cm(0.00019863128545694053D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2804007530212402D), Telerik.Reporting.Drawing.Unit.Cm(0.59979987144470215D));
            this.textBox16.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox16.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox16.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox16.Style.Font.Bold = true;
            this.textBox16.Style.Font.Name = "Tahoma";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox16.StyleName = "Caption";
            this.textBox16.Value = "���� (�����������)";
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "Proteus.Properties.Settings.DBConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.SelectCommand = "SELECT       �������_����, ���, TEACHER_ID, LESSON_DESC, ����\r\nFROM            ab" +
    "p�����������_�������_������";
            // 
            // sqlDataSourceSchoolYears
            // 
            this.sqlDataSourceSchoolYears.ConnectionString = "Proteus.Properties.Settings.DBConnectionString";
            this.sqlDataSourceSchoolYears.Name = "sqlDataSourceSchoolYears";
            this.sqlDataSourceSchoolYears.SelectCommand = "SELECT        SY_ID, SY_TEXT\r\nFROM            SYS_SCHOOLYEARS\r\nORDER BY SY_TEXT";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.39999613165855408D);
            this.pageFooter.Name = "pageFooter";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.80000168085098267D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.�����_����DataTextBox,
            this.fieldTotalHours,
            this.txtHoursWord});
            this.detail.KeepTogether = false;
            this.detail.Name = "detail";
            this.detail.ItemDataBound += new System.EventHandler(this.detail_ItemDataBound);
            // 
            // �����_����DataTextBox
            // 
            this.�����_����DataTextBox.CanGrow = true;
            this.�����_����DataTextBox.CanShrink = false;
            this.�����_����DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.0529198944568634D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.�����_����DataTextBox.Name = "�����_����DataTextBox";
            this.�����_����DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.646880149841309D), Telerik.Reporting.Drawing.Unit.Cm(0.80000162124633789D));
            this.�����_����DataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.�����_����DataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.�����_����DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.�����_����DataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.�����_����DataTextBox.StyleName = "Data";
            this.�����_����DataTextBox.Value = "= Fields.LESSON_DESC";
            // 
            // fieldTotalHours
            // 
            this.fieldTotalHours.CanGrow = true;
            this.fieldTotalHours.CanShrink = false;
            this.fieldTotalHours.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.007833480834961D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.fieldTotalHours.Name = "fieldTotalHours";
            this.fieldTotalHours.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2804982662200928D), Telerik.Reporting.Drawing.Unit.Cm(0.79980140924453735D));
            this.fieldTotalHours.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.fieldTotalHours.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.fieldTotalHours.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.fieldTotalHours.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.fieldTotalHours.StyleName = "Data";
            this.fieldTotalHours.Value = "= Fields.����";
            // 
            // txtHoursWord
            // 
            this.txtHoursWord.CanGrow = true;
            this.txtHoursWord.CanShrink = false;
            this.txtHoursWord.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.700000762939453D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.txtHoursWord.Name = "txtHoursWord";
            this.txtHoursWord.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.3076324462890625D), Telerik.Reporting.Drawing.Unit.Cm(0.79980140924453735D));
            this.txtHoursWord.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.txtHoursWord.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.txtHoursWord.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.txtHoursWord.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtHoursWord.StyleName = "Data";
            this.txtHoursWord.Value = "";
            // 
            // TeacherBebeosiLessons
            // 
            this.DataSource = this.sqlDataSource1;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.TEACHER_ID", Telerik.Reporting.FilterOperator.Equal, "=Parameters.teacherID.Value"));
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.�������_����", Telerik.Reporting.FilterOperator.Equal, "=Parameters.schoolyearID.Value"));
            group1.GroupFooter = this.�������_����GroupFooterSection;
            group1.GroupHeader = this.�������_����GroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.�������_����"));
            group1.Name = "�������_����Group";
            group2.GroupFooter = this.TEACHER_IDGroupFooter;
            group2.GroupHeader = this.tEACHER_IDGroupHeaderSection;
            group2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.TEACHER_ID"));
            group2.Name = "tEACHER_IDGroup";
            group3.GroupFooter = this.labelsGroupFooterSection;
            group3.GroupHeader = this.labelsGroupHeaderSection;
            group3.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2,
            group3});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.�������_����GroupHeaderSection,
            this.�������_����GroupFooterSection,
            this.tEACHER_IDGroupHeaderSection,
            this.TEACHER_IDGroupFooter,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageFooter,
            this.detail});
            this.Name = "TeacherBebeosiLessons";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(15D), Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(15D), Telerik.Reporting.Drawing.Unit.Mm(10D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlDataSource1;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.TEACHER_ID";
            reportParameter1.AvailableValues.ValueMember = "= Fields.TEACHER_ID";
            reportParameter1.Name = "teacherID";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter2.AllowNull = true;
            reportParameter2.AutoRefresh = true;
            reportParameter2.AvailableValues.DataSource = this.sqlDataSourceSchoolYears;
            reportParameter2.AvailableValues.DisplayMember = "= Fields.SY_TEXT";
            reportParameter2.AvailableValues.ValueMember = "= Fields.SY_ID";
            reportParameter2.Name = "schoolyearID";
            reportParameter2.Text = "������� ����";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
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
        private Telerik.Reporting.GroupHeaderSection �������_����GroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection �������_����GroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection tEACHER_IDGroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection TEACHER_IDGroupFooter;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox �������������CaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox �����_����DataTextBox;
        private Telerik.Reporting.TextBox fieldTotalHours;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox txtHoursWord;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox txtSumHours;
        private Telerik.Reporting.TextBox txtTotalHoursWord;
        private Telerik.Reporting.SqlDataSource sqlDataSourceSchoolYears;

    }
}