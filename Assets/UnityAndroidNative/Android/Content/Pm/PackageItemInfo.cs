#if UNITY_ANDROID
using System;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace UnityAndroidNative.Android.Content.Pm {
    public class PackageItemInfo : Object {

        public string PackageName {
            get {
                return Get<string>("packageName");
            }
        }

        public string Name {
            get {
                return Get<string>("name");
            }
        }

        internal PackageItemInfo(IntPtr obj) : base(obj) {
        }
    }
}
#endif