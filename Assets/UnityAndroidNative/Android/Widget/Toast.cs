#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityAndroidNative.Android.App;
using UnityAndroidNative.Android.Content;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace UnityAndroidNative.Android.Widget {
    public class Toast : Object {

        /// <summary>
        /// Show the view or text notification for a short period of time.  This time
        /// could be user-definable.This is the default. <see cref="SetDuration"/>
        /// </summary>
        public const int LENGTH_SHORT = 0;

        /// <summary>
        /// Show the view or text notification for a long period of time.  This time
        /// could be user-definable. <see cref="SetDuration"/>
        /// </summary>
        public const int LENGTH_LONG = 1;

        public Toast(Context context) : base(context) {

        }

        public Toast(IntPtr obj) : base(obj) {
        }

        /// <summary>
        /// Show the view for the specified duration.
        /// </summary>
        public void Show() {
            CallVoid("show");
        }

        /// <summary>
        /// Close the view if it's showing, or don't show it if it isn't showing yet.
        /// You do not normally have to call this.  Normally view will disappear on its own
        /// after the appropriate duration.
        /// </summary>
        public void Cancel() {
            CallVoid("cancel");
        }

        /// <summary>
        /// Set how long to show the view for.
        /// <seealso cref="LENGTH_SHORT"/>
        /// <seealso cref="LENGTH_LONG"/>
        /// </summary>
        /// <param name="duration"></param>
        public void SetDuration(int duration) {
            CallVoid("setDuration", duration);
        }

        /// <summary>
        ///  Return the duration.
        /// </summary>
        /// <seealso cref="SetDuration"/>
        /// <returns></returns>
        public int GetDuration() {
            return Call<int>("getDuration");
        }

        /// <summary>
        /// Make a standard toast that just contains a text view with the text from a resource.
        /// </summary>
        /// <param name="context">The context to use.  Usually your <see cref="Application"/> or <see cref="Activity"/> object.</param>
        /// <param name="resId">The resource id of the string resource to use.  Can be formatted text.</param>
        /// <param name="duration">How long to display the message.  Either <see cref="LENGTH_SHORT"/> or <see cref="LENGTH_LONG"/></param>
        /// <returns></returns>
        public static Toast MakeText(Context context, int resId, int duration) {
            return CallStatic<Toast, Toast>("makeText", context, resId, duration);
        }

        /// <summary>
        /// Make a standard toast that just contains a text view.
        /// </summary>
        /// <param name="context">The context to use.  Usually your <see cref="Application"/> or <see cref="Activity"/> object.</param>
        /// <param name="text">The text to show.  Can be formatted text.</param>
        /// <param name="duration">How long to display the message.  Either <see cref="LENGTH_SHORT"/> or <see cref="LENGTH_LONG"/></param>
        /// <returns></returns>
        public static Toast MakeText(Context context, string text, int duration) {
            return CallStatic<Toast, Toast>("makeText", context, text, duration);
        }
    }
}

#endif