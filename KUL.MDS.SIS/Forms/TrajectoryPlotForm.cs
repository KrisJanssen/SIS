// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrajectoryPlotForm.cs" company="">
//   
// </copyright>
// <summary>
//   The trajectory plot form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using ZedGraph.ZedGraph;

    /// <summary>
    /// The trajectory plot form.
    /// </summary>
    public partial class TrajectoryPlotForm : Form
    {
        #region Fields

        /// <summary>
        /// The m_ nm coordinates.
        /// </summary>
        private double[,] m_NMCoordinates;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrajectoryPlotForm"/> class.
        /// </summary>
        public TrajectoryPlotForm()
        {
            this.InitializeComponent();

            GraphPane XCoordPane = this.XCoordGraph.GraphPane;
            GraphPane YCoordPane = this.YCoordGraph.GraphPane;
            GraphPane XYCoordPane = this.XYCoordGraph.GraphPane;

            XCoordPane.Title.Text = "X Wave Points";
            XCoordPane.XAxis.Title.Text = "Point";
            XCoordPane.YAxis.Title.Text = "Amplitude (um)";

            YCoordPane.Title.Text = "Y Wave Points";
            YCoordPane.XAxis.Title.Text = "Point";
            YCoordPane.YAxis.Title.Text = "Amplitude (um)";

            XYCoordPane.Title.Text = "Single Period coordinates";
            XYCoordPane.XAxis.Title.Text = "X Amplitude (um)";
            XYCoordPane.YAxis.Title.Text = "Y Amplitude (um)";

            this.tabPage1.Text = "X";
            this.tabPage2.Text = "Y";
            this.tabPage3.Text = "XY";

            this.DataChanged += new EventHandler(this.TrajectoryPlotForm_DataChanged);
        }

        #endregion

        #region Events

        /// <summary>
        /// The data changed.
        /// </summary>
        private event EventHandler DataChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Sets the nm coordinates.
        /// </summary>
        public double[,] NMCoordinates
        {
            set
            {
                this.m_NMCoordinates = value;
                if (this.DataChanged != null)
                {
                    this.DataChanged(this, new EventArgs());
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The trajectory plot form_ data changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TrajectoryPlotForm_DataChanged(object sender, EventArgs e)
        {
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            PointPairList list3 = new PointPairList();

            for (int i = 0; i < (int)(this.m_NMCoordinates.Length / 2); i++)
            {
                list1.Add(i, this.m_NMCoordinates[0, i]);
                list2.Add(i, this.m_NMCoordinates[1, i]);
                list3.Add(this.m_NMCoordinates[0, i], this.m_NMCoordinates[1, i]);
            }

            this.XCoordGraph.GraphPane.CurveList.Clear();
            this.YCoordGraph.GraphPane.CurveList.Clear();
            this.XYCoordGraph.GraphPane.CurveList.Clear();

            LineItem myCurve1 = this.XCoordGraph.GraphPane.AddCurve("X", list1, Color.Red, SymbolType.None);

            LineItem myCurve2 = this.YCoordGraph.GraphPane.AddCurve("Y", list2, Color.Red, SymbolType.None);

            LineItem myCurve3 = this.XYCoordGraph.GraphPane.AddCurve("XY", list3, Color.Red, SymbolType.None);

            this.XCoordGraph.AxisChange();
            this.YCoordGraph.AxisChange();
            this.XYCoordGraph.AxisChange();

            this.XCoordGraph.Invalidate();
            this.YCoordGraph.Invalidate();
            this.XYCoordGraph.Invalidate();
        }

        /// <summary>
        /// The trajectory plot form_ form closing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TrajectoryPlotForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                return;
            }

            e.Cancel = true;
            this.Hide();
        }

        #endregion
    }
}