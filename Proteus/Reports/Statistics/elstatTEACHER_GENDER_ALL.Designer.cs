namespace Proteus.Reports.Statistics
{
    partial class elstatTEACHER_GENDER_ALL
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TypeReportSource typeReportSource1 = new Telerik.Reporting.TypeReportSource();
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group3 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.sY_TEXTGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.sY_TEXTGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.subReport1 = new Telerik.Reporting.SubReport();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.sY_TEXTDataTextBox = new Telerik.Reporting.TextBox();
            this.sY_TEXTCaptionTextBox = new Telerik.Reporting.TextBox();
            this.����GroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.�����CountFunctionTextBox2 = new Telerik.Reporting.TextBox();
            this.������SumFunctionTextBox2 = new Telerik.Reporting.TextBox();
            this.����GroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.����CaptionTextBox = new Telerik.Reporting.TextBox();
            this.����DataTextBox = new Telerik.Reporting.TextBox();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.�����CaptionTextBox = new Telerik.Reporting.TextBox();
            this.sqlDataSourceSchoolYears = new Telerik.Reporting.SqlDataSource();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.�����DataTextBox = new Telerik.Reporting.TextBox();
            this.������DataTextBox = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sY_TEXTGroupFooterSection
            // 
            this.sY_TEXTGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.69999885559082031D);
            this.sY_TEXTGroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox5,
            this.textBox6});
            this.sY_TEXTGroupFooterSection.Name = "sY_TEXTGroupFooterSection";
            this.sY_TEXTGroupFooterSection.Style.BackgroundColor = System.Drawing.Color.LightGray;
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.10583332926034927D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(12.194164276123047D), Telerik.Reporting.Drawing.Unit.Cm(0.64708214998245239D));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox5.StyleName = "Data";
            this.textBox5.Value = "=\"�������� ������ ��� �� ����. ���� : \" + SY_TEXT";
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = true;
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.300195693969727D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.788330078125D), Telerik.Reporting.Drawing.Unit.Cm(0.64708214998245239D));
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.StyleName = "Data";
            this.textBox6.Value = "=Sum(Fields.������)";
            // 
            // sY_TEXTGroupHeaderSection
            // 
            this.sY_TEXTGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(6.1000003814697266D);
            this.sY_TEXTGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.subReport1,
            this.textBox2,
            this.sY_TEXTDataTextBox,
            this.sY_TEXTCaptionTextBox});
            this.sY_TEXTGroupHeaderSection.Name = "sY_TEXTGroupHeaderSection";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.8106250762939453D), Telerik.Reporting.Drawing.Unit.Cm(3.0957248210906982D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.9777073860168457D), Telerik.Reporting.Drawing.Unit.Cm(0.79999995231628418D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Calibri";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "���������� �������� ��� ������";
            // 
            // subReport1
            // 
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.subReport1.Name = "subReport1";
            typeReportSource1.TypeName = "Proteus.Reports.LogoA2, Proteus, Version=1.0.0.0, Culture=neutral, PublicKeyToken" +
    "=null";
            this.subReport1.ReportSource = typeReportSource1;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.7470827102661133D), Telerik.Reporting.Drawing.Unit.Cm(3.899899959564209D));
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(4.5D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.735414505004883D), Telerik.Reporting.Drawing.Unit.Cm(0.59999966621398926D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Calibri";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(13D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.StyleName = "Caption";
            this.textBox2.Value = "�������� ������� ����������ʿ� ��� ��� ������� ����";
            // 
            // sY_TEXTDataTextBox
            // 
            this.sY_TEXTDataTextBox.CanGrow = true;
            this.sY_TEXTDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.547283411026001D), Telerik.Reporting.Drawing.Unit.Cm(5.5D));
            this.sY_TEXTDataTextBox.Name = "sY_TEXTDataTextBox";
            this.sY_TEXTDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0527167320251465D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.sY_TEXTDataTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.sY_TEXTDataTextBox.Style.Font.Bold = true;
            this.sY_TEXTDataTextBox.Style.Font.Name = "Calibri";
            this.sY_TEXTDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.sY_TEXTDataTextBox.StyleName = "Data";
            this.sY_TEXTDataTextBox.Value = "=Fields.SY_TEXT";
            // 
            // sY_TEXTCaptionTextBox
            // 
            this.sY_TEXTCaptionTextBox.CanGrow = true;
            this.sY_TEXTCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(5.5D));
            this.sY_TEXTCaptionTextBox.Name = "sY_TEXTCaptionTextBox";
            this.sY_TEXTCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.5470831394195557D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.sY_TEXTCaptionTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.sY_TEXTCaptionTextBox.Style.Font.Bold = true;
            this.sY_TEXTCaptionTextBox.Style.Font.Name = "Calibri";
            this.sY_TEXTCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.sY_TEXTCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.sY_TEXTCaptionTextBox.StyleName = "Caption";
            this.sY_TEXTCaptionTextBox.Value = "������� ����:";
            // 
            // ����GroupFooterSection
            // 
            this.����GroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.96770858764648438D);
            this.����GroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.�����CountFunctionTextBox2,
            this.������SumFunctionTextBox2});
            this.����GroupFooterSection.Name = "����GroupFooterSection";
            // 
            // �����CountFunctionTextBox2
            // 
            this.�����CountFunctionTextBox2.CanGrow = true;
            this.�����CountFunctionTextBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.2585500478744507D), Telerik.Reporting.Drawing.Unit.Cm(0.21156410872936249D));
            this.�����CountFunctionTextBox2.Name = "�����CountFunctionTextBox2";
            this.�����CountFunctionTextBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(11.041447639465332D), Telerik.Reporting.Drawing.Unit.Cm(0.55614453554153442D));
            this.�����CountFunctionTextBox2.Style.Font.Bold = true;
            this.�����CountFunctionTextBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.�����CountFunctionTextBox2.StyleName = "Data";
            this.�����CountFunctionTextBox2.Value = "=\"�������� ������ : \" + GENDER";
            // 
            // ������SumFunctionTextBox2
            // 
            this.������SumFunctionTextBox2.CanGrow = true;
            this.������SumFunctionTextBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.300198554992676D), Telerik.Reporting.Drawing.Unit.Cm(0.21156410872936249D));
            this.������SumFunctionTextBox2.Name = "������SumFunctionTextBox2";
            this.������SumFunctionTextBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.788330078125D), Telerik.Reporting.Drawing.Unit.Cm(0.55614453554153442D));
            this.������SumFunctionTextBox2.Style.Font.Bold = true;
            this.������SumFunctionTextBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.������SumFunctionTextBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������SumFunctionTextBox2.StyleName = "Data";
            this.������SumFunctionTextBox2.Value = "=Sum(Fields.������)";
            // 
            // ����GroupHeaderSection
            // 
            this.����GroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.65301769971847534D);
            this.����GroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.����CaptionTextBox,
            this.����DataTextBox});
            this.����GroupHeaderSection.Name = "����GroupHeaderSection";
            this.����GroupHeaderSection.PrintOnEveryPage = true;
            // 
            // ����CaptionTextBox
            // 
            this.����CaptionTextBox.CanGrow = true;
            this.����CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.2585500478744507D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.����CaptionTextBox.Name = "����CaptionTextBox";
            this.����CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.847083330154419D), Telerik.Reporting.Drawing.Unit.Cm(0.64708375930786133D));
            this.����CaptionTextBox.Style.Font.Bold = true;
            this.����CaptionTextBox.Style.Font.Name = "Calibri";
            this.����CaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.����CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.����CaptionTextBox.StyleName = "Caption";
            this.����CaptionTextBox.Value = "����:";
            // 
            // ����DataTextBox
            // 
            this.����DataTextBox.CanGrow = true;
            this.����DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.1058332920074463D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.����DataTextBox.Name = "����DataTextBox";
            this.����DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.682499885559082D), Telerik.Reporting.Drawing.Unit.Cm(0.64708375930786133D));
            this.����DataTextBox.Style.Font.Bold = true;
            this.����DataTextBox.Style.Font.Name = "Calibri";
            this.����DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.����DataTextBox.StyleName = "Data";
            this.����DataTextBox.Value = "= Fields.GENDER";
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.6413501501083374D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.������CaptionTextBox,
            this.�����CaptionTextBox});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // ������CaptionTextBox
            // 
            this.������CaptionTextBox.CanGrow = true;
            this.������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.353115081787109D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.������CaptionTextBox.Name = "������CaptionTextBox";
            this.������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7883317470550537D), Telerik.Reporting.Drawing.Unit.Cm(0.64125001430511475D));
            this.������CaptionTextBox.Style.Font.Bold = true;
            this.������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������CaptionTextBox.StyleName = "Caption";
            this.������CaptionTextBox.Value = "������";
            // 
            // �����CaptionTextBox
            // 
            this.�����CaptionTextBox.CanGrow = true;
            this.�����CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.�����CaptionTextBox.Name = "�����CaptionTextBox";
            this.�����CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.7529153823852539D), Telerik.Reporting.Drawing.Unit.Cm(0.64125001430511475D));
            this.�����CaptionTextBox.Style.Font.Bold = true;
            this.�����CaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.�����CaptionTextBox.StyleName = "Caption";
            this.�����CaptionTextBox.Value = "����� �������";
            // 
            // sqlDataSourceSchoolYears
            // 
            this.sqlDataSourceSchoolYears.ConnectionString = "Proteus.Properties.Settings.DBConnectionString";
            this.sqlDataSourceSchoolYears.Name = "sqlDataSourceSchoolYears";
            this.sqlDataSourceSchoolYears.SelectCommand = "SELECT        SY_ID, SY_TEXT\r\nFROM            SYS_SCHOOLYEARS\r\nORDER BY SY_TEXT";
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "Proteus.Properties.Settings.DBConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.SelectCommand = "SELECT        ������, �����, GENDER, ����, SY_ID, SY_TEXT\r\nFROM            elstat" +
    "TEACHER_GENDER_ALL\r\nORDER BY SY_TEXT, ����, �����";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.94125187397003174D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.pageInfoTextBox});
            this.pageFooter.Name = "pageFooter";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.294168084859848D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.2054166793823242D), Telerik.Reporting.Drawing.Unit.Cm(0.64708375930786133D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.9206242561340332D), Telerik.Reporting.Drawing.Unit.Cm(0.294168084859848D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.8677082061767578D), Telerik.Reporting.Drawing.Unit.Cm(0.64708375930786133D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=\"���. \" + PageNumber + \" ��� \" + PageCount";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.50563246011734009D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.�����DataTextBox,
            this.������DataTextBox});
            this.detail.Name = "detail";
            // 
            // �����DataTextBox
            // 
            this.�����DataTextBox.CanGrow = true;
            this.�����DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.�����DataTextBox.Name = "�����DataTextBox";
            this.�����DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.7529153823852539D), Telerik.Reporting.Drawing.Unit.Cm(0.50553232431411743D));
            this.�����DataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.�����DataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.�����DataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.�����DataTextBox.StyleName = "Data";
            this.�����DataTextBox.Value = "=Fields.�����";
            // 
            // ������DataTextBox
            // 
            this.������DataTextBox.CanGrow = true;
            this.������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.353115081787109D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.������DataTextBox.Name = "������DataTextBox";
            this.������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7883317470550537D), Telerik.Reporting.Drawing.Unit.Cm(0.50553232431411743D));
            this.������DataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.������DataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.������DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������DataTextBox.StyleName = "Data";
            this.������DataTextBox.Value = "=Fields.������";
            // 
            // elstatTEACHER_GENDER_ALL
            // 
            this.DataSource = this.sqlDataSource1;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.SY_ID", Telerik.Reporting.FilterOperator.Equal, "=Parameters.schoolyearID.Value"));
            group1.GroupFooter = this.sY_TEXTGroupFooterSection;
            group1.GroupHeader = this.sY_TEXTGroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.SY_TEXT"));
            group1.Name = "sY_TEXTGroup";
            group2.GroupFooter = this.����GroupFooterSection;
            group2.GroupHeader = this.����GroupHeaderSection;
            group2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.����"));
            group2.Name = "����Group";
            group3.GroupFooter = this.labelsGroupFooterSection;
            group3.GroupHeader = this.labelsGroupHeaderSection;
            group3.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2,
            group3});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.sY_TEXTGroupHeaderSection,
            this.sY_TEXTGroupFooterSection,
            this.����GroupHeaderSection,
            this.����GroupFooterSection,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageFooter,
            this.detail});
            this.Name = "elstatTEACHER_GENDER_ALL";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(15D), Telerik.Reporting.Drawing.Unit.Mm(10D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlDataSourceSchoolYears;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.SY_TEXT";
            reportParameter1.AvailableValues.ValueMember = "= Fields.SY_ID";
            reportParameter1.Name = "schoolyearID";
            reportParameter1.Text = "������� ����";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter1.Visible = true;
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(16.894166946411133D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.GroupHeaderSection sY_TEXTGroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection sY_TEXTGroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection ����GroupHeaderSection;
        private Telerik.Reporting.TextBox ����CaptionTextBox;
        private Telerik.Reporting.TextBox ����DataTextBox;
        private Telerik.Reporting.GroupFooterSection ����GroupFooterSection;
        private Telerik.Reporting.TextBox �����CountFunctionTextBox2;
        private Telerik.Reporting.TextBox ������SumFunctionTextBox2;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox �����CaptionTextBox;
        private Telerik.Reporting.TextBox ������CaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox �����DataTextBox;
        private Telerik.Reporting.TextBox ������DataTextBox;
        private Telerik.Reporting.SqlDataSource sqlDataSourceSchoolYears;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.SubReport subReport1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox sY_TEXTDataTextBox;
        private Telerik.Reporting.TextBox sY_TEXTCaptionTextBox;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;

    }
}