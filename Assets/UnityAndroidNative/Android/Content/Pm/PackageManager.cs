#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using UnityAndroidNative.Private;
using Object = UnityAndroidNative.Java.Lang.Object;
using PackageInfo = Assets.UnityAndroidNative.Android.Content.Pm.PackageInfo;

namespace UnityAndroidNative.Android.Content.Pm {
    public class PackageManager : Object {

        public PackageManager(IntPtr obj) : base(obj) {
        }

        public List<ResolveInfo> QueryIntentActivities(Intent intent, int flags) {
            if(mObject == IntPtr.Zero)
                throw  new NullReferenceException("Native object is not initialized");

            IntPtr apps = ObjectCall("queryIntentActivities", "java/lang/Object",  intent, flags);
            var cl = new Object(apps);
            int count = cl.Call<int>("size");
            
            List<ResolveInfo> res = new List<ResolveInfo>(count);
            
            for (int i = 0; i < count; ++i) {
                //get the object
                var obj = cl.ObjectCall("get", "java/lang/Object", i);
                res.Add(new ResolveInfo(obj));
            }

            return res;
        }

        public PackageInfo GetPackageInfo(string packageName, int flags) {
            return Call<PackageInfo>("getPackageInfo", packageName, flags);
        }

        public string GetApplicationLabel(ApplicationInfo info) {
            return Call<string>("getApplicationLabel", info);
        }
    }
}
#endif