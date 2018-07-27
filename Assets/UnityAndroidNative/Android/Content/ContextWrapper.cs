#if UNITY_ANDROID
using System;
using UnityAndroidNative.Android.Content.Pm;
using UnityEngine;
using Application = UnityAndroidNative.Android.App.Application;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace UnityAndroidNative.Android.Content {
    public class ContextWrapper : Context {
        public ContextWrapper(IntPtr obj) : base(obj) {
        }

        public override PackageManager GetPackageManager() {
            return Call<PackageManager>("getPackageManager");
        }

        public override string GetPackageName() {
            return Call<string>("getPackageName");
        }

        public Context GetApplicationContext() {
            return (Context) ObjectCallAutoResolve("getApplicationContext");
        }
    }
}
#endif