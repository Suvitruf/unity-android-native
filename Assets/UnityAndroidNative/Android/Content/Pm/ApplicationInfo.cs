using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityAndroidNative.Android.Content.Pm;

namespace UnityAndroidNative.Android.Content.Pm {
    public class ApplicationInfo : PackageItemInfo {


        internal ApplicationInfo(IntPtr obj) : base(obj) {
        }

        public ApplicationInfo(ApplicationInfo orig) : base(orig) {
        }
    }
}
