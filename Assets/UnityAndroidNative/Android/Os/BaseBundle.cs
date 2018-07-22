#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace UnityAndroidNative.Android.Os {
    public class BaseBundle : Object {
        public BaseBundle(IntPtr obj) : base(obj) {
        }

        public BaseBundle(string clsName, params object[] args) : base(clsName, args) {
        }
    }
}
#endif