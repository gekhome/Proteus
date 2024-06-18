namespace Proteus.Reports.Moria
{
    partial class xmAitiseisDaily
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.GraphGroup graphGroup1 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.CategoryScale categoryScale1 = new Telerik.Reporting.CategoryScale();
            Telerik.Reporting.NumericalScale numericalScale1 = new Telerik.Reporting.NumericalScale();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(xmAitiseisDaily));
            Telerik.Reporting.GraphGroup graphGroup2 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.TypeReportSource typeReportSource1 = new Telerik.Reporting.TypeReportSource();
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.���������_��GroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.graph1 = new Telerik.Reporting.Graph();
            this.cartesianCoordinateSystem1 = new Telerik.Reporting.CartesianCoordinateSystem();
            this.graphAxis1 = new Telerik.Reporting.GraphAxis();
            this.graphAxis2 = new Telerik.Reporting.GraphAxis();
            this.sqlDataSource = new Telerik.Reporting.SqlDataSource();
            this.lineSeries1 = new Telerik.Reporting.LineSeries();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.���������_��GroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.����_����CaptionTextBox = new Telerik.Reporting.TextBox();
            this.����_����DataTextBox = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.sqlEgyklioi = new Telerik.Reporting.SqlDataSource();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.subReport1 = new Telerik.Reporting.SubReport();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // ���������_��GroupFooterSection
            // 
            this.���������_��GroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(13.432622909545898D);
            this.���������_��GroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.graph1,
            this.textBox4});
            this.���������_��GroupFooterSection.KeepTogether = false;
            this.���������_��GroupFooterSection.Name = "���������_��GroupFooterSection";
            this.���������_��GroupFooterSection.Style.BackgroundColor = System.Drawing.Color.LightGray;
            // 
            // graph1
            // 
            graphGroup1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.AITISI_DATE"));
            graphGroup1.Label = "= Format(\'{0:dd/MM}\', Fields.AITISI_DATE)";
            graphGroup1.Name = "categoryGroup";
            graphGroup1.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.AITISI_DATE", Telerik.Reporting.SortDirection.Asc));
            this.graph1.CategoryGroups.Add(graphGroup1);
            this.graph1.CoordinateSystems.Add(this.cartesianCoordinateSystem1);
            this.graph1.DataSource = this.sqlDataSource;
            this.graph1.Filters.Add(new Telerik.Reporting.Filter("=Fields.���������_���", Telerik.Reporting.FilterOperator.Equal, "=Parameters.egykliosID.Value"));
            this.graph1.Legend.IsInsidePlotArea = true;
            this.graph1.Legend.Position = Telerik.Reporting.GraphItemPosition.TopCenter;
            this.graph1.Legend.Style.LineColor = System.Drawing.Color.LightGray;
            this.graph1.Legend.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.graph1.Legend.Style.Visible = true;
            this.graph1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.30000004172325134D), Telerik.Reporting.Drawing.Unit.Cm(0.33262342214584351D));
            this.graph1.Name = "graph1";
            this.graph1.PlotAreaStyle.LineColor = System.Drawing.Color.LightGray;
            this.graph1.PlotAreaStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.graph1.Series.Add(this.lineSeries1);
            this.graph1.SeriesGroups.Add(graphGroup2);
            this.graph1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(26.100000381469727D), Telerik.Reporting.Drawing.Unit.Cm(12.452613830566406D));
            this.graph1.Style.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            // 
            // cartesianCoordinateSystem1
            // 
            this.cartesianCoordinateSystem1.Name = "cartesianCoordinateSystem1";
            this.cartesianCoordinateSystem1.XAxis = this.graphAxis1;
            this.cartesianCoordinateSystem1.YAxis = this.graphAxis2;
            // 
            // graphAxis1
            // 
            this.graphAxis1.LabelAngle = 70;
            this.graphAxis1.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis1.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis1.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis1.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis1.MinorGridLineStyle.Visible = false;
            this.graphAxis1.Name = "GraphAxis1";
            this.graphAxis1.Scale = categoryScale1;
            this.graphAxis1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.graphAxis1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.graphAxis1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            // 
            // graphAxis2
            // 
            this.graphAxis2.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.Visible = false;
            this.graphAxis2.MinorTickMarkDisplayType = Telerik.Reporting.GraphAxisTickMarkDisplayType.Inside;
            this.graphAxis2.Name = "GraphAxis2";
            numericalScale1.Minimum = 0D;
            this.graphAxis2.Scale = numericalScale1;
            // 
            // sqlDataSource
            // 
            this.sqlDataSource.ConnectionString = "Proteus.Properties.Settings.DBConnectionString";
            this.sqlDataSource.Name = "sqlDataSource";
            this.sqlDataSource.SelectCommand = resources.GetString("sqlDataSource.SelectCommand");
            // 
            // lineSeries1
            // 
            this.lineSeries1.ArrangeByAxis = this.graphAxis1;
            this.lineSeries1.CategoryGroup = graphGroup1;
            this.lineSeries1.CoordinateSystem = this.cartesianCoordinateSystem1;
            this.lineSeries1.DataPointLabel = "= Fields.������";
            this.lineSeries1.DataPointLabelStyle.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.lineSeries1.DataPointLabelStyle.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0.20000000298023224D);
            this.lineSeries1.DataPointLabelStyle.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.lineSeries1.DataPointStyle.Visible = true;
            this.lineSeries1.LegendItem.Style.BackgroundColor = System.Drawing.Color.Transparent;
            this.lineSeries1.LegendItem.Style.LineColor = System.Drawing.Color.Transparent;
            this.lineSeries1.LegendItem.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.lineSeries1.LineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(2D);
            this.lineSeries1.LineType = Telerik.Reporting.LineSeries.LineTypes.Smooth;
            this.lineSeries1.MarkerMaxSize = Telerik.Reporting.Drawing.Unit.Pixel(50D);
            this.lineSeries1.MarkerMinSize = Telerik.Reporting.Drawing.Unit.Pixel(5D);
            this.lineSeries1.MarkerSize = Telerik.Reporting.Drawing.Unit.Pixel(8D);
            this.lineSeries1.MarkerType = Telerik.Reporting.DataPointMarkerType.Circle;
            graphGroup2.Name = "seriesGroup";
            this.lineSeries1.SeriesGroup = graphGroup2;
            this.lineSeries1.Size = null;
            this.lineSeries1.Y = "= Fields.������";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.29999944567680359D), Telerik.Reporting.Drawing.Unit.Cm(12.785438537597656D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(26.100000381469727D), Telerik.Reporting.Drawing.Unit.Cm(0.64708298444747925D));
            this.textBox4.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Calibri";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox4.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.StyleName = "Data";
            this.textBox4.Value = "= \"������ ����� ���� ������� ��������/����� = \" + CStr(Sum(Fields.������)/Count(F" +
    "ields.AITISI_DATE))";
            // 
            // ���������_��GroupHeaderSection
            // 
            this.���������_��GroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.6471831202507019D);
            this.���������_��GroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.����_����CaptionTextBox,
            this.����_����DataTextBox,
            this.textBox2,
            this.textBox3});
            this.���������_��GroupHeaderSection.Name = "���������_��GroupHeaderSection";
            this.���������_��GroupHeaderSection.Style.BackgroundColor = System.Drawing.Color.LightGray;
            // 
            // ����_����CaptionTextBox
            // 
            this.����_����CaptionTextBox.CanGrow = true;
            this.����_����CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.����_����CaptionTextBox.Name = "����_����CaptionTextBox";
            this.����_����CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.6999998092651367D), Telerik.Reporting.Drawing.Unit.Cm(0.64708298444747925D));
            this.����_����CaptionTextBox.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.����_����CaptionTextBox.Style.Font.Bold = true;
            this.����_����CaptionTextBox.Style.Font.Name = "Calibri";
            this.����_����CaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.����_����CaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.����_����CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.����_����CaptionTextBox.StyleName = "Caption";
            this.����_����CaptionTextBox.Value = "���������:";
            // 
            // ����_����DataTextBox
            // 
            this.����_����DataTextBox.CanGrow = true;
            this.����_����DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.7530181407928467D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.����_����DataTextBox.Name = "����_����DataTextBox";
            this.����_����DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.0469827651977539D), Telerik.Reporting.Drawing.Unit.Cm(0.64708298444747925D));
            this.����_����DataTextBox.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.����_����DataTextBox.Style.Font.Bold = true;
            this.����_����DataTextBox.Style.Font.Name = "Calibri";
            this.����_����DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.����_����DataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.����_����DataTextBox.StyleName = "Data";
            this.����_����DataTextBox.Value = "= Fields.���������_��";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(20.711570739746094D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.8881328105926514D), Telerik.Reporting.Drawing.Unit.Cm(0.64708298444747925D));
            this.textBox2.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Calibri";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.StyleName = "Caption";
            this.textBox2.Value = "������� ����:";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(23.599905014038086D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.1000001430511475D), Telerik.Reporting.Drawing.Unit.Cm(0.64708298444747925D));
            this.textBox3.Style.BackgroundColor = System.Drawing.Color.Empty;
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Calibri";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.textBox3.StyleName = "Data";
            this.textBox3.Value = "= Fields.SY_TEXT";
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.308017373085022D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.33676722645759583D);
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            this.labelsGroupHeaderSection.Style.Visible = false;
            // 
            // sqlEgyklioi
            // 
            this.sqlEgyklioi.ConnectionString = "Proteus.Properties.Settings.DBConnectionString";
            this.sqlEgyklioi.Name = "sqlEgyklioi";
            this.sqlEgyklioi.SelectCommand = "SELECT        ���������_���, ���������_��\r\nFROM            ��_���������";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.55946636199951172D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.pageInfoTextBox,
            this.textBox20,
            this.textBox16});
            this.pageFooter.Name = "pageFooter";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.10262615978717804D), Telerik.Reporting.Drawing.Unit.Cm(9.850819333223626E-05D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.8412480354309082D), Telerik.Reporting.Drawing.Unit.Cm(0.55936628580093384D));
            this.currentTimeTextBox.Style.Font.Name = "Calibri";
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(21.549709320068359D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.0972805023193359D), Telerik.Reporting.Drawing.Unit.Cm(0.55936628580093384D));
            this.pageInfoTextBox.Style.Font.Name = "Calibri";
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=\"���. \" + PageNumber + \"/\" + PageCount";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.9440746307373047D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.8412480354309082D), Telerik.Reporting.Drawing.Unit.Cm(0.55926614999771118D));
            this.textBox20.Style.Font.Name = "Calibri";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.StyleName = "PageInfo";
            this.textBox20.Value = "�������� PROTEUS";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.7855234146118164D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(11.763987541198731D), Telerik.Reporting.Drawing.Unit.Cm(0.55936628580093384D));
            this.textBox16.Style.Font.Name = "Calibri";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox16.StyleName = "PageInfo";
            this.textBox16.Value = "��������� �������������� ����������";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(2.5002000331878662D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.subReport1,
            this.textBox1});
            this.reportHeader.Name = "reportHeader";
            // 
            // subReport1
            // 
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.subReport1.Name = "subReport1";
            typeReportSource1.TypeName = "Proteus.Reports.Moria.XmLogoOaed3, Proteus, Version=1.0.0.0, Culture=neutral, Pub" +
    "licKeyToken=null";
            this.subReport1.ReportSource = typeReportSource1;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(26.646987915039062D), Telerik.Reporting.Drawing.Unit.Cm(1.7999998331069946D));
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.8001999855041504D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(26.646989822387695D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Calibri";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "�������� �������� �������� ��������� ���������� ��� ���";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.32239228487014771D);
            this.detail.Name = "detail";
            this.detail.Style.Visible = false;
            // 
            // xmAitiseisDaily
            // 
            this.DataSource = this.sqlDataSource;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.���������_���", Telerik.Reporting.FilterOperator.Equal, "=Parameters.egykliosID.Value"));
            group1.GroupFooter = this.���������_��GroupFooterSection;
            group1.GroupHeader = this.���������_��GroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.���������_��"));
            group1.Name = "���������_��Group";
            group2.GroupFooter = this.labelsGroupFooterSection;
            group2.GroupHeader = this.labelsGroupHeaderSection;
            group2.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.���������_��GroupHeaderSection,
            this.���������_��GroupFooterSection,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageFooter,
            this.reportHeader,
            this.detail});
            this.Name = "xmAitiseisDaily";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(15D), Telerik.Reporting.Drawing.Unit.Mm(15D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlEgyklioi;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.���������_��";
            reportParameter1.AvailableValues.ValueMember = "= Fields.���������_���";
            reportParameter1.Name = "egykliosID";
            reportParameter1.Text = "���������";
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(26.700004577636719D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource;
        private Telerik.Reporting.GroupHeaderSection ���������_��GroupHeaderSection;
        private Telerik.Reporting.TextBox ����_����CaptionTextBox;
        private Telerik.Reporting.TextBox ����_����DataTextBox;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.GroupFooterSection ���������_��GroupFooterSection;
        private Telerik.Reporting.Graph graph1;
        private Telerik.Reporting.CartesianCoordinateSystem cartesianCoordinateSystem1;
        private Telerik.Reporting.GraphAxis graphAxis1;
        private Telerik.Reporting.GraphAxis graphAxis2;
        private Telerik.Reporting.LineSeries lineSeries1;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.SubReport subReport1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.SqlDataSource sqlEgyklioi;

    }
}