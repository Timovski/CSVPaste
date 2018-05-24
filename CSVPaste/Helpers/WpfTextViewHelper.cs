using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using System;

namespace CSVPaste.Helpers
{
    /// <summary>
    /// Helper class to work with <see cref="IWpfTextView"/>.
    /// </summary>
    internal class WpfTextViewHelper
    {
        /// <summary>
        /// Gets the <see cref="IWpfTextView"/> from the active view.
        /// </summary>
        /// <param name="serviceProvider">Owner package, not null.</param>
        /// <returns>The <see cref="IWpfTextView"/>.</returns>
        public static IWpfTextView GetWpfTextView(IServiceProvider serviceProvider)
        {
            var textManager = (IVsTextManager)serviceProvider.GetService(typeof(SVsTextManager));
            var componentModel = (IComponentModel)serviceProvider.GetService(typeof(SComponentModel));
            var editor = componentModel.GetService<IVsEditorAdaptersFactoryService>();

            IVsTextView textViewCurrent;
            textManager.GetActiveView(1, null, out textViewCurrent);
            return editor.GetWpfTextView(textViewCurrent);
        }

        /// <summary>
        /// Gets the current caret position in the <see cref="IWpfTextView"/>.
        /// </summary>
        /// <param name="wpfTextView">IWpfTextView of the active view.</param>
        /// <returns>The current caret position.</returns>
        public static int GetCaretPosition(IWpfTextView wpfTextView)
        {
            return wpfTextView.Caret.Position.BufferPosition.Position;
        }
    }
}