#if UNITY_ANDROID
using System;

namespace UnityAndroidNative.Android.Content.Pm {
    public class ComponentInfo : PackageItemInfo {
        public ComponentInfo(IntPtr obj) : base(obj) {
        }
    }
}
#endif