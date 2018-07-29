#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityAndroidNative.Android.Content;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace UnityAndroidNative.Android.App {
    public class AlertDialog : Dialog {

        public AlertDialog(IntPtr obj) : base(obj) {
        }

        /// <summary>
        /// Set the message to display.
        /// </summary>
        /// <param name="message">This Builder object to allow for chaining of calls to set methods</param>
        /// <returns></returns>
        public AlertDialog SetMessage(string message) {
            CallVoid("setMessage", message);

            return this;
        }

        /// <summary>
        /// Set a listener to be invoked when the positive button of the dialog is pressed.
        /// </summary>
        /// <param name="whichButton">Which button to set the listener on, can be one of 
        /// <see cref="Dialog.BUTTON_POSITIVE"/>,
        /// <see cref="Dialog.BUTTON_NEGATIVE"/>
        /// <see cref="Dialog.BUTTON_NEUTRAL"/>
        /// </param>
        /// <param name="text">The text to display in positive button.</param>
        /// <param name="listener">The <see cref="OnClickListener"/> to use.</param>
        /// <returns></returns>
        public AlertDialog SetButton(int whichButton, string text, OnClickListener listener) {
            CallVoid("setButton", whichButton, text, listener);

            return this;
        }

        public class Builder : Object {
            public Builder(Context context) : base(context) {

            }

            public AlertDialog Create() {
                return Call<AlertDialog>("create");
            }

            /// <summary>
            /// Creates an <see cref="AlertDialog"/> with the arguments supplied to this builder and immediately displays the dialog.
            /// </summary>
            public void Show() {
                CallVoid("show");
            }
        }
    }
}
#endif