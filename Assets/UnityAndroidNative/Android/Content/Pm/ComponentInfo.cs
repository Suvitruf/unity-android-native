#if UNITY_ANDROID
using System;

namespace UnityAndroidNative.Android.Content.Pm {
    public class ComponentInfo : PackageItemInfo {
        public ComponentInfo() : base() {
        }

        public ComponentInfo(IntPtr obj) : base(obj) {
        }

        public ComponentInfo(ComponentInfo orig) : base(orig) {
        }
    }
}
#endif