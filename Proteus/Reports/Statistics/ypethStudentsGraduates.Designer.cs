namespace Proteus.Reports.Statistics
{
    partial class ypethStudentsGraduates
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ypethStudentsGraduates));
            Telerik.Reporting.TypeReportSource typeReportSource1 = new Telerik.Reporting.TypeReportSource();
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.����_����������GroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.����_����������GroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.����_����������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.����_����������DataTextBox = new Telerik.Reporting.TextBox();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.������������_�������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.�������_������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.����������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.sqlYears = new Telerik.Reporting.SqlDataSource();
            this.sqlDataSource = new Telerik.Reporting.SqlDataSource();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.subReport1 = new Telerik.Reporting.SubReport();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.������������_�������DataTextBox = new Telerik.Reporting.TextBox();
            this.�������_������DataTextBox = new Telerik.Reporting.TextBox();
            this.����������DataTextBox = new Telerik.Reporting.TextBox();
            this.������DataTextBox = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // ����_����������GroupFooterSection
            // 
            this.����_����������GroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.81166678667068481D);
            this.����_����������GroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2});
            this.����_����������GroupFooterSection.Name = "����_����������GroupFooterSection";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(23.799999237060547D), Telerik.Reporting.Drawing.Unit.Cm(0.21166665852069855D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7412501573562622D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox1.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.StyleName = "Data";
            this.textBox1.Value = "=Sum(Fields.������)";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.060425896197557449D), Telerik.Reporting.Drawing.Unit.Cm(0.21166665852069855D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(23.7393741607666D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox2.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.StyleName = "Caption";
            this.textBox2.Value = "= \"������ �������������� ���������� ��� ��� �� ���� ��������: \" + CStr(Fields.���" +
    "�_����������)";
            // 
            // ����_����������GroupHeaderSection
            // 
            this.����_����������GroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.88813251256942749D);
            this.����_����������GroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.����_����������CaptionTextBox,
            this.����_����������DataTextBox});
            this.����_����������GroupHeaderSection.Name = "����_����������GroupHeaderSection";
            // 
            // ����_����������CaptionTextBox
            // 
            this.����_����������CaptionTextBox.CanGrow = true;
            this.����_����������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.����_����������CaptionTextBox.Name = "����_����������CaptionTextBox";
            this.����_����������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.7470834255218506D), Telerik.Reporting.Drawing.Unit.Cm(0.68833285570144653D));
            this.����_����������CaptionTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.����_����������CaptionTextBox.Style.Font.Bold = true;
            this.����_����������CaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.����_����������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.����_����������CaptionTextBox.StyleName = "Caption";
            this.����_����������CaptionTextBox.Value = "���� ��������:";
            // 
            // ����_����������DataTextBox
            // 
            this.����_����������DataTextBox.CanGrow = true;
            this.����_����������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.8002004623413086D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.����_����������DataTextBox.Name = "����_����������DataTextBox";
            this.����_����������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(21.741048812866211D), Telerik.Reporting.Drawing.Unit.Cm(0.68833285570144653D));
            this.����_����������DataTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.����_����������DataTextBox.Style.Font.Bold = true;
            this.����_����������DataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.����_����������DataTextBox.StyleName = "Data";
            this.����_����������DataTextBox.Value = "=Fields.����_����������";
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.50000017881393433D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.������������_�������CaptionTextBox,
            this.�������_������CaptionTextBox,
            this.����������CaptionTextBox,
            this.������CaptionTextBox});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // ������������_�������CaptionTextBox
            // 
            this.������������_�������CaptionTextBox.CanGrow = true;
            this.������������_�������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.������������_�������CaptionTextBox.Name = "������������_�������CaptionTextBox";
            this.������������_�������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.3323960304260254D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.������������_�������CaptionTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.������������_�������CaptionTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.������������_�������CaptionTextBox.Style.Font.Bold = true;
            this.������������_�������CaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.������������_�������CaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.������������_�������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.������������_�������CaptionTextBox.StyleName = "Caption";
            this.������������_�������CaptionTextBox.Value = "������������ �������";
            // 
            // �������_������CaptionTextBox
            // 
            this.�������_������CaptionTextBox.CanGrow = true;
            this.�������_������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.3855128288269043D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.�������_������CaptionTextBox.Name = "�������_������CaptionTextBox";
            this.�������_������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.6144876480102539D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.�������_������CaptionTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.�������_������CaptionTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.�������_������CaptionTextBox.Style.Font.Bold = true;
            this.�������_������CaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.�������_������CaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.�������_������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.�������_������CaptionTextBox.StyleName = "Caption";
            this.�������_������CaptionTextBox.Value = "������������ ������";
            // 
            // ����������CaptionTextBox
            // 
            this.����������CaptionTextBox.CanGrow = true;
            this.����������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.000200271606445D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.����������CaptionTextBox.Name = "����������CaptionTextBox";
            this.����������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(11.799598693847656D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.����������CaptionTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.����������CaptionTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.����������CaptionTextBox.Style.Font.Bold = true;
            this.����������CaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.����������CaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.����������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.����������CaptionTextBox.StyleName = "Caption";
            this.����������CaptionTextBox.Value = "����������";
            // 
            // ������CaptionTextBox
            // 
            this.������CaptionTextBox.CanGrow = true;
            this.������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(23.799999237060547D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.������CaptionTextBox.Name = "������CaptionTextBox";
            this.������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7412501573562622D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.������CaptionTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.������CaptionTextBox.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.������CaptionTextBox.Style.Font.Bold = true;
            this.������CaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������CaptionTextBox.StyleName = "Caption";
            this.������CaptionTextBox.Value = "������";
            // 
            // sqlYears
            // 
            this.sqlYears.ConnectionString = "Proteus.Properties.Settings.DBConnectionString";
            this.sqlYears.Name = "sqlYears";
            this.sqlYears.SelectCommand = "SELECT        ����\r\nFROM            ���\r\nWHERE        (���� > 2015)";
            // 
            // sqlDataSource
            // 
            this.sqlDataSource.ConnectionString = "Proteus.Properties.Settings.DBConnectionString";
            this.sqlDataSource.Name = "sqlDataSource";
            this.sqlDataSource.SelectCommand = "SELECT        ������������_�������, �������_������, ����������, ���������, ����_�" +
    "���������, ������\r\nFROM            ���_StudentsApofitisi\r\nORDER BY �������_�����" +
    "�, ����������";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.76260817050933838D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox19,
            this.pictureBox1,
            this.pageInfoTextBox,
            this.shape1});
            this.pageFooter.Name = "pageFooter";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D), Telerik.Reporting.Drawing.Unit.Cm(0.32770851254463196D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.099800109863281D), Telerik.Reporting.Drawing.Unit.Cm(0.41749951243400574D));
            this.textBox19.Style.Font.Italic = true;
            this.textBox19.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.textBox19.StyleName = "PageInfo";
            this.textBox19.Value = "PROTEUS - ������� ����������� ������� ������������ ���-����";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.060425695031881332D), Telerik.Reporting.Drawing.Unit.Cm(0.34510812163352966D));
            this.pictureBox1.MimeType = "image/png";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.53937435150146484D), Telerik.Reporting.Drawing.Unit.Cm(0.40010008215904236D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.152915954589844D), Telerik.Reporting.Drawing.Unit.Cm(0.34510812163352966D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.38833236694336D), Telerik.Reporting.Drawing.Unit.Cm(0.41750004887580872D));
            this.pageInfoTextBox.Style.Font.Italic = true;
            this.pageInfoTextBox.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(4D);
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=\"���. \" + PageNumber + \" ��� \" + PageCount";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.060425896197557449D), Telerik.Reporting.Drawing.Unit.Cm(0.1952165961265564D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(25.480825424194336D), Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144D));
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(3.1000001430511475D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.subReport1,
            this.textBox14,
            this.textBox13});
            this.reportHeader.Name = "reportHeader";
            // 
            // subReport1
            // 
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.060425896197557449D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.subReport1.Name = "subReport1";
            typeReportSource1.TypeName = "Proteus.Reports.LogoD3, Proteus, Version=1.0.0.0, Culture=neutral, PublicKeyToken" +
    "=null";
            this.subReport1.ReportSource = typeReportSource1;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.1000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(2.7999999523162842D));
            // 
            // textBox14
            // 
            this.textBox14.CanGrow = true;
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.1606264114379883D), Telerik.Reporting.Drawing.Unit.Cm(1.6000000238418579D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.380622863769531D), Telerik.Reporting.Drawing.Unit.Cm(0.59970051050186157D));
            this.textBox14.Style.Font.Bold = true;
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox14.StyleName = "Caption";
            this.textBox14.Value = "(�) ������� ��������� �.�.�. - �.��.�. ��� ��";
            // 
            // textBox13
            // 
            this.textBox13.CanGrow = true;
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.1606264114379883D), Telerik.Reporting.Drawing.Unit.Cm(2.2085416316986084D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.380624771118164D), Telerik.Reporting.Drawing.Unit.Cm(0.59990018606185913D));
            this.textBox13.Style.Font.Bold = true;
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.StyleName = "Caption";
            this.textBox13.Value = "��������� ����������� ��� ������������ ��� �.���.�. �� ���� ������� ��������� ���" +
    " �.�.�.�";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.������������_�������DataTextBox,
            this.�������_������DataTextBox,
            this.����������DataTextBox,
            this.������DataTextBox});
            this.detail.Name = "detail";
            // 
            // ������������_�������DataTextBox
            // 
            this.������������_�������DataTextBox.CanGrow = true;
            this.������������_�������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.060425896197557449D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.������������_�������DataTextBox.Name = "������������_�������DataTextBox";
            this.������������_�������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.3323960304260254D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.������������_�������DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.������������_�������DataTextBox.StyleName = "Data";
            this.������������_�������DataTextBox.Value = "=Fields.������������_�������";
            // 
            // �������_������DataTextBox
            // 
            this.�������_������DataTextBox.CanGrow = true;
            this.�������_������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.3930220603942871D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.�������_������DataTextBox.Name = "�������_������DataTextBox";
            this.�������_������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.6069784164428711D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.�������_������DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.�������_������DataTextBox.StyleName = "Data";
            this.�������_������DataTextBox.Value = "=Fields.�������_������";
            // 
            // ����������DataTextBox
            // 
            this.����������DataTextBox.CanGrow = true;
            this.����������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.000200271606445D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.����������DataTextBox.Name = "����������DataTextBox";
            this.����������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(11.799598693847656D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.����������DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.����������DataTextBox.StyleName = "Data";
            this.����������DataTextBox.Value = "=Fields.����������";
            // 
            // ������DataTextBox
            // 
            this.������DataTextBox.CanGrow = true;
            this.������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(23.799999237060547D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.������DataTextBox.Name = "������DataTextBox";
            this.������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7412501573562622D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.������DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.������DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������DataTextBox.StyleName = "Data";
            this.������DataTextBox.Value = "=Fields.������";
            // 
            // ypethStudentsGraduates
            // 
            this.DataSource = this.sqlDataSource;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.����_����������", Telerik.Reporting.FilterOperator.Equal, "=Parameters.year.Value"));
            group1.GroupFooter = this.����_����������GroupFooterSection;
            group1.GroupHeader = this.����_����������GroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.����_����������"));
            group1.Name = "����_����������Group";
            group2.GroupFooter = this.labelsGroupFooterSection;
            group2.GroupHeader = this.labelsGroupHeaderSection;
            group2.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.����_����������GroupHeaderSection,
            this.����_����������GroupFooterSection,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageFooter,
            this.reportHeader,
            this.detail});
            this.Name = "ypethStudentsGraduates";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(10D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlYears;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.����";
            reportParameter1.AvailableValues.ValueMember = "= Fields.����";
            reportParameter1.Name = "year";
            reportParameter1.Text = "���� ��������";
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(25.700002670288086D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource;
        private Telerik.Reporting.GroupHeaderSection ����_����������GroupHeaderSection;
        private Telerik.Reporting.TextBox ����_����������CaptionTextBox;
        private Telerik.Reporting.TextBox ����_����������DataTextBox;
        private Telerik.Reporting.GroupFooterSection ����_����������GroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox ������������_�������CaptionTextBox;
        private Telerik.Reporting.TextBox �������_������CaptionTextBox;
        private Telerik.Reporting.TextBox ����������CaptionTextBox;
        private Telerik.Reporting.TextBox ������CaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox ������������_�������DataTextBox;
        private Telerik.Reporting.TextBox �������_������DataTextBox;
        private Telerik.Reporting.TextBox ����������DataTextBox;
        private Telerik.Reporting.TextBox ������DataTextBox;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.Shape shape1;
        private Telerik.Reporting.SubReport subReport1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.SqlDataSource sqlYears;

    }
}