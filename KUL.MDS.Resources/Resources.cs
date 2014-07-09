// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Resources.cs" company="">
//   
// </copyright>
// <summary>
//   The resources.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Resources;

    using SIS.Systemlayer.Settings;

    /// <summary>
    /// The resources.
    /// </summary>
    public static class Resources
    {
        #region Constants

        /// <summary>
        /// The our namespace.
        /// </summary>
        private const string ourNamespace = "SIS.Resources";

        #endregion

        #region Static Fields

        /// <summary>
        /// The locale dirs.
        /// </summary>
        private static string[] localeDirs;

        /// <summary>
        /// The our assembly.
        /// </summary>
        private static Assembly ourAssembly;

        /// <summary>
        /// The pdn culture.
        /// </summary>
        private static CultureInfo pdnCulture;

        /// <summary>
        /// The resource manager.
        /// </summary>
        private static ResourceManager resourceManager;

        /// <summary>
        /// The resources dir.
        /// </summary>
        private static string resourcesDir;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="Resources"/> class.
        /// </summary>
        static Resources()
        {
            Initialize();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        public static CultureInfo Culture
        {
            get
            {
                return pdnCulture;
            }

            set
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;
                Initialize();
            }
        }

        /// <summary>
        /// Gets or sets the resources dir.
        /// </summary>
        public static string ResourcesDir
        {
            get
            {
                if (resourcesDir == null)
                {
                    resourcesDir = Path.GetDirectoryName(typeof(Resources).Assembly.Location);
                }

                return resourcesDir;
            }

            set
            {
                resourcesDir = value;
                Initialize();
            }
        }

        /// <summary>
        /// Gets the strings.
        /// </summary>
        public static ResourceManager Strings
        {
            get
            {
                return resourceManager;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get icon.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="Icon"/>.
        /// </returns>
        public static Icon GetIcon(string fileName)
        {
            Stream stream = GetResourceStream(fileName);
            Icon icon = null;

            if (stream != null)
            {
                icon = new Icon(stream);
            }

            return icon;
        }

        /// <summary>
        /// The get icon from image.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="Icon"/>.
        /// </returns>
        public static Icon GetIconFromImage(string fileName)
        {
            Stream stream = GetResourceStream(fileName);

            Icon icon = null;

            if (stream != null)
            {
                Image image = LoadImage(stream);
                icon = Icon.FromHandle(((Bitmap)image).GetHicon());
                image.Dispose();
                stream.Close();
            }

            return icon;
        }

        /// <summary>
        /// The get image.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="Image"/>.
        /// </returns>
        public static Image GetImage(string fileName)
        {
            Stream stream = GetResourceStream(fileName);

            Image image = null;
            if (stream != null)
            {
                image = LoadImage(stream);
            }

            return image;
        }

        /// <summary>
        /// The get image bmp or png.
        /// </summary>
        /// <param name="fileNameNoExt">
        /// The file name no ext.
        /// </param>
        /// <returns>
        /// The <see cref="Image"/>.
        /// </returns>
        public static Image GetImageBmpOrPng(string fileNameNoExt)
        {
            // using Path.ChangeExtension is not what we want; quite often filenames are "Icons.BlahBlahBlah"
            string fileNameBmp = fileNameNoExt + ".bmp";
            Image image = GetImage(fileNameBmp);

            if (image == null)
            {
                string fileNamePng = fileNameNoExt + ".png";
                image = GetImage(fileNamePng);
            }

            return image;
        }

        /// <summary>
        /// The get image resource.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="ImageResource"/>.
        /// </returns>
        public static ImageResource GetImageResource(string fileName)
        {
            return SISImageResource.Get(fileName);
        }

        /// <summary>
        /// The get installed locales.
        /// </summary>
        /// <returns>
        /// The <see cref="string[]"/>.
        /// </returns>
        public static string[] GetInstalledLocales()
        {
            // const string left = "PaintDotNet.Strings.3";
            // const string right = ".resources";
            const string left = "KUL.MDS";
            const string right = ".dll";
            string ourDir = ResourcesDir;
            string fileSpec = left + "*" + right;
            string[] pathNames = Directory.GetFiles(ourDir, fileSpec);
            List<string> locales = new List<string>();

            for (int i = 0; i < pathNames.Length; ++i)
            {
                string pathName = pathNames[i];
                string dirName = Path.GetDirectoryName(pathName);
                string fileName = Path.GetFileName(pathName);
                string sansRight = fileName.Substring(0, fileName.Length - right.Length);
                string sansLeft = sansRight.Substring(left.Length);

                string locale;

                if (sansLeft.Length > 0 && sansLeft[0] == '.')
                {
                    locale = sansLeft.Substring(1);
                }
                else if (sansLeft.Length == 0)
                {
                    locale = "en-US";
                }
                else
                {
                    locale = sansLeft;
                }

                try
                {
                    // Ensure this locale can create a valid CultureInfo object.
                    CultureInfo ci = new CultureInfo(locale);
                }
                catch (Exception)
                {
                    // Skip past invalid locales -- don't let them crash us
                    continue;
                }

                locales.Add(locale);
            }

            return locales.ToArray();
        }

        /// <summary>
        /// The get locale name chain.
        /// </summary>
        /// <returns>
        /// The <see cref="string[]"/>.
        /// </returns>
        public static string[] GetLocaleNameChain()
        {
            List<string> names = new List<string>();
            CultureInfo ci = pdnCulture;

            while (ci.Name != string.Empty)
            {
                names.Add(ci.Name);
                ci = ci.Parent;
            }

            return names.ToArray();
        }

        /// <summary>
        /// The get resource stream.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="Stream"/>.
        /// </returns>
        public static Stream GetResourceStream(string fileName)
        {
            Stream stream = null;

            for (int i = 0; i < localeDirs.Length; ++i)
            {
                string filePath = Path.Combine(localeDirs[i], fileName);

                if (File.Exists(filePath))
                {
                    stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    break;
                }
            }

            if (stream == null)
            {
                string fullName = ourNamespace + "." + fileName;
                stream = ourAssembly.GetManifestResourceStream(fullName);
            }

            return stream;
        }

        /// <summary>
        /// The get string.
        /// </summary>
        /// <param name="stringName">
        /// The string name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetString(string stringName)
        {
            string theString = resourceManager.GetString(stringName, pdnCulture);

            if (theString == null)
            {
                Debug.WriteLine(stringName + " not found");
            }

            return theString;
        }

        /// <summary>
        /// The is gdi plus image allowed.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsGdiPlusImageAllowed(Stream input)
        {
            byte[] wmfSig = new byte[] { 0xd7, 0xcd, 0xc6, 0x9a };
            byte[] emfSig = new byte[] { 0x01, 0x00, 0x00, 0x00 };

            // Check for and explicitely block WMF and EMF images
            return !(CheckForSignature(input, emfSig) || CheckForSignature(input, wmfSig));
        }

        /// <summary>
        /// The load image.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="Image"/>.
        /// </returns>
        public static Image LoadImage(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return LoadImage(stream);
            }
        }

        /// <summary>
        /// Loads an image from the given stream. The stream must be seekable.
        /// </summary>
        /// <param name="input">
        /// The Stream to load the image from.
        /// </param>
        /// <returns>
        /// The <see cref="Image"/>.
        /// </returns>
        public static Image LoadImage(Stream input)
        {
            /*
            if (!IsGdiPlusImageAllowed(input))
            {
                throw new IOException("File format is not supported");
            }
            */
            Image image = Image.FromStream(input);

            if (image.RawFormat == ImageFormat.Wmf || image.RawFormat == ImageFormat.Emf)
            {
                image.Dispose();
                throw new IOException("File format isn't supported");
            }

            return image;
        }

        /// <summary>
        /// The set new culture.
        /// </summary>
        /// <param name="newLocaleName">
        /// The new locale name.
        /// </param>
        public static void SetNewCulture(string newLocaleName)
        {
            // TODO, HACK: post-3.0 we must refactor and have an actual user data manager that can handle all this renaming
            string oldUserDataPath = Info.UserDataPath;
            string oldPaletteDirName = Resources.GetString("ColorPalettes.UserDataSubDirName");

            // END HACK
            CultureInfo newCI = new CultureInfo(newLocaleName);
            Settings.CurrentUser.SetString("LanguageName", newLocaleName);
            Culture = newCI;

            // TODO, HACK: finish up renaming
            string newUserDataPath = Info.UserDataPath;
            string newPaletteDirName = Resources.GetString("ColorPalettes.UserDataSubDirName");

            // 1. rename user data dir from old localized name to new localized name
            if (oldUserDataPath != newUserDataPath)
            {
                try
                {
                    Directory.Move(oldUserDataPath, newUserDataPath);
                }
                catch (Exception)
                {
                }
            }

            // 2. rename palette dir from old localized name (in new localized user data path) to new localized name
            string oldPalettePath = Path.Combine(newUserDataPath, oldPaletteDirName);
            string newPalettePath = Path.Combine(newUserDataPath, newPaletteDirName);

            if (oldPalettePath != newPalettePath)
            {
                try
                {
                    Directory.Move(oldPalettePath, newPalettePath);
                }
                catch (Exception)
                {
                }
            }

            // END HACK
        }

        #endregion

        #region Methods

        /// <summary>
        /// The check for signature.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="signature">
        /// The signature.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool CheckForSignature(Stream input, byte[] signature)
        {
            long oldPos = input.Position;
            byte[] inputSig = new byte[signature.Length];
            int amountRead = input.Read(inputSig, 0, inputSig.Length);

            bool foundSig = false;
            if (amountRead == signature.Length)
            {
                foundSig = true;

                for (int i = 0; i < signature.Length; ++i)
                {
                    foundSig &= signature[i] == inputSig[i];
                }
            }

            input.Position = oldPos;
            return foundSig;
        }

        /// <summary>
        /// The create resource manager.
        /// </summary>
        /// <returns>
        /// The <see cref="ResourceManager"/>.
        /// </returns>
        private static ResourceManager CreateResourceManager()
        {
            // const string stringsFileName = "PaintDotNet.Strings.3";
            // const string stringsFileName = "PI_Digital_Stage_Test_Framework.Resources.SIS";
            const string stringsFileName = "SIS.Resources.SIS";

            // ResourceManager rm = ResourceManager.CreateFileBasedResourceManager(stringsFileName, ResourcesDir, null);
            ResourceManager rm = new ResourceManager(stringsFileName, Assembly.GetEntryAssembly());
            return rm;
        }

        /// <summary>
        /// The get locale dirs.
        /// </summary>
        /// <returns>
        /// The <see cref="string[]"/>.
        /// </returns>
        private static string[] GetLocaleDirs()
        {
            const string rootDirName = "Resources";
            string appDir = ResourcesDir;
            string rootDir = Path.Combine(appDir, rootDirName);
            List<string> dirs = new List<string>();

            CultureInfo ci = pdnCulture;

            while (ci.Name != string.Empty)
            {
                string localeDir = Path.Combine(rootDir, ci.Name);

                if (Directory.Exists(localeDir))
                {
                    dirs.Add(localeDir);
                }

                ci = ci.Parent;
            }

            return dirs.ToArray();
        }

        /// <summary>
        /// The initialize.
        /// </summary>
        private static void Initialize()
        {
            resourceManager = CreateResourceManager();
            ourAssembly = Assembly.GetExecutingAssembly();
            pdnCulture = CultureInfo.CurrentUICulture;
            localeDirs = GetLocaleDirs();
        }

        #endregion

        /// <summary>
        /// The sis image resource.
        /// </summary>
        private sealed class SISImageResource : ImageResource
        {
            #region Static Fields

            /// <summary>
            /// The images.
            /// </summary>
            private static Dictionary<string, ImageResource> images;

            #endregion

            #region Fields

            /// <summary>
            /// The name.
            /// </summary>
            private string name;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes static members of the <see cref="SISImageResource"/> class.
            /// </summary>
            static SISImageResource()
            {
                images = new Dictionary<string, ImageResource>();
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="SISImageResource"/> class.
            /// </summary>
            /// <param name="name">
            /// The name.
            /// </param>
            private SISImageResource(string name)
                : base()
            {
                this.name = name;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="SISImageResource"/> class.
            /// </summary>
            /// <param name="image">
            /// The image.
            /// </param>
            private SISImageResource(Image image)
                : base(image)
            {
                this.name = null;
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// The get.
            /// </summary>
            /// <param name="name">
            /// The name.
            /// </param>
            /// <returns>
            /// The <see cref="ImageResource"/>.
            /// </returns>
            public static ImageResource Get(string name)
            {
                ImageResource ir;

                if (!images.TryGetValue(name, out ir))
                {
                    ir = new SISImageResource(name);
                    images.Add(name, ir);
                }

                return ir;
            }

            #endregion

            #region Methods

            /// <summary>
            /// The load.
            /// </summary>
            /// <returns>
            /// The <see cref="Image"/>.
            /// </returns>
            protected override Image Load()
            {
                return Resources.GetImage(this.name);
            }

            #endregion
        }
    }
}