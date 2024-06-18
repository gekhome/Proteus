namespace Proteus.Reports.Moria
{
    partial class srepAitisiEidikotites
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
            this.sCHOOL_NAMEGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.sCHOOL_NAMEGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.eIDIKOTITA_TERMCaptionTextBox = new Telerik.Reporting.TextBox();
            this.sqlDataSource = new Telerik.Reporting.SqlDataSource();
            this.detail = new Telerik.Reporting.DetailSection();
            this.eIDIKOTITA_TERMDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sCHOOL_NAMEGroupFooterSection
            // 
            this.sCHOOL_NAMEGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.38562485575675964D);
            this.sCHOOL_NAMEGroupFooterSection.Name = "sCHOOL_NAMEGroupFooterSection";
            this.sCHOOL_NAMEGroupFooterSection.Style.Visible = false;
            // 
            // sCHOOL_NAMEGroupHeaderSection
            // 
            this.sCHOOL_NAMEGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.49999997019767761D);
            this.sCHOOL_NAMEGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1});
            this.sCHOOL_NAMEGroupHeaderSection.Name = "sCHOOL_NAMEGroupHeaderSection";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.841249465942383D), Telerik.Reporting.Drawing.Unit.Cm(0.49990001320838928D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.StyleName = "Data";
            this.textBox1.Value = "= IIf(Fields.IEK = \"IEK1\", \"…≈  1ÁÚ ÂÈÎÔ„ﬁÚ\", \"…≈  2ÁÚ ÂÈÎÔ„ﬁÚ\") + \" : \" + Fiel" +
    "ds.SCHOOL_NAME";
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.39999979734420776D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.49990004301071167D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.eIDIKOTITA_TERMCaptionTextBox});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // eIDIKOTITA_TERMCaptionTextBox
            // 
            this.eIDIKOTITA_TERMCaptionTextBox.CanGrow = true;
            this.eIDIKOTITA_TERMCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.eIDIKOTITA_TERMCaptionTextBox.Name = "eIDIKOTITA_TERMCaptionTextBox";
            this.eIDIKOTITA_TERMCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.788331985473633D), Telerik.Reporting.Drawing.Unit.Cm(0.49990004301071167D));
            this.eIDIKOTITA_TERMCaptionTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.eIDIKOTITA_TERMCaptionTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.eIDIKOTITA_TERMCaptionTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.eIDIKOTITA_TERMCaptionTextBox.Style.Font.Bold = true;
            this.eIDIKOTITA_TERMCaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.eIDIKOTITA_TERMCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.eIDIKOTITA_TERMCaptionTextBox.StyleName = "Caption";
            this.eIDIKOTITA_TERMCaptionTextBox.Value = "≈…ƒ… œ‘«‘¡ - ≈Œ¡Ã«Õœ";
            // 
            // sqlDataSource
            // 
            this.sqlDataSource.ConnectionString = "Proteus.Properties.Settings.DBConnectionString";
            this.sqlDataSource.Name = "sqlDataSource";
            this.sqlDataSource.SelectCommand = "SELECT        ¡…‘«”«_ Ÿƒ, SCHOOL_ID, SCHOOL_NAME, IEK, EIDIKOTITA, EIDIKOTITA_TER" +
    "M, TERM\r\nFROM            ◊Ã_tabAITISI_EIDIKOTITES\r\nORDER BY IEK, EIDIKOTITA";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.63269168138504028D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.eIDIKOTITA_TERMDataTextBox,
            this.textBox2,
            this.shape1});
            this.detail.Name = "detail";
            // 
            // eIDIKOTITA_TERMDataTextBox
            // 
            this.eIDIKOTITA_TERMDataTextBox.CanGrow = true;
            this.eIDIKOTITA_TERMDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.1531161069869995D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.eIDIKOTITA_TERMDataTextBox.Name = "eIDIKOTITA_TERMDataTextBox";
            this.eIDIKOTITA_TERMDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.688132286071777D), Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D));
            this.eIDIKOTITA_TERMDataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.eIDIKOTITA_TERMDataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.eIDIKOTITA_TERMDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.eIDIKOTITA_TERMDataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.eIDIKOTITA_TERMDataTextBox.StyleName = "Data";
            this.eIDIKOTITA_TERMDataTextBox.Value = "=Fields.EIDIKOTITA_TERM";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.00020004430552944541D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.0999996662139893D), Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D));
            this.textBox2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.StyleName = "Data";
            this.textBox2.Value = "= RowNumber()";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.50040006637573242D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.788331985473633D), Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144D));
            // 
            // srepAitisiEidikotites
            // 
            this.DataSource = this.sqlDataSource;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.¡…‘«”«_ Ÿƒ", Telerik.Reporting.FilterOperator.Equal, "=Parameters.aitisiID.Value"));
            group1.GroupFooter = this.sCHOOL_NAMEGroupFooterSection;
            group1.GroupHeader = this.sCHOOL_NAMEGroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.SCHOOL_NAME"));
            group1.Name = "sCHOOL_NAMEGroup";
            group2.GroupFooter = this.labelsGroupFooterSection;
            group2.GroupHeader = this.labelsGroupHeaderSection;
            group2.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.sCHOOL_NAMEGroupHeaderSection,
            this.sCHOOL_NAMEGroupFooterSection,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.detail});
            this.Name = "srepAitisiEidikotites";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AvailableValues.DataSource = this.sqlDataSource;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.¡…‘«”«_ Ÿƒ";
            reportParameter1.AvailableValues.ValueMember = "= Fields.¡…‘«”«_ Ÿƒ";
            reportParameter1.Name = "aitisiID";
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(16.894166946411133D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource;
        private Telerik.Reporting.GroupHeaderSection sCHOOL_NAMEGroupHeaderSection;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.GroupFooterSection sCHOOL_NAMEGroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox eIDIKOTITA_TERMCaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox eIDIKOTITA_TERMDataTextBox;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.Shape shape1;

    }
}