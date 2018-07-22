#if UNITY_ANDROID
namespace UnityAndroidNative.Android.Content.Pm {
    public class LabeledIntent : Intent {

        public LabeledIntent(Intent origIntent, string sourcePackage, int labelRes, int icon) : base(origIntent, sourcePackage, labelRes, icon) {
        }

        public LabeledIntent(Intent origIntent, string sourcePackage, string nonLocalizedLabel, int icon) : base(origIntent, sourcePackage, nonLocalizedLabel, icon) {
        }
    }
}
#endif