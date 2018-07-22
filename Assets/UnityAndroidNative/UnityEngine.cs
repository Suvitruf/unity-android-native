#if UNITY_ANDROID
using System;

namespace UnityAndroidNative {
    public sealed class AndroidJavaException : Exception {
        private readonly string mJavaStackTrace;

        public override string StackTrace {
            get { return mJavaStackTrace + base.StackTrace; }
        }

        public AndroidJavaException(string message, string javaStackTrace)
            : base(message) {
            mJavaStackTrace = javaStackTrace;
        }
    }
}
#endif