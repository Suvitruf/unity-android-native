#if UNITY_ANDROID
using System;
using UnityAndroidNative.Android.Os;
using UnityAndroidNative.Private;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace Assets.UnityAndroidNative.Android.Content.Pm {
    public class PackageInfo : Object, Parcelable {

        private int mVersionCode = -1;
        private string mVersionName;

        public int VersionCode {
            get {
                if(mVersionCode == -1)
                    mVersionCode = Get<int>("versionCode");

                return mVersionCode;
            }
        }

        public string VersionName {
            get {
                if (string.IsNullOrEmpty(mVersionName))
                    mVersionName = Get<string>("versionName");

                return mVersionName;
            }
        }

        public PackageInfo(IntPtr obj) : base(obj) {
        }

        public JavaObject GetInternalObject() {
            return this;
        }
    }
}
#endif