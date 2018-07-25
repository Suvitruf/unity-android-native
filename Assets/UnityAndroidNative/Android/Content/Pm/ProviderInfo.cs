using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityAndroidNative.Android.Content.Pm;
using UnityAndroidNative.Android.Os;

namespace UnityAndroidNative.Android.Content.Pm {
    public class ProviderInfo : ComponentInfo {

        /// <summary>
        /// The name provider is published under content:// 
        /// </summary>
        public string Authority {
            get { return Get<string>("authority"); }
            set { Set("authority", value); }
        }

        /// <summary>
        /// Optional permission required for read-only access this content provider.
        /// </summary>
        public string ReadPermission {
            get { return Get<string>("readPermission"); }
            set { Set("readPermission", value); }
        }

        /// <summary>
        /// Optional permission required for read/write access this content provider.
        /// </summary>
        public string WritePermission {
            get { return Get<string>("writePermission"); }
            set { Set("writePermission", value); }
        }

        /// <summary>
        /// If true, additional permissions to specific Uris in this content provider can be granted.
        /// </summary>
        public bool GrantUriPermissions {
            get { return Get<bool>("grantUriPermissions"); }
            set { Set("grantUriPermissions", value); }
        }

        /// <summary>
        /// Bit in <see cref="Flags"/> indicating if the provider is visible to ephemeral applications.
        /// </summary>
        public const int FLAG_VISIBLE_TO_INSTANT_APP = 0x100000;

        /// <summary>
        /// Bit in <see cref="Flags"/>: If set, a single instance of the provider willrun for all users on the device. 
        /// Set from the {@link android.R.attr#singleUser} attribute.
        /// </summary>
        public const int FLAG_SINGLE_USER = 0x40000000;

        /// <summary>
        /// Options that have been set in the provider declaration in the manifest.
        /// These include: <see cref="FLAG_SINGLE_USER"/>
        /// </summary>
        public int Flags {
            get { return Get<int>("flags"); }
            set { Set("flags", value); }
        }

        /// <summary>
        /// Used to control initialization order of single-process providers running in the same process.  Higher goes first.
        /// </summary>
        public int InitOrder {
            get { return Get<int>("initOrder"); }
            set { Set("initOrder", value); }
        }

        /// <summary>
        /// If true, this content provider allows multiple instances of itself 
        /// to run in different process.If false, a single instances is always run in <see cref="ComponentInfo.ProcessName"/>
        /// </summary>
        public bool Multiprocess {
            get { return Get<bool>("multiprocess"); }
            set { Set("multiprocess", value); }
        }

        public ProviderInfo() : base() {
        }

        internal ProviderInfo(IntPtr obj) : base(obj) {
        }

        public ProviderInfo(ProviderInfo orig) : base(orig) {
        }

        public override string ToString() {
            return Call<string>("toString");
        }
    }
}
