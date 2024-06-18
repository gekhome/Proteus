namespace Proteus.Reports
{
    partial class LogoA2
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogoA2));
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.sCHOOL_PHONECaptionTextBox = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.sCHOOL_EMAILDataTextBox = new Telerik.Reporting.TextBox();
            this.sCHOOL_INFODataTextBox = new Telerik.Reporting.TextBox();
            this.sCHOOL_NAMEDataTextBox = new Telerik.Reporting.TextBox();
            this.sCHOOL_PHONEDataTextBox = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "Proteus.Properties.Settings.DBConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.SelectCommand = "SELECT        A2_DEPARTEMENT, A2_TMIMA, A2_ADDRESS, A2_INFO, A2_PHONE, A2_FAX, A2" +
    "_EMAIL\r\nFROM            A2_DATA";
            // 
            // sCHOOL_PHONECaptionTextBox
            // 
            this.sCHOOL_PHONECaptionTextBox.CanGrow = true;
            this.sCHOOL_PHONECaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010093052696902305D), Telerik.Reporting.Drawing.Unit.Cm(3.4060330390930176D));
            this.sCHOOL_PHONECaptionTextBox.Name = "sCHOOL_PHONECaptionTextBox";
            this.sCHOOL_PHONECaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.0470825433731079D), Telerik.Reporting.Drawing.Unit.Cm(0.50563287734985352D));
            this.sCHOOL_PHONECaptionTextBox.Style.Font.Bold = true;
            this.sCHOOL_PHONECaptionTextBox.Style.Font.Name = "Calibri";
            this.sCHOOL_PHONECaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.sCHOOL_PHONECaptionTextBox.StyleName = "Caption";
            this.sCHOOL_PHONECaptionTextBox.Value = "Τηλ.";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(4.41749906539917D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.sCHOOL_EMAILDataTextBox,
            this.sCHOOL_INFODataTextBox,
            this.sCHOOL_NAMEDataTextBox,
            this.sCHOOL_PHONEDataTextBox,
            this.pictureBox1,
            this.textBox1,
            this.sCHOOL_PHONECaptionTextBox,
            this.textBox2,
            this.textBox4,
            this.textBox5});
            this.detail.Name = "detail";
            // 
            // sCHOOL_EMAILDataTextBox
            // 
            this.sCHOOL_EMAILDataTextBox.CanGrow = true;
            this.sCHOOL_EMAILDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.0473835468292236D), Telerik.Reporting.Drawing.Unit.Cm(3.9118661880493164D));
            this.sCHOOL_EMAILDataTextBox.Name = "sCHOOL_EMAILDataTextBox";
            this.sCHOOL_EMAILDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.29979944229126D), Telerik.Reporting.Drawing.Unit.Cm(0.50563246011734009D));
            this.sCHOOL_EMAILDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.sCHOOL_EMAILDataTextBox.StyleName = "Data";
            this.sCHOOL_EMAILDataTextBox.Value = "= Fields.A2_EMAIL";
            // 
            // sCHOOL_INFODataTextBox
            // 
            this.sCHOOL_INFODataTextBox.CanGrow = true;
            this.sCHOOL_INFODataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.2002005577087402D), Telerik.Reporting.Drawing.Unit.Cm(2.9002001285552979D));
            this.sCHOOL_INFODataTextBox.Name = "sCHOOL_INFODataTextBox";
            this.sCHOOL_INFODataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.1469821929931641D), Telerik.Reporting.Drawing.Unit.Cm(0.50563287734985352D));
            this.sCHOOL_INFODataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.sCHOOL_INFODataTextBox.StyleName = "Data";
            this.sCHOOL_INFODataTextBox.Value = "= Fields.A2_INFO";
            // 
            // sCHOOL_NAMEDataTextBox
            // 
            this.sCHOOL_NAMEDataTextBox.CanGrow = true;
            this.sCHOOL_NAMEDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(1.2001998424530029D));
            this.sCHOOL_NAMEDataTextBox.Name = "sCHOOL_NAMEDataTextBox";
            this.sCHOOL_NAMEDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.3469839096069336D), Telerik.Reporting.Drawing.Unit.Cm(0.69980019330978394D));
            this.sCHOOL_NAMEDataTextBox.Style.Font.Bold = true;
            this.sCHOOL_NAMEDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.sCHOOL_NAMEDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.sCHOOL_NAMEDataTextBox.StyleName = "Data";
            this.sCHOOL_NAMEDataTextBox.Value = "= Fields.A2_DEPARTEMENT";
            // 
            // sCHOOL_PHONEDataTextBox
            // 
            this.sCHOOL_PHONEDataTextBox.CanGrow = true;
            this.sCHOOL_PHONEDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.0473835468292236D), Telerik.Reporting.Drawing.Unit.Cm(3.4060330390930176D));
            this.sCHOOL_PHONEDataTextBox.Name = "sCHOOL_PHONEDataTextBox";
            this.sCHOOL_PHONEDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.29979944229126D), Telerik.Reporting.Drawing.Unit.Cm(0.50563287734985352D));
            this.sCHOOL_PHONEDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.sCHOOL_PHONEDataTextBox.StyleName = "Data";
            this.sCHOOL_PHONEDataTextBox.Value = "= Fields.A2_PHONE";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.pictureBox1.MimeType = "image/png";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.700000524520874D), Telerik.Reporting.Drawing.Unit.Cm(1.1999999284744263D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(2.400200366973877D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.34708309173584D), Telerik.Reporting.Drawing.Unit.Cm(0.4997994601726532D));
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox1.StyleName = "Data";
            this.textBox1.Value = "= Fields.A2_ADDRESS";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(2.9002001285552979D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2000002861022949D), Telerik.Reporting.Drawing.Unit.Cm(0.50563287734985352D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Calibri";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox2.StyleName = "Caption";
            this.textBox2.Value = "Πληροφορίες";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(3.9118661880493164D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.0470833778381348D), Telerik.Reporting.Drawing.Unit.Cm(0.50563287734985352D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Calibri";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox4.StyleName = "Caption";
            this.textBox4.Value = "E-mail";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010093052696902305D), Telerik.Reporting.Drawing.Unit.Cm(1.9002002477645874D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.3469829559326172D), Telerik.Reporting.Drawing.Unit.Cm(0.49979987740516663D));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.StyleName = "Data";
            this.textBox5.Value = "= Fields.A2_TMIMA";
            // 
            // LogoA2
            // 
            this.DataSource = this.sqlDataSource1;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
            this.Name = "LogoIek";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(3D), Telerik.Reporting.Drawing.Unit.Mm(3D), Telerik.Reporting.Drawing.Unit.Mm(3D), Telerik.Reporting.Drawing.Unit.Mm(3D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(8.3471841812133789D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.TextBox sCHOOL_PHONECaptionTextBox;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox sCHOOL_EMAILDataTextBox;
        private Telerik.Reporting.TextBox sCHOOL_INFODataTextBox;
        private Telerik.Reporting.TextBox sCHOOL_NAMEDataTextBox;
        private Telerik.Reporting.TextBox sCHOOL_PHONEDataTextBox;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;

    }
}