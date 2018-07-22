#if UNITY_ANDROID
using System;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace UnityAndroidNative.Android.Content.Pm {
    public class ResolveInfo : Object {
        private ActivityInfo mActivityInfo;

        public ActivityInfo ActivityInfo {
            get {
                if (mActivityInfo == null)
                    mActivityInfo = Get<ActivityInfo>("activityInfo");
                return mActivityInfo;
            }
        }

        public int Icon {
            get { return Get<int>("icon"); }
        }

        internal ResolveInfo(IntPtr obj) : base(obj) {
        }

        public string LoadLabel(PackageManager pm) {
            return Call<string>("loadLabel", pm);
        }       
    }
}
#endif