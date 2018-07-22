#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityAndroidNative.Android.Content;
using UnityAndroidNative.Android.Content.Pm;

namespace UnityAndroidNative.Android.View {
    public class ContextThemeWrapper : ContextWrapper {
        public ContextThemeWrapper(IntPtr obj) : base(obj) {
        }
    }
}
#endif