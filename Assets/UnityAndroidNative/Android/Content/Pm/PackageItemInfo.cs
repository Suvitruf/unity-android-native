#if UNITY_ANDROID
using System;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace UnityAndroidNative.Android.Content.Pm {
    public class PackageItemInfo : Object {

        public string PackageName {
            get { return Get<string>("packageName"); }
            set { Set("packageName", value); }
        }

        public string Name {
            get { return Get<string>("name"); }
            set { Set("name", value); }
        }

        public int LabelRes {
            get { return Get<int>("labelRes"); }
            set { Set("labelRes", value); }
        }

        public string NonLocalizedLabel {
            get { return Get<string>("nonLocalizedLabel"); }
            set { Set("nonLocalizedLabel", value); }
        }

        public int Icon {
            get { return Get<int>("icon"); }
            set { Set("icon", value); }
        }

        public int Banner {
            get { return Get<int>("banner"); }
            set { Set("banner", value); }
        }

        public int Logo {
            get { return Get<int>("logo"); }
            set { Set("logo", value); }
        }

        internal PackageItemInfo(IntPtr obj) : base(obj) {
        }
    }
}

#endif