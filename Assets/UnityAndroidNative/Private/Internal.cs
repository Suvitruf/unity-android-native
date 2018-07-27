#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityAndroidNative.Android.App;
using UnityEngine;

namespace UnityAndroidNative.Private {
    public class Internal {
        internal static bool mDebug = false;


        private static Activity mCurrentActivity;

        internal static readonly Dictionary<Type, string> mClassNames = new Dictionary<Type, string>();
        internal static readonly Dictionary<string, Type> mCsharpTypes = new Dictionary<string, Type>();
        internal static IntPtr mJavaLangClass;

        private static void Init() {
            try {
                mCurrentActivity = JavaObject.GetStatic<Activity>("com/unity3d/player/UnityPlayer", "currentActivity");
            }
            catch (Exception ex) {
                Debug.LogError("ex: " + ex.Message + " => " + ex.StackTrace);
            }
        }

        public static Activity GetCurrentActivity() {
            if (mCurrentActivity == null)
                Init();
            return mCurrentActivity;
        }
    }
}
#endif