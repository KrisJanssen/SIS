using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KUL.MDS.Hardware
{
    public struct DSCCommand
    {
        public UInt64 Cycle;
        public ScanCommand ScanCmd;
        public DSCChannel Channel;
        public Int64 Value;

        DSCCommand(UInt64 __ui64Cycle, ScanCommand __scCommand, DSCChannel __dsccChannel, Int64 __dValue)
        {
            Cycle = __ui64Cycle;
            ScanCmd = __scCommand;
            Channel = __dsccChannel;
            Value = __dValue;
        }
    }

    // Channel can be axes Channel 3 (X), Channel 4 (Y), Channel 5 (Polytrope) or
    // digital outputs (Channel 7). Value is either the galvo position or the 
    // digital outputs bit 	pattern.
    public enum DSCChannel
    {
        X = 3,
        Y = 4,
        Polytrope = 5,
        DO = 7
    }

    // Definition of DSC control commands
    public enum DSPCommand
    {
        // does nothing, but keeps the interface busy and signals // break of protocol execution, can also serve as comment // marker
        // no arguments
        DSP_CMD_DO_NOTHING = '#',
        // tells DSP to return null terminated information string // no arguments
        DSP_CMD_RETURN_INFO = 'R',
        // clears the scan control protocol // no arguments
        //
        // returns 0 on success
        DSP_CMD_CLEAR_PROT = 'C',
        // adds a command to the scan command list
        // arguments:
        // char <scanner control command (see above)>
        // cycles_t <cycle (current type long>
        // int <channel>
        // int <value>
        // all arguments must be present. Even if they don't apply
        // to the scan command //
        // returns 0 on success or an error
        DSP_CMD_ADD_PROT_CMD = 'A',
        // initialize protocol: sort commands within blocks ...(?) // currently not functional.
        // no arguments
        DSP_CMD_INIT_PROT = 'I',
        // executes the current protocol
        // no arguments
        //
        // returns 0 on success or a numerical error code
        DSP_CMD_EXECUTE_PROT = 'X',
        // list protocol // no arguments
        DSP_CMD_LIST_PROT = 'L',
        // shows the contents of the buffer for scan channel "channel" // argument:
        // int <channel>
        DSP_CMD_SHOW_BUFFER = 'B',
        // directly sets the value of a scan channel. Like the 'V'
        // command in scan protocols, but the current protocol remains
        // unchanged and the value is set immediately after the line
        // is parsed.
        // arguments:
        // int <channel>
        // long <value> //
        // returns 0 on success or a numerical error code
        DSP_CMD_SET_VALUE = 'V'
    }

    //The scan control commands have three parameters:
    //cycle (long; 48 bit)
    // number of 10μs cycle, in which command is executed
    //channel (integer; 24 bit)
    // output channel, whose increment is changed
    //value (long; 48 bit)
    // value to set
    //The scan control commands are:
    public enum ScanCommand
    {
        // dummy command. E.g. to extend execution of commands // beyond last "real" command
        scan_cmd_do_nothing = (int) '0',
        // command to directly set the value of an output channel // cycle: 10 μs cycle, in which value is set
        // channel: channel, which is set
        // value: value, to which the channel is set
        scan_cmd_set_value = 'V',
        // command to set value of an output channel relative // to the current value
        scan_cmd_set_value_relative = 'R',
        // command to set value by which a channel is incremented // every cycle (^= 1st derivative)
        // cycle: 10 μs cycle, in which value is set
        // channel: channel, whose 1st increment is set
        // value: increment value
        scan_cmd_set_increment_1 = 'I',
        // command to set value by which the increment of a channel // is incremented every cycle (^= 2nd derivative).
        // cycle: 10 μs cycle, in which value is set
        // channel: channel, whose 2nd increment is set
        // value: increment value
        scan_cmd_set_increment_2 = 'J',
        // start of a loop; Up to 100 loops can be nested. // channel: not used
        // value: number of iterations
        scan_cmd_loop_start = 'S',
        // end of a loop
        // channel: not used // value: not used
        scan_cmd_loop_end = 'E',
        // wait for rising trigger
        // cycle: execution of commands behind this one is halted // at this cycle until a rising trigger is detected
        // channel: not used
        // value: not used
        // While the protocol is waiting for a trigger, increments are // not applied.
        scan_cmd_wait_trig_rising = 'U',
        // wait for falling trigger
        // parameters and usage same as for scan_cmd_wait_trig_rising
        scan_cmd_wait_trig_falling = 'D'
    }
}
