using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KUL.MDS.WPFControls.CCDControl.Device;
using System.Collections.ObjectModel;

namespace KUL.MDS.WPFControls.CCDControl.UI
{
    /// <summary>
    /// Interaction logic for WebcanControl.xaml
    /// </summary>
    public partial class CCDControl : UserControl
    {
        #region Variables
        #endregion

        #region Constructor & destructor

        public CCDControl()
        {
            // Initialize component
            InitializeComponent();

            //// Subscribe command bindings
            //CommandBindings.Add(new CommandBinding(Input.CaptureImageCommands.CaptureImage,
            //    new ExecutedRoutedEventHandler(CaptureImage_Executed), new CanExecuteRoutedEventHandler(CaptureImage_CanExecute)));
            //CommandBindings.Add(new CommandBinding(Input.CaptureImageCommands.RemoveImage,
            //    new ExecutedRoutedEventHandler(RemoveImage_Executed)));
            //CommandBindings.Add(new CommandBinding(Input.CaptureImageCommands.ClearAllImages,
            //    new ExecutedRoutedEventHandler(ClearAllImages_Executed)));

            // Create default device
            //SelectedWebcamMonikerString = (CCDDevice.DeviceMonikers.Length > 0) ? CCDDevice.DeviceMonikers[0].MonikerString : "";
            this.SelectedWebcamMonikerString = "";
            this.Unloaded += new RoutedEventHandler(CCDControl_Unloaded);
        }

        void CCDControl_Unloaded(object sender, RoutedEventArgs e)
        {
            this.SelectedWebcamMonikerString = "";
        }

        /// <summary>
        /// Disposes the object
        /// </summary>
        public void Dispose()
        {
            // Stop
            this.SelectedWebcamMonikerString = string.Empty;
        }

        #endregion

        //#region Command bindings
        ///// <summary>
        ///// Determines whether the CaptureImage command can be executed
        ///// </summary>
        ///// <param name="sender">Sender</param>
        ///// <param name="e">EventArgs</param>
        //private void CaptureImage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    // Check if there is a valid webcam
        //    e.CanExecute = (SelectedWebcam != null);
        //}

        ///// <summary>
        ///// Invoked when the CaptureImage command is executed
        ///// </summary>
        ///// <param name="sender">Sender</param>
        ///// <param name="e">EventArgs</param>
        //private void CaptureImage_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    //// Store current image in the webcam
        //    BitmapSource bitmap = webcamPlayer.CurrentBitmap;
        //    if (bitmap != null)
        //    {
        //        SelectedImages.Add(bitmap);
        //    }
        //}

        ///// <summary>
        ///// Invoked when the RemoveImage command is executed
        ///// </summary>
        ///// <param name="sender">Sender</param>
        ///// <param name="e">EventArgs</param>
        //private void RemoveImage_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    // Store current image in the webcam
        //    BitmapSource bitmap = e.Parameter as BitmapSource;
        //    if (bitmap != null)
        //    {
        //        SelectedImages.Remove(bitmap);
        //    }
        //}

        ///// <summary>
        ///// Invoked when the ClearAllImages command is executed
        ///// </summary>
        ///// <param name="sender">Sender</param>
        ///// <param name="e">EventArgs</param>
        //private void ClearAllImages_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    // Clear all images
        //    SelectedImages.Clear();
        //}
        //#endregion

        #region Properties
        /// <summary>
        /// Wrapper for the WebcamRotation dependency property
        /// </summary>
        public double WebcamRotation
        {
            get { return (double)GetValue(WebcamRotationProperty); }
            set { SetValue(WebcamRotationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WebcamRotation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WebcamRotationProperty =
            DependencyProperty.Register("WebcamRotation", typeof(double), typeof(CCDControl), new UIPropertyMetadata(180d));

        public double TargetEllipseDiameter
        {
            get { return (double)GetValue(TargetEllipseDiameterProperty); }
            set { SetValue(TargetEllipseDiameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WebcamRotation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetEllipseDiameterProperty =
            DependencyProperty.Register("TargetEllipseDiameter", typeof(double), typeof(CCDControl), new UIPropertyMetadata(50d));

        /// <summary>
        /// Wrapper for the SelectedWebcam dependency property
        /// </summary>
        public CCDDevice SelectedWebcam
        {
            get { return (CCDDevice)GetValue(SelectedWebcamProperty); }
            set { SetValue(SelectedWebcamProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedWebcam.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedWebcamProperty =
            DependencyProperty.Register("SelectedWebcam", typeof(CCDDevice), typeof(CCDControl), new UIPropertyMetadata(null));

        /// <summary>
        /// Wrapper for the SelectedWebcamMonikerString dependency property
        /// </summary>
        public string SelectedWebcamMonikerString
        {
            get { return (string)GetValue(SelectedWebcamMonikerStringProperty); }
            set { SetValue(SelectedWebcamMonikerStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedWebcamMonikerString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedWebcamMonikerStringProperty = DependencyProperty.Register("SelectedWebcamMonikerString", typeof(string),
            typeof(CCDControl), new UIPropertyMetadata("", new PropertyChangedCallback(SelectedWebcamMonikerString_Changed)));

        /// <summary>
        /// Wrapper for the SelectedImages dependency property
        /// </summary>
        public ObservableCollection<BitmapSource> SelectedImages
        {
            get { return (ObservableCollection<BitmapSource>)GetValue(SelectedImagesProperty); }
            set { SetValue(SelectedImagesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedImages.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedImagesProperty = DependencyProperty.Register("SelectedImages", typeof(ObservableCollection<BitmapSource>),
            typeof(CCDControl), new UIPropertyMetadata(new ObservableCollection<BitmapSource>()));
        #endregion

        #region Methods
        /// <summary>
        /// Invoked when the SelectedWebcamMonikerString dependency property has changed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private static void SelectedWebcamMonikerString_Changed(DependencyObject __depoSender, DependencyPropertyChangedEventArgs __deppropcheaE)
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
                    _CCDSender.SelectedWebcam = new CCDDevice("");
                }

                // Now set the moniker string
                _CCDSender.SelectedWebcam.MonikerString = _sNewMonikerString;
            }
        }
        #endregion

        private void btnCCDOff_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedWebcamMonikerString = string.Empty;
        }
    }
}
