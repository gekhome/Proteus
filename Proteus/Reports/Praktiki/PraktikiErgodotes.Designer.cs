namespace Proteus.Reports.Praktiki
{
    partial class PraktikiErgodotes
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.detail = new Telerik.Reporting.DetailSection();
            this.���������_��������DataTextBox = new Telerik.Reporting.TextBox();
            this.�����_���DataTextBox = new Telerik.Reporting.TextBox();
            this.�����_���DataTextBox = new Telerik.Reporting.TextBox();
            this.����DataTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "Proteus.Properties.Settings.DBConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.SelectCommand = "SELECT        STUDENT_ID, �������������, ���������_��������, �����_���, �����_���" +
    ", ����\r\nFROM            rep��������_���������\r\nORDER BY �����_���";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.83249157667160034D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.���������_��������DataTextBox,
            this.�����_���DataTextBox,
            this.�����_���DataTextBox,
            this.����DataTextBox,
            this.textBox1,
            this.shape1});
            this.detail.Name = "detail";
            // 
            // ���������_��������DataTextBox
            // 
            this.���������_��������DataTextBox.CanGrow = true;
            this.���������_��������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.60019993782043457D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.���������_��������DataTextBox.Name = "���������_��������DataTextBox";
            this.���������_��������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.4998006820678711D), Telerik.Reporting.Drawing.Unit.Cm(0.64708340167999268D));
            this.���������_��������DataTextBox.StyleName = "Data";
            this.���������_��������DataTextBox.Value = "=Fields.���������_��������";
            // 
            // �����_���DataTextBox
            // 
            this.�����_���DataTextBox.CanGrow = true;
            this.�����_���DataTextBox.Format = "{0:d}";
            this.�����_���DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.1001996994018555D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.�����_���DataTextBox.Name = "�����_���DataTextBox";
            this.�����_���DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0261456966400146D), Telerik.Reporting.Drawing.Unit.Cm(0.64708340167999268D));
            this.�����_���DataTextBox.StyleName = "Data";
            this.�����_���DataTextBox.Value = "=Fields.�����_���";
            // 
            // �����_���DataTextBox
            // 
            this.�����_���DataTextBox.CanGrow = true;
            this.�����_���DataTextBox.Format = "{0:d}";
            this.�����_���DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.126545906066895D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.�����_���DataTextBox.Name = "�����_���DataTextBox";
            this.�����_���DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0261456966400146D), Telerik.Reporting.Drawing.Unit.Cm(0.64708340167999268D));
            this.�����_���DataTextBox.StyleName = "Data";
            this.�����_���DataTextBox.Value = "=Fields.�����_���";
            // 
            // ����DataTextBox
            // 
            this.����DataTextBox.CanGrow = true;
            this.����DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.152891159057617D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.����DataTextBox.Name = "����DataTextBox";
            this.����DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.6083590984344482D), Telerik.Reporting.Drawing.Unit.Cm(0.64708340167999268D));
            this.����DataTextBox.StyleName = "Data";
            this.����DataTextBox.Value = "=CStr(Fields.����) + \"  ����\"";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.54708337783813477D), Telerik.Reporting.Drawing.Unit.Cm(0.6470833420753479D));
            this.textBox1.Style.Visible = false;
            this.textBox1.StyleName = "Data";
            this.textBox1.Value = "= Fields.STUDENT_ID";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.70019990205764771D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.708333015441895D), Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144D));
            this.shape1.Style.LineStyle = Telerik.Reporting.Drawing.LineStyle.Dotted;
            // 
            // PraktikiErgodotes
            // 
            this.DataSource = this.sqlDataSource1;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.STUDENT_ID", Telerik.Reporting.FilterOperator.Equal, "=Parameters.studentID.Value"));
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
            this.Name = "PraktikiErgodotes";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlDataSource1;
            reportParameter1.AvailableValues.ValueMember = "= Fields.STUDENT_ID";
            reportParameter1.Name = "studentID";
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(15.814167022705078D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox ���������_��������DataTextBox;
        private Telerik.Reporting.TextBox �����_���DataTextBox;
        private Telerik.Reporting.TextBox �����_���DataTextBox;
        private Telerik.Reporting.TextBox ����DataTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.Shape shape1;

    }
}