// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScanModeComboBox.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The scan mode combo box.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Controls
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    using SIS.AppResources;
    using SIS.Library;
    using SIS.ScanModes;
    using SIS.SystemLayer;

    /// <summary>
    /// The scan mode combo box.
    /// </summary>
    public partial class ScanModeComboBox : ComboBox
    {
        #region Fields

        /// <summary>
        /// The m_b is populated.
        /// </summary>
        private bool m_bIsPopulated = false;

        /// <summary>
        /// The m_scmc scan modes.
        /// </summary>
        private ScanModeCollection m_scmcScanModes;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanModeComboBox"/> class.
        /// </summary>
        public ScanModeComboBox()
        {
            this.InitializeComponent();

            if (!this.m_bIsPopulated)
            {
                this.PopulateMenu();
            }
        }

        #endregion

        // protected override void OnDropDown(EventArgs e)
        // {
        // if (!this.m_bIsPopulated)
        // {
        // PopulateMenu();
        // }
        // base.OnDropDown(e);
        // }
        #region Public Properties

        /// <summary>
        /// Gets the scan modes.
        /// </summary>
        public ScanModeCollection ScanModes
        {
            get
            {
                if (this.m_scmcScanModes == null)
                {
                    this.m_scmcScanModes = GatherScanModes();
                }

                return this.m_scmcScanModes;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Loads additional ScanMode objects from dlls in the scanmode plugin directory.
        /// </summary>
        /// <returns>A Collection of ScanMode objects.</returns>
        private static ScanModeCollection GatherScanModes()
        {
            var assemblies = new List<Assembly>();

            // this assembly
            assemblies.Add(Assembly.GetExecutingAssembly());

            // PaintDotNet.Effects.dll
            assemblies.Add(Assembly.GetAssembly(typeof(Scanmode)));

            // TARGETDIR\Effects\*.dll
            string homeDir = Info.GetApplicationDir();
            string effectsDir = Path.Combine(homeDir, InvariantStrings.EffectsSubDir);
            bool dirExists;

            try
            {
                dirExists = Directory.Exists(effectsDir);
            }
            catch
            {
                dirExists = false;
            }

            if (dirExists)
            {
                string fileSpec = "*" + InvariantStrings.DllExtension;
                string[] filePaths = Directory.GetFiles(effectsDir, fileSpec);

                foreach (string filePath in filePaths)
                {
                    Assembly pluginAssembly = null;

                    try
                    {
                        pluginAssembly = Assembly.LoadFrom(filePath);
                        assemblies.Add(pluginAssembly);
                    }
                    catch (Exception ex)
                    {
                        Tracing.Ping("Exception while loading " + filePath + ": " + ex);
                    }
                }
            }

            var ec = new ScanModeCollection(assemblies);
            return ec;
        }

        /// <summary>
        /// The add scan modes to combo box.
        /// </summary>
        private void AddScanModesToComboBox()
        {
            if (this.m_scmcScanModes == null)
            {
                this.m_scmcScanModes = GatherScanModes();
            }

            ScanModeCollection _colScanModesCollection = this.m_scmcScanModes;
            Type[] effectTypes = _colScanModesCollection.Scanmodes;

            foreach (Type type in effectTypes)
            {
                try
                {
                    var _ScanModeAttributes =
                        type.GetCustomAttributes(typeof(ScanModeAttribute), true) as ScanModeAttribute[];
                    this.Items.Add(new ComboBoxItem<Type>(_ScanModeAttributes[0].Name, type));
                }
                catch
                {
                    // We don't want a DLL that can't be figured out to cause the app to crash
                }
            }

            this.SelectedIndex = 0;
        }

        /// <summary>
        /// The populate menu.
        /// </summary>
        private void PopulateMenu()
        {
            this.Items.Clear();

            this.AddScanModesToComboBox();
        }

        #endregion
    }
}