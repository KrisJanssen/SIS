/* Functions exported by the TimeHarp 200 Driver-DLL THLib.dll*/

/* Ver. 6.1.0.0 February 2009*/

#ifdef __linux__
#define _stdcall
#endif

extern int _stdcall TH_GetLibraryVersion(char* vers);
extern int _stdcall TH_GetHardwareVersion(char* vers);
extern int _stdcall TH_GetSerialNumber(char* serial);
extern int _stdcall TH_GetBaseResolution(void);
extern int _stdcall TH_GetErrorString(char* errstring, int errcode);

extern int _stdcall TH_Initialize(int mode);
extern int _stdcall TH_Shutdown(void);
extern int _stdcall TH_Calibrate(void);

extern int _stdcall TH_SetCFDDiscrMin(int value);
extern int _stdcall TH_SetCFDZeroCross(int value);
extern int _stdcall TH_SetSyncLevel(int value);
extern int _stdcall TH_SetStopOverflow(int stop_ovfl, int stopcount);
extern int _stdcall TH_SetRange(int range);
extern int _stdcall TH_SetOffset(int offset);
extern int _stdcall TH_NextOffset(int direction);

extern int _stdcall TH_ClearHistMem(int block);
extern int _stdcall TH_SetMMode(int mmode, int tacq);
extern int _stdcall TH_StartMeas(void);
extern int _stdcall TH_StopMeas(void);
extern int _stdcall TH_CTCStatus(void);
extern int _stdcall TH_SetSyncMode(void);

extern int _stdcall TH_GetBlock(unsigned int *chcount, int block);
extern float _stdcall  TH_GetResolution(void);
extern int _stdcall  TH_GetCountRate(void);
extern int _stdcall  TH_GetFlags(void);
extern int _stdcall TH_GetElapsedMeasTime(void);

//for routing
extern int _stdcall TH_GetRouterModel(char* model);
extern int _stdcall TH_GetRoutingChannels(void);
extern int _stdcall TH_EnableRouting(int enable);
extern int _stdcall TH_SetNRT400CFD(int channel, int level, int zerocross);

//for continuous mode
extern int _stdcall TH_GetBank(unsigned short *buffer, int chanfrom, int chanto);
extern int _stdcall TH_GetLostBlocks(void);

//for TTTR mode
extern int _stdcall TH_T3RDoDMA(unsigned int* buffer, unsigned int count);
extern int _stdcall TH_T3RStartDMA(unsigned int* buffer, unsigned int count);
extern int _stdcall TH_T3RCompleteDMA(void);
extern int _stdcall TH_T3RSetMarkerEdges(int me0, int me1, int me2);

//for SCX 200 scanning (in TTTR mode)
extern int _stdcall TH_ScanEnable(int enable);
extern int _stdcall TH_ScanGotoXY(int x, int y, int shutter);
extern int _stdcall TH_ScanDone(void);
extern int _stdcall TH_ScanSetup(int startx, int starty, int widthx, int widthy, int pixtime, int pause, int shutterdel);
extern int _stdcall TH_ScanReset(void);

