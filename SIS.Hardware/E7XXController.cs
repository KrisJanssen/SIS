// --------------------------------------------------------------------------------------------------------------------
// <copyright file="E7XXController.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Summary description for E7XX.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Hardware
{
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    ///     Summary description for E7XX.
    /// </summary>
    internal static class E7XXController
    {
        ///////////////////////////
        // E-7XX Bits (BIT_XXX). //
        ///////////////////////////

        /* Wave-Trigger E7XX_BIT_TRG_XXX */
        #region Constants

        /// <summary>
        /// The bi t_ tr g_ al l_ curv e_ points.
        /// </summary>
        internal const int BIT_TRG_ALL_CURVE_POINTS = 0x0100;

        /// <summary>
        /// The bi t_ tr g_ lin e_1.
        /// </summary>
        internal const int BIT_TRG_LINE_1 = 0x0001;

        /// <summary>
        /// The bi t_ tr g_ lin e_2.
        /// </summary>
        internal const int BIT_TRG_LINE_2 = 0x0002;

        /// <summary>
        /// The bi t_ tr g_ lin e_3.
        /// </summary>
        internal const int BIT_TRG_LINE_3 = 0x0004;

        /// <summary>
        /// The bi t_ tr g_ lin e_4.
        /// </summary>
        internal const int BIT_TRG_LINE_4 = 0x0008;

        /// <summary>
        /// The bi t_ wg o_ sav e_ bi t_1.
        /// </summary>
        internal const int BIT_WGO_SAVE_BIT_1 = 0x00100000;

        /// <summary>
        /// The bi t_ wg o_ sav e_ bi t_2.
        /// </summary>
        internal const int BIT_WGO_SAVE_BIT_2 = 0x00200000;

        /// <summary>
        /// The bi t_ wg o_ sav e_ bi t_3.
        /// </summary>
        internal const int BIT_WGO_SAVE_BIT_3 = 0x00400000;

        /// <summary>
        /// The bi t_ wg o_ singl e_ ru n_ dd l_ test.
        /// </summary>
        internal const int BIT_WGO_SINGLE_RUN_DDL_TEST = 0x00000200;

        /// <summary>
        /// The bi t_ wg o_ star t_ a t_ endposition.
        /// </summary>
        internal const int BIT_WGO_START_AT_ENDPOSITION = 0x00000100;

        /// <summary>
        /// The bi t_ wg o_ star t_ default.
        /// </summary>
        /// * Curve Controll E7XX_BIT_WGO_XXX */
        internal const int BIT_WGO_START_DEFAULT = 0x00000001;

        /// <summary>
        /// The bi t_ wg o_ star t_ exter n_ trigger.
        /// </summary>
        internal const int BIT_WGO_START_EXTERN_TRIGGER = 0x00000002;

        /// <summary>
        /// The bi t_ wg o_ wit h_ ddl.
        /// </summary>
        internal const int BIT_WGO_WITH_DDL = 0x00000080;

        /// <summary>
        /// The bi t_ wg o_ wit h_ dd l_ initialisation.
        /// </summary>
        internal const int BIT_WGO_WITH_DDL_INITIALISATION = 0x00000040;

        /// <summary>
        /// The pi f_ m t_ all.
        /// </summary>
        internal const int PIF_MT_ALL = PIF_MT_RAM | PIF_MT_EPROM;

        /// <summary>
        /// The pi f_ m t_ eprom.
        /// </summary>
        internal const int PIF_MT_EPROM = 0x00000002;

        /// <summary>
        /// The pi f_ m t_ ram.
        /// </summary>
        internal const int PIF_MT_RAM = 0x00000001;

        #endregion

        #region Methods

        /// <summary>
        /// The atz.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="dLowVoltageArray">
        /// The d low voltage array.
        /// </param>
        /// <param name="fUseDefaultArray">
        /// The f use default array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_ATZ")]
        internal static extern int ATZ(int iId, string sAxes, double[] dLowVoltageArray, int[] fUseDefaultArray);

        /// <summary>
        /// The ccl.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="iComandLevel">
        /// The i comand level.
        /// </param>
        /// <param name="sPassWord">
        /// The s pass word.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_CCL")]
        internal static extern int CCL(int iId, int iComandLevel, string sPassWord);

        /// <summary>
        /// The cst.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="names">
        /// The names.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_CST")]
        internal static extern int CST(int iId, string sAxes, string names);

        /////////////////////////////////////////////////////////////////////////////
        // DLL initialization and comm functions

        /// <summary>
        /// The change n igpib address.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="nDevAddr">
        /// The n dev addr.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_ChangeNIgpibAddress")]
        internal static extern int ChangeNIgpibAddress(int iId, int nDevAddr);

        /// <summary>
        /// The close connection.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_CloseConnection")]
        internal static extern void CloseConnection(int iId);

        /// <summary>
        /// The connect n igpib.
        /// </summary>
        /// <param name="nBoard">
        /// The n board.
        /// </param>
        /// <param name="nDevAddr">
        /// The n dev addr.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_ConnectNIgpib")]
        internal static extern int ConnectNIgpib(int nBoard, int nDevAddr);

        /// <summary>
        /// The connect pci board.
        /// </summary>
        /// <param name="iBoardNumber">
        /// The i board number.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_ConnectPciBoard")]
        internal static extern int ConnectPciBoard(int iBoardNumber);

        /// <summary>
        /// The connect r s 232.
        /// </summary>
        /// <param name="nPortNr">
        /// The n port nr.
        /// </param>
        /// <param name="nBaudRate">
        /// The n baud rate.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_ConnectRS232")]
        internal static extern int ConnectRS232(int nPortNr, int nBaudRate);

        /// <summary>
        /// The count pci boards.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_CountPciBoards")]
        internal static extern int CountPciBoards();

        /// <summary>
        /// The ddl.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="iLinearizationTable">
        /// The i linearization table.
        /// </param>
        /// <param name="iOffset">
        /// The i offset.
        /// </param>
        /// <param name="iValues">
        /// The i values.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_DDL")]
        internal static extern int DDL(int iId, int iLinearizationTable, int iOffset, int iValues, double[] dValArray);

        /// <summary>
        /// The dfh.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_DFH")]
        internal static extern int DFH(int iId, string sAxes);

        /// <summary>
        /// The dpo.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_DPO")]
        internal static extern int DPO(int iId, string sAxes);

        /// <summary>
        /// The drc.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="iRecChannelId">
        /// The i rec channel id.
        /// </param>
        /// <param name="sRecSourceId">
        /// The s rec source id.
        /// </param>
        /// <param name="iRecOption">
        /// The i rec option.
        /// </param>
        /// <param name="iTriggerOption">
        /// The i trigger option.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_DRC")]
        internal static extern int DRC(
            int iId, 
            int[] iRecChannelId, 
            string sRecSourceId, 
            int[] iRecOption, 
            int[] iTriggerOption);

        /// <summary>
        /// The dtc.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="iLearnTable">
        /// The i learn table.
        /// </param>
        /// <param name="bClearWaveAllsow">
        /// The b clear wave allsow.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_DTC")]
        internal static extern int DTC(int iId, int iLearnTable, int bClearWaveAllsow);

        /// <summary>
        /// The e 7 xx read line.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAnswer">
        /// The s answer.
        /// </param>
        /// <param name="iBufSize">
        /// The i buf size.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_E7XXReadLine")]
        internal static extern int E7XXReadLine(int iId, StringBuilder sAnswer, int iBufSize);

        /// <summary>
        /// The e 7 xx send string.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sString">
        /// The s string.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_E7XXSendString")]
        internal static extern int E7XXSendString(int iId, string sString);

        /// <summary>
        /// The goh.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_GOH")]
        internal static extern int GOH(int iId, string sAxes);

        /// <summary>
        /// The gcs commandset.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sCommand">
        /// The s command.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_GcsCommandset")]
        internal static extern int GcsCommandset(int iId, string sCommand);

        /// <summary>
        /// The gcs get answer.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAnswer">
        /// The s answer.
        /// </param>
        /// <param name="iBufSize">
        /// The i buf size.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_GcsGetAnswer")]
        internal static extern int GcsGetAnswer(int iId, StringBuilder sAnswer, int iBufSize);

        /// <summary>
        /// The gcs get answer size.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="iAnswerSize">
        /// The i answer size.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_GcsGetAnswerSize")]
        internal static extern int GcsGetAnswerSize(int iId, ref int iAnswerSize);

        /// <summary>
        /// The get error.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_GetError")]
        internal static extern int GetError(int iId);

        /// <summary>
        /// The get supported functions.
        /// </summary>
        /// <param name="ID">
        /// The id.
        /// </param>
        /// <param name="piComandLevelArray">
        /// The pi comand level array.
        /// </param>
        /// <param name="iMaxlen">
        /// The i maxlen.
        /// </param>
        /// <param name="sFunctionNames">
        /// The s function names.
        /// </param>
        /// <param name="iMaxFunctioNamesLength">
        /// The i max functio names length.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_GetSupportedFunctions")]
        internal static extern int GetSupportedFunctions(
            int ID, 
            int[] piComandLevelArray, 
            int iMaxlen, 
            StringBuilder sFunctionNames, 
            int iMaxFunctioNamesLength);

        /// <summary>
        /// The get supported parameters.
        /// </summary>
        /// <param name="ID">
        /// The id.
        /// </param>
        /// <param name="piParameterIdArray">
        /// The pi parameter id array.
        /// </param>
        /// <param name="piComandLevelArray">
        /// The pi comand level array.
        /// </param>
        /// <param name="piMemoryLocationArray">
        /// The pi memory location array.
        /// </param>
        /// <param name="iMaxlen">
        /// The i maxlen.
        /// </param>
        /// <param name="sParameterName">
        /// The s parameter name.
        /// </param>
        /// <param name="iMaxParameterNameSize">
        /// The i max parameter name size.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_GetSupportedParameters")]
        internal static extern int GetSupportedParameters(
            int ID, 
            int[] piParameterIdArray, 
            int[] piComandLevelArray, 
            int[] piMemoryLocationArray, 
            int iMaxlen, 
            StringBuilder sParameterName, 
            int iMaxParameterNameSize);

        /// <summary>
        /// The get supportrd controllers.
        /// </summary>
        /// <param name="szControllerNames">
        /// The sz controller names.
        /// </param>
        /// <param name="iMaxlen">
        /// The i maxlen.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_GetSupportrdControllers")]
        internal static extern int GetSupportrdControllers(StringBuilder szControllerNames, int iMaxlen);

        /// <summary>
        /// The hlt.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_HLT")]
        internal static extern int HLT(int iId, string sAxes);

        /// <summary>
        /// The imp.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="cAxis">
        /// The c axis.
        /// </param>
        /// <param name="dOffset">
        /// The d offset.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_IMP")]
        internal static extern int IMP(int iId, char cAxis, double dOffset);

        /////////////////////////////////////////////////////////////////////////////
        // general

        /// <summary>
        /// The ini.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_INI")]
        internal static extern int INI(int iId, string sAxes);

        /// <summary>
        /// The interface setup dlg.
        /// </summary>
        /// <param name="sRegKeyName">
        /// The s reg key name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_InterfaceSetupDlg")]
        internal static extern int InterfaceSetupDlg(string sRegKeyName);

        /// <summary>
        /// The is connected.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_IsConnected")]
        internal static extern int IsConnected(int iId);

        /// <summary>
        /// The is generator running.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="pValArray">
        /// The p val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_IsGeneratorRunning")]
        internal static extern int IsGeneratorRunning(int iId, string sAxes, int[] pValArray);

        /// <summary>
        /// The is moving.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="bValArray">
        /// The b val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_IsMoving")]
        internal static extern int IsMoving(int iId, string sAxes, int[] bValArray);

        /// <summary>
        /// The mov.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_MOV")]
        internal static extern int MOV(int iId, string sAxes, double[] dValArray);

        /// <summary>
        /// The mvr.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_MVR")]
        internal static extern int MVR(int iId, string sAxes, double[] dValArray);

        /// <summary>
        /// The nmov.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_NMOV")]
        internal static extern int NMOV(int iId, string sAxes, double[] dValArray);

        /// <summary>
        /// The nmvr.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_NMVR")]
        internal static extern int NMVR(int iId, string sAxes, double[] dValArray);

        /// <summary>
        /// The rpa.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="iParameterArray">
        /// The i parameter array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_RPA")]
        internal static extern int RPA(int iId, string sAxes, uint[] iParameterArray);

        /// <summary>
        /// The sai.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sOldAxes">
        /// The s old axes.
        /// </param>
        /// <param name="sNewAxes">
        /// The s new axes.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_SAI")]
        internal static extern int SAI(int iId, string sOldAxes, string sNewAxes);

        /// <summary>
        /// The sep.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sPassword">
        /// The s password.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="iParameterArray">
        /// The i parameter array.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <param name="szStrings">
        /// The sz strings.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_SEP")]
        internal static extern int SEP(
            int iId, 
            string sPassword, 
            string sAxes, 
            uint[] iParameterArray, 
            double[] dValArray, 
            string szStrings);

        /// <summary>
        /// The spa.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="iParameterArray">
        /// The i parameter array.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <param name="sStrings">
        /// The s strings.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_SPA")]
        internal static extern int SPA(
            int iId, 
            string sAxes, 
            uint[] iParameterArray, 
            double[] dValArray, 
            string sStrings);

        /// <summary>
        /// The ste.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="cAxis">
        /// The c axis.
        /// </param>
        /// <param name="dOffset">
        /// The d offset.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_STE")]
        internal static extern int STE(int iId, char cAxis, double dOffset);

        /// <summary>
        /// The sva.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_SVA")]
        internal static extern int SVA(int iId, string sAxes, double[] dValArray);

        /// <summary>
        /// The svo.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="iValArray">
        /// The i val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_SVO")]
        internal static extern int SVO(int iId, string sAxes, int[] iValArray);

        /// <summary>
        /// The svr.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_SVR")]
        internal static extern int SVR(int iId, string sAxes, double[] dValArray);

        /// <summary>
        /// The set error check.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="bErrorCheck">
        /// The b error check.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_SetErrorCheck")]
        internal static extern int SetErrorCheck(int iId, int bErrorCheck);

        /// <summary>
        /// The twc.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_TWC")]
        internal static extern int TWC(int iId);

        /// <summary>
        /// The tws.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="iWavePoint">
        /// The i wave point.
        /// </param>
        /// <param name="iTriggerLevel">
        /// The i trigger level.
        /// </param>
        /// <param name="iNumberOfPoints">
        /// The i number of points.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_TWS")]
        internal static extern int TWS(int iId, int[] iWavePoint, int[] iTriggerLevel, int iNumberOfPoints);

        /// <summary>
        /// The translate error.
        /// </summary>
        /// <param name="errNr">
        /// The err nr.
        /// </param>
        /// <param name="sBuffer">
        /// The s buffer.
        /// </param>
        /// <param name="iMaxlen">
        /// The i maxlen.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_TranslateError")]
        internal static extern int TranslateError(int errNr, StringBuilder sBuffer, int iMaxlen);

        /// <summary>
        /// The vel.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_VEL")]
        internal static extern int VEL(int iId, string sAxes, double[] dValArray);

        /// <summary>
        /// The vma.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sPiezoChannels">
        /// The s piezo channels.
        /// </param>
        /// <param name="dValarray">
        /// The d valarray.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_VMA")]
        internal static extern int VMA(int iId, string sPiezoChannels, double[] dValarray);

        /// <summary>
        /// The vmi.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sPiezoChannels">
        /// The s piezo channels.
        /// </param>
        /// <param name="dValarray">
        /// The d valarray.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_VMI")]
        internal static extern int VMI(int iId, string sPiezoChannels, double[] dValarray);

        /// <summary>
        /// The wa v_ lin.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="nStart">
        /// The n start.
        /// </param>
        /// <param name="nLength">
        /// The n length.
        /// </param>
        /// <param name="iAppend">
        /// The i append.
        /// </param>
        /// <param name="iSpeedUpDown">
        /// The i speed up down.
        /// </param>
        /// <param name="rAmplitude">
        /// The r amplitude.
        /// </param>
        /// <param name="rOffset">
        /// The r offset.
        /// </param>
        /// <param name="iSegmentLength">
        /// The i segment length.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WAV_LIN")]
        internal static extern int WAV_LIN(
            int iId, 
            string sAxes, 
            int nStart, 
            int nLength, 
            int iAppend, 
            int iSpeedUpDown, 
            double rAmplitude, 
            double rOffset, 
            int iSegmentLength);

        /// <summary>
        /// The wa v_ pnt.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="nStart">
        /// The n start.
        /// </param>
        /// <param name="nLength">
        /// The n length.
        /// </param>
        /// <param name="iAppend">
        /// The i append.
        /// </param>
        /// <param name="pPoints">
        /// The p points.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WAV_PNT")]
        internal static extern int WAV_PNT(
            int iId, 
            string sAxes, 
            int nStart, 
            int nLength, 
            int iAppend, 
            double[] pPoints);

        /// <summary>
        /// The wa v_ ramp.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="nStart">
        /// The n start.
        /// </param>
        /// <param name="nLength">
        /// The n length.
        /// </param>
        /// <param name="iAppend">
        /// The i append.
        /// </param>
        /// <param name="iCenterPoint">
        /// The i center point.
        /// </param>
        /// <param name="iSpeedUpDown">
        /// The i speed up down.
        /// </param>
        /// <param name="rAmplitude">
        /// The r amplitude.
        /// </param>
        /// <param name="rOffset">
        /// The r offset.
        /// </param>
        /// <param name="iSegmentLength">
        /// The i segment length.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WAV_RAMP")]
        internal static extern int WAV_RAMP(
            int iId, 
            string sAxes, 
            int nStart, 
            int nLength, 
            int iAppend, 
            int iCenterPoint, 
            int iSpeedUpDown, 
            double rAmplitude, 
            double rOffset, 
            int iSegmentLength);

        /// <summary>
        /// The wa v_ si n_ p.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="nStart">
        /// The n start.
        /// </param>
        /// <param name="nLength">
        /// The n length.
        /// </param>
        /// <param name="iAppend">
        /// The i append.
        /// </param>
        /// <param name="iCenterPoint">
        /// The i center point.
        /// </param>
        /// <param name="rAmplitude">
        /// The r amplitude.
        /// </param>
        /// <param name="rOffset">
        /// The r offset.
        /// </param>
        /// <param name="iSegmentLength">
        /// The i segment length.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WAV_SIN_P")]
        internal static extern int WAV_SIN_P(
            int iId, 
            string sAxes, 
            int nStart, 
            int nLength, 
            int iAppend, 
            int iCenterPoint, 
            double rAmplitude, 
            double rOffset, 
            int iSegmentLength);

        /// <summary>
        /// The wcl.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="lWaveTable">
        /// The l wave table.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WCL")]
        internal static extern int WCL(int iId, long lWaveTable);

        /// <summary>
        /// The wgo.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="iStartMod">
        /// The i start mod.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WGO")]
        internal static extern int WGO(int iId, string sAxes, int[] iStartMod);

        /// <summary>
        /// The wgr.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WGR")]
        internal static extern int WGR(int iId);

        /// <summary>
        /// The wms.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="iMaxWaveSize">
        /// The i max wave size.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WMS")]
        internal static extern int WMS(int iId, string sAxes, int[] iMaxWaveSize);

        /// <summary>
        /// The wpa.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sPassword">
        /// The s password.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="iParameterArray">
        /// The i parameter array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WPA")]
        internal static extern int WPA(int iId, string sPassword, string sAxes, uint[] iParameterArray);

        /// <summary>
        /// The q ccl.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="iComandLevel">
        /// The i comand level.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qCCL")]
        internal static extern int qCCL(int iId, ref int iComandLevel);

        /// <summary>
        /// The q cst.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="sNames">
        /// The s names.
        /// </param>
        /// <param name="iMaxlen">
        /// The i maxlen.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qCST")]
        internal static extern int qCST(int iId, string sAxes, StringBuilder sNames, int iMaxlen);

        /// <summary>
        /// The q ddl.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="iLinearizationTable">
        /// The i linearization table.
        /// </param>
        /// <param name="iOffset">
        /// The i offset.
        /// </param>
        /// <param name="iValues">
        /// The i values.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qDDL")]
        internal static extern int qDDL(int iId, int iLinearizationTable, int iOffset, int iValues, double[] dValArray);

        /// <summary>
        /// The q dfh.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qDFH")]
        internal static extern int qDFH(int iId, string sAxes, double[] dValArray);

        /// <summary>
        /// The q drc.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="iRecChannelId">
        /// The i rec channel id.
        /// </param>
        /// <param name="sRecSourceId">
        /// The s rec source id.
        /// </param>
        /// <param name="iRecOption">
        /// The i rec option.
        /// </param>
        /// <param name="iTriggerOption">
        /// The i trigger option.
        /// </param>
        /// <param name="ArraySize">
        /// The array size.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qDRC")]
        internal static extern int qDRC(
            int iId, 
            int[] iRecChannelId, 
            StringBuilder sRecSourceId, 
            int[] iRecOption, 
            int[] iTriggerOption, 
            int ArraySize);

        /// <summary>
        /// The q dr r_ sync.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="iRecChannelId">
        /// The i rec channel id.
        /// </param>
        /// <param name="iOffset">
        /// The i offset.
        /// </param>
        /// <param name="iValues">
        /// The i values.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qDRR_SYNC")]
        internal static extern int qDRR_SYNC(int iId, int iRecChannelId, int iOffset, int iValues, double[] dValArray);

        /// <summary>
        /// The q err.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="nError">
        /// The n error.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qERR")]
        internal static extern int qERR(int iId, ref int nError);

        /// <summary>
        /// The q gwd.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="cAxis">
        /// The c axis.
        /// </param>
        /// <param name="iStart">
        /// The i start.
        /// </param>
        /// <param name="iLength">
        /// The i length.
        /// </param>
        /// <param name="dData">
        /// The d data.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qGWD")]
        internal static extern int qGWD(int iId, char cAxis, int iStart, int iLength, double[] dData);

        /// <summary>
        /// The q hlp.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sBuffer">
        /// The s buffer.
        /// </param>
        /// <param name="iMaxlen">
        /// The i maxlen.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qHLP")]
        internal static extern int qHLP(int iId, StringBuilder sBuffer, int iMaxlen);

        /// <summary>
        /// The q hpa.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sBuffer">
        /// The s buffer.
        /// </param>
        /// <param name="iMaxlen">
        /// The i maxlen.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qHPA")]
        internal static extern int qHPA(int iId, StringBuilder sBuffer, int iMaxlen);

        /// <summary>
        /// The q idn.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="iMaxlen">
        /// The i maxlen.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qIDN")]
        internal static extern int qIDN(int iId, StringBuilder buffer, int iMaxlen);

        /// <summary>
        /// The q imp.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="cAxis">
        /// The c axis.
        /// </param>
        /// <param name="iOffset">
        /// The i offset.
        /// </param>
        /// <param name="nrValues">
        /// The nr values.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qIMP")]
        internal static extern int qIMP(int iId, char cAxis, int iOffset, int nrValues, double[] dValArray);

        /// <summary>
        /// The q mov.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qMOV")]
        internal static extern int qMOV(int iId, string sAxes, double[] dValArray);

        /// <summary>
        /// The q ovf.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="iOverFlow">
        /// The i over flow.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qOVF")]
        internal static extern int qOVF(int iId, string sAxes, int[] iOverFlow);

        /// <summary>
        /// The q pos.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qPOS")]
        internal static extern int qPOS(int iId, string sAxes, double[] dValArray);

        /// <summary>
        /// The q sai.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="iMaxlen">
        /// The i maxlen.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qSAI")]
        internal static extern int qSAI(int iId, StringBuilder sAxes, int iMaxlen);

        /// <summary>
        /// The q sa i_ all.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="iMaxlen">
        /// The i maxlen.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qSAI_ALL")]
        internal static extern int qSAI_ALL(int iId, StringBuilder sAxes, int iMaxlen);

        /// <summary>
        /// The q sep.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="iParameterArray">
        /// The i parameter array.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <param name="sStrings">
        /// The s strings.
        /// </param>
        /// <param name="iMaxNameSize">
        /// The i max name size.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qSEP")]
        internal static extern int qSEP(
            int iId, 
            string sAxes, 
            uint[] iParameterArray, 
            double[] dValArray, 
            StringBuilder sStrings, 
            int iMaxNameSize);

        /// <summary>
        /// The q spa.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="iParameterArray">
        /// The i parameter array.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <param name="sStrings">
        /// The s strings.
        /// </param>
        /// <param name="iMaxNameSize">
        /// The i max name size.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qSPA")]
        internal static extern int qSPA(
            int iId, 
            string sAxes, 
            uint[] iParameterArray, 
            double[] dValArray, 
            StringBuilder sStrings, 
            int iMaxNameSize);

        /// <summary>
        /// The q ste.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="cAxis">
        /// The c axis.
        /// </param>
        /// <param name="iOffset">
        /// The i offset.
        /// </param>
        /// <param name="nrValues">
        /// The nr values.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qSTE")]
        internal static extern int qSTE(int iId, char cAxis, int iOffset, int nrValues, double[] dValArray);

        /// <summary>
        /// The q sva.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qSVA")]
        internal static extern int qSVA(int iId, string sAxes, double[] dValArray);

        /// <summary>
        /// The q svo.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="iValArray">
        /// The i val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qSVO")]
        internal static extern int qSVO(int iId, string sAxes, int[] iValArray);

        /// <summary>
        /// The q tad.
        /// </summary>
        /// <param name="ID">
        /// The id.
        /// </param>
        /// <param name="sSensorChannels">
        /// The s sensor channels.
        /// </param>
        /// <param name="iValarray">
        /// The i valarray.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTAD")]
        internal static extern int qTAD(int ID, string sSensorChannels, int[] iValarray);

        /// <summary>
        /// The q tlt.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="iLinearizationTable">
        /// The i linearization table.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTLT")]
        internal static extern int qTLT(int iId, ref int iLinearizationTable);

        /// <summary>
        /// The q tmn.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTMN")]
        internal static extern int qTMN(int iId, string sAxes, double[] dValArray);

        /// <summary>
        /// The q tmx.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTMX")]
        internal static extern int qTMX(int iId, string sAxes, double[] dValArray);

        /////////////////////////////////////////////////////////////////////////////
        // Wave commands.

        /// <summary>
        /// The q tnr.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="iRecChannels">
        /// The i rec channels.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTNR")]
        internal static extern int qTNR(int iId, ref int iRecChannels);

        /// <summary>
        /// The q tns.
        /// </summary>
        /// <param name="ID">
        /// The id.
        /// </param>
        /// <param name="sSensorChannels">
        /// The s sensor channels.
        /// </param>
        /// <param name="dValarray">
        /// The d valarray.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTNS")]
        internal static extern int qTNS(int ID, string sSensorChannels, double[] dValarray);

        /// <summary>
        /// The q tpc.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="iPiezoCannels">
        /// The i piezo cannels.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTPC")]
        internal static extern int qTPC(int iId, ref int iPiezoCannels);

        /// <summary>
        /// The q tsc.
        /// </summary>
        /// <param name="ID">
        /// The id.
        /// </param>
        /// <param name="iSensorCannels">
        /// The i sensor cannels.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTSC")]
        internal static extern int qTSC(int ID, ref int iSensorCannels);

        /// <summary>
        /// The q tsp.
        /// </summary>
        /// <param name="ID">
        /// The id.
        /// </param>
        /// <param name="sSensorChannels">
        /// The s sensor channels.
        /// </param>
        /// <param name="dValarray">
        /// The d valarray.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTSP")]
        internal static extern int qTSP(int ID, string sSensorChannels, double[] dValarray);

        /// <summary>
        /// The q tvi.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sValideAxisIds">
        /// The s valide axis ids.
        /// </param>
        /// <param name="iMaxlen">
        /// The i maxlen.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTVI")]
        internal static extern int qTVI(int iId, StringBuilder sValideAxisIds, int iMaxlen);

        /// <summary>
        /// The q twg.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="iGenerator">
        /// The i generator.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTWG")]
        internal static extern int qTWG(int iId, ref int iGenerator);

        /// <summary>
        /// The q vel.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qVEL")]
        internal static extern int qVEL(int iId, string sAxes, double[] dValArray);

        /// <summary>
        /// The q vma.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sPiezoChannels">
        /// The s piezo channels.
        /// </param>
        /// <param name="dValarray">
        /// The d valarray.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qVMA")]
        internal static extern int qVMA(int iId, string sPiezoChannels, double[] dValarray);

        /// <summary>
        /// The q vmi.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sPiezoChannels">
        /// The s piezo channels.
        /// </param>
        /// <param name="dValarray">
        /// The d valarray.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qVMI")]
        internal static extern int qVMI(int iId, string sPiezoChannels, double[] dValarray);

        /// <summary>
        /// The q vol.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sPiezoChannels">
        /// The s piezo channels.
        /// </param>
        /// <param name="dValarray">
        /// The d valarray.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qVOL")]
        internal static extern int qVOL(int iId, string sPiezoChannels, double[] dValarray);

        /// <summary>
        /// The q vst.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sValideStages">
        /// The s valide stages.
        /// </param>
        /// <param name="iMaxlen">
        /// The i maxlen.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qVST")]
        internal static extern int qVST(int iId, StringBuilder sValideStages, int iMaxlen);

        /// <summary>
        /// The q wav.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="iCmdarray">
        /// The i cmdarray.
        /// </param>
        /// <param name="dValArray">
        /// The d val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qWAV")]
        internal static extern int qWAV(int iId, string sAxes, int[] iCmdarray, double[] dValArray);

        /// <summary>
        /// The q wgo.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="iValArray">
        /// The i val array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qWGO")]
        internal static extern int qWGO(int iId, string sAxes, int[] iValArray);

        /// <summary>
        /// The q wms.
        /// </summary>
        /// <param name="iId">
        /// The i id.
        /// </param>
        /// <param name="sAxes">
        /// The s axes.
        /// </param>
        /// <param name="iMaxWaveSize">
        /// The i max wave size.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qWMS")]
        internal static extern int qWMS(int iId, string sAxes, int[] iMaxWaveSize);

        #endregion
    }
}