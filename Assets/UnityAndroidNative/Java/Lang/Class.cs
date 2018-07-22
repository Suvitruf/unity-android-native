#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityAndroidNative.Private;

namespace UnityAndroidNative.Java.Lang {
    public class Class : JavaObject{
        public Class(IntPtr obj) : base(obj) {
        }

        /// <summary>
        /// Returns the name of the entity (class, interface, array class, primitive type, or void) 
        /// represented by this object, as a <see cref="string"/>.
        /// </summary>
        /// <returns></returns>
        public string GetName() {
            return Call<string>("getName");
        }
    }
}
#endif