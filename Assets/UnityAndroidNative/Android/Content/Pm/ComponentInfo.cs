#if UNITY_ANDROID
using System;

namespace UnityAndroidNative.Android.Content.Pm {
    public class ComponentInfo : PackageItemInfo {

        /// <summary>
        /// The name of the process this component should run in.
        /// From the "android:process" attribute or, if not set, the sameas applicationInfo.processName.
        /// </summary>
        public string ProcessName {
            get { return Get<string>("processName"); }
            set { Set("processName", value); }
        }

        public ComponentInfo() : base() {
        }

        public ComponentInfo(IntPtr obj) : base(obj) {
        }

        public ComponentInfo(ComponentInfo orig) : base(orig) {
        }
    }
}
#endif