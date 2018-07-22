#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityAndroidNative.Android.Os {
    public class Bundle : BaseBundle {
        public Bundle(IntPtr obj) : base(obj) {
        }
    }
}
#endif