using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.Office.Utils;
using System.Collections.Generic;

namespace CodeBeat_Recipe
{
    public partial class Recipe_Main : DevExpress.XtraReports.UI.XtraReport
    {
        //Personality Variables
        private string recipeName;
        private string modelId;
        private string modelcomponentId;
        private string frameworkId;

        //Design members
        DbManage data;
        PointF myUnboundPoint;
        float pageWidth;

        public Recipe_Main()
        {
            this.myUnboundPoint = new PointF(0, 0);
            this.ReportUnit = ReportUnit.HundredthsOfAnInch;
            this.data = new DbManage();
            this.Margins.Left = 50F;
            this.Margins.Right = 50F;
            this.pageWidth = this.RootReport.PageWidth - 100F;
       
            InitializeComponent();
            
            PopulateReport();

            //AddOCRBlock();
            //AddBarcodeBlock();
            //AddPharamcodeBlock();
            //AddOCRBlock();
            //AddBarcodeBlock();
            //AddArtworkBlock();
            //float k = this.myUnboundPoint.Y;
            //float h = this.GroupBand.HeightF;
            //AddDatamatixBlock();
            //AddBarcodeBlock();
            //k = this.myUnboundPoint.Y;
            //h = this.GroupBand.HeightF;

            //int j = 0;
            
        }

        public Recipe_Main(string name)
        {
            this.myUnboundPoint = new PointF(0, 0);
            this.ReportUnit = ReportUnit.HundredthsOfAnInch;
            this.data = new DbManage();
            this.Margins.Left = 50F;
            this.Margins.Right = 50F;
            this.pageWidth = this.RootReport.PageWidth - 100F;
            this.recipeName = name;

            InitializeComponent();

            PopulateReport();
        }
        
        public float getHeightInHunOfInch(string path)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(path);
            return (float)Units.PixelsToHundredthsOfInch(img.Height, img.VerticalResolution);
        }

        public void MakeDocument()
        {
            this.CreateDocument();
        }

        private void PopulateReport()
        {
            //Find and Set modelId, modelcomponentId for given Recipe Name
            DbManage d1 = new DbManage();
            d1.make_connection();

            this.modelId = d1.SelectAField("SELECT Id FROM span_db.model WHERE Name = '" + this.recipeName + "'");
            this.modelcomponentId = d1.SelectAField("SELECT * FROM span_db.model_component WHERE ModelId = '" + this.modelId + "'");
            this.frameworkId = d1.SelectAField("SELECT * FROM span_db_codebeat_codereader.framework WHERE ModelComponentId = '" + this.modelcomponentId + "'");

            //Get the list of blockIds
            List<string> blockId = new List<string>();
            d1.SelectMultiple("SELECT Id FROM span_db_codebeat_codereader.block WHERE FrameworkId = '" + this.frameworkId + "'", ref blockId);

            for (int i=0; i<blockId.Count; i++)
            {
                string blocktype = d1.SelectAField("SELECT BlockType FROM span_db_codebeat_codereader.block WHERE Id = '" + blockId[0] + "'");

                switch (blocktype)
                {
                    case "0":
                        //Need to add blockId as argument
                        AddPharamcodeBlock();
                        break;
                    case "1":
                        AddDatamatixBlock();
                        break;
                    case "2":

                        break;
                    case "3":
                        AddOCRBlock();
                        break;
                    case "4":
                        break;
                    case "5":
                        AddArtworkBlock();
                        break;
                }
            }
        }

        public void ForBlocks()
        {
            this.data.make_connection();
            this.data.Select("SELECT * " +
                             "FROM span_db_codebeat_codereader.framework " +
                             "WHERE ModelComponentId=116;");
            int noBlock = Convert.ToInt32(this.data.dict["NoOfBlocks"]);

            
            for (int i=0; i<noBlock; i++)
            {
                this.data.Select("SELECT Id, BlockType" +
                                 "FROM span_db_codebeat_codereader.framework" +
                                 "WHERE FrameworkId=50 AND BlockIndex=" + i.ToString() + ";");
                switch (this.data.dict["BlockType"])
                {
                    case "0":
                        AddPharamcodeBlock();
                        break;
                    case "1":
                        
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                }
            }
        }

        public void AddPharamcodeBlock()
        {
            DbManage pData = new DbManage();
            pData.make_connection();

            this.GroupBand.HeightF += 700F;


            XRPanel XPBox = new XRPanel();
            this.GroupBand.Controls.Add(XPBox);
            //this.MyUnboundDetail.Controls.Add(XPBox);
            XPBox.LocationF = new DevExpress.Utils.PointFloat(this.myUnboundPoint.X, 
                                                                this.myUnboundPoint.Y);
            XPBox.WidthF = this.pageWidth;
            XPBox.Borders = DevExpress.XtraPrinting.BorderSide.All;
            XPBox.BorderWidth = 4;

            XRLabel blockName = new XRLabel();
            XPBox.Controls.Add(blockName);
            blockName.LocationF = new DevExpress.Utils.PointFloat(0F, 20F);
            blockName.Text = "Block :1";
            blockName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            blockName.BackColor = System.Drawing.Color.SkyBlue;

            XRPictureBox pic = new XRPictureBox();
            XPBox.Controls.Add(pic);
            pic.LocationF = new DevExpress.Utils.PointFloat((float)this.pageWidth/2,  20F);

            string imgLoc = "C:\\Users\\SPAN_CHAITANYA\\Desktop\\Report_Samples\\images--chaitanya\\r45678_0_product_cavity.bmp";
            pic.ImageUrl = imgLoc;
            pic.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            pic.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
            pic.SizeF = new SizeF((float)this.pageWidth / 2, this.getHeightInHunOfInch(imgLoc));
            blockName.SizeF = pic.SizeF;
            pic.Borders = DevExpress.XtraPrinting.BorderSide.All;
            blockName.Borders = DevExpress.XtraPrinting.BorderSide.Left |
                                DevExpress.XtraPrinting.BorderSide.Top |
                                DevExpress.XtraPrinting.BorderSide.Bottom;
            blockName.BorderWidth = 1; 
            pic.BorderWidth = 1;

            //Update current Point
            this.myUnboundPoint.Y = this.myUnboundPoint.Y + pic.HeightF;

            //Now Adding Tabel
            XRTable table = new XRTable();
            XPBox.Controls.Add(table);
            table.LocationF = new DevExpress.Utils.PointFloat(0F, pic.HeightF+20F);
            table.SizeF = new SizeF(this.pageWidth, 200F);

            //Start Initalization of tabel
            table.BeginInit();

            //Adding 1st Row
            XRTableRow row0 = new XRTableRow();
            row0.HeightF = 50F;
            
            XRTableCell cell0 = new XRTableCell();
            XRTableCell cell1 = new XRTableCell();
            cell0.Text = "Property";
            cell1.Text = "Value";

            row0.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            row0.BackColor = System.Drawing.Color.LightSkyBlue;
            row0.Cells.AddRange(new XRTableCell[]{cell0, cell1});
            table.Rows.Add(row0);

            //Ading 2nd row
            XRTableRow row1 = new XRTableRow();
            row1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell2 = new XRTableCell();
            XRTableCell cell3 = new XRTableCell();
            XRTableCell cell4 = new XRTableCell();
            XRTableCell cell5 = new XRTableCell();

            cell2.Text = "Orientation";
            cell3.Text = "Picket Fence";
            cell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell4.Text = "Taught Value";
            cell5.Text = "99899";
            cell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row1.Cells.AddRange(new XRTableCell[] { cell2, cell3, cell4, cell5 });
            table.Rows.Add(row1);


            //Adding 3rd row
            XRTableRow row2 = new XRTableRow();
            row2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell6 = new XRTableCell();
            XRTableCell cell7 = new XRTableCell();
            XRTableCell cell8 = new XRTableCell();
            XRTableCell cell9 = new XRTableCell();

            cell6.Text = "Direction";
            cell7.Text = "Left to Right";
            cell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell8.Text = "Standards";
            cell9.Text = "Off";
            cell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row2.Cells.AddRange(new XRTableCell[] { cell6, cell7, cell8, cell9 });
            table.Rows.Add(row2);

            //Adding 4th row
            XRTableRow row3 = new XRTableRow();
            row3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell10 = new XRTableCell();
            XRTableCell cell11 = new XRTableCell();
            XRTableCell cell12 = new XRTableCell();
            XRTableCell cell13 = new XRTableCell();

            cell10.Text = "Foreground";
            cell11.Text = "Dark";
            cell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell12.Text = "Extended Search(Px)";
            cell13.Text = "70";
            cell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row3.Cells.AddRange(new XRTableCell[] { cell10, cell11, cell12, cell13 });
            table.Rows.Add(row3);

            //Adding 5th row
            XRTableRow row4 = new XRTableRow();
            row4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell14 = new XRTableCell();
            XRTableCell cell15 = new XRTableCell();
            XRTableCell cell16 = new XRTableCell();
            XRTableCell cell17 = new XRTableCell();

            cell14.Text = "-";
            cell15.Text = "-";
            cell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell16.Text = "Length(%)";
            cell17.Text = "50";
            cell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row4.Cells.AddRange(new XRTableCell[] { cell14, cell15, cell16, cell17 });
            table.Rows.Add(row4);

            //Adding 6th Row
            XRTableRow row5 = new XRTableRow();
            row5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell18 = new XRTableCell();
            XRTableCell cell19 = new XRTableCell();
            XRTableCell cell20 = new XRTableCell();
            XRTableCell cell21 = new XRTableCell();

            cell18.Text = "-";
            cell19.Text = "-";
            cell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell20.Text = "Color";
            cell21.Text = "50";
            cell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row5.Cells.AddRange(new XRTableCell[] { cell18, cell19, cell20, cell21 });
            table.Rows.Add(row5);


            table.EndInit();

            //Updating cuurent point in canvas
            this.myUnboundPoint.Y = this.myUnboundPoint.Y + table.HeightF + 20F;
            XPBox.SizeF = new SizeF(750F, pic.HeightF + table.HeightF + 20F);
            this.GroupBand.HeightF = this.myUnboundPoint.Y;
     
        }

        public void AddOCRBlock()
        {
            this.GroupBand.HeightF += 700F;

            XRPanel XPBox = new XRPanel();
            this.GroupBand.Controls.Add(XPBox);
            //this.MyUnboundDetail.Controls.Add(XPBox);
            XPBox.LocationF = new DevExpress.Utils.PointFloat(this.myUnboundPoint.X,
                                                                this.myUnboundPoint.Y);
            XPBox.WidthF = this.pageWidth;
            XPBox.Borders = DevExpress.XtraPrinting.BorderSide.All;
            XPBox.BorderWidth = 4;

            XRLabel blockName = new XRLabel();
            XPBox.Controls.Add(blockName);
            blockName.LocationF = new DevExpress.Utils.PointFloat(0F, 20F);
            blockName.Text = "Block :1";
            blockName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            blockName.BackColor = System.Drawing.Color.SkyBlue;

            XRPictureBox pic = new XRPictureBox();
            XPBox.Controls.Add(pic);
            pic.LocationF = new DevExpress.Utils.PointFloat((float)this.pageWidth/2, 20F);

            string imgLoc = "C:\\Users\\SPAN_CHAITANYA\\Desktop\\Report_Samples\\images--chaitanya\\r45678_0_product_cavity.bmp";
            pic.ImageUrl = imgLoc;
            pic.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            pic.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
            pic.SizeF = new SizeF((float)this.pageWidth / 2, this.getHeightInHunOfInch(imgLoc));
            blockName.SizeF = pic.SizeF;
            pic.Borders = DevExpress.XtraPrinting.BorderSide.All;
            blockName.Borders = DevExpress.XtraPrinting.BorderSide.Left |
                                DevExpress.XtraPrinting.BorderSide.Top |
                                DevExpress.XtraPrinting.BorderSide.Bottom;
            blockName.BorderWidth = 1;
            pic.BorderWidth = 1;

            //Update current Point
            this.myUnboundPoint.Y = this.myUnboundPoint.Y + pic.HeightF;

            //Now Adding Tabel
            XRTable table = new XRTable();
            XPBox.Controls.Add(table);
            table.LocationF = new DevExpress.Utils.PointFloat(0F, pic.HeightF + 20F);
            table.SizeF = new SizeF(this.pageWidth, 340F);
            table.Font = new DevExpress.Drawing.DXFont("Harrington", 10F);

            table.BeginInit();

            //Adding 1st Row
            XRTableRow row0 = new XRTableRow();
            row0.HeightF = 50F;

            XRTableCell cell0 = new XRTableCell();
            XRTableCell cell1 = new XRTableCell();
            cell0.Text = "Segment";
            cell1.Text = "Property";

            row0.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            row0.BackColor = System.Drawing.Color.LightSkyBlue;
            row0.Cells.AddRange(new XRTableCell[] { cell0, cell1 });
            table.Rows.Add(row0);

            //Ading 2nd row
            XRTableRow row1 = new XRTableRow();
            row1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell2 = new XRTableCell();
            XRTableCell cell3 = new XRTableCell();
            XRTableCell cell4 = new XRTableCell();
            XRTableCell cell5 = new XRTableCell();

            cell2.Text = "Foreground";
            cell3.Text = "Dark";
            cell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell4.Text = "Orientation";
            cell5.Text = "Anti-Clock wise 90 Degree";
            cell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row1.Cells.AddRange(new XRTableCell[] { cell2, cell3, cell4, cell5 });
            table.Rows.Add(row1);

            //Adding 3rd row
            XRTableRow row2 = new XRTableRow();
            row2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell6 = new XRTableCell();
            XRTableCell cell7 = new XRTableCell();
            XRTableCell cell8 = new XRTableCell();
            XRTableCell cell9 = new XRTableCell();

            cell6.Text = "Char Foming";
            cell7.Text = "0";
            cell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell8.Text = "Tilt Orientation";
            cell9.Text = "Off";
            cell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row2.Cells.AddRange(new XRTableCell[] { cell6, cell7, cell8, cell9 });
            table.Rows.Add(row2);

            //Adding 4th row
            XRTableRow row3 = new XRTableRow();
            row3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell10 = new XRTableCell();
            XRTableCell cell11 = new XRTableCell();
            XRTableCell cell12 = new XRTableCell();
            XRTableCell cell13 = new XRTableCell();

            cell10.Text = "Threshold";
            cell11.Text = "Local Threshold - 36";
            cell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell12.Text = "Char Type";
            cell13.Text = "All";
            cell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row3.Cells.AddRange(new XRTableCell[] { cell10, cell11, cell12, cell13 });
            table.Rows.Add(row3);

            //Adding 5th row
            XRTableRow row4 = new XRTableRow();
            row4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell14 = new XRTableCell();
            XRTableCell cell15 = new XRTableCell();
            XRTableCell cell16 = new XRTableCell();
            XRTableCell cell17 = new XRTableCell();

            cell14.Text = "-";
            cell15.Text = "-";
            cell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell16.Text = "Joining";
            cell17.Text = "Off";
            cell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row4.Cells.AddRange(new XRTableCell[] { cell14, cell15, cell16, cell17 });
            table.Rows.Add(row4);

            //Adding 6th Row
            XRTableRow row5 = new XRTableRow();
            row5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell18 = new XRTableCell();
            XRTableCell cell19 = new XRTableCell();
            XRTableCell cell20 = new XRTableCell();
            XRTableCell cell21 = new XRTableCell();

            cell18.Text = "-";
            cell19.Text = "-";
            cell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell20.Text = "Blob Size";
            cell21.Text = "50";
            cell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row5.Cells.AddRange(new XRTableCell[] { cell18, cell19, cell20, cell21 });
            table.Rows.Add(row5);

            //Adding 7th Row
            XRTableRow row6 = new XRTableRow();
            row6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell22 = new XRTableCell();
            XRTableCell cell23 = new XRTableCell();
            XRTableCell cell24 = new XRTableCell();
            XRTableCell cell25 = new XRTableCell();

            cell22.Text = "-";
            cell23.Text = "-";
            cell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell24.Text = "Character Presence";
            cell25.Text = "0";
            cell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row6.Cells.AddRange(new XRTableCell[] { cell22, cell23, cell24, cell25 });
            table.Rows.Add(row6);

            //Adding 8st Row
            XRTableRow row7 = new XRTableRow();
            row7.HeightF = 50F;

            XRTableCell cell26 = new XRTableCell();
            XRTableCell cell27 = new XRTableCell();
            cell26.Text = "Taught Value";
            cell27.Text = "Matching(%)";

            row7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            row7.BackColor = System.Drawing.Color.LightSkyBlue;
            row7.Cells.AddRange(new XRTableCell[] { cell26, cell27 });
            table.Rows.Add(row7);

            //Adding 9th row
            XRTableRow row8 = new XRTableRow();
            row8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row8.HeightF = 60F;

            XRTableCell cell28 = new XRTableCell();
            XRTableCell cell29 = new XRTableCell();

            cell28.Multiline = true;
            cell29.Multiline = true;
            cell28.Text = "This is multiline" + Environment.NewLine + "CHECK";
            cell29.Text = "20 20 20 20 20" + Environment.NewLine + "20 20 20";
            row8.Cells.AddRange(new XRTableCell[] { cell28, cell29 });
            table.Rows.Add(row8);

            table.EndInit();

            //Updating cuurent point in canvas
            this.myUnboundPoint.Y = this.myUnboundPoint.Y + table.HeightF + 20F;
            XPBox.SizeF = new SizeF(750F, pic.HeightF + table.HeightF + 20F);
            this.GroupBand.HeightF = this.myUnboundPoint.Y;

        }

        public void AddBarcodeBlock()
        {
            this.GroupBand.HeightF += 700F;

            //Adding a Panel
            XRPanel XPBox = new XRPanel();
            this.GroupBand.Controls.Add(XPBox);
            //this.MyUnboundDetail.Controls.Add(XPBox);
            XPBox.LocationF = new System.Drawing.PointF(this.myUnboundPoint.X,
                                                                this.myUnboundPoint.Y);
            
            
            XPBox.Borders = DevExpress.XtraPrinting.BorderSide.All;
            XPBox.BorderWidth = 4;
            XPBox.BorderColor = System.Drawing.Color.Red;

            XRLabel blockName = new XRLabel();
            XPBox.Controls.Add(blockName);
            blockName.LocationF = new System.Drawing.PointF(0F, 20F);
            blockName.Text = "Block :1";
            blockName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            blockName.BackColor = System.Drawing.Color.SkyBlue;

            XRPictureBox pic = new XRPictureBox();
            XPBox.Controls.Add(pic);
            pic.LocationF = new DevExpress.Utils.PointFloat((float)this.pageWidth/2, 20F);
            string imgLoc = "C:\\Users\\SPAN_CHAITANYA\\Desktop\\Report_Samples\\images--chaitanya\\r45678_0_product_cavity.bmp";
            pic.ImageUrl = imgLoc;
            pic.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            pic.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
            pic.SizeF = new SizeF((float)this.pageWidth / 2, this.getHeightInHunOfInch(imgLoc));
            blockName.SizeF = pic.SizeF;
            pic.Borders = DevExpress.XtraPrinting.BorderSide.All;
            blockName.Borders = DevExpress.XtraPrinting.BorderSide.Left |
                                DevExpress.XtraPrinting.BorderSide.Top |
                                DevExpress.XtraPrinting.BorderSide.Bottom;
            blockName.BorderWidth = 1;
            pic.BorderWidth = 1;

            //Update current Point
            this.myUnboundPoint.Y = this.myUnboundPoint.Y + pic.HeightF;

            //Create A Table
            XRTable table = new XRTable();
            XPBox.Controls.Add(table);
            table.LocationF = new DevExpress.Utils.PointFloat(0F, pic.HeightF + 20F);
            table.SizeF = new SizeF(this.pageWidth, 110F);

            //Start Initalization of tabel
            table.BeginInit();

            //Adding 1st Row
            XRTableRow row0 = new XRTableRow();
            row0.HeightF = 50F;

            XRTableCell cell0 = new XRTableCell();
            XRTableCell cell1 = new XRTableCell();
            cell0.Text = "Property";
            cell1.Text = "Value";

            row0.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            row0.BackColor = System.Drawing.Color.LightSkyBlue;
            row0.Cells.AddRange(new XRTableCell[] { cell0, cell1 });
            table.Rows.Add(row0);

            //Ading 2nd row
            XRTableRow row1 = new XRTableRow();
            row1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell2 = new XRTableCell();
            XRTableCell cell3 = new XRTableCell();
            XRTableCell cell4 = new XRTableCell();
            XRTableCell cell5 = new XRTableCell();

            cell2.Text = "Type";
            cell3.Text = "Code 128";
            cell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell4.Text = "Taught Value";
            cell5.Text = "4595364";
            cell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row1.Cells.AddRange(new XRTableCell[] { cell2, cell3, cell4, cell5 });
            table.Rows.Add(row1);

            //Adding 3rd row
            XRTableRow row2 = new XRTableRow();
            row2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell6 = new XRTableCell();
            XRTableCell cell7 = new XRTableCell();
            XRTableCell cell8 = new XRTableCell();
            XRTableCell cell9 = new XRTableCell();

            cell6.Text = "Print Quality";
            cell7.Text = "Off";
            cell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell8.Text = "-";
            cell9.Text = "-";
            cell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row2.Cells.AddRange(new XRTableCell[] { cell6, cell7, cell8, cell9 });
            table.Rows.Add(row2);

            table.EndInit();

            float p = XPBox.HeightF;
            float k = this.myUnboundPoint.Y;


            //Updating cuurent point in canvas
            
            this.myUnboundPoint.Y = this.myUnboundPoint.Y +  table.HeightF + 20F;
            XPBox.SizeF = new SizeF(750F, pic.HeightF+table.HeightF+20F);
            this.GroupBand.HeightF = this.myUnboundPoint.Y;
        }

        public void AddDatamatixBlock()
        {
            this.GroupBand.HeightF += 700F;

            XRPanel XPBox = new XRPanel();
            this.GroupBand.Controls.Add(XPBox);
            XPBox.LocationF = new DevExpress.Utils.PointFloat(this.myUnboundPoint.X,
                                                                this.myUnboundPoint.Y);
            XPBox.WidthF = this.pageWidth;

            XPBox.Borders = DevExpress.XtraPrinting.BorderSide.All;
            XPBox.BorderWidth = 4;
            XPBox.BorderColor = System.Drawing.Color.Red;

            XRLabel blockName = new XRLabel();
            XPBox.Controls.Add(blockName);
            blockName.LocationF = new DevExpress.Utils.PointFloat(0F, 20F);
            blockName.Text = "Block :1";
            blockName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            blockName.BackColor = System.Drawing.Color.SkyBlue;

            XRPictureBox pic = new XRPictureBox();
            XPBox.Controls.Add(pic);
            pic.LocationF = new DevExpress.Utils.PointFloat((float)this.pageWidth/2, 20F);
            string imgLoc = "C:\\Users\\SPAN_CHAITANYA\\Desktop\\Report_Samples\\images--chaitanya\\r45678_0_product_cavity.bmp";
            pic.ImageUrl = imgLoc;
            pic.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            pic.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
            pic.SizeF = new SizeF((float)this.pageWidth / 2, this.getHeightInHunOfInch(imgLoc));
            blockName.SizeF = pic.SizeF;
            pic.Borders = DevExpress.XtraPrinting.BorderSide.All;
            blockName.Borders = DevExpress.XtraPrinting.BorderSide.Left |
                                DevExpress.XtraPrinting.BorderSide.Top |
                                DevExpress.XtraPrinting.BorderSide.Bottom;
            blockName.BorderWidth = 1;
            pic.BorderWidth = 1;

            //Update current Point
            this.myUnboundPoint.Y = this.myUnboundPoint.Y + pic.HeightF;

            //Create A Table
            XRTable table = new XRTable();
            XPBox.Controls.Add(table);
            table.LocationF = new DevExpress.Utils.PointFloat(0F, pic.HeightF + 20F);
            table.SizeF = new SizeF(this.pageWidth, 170F);

            //Start Initalization of tabel
            table.BeginInit();

            //Adding 1st Row
            XRTableRow row0 = new XRTableRow();
            row0.HeightF = 50F;

            XRTableCell cell0 = new XRTableCell();
            XRTableCell cell1 = new XRTableCell();
            cell0.Text = "Property";
            cell1.Text = "Value";

            row0.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            row0.BackColor = System.Drawing.Color.LightSkyBlue;
            row0.Cells.AddRange(new XRTableCell[] { cell0, cell1 });
            table.Rows.Add(row0);

            //Ading 2nd row
            XRTableRow row1 = new XRTableRow();
            row1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell2 = new XRTableCell();
            XRTableCell cell3 = new XRTableCell();
            XRTableCell cell4 = new XRTableCell();
            XRTableCell cell5 = new XRTableCell();

            cell2.Text = "Type";
            cell3.Text = "Data Matrix ECC 200";
            cell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell4.Text = "Taught Value";
            cell5.Text = "SPAN INSPECTION SYSTEM PVT. LTD.";
            cell5.Multiline = true;
            cell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row1.Cells.AddRange(new XRTableCell[] { cell2, cell3, cell4, cell5 });
            table.Rows.Add(row1);

            //Add 3nd Row
            XRTableRow row2 = new XRTableRow();
            row2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell6 = new XRTableCell();
            XRTableCell cell7 = new XRTableCell();
            XRTableCell cell8 = new XRTableCell();
            XRTableCell cell9 = new XRTableCell();

            cell6.Text = "Speed";
            cell7.Text = "Maximum";
            cell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell8.Text = "--";
            cell9.Text = "--";
            cell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row2.Cells.AddRange(new XRTableCell[] { cell6, cell7, cell8, cell9 });
            table.Rows.Add(row2);

            //Add 4th row
            XRTableRow row3 = new XRTableRow();
            row3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell10 = new XRTableCell();
            XRTableCell cell11 = new XRTableCell();
            XRTableCell cell12 = new XRTableCell();
            XRTableCell cell13 = new XRTableCell();

            cell10.Text = "Polarity";
            cell11.Text = "Dark On Light";
            cell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell12.Text = "--";
            cell13.Text = "--";
            cell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row3.Cells.AddRange(new XRTableCell[] { cell10, cell11, cell12, cell13 });
            table.Rows.Add(row3);

            //Add 5th Row
            XRTableRow row4 = new XRTableRow();
            row4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell14 = new XRTableCell();
            XRTableCell cell15 = new XRTableCell();
            XRTableCell cell16 = new XRTableCell();
            XRTableCell cell17 = new XRTableCell();

            cell14.Text = "Print Quality";
            cell15.Text = "Off";
            cell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell16.Text = "--";
            cell17.Text = "--";
            cell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row4.Cells.AddRange(new XRTableCell[] { cell14, cell15, cell16, cell17 });
            table.Rows.Add(row4);

            //End Table
            table.EndInit();

            float i = pic.HeightF;
            float j = table.HeightF;
            float h = this.myUnboundPoint.Y;

            //Updating cuurent point in canvas
            this.myUnboundPoint.Y = this.myUnboundPoint.Y + table.HeightF + 20F;
            XPBox.SizeF = new SizeF(750F, pic.HeightF + table.HeightF + 20F);
            this.GroupBand.HeightF = this.myUnboundPoint.Y;
        }

        public void AddArtworkBlock()
        {
            this.GroupBand.HeightF += 700F;

            //Adding a Panel
            XRPanel XPBox = new XRPanel();
            this.GroupBand.Controls.Add(XPBox);
            //this.MyUnboundDetail.Controls.Add(XPBox);
            XPBox.LocationF = new System.Drawing.PointF(this.myUnboundPoint.X,
                                                                this.myUnboundPoint.Y);
            XPBox.WidthF = this.pageWidth;
            
            XPBox.Borders = DevExpress.XtraPrinting.BorderSide.All;
            XPBox.BorderWidth = 4;
            XPBox.BorderColor = System.Drawing.Color.Red;

            XRLabel blockName = new XRLabel();
            XPBox.Controls.Add(blockName);
            blockName.LocationF = new DevExpress.Utils.PointFloat(0F, 20F);
            blockName.Text = "Block :1";
            blockName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            blockName.BackColor = System.Drawing.Color.SkyBlue;

            XRPictureBox pic = new XRPictureBox();
            XPBox.Controls.Add(pic);
            pic.LocationF = new DevExpress.Utils.PointFloat((float)this.pageWidth/2, 20F);
            string imgLoc = "C:\\Users\\SPAN_CHAITANYA\\Desktop\\Report_Samples\\images--chaitanya\\r45678_0_product_cavity.bmp";
            pic.ImageUrl = imgLoc;
            pic.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            pic.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
            pic.SizeF = new SizeF((float)this.pageWidth / 2, this.getHeightInHunOfInch(imgLoc));
            blockName.SizeF = pic.SizeF;
            pic.Borders = DevExpress.XtraPrinting.BorderSide.All;
            blockName.Borders = DevExpress.XtraPrinting.BorderSide.Left |
                                DevExpress.XtraPrinting.BorderSide.Top |
                                DevExpress.XtraPrinting.BorderSide.Bottom;
            blockName.BorderWidth = 1;
            pic.BorderWidth = 1;

            //Update current Point
            this.myUnboundPoint.Y = this.myUnboundPoint.Y + pic.HeightF;

            //Create A Table
            XRTable table = new XRTable();
            XPBox.Controls.Add(table);
            table.LocationF = new DevExpress.Utils.PointFloat(0F, pic.HeightF + 20F);
            table.SizeF = new SizeF(this.pageWidth, 110F);

            //Start Initalization of tabel
            table.BeginInit();

            //Adding 1st Row
            XRTableRow row0 = new XRTableRow();
            row0.HeightF = 50F;

            XRTableCell cell0 = new XRTableCell();
            XRTableCell cell1 = new XRTableCell();
            cell0.Text = "Property";
            cell1.Text = "Value";

            row0.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            row0.BackColor = System.Drawing.Color.LightSkyBlue;
            row0.Cells.AddRange(new XRTableCell[] { cell0, cell1 });
            table.Rows.Add(row0);

            //Ading 2nd row
            XRTableRow row1 = new XRTableRow();
            row1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell2 = new XRTableCell();
            XRTableCell cell3 = new XRTableCell();
            XRTableCell cell4 = new XRTableCell();
            XRTableCell cell5 = new XRTableCell();

            cell2.Text = "Type";
            cell3.Text = "Gray";
            cell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell4.Text = "Mismatch(%)";
            cell5.Text = "9";
            cell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row1.Cells.AddRange(new XRTableCell[] { cell2, cell3, cell4, cell5 });
            table.Rows.Add(row1);

            //Adding 3rd row
            XRTableRow row2 = new XRTableRow();
            row2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            XRTableCell cell6 = new XRTableCell();
            XRTableCell cell7 = new XRTableCell();
            XRTableCell cell8 = new XRTableCell();
            XRTableCell cell9 = new XRTableCell();

            cell6.Text = "Speed";
            cell7.Text = "Medium";
            cell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            cell8.Text = "-";
            cell9.Text = "-";
            cell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            row2.Cells.AddRange(new XRTableCell[] { cell6, cell7, cell8, cell9 });
            table.Rows.Add(row2);

            table.EndInit();

            //Updating cuurent point in canvas
            
            this.myUnboundPoint.Y = this.myUnboundPoint.Y + table.HeightF + 20F ;
            XPBox.SizeF = new SizeF(750F, pic.HeightF+table.HeightF+20F);
            this.GroupBand.HeightF = this.myUnboundPoint.Y;
          
            
        }

        private void Recipe_Main_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
