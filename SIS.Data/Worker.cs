// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Worker.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The worker.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Data
{
    /// <summary>
    /// The worker.
    /// </summary>
    public static class Worker
    {
        // static DataSet LoadFile(string __sPath, string __sFormat, List<string> __lOptions)
        // {
        // try
        // {
        // FormatInfo fi;
        // FileStream _fsStream = new FileStream(__sPath, FileMode.Open);
        // return LoadStream(_fsStream, fi, __lOptions);
        // }
        // catch (System.IO.IOException ex)
        // {
        // MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
        // return null;
        // }
        // }

        // static DataSet LoadStream(FileStream __fstrmFile, FormatInfo __fiFormatInfo, List<string> __lOptions)
        // {
        // DataSet _dtstData = new WinspecDataSet();
        // //DataSet _dtstData = (*fi->ctor)();
        // //_dtstData.lOptions = __lOptions;
        // //_dtstData.LoadData(__fstrmFile);
        // return _dtstData;
        // }
    }
}