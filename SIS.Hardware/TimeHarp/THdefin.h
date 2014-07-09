/* THLib Version 6.1 for TimeHarp 200 PCI
   PicoQuant GmbH, February 2009
   History: 
    6.1: no change except Windows driver and more demos
    6.0: SetStopOverflow with variable stop level
    5.3: Fix for TTTR on boards from #0119
    5.2: Support for NRT 400 and settable marker polarity
    5.0: Support for Scanning and External Markers 
 */


#define LIB_VERSION "6.1"

#define BLOCKSIZE	4096	
#define CURVES		32
#define BANKSIZE	CURVES*BLOCKSIZE
#define FIFOSIZE	131072  // 128K event records
#define RANGES		6

#define FLAG_SYSERR       0x0100
#define FLAG_OVERFLOW     0x0200
#define FLAG_RAMREADY     0x0400
#define FLAG_FIFOFULL     0x0800
#define FLAG_FIFOHALFFULL 0x1000
#define FLAG_FIFOEMPTY    0x2000
#define FLAG_SCANACTIVE   0x4000

#define ZCMIN		0			//mV
#define ZCMAX		40			//mV
#define DISCRMIN	0			//mV
#define DISCRMAX	400			//mV
#define SYNCMIN		-1300		//mV
#define SYNCMAX		400			//mV
#define OFFSETMIN	0			//ns
#define OFFSETMAX	2000		//ns
#define ACQTMIN		1			//ms
#define ACQTMAX		36000000	//ms  (10*60*60*1000ms = 10h) 

#define SCX_XMIN		0		//pixels
#define SCX_XMAX		4095	//pixels
#define SCX_YMIN		0		//pixels
#define SCX_YMAX		4095	//pixels
#define SCX_TPIXMIN		100		//us
#define SCX_TPIXMAX		3200	//us
#define SCX_PAUSEMIN	0		//pixel durations
#define SCX_PAUSEMAX	15		//pixel durations
#define SCX_SHUTDELMIN	0		//ms
#define SCX_SHUTDELMAX	1000	//ms

