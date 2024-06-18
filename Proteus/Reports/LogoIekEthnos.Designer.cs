namespace Proteus.Reports
{
    partial class LogoIekEthnos
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogoIekEthnos));
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.sCHOOL_PHONECaptionTextBox = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.sCHOOL_EMAILDataTextBox = new Telerik.Reporting.TextBox();
            this.sCHOOL_FAXDataTextBox = new Telerik.Reporting.TextBox();
            this.sCHOOL_INFODataTextBox = new Telerik.Reporting.TextBox();
            this.sCHOOL_NAMEDataTextBox = new Telerik.Reporting.TextBox();
            this.sCHOOL_PHONEDataTextBox = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "Proteus.Properties.Settings.DBConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.SelectCommand = "SELECT        SCHOOL_ID, SCHOOL_NAME, SCHOOL_EMAIL, SCHOOL_PHONE, SCHOOL_FAX, SCH" +
    "OOL_INFO, ADDRESS\r\nFROM            repLOGO_IEK";
            // 
            // sCHOOL_PHONECaptionTextBox
            // 
            this.sCHOOL_PHONECaptionTextBox.CanGrow = true;
            this.sCHOOL_PHONECaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.0529174730181694D), Telerik.Reporting.Drawing.Unit.Cm(3.400399923324585D));
            this.sCHOOL_PHONECaptionTextBox.Name = "sCHOOL_PHONECaptionTextBox";
            this.sCHOOL_PHONECaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.0470825433731079D), Telerik.Reporting.Drawing.Unit.Cm(0.50563287734985352D));
            this.sCHOOL_PHONECaptionTextBox.Style.Font.Bold = true;
            this.sCHOOL_PHONECaptionTextBox.StyleName = "Caption";
            this.sCHOOL_PHONECaptionTextBox.Value = "Ôçë.:";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(4.9178991317749023D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.sCHOOL_EMAILDataTextBox,
            this.sCHOOL_FAXDataTextBox,
            this.sCHOOL_INFODataTextBox,
            this.sCHOOL_NAMEDataTextBox,
            this.sCHOOL_PHONEDataTextBox,
            this.pictureBox1,
            this.textBox1,
            this.sCHOOL_PHONECaptionTextBox,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox5});
            this.detail.Name = "detail";
            // 
            // sCHOOL_EMAILDataTextBox
            // 
            this.sCHOOL_EMAILDataTextBox.CanGrow = true;
            this.sCHOOL_EMAILDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.5068669319152832D), Telerik.Reporting.Drawing.Unit.Cm(4.4122662544250488D));
            this.sCHOOL_EMAILDataTextBox.Name = "sCHOOL_EMAILDataTextBox";
            this.sCHOOL_EMAILDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.7931327819824219D), Telerik.Reporting.Drawing.Unit.Cm(0.50563246011734009D));
            this.sCHOOL_EMAILDataTextBox.StyleName = "Data";
            this.sCHOOL_EMAILDataTextBox.Value = "=Fields.SCHOOL_EMAIL";
            // 
            // sCHOOL_FAXDataTextBox
            // 
            this.sCHOOL_FAXDataTextBox.CanGrow = true;
            this.sCHOOL_FAXDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.1002002954483032D), Telerik.Reporting.Drawing.Unit.Cm(3.9062330722808838D));
            this.sCHOOL_FAXDataTextBox.Name = "sCHOOL_FAXDataTextBox";
            this.sCHOOL_FAXDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.1997995376586914D), Telerik.Reporting.Drawing.Unit.Cm(0.50583350658416748D));
            this.sCHOOL_FAXDataTextBox.StyleName = "Data";
            this.sCHOOL_FAXDataTextBox.Value = "=Fields.SCHOOL_FAX";
            // 
            // sCHOOL_INFODataTextBox
            // 
            this.sCHOOL_INFODataTextBox.CanGrow = true;
            this.sCHOOL_INFODataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.6797919273376465D), Telerik.Reporting.Drawing.Unit.Cm(2.8945667743682861D));
            this.sCHOOL_INFODataTextBox.Name = "sCHOOL_INFODataTextBox";
            this.sCHOOL_INFODataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.6202077865600586D), Telerik.Reporting.Drawing.Unit.Cm(0.50563287734985352D));
            this.sCHOOL_INFODataTextBox.StyleName = "Data";
            this.sCHOOL_INFODataTextBox.Value = "=Fields.SCHOOL_INFO";
            // 
            // sCHOOL_NAMEDataTextBox
            // 
            this.sCHOOL_NAMEDataTextBox.CanGrow = true;
            this.sCHOOL_NAMEDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(1.8001995086669922D));
            this.sCHOOL_NAMEDataTextBox.Name = "sCHOOL_NAMEDataTextBox";
            this.sCHOOL_NAMEDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.2470831871032715D), Telerik.Reporting.Drawing.Unit.Cm(0.494166761636734D));
            this.sCHOOL_NAMEDataTextBox.Style.Font.Bold = true;
            this.sCHOOL_NAMEDataTextBox.StyleName = "Data";
            this.sCHOOL_NAMEDataTextBox.Value = "=Fields.SCHOOL_NAME";
            // 
            // sCHOOL_PHONEDataTextBox
            // 
            this.sCHOOL_PHONEDataTextBox.CanGrow = true;
            this.sCHOOL_PHONEDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.1002000570297241D), Telerik.Reporting.Drawing.Unit.Cm(3.400399923324585D));
            this.sCHOOL_PHONEDataTextBox.Name = "sCHOOL_PHONEDataTextBox";
            this.sCHOOL_PHONEDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.1997995376586914D), Telerik.Reporting.Drawing.Unit.Cm(0.50563287734985352D));
            this.sCHOOL_PHONEDataTextBox.StyleName = "Data";
            this.sCHOOL_PHONEDataTextBox.Value = "=Fields.SCHOOL_PHONE";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.pictureBox1.MimeType = "image/png";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7470835447311401D), Telerik.Reporting.Drawing.Unit.Cm(1.2999998331069946D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(2.2945671081542969D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.2470831871032715D), Telerik.Reporting.Drawing.Unit.Cm(0.59979981184005737D));
            this.textBox1.StyleName = "Data";
            this.textBox1.Value = "= Fields.ADDRESS";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(2.8945667743682861D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.6267752647399902D), Telerik.Reporting.Drawing.Unit.Cm(0.50563287734985352D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.StyleName = "Caption";
            this.textBox2.Value = "Ðëçñïöïñßåò:";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.05291767418384552D), Telerik.Reporting.Drawing.Unit.Cm(3.9062330722808838D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.0470825433731079D), Telerik.Reporting.Drawing.Unit.Cm(0.50583314895629883D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = "Fax.:";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(4.4122662544250488D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4537502527236939D), Telerik.Reporting.Drawing.Unit.Cm(0.50563287734985352D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.StyleName = "Caption";
            this.textBox4.Value = "E-mail:";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(1.3001996278762817D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.2470831871032715D), Telerik.Reporting.Drawing.Unit.Cm(0.499799907207489D));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.textBox5.StyleName = "Data";
            this.textBox5.Value = "ÄÇÌÏÓÉÁ ÕÐÇÑÅÓÉÁ ÁÐÁÓ×ÏËÇÓÇÓ";
            // 
            // LogoIekEthnos
            // 
            this.DataSource = this.sqlDataSource1;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.SCHOOL_ID", Telerik.Reporting.FilterOperator.Equal, "=Parameters.schoolID.Value"));
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
            this.Name = "LogoIek";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(3D), Telerik.Reporting.Drawing.Unit.Mm(3D), Telerik.Reporting.Drawing.Unit.Mm(3D), Telerik.Reporting.Drawing.Unit.Mm(3D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlDataSource1;
            reportParameter1.AvailableValues.ValueMember = "= Fields.SCHOOL_ID";
            reportParameter1.Name = "schoolID";
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(7.2999997138977051D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.TextBox sCHOOL_PHONECaptionTextBox;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox sCHOOL_EMAILDataTextBox;
        private Telerik.Reporting.TextBox sCHOOL_FAXDataTextBox;
        private Telerik.Reporting.TextBox sCHOOL_INFODataTextBox;
        private Telerik.Reporting.TextBox sCHOOL_NAMEDataTextBox;
        private Telerik.Reporting.TextBox sCHOOL_PHONEDataTextBox;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;

    }
}