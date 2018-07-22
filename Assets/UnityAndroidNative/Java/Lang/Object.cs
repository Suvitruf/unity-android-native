#if UNITY_ANDROID
using System;
using UnityAndroidNative.Private;

namespace UnityAndroidNative.Java.Lang {
    public class Object : JavaObject {

        private Class mJavaClass;
        public Object(IntPtr obj) : base(obj) {
        }

        public Object(string clsName, params object[] args) : base(clsName, args) {

        }

        public Class GetClass() {
            if (mJavaClass == null)
                mJavaClass = Call<Class>("getClass");

            return mJavaClass;
        }
    }
}
#endif