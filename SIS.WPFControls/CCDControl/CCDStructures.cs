// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Kris Janssen" file="CCDStructures.cs">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The pin direction.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.WPFControls.CCDControl
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The pin direction.
    /// </summary>
    [ComVisible(false)]
    internal enum PinDirection
    {
        /// <summary>
        /// The input.
        /// </summary>
        Input, 

        /// <summary>
        /// The output.
        /// </summary>
        Output
    }

    /// <summary>
    /// The am media type.
    /// </summary>
    [ComVisible(false)]
    [StructLayout(LayoutKind.Sequential)]
    internal class AMMediaType : IDisposable
    {
        /// <summary>
        /// The major type.
        /// </summary>
        public Guid MajorType;

        /// <summary>
        /// The sub type.
        /// </summary>
        public Guid SubType;

        /// <summary>
        /// The fixed size samples.
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool FixedSizeSamples = true;

        /// <summary>
        /// The temporal compression.
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool TemporalCompression;

        /// <summary>
        /// The sample size.
        /// </summary>
        public int SampleSize = 1;

        /// <summary>
        /// The format type.
        /// </summary>
        public Guid FormatType;

        /// <summary>
        /// The unk ptr.
        /// </summary>
        public IntPtr unkPtr;

        /// <summary>
        /// The format size.
        /// </summary>
        public int FormatSize;

        /// <summary>
        /// The format ptr.
        /// </summary>
        public IntPtr FormatPtr;

        /// <summary>
        /// Finalizes an instance of the <see cref="AMMediaType"/> class. 
        /// </summary>
        ~AMMediaType()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            // remove me from the Finalization queue 
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.FormatSize != 0)
            {
                Marshal.FreeCoTaskMem(this.FormatPtr);
            }

            if (this.unkPtr != IntPtr.Zero)
            {
                Marshal.Release(this.unkPtr);
            }
        }
    }

    /// <summary>
    /// The pin info.
    /// </summary>
    [ComVisible(false)]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal class PinInfo
    {
        /// <summary>
        /// The filter.
        /// </summary>
        public IBaseFilter Filter;

        /// <summary>
        /// The direction.
        /// </summary>
        public PinDirection Direction;

        /// <summary>
        /// The name.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string Name;
    }

    /// <summary>
    /// The video info header.
    /// </summary>
    [ComVisible(false)]
    [StructLayout(LayoutKind.Sequential)]
    internal struct VideoInfoHeader
    {
        /// <summary>
        /// The src rect.
        /// </summary>
        public RECT SrcRect;

        /// <summary>
        /// The target rect.
        /// </summary>
        public RECT TargetRect;

        /// <summary>
        /// The bit rate.
        /// </summary>
        public int BitRate;

        /// <summary>
        /// The bit error rate.
        /// </summary>
        public int BitErrorRate;

        /// <summary>
        /// The average time per frame.
        /// </summary>
        public long AverageTimePerFrame;

        /// <summary>
        /// The bmi header.
        /// </summary>
        public BitmapInfoHeader BmiHeader;
    }

    /// <summary>
    /// The bitmap info header.
    /// </summary>
    [ComVisible(false)]
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct BitmapInfoHeader
    {
        /// <summary>
        /// The size.
        /// </summary>
        public int Size;

        /// <summary>
        /// The width.
        /// </summary>
        public int Width;

        /// <summary>
        /// The height.
        /// </summary>
        public int Height;

        /// <summary>
        /// The planes.
        /// </summary>
        public short Planes;

        /// <summary>
        /// The bit count.
        /// </summary>
        public short BitCount;

        /// <summary>
        /// The compression.
        /// </summary>
        public int Compression;

        /// <summary>
        /// The image size.
        /// </summary>
        public int ImageSize;

        /// <summary>
        /// The x pels per meter.
        /// </summary>
        public int XPelsPerMeter;

        /// <summary>
        /// The y pels per meter.
        /// </summary>
        public int YPelsPerMeter;

        /// <summary>
        /// The colors used.
        /// </summary>
        public int ColorsUsed;

        /// <summary>
        /// The colors important.
        /// </summary>
        public int ColorsImportant;
    }

    /// <summary>
    /// The rect.
    /// </summary>
    [ComVisible(false)]
    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        /// <summary>
        /// The left.
        /// </summary>
        public int Left;

        /// <summary>
        /// The top.
        /// </summary>
        public int Top;

        /// <summary>
        /// The right.
        /// </summary>
        public int Right;

        /// <summary>
        /// The bottom.
        /// </summary>
        public int Bottom;
    }
}