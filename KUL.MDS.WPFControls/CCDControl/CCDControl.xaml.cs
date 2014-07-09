// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CCDControl.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for WebcanControl.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.WPFControls.CCDControl
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Interaction logic for WebcanControl.xaml
    /// </summary>
    public partial class CCDControl : UserControl
    {
        // Using a DependencyProperty as the backing store for WebcamRotation.  This enables animation, styling, binding, etc...
        #region Static Fields

        /// <summary>
        /// The selected images property.
        /// </summary>
        public static readonly DependencyProperty SelectedImagesProperty = DependencyProperty.Register(
            "SelectedImages", 
            typeof(ObservableCollection<BitmapSource>), 
            typeof(CCDControl), 
            new UIPropertyMetadata(new ObservableCollection<BitmapSource>()));

        /// <summary>
        /// The selected webcam moniker string property.
        /// </summary>
        public static readonly DependencyProperty SelectedWebcamMonikerStringProperty =
            DependencyProperty.Register(
                "SelectedWebcamMonikerString", 
                typeof(string), 
                typeof(CCDControl), 
                new UIPropertyMetadata(string.Empty, new PropertyChangedCallback(SelectedWebcamMonikerString_Changed)));

        /// <summary>
        /// The selected webcam property.
        /// </summary>
        public static readonly DependencyProperty SelectedWebcamProperty = DependencyProperty.Register(
            "SelectedWebcam", 
            typeof(CCDDevice), 
            typeof(CCDControl), 
            new UIPropertyMetadata(null));

        /// <summary>
        /// The target ellipse diameter property.
        /// </summary>
        public static readonly DependencyProperty TargetEllipseDiameterProperty =
            DependencyProperty.Register(
                "TargetEllipseDiameter", 
                typeof(double), 
                typeof(CCDControl), 
                new UIPropertyMetadata(50d));

        /// <summary>
        /// The webcam rotation property.
        /// </summary>
        public static readonly DependencyProperty WebcamRotationProperty = DependencyProperty.Register(
            "WebcamRotation", 
            typeof(double), 
            typeof(CCDControl), 
            new UIPropertyMetadata(180d));

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CCDControl"/> class.
        /// </summary>
        public CCDControl()
        {
            // Initialize component
            this.InitializeComponent();

            //// Subscribe command bindings
            // CommandBindings.Add(new CommandBinding(Input.CaptureImageCommands.CaptureImage,
            // new ExecutedRoutedEventHandler(CaptureImage_Executed), new CanExecuteRoutedEventHandler(CaptureImage_CanExecute)));
            // CommandBindings.Add(new CommandBinding(Input.CaptureImageCommands.RemoveImage,
            // new ExecutedRoutedEventHandler(RemoveImage_Executed)));
            // CommandBindings.Add(new CommandBinding(Input.CaptureImageCommands.ClearAllImages,
            // new ExecutedRoutedEventHandler(ClearAllImages_Executed)));

            // Create default device
            // SelectedWebcamMonikerString = (CCDDevice.DeviceMonikers.Length > 0) ? CCDDevice.DeviceMonikers[0].MonikerString : "";
            this.SelectedWebcamMonikerString = string.Empty;
            this.Unloaded += new RoutedEventHandler(this.CCDControl_Unloaded);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Wrapper for the SelectedImages dependency property
        /// </summary>
        public ObservableCollection<BitmapSource> SelectedImages
        {
            get
            {
                return (ObservableCollection<BitmapSource>)this.GetValue(SelectedImagesProperty);
            }

            set
            {
                this.SetValue(SelectedImagesProperty, value);
            }
        }

        /// <summary>
        /// Wrapper for the SelectedWebcam dependency property
        /// </summary>
        public CCDDevice SelectedWebcam
        {
            get
            {
                return (CCDDevice)this.GetValue(SelectedWebcamProperty);
            }

            set
            {
                this.SetValue(SelectedWebcamProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for SelectedWebcam.  This enables animation, styling, binding, etc...

        /// <summary>
        /// Wrapper for the SelectedWebcamMonikerString dependency property
        /// </summary>
        public string SelectedWebcamMonikerString
        {
            get
            {
                return (string)this.GetValue(SelectedWebcamMonikerStringProperty);
            }

            set
            {
                this.SetValue(SelectedWebcamMonikerStringProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the target ellipse diameter.
        /// </summary>
        public double TargetEllipseDiameter
        {
            get
            {
                return (double)this.GetValue(TargetEllipseDiameterProperty);
            }

            set
            {
                this.SetValue(TargetEllipseDiameterProperty, value);
            }
        }

        /// <summary>
        /// Wrapper for the WebcamRotation dependency property
        /// </summary>
        public double WebcamRotation
        {
            get
            {
                return (double)this.GetValue(WebcamRotationProperty);
            }

            set
            {
                this.SetValue(WebcamRotationProperty, value);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Disposes the object
        /// </summary>
        public void Dispose()
        {
            // Stop
            this.SelectedWebcamMonikerString = string.Empty;
        }

        #endregion

        // Using a DependencyProperty as the backing store for SelectedWebcamMonikerString.  This enables animation, styling, binding, etc...
        #region Methods

        /// <summary>
        /// Invoked when the SelectedWebcamMonikerString dependency property has changed
        /// </summary>
        /// <param name="__depoSender">
        /// The __depo Sender.
        /// </param>
        /// <param name="__deppropcheaE">
        /// The __deppropchea E.
        /// </param>
        private static void SelectedWebcamMonikerString_Changed(
            DependencyObject __depoSender, 
            DependencyPropertyChangedEventArgs __deppropcheaE)
        {
            // Get typed sender
            CCDControl _CCDSender = (CCDControl)__depoSender;

            if (_CCDSender != null)
            {
                // Get new value
                string _sNewMonikerString = (string)__deppropcheaE.NewValue;

                // Update the device
                if (_CCDSender.SelectedWebcam == null)
                {
                    // Create it
                    _CCDSender.SelectedWebcam = new CCDDevice(string.Empty);
                }

                // Now set the moniker string
                _CCDSender.SelectedWebcam.MonikerString = _sNewMonikerString;
            }
        }

        /// <summary>
        /// The ccd control_ unloaded.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CCDControl_Unloaded(object sender, RoutedEventArgs e)
        {
            this.SelectedWebcamMonikerString = string.Empty;
        }

        /// <summary>
        /// The btn ccd off_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnCCDOff_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedWebcamMonikerString = string.Empty;
        }

        #endregion
    }
}