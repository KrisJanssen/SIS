using System;
using System.Runtime.InteropServices;
using System.Text;


namespace KUL.MDS.Hardware
{
	/// <summary>
	/// Summary description for E7XX.
	/// </summary>
	internal static class E7XXController
	{
		///////////////////////////
		// E-7XX Bits (BIT_XXX). //
		///////////////////////////

		///* Curve Controll E7XX_BIT_WGO_XXX */
		internal const int BIT_WGO_START_DEFAULT		    = 0x00000001;
		internal const int BIT_WGO_START_EXTERN_TRIGGER		= 0x00000002;
		internal const int BIT_WGO_WITH_DDL_INITIALISATION	= 0x00000040;
		internal const int BIT_WGO_WITH_DDL					= 0x00000080;
		internal const int BIT_WGO_START_AT_ENDPOSITION		= 0x00000100;
		internal const int BIT_WGO_SINGLE_RUN_DDL_TEST	    = 0x00000200;
		internal const int BIT_WGO_SAVE_BIT_1				= 0x00100000;
		internal const int BIT_WGO_SAVE_BIT_2				= 0x00200000;
		internal const int BIT_WGO_SAVE_BIT_3				= 0x00400000;

		/* Wave-Trigger E7XX_BIT_TRG_XXX */
		internal const int BIT_TRG_LINE_1					= 0x0001;
		internal const int BIT_TRG_LINE_2					= 0x0002;
		internal const int BIT_TRG_LINE_3					= 0x0004;
		internal const int BIT_TRG_LINE_4					= 0x0008;
		internal const int BIT_TRG_ALL_CURVE_POINTS		    = 0x0100;


		/* P(arameter)I(nfo)F(lag)_M(emory)T(ype)_XX */
		internal const int PIF_MT_RAM			= 0x00000001;
		internal const int PIF_MT_EPROM			= 0x00000002;
		internal const int PIF_MT_ALL			= PIF_MT_RAM | PIF_MT_EPROM;


		/////////////////////////////////////////////////////////////////////////////
		// DLL initialization and comm functions
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_InterfaceSetupDlg")]		internal static extern int	InterfaceSetupDlg(string sRegKeyName);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_ConnectRS232")]			internal static extern int	ConnectRS232(int nPortNr, int nBaudRate);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_ConnectNIgpib")]			internal static extern int	ConnectNIgpib(int nBoard, int nDevAddr);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_ConnectPciBoard")]		internal static extern int	ConnectPciBoard(int iBoardNumber);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_ChangeNIgpibAddress")]	internal static extern int	ChangeNIgpibAddress(int iId, int nDevAddr);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_IsConnected")]			internal static extern int	IsConnected(int iId);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_CloseConnection")]		internal static extern void	CloseConnection(int iId);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_GetError")]				internal static extern int	GetError(int iId);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_SetErrorCheck")]			internal static extern int	SetErrorCheck(int iId, int bErrorCheck);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_TranslateError")]			internal static extern int	TranslateError(int errNr, StringBuilder sBuffer, int iMaxlen);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_CountPciBoards")]			internal static extern int	CountPciBoards();

		
		/////////////////////////////////////////////////////////////////////////////
		// general
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qERR")]	internal static extern int	qERR(int iId, ref int nError);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qIDN")]	internal static extern int	qIDN(int iId, StringBuilder buffer, int iMaxlen);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_INI")]	internal static extern int	INI(int iId, string sAxes);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qHLP")]	internal static extern int	qHLP(int iId, StringBuilder sBuffer, int iMaxlen);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qHPA")]	internal static extern int	qHPA(int iId, StringBuilder sBuffer, int iMaxlen);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qOVF")]	internal static extern int	qOVF(int iId, string sAxes, int []iOverFlow);
		
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_MOV")]		internal static extern int	MOV(int iId, string sAxes, double []dValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qMOV")]		internal static extern int	qMOV(int iId, string sAxes, double []dValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_MVR")]		internal static extern int	MVR(int iId, string sAxes, double []dValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qPOS")]		internal static extern int	qPOS(int iId, string sAxes, double []dValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_IsMoving")]	internal static extern int	IsMoving(int iId, string sAxes, int []bValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_HLT")]		internal static extern int	HLT(int iId, string sAxes);
		
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_SVA")]	internal static extern int	SVA(int iId, string sAxes, double []dValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qSVA")]	internal static extern int	qSVA(int iId, string sAxes, double []dValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_SVR")]	internal static extern int	SVR(int iId, string sAxes, double []dValArray);
		
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_DFH")]	internal static extern int	DFH(int iId, string sAxes);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qDFH")]	internal static extern int	qDFH(int iId, string sAxes, double []dValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_GOH")]	internal static extern int	GOH(int iId, string sAxes);
		
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qCST")]	internal static extern int	qCST(int iId, string sAxes, StringBuilder sNames, int iMaxlen);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_CST")]	internal static extern int	CST(int iId, string sAxes, string names);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qVST")]	internal static extern int	qVST(int iId, StringBuilder sValideStages, int iMaxlen);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTVI")]	internal static extern int	qTVI(int iId, StringBuilder sValideAxisIds, int iMaxlen);
		
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_SVO")]	internal static extern int	SVO(int iId, string sAxes, int []iValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qSVO")]	internal static extern int	qSVO(int iId, string sAxes, int []iValArray);
		
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_VEL")]	internal static extern int	VEL(int iId, string sAxes, double []dValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qVEL")]	internal static extern int	qVEL(int iId, string sAxes, double []dValArray);
		
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_SPA")]	internal static extern int	SPA(int iId, string sAxes, uint []iParameterArray, double []dValArray, string sStrings);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qSPA")]	internal static extern int	qSPA(int iId, string sAxes, uint []iParameterArray, double []dValArray, StringBuilder sStrings, int iMaxNameSize);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_SEP")]	internal static extern int	SEP(int iId, string sPassword, string sAxes, uint []iParameterArray, double []dValArray, string szStrings);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qSEP")]	internal static extern int	qSEP(int iId, string sAxes, uint []iParameterArray, double []dValArray, StringBuilder sStrings, int iMaxNameSize);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WPA")]	internal static extern int	WPA(int iId, string sPassword, string sAxes, uint []iParameterArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_RPA")]	internal static extern int	RPA(int iId, string sAxes, uint []iParameterArray);
		
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_STE")]	internal static extern int	STE(int iId, char cAxis, double dOffset);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qSTE")]	internal static extern int	qSTE(int iId, char cAxis, int iOffset, int nrValues, double []dValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_IMP")]	internal static extern int	IMP(int iId, char cAxis, double dOffset);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qIMP")]	internal static extern int	qIMP(int iId, char cAxis, int iOffset, int nrValues, double []dValArray);
		
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_SAI")]		internal static extern int	SAI(int iId, string sOldAxes, string sNewAxes);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qSAI")]		internal static extern int	qSAI(int iId, StringBuilder sAxes, int iMaxlen);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qSAI_ALL")]	internal static extern int	qSAI_ALL(int iId, StringBuilder sAxes, int iMaxlen);
		
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_CCL")]		internal static extern int	CCL(int iId, int iComandLevel, string sPassWord);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qCCL")]		internal static extern int	qCCL(int iId, ref int iComandLevel);
		
		
		/////////////////////////////////////////////////////////////////////////////
		// String commands.
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_E7XXSendString")]		internal static extern int	E7XXSendString(int iId, string sString);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_E7XXReadLine")]		internal static extern int	E7XXReadLine(int iId, StringBuilder sAnswer, int iBufSize);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_GcsCommandset")]		internal static extern int	GcsCommandset(int iId, string sCommand);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_GcsGetAnswer")]		internal static extern int	GcsGetAnswer(int iId, StringBuilder sAnswer, int iBufSize);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_GcsGetAnswerSize")]	internal static extern int	GcsGetAnswerSize(int iId, ref int iAnswerSize);
		
		
		/////////////////////////////////////////////////////////////////////////////
		// limits.
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_ATZ")]	internal static extern int	ATZ(int iId,  string sAxes, double []dLowVoltageArray, int []fUseDefaultArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTMN")]	internal static extern int	qTMN(int iId, string sAxes, double []dValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTMX")]	internal static extern int	qTMX(int iId, string sAxes, double []dValArray);
		
		
		/////////////////////////////////////////////////////////////////////////////
		// Wave commands.
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WMS")]		internal static extern int	WMS(int iId, string sAxes, int []iMaxWaveSize);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qWMS")]		internal static extern int	qWMS(int iId, string sAxes, int []iMaxWaveSize);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WAV_SIN_P")]	internal static extern int	WAV_SIN_P(int iId, string sAxes, int nStart, int nLength, int iAppend, int iCenterPoint, double rAmplitude, double rOffset, int iSegmentLength);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WAV_LIN")]	internal static extern int	WAV_LIN(int iId, string sAxes, int nStart, int nLength, int iAppend, int iSpeedUpDown, double rAmplitude, double rOffset, int iSegmentLength);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WAV_RAMP")]	internal static extern int	WAV_RAMP(int iId, string sAxes, int nStart, int nLength, int iAppend, int iCenterPoint, int iSpeedUpDown, double rAmplitude, double rOffset, int iSegmentLength);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WAV_PNT")]	internal static extern int	WAV_PNT(int iId, string sAxes, int nStart, int nLength, int iAppend, double []pPoints);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qWAV")]		internal static extern int	qWAV(int iId, string sAxes, int []iCmdarray, double []dValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qGWD")]		internal static extern int	qGWD(int iId, char cAxis, int iStart, int iLength, double []dData);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WGO")]		internal static extern int	WGO(int iId, string sAxes, int []iStartMod);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qWGO")]		internal static extern int	qWGO(int iId, string sAxes, int []iValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WGR")]		internal static extern int	WGR(int iId);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTNR")]		internal static extern int	qTNR(int iId, ref int iRecChannels);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_DRC")]		internal static extern int	DRC(int iId, int []iRecChannelId, string sRecSourceId, int []iRecOption, int []iTriggerOption);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qDRC")]		internal static extern int	qDRC(int iId, int []iRecChannelId, StringBuilder sRecSourceId, int []iRecOption, int []iTriggerOption, int ArraySize);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qDRR_SYNC")]	internal static extern int	qDRR_SYNC(int iId, int iRecChannelId, int iOffset, int iValues, double []dValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTWG")]		internal static extern int	qTWG(int iId, ref int iGenerator);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTLT")]		internal static extern int	qTLT(int iId, ref int iLinearizationTable);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_DDL")]		internal static extern int	DDL(int iId, int iLinearizationTable, int iOffset, int iValues, double []dValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qDDL")]		internal static extern int	qDDL(int iId, int iLinearizationTable, int iOffset, int iValues, double []dValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_DTC")]		internal static extern int	DTC(int iId, int iLearnTable, int bClearWaveAllsow);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_TWS")]		internal static extern int	TWS(int iId, int []iWavePoint, int []iTriggerLevel, int iNumberOfPoints);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_TWC")]		internal static extern int	TWC(int iId);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_DPO")]		internal static extern int	DPO(int iId, string sAxes);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_IsGeneratorRunning")]		internal static extern int	IsGeneratorRunning(int iId, string sAxes, int []pValArray);
        [DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_WCL")]        internal static extern int WCL(int iId, long lWaveTable);
		
		/////////////////////////////////////////////////////////////////////////////
		// Piezo-Channel commands.
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_VMA")]	internal static extern int	VMA(int iId, string sPiezoChannels, double []dValarray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qVMA")]	internal static extern int	qVMA(int iId, string sPiezoChannels, double []dValarray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_VMI")]	internal static extern int	VMI(int iId, string sPiezoChannels, double []dValarray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qVMI")]	internal static extern int	qVMI(int iId, string sPiezoChannels, double []dValarray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qVOL")]	internal static extern int	qVOL(int iId, string sPiezoChannels, double []dValarray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTPC")]	internal static extern int	qTPC(int iId, ref int iPiezoCannels);
		
		
		/////////////////////////////////////////////////////////////////////////////
		// Sensor-Channel commands.
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTAD")]	internal static extern int	qTAD(int ID, string sSensorChannels, int []iValarray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTNS")]	internal static extern int	qTNS(int ID, string sSensorChannels, double []dValarray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTSP")]	internal static extern int	qTSP(int ID, string sSensorChannels, double []dValarray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_qTSC")]	internal static extern int	qTSC(int ID, ref int iSensorCannels);
		
		
		///////////////////////////////////////////////////////////////////////////////
		//// AutoFocus (6 channel version only).
		//BOOL E7XX_FUNC_DECL E7XX_AFB(const int ID, char* const szAxes, double* pdValarray);
		//BOOL E7XX_FUNC_DECL E7XX_qAFB(const int ID, char* const szAxes, double* pdValarray);
		//BOOL E7XX_FUNC_DECL E7XX_AFR(const int ID, char* const szAxes, double* pdValarray);
		//BOOL E7XX_FUNC_DECL E7XX_qAFR(const int ID, char* const szAxes, double* pdValarray);
		//BOOL E7XX_FUNC_DECL E7XX_qAFI(const int ID, char* const szAxes, double* pdValarray);
		//
		//
		///////////////////////////////////////////////////////////////////////////////
		//// Flatness Compensation (6 channel version only).
		//BOOL E7XX_FUNC_DECL E7XX_FCG(const int ID, char const cAxis, char* const szExternalAxes, double* pdMaxValarray, double* pdMinValarray, int* NumberOfPointsArray);
		//BOOL E7XX_FUNC_DECL E7XX_qFCG(const int ID, char const cAxis, char* const szExternalAxes, double* pdMaxValarray, double* pdMinValarray, int* NumberOfPointsArray);
		//BOOL E7XX_FUNC_DECL E7XX_FCA(const int ID, char* const szAxes, BOOL* pbActive);
		//BOOL E7XX_FUNC_DECL E7XX_qFCA(const int ID, char* const szAxes, BOOL* pbActive);
		//BOOL E7XX_FUNC_DECL E7XX_FCD(const int ID, char const cAxis, int iArraySize, double* pdValarray);
		//BOOL E7XX_FUNC_DECL E7XX_qFCD(const int ID, char const cAxis, double* pdValarray, int iArrayMaxSize);
		//BOOL E7XX_FUNC_DECL E7XX_FCP(const int ID, char* const szExternalAxes, double* pdPositionArray);
		//BOOL E7XX_FUNC_DECL E7XX_qFCP(const int ID, char* const szExternalAxes, double* pdPositionArray);
		//
		//
		///////////////////////////////////////////////////////////////////////////////
		//// Crosstalk correction (6 channel version only).
		//BOOL E7XX_FUNC_DECL E7XX_CTC(const int ID, char const cAxis, char* const szCosstalkAxes, double* pdRange, double* pdErrorFactor);
		//
		//
		///////////////////////////////////////////////////////////////////////////////
		//// DDL Auto Examine (6 channel version only).
		//BOOL E7XX_FUNC_DECL E7XX_DAS(const int ID, char* const szAxes, BOOL* pbEnable, int* plLowerLimit, int* plUperLimit);
		//BOOL E7XX_FUNC_DECL E7XX_qDAS(const int ID, char* const szAxes, BOOL* pbEnable, int* plLowerLimit, int* plUperLimit);
		//BOOL E7XX_FUNC_DECL E7XX_qDAE(const int ID, char* const szAxes, double* pdAverageError);
		//BOOL E7XX_FUNC_DECL E7XX_qDAD(const int ID, char* const szAxes, double* pdDeviation);
		//BOOL E7XX_FUNC_DECL E7XX_DAC(const int ID, char* const szAxes);
		//
		//
		///////////////////////////////////////////////////////////////////////////////
		//// Special
		//BOOL	E7XX_FUNC_DECL	E7XX_AddStage(const int ID, char* const szAxes);
		//BOOL	E7XX_FUNC_DECL	E7XX_RemoveStage(const int ID, char* szStageName);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_GetSupportedFunctions")]		internal static extern int	GetSupportedFunctions(int ID, int []piComandLevelArray, int iMaxlen, StringBuilder sFunctionNames, int iMaxFunctioNamesLength);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_GetSupportedParameters")]		internal static extern int	GetSupportedParameters(int ID, int []piParameterIdArray, int []piComandLevelArray, int []piMemoryLocationArray, int iMaxlen, StringBuilder sParameterName, int iMaxParameterNameSize);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_GetSupportrdControllers")]	internal static extern int	GetSupportrdControllers(StringBuilder szControllerNames, int iMaxlen);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_NMOV")]		internal static extern int	NMOV(int iId, string sAxes, double []dValArray);
		[DllImport("E7XX_GCS_DLL.dll", EntryPoint = "E7XX_NMVR")]		internal static extern int	NMVR(int iId, string sAxes, double []dValArray);
	}
}
