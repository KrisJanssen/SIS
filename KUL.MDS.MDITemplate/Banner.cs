// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Banner.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The banner.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    using SIS.Library;
    using SIS.Resources;
    using SIS.Systemlayer;

    /// <summary>
    /// The banner.
    /// </summary>
    public sealed class Banner : Control
    {
        #region Static Fields

        /// <summary>
        /// The default size.
        /// </summary>
        private static readonly Size defaultSize = new Size(495, 71);

        #endregion

        #region Fields

        /// <summary>
        /// The banner alpha.
        /// </summary>
        private float bannerAlpha = 1.0f;

        /// <summary>
        /// The banner image.
        /// </summary>
        private PictureBox bannerImage;

        /// <summary>
        /// The banner index.
        /// </summary>
        private int bannerIndex = 0;

        /// <summary>
        /// The banner text.
        /// </summary>
        private Label bannerText;

        // for awhile there, this control was designed to fade between several different
        // images. for 3.0 this did not end up being the case, but the functionality is
        // still here
        /// <summary>
        /// The banner timer.
        /// </summary>
        private Timer bannerTimer;

        /// <summary>
        /// The banners.
        /// </summary>
        private Image[] banners = new[] { Resources.GetImageResource("Images.Banner.png").GetCopy(), };

        /// <summary>
        /// The first tick.
        /// </summary>
        private int firstTick = Environment.TickCount;

        /// <summary>
        /// The high quality bmp.
        /// </summary>
        private Bitmap highQualityBmp = null;

        /// <summary>
        /// The index offset.
        /// </summary>
        private int indexOffset;

        /// <summary>
        /// The logo and gradient.
        /// </summary>
        private Bitmap logoAndGradient = new Bitmap(495, 71, PixelFormat.Format24bppRgb);

        /// <summary>
        /// The pdn logo.
        /// </summary>
        private Image pdnLogo = Resources.GetImageResource("Images.TransparentLogo.png").GetCopy();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Banner"/> class.
        /// </summary>
        public Banner()
        {
            this.InitializeComponent();
            this.indexOffset = new Random().Next(this.banners.Length);
            this.SetUpBannerImage();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the banner font.
        /// </summary>
        public Font BannerFont
        {
            get
            {
                return (Font)this.bannerText.Font.Clone();
            }

            set
            {
                this.bannerText.Font = (Font)value.Clone();
            }
        }

        /// <summary>
        /// Gets or sets the banner text.
        /// </summary>
        public string BannerText
        {
            get
            {
                return this.bannerText.Text;
            }

            set
            {
                this.bannerText.Text = value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the default size.
        /// </summary>
        protected override Size DefaultSize
        {
            get
            {
                return defaultSize;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.bannerTimer != null)
                {
                    this.bannerTimer.Dispose();
                    this.bannerTimer = null;
                }

                if (this.pdnLogo != null)
                {
                    this.pdnLogo.Dispose();
                    this.pdnLogo = null;
                }

                if (this.logoAndGradient != null)
                {
                    this.logoAndGradient.Dispose();
                    this.logoAndGradient = null;
                }

                if (this.highQualityBmp != null)
                {
                    this.highQualityBmp.Dispose();
                    this.highQualityBmp = null;
                }

                if (this.banners != null)
                {
                    foreach (Image image in this.banners)
                    {
                        if (image != null)
                        {
                            image.Dispose();
                        }
                    }

                    this.banners = null;
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// The banner image_ size changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BannerImage_SizeChanged(object sender, EventArgs e)
        {
            this.SetUpBannerImage();
        }

        /// <summary>
        /// The banner timer_ tick.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BannerTimer_Tick(object sender, EventArgs e)
        {
            Form findForm = this.FindForm();

            if (findForm != null && findForm.WindowState != FormWindowState.Minimized)
            {
                const int bannerUpDuration = 4000;
                const int bannerFadeDuration = 2000;
                const int bannerPeriod = bannerUpDuration + bannerFadeDuration;
                int ticks = unchecked(Environment.TickCount - this.firstTick + bannerUpDuration / 2);
                int localTick = ticks % bannerPeriod;

                double a;
                if (localTick < bannerUpDuration)
                {
                    a = 1.0;
                }
                else
                {
                    int fadeTick = localTick - bannerUpDuration;
                    a = (double)(bannerFadeDuration - fadeTick) / (double)bannerFadeDuration;
                    a = 1.0 - a;
                    a = a * a;
                    a = 1.0 - a;
                }

                int newBannerIndex = ticks / bannerPeriod;
                float newBannerAlpha = (float)a;

                if (this.banners.Length < 2 || UserSessions.IsRemote)
                {
                    newBannerAlpha = 1.0f;
                }

                if (newBannerAlpha != this.bannerAlpha || newBannerIndex != this.bannerIndex)
                {
                    this.bannerAlpha = newBannerAlpha;
                    this.bannerIndex = newBannerIndex;
                    this.SetUpBannerImage();
                }
            }
        }

        /// <summary>
        /// The initialize component.
        /// </summary>
        private void InitializeComponent()
        {
            this.bannerImage = new System.Windows.Forms.PictureBox();
            this.bannerText = new System.Windows.Forms.Label();
            this.bannerTimer = new System.Windows.Forms.Timer();
            ((System.ComponentModel.ISupportInitialize)this.bannerImage).BeginInit();
            this.SuspendLayout();

            // bannerImage
            this.bannerImage.BackColor = System.Drawing.Color.White;
            this.bannerImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.bannerImage.Location = new System.Drawing.Point(0, 0);
            this.bannerImage.Name = "headerImage";
            this.bannerImage.Size = defaultSize;
            this.bannerImage.SizeChanged += new EventHandler(this.BannerImage_SizeChanged);
            this.bannerImage.SizeMode = PictureBoxSizeMode.CenterImage;
            this.bannerImage.TabIndex = 0;
            this.bannerImage.TabStop = false;
            this.bannerImage.Controls.Add(this.bannerText);

            // bannerText
            this.bannerText.BackColor = System.Drawing.Color.Transparent;
            this.bannerText.ForeColor = System.Drawing.Color.Black;
            this.bannerText.Font = Utility.CreateFont("Tahoma", 10.0f, FontStyle.Regular);
            this.bannerText.Location = new System.Drawing.Point(70, 47);
            this.bannerText.Name = "headingText";
            this.bannerText.Size = new System.Drawing.Size(441, 25);
            this.bannerText.TabIndex = 4;
            this.bannerText.Text = "headingText";
            this.bannerText.Visible = false;

            // bannerTimer
            this.bannerTimer.Interval = 30;
            this.bannerTimer.Tick += new EventHandler(this.BannerTimer_Tick);
            this.bannerTimer.Enabled = true;

            // PdnBanner
            this.Controls.Add(this.bannerImage);
            this.Name = "PdnBanner";
            ((System.ComponentModel.ISupportInitialize)this.bannerImage).EndInit();
            this.ResumeLayout();
            this.PerformLayout();
        }

        /// <summary>
        /// The set up banner image.
        /// </summary>
        private void SetUpBannerImage()
        {
            Image banner1 = this.banners[(this.bannerIndex + this.indexOffset) % this.banners.Length];
            Image banner2 = this.banners[(this.bannerIndex + 1 + this.indexOffset) % this.banners.Length];

            using (Graphics g = Graphics.FromImage(this.logoAndGradient))
            {
                g.Clear(Color.White);

                Rectangle gradientSrcBounds = new Rectangle(new Point(0, 0), banner2.Size);

                Rectangle gradientDstBounds = new Rectangle(
                    new Point(this.logoAndGradient.Width - banner2.Width, 0), 
                    banner2.Size);

                float alpha1 = this.bannerAlpha;
                float alpha2 = 1.0f - alpha1;

                ColorMatrix cm1 =
                    new ColorMatrix(
                        new[]
                            {
                                new float[] { 1, 0, 0, 0, 0 }, new float[] { 0, 1, 0, 0, 0 }, 
                                new float[] { 0, 0, 1, 0, 0 }, new[] { 0, 0, 0, alpha1, 0 }, new float[] { 0, 0, 0, 0, 1 }
                            });

                ImageAttributes ia1 = new ImageAttributes();
                ia1.SetColorMatrix(cm1);

                ColorMatrix cm2 =
                    new ColorMatrix(
                        new[]
                            {
                                new float[] { 1, 0, 0, 0, 0 }, new float[] { 0, 1, 0, 0, 0 }, 
                                new float[] { 0, 0, 1, 0, 0 }, new[] { 0, 0, 0, alpha2, 0 }, new float[] { 0, 0, 0, 0, 1 }
                            });

                ImageAttributes ia2 = new ImageAttributes();
                ia2.SetColorMatrix(cm2);

                if (banner1 != null)
                {
                    float inflateAmt1X = 0; // 1500.0f - (alpha1 * 1500.0f);
                    float inflateAmt1Y = 0; // (inflateAmt1X * (float)banner1.Height) / (float)banner2.Width;

                    RectangleF dstRect1 =
                        new RectangleF(
                            gradientDstBounds.X - inflateAmt1X * 2 + (inflateAmt1X / 150.0f), 
                            gradientDstBounds.Y - (inflateAmt1Y * 3) / 2, 
                            gradientDstBounds.Width + (2 * inflateAmt1X), 
                            gradientDstBounds.Height + (2 * inflateAmt1Y));

                    g.DrawImage(
                        banner1, 
                        new[]
                            {
                                dstRect1.Location, new PointF(dstRect1.Right, dstRect1.Top), 
                                new PointF(dstRect1.Left, dstRect1.Bottom)
                            }, 
                        gradientSrcBounds, 
                        GraphicsUnit.Pixel, 
                        ia1);
                }

                float inflateAmt2X = 0; // 1500.0f - (alpha2 * 1500.0f);
                float inflateAmt2Y = 0; // (inflateAmt2X * (float)banner2.Height) / (float)banner2.Width;

                RectangleF dstRect2 = new RectangleF(
                    gradientDstBounds.X - inflateAmt2X * 2 + (inflateAmt2X / 150.0f), 
                    gradientDstBounds.Y - (inflateAmt2Y * 3) / 2, 
                    gradientDstBounds.Width + (2 * inflateAmt2X), 
                    gradientDstBounds.Height + (2 * inflateAmt2Y));

                g.DrawImage(
                    banner2, 
                    new[]
                        {
                            dstRect2.Location, new PointF(dstRect2.Right, dstRect2.Top), 
                            new PointF(dstRect2.Left, dstRect2.Bottom)
                        }, 
                    (RectangleF)gradientSrcBounds, 
                    GraphicsUnit.Pixel, 
                    ia2);

                Rectangle pdnLogoBounds = new Rectangle(new Point(0, 0), this.pdnLogo.Size);
                g.DrawImage(this.pdnLogo, pdnLogoBounds, pdnLogoBounds, GraphicsUnit.Pixel);

                ia1.Dispose();
                ia1 = null;

                ia2.Dispose();
                ia2 = null;
            }

            Bitmap useThis;

            if (this.bannerImage.Size == this.logoAndGradient.Size)
            {
                useThis = this.logoAndGradient;
            }
            else
            {
                if (this.highQualityBmp == null)
                {
                    this.highQualityBmp = new Bitmap(
                        this.bannerImage.Width, 
                        this.bannerImage.Height, 
                        PixelFormat.Format24bppRgb);
                }

                using (Graphics g = Graphics.FromImage(this.highQualityBmp))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    g.DrawImage(
                        this.logoAndGradient, 
                        new Rectangle(0, 0, this.highQualityBmp.Width, this.highQualityBmp.Height), 
                        new Rectangle(0, 0, this.logoAndGradient.Width, this.logoAndGradient.Height), 
                        GraphicsUnit.Pixel);
                }

                useThis = this.highQualityBmp;
            }

            this.bannerImage.Image = useThis;
        }

        #endregion
    }
}