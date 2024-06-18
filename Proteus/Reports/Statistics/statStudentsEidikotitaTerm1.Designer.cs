namespace Proteus.Reports.Statistics
{
    partial class statStudentsEidikotitaTerm1
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
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.sqlDataSource = new Telerik.Reporting.SqlDataSource();
            this.sY_TEXTGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.sY_TEXTGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.eIDIKOTITA_TEXTCountFunctionTextBox = new Telerik.Reporting.TextBox();
            this.À«»œ”SumFunctionTextBox = new Telerik.Reporting.TextBox();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.eIDIKOTITA_TEXTCaptionTextBox = new Telerik.Reporting.TextBox();
            this.À«»œ”CaptionTextBox = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.subReport1 = new Telerik.Reporting.SubReport();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.sqlSchoolYears = new Telerik.Reporting.SqlDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource
            // 
            this.sqlDataSource.ConnectionString = "Proteus.Properties.Settings.DBConnectionString";
            this.sqlDataSource.Name = "sqlDataSource";
            this.sqlDataSource.SelectCommand = "SELECT        ”◊œÀ… œ_≈‘œ”, ≈…ƒ… œ‘«‘¡_ Ÿƒ, SY_TEXT, EIDIKOTITA_TEXT, –À«»œ”\r\nFRO" +
    "M            viewStudentsEidikotitaTerm1\r\nORDER BY ”◊œÀ… œ_≈‘œ”, –À«»œ” DESC, EI" +
    "DIKOTITA_TEXT";
            // 
            // sY_TEXTGroupHeaderSection
            // 
            this.sY_TEXTGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.70000046491622925D);
            this.sY_TEXTGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2,
            this.textBox3});
            this.sY_TEXTGroupHeaderSection.KeepTogether = true;
            this.sY_TEXTGroupHeaderSection.Name = "sY_TEXTGroupHeaderSection";
            this.sY_TEXTGroupHeaderSection.PageBreak = Telerik.Reporting.PageBreak.None;
            this.sY_TEXTGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // sY_TEXTGroupFooterSection
            // 
            this.sY_TEXTGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.995999813079834D);
            this.sY_TEXTGroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.eIDIKOTITA_TEXTCountFunctionTextBox,
            this.À«»œ”SumFunctionTextBox});
            this.sY_TEXTGroupFooterSection.Name = "sY_TEXTGroupFooterSection";
            // 
            // eIDIKOTITA_TEXTCountFunctionTextBox
            // 
            this.eIDIKOTITA_TEXTCountFunctionTextBox.CanGrow = true;
            this.eIDIKOTITA_TEXTCountFunctionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.19599901139736176D));
            this.eIDIKOTITA_TEXTCountFunctionTextBox.Name = "eIDIKOTITA_TEXTCountFunctionTextBox";
            this.eIDIKOTITA_TEXTCountFunctionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.146882057189941D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.eIDIKOTITA_TEXTCountFunctionTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.eIDIKOTITA_TEXTCountFunctionTextBox.Style.Font.Bold = true;
            this.eIDIKOTITA_TEXTCountFunctionTextBox.StyleName = "Data";
            this.eIDIKOTITA_TEXTCountFunctionTextBox.Value = "= \"”’ÕœÀœ ”–œ’ƒ¡”‘ŸÕ ¡\' ≈Œ¡Ã«Õœ’ √…¡ ‘œ ”◊œÀ… œ ≈‘œ” \" + Fields.SY_TEXT";
            // 
            // À«»œ”SumFunctionTextBox
            // 
            this.À«»œ”SumFunctionTextBox.CanGrow = true;
            this.À«»œ”SumFunctionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.19999885559082D), Telerik.Reporting.Drawing.Unit.Cm(0.19599901139736176D));
            this.À«»œ”SumFunctionTextBox.Name = "À«»œ”SumFunctionTextBox";
            this.À«»œ”SumFunctionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.6412521600723267D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.À«»œ”SumFunctionTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.À«»œ”SumFunctionTextBox.Style.Font.Bold = true;
            this.À«»œ”SumFunctionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.À«»œ”SumFunctionTextBox.StyleName = "Data";
            this.À«»œ”SumFunctionTextBox.Value = "=Sum(Fields.–À«»œ”)";
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.64708375930786133D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.eIDIKOTITA_TEXTCaptionTextBox,
            this.À«»œ”CaptionTextBox,
            this.textBox4});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.499799907207489D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // eIDIKOTITA_TEXTCaptionTextBox
            // 
            this.eIDIKOTITA_TEXTCaptionTextBox.CanGrow = true;
            this.eIDIKOTITA_TEXTCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.eIDIKOTITA_TEXTCaptionTextBox.Name = "eIDIKOTITA_TEXTCaptionTextBox";
            this.eIDIKOTITA_TEXTCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(14.199797630310059D), Telerik.Reporting.Drawing.Unit.Cm(0.64708375930786133D));
            this.eIDIKOTITA_TEXTCaptionTextBox.Style.Font.Bold = true;
            this.eIDIKOTITA_TEXTCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.eIDIKOTITA_TEXTCaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.eIDIKOTITA_TEXTCaptionTextBox.StyleName = "Caption";
            this.eIDIKOTITA_TEXTCaptionTextBox.Value = "≈…ƒ… œ‘«‘¡  ¡‘¡—‘…”«”";
            // 
            // À«»œ”CaptionTextBox
            // 
            this.À«»œ”CaptionTextBox.CanGrow = true;
            this.À«»œ”CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.199999809265137D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.À«»œ”CaptionTextBox.Name = "À«»œ”CaptionTextBox";
            this.À«»œ”CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.6412489414215088D), Telerik.Reporting.Drawing.Unit.Cm(0.64708298444747925D));
            this.À«»œ”CaptionTextBox.Style.Font.Bold = true;
            this.À«»œ”CaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.À«»œ”CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.À«»œ”CaptionTextBox.StyleName = "Caption";
            this.À«»œ”CaptionTextBox.Value = "–À«»œ”";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.56812465190887451D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox7,
            this.pageInfoTextBox});
            this.pageFooter.Name = "pageFooter";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(4.5D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.subReport1,
            this.textBox1});
            this.reportHeader.Name = "reportHeader";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.657116711139679D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.textBox5,
            this.textBox8});
            this.detail.Name = "detail";
            // 
            // subReport1
            // 
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.subReport1.Name = "subReport1";
            typeReportSource1.TypeName = "Proteus.Reports.LogoA2, Proteus, Version=1.0.0.0, Culture=neutral, PublicKeyToken" +
    "=null";
            this.subReport1.ReportSource = typeReportSource1;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.6470837593078613D), Telerik.Reporting.Drawing.Unit.Cm(3.5999999046325684D));
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(3.6001999378204346D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.788333892822266D), Telerik.Reporting.Drawing.Unit.Cm(0.89980006217956543D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "–À«»œ” ”–œ’ƒ¡”‘ŸÕ …≈  - ƒ’–¡ ¡\' ≈Œ¡Ã«Õœ’ ¡Õ¡ ≈…ƒ… œ‘«‘¡  ¡‘¡—‘…”«”";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.042917277663946152D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.3570826053619385D), Telerik.Reporting.Drawing.Unit.Cm(0.69990003108978271D));
            this.textBox2.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.StyleName = "Caption";
            this.textBox2.Value = "”◊œÀ… œ ≈‘œ” :";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.4002001285552979D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.441049575805664D), Telerik.Reporting.Drawing.Unit.Cm(0.69990003108978271D));
            this.textBox3.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.textBox3.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.StyleName = "Data";
            this.textBox3.Value = "= Fields.SY_TEXT";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.3677082061767578D), Telerik.Reporting.Drawing.Unit.Cm(0.54553234577178955D));
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox7.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.textBox7.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.textBox7.StyleName = "PageInfo";
            this.textBox7.Value = "≈ˆ·ÒÏÔ„ﬁ Proteus - ÷ıÛÈÍ¸ ¡ÌÙÈÍÂﬂÏÂÌÔ …≈ ";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.4208250045776367D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.4204225540161133D), Telerik.Reporting.Drawing.Unit.Cm(0.54563242197036743D));
            this.pageInfoTextBox.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=\"”ÂÎ. \" + PageNumber + \"/\" + PageCount";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.9468834400177002D), Telerik.Reporting.Drawing.Unit.Cm(0.64708298444747925D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.StyleName = "Caption";
            this.textBox4.Value = "¡/¡";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.0042011323384940624D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.94688338041305542D), Telerik.Reporting.Drawing.Unit.Cm(0.65291553735733032D));
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "=RowNumber()";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(14.199798583984375D), Telerik.Reporting.Drawing.Unit.Cm(0.65291553735733032D));
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox5.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "= Fields.EIDIKOTITA_TEXT";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.200000762939453D), Telerik.Reporting.Drawing.Unit.Cm(0.0042011323384940624D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.6412482261657715D), Telerik.Reporting.Drawing.Unit.Cm(0.65291553735733032D));
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "= Fields.–À«»œ”";
            // 
            // sqlSchoolYears
            // 
            this.sqlSchoolYears.ConnectionString = "Proteus.Properties.Settings.DBConnectionString";
            this.sqlSchoolYears.Name = "sqlSchoolYears";
            this.sqlSchoolYears.SelectCommand = "SELECT        SY_ID, SY_TEXT\r\nFROM            SYS_SCHOOLYEARS\r\nWHERE        (SY_I" +
    "D >= 2046)";
            // 
            // statStudentsEidikotitaTerm1
            // 
            this.DataSource = this.sqlDataSource;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.”◊œÀ… œ_≈‘œ”", Telerik.Reporting.FilterOperator.In, "=Parameters.school_year.Value"));
            group1.GroupFooter = this.sY_TEXTGroupFooterSection;
            group1.GroupHeader = this.sY_TEXTGroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.SY_TEXT"));
            group1.Name = "sY_TEXTGroup";
            group2.GroupFooter = this.labelsGroupFooterSection;
            group2.GroupHeader = this.labelsGroupHeaderSection;
            group2.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.sY_TEXTGroupHeaderSection,
            this.sY_TEXTGroupFooterSection,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageFooter,
            this.reportHeader,
            this.detail});
            this.Name = "statStudentsEidikotitaTerm1";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlSchoolYears;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.SY_TEXT";
            reportParameter1.AvailableValues.ValueMember = "= Fields.SY_ID";
            reportParameter1.MultiValue = true;
            reportParameter1.Name = "school_year";
            reportParameter1.Text = "”˜ÔÎÈÍ¸ ›ÙÔÚ";
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(17D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource;
        private Telerik.Reporting.GroupHeaderSection sY_TEXTGroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection sY_TEXTGroupFooterSection;
        private Telerik.Reporting.TextBox eIDIKOTITA_TEXTCountFunctionTextBox;
        private Telerik.Reporting.TextBox À«»œ”SumFunctionTextBox;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox eIDIKOTITA_TEXTCaptionTextBox;
        private Telerik.Reporting.TextBox À«»œ”CaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.SubReport subReport1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.SqlDataSource sqlSchoolYears;

    }
}