#if UNITY_ANDROID
using System;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace UnityAndroidNative.Android.Content.Pm {
    public class PackageItemInfo : Object {

        /// <summary>
        /// Name of the package that this item is in.
        /// </summary>
        public string PackageName {
            get { return Get<string>("packageName"); }
            set { Set("packageName", value); }
        }

        /// <summary>
        /// Public name of this item. From the "android:name" attribute.
        /// </summary>
        public string Name {
            get { return Get<string>("name"); }
            set { Set("name", value); }
        }

        /// <summary>
        /// A string resource identifier (in the package's resources) of this
        /// component's label.  From the "label" attribute or, if not set, 0.
        /// </summary>
        public int LabelRes {
            get { return Get<int>("labelRes"); }
            set { Set("labelRes", value); }
        }

        /// <summary>
        /// The string provided in the AndroidManifest file, if any. 
        /// You probably don't want to use this.  You probably want <see cref="PackageManager.GetApplicationLabel"/>
        /// </summary>
        public string NonLocalizedLabel {
            get { return Get<string>("nonLocalizedLabel"); }
            set { Set("nonLocalizedLabel", value); }
        }

        /// <summary>
        /// A drawable resource identifier (in the package's resources) of this
        /// component's icon.  From the "icon" attribute or, if not set, 0.
        /// </summary>
        public int Icon {
            get { return Get<int>("icon"); }
            set { Set("icon", value); }
        }

        /// <summary>
        /// A drawable resource identifier (in the package's resources) of this
        /// component's banner.  From the "banner" attribute or, if not set, 0.
        /// </summary>
        public int Banner {
            get { return Get<int>("banner"); }
            set { Set("banner", value); }
        }

        /// <summary>
        /// A drawable resource identifier (in the package's resources) of this 
        /// component's logo. Logos may be larger/wider than icons and are
        /// displayed by certain UI elements in place of a name or name/icon
        /// combination.From the "logo" attribute or, if not set, 0.
        /// </summary>
        public int Logo {
            get { return Get<int>("logo"); }
            set { Set("logo", value); }
        }

        public PackageItemInfo(PackageItemInfo orig) : base(orig) {
        }

        internal PackageItemInfo(IntPtr obj) : base(obj) {
        }

        public PackageItemInfo() : base() {
        }
    }
}

#endif