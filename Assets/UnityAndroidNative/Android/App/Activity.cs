#if UNITY_ANDROID
using System;
using UnityAndroidNative.Android.Content;
using UnityAndroidNative.Android.View;

namespace UnityAndroidNative.Android.App {
    public class Activity : ContextThemeWrapper {


        public Activity(IntPtr obj) : base(obj) {
        }

        /// <summary>
        /// <para>
        /// Launch a new activity.  You will not receive any information about when
        /// the activity exits.  This implementation overrides the base version,
        /// providing information about
        /// the activity performing the launch.  Because of this additional
        /// information, the <see cref="Intent.FLAG_ACTIVITY_NEW_TASK"/> launch flag is not
        /// required; if not specified, the new activity will be added to the
        /// task of the caller.
        /// </para>
        /// <para>
        /// This method throws <see cref="ActivityNotFoundException"/>
        /// if there was no Activity found to run the given Intent.
        /// </para>
        /// </summary>
        /// <param name="intent">The intent to start</param>
        public void StartActivity(Intent intent) {
            CallVoid("startActivity", intent);
        }
    }
}
#endif