#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityAndroidNative.Private;
using UnityEngine;
using UnityEngine.Events;

namespace UnityAndroidNative.Android.Content {
    public interface DialogInterface {

    }

    public interface OnClickListener {
        void onClick(AndroidJavaObject dialog, int which);
    }

    public class DialogOnClickListener : JavaProxy, OnClickListener {
        private UnityAction<int> m_CallBack;

        public DialogOnClickListener(UnityAction<int> cb) : base("android.content.DialogInterface$OnClickListener") {
            m_CallBack = cb;
        }

        public void onClick(AndroidJavaObject dialog, int which) {
            m_CallBack(which);
        }
    }
}
#endif