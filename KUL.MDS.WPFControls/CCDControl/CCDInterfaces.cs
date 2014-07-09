// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CCDInterfaces.cs">
//   
// </copyright>
// <summary>
//   The GraphBuilder interface.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.WPFControls.CCDControl
{
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

    /// <summary>
    /// The GraphBuilder interface.
    /// </summary>
    [ComImport]
    [Guid("56A868A9-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IGraphBuilder
    {
        /// <summary>
        /// The add filter.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int AddFilter([In] IBaseFilter filter, [In] [MarshalAs(UnmanagedType.LPWStr)] string name);

        /// <summary>
        /// The remove filter.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int RemoveFilter([In] IBaseFilter filter);

        /// <summary>
        /// The enum filters.
        /// </summary>
        /// <param name="enumerator">
        /// The enumerator.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int EnumFilters([Out] out IntPtr enumerator);

        /// <summary>
        /// The find filter by name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int FindFilterByName([In] [MarshalAs(UnmanagedType.LPWStr)] string name, [Out] out IBaseFilter filter);

        /// <summary>
        /// The connect direct.
        /// </summary>
        /// <param name="pinOut">
        /// The pin out.
        /// </param>
        /// <param name="pinIn">
        /// The pin in.
        /// </param>
        /// <param name="mediaType">
        /// The media type.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int ConnectDirect(
            [In] IPin pinOut, 
            [In] IPin pinIn, 
            [In] [MarshalAs(UnmanagedType.LPStruct)] AMMediaType mediaType);

        /// <summary>
        /// The reconnect.
        /// </summary>
        /// <param name="pin">
        /// The pin.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Reconnect([In] IPin pin);

        /// <summary>
        /// The disconnect.
        /// </summary>
        /// <param name="pin">
        /// The pin.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Disconnect([In] IPin pin);

        /// <summary>
        /// The set default sync source.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int SetDefaultSyncSource();

        /// <summary>
        /// The connect.
        /// </summary>
        /// <param name="pinOut">
        /// The pin out.
        /// </param>
        /// <param name="pinIn">
        /// The pin in.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Connect([In] IPin pinOut, [In] IPin pinIn);

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="pinOut">
        /// The pin out.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Render([In] IPin pinOut);

        /// <summary>
        /// The render file.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <param name="playList">
        /// The play list.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int RenderFile(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string file, 
            [In] [MarshalAs(UnmanagedType.LPWStr)] string playList);

        /// <summary>
        /// The add source filter.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="filterName">
        /// The filter name.
        /// </param>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int AddSourceFilter(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string fileName, 
            [In] [MarshalAs(UnmanagedType.LPWStr)] string filterName, 
            [Out] out IBaseFilter filter);

        /// <summary>
        /// The set log file.
        /// </summary>
        /// <param name="hFile">
        /// The h file.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int SetLogFile(IntPtr hFile);

        /// <summary>
        /// The abort.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Abort();

        /// <summary>
        /// The should operation continue.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int ShouldOperationContinue();
    }

    /// <summary>
    /// The BaseFilter interface.
    /// </summary>
    [ComImport]
    [Guid("56A86895-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IBaseFilter
    {
        /// <summary>
        /// The get class id.
        /// </summary>
        /// <param name="ClassID">
        /// The class id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int GetClassID([Out] out Guid ClassID);

        /// <summary>
        /// The stop.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Stop();

        /// <summary>
        /// The pause.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Pause();

        /// <summary>
        /// The run.
        /// </summary>
        /// <param name="start">
        /// The start.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Run(long start);

        /// <summary>
        /// The get state.
        /// </summary>
        /// <param name="milliSecsTimeout">
        /// The milli secs timeout.
        /// </param>
        /// <param name="filterState">
        /// The filter state.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int GetState(int milliSecsTimeout, [Out] out int filterState);

        /// <summary>
        /// The set sync source.
        /// </summary>
        /// <param name="clock">
        /// The clock.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int SetSyncSource([In] IntPtr clock);

        /// <summary>
        /// The get sync source.
        /// </summary>
        /// <param name="clock">
        /// The clock.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int GetSyncSource([Out] out IntPtr clock);

        /// <summary>
        /// The enum pins.
        /// </summary>
        /// <param name="enumPins">
        /// The enum pins.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int EnumPins([Out] out IEnumPins enumPins);

        /// <summary>
        /// The find pin.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="pin">
        /// The pin.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int FindPin([In] [MarshalAs(UnmanagedType.LPWStr)] string id, [Out] out IPin pin);

        /// <summary>
        /// The query filter info.
        /// </summary>
        /// <param name="filterInfo">
        /// The filter info.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int QueryFilterInfo([Out] FilterInfo filterInfo);

        /// <summary>
        /// The join filter graph.
        /// </summary>
        /// <param name="graph">
        /// The graph.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int JoinFilterGraph([In] IFilterGraph graph, [In] [MarshalAs(UnmanagedType.LPWStr)] string name);

        /// <summary>
        /// The query vendor info.
        /// </summary>
        /// <param name="vendorInfo">
        /// The vendor info.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int QueryVendorInfo([Out] [MarshalAs(UnmanagedType.LPWStr)] out string vendorInfo);
    }

    /// <summary>
    /// The Pin interface.
    /// </summary>
    [ComImport]
    [Guid("56A86891-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPin
    {
        /// <summary>
        /// The connect.
        /// </summary>
        /// <param name="receivePin">
        /// The receive pin.
        /// </param>
        /// <param name="mediaType">
        /// The media type.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Connect([In] IPin receivePin, [In] [MarshalAs(UnmanagedType.LPStruct)] AMMediaType mediaType);

        /// <summary>
        /// The receive connection.
        /// </summary>
        /// <param name="receivePin">
        /// The receive pin.
        /// </param>
        /// <param name="mediaType">
        /// The media type.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int ReceiveConnection([In] IPin receivePin, [In] [MarshalAs(UnmanagedType.LPStruct)] AMMediaType mediaType);

        /// <summary>
        /// The disconnect.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Disconnect();

        /// <summary>
        /// The connected to.
        /// </summary>
        /// <param name="pin">
        /// The pin.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int ConnectedTo([Out] out IPin pin);

        /// <summary>
        /// The connection media type.
        /// </summary>
        /// <param name="mediaType">
        /// The media type.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int ConnectionMediaType([Out] [MarshalAs(UnmanagedType.LPStruct)] AMMediaType mediaType);

        /// <summary>
        /// The query pin info.
        /// </summary>
        /// <param name="pinInfo">
        /// The pin info.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int QueryPinInfo([Out] [MarshalAs(UnmanagedType.LPStruct)] PinInfo pinInfo);

        /// <summary>
        /// The query direction.
        /// </summary>
        /// <param name="pinDirection">
        /// The pin direction.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int QueryDirection(out PinDirection pinDirection);

        /// <summary>
        /// The query id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int QueryId([Out] [MarshalAs(UnmanagedType.LPWStr)] out string id);

        /// <summary>
        /// The query accept.
        /// </summary>
        /// <param name="mediaType">
        /// The media type.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int QueryAccept([In] [MarshalAs(UnmanagedType.LPStruct)] AMMediaType mediaType);

        /// <summary>
        /// The enum media types.
        /// </summary>
        /// <param name="enumerator">
        /// The enumerator.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int EnumMediaTypes(IntPtr enumerator);

        /// <summary>
        /// The query internal connections.
        /// </summary>
        /// <param name="apPin">
        /// The ap pin.
        /// </param>
        /// <param name="nPin">
        /// The n pin.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int QueryInternalConnections(IntPtr apPin, [In] [Out] ref int nPin);

        /// <summary>
        /// The end of stream.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int EndOfStream();

        /// <summary>
        /// The begin flush.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int BeginFlush();

        /// <summary>
        /// The end flush.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int EndFlush();

        /// <summary>
        /// The new segment.
        /// </summary>
        /// <param name="start">
        /// The start.
        /// </param>
        /// <param name="stop">
        /// The stop.
        /// </param>
        /// <param name="rate">
        /// The rate.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int NewSegment(long start, long stop, double rate);
    }

    /// <summary>
    /// The EnumPins interface.
    /// </summary>
    [ComImport]
    [Guid("56A86892-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IEnumPins
    {
        /// <summary>
        /// The next.
        /// </summary>
        /// <param name="cPins">
        /// The c pins.
        /// </param>
        /// <param name="pins">
        /// The pins.
        /// </param>
        /// <param name="pinsFetched">
        /// The pins fetched.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Next(
            [In] int cPins, 
            [Out] [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] IPin[] pins, 
            [Out] out int pinsFetched);

        /// <summary>
        /// The skip.
        /// </summary>
        /// <param name="cPins">
        /// The c pins.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Skip([In] int cPins);

        /// <summary>
        /// The reset.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Reset();

        /// <summary>
        /// The clone.
        /// </summary>
        /// <param name="enumPins">
        /// The enum pins.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Clone([Out] out IEnumPins enumPins);
    }

    /// <summary>
    /// The FilterGraph interface.
    /// </summary>
    [ComImport]
    [Guid("56A8689F-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFilterGraph
    {
        /// <summary>
        /// The add filter.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int AddFilter([In] IBaseFilter filter, [In] [MarshalAs(UnmanagedType.LPWStr)] string name);

        /// <summary>
        /// The remove filter.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int RemoveFilter([In] IBaseFilter filter);

        /// <summary>
        /// The enum filters.
        /// </summary>
        /// <param name="enumerator">
        /// The enumerator.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int EnumFilters([Out] out IntPtr enumerator);

        /// <summary>
        /// The find filter by name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int FindFilterByName([In] [MarshalAs(UnmanagedType.LPWStr)] string name, [Out] out IBaseFilter filter);

        /// <summary>
        /// The connect direct.
        /// </summary>
        /// <param name="pinOut">
        /// The pin out.
        /// </param>
        /// <param name="pinIn">
        /// The pin in.
        /// </param>
        /// <param name="mediaType">
        /// The media type.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int ConnectDirect(
            [In] IPin pinOut, 
            [In] IPin pinIn, 
            [In] [MarshalAs(UnmanagedType.LPStruct)] AMMediaType mediaType);

        /// <summary>
        /// The reconnect.
        /// </summary>
        /// <param name="pin">
        /// The pin.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Reconnect([In] IPin pin);

        /// <summary>
        /// The disconnect.
        /// </summary>
        /// <param name="pin">
        /// The pin.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Disconnect([In] IPin pin);

        /// <summary>
        /// The set default sync source.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int SetDefaultSyncSource();
    }

    /// <summary>
    /// The PropertyBag interface.
    /// </summary>
    [ComImport]
    [Guid("55272A00-42CB-11CE-8135-00AA004BB851")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPropertyBag
    {
        /// <summary>
        /// The read.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <param name="pVar">
        /// The p var.
        /// </param>
        /// <param name="pErrorLog">
        /// The p error log.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Read(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string propertyName, 
            [In] [Out] [MarshalAs(UnmanagedType.Struct)] ref object pVar, 
            [In] IntPtr pErrorLog);

        /// <summary>
        /// The write.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <param name="pVar">
        /// The p var.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Write(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string propertyName, 
            [In] [MarshalAs(UnmanagedType.Struct)] ref object pVar);
    }

    /// <summary>
    /// The SampleGrabber interface.
    /// </summary>
    [ComImport]
    [Guid("6B652FFF-11FE-4FCE-92AD-0266B5D7C78F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ISampleGrabber
    {
        /// <summary>
        /// The set one shot.
        /// </summary>
        /// <param name="oneShot">
        /// The one shot.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int SetOneShot([In] [MarshalAs(UnmanagedType.Bool)] bool oneShot);

        /// <summary>
        /// The set media type.
        /// </summary>
        /// <param name="mediaType">
        /// The media type.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int SetMediaType([In] [MarshalAs(UnmanagedType.LPStruct)] AMMediaType mediaType);

        /// <summary>
        /// The get connected media type.
        /// </summary>
        /// <param name="mediaType">
        /// The media type.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int GetConnectedMediaType([Out] [MarshalAs(UnmanagedType.LPStruct)] AMMediaType mediaType);

        /// <summary>
        /// The set buffer samples.
        /// </summary>
        /// <param name="bufferThem">
        /// The buffer them.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int SetBufferSamples([In] [MarshalAs(UnmanagedType.Bool)] bool bufferThem);

        /// <summary>
        /// The get current buffer.
        /// </summary>
        /// <param name="bufferSize">
        /// The buffer size.
        /// </param>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int GetCurrentBuffer(ref int bufferSize, IntPtr buffer);

        /// <summary>
        /// The get current sample.
        /// </summary>
        /// <param name="sample">
        /// The sample.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int GetCurrentSample(IntPtr sample);

        /// <summary>
        /// The set callback.
        /// </summary>
        /// <param name="callback">
        /// The callback.
        /// </param>
        /// <param name="whichMethodToCallback">
        /// The which method to callback.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int SetCallback(ISampleGrabberCB callback, int whichMethodToCallback);
    }

    /// <summary>
    /// The SampleGrabberCB interface.
    /// </summary>
    [ComImport]
    [Guid("0579154A-2B53-4994-B0D0-E773148EFF85")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ISampleGrabberCB
    {
        /// <summary>
        /// The sample cb.
        /// </summary>
        /// <param name="sampleTime">
        /// The sample time.
        /// </param>
        /// <param name="sample">
        /// The sample.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int SampleCB(double sampleTime, IntPtr sample);

        /// <summary>
        /// The buffer cb.
        /// </summary>
        /// <param name="sampleTime">
        /// The sample time.
        /// </param>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="bufferLen">
        /// The buffer len.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int BufferCB(double sampleTime, IntPtr buffer, int bufferLen);
    }

    /// <summary>
    /// The CreateDevEnum interface.
    /// </summary>
    [ComImport]
    [Guid("29840822-5B84-11D0-BD3B-00A0C911CE86")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ICreateDevEnum
    {
        /// <summary>
        /// The create class enumerator.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="enumMoniker">
        /// The enum moniker.
        /// </param>
        /// <param name="flags">
        /// The flags.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int CreateClassEnumerator([In] ref Guid type, [Out] out IEnumMoniker enumMoniker, [In] int flags);
    }

    /// <summary>
    /// The VideoWindow interface.
    /// </summary>
    [ComImport]
    [Guid("56A868B4-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    internal interface IVideoWindow
    {
        /// <summary>
        /// The put_ caption.
        /// </summary>
        /// <param name="caption">
        /// The caption.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int put_Caption(string caption);

        /// <summary>
        /// The get_ caption.
        /// </summary>
        /// <param name="caption">
        /// The caption.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_Caption([Out] out string caption);

        /// <summary>
        /// The put_ window style.
        /// </summary>
        /// <param name="windowStyle">
        /// The window style.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int put_WindowStyle(int windowStyle);

        /// <summary>
        /// The get_ window style.
        /// </summary>
        /// <param name="windowStyle">
        /// The window style.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_WindowStyle(out int windowStyle);

        /// <summary>
        /// The put_ window style ex.
        /// </summary>
        /// <param name="windowStyleEx">
        /// The window style ex.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int put_WindowStyleEx(int windowStyleEx);

        /// <summary>
        /// The get_ window style ex.
        /// </summary>
        /// <param name="windowStyleEx">
        /// The window style ex.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_WindowStyleEx(out int windowStyleEx);

        /// <summary>
        /// The put_ auto show.
        /// </summary>
        /// <param name="autoShow">
        /// The auto show.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int put_AutoShow([In] [MarshalAs(UnmanagedType.Bool)] bool autoShow);

        /// <summary>
        /// The get_ auto show.
        /// </summary>
        /// <param name="autoShow">
        /// The auto show.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_AutoShow([Out] [MarshalAs(UnmanagedType.Bool)] out bool autoShow);

        /// <summary>
        /// The put_ window state.
        /// </summary>
        /// <param name="windowState">
        /// The window state.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int put_WindowState(int windowState);

        /// <summary>
        /// The get_ window state.
        /// </summary>
        /// <param name="windowState">
        /// The window state.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_WindowState(out int windowState);

        /// <summary>
        /// The put_ background palette.
        /// </summary>
        /// <param name="backgroundPalette">
        /// The background palette.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int put_BackgroundPalette([In] [MarshalAs(UnmanagedType.Bool)] bool backgroundPalette);

        /// <summary>
        /// The get_ background palette.
        /// </summary>
        /// <param name="backgroundPalette">
        /// The background palette.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_BackgroundPalette([Out] [MarshalAs(UnmanagedType.Bool)] out bool backgroundPalette);

        /// <summary>
        /// The put_ visible.
        /// </summary>
        /// <param name="visible">
        /// The visible.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int put_Visible([In] [MarshalAs(UnmanagedType.Bool)] bool visible);

        /// <summary>
        /// The get_ visible.
        /// </summary>
        /// <param name="visible">
        /// The visible.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_Visible([Out] [MarshalAs(UnmanagedType.Bool)] out bool visible);

        /// <summary>
        /// The put_ left.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int put_Left(int left);

        /// <summary>
        /// The get_ left.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_Left(out int left);

        /// <summary>
        /// The put_ width.
        /// </summary>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int put_Width(int width);

        /// <summary>
        /// The get_ width.
        /// </summary>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_Width(out int width);

        /// <summary>
        /// The put_ top.
        /// </summary>
        /// <param name="top">
        /// The top.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int put_Top(int top);

        /// <summary>
        /// The get_ top.
        /// </summary>
        /// <param name="top">
        /// The top.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_Top(out int top);

        /// <summary>
        /// The put_ height.
        /// </summary>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int put_Height(int height);

        /// <summary>
        /// The get_ height.
        /// </summary>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_Height(out int height);

        /// <summary>
        /// The put_ owner.
        /// </summary>
        /// <param name="owner">
        /// The owner.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int put_Owner(IntPtr owner);

        /// <summary>
        /// The get_ owner.
        /// </summary>
        /// <param name="owner">
        /// The owner.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_Owner(out IntPtr owner);

        /// <summary>
        /// The put_ message drain.
        /// </summary>
        /// <param name="drain">
        /// The drain.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int put_MessageDrain(IntPtr drain);

        /// <summary>
        /// The get_ message drain.
        /// </summary>
        /// <param name="drain">
        /// The drain.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_MessageDrain(out IntPtr drain);

        /// <summary>
        /// The get_ border color.
        /// </summary>
        /// <param name="color">
        /// The color.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_BorderColor(out int color);

        /// <summary>
        /// The put_ border color.
        /// </summary>
        /// <param name="color">
        /// The color.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int put_BorderColor(int color);

        /// <summary>
        /// The get_ full screen mode.
        /// </summary>
        /// <param name="fullScreenMode">
        /// The full screen mode.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_FullScreenMode([Out] [MarshalAs(UnmanagedType.Bool)] out bool fullScreenMode);

        /// <summary>
        /// The put_ full screen mode.
        /// </summary>
        /// <param name="fullScreenMode">
        /// The full screen mode.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int put_FullScreenMode([In] [MarshalAs(UnmanagedType.Bool)] bool fullScreenMode);

        /// <summary>
        /// The set window foreground.
        /// </summary>
        /// <param name="focus">
        /// The focus.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int SetWindowForeground(int focus);

        /// <summary>
        /// The notify owner message.
        /// </summary>
        /// <param name="hwnd">
        /// The hwnd.
        /// </param>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <param name="wParam">
        /// The w param.
        /// </param>
        /// <param name="lParam">
        /// The l param.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int NotifyOwnerMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// The set window position.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="top">
        /// The top.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int SetWindowPosition(int left, int top, int width, int height);

        /// <summary>
        /// The get window position.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="top">
        /// The top.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int GetWindowPosition(out int left, out int top, out int width, out int height);

        /// <summary>
        /// The get min ideal image size.
        /// </summary>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int GetMinIdealImageSize(out int width, out int height);

        /// <summary>
        /// The get max ideal image size.
        /// </summary>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int GetMaxIdealImageSize(out int width, out int height);

        /// <summary>
        /// The get restore position.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="top">
        /// The top.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int GetRestorePosition(out int left, out int top, out int width, out int height);

        /// <summary>
        /// The hide cursor.
        /// </summary>
        /// <param name="hideCursor">
        /// The hide cursor.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int HideCursor([In] [MarshalAs(UnmanagedType.Bool)] bool hideCursor);

        /// <summary>
        /// The is cursor hidden.
        /// </summary>
        /// <param name="hideCursor">
        /// The hide cursor.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int IsCursorHidden([Out] [MarshalAs(UnmanagedType.Bool)] out bool hideCursor);
    }

    /// <summary>
    /// The MediaControl interface.
    /// </summary>
    [ComImport]
    [Guid("56A868B1-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    internal interface IMediaControl
    {
        /// <summary>
        /// The run.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Run();

        /// <summary>
        /// The pause.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Pause();

        /// <summary>
        /// The stop.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int Stop();

        /// <summary>
        /// The get state.
        /// </summary>
        /// <param name="timeout">
        /// The timeout.
        /// </param>
        /// <param name="filterState">
        /// The filter state.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int GetState(int timeout, out int filterState);

        /// <summary>
        /// The render file.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int RenderFile(string fileName);

        /// <summary>
        /// The add source filter.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="filterInfo">
        /// The filter info.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int AddSourceFilter([In] string fileName, [Out] [MarshalAs(UnmanagedType.IDispatch)] out object filterInfo);

        /// <summary>
        /// The get_ filter collection.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_FilterCollection([Out] [MarshalAs(UnmanagedType.IDispatch)] out object collection);

        /// <summary>
        /// The get_ reg filter collection.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int get_RegFilterCollection([Out] [MarshalAs(UnmanagedType.IDispatch)] out object collection);

        /// <summary>
        /// The stop when ready.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [PreserveSig]
        int StopWhenReady();
    }
}