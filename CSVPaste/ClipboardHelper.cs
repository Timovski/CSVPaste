using System.Threading;
using System.Windows.Forms;

namespace CSVPaste
{
    /// <summary>
    /// Helper class to work with the clipboard.
    /// </summary>
    internal class ClipboardHelper
    {
        /// <summary>
        /// Tries to get the clipboard data as a string.
        /// </summary>
        public static string GetText()
        {
            string text = null;
            var staThread = new Thread(() =>
            {
                try
                {
                    if (Clipboard.ContainsText())
                    {
                        text = Clipboard.GetText(TextDataFormat.UnicodeText);
                    }
                }
                catch
                {
                    // ignored
                }
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();

            return text;
        }
    }
}