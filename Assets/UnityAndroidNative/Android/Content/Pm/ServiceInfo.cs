#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace UnityAndroidNative.Android.Content.Pm {
    public class ServiceInfo : ComponentInfo {

        /// <summary>
        /// Optional name of a permission required to be able to access this Service.  From the "permission" attribute.
        /// </summary>
        public string Permission {
            get { return Get<string>("permission"); }
        }

        public int Flags {
            get { return Get<int>("flags"); }
        }

        /// <summary>
        /// Bit in <see cref="Flags"/>: If set, the service will automatically be
        /// stopped by the system if the user removes a task that is rooted
        /// in one of the application's activities. 
        /// </summary>
        public const int FLAG_STOP_WITH_TASK = 0x0001;

        /// <summary>
        /// Bit in <see cref="Flags"/>: If set, the service will run in its own isolated process.
        /// </summary>
        public const int FLAG_ISOLATED_PROCESS = 0x0002;

        /// <summary>
        /// Bit in <see cref="Flags"/>: If set, the service can be bound and run in the
        /// calling application's package, rather than the package in which it is
        /// declared.
        /// </summary>
        public const int FLAG_EXTERNAL_SERVICE = 0x0004;

        /// <summary>
        /// Bit in <see cref="Flags"/> indicating if the service is visible to ephemeral applications.
        /// </summary>
        public const int FLAG_VISIBLE_TO_INSTANT_APP = 0x100000;

        /// <summary>
        /// Bit in <see cref="Flags"/>: If set, a single instance of the service will
        /// run for all users on the device.
        /// </summary>
        public const int FLAG_SINGLE_USER = 0x40000000;

        internal ServiceInfo(IntPtr obj) : base(obj) {
        }

        public ServiceInfo() : base() {
        }

        public ServiceInfo(ServiceInfo orig) : base(orig) {
        }

        public override string ToString() {
            return Call<string>("toString");
        }
    }
}
#endif