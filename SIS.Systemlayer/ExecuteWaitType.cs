using System;

namespace SIS.SystemLayer
{
    public enum ExecuteWaitType
    {
        /// <summary>
        /// Returns immediately after executing without waiting for the task to finish.
        /// </summary>
        ReturnImmediately,

        /// <summary>
        /// Waits until the task exits before returning control to the calling method.
        /// </summary>
        WaitForExit,

        /// <summary>
        /// Returns immediately after executing without waiting for the task to finish.
        /// However, another task will be spawned that will wait for the requested task
        /// to finish, and it will then relaunch SIS if the task was successful.
        /// This is only intended to be used by the SIS updater so that it can
        /// relaunch SIS with the same user and privilege-level that initiated
        /// the update.
        /// </summary>
        RelaunchPdnOnExit
    }
}
