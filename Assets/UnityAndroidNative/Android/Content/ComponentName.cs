#if UNITY_ANDROID
using System;
using JetBrains.Annotations;
using UnityAndroidNative.Android.Os;
using UnityAndroidNative.Private;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace UnityAndroidNative.Android.Content {
    public class ComponentName : Object, Parcelable {

        public ComponentName(IntPtr obj) :base(obj) {
        }

        /// <summary>
        /// Create a new component identifier.
        /// </summary>
        /// <param name="pkg">The name of the package that the component exists in.  Can</param>
        /// <param name="cls">The name of the class inside of <var>pkg</var> that implements the component.  Can not be null.</param>
        public ComponentName([NotNull] string pkg, [NotNull] string cls) : base(pkg, cls){
            if (pkg == null)
                throw new NullReferenceException("Package name is null");
            if (cls == null)
                throw new NullReferenceException("Class name is null");
        }

        public JavaObject GetInternalObject() {
            return this;
        }
    }
}
#endif