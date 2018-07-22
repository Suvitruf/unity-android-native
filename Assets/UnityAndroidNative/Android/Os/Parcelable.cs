#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityAndroidNative.Private;
using UnityEngine;

namespace UnityAndroidNative.Android.Os {
    public interface Parcelable {
        JavaObject GetInternalObject();
    }
}
#endif