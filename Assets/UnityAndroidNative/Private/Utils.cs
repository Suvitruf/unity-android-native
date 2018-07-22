#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityAndroidNative.Private {
    internal class Utils {
        internal static string GetProjectPrefix() {
            return "UnityAndroidNative";
        }
    }
}
#endif