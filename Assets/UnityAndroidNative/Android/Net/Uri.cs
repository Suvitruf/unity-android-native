#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityAndroidNative.Android.Os;
using UnityAndroidNative.Private;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace UnityAndroidNative.Android.Net {

    public class Uri : Object, Parcelable {

        public Uri(IntPtr obj) : base(obj) {
        }

        public JavaObject GetInternalObject() {
            return this;
        }

        /// <summary>
        /// Creates a Uri which parses the given encoded URI string.
        /// </summary>
        /// <param name="uriString">String an RFC 2396-compliant, encoded URI</param>
        /// <returns><see cref="Uri"/> for this given uri string</returns>
        public static Uri Parse(string uriString) {
            return CallStatic<Uri, Uri>("parse", uriString);
        }
    }
}
#endif