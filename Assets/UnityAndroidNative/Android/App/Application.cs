using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityAndroidNative.Android.Content;

namespace UnityAndroidNative.Android.App {
    public class Application : ContextWrapper {
        public Application(IntPtr obj) : base(obj) {
        }
    }
}
