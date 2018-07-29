#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityAndroidNative.Java.Lang;
using UnityEngine;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace UnityAndroidNative.Private {
    public class JavaProxy : AndroidJavaProxy {

        public JavaProxy(string javaInterface) : base(javaInterface) {
        }
    }
}

#endif