#if UNITY_ANDROID
using System;

namespace UnityAndroidNative.Android.Content.Pm {
    public class ActivityInfo : ComponentInfo {

        public ActivityInfo(IntPtr obj) : base(obj) {
        }
    }
}
#endif