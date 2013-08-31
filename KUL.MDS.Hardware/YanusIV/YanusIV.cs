using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KUL.MDS.Hardware
{
    class YanusIV : IPiezoStage
    {

        string IPiezoStage.CurrentError
        {
            get { throw new NotImplementedException(); }
        }

        double IPiezoStage.XPosition
        {
            get { throw new NotImplementedException(); }
        }

        double IPiezoStage.YPosition
        {
            get { throw new NotImplementedException(); }
        }

        double IPiezoStage.ZPosition
        {
            get { throw new NotImplementedException(); }
        }

        int IPiezoStage.SamplesWritten
        {
            get { throw new NotImplementedException(); }
        }

        bool IPiezoStage.IsInitialized
        {
            get { throw new NotImplementedException(); }
        }

        bool IPiezoStage.IsMoving
        {
            get { throw new NotImplementedException(); }
        }

        event EventHandler IPiezoStage.PositionChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        event EventHandler IPiezoStage.ErrorOccurred
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        event EventHandler IPiezoStage.EngagedChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        void IPiezoStage.Initialize()
        {
            throw new NotImplementedException();
        }

        void IPiezoStage.Configure(double __dCycleTimeMilisec, int __iSteps)
        {
            throw new NotImplementedException();
        }

        void IPiezoStage.Release()
        {
            throw new NotImplementedException();
        }

        void IPiezoStage.Home()
        {
            throw new NotImplementedException();
        }

        void IPiezoStage.MoveAbs(double __dXPosNm, double __dYPosNm, double __dZPosNm)
        {
            throw new NotImplementedException();
        }

        void IPiezoStage.MoveRel(double __dXPosNm, double __dYPosNm, double __dZPosNm)
        {
            throw new NotImplementedException();
        }

        void IPiezoStage.Scan(ScanModes.Scanmode __scmScanMode, bool __bResend)
        {
            throw new NotImplementedException();
        }

        void IPiezoStage.Stop()
        {
            throw new NotImplementedException();
        }

        //Codes are returned by the DSC via RS232 as decimal numbers in ASCII representation.
        public enum Error
        {
            SCAN_CMD_NO_ERROR = 0,
            // Calculation took too long for 10 us frame
            SCAN_CMD_RUN_TIMEOUT = 1,
            // Run aborted by sending character on RS232
            SCAN_CMD_RUN_ABORTED = 2,
            // Trying to run an empty command list
            SCAN_CMD_LIST_EMPTY = 3,
            // Trying to run a command list with unclosed loops.
            SCAN_CMD_LIST_NOT_CLOSED = 4,
            // Tried to add command to a command-list that has // already maximum length.
            SCAN_CMD_LIST_OVERFLOW = 10,
            // Cycle time of newly added command is impossible.
            SCAN_CMD_LIST_DISORDER = 11,
            // Command is specified with non-existing channel number.
            SCAN_CMD_INVALID_CHANNEL = 12,
            // Too many nested loops.
            SCAN_CMD_LOOP_OVERFLOW = 13,
            // Invalid number of loop, iterations (<0)
            SCAN_CMD_INVALID_ITERATIONS = 14,
            // Trying to close a loop, which is already closed.
            SCAN_CMD_LOOP_IS_CLOSED = 15,
            // Unknown command
            SCAN_CMD_UNKONWN_COMMAND = 16,
            // No debug buffer available for the selected channel
            SCAN_CMD_NO_BUFFER = 17,
            // Invalid syntax when adding a scan command:
            // not enough parameters or too many parameters.
            SCAN_CMD_INVALID_SYNTAX = 18
        }
    }
}
