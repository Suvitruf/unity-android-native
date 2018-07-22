#if UNITY_ANDROID
using System;
using UnityAndroidNative.Android.Content.Pm;
using Object = UnityAndroidNative.Java.Lang.Object;


namespace UnityAndroidNative.Android.Content {
    public abstract class Context : Object {

        public Context(IntPtr obj) : base(obj) {
        }

        /// <summary>
        /// Return PackageManager instance to find global package information
        /// </summary>
        /// <returns></returns>
        public abstract PackageManager GetPackageManager();

        /// <summary>
        /// Return the name of this application's package.
        /// </summary>
        /// <returns></returns>
        public abstract string GetPackageName();
    }
}

#endif