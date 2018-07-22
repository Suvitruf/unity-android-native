using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityAndroidNative.Private;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace UnityAndroidNative.Android.Os {
    public class Build : Object {
        public Build(IntPtr obj) : base(obj) {
        }

        public static class VERSION_CODES {
        }

        public static class VERSION {

            /// <summary>
            /// The user-visible SDK version of the framework; its possible values are defined in <see cref="Build.VERSION_CODES"/>.
            /// </summary>
            public static int SDK_INT {
                get { return GetStatic<int>(typeof (VERSION), "SDK_INT"); }
            }

            /// <summary>
            /// The current development codename, or the string "REL" if this is a release build.
            /// </summary>
            public static string CODENAME {
                get { return GetStatic<string>(typeof (VERSION), "CODENAME"); }
            }
        }
    }
}