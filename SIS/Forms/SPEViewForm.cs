//using System;
//using System.Collections;
//using System.ComponentModel;
//using System.Drawing;
//using System.Windows.Forms;
//using KUL.MDS.SIS.Documents;
//using System.Reflection;
//using ZedGraph;

//namespace KUL.MDS.SIS.Forms
//{
//    public class SPEViewForm : KUL.MDS.MDITemplate.MdiViewForm
//    {
//        private ZedGraph.ZedGraphControl zedGraphControl1;
//        private System.ComponentModel.IContainer components = null;

//        public SPEViewForm()
//        {
//            // This call is required by the Windows Form Designer.
//            InitializeComponent();

//            // TODO: Add any initialization after the InitializeComponent call
//        }

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        protected override void Dispose( bool disposing )
//        {
//            if( disposing )
//            {
//                if (components != null) 
//                {
//                    components.Dispose();
//                }
//            }
//            base.Dispose( disposing );
//        }

//        #region Designer generated code
//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.components = new System.ComponentModel.Container();
//            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
//            this.SuspendLayout();
//            // 
//            // zedGraphControl1
//            // 
//            this.zedGraphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.zedGraphControl1.Location = new System.Drawing.Point(0, 0);
//            this.zedGraphControl1.Name = "zedGraphControl1";
//            this.zedGraphControl1.ScrollGrace = 0;
//            this.zedGraphControl1.ScrollMaxX = 0;
//            this.zedGraphControl1.ScrollMaxY = 0;
//            this.zedGraphControl1.ScrollMaxY2 = 0;
//            this.zedGraphControl1.ScrollMinX = 0;
//            this.zedGraphControl1.ScrollMinY = 0;
//            this.zedGraphControl1.ScrollMinY2 = 0;
//            this.zedGraphControl1.Size = new System.Drawing.Size(737, 396);
//            this.zedGraphControl1.TabIndex = 0;
//            // 
//            // SPEViewForm
//            // 
//            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
//            this.ClientSize = new System.Drawing.Size(737, 396);
//            this.Controls.Add(this.zedGraphControl1);
//            this.Name = "SPEViewForm";
//            this.ResumeLayout(false);

//        }
//        #endregion

//        protected override void OnUpdateDocument()
//        {
//        }

//        protected override void OnUpdateView(object update)
//        {
//        }

//        protected override void OnInitialUpdate()
//        {

//            CreateGraph(zedGraphControl1);
//        }

//        private void CreateGraph(ZedGraphControl zgc)
//        {
//            SPEDocument _docDocument = this.Document as SPEDocument;

//            KUL.MDS.Data.Column XCol = _docDocument.DataSet.GetBlock(0).GetColumn(1);
//            KUL.MDS.Data.Column YCol = _docDocument.DataSet.GetBlock(0).GetColumn(2);

//            // get a reference to the GraphPane

//            GraphPane myPane = zgc.GraphPane;

//            // Set the Titles

//            myPane.Title.Text = "My Test Graph\n(For CodeProject Sample)";
//            myPane.XAxis.Title.Text = "My X Axis";
//            myPane.YAxis.Title.Text = "My Y Axis";

//            // Make up some data arrays based on the Sine function

//            double x, y1, y2;
//            PointPairList list1 = new PointPairList();

//            for (int i = 0; i < (int)(XCol.GetPointCount()); i++)
//            {
//                list1.Add((double)(XCol.GetValue(i)), (double)(YCol.GetValue(i)));
//            }

//            // Generate a red curve with diamond

//            // symbols, and "Porsche" in the legend

//            LineItem myCurve = myPane.AddCurve("Porsche",
//                  list1, Color.Red, SymbolType.Diamond);

//            // Generate a blue curve with circle

//            // Tell ZedGraph to refigure the

//            // axes since the data have changed

//            zgc.AxisChange();
//        }
//    }
//}

