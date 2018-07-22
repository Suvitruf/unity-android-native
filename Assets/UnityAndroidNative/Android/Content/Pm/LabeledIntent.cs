#if UNITY_ANDROID

namespace UnityAndroidNative.Android.Content.Pm {
    /// <summary>
    /// A special subclass of Intent that can have a custom label/icon associated with it.Primarily for use with <see cref="Intent#ACTION_CHOOSER"/>.
    /// </summary>
    public class LabeledIntent : Intent {

        /// <summary>
        /// Create a labeled intent from the given intent, supplying a textual label and icon resource for it.
        /// </summary>
        /// <param name="origIntent">The original Intent to copy.</param>
        /// <param name="sourcePackage">The package in which the label and icon live.</param>
        /// <param name="labelRes">Concrete text to use for the label.</param>
        /// <param name="icon">Resource containing the icon, or 0 if none.</param>
        public LabeledIntent(Intent origIntent, string sourcePackage, int labelRes, int icon) : base(origIntent, sourcePackage, labelRes, icon) {
        }

        /// <summary>
        /// Create a labeled intent from the given intent, supplying a textual label and icon resource for it.
        /// </summary>
        /// <param name="origIntent">The original Intent to copy.</param>
        /// <param name="sourcePackage">The package in which the label and icon live.</param>
        /// <param name="nonLocalizedLabel">Concrete text to use for the label.</param>
        /// <param name="icon">Resource containing the icon, or 0 if none.</param>
        public LabeledIntent(Intent origIntent, string sourcePackage, string nonLocalizedLabel, int icon) : base(origIntent, sourcePackage, nonLocalizedLabel, icon) {
        }

        /// <summary>
        /// Create a labeled intent with no intent data but supplying the label and icon resources for it.
        /// </summary>
        /// <param name="sourcePackage">The package in which the label and icon live.</param>
        /// <param name="nonLocalizedLabel">nonLocalizedLabel Concrete text to use for the label.</param>
        /// <param name="icon">Resource containing the icon, or 0 if none.</param>
        public LabeledIntent(string sourcePackage, string nonLocalizedLabel, int icon) : base(sourcePackage, nonLocalizedLabel, icon) {
        }

        /// <summary>
        /// Create a labeled intent with no intent data but supplying the label and icon resources for it.
        /// </summary>
        /// <param name="sourcePackage">The package in which the label and icon live.</param>
        /// <param name="labelRes">Resource containing the label, or 0 if none.</param>
        /// <param name="icon">Resource containing the icon, or 0 if none.</param>  
        public LabeledIntent(string sourcePackage, int labelRes, int icon) : base(sourcePackage, labelRes, icon) {
        }


        /// <summary>
        /// Return the name of the package holding label and icon resources.
        /// </summary>
        /// <returns></returns>
        public string GetSourcePackage() {
            return Call<string>("getSourcePackage");
        }

        /// <summary>
        /// Return any resource identifier that has been given for the label text.
        /// </summary>
        /// <returns></returns>
        public int GetLabelResource() {
            return Call<int>("getLabelResource");
        }

        /// <summary>
        /// Return any concrete text that has been given for the label text.
        /// </summary>
        /// <returns></returns>
        public string GetNonLocalizedLabel() {
            return Call<string>("getNonLocalizedLabel");
        }

        /// <summary>
        /// Return any resource identifier that has been given for the label icon.
        /// </summary>
        /// <returns></returns>
        public int GetIconResource() {
            return Call<int>("getIconResource");
        }

        /// <summary>
        /// Retrieve the label associated with this object.  If the object does
        //  not have a label, null will be returned, in which case you will probably
        /// want to load the label from the underlying resolved info for the Intent.
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public string LoadLabel(PackageManager pm) {
            return Call<string>("loadLabel", pm);
        }

        /// <summary>
        /// Retrieve the icon associated with this object.  If the object does
        /// not have a icon, null will be returned, in which case you will probably
        /// want to load the icon from the underlying resolved info for the Intent.
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public string LoadIcon(PackageManager pm) {
            return Call<string>("loadIcon", pm);
        }
    }
}
#endif