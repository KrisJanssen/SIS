using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using SIS.ScanModes;
using SIS.SystemLayer;
using SIS.AppResources;

namespace SIS.Controls
{
    public partial class ScanModeComboBox : ComboBox
    {
        private bool m_bIsPopulated = false;

        private ScanModeCollection m_scmcScanModes;

        public ScanModeComboBox()
        {
            InitializeComponent();

            if (!this.m_bIsPopulated)
            {
                PopulateMenu();
            }
            
        }
        
        //protected override void OnDropDown(EventArgs e)
        //{
        //    if (!this.m_bIsPopulated)
        //    {
        //        PopulateMenu();
        //    }
        //    base.OnDropDown(e);
        //}

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

        private void PopulateMenu()
        {
            this.Items.Clear();

            AddScanModesToComboBox();
        }

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
                    ScanModeAttribute[] _ScanModeAttributes = type.GetCustomAttributes(typeof(ScanModeAttribute), true) as ScanModeAttribute[];
                    this.Items.Add(new SIS.Library.ComboBoxItem<Type>(_ScanModeAttributes[0].Name, type));
                }

                catch
                {
                    // We don't want a DLL that can't be figured out to cause the app to crash
                    continue;
                }
            }

            this.SelectedIndex = 0;
        }

        /// <summary>
        /// Loads additional ScanMode objects from dlls in the scanmode plugin directory.
        /// </summary>
        /// <returns>A Collection of ScanMode objects.</returns>
        private static ScanModeCollection GatherScanModes()
        {
            List<Assembly> assemblies = new List<Assembly>();

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
                        Tracing.Ping("Exception while loading " + filePath + ": " + ex.ToString());
                    }
                }
            }

            ScanModeCollection ec = new ScanModeCollection(assemblies);
            return ec;
        }
    }
}
