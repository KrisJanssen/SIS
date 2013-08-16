using System;
using System.Threading;
using System.Windows.Forms;

namespace DevDefined.Tools.UI
{
    /// <summary>
    /// Helper class containing utility methods for working with the Focus of components.
    /// </summary>
    public static class FocusHelper
    {
        /// <summary>
        /// Invoke with a form and a number of seconds and the helper will attempt to keep the form focused
        /// for that period of time.
        /// </summary>
        /// <param name="form">The form to retain focus on</param>
        /// <param name="seconds">The number of seconds to retain focus for</param>
        /// <remarks>This method uses the <see cref="ThreadPool" /> to asynchronously maintain focus.</remarks>
        public static void EnqueRetainFocusCallback(Form form, int seconds)
        {
            WaitCallback callback = delegate
                                        {
                                            try
                                            {
                                                for (int i = 0; i < (seconds*10); i++)
                                                {
                                                    Thread.Sleep(100);

                                                    // must ensure the form hasn't be disposed (in case the user closes the 
                                                    // form before the retain focus time has expired).

                                                    if (form.IsDisposed || form.Disposing) return;

                                                    if (!form.IsHandleCreated) continue;

                                                    form.Invoke(new ThreadStart(delegate
                                                                                    {
                                                                                        if (form.IsDisposed ||
                                                                                            form.Disposing) return;
                                                                                        form.Focus();
                                                                                    }));
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                throw;
                                                // TODO: fix this
                                                /*if (IoC.IsInitialized)
                                                {
                                                    ILogger logger =
                                                        IoC.Resolve<ILoggerFactory>().Create(form.GetType());
                                                    logger.Error(
                                                        "Exception raised while attempting to maintain form focus, aborting callback",
                                                        ex);
                                                }
                                                return;*/
                                            }
                                        };

            ThreadPool.QueueUserWorkItem(callback);
        }
    }
}