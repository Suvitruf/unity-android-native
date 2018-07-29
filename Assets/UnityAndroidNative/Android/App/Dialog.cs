#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace UnityAndroidNative.Android.App {

    public class Dialog : Object {

        /// <summary>
        /// The identifier for the positive button.
        /// </summary>
        public const int BUTTON_POSITIVE = -1;

        /// <summary>
        /// The identifier for the negative button.
        /// </summary>
        public const int BUTTON_NEGATIVE = -2;

        /// <summary>
        /// The identifier for the neutral button.
        /// </summary>
        public const int BUTTON_NEUTRAL = -3;

        public Dialog(IntPtr obj) : base(obj) {
        }

        /// <summary>
        /// Set the title text for this dialog's window.
        /// </summary>
        /// <param name="title">The new text to display in the title.</param>
        /// <returns></returns>
        public Dialog SetTitle(string title) {
            CallVoid("setTitle", title);

            return this;
        }

        /// <summary>
        /// Set the title text for this dialog's window. The text is retrieved from the resources with the supplied identifier.
        /// </summary>
        /// <param name="titleId">The title's text resource identifier</param>
        /// <returns></returns>
        public Dialog SetTitle(int titleId) {
            CallVoid("setTitle", titleId);

            return this;
        }

        /// <summary>
        /// Start the dialog and display it on screen.  The window is placed in the 
        /// application layer and opaque.Note that you should not override this
        /// method to do initialization when the dialog is shown, instead implement
        /// that in onStart.
        /// </summary>
        public void Show() {
            CallVoid("show");
        }
    }
}
#endif