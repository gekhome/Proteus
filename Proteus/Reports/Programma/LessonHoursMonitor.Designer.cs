namespace Proteus.Reports.Programma
{
    partial class LessonHoursMonitor
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TypeReportSource typeReportSource1 = new Telerik.Reporting.TypeReportSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LessonHoursMonitor));
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group3 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group4 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.��������GroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.��������GroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.subReport1 = new Telerik.Reporting.SubReport();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.��������DataTextBox = new Telerik.Reporting.TextBox();
            this.��������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.eIDIKOTITA_IDGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.eIDIKOTITA_IDGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.eIDIKOTITA_IDDataTextBox = new Telerik.Reporting.TextBox();
            this.eIDIKOTITA_IDCaptionTextBox = new Telerik.Reporting.TextBox();
            this.���_�����GroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.���_�����GroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.�����_���CaptionTextBox = new Telerik.Reporting.TextBox();
            this.�����_���DataTextBox = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.lESSON_TEXTCaptionTextBox = new Telerik.Reporting.TextBox();
            this.�������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.����_������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.����_����CaptionTextBox = new Telerik.Reporting.TextBox();
            this.��������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.shape3 = new Telerik.Reporting.Shape();
            this.detail = new Telerik.Reporting.DetailSection();
            this.lESSON_TEXTDataTextBox = new Telerik.Reporting.TextBox();
            this.�������DataTextBox = new Telerik.Reporting.TextBox();
            this.����_������DataTextBox = new Telerik.Reporting.TextBox();
            this.����_����DataTextBox = new Telerik.Reporting.TextBox();
            this.��������DataTextBox = new Telerik.Reporting.TextBox();
            this.shape2 = new Telerik.Reporting.Shape();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // ��������GroupFooterSection
            // 
            this.��������GroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.30000025033950806D);
            this.��������GroupFooterSection.Name = "��������GroupFooterSection";
            this.��������GroupFooterSection.Style.Visible = false;
            // 
            // ��������GroupHeaderSection
            // 
            this.��������GroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(4.3999996185302734D);
            this.��������GroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.subReport1,
            this.textBox1,
            this.��������DataTextBox,
            this.��������CaptionTextBox});
            this.��������GroupHeaderSection.Name = "��������GroupHeaderSection";
            // 
            // subReport1
            // 
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.subReport1.Name = "subReport1";
            typeReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("schoolID", "=Fields.���"));
            typeReportSource1.TypeName = "Proteus.Reports.LogoIekShort, Proteus, Version=1.0.0.0, Culture=neutral, PublicKe" +
    "yToken=null";
            this.subReport1.ReportSource = typeReportSource1;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(11.547082901000977D), Telerik.Reporting.Drawing.Unit.Cm(1.9000000953674316D));
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(2.4000000953674316D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(17.804584503173828D), Telerik.Reporting.Drawing.Unit.Cm(0.800000011920929D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Calibri";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "������� �������������� ������������� ���� ���������";
            // 
            // ��������DataTextBox
            // 
            this.��������DataTextBox.CanGrow = true;
            this.��������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.1220831871032715D), Telerik.Reporting.Drawing.Unit.Cm(3.7000000476837158D));
            this.��������DataTextBox.Name = "��������DataTextBox";
            this.��������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(14.682499885559082D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.��������DataTextBox.Style.Font.Bold = true;
            this.��������DataTextBox.Style.Font.Name = "Calibri";
            this.��������DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.��������DataTextBox.StyleName = "Data";
            this.��������DataTextBox.Value = "=Fields.��������";
            // 
            // ��������CaptionTextBox
            // 
            this.��������CaptionTextBox.CanGrow = true;
            this.��������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(3.7000000476837158D));
            this.��������CaptionTextBox.Name = "��������CaptionTextBox";
            this.��������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9837501049041748D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.��������CaptionTextBox.Style.Font.Bold = true;
            this.��������CaptionTextBox.Style.Font.Name = "Calibri";
            this.��������CaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.��������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.��������CaptionTextBox.StyleName = "Caption";
            this.��������CaptionTextBox.Value = "��������:";
            // 
            // eIDIKOTITA_IDGroupFooterSection
            // 
            this.eIDIKOTITA_IDGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.30000025033950806D);
            this.eIDIKOTITA_IDGroupFooterSection.Name = "eIDIKOTITA_IDGroupFooterSection";
            this.eIDIKOTITA_IDGroupFooterSection.Style.Visible = false;
            // 
            // eIDIKOTITA_IDGroupHeaderSection
            // 
            this.eIDIKOTITA_IDGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D);
            this.eIDIKOTITA_IDGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.eIDIKOTITA_IDDataTextBox,
            this.eIDIKOTITA_IDCaptionTextBox});
            this.eIDIKOTITA_IDGroupHeaderSection.Name = "eIDIKOTITA_IDGroupHeaderSection";
            // 
            // eIDIKOTITA_IDDataTextBox
            // 
            this.eIDIKOTITA_IDDataTextBox.CanGrow = true;
            this.eIDIKOTITA_IDDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.1220831871032715D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.eIDIKOTITA_IDDataTextBox.Name = "eIDIKOTITA_IDDataTextBox";
            this.eIDIKOTITA_IDDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(14.682499885559082D), Telerik.Reporting.Drawing.Unit.Cm(0.59989994764328D));
            this.eIDIKOTITA_IDDataTextBox.Style.Font.Bold = true;
            this.eIDIKOTITA_IDDataTextBox.Style.Font.Name = "Calibri";
            this.eIDIKOTITA_IDDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.eIDIKOTITA_IDDataTextBox.StyleName = "Data";
            this.eIDIKOTITA_IDDataTextBox.Value = "= Fields.EIDIKOTITA_TEXT";
            // 
            // eIDIKOTITA_IDCaptionTextBox
            // 
            this.eIDIKOTITA_IDCaptionTextBox.CanGrow = true;
            this.eIDIKOTITA_IDCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.eIDIKOTITA_IDCaptionTextBox.Name = "eIDIKOTITA_IDCaptionTextBox";
            this.eIDIKOTITA_IDCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.59989994764328D));
            this.eIDIKOTITA_IDCaptionTextBox.Style.Font.Bold = true;
            this.eIDIKOTITA_IDCaptionTextBox.Style.Font.Name = "Calibri";
            this.eIDIKOTITA_IDCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.eIDIKOTITA_IDCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.eIDIKOTITA_IDCaptionTextBox.StyleName = "Caption";
            this.eIDIKOTITA_IDCaptionTextBox.Value = "����������:";
            // 
            // ���_�����GroupFooterSection
            // 
            this.���_�����GroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.29999944567680359D);
            this.���_�����GroupFooterSection.Name = "���_�����GroupFooterSection";
            this.���_�����GroupFooterSection.Style.Visible = false;
            // 
            // ���_�����GroupHeaderSection
            // 
            this.���_�����GroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.800000786781311D);
            this.���_�����GroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.�����_���CaptionTextBox,
            this.�����_���DataTextBox,
            this.textBox2});
            this.���_�����GroupHeaderSection.Name = "���_�����GroupHeaderSection";
            // 
            // �����_���CaptionTextBox
            // 
            this.�����_���CaptionTextBox.CanGrow = true;
            this.�����_���CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.�����_���CaptionTextBox.Name = "�����_���CaptionTextBox";
            this.�����_���CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9837501049041748D), Telerik.Reporting.Drawing.Unit.Cm(0.64708298444747925D));
            this.�����_���CaptionTextBox.Style.Font.Bold = true;
            this.�����_���CaptionTextBox.Style.Font.Name = "Calibri";
            this.�����_���CaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.�����_���CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.�����_���CaptionTextBox.StyleName = "Caption";
            this.�����_���CaptionTextBox.Value = "�����:";
            // 
            // �����_���DataTextBox
            // 
            this.�����_���DataTextBox.CanGrow = true;
            this.�����_���DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.1220831871032715D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.�����_���DataTextBox.Name = "�����_���DataTextBox";
            this.�����_���DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.4250001907348633D), Telerik.Reporting.Drawing.Unit.Cm(0.64708298444747925D));
            this.�����_���DataTextBox.Style.Font.Bold = true;
            this.�����_���DataTextBox.Style.Font.Name = "Calibri";
            this.�����_���DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.�����_���DataTextBox.StyleName = "Data";
            this.�����_���DataTextBox.Value = "= Fields.�����_�����";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.705416679382324D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.1000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0.64708292484283447D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Calibri";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox2.StyleName = "Data";
            this.textBox2.Value = "= Fields.TERM_TEXT";
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.20000070333480835D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1854088306427002D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.lESSON_TEXTCaptionTextBox,
            this.�������CaptionTextBox,
            this.����_������CaptionTextBox,
            this.����_����CaptionTextBox,
            this.��������CaptionTextBox,
            this.shape1});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // lESSON_TEXTCaptionTextBox
            // 
            this.lESSON_TEXTCaptionTextBox.CanGrow = true;
            this.lESSON_TEXTCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.lESSON_TEXTCaptionTextBox.Name = "lESSON_TEXTCaptionTextBox";
            this.lESSON_TEXTCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.9468832015991211D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.lESSON_TEXTCaptionTextBox.Style.Font.Bold = true;
            this.lESSON_TEXTCaptionTextBox.StyleName = "Caption";
            this.lESSON_TEXTCaptionTextBox.Value = "������";
            // 
            // �������CaptionTextBox
            // 
            this.�������CaptionTextBox.CanGrow = true;
            this.�������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.�������CaptionTextBox.Name = "�������CaptionTextBox";
            this.�������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5365660190582275D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.�������CaptionTextBox.Style.Font.Bold = true;
            this.�������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.�������CaptionTextBox.StyleName = "Caption";
            this.�������CaptionTextBox.Value = "�/�";
            // 
            // ����_������CaptionTextBox
            // 
            this.����_������CaptionTextBox.CanGrow = true;
            this.����_������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.536766052246094D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.����_������CaptionTextBox.Name = "����_������CaptionTextBox";
            this.����_������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.963032603263855D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.����_������CaptionTextBox.Style.Font.Bold = true;
            this.����_������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.����_������CaptionTextBox.StyleName = "Caption";
            this.����_������CaptionTextBox.Value = "����. ����";
            // 
            // ����_����CaptionTextBox
            // 
            this.����_����CaptionTextBox.CanGrow = true;
            this.����_����CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.499999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.����_����CaptionTextBox.Name = "����_����CaptionTextBox";
            this.����_����CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9630334377288818D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.����_����CaptionTextBox.Style.Font.Bold = true;
            this.����_����CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.����_����CaptionTextBox.StyleName = "Caption";
            this.����_����CaptionTextBox.Value = "������ ����";
            // 
            // ��������CaptionTextBox
            // 
            this.��������CaptionTextBox.CanGrow = true;
            this.��������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.463232040405273D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.��������CaptionTextBox.Name = "��������CaptionTextBox";
            this.��������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.341249942779541D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.��������CaptionTextBox.Style.Font.Bold = true;
            this.��������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.��������CaptionTextBox.StyleName = "Caption";
            this.��������CaptionTextBox.Value = "��������";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(1.0531171560287476D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(17.751564025878906D), Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144D));
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "Proteus.Properties.Settings.DBConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.SelectCommand = resources.GetString("sqlDataSource1.SelectCommand");
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(1.4901912212371826D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox3,
            this.currentTimeTextBox,
            this.pageInfoTextBox,
            this.shape3});
            this.pageFooter.Name = "pageFooter";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.34250009059906006D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(17.751564025878906D), Telerik.Reporting.Drawing.Unit.Cm(0.54250115156173706D));
            this.textBox3.Style.Font.Bold = false;
            this.textBox3.Style.Font.Italic = true;
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = "��������: ������ �������� �������� ��� ��������� ����, ��� �������� ��� ����� ���" +
    "���� �� ������ ����.";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.0112496614456177D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.8677082061767578D), Telerik.Reporting.Drawing.Unit.Cm(0.47894155979156494D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.9164581298828125D), Telerik.Reporting.Drawing.Unit.Cm(1.0112496614456177D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.8677082061767578D), Telerik.Reporting.Drawing.Unit.Cm(0.47894155979156494D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=\"���. \" + PageNumber + \" ��� \" + PageCount";
            // 
            // shape3
            // 
            this.shape3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.2100081741809845D));
            this.shape3.Name = "shape3";
            this.shape3.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.64708423614502D), Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144D));
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.74708372354507446D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.lESSON_TEXTDataTextBox,
            this.�������DataTextBox,
            this.����_������DataTextBox,
            this.����_����DataTextBox,
            this.��������DataTextBox,
            this.shape2});
            this.detail.Name = "detail";
            // 
            // lESSON_TEXTDataTextBox
            // 
            this.lESSON_TEXTDataTextBox.CanGrow = true;
            this.lESSON_TEXTDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.lESSON_TEXTDataTextBox.Name = "lESSON_TEXTDataTextBox";
            this.lESSON_TEXTDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.9468832015991211D), Telerik.Reporting.Drawing.Unit.Cm(0.56167477369308472D));
            this.lESSON_TEXTDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.lESSON_TEXTDataTextBox.StyleName = "Data";
            this.lESSON_TEXTDataTextBox.Value = "=Fields.LESSON_TEXT";
            // 
            // �������DataTextBox
            // 
            this.�������DataTextBox.CanGrow = true;
            this.�������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.�������DataTextBox.Name = "�������DataTextBox";
            this.�������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5365660190582275D), Telerik.Reporting.Drawing.Unit.Cm(0.56167477369308472D));
            this.�������DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.�������DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.�������DataTextBox.StyleName = "Data";
            this.�������DataTextBox.Value = "=Fields.�������";
            // 
            // ����_������DataTextBox
            // 
            this.����_������DataTextBox.CanGrow = true;
            this.����_������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.536766052246094D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.����_������DataTextBox.Name = "����_������DataTextBox";
            this.����_������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9630334377288818D), Telerik.Reporting.Drawing.Unit.Cm(0.56167477369308472D));
            this.����_������DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.����_������DataTextBox.StyleName = "Data";
            this.����_������DataTextBox.Value = "=Fields.����_������";
            // 
            // ����_����DataTextBox
            // 
            this.����_����DataTextBox.CanGrow = true;
            this.����_����DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.499999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.����_����DataTextBox.Name = "����_����DataTextBox";
            this.����_����DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9630318880081177D), Telerik.Reporting.Drawing.Unit.Cm(0.56167477369308472D));
            this.����_����DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.����_����DataTextBox.StyleName = "Data";
            this.����_����DataTextBox.Value = "=Fields.����_����";
            // 
            // ��������DataTextBox
            // 
            this.��������DataTextBox.CanGrow = true;
            this.��������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.463232040405273D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.��������DataTextBox.Name = "��������DataTextBox";
            this.��������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3412485122680664D), Telerik.Reporting.Drawing.Unit.Cm(0.56167477369308472D));
            this.��������DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.��������DataTextBox.StyleName = "Data";
            this.��������DataTextBox.Value = "=Fields.��������";
            // 
            // shape2
            // 
            this.shape2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.61479204893112183D));
            this.shape2.Name = "shape2";
            this.shape2.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(17.751564025878906D), Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144D));
            // 
            // LessonHoursMonitor
            // 
            this.DataSource = this.sqlDataSource1;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.���_�����", Telerik.Reporting.FilterOperator.Equal, "=Parameters.tmimaID.Value"));
            group1.GroupFooter = this.��������GroupFooterSection;
            group1.GroupHeader = this.��������GroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.��������"));
            group1.Name = "��������Group";
            group2.GroupFooter = this.eIDIKOTITA_IDGroupFooterSection;
            group2.GroupHeader = this.eIDIKOTITA_IDGroupHeaderSection;
            group2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.EIDIKOTITA_ID"));
            group2.Name = "eIDIKOTITA_IDGroup";
            group3.GroupFooter = this.���_�����GroupFooterSection;
            group3.GroupHeader = this.���_�����GroupHeaderSection;
            group3.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.���_�����"));
            group3.Name = "���_�����Group";
            group4.GroupFooter = this.labelsGroupFooterSection;
            group4.GroupHeader = this.labelsGroupHeaderSection;
            group4.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2,
            group3,
            group4});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.��������GroupHeaderSection,
            this.��������GroupFooterSection,
            this.eIDIKOTITA_IDGroupHeaderSection,
            this.eIDIKOTITA_IDGroupFooterSection,
            this.���_�����GroupHeaderSection,
            this.���_�����GroupFooterSection,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageFooter,
            this.detail});
            this.Name = "LessonHoursMonitor";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlDataSource1;
            reportParameter1.AvailableValues.ValueMember = "= Fields.���_�����";
            reportParameter1.Name = "tmimaID";
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(17.837081909179688D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.GroupHeaderSection ��������GroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection ��������GroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection eIDIKOTITA_IDGroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection eIDIKOTITA_IDGroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection ���_�����GroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection ���_�����GroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox lESSON_TEXTCaptionTextBox;
        private Telerik.Reporting.TextBox �������CaptionTextBox;
        private Telerik.Reporting.TextBox ����_������CaptionTextBox;
        private Telerik.Reporting.TextBox ����_����CaptionTextBox;
        private Telerik.Reporting.TextBox ��������CaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox lESSON_TEXTDataTextBox;
        private Telerik.Reporting.TextBox �������DataTextBox;
        private Telerik.Reporting.TextBox ����_������DataTextBox;
        private Telerik.Reporting.TextBox ����_����DataTextBox;
        private Telerik.Reporting.TextBox ��������DataTextBox;
        private Telerik.Reporting.SubReport subReport1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox ��������DataTextBox;
        private Telerik.Reporting.TextBox ��������CaptionTextBox;
        private Telerik.Reporting.TextBox eIDIKOTITA_IDDataTextBox;
        private Telerik.Reporting.TextBox eIDIKOTITA_IDCaptionTextBox;
        private Telerik.Reporting.TextBox �����_���CaptionTextBox;
        private Telerik.Reporting.TextBox �����_���DataTextBox;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.Shape shape1;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.Shape shape2;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.Shape shape3;

    }
}