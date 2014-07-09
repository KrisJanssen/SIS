// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="Program.cs">
//   
// </copyright>
// <summary>
//   The program.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace SISRepair
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Security.Principal;
    using System.Threading;

    using Microsoft.Win32;

    /// <summary>
    /// The program.
    /// </summary>
    public sealed class Program
    {
        #region Constants

        /// <summary>
        /// The reinstallmod e_ fileequalversion.
        /// </summary>
        internal const uint REINSTALLMODE_FILEEQUALVERSION = 0x00000008;

        /// <summary>
        /// The reinstallmod e_ fileexact.
        /// </summary>
        internal const uint REINSTALLMODE_FILEEXACT = 0x00000010;

        /// <summary>
        /// The reinstallmod e_ filemissing.
        /// </summary>
        internal const uint REINSTALLMODE_FILEMISSING = 0x00000002;

        /// <summary>
        /// The reinstallmod e_ fileolderversion.
        /// </summary>
        internal const uint REINSTALLMODE_FILEOLDERVERSION = 0x00000004;

        /// <summary>
        /// The reinstallmod e_ filereplace.
        /// </summary>
        internal const uint REINSTALLMODE_FILEREPLACE = 0x00000040;

        /// <summary>
        /// The reinstallmod e_ fileverify.
        /// </summary>
        internal const uint REINSTALLMODE_FILEVERIFY = 0x00000020;

        /// <summary>
        /// The reinstallmod e_ machinedata.
        /// </summary>
        internal const uint REINSTALLMODE_MACHINEDATA = 0x00000080;

        /// <summary>
        /// The reinstallmod e_ package.
        /// </summary>
        internal const uint REINSTALLMODE_PACKAGE = 0x00000400;

        /// <summary>
        /// The reinstallmod e_ repair.
        /// </summary>
        internal const uint REINSTALLMODE_REPAIR = 0x00000001;

        /// <summary>
        /// The reinstallmod e_ shortcut.
        /// </summary>
        internal const uint REINSTALLMODE_SHORTCUT = 0x00000200;

        /// <summary>
        /// The reinstallmod e_ userdata.
        /// </summary>
        internal const uint REINSTALLMODE_USERDATA = 0x00000100;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int Main(string[] args)
        {
            int returnVal = 0;

            try
            {
                returnVal = MainImpl(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("--- Error: ");
                Console.WriteLine(ex.ToString());

                returnVal = -1;
            }

            if (args.Length == 0)
            {
                Console.WriteLine();
                Console.Write("Press Enter to exit...");
                Console.ReadLine();
            }

            return returnVal;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The msi reinstall product w.
        /// </summary>
        /// <param name="szProduct">
        /// The sz product.
        /// </param>
        /// <param name="dwReinstallMode">
        /// The dw reinstall mode.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        internal static extern uint MsiReinstallProductW(
            [MarshalAs(UnmanagedType.LPWStr)] string szProduct, 
            uint dwReinstallMode);

        /// <summary>
        /// The main impl.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private static int MainImpl(string[] args)
        {
            bool success = true;
            int returnVal = 0;

            Console.WriteLine("SIS Repair Tool");
            Console.WriteLine();

            if (success)
            {
                AppDomain domain = Thread.GetDomain();
                domain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
                WindowsPrincipal principal = (WindowsPrincipal)Thread.CurrentPrincipal;
                bool isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);

                if (!isAdmin)
                {
                    Console.WriteLine("This utility must be run with administrator privilege.");
                    success = false;
                    returnVal = 740; // ERROR_ELEVATION_REQUIRED
                }
            }

            RegistryKey key = null;
            if (success)
            {
                Console.Write(@"* Opening registry key, HKEY_LOCAL_MACHINE\SOFTWARE\SIS: ");
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\SIS", false);

                if (key != null)
                {
                    Console.WriteLine("ok");
                }
                else
                {
                    Console.WriteLine("null");
                    success = false;
                    returnVal = -1;
                }
            }

            string productCode = null;
            if (success)
            {
                Console.Write("* Retrieving MSI product code GUID: ");
                string productCodeString =
                    (string)key.GetValue("ProductCode", null, RegistryValueOptions.DoNotExpandEnvironmentNames);
                Guid productCodeGuid = new Guid(productCodeString);
                productCode =
                    productCodeGuid.ToString("B", CultureInfo.InvariantCulture).ToUpper(CultureInfo.InvariantCulture);
                Console.WriteLine(productCode);
            }

            if (success)
            {
                Console.Write("* Attempting to repair: ");
                uint dwResult = MsiReinstallProductW(productCode, REINSTALLMODE_FILEMISSING);

                if (dwResult == 0)
                {
                    Console.WriteLine("success");
                }
                else
                {
                    Console.WriteLine("failed, dwResult=" + dwResult.ToString());
                    returnVal = unchecked((int)dwResult);
                }
            }

            return returnVal;
        }

        #endregion
    }
}