#if UNITY_ANDROID
using System;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace UnityAndroidNative.Android.Content.Pm {
    public class ResolveInfo : Object {

        /// <summary>
        /// The activity or broadcast receiver that corresponds to this resolution
        /// match, if this resolution is for an activity or broadcast receiver.
        /// Exactly one of <see cref="ActivityInfo"/>, <see cref="ServiceInfo"/>, or
        /// {@link #providerInfo} will be non-null.
        /// </summary>
        public ActivityInfo ActivityInfo {
            get { return Get<ActivityInfo>("activityInfo"); }
        }

        /// <summary>
        /// The service that corresponds to this resolution match, if this resolution
        /// is for a service. Exactly one of <see cref="ActivityInfo"/>,
        /// <see cref="ServiceInfo"/>, or {@link #providerInfo} will be non-null.
        /// </summary>
        public ServiceInfo ServiceInfo {
            get { return Get<ServiceInfo>("serviceInfo"); }
        }

        public int Icon {
            get { return Get<int>("icon"); }
        }

        internal ResolveInfo(IntPtr obj) : base(obj) {
        }

        public ResolveInfo(ResolveInfo orig) : base(orig) {
        }
        /// <summary>
        /// Retrieve the current textual label associated with this resolution.  
        /// This will call back on the given PackageManager to load the label from the application.
        /// </summary>
        /// <param name="pm">A PackageManager from which the label can be loaded; 
        /// usually the PackageManager from which you originally retrieved this item.</param>
        /// <returns>Returns a CharSequence containing the resolutions's label.  
        /// If the item does not have a label, its name is returned.</returns>
        public string LoadLabel(PackageManager pm) {
            return Call<string>("loadLabel", pm);
        }

        /// <summary>
        /// Return the icon resource identifier to use for this match.If the
        /// match defines an icon, that is used; else if the activity defines
        /// an icon, that is used; else, the application icon is used.
        /// </summary>
        /// <returns>The icon associated with this match.</returns>
        public int GetIconResource() {
            return Call<int>("getIconResource");
        }

        public ComponentInfo GetComponentInfo() {
            return Call<ComponentInfo>("getComponentInfo");
        }

        public override string ToString() {
            return Call<string>("toString");
        }
    }
}
#endif