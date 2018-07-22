#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityAndroidNative.Private {
    internal class AndroidReflection {
        public static bool IsPrimitive(System.Type t) {
            return t.IsPrimitive;
        }
    }
}
#endif