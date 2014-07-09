// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OS.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The os.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    using System;
    using System.Globalization;

    using Microsoft.Win32;

    /// <summary>
    /// The os.
    /// </summary>
    public static class OS
    {
        #region Public Properties

        /// <summary>
        /// Gets a flag indicating whether we are running on Windows Vista or later.
        /// </summary>
        /// <remarks>
        /// This is the preferred method for detecting this condition, and the most performant
        /// (it avoids creating some temporary Version objects).
        /// </remarks>
        public static bool IsVistaOrLater
        {
            get
            {
                return Environment.OSVersion.Version.Major >= 6;
            }
        }

        /// <summary>
        /// Gets the revision.
        /// </summary>
        public static string Revision
        {
            get
            {
                NativeStructs.OSVERSIONINFOEX osviex = new NativeStructs.OSVERSIONINFOEX();
                osviex.dwOSVersionInfoSize = (uint)NativeStructs.OSVERSIONINFOEX.SizeOf;
                bool result = SafeNativeMethods.GetVersionEx(ref osviex);

                if (result)
                {
                    return osviex.szCSDVersion;
                }
                else
                {
                    return "Unknown";
                }
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public static OSType Type
        {
            get
            {
                NativeStructs.OSVERSIONINFOEX osviex = new NativeStructs.OSVERSIONINFOEX();
                osviex.dwOSVersionInfoSize = (uint)NativeStructs.OSVERSIONINFOEX.SizeOf;
                bool result = SafeNativeMethods.GetVersionEx(ref osviex);
                OSType type;

                if (result)
                {
                    if (Enum.IsDefined(typeof(OSType), (OSType)osviex.wProductType))
                    {
                        type = (OSType)osviex.wProductType;
                    }
                    else
                    {
                        type = OSType.Unknown;
                    }
                }
                else
                {
                    type = OSType.Unknown;
                }

                return type;
            }
        }

        /// <summary>
        /// Gets the windows server 2003.
        /// </summary>
        public static Version WindowsServer2003
        {
            get
            {
                return new Version(5, 2);
            }
        }

        /// <summary>
        /// Gets the windows vista.
        /// </summary>
        public static Version WindowsVista
        {
            get
            {
                return new Version(6, 0);
            }
        }

        /// <summary>
        /// Gets the windows xp.
        /// </summary>
        public static Version WindowsXP
        {
            get
            {
                return new Version(5, 1);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The check os requirement.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool CheckOSRequirement()
        {
            // Just say "no" to Windows 9x
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                return false;
            }

            // Windows Vista or later?
            bool winVista = OS.CheckWindowsVersion(6, 0, 0);

            // Windows 2003 or later?
            bool win2k3 = OS.CheckWindowsVersion(5, 2, 0);

            // Windows 2003 SP1 or later?
            bool win2k3SP1 = OS.CheckWindowsVersion(5, 2, 1);

            // Windows XP or later?
            bool winXP = OS.CheckWindowsVersion(5, 1, 0);

            // Windows XP SP2 or later?
            bool winXPSP2 = OS.CheckWindowsVersion(5, 1, 2);

            return winVista || (win2k3 && win2k3SP1) || (winXP && winXPSP2);
        }

        /// <summary>
        /// The check windows version.
        /// </summary>
        /// <param name="major">
        /// The major.
        /// </param>
        /// <param name="minor">
        /// The minor.
        /// </param>
        /// <param name="servicePack">
        /// The service pack.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool CheckWindowsVersion(int major, int minor, short servicePack)
        {
            NativeStructs.OSVERSIONINFOEX osvi = new NativeStructs.OSVERSIONINFOEX();
            osvi.dwOSVersionInfoSize = (uint)NativeStructs.OSVERSIONINFOEX.SizeOf;
            osvi.dwMajorVersion = (uint)major;
            osvi.dwMinorVersion = (uint)minor;
            osvi.wServicePackMajor = (ushort)servicePack;

            ulong mask = 0;
            mask = NativeMethods.VerSetConditionMask(
                mask, 
                NativeConstants.VER_MAJORVERSION, 
                NativeConstants.VER_GREATER_EQUAL);
            mask = NativeMethods.VerSetConditionMask(
                mask, 
                NativeConstants.VER_MINORVERSION, 
                NativeConstants.VER_GREATER_EQUAL);
            mask = NativeMethods.VerSetConditionMask(
                mask, 
                NativeConstants.VER_SERVICEPACKMAJOR, 
                NativeConstants.VER_GREATER_EQUAL);

            bool result = NativeMethods.VerifyVersionInfo(
                ref osvi, 
                NativeConstants.VER_MAJORVERSION | NativeConstants.VER_MINORVERSION
                | NativeConstants.VER_SERVICEPACKMAJOR, 
                mask);

            return result;
        }

        /// <summary>
        /// The is dot net version installed.
        /// </summary>
        /// <param name="major">
        /// The major.
        /// </param>
        /// <param name="minor">
        /// The minor.
        /// </param>
        /// <param name="servicePack">
        /// The service pack.
        /// </param>
        /// <param name="allowClientSubset">
        /// The allow client subset.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsDotNetVersionInstalled(int major, int minor, int servicePack, bool allowClientSubset)
        {
            bool result = false;

            if (!result)
            {
                // .NET 2.0 should always have a build # of 50727
                result |= IsDotNet2VersionInstalled(major, minor, 50727, servicePack);
            }

            if (!result)
            {
                result |= IsDotNet3VersionInstalled(major, minor, servicePack);
            }

            if (!result && allowClientSubset)
            {
                result |= IsDotNet3ClientVersionInstalled(major, minor, servicePack);
            }

            return result;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The check for reg value equals int.
        /// </summary>
        /// <param name="regKeyName">
        /// The reg key name.
        /// </param>
        /// <param name="regValueName">
        /// The reg value name.
        /// </param>
        /// <param name="intValue">
        /// The int value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool CheckForRegValueEqualsInt(string regKeyName, string regValueName, int intValue)
        {
            int? value = GetRegValueAsInt(regKeyName, regValueName);

            if (value.HasValue)
            {
                return value.Value == intValue;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The get reg value as int.
        /// </summary>
        /// <param name="regKeyName">
        /// The reg key name.
        /// </param>
        /// <param name="regValueName">
        /// The reg value name.
        /// </param>
        /// <returns>
        /// The <see cref="int?"/>.
        /// </returns>
        private static int? GetRegValueAsInt(string regKeyName, string regValueName)
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regKeyName, false))
            {
                object value = null;

                if (key != null)
                {
                    value = key.GetValue(regValueName);
                }

                if (value != null && value is int)
                {
                    return (int)value;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// The is dot net 2 version installed.
        /// </summary>
        /// <param name="major">
        /// The major.
        /// </param>
        /// <param name="minor">
        /// The minor.
        /// </param>
        /// <param name="build">
        /// The build.
        /// </param>
        /// <param name="minServicePack">
        /// The min service pack.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsDotNet2VersionInstalled(int major, int minor, int build, int minServicePack)
        {
            bool result = false;

            const string regKeyNameFormat = "SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v{0}.{1}.{2}";
            string regKeyName = string.Format(
                regKeyNameFormat, 
                major.ToString(CultureInfo.InvariantCulture), 
                minor.ToString(CultureInfo.InvariantCulture), 
                build.ToString(CultureInfo.InvariantCulture));

            if (!result)
            {
                const string regValueName = "Install";
                result |= CheckForRegValueEqualsInt(regKeyName, regValueName, 1);
            }

            if (result)
            {
                const string regValueName = "SP";
                int? spLevel = GetRegValueAsInt(regKeyName, regValueName);
                result &= spLevel.HasValue && spLevel.Value >= minServicePack;
            }

            return result;
        }

        /// <summary>
        /// The is dot net 3 client version installed.
        /// </summary>
        /// <param name="major">
        /// The major.
        /// </param>
        /// <param name="minor">
        /// The minor.
        /// </param>
        /// <param name="minServicePack">
        /// The min service pack.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsDotNet3ClientVersionInstalled(int major, int minor, int minServicePack)
        {
            bool result = false;

            const string regKeyNameFormat = "SOFTWARE\\Microsoft\\NET Framework Setup\\DotNetClient\\v{0}.{1}";
            string regKeyName = string.Format(regKeyNameFormat, major, minor);

            if (!result)
            {
                const string regValueName = "Install";
                result |= CheckForRegValueEqualsInt(regKeyName, regValueName, 1);
            }

            if (result)
            {
                const string regValueName = "SP";
                int? spLevel = GetRegValueAsInt(regKeyName, regValueName);
                result &= spLevel.HasValue && spLevel.Value >= minServicePack;
            }

            return result;
        }

        /// <summary>
        /// The is dot net 3 version installed.
        /// </summary>
        /// <param name="major">
        /// The major.
        /// </param>
        /// <param name="minor">
        /// The minor.
        /// </param>
        /// <param name="minServicePack">
        /// The min service pack.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsDotNet3VersionInstalled(int major, int minor, int minServicePack)
        {
            bool result = false;

            const string regKeyNameFormat = "SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v{0}.{1}";
            string regKeyName = string.Format(regKeyNameFormat, major, minor);

            if (!result)
            {
                const string regValueName = "Install";
                result |= CheckForRegValueEqualsInt(regKeyName, regValueName, 1);
            }

            if (result)
            {
                const string regValueName = "SP";
                int? spLevel = GetRegValueAsInt(regKeyName, regValueName);
                result &= spLevel.HasValue && spLevel.Value >= minServicePack;
            }

            return result;
        }

        #endregion
    }
}