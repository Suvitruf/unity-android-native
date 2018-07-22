#if UNITY_ANDROID
using System;
using UnityEngine;

namespace UnityAndroidNative.Private {
    internal class JNISafe {
        public static void DeleteGlobalRef(IntPtr globalref) {
            if (!(globalref != IntPtr.Zero))
                return;

            AndroidJNI.DeleteGlobalRef(globalref);
        }

        public static void DeleteLocalRef(IntPtr localRef) {
            if (!(localRef != IntPtr.Zero))
                return;

            AndroidJNI.DeleteLocalRef(localRef);
        }

        public static IntPtr NewObject(IntPtr clazz, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.NewObject(clazz, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static IntPtr FindClass(string name) {
            try {
                return AndroidJNI.FindClass(name);
            }
            finally {
                CheckException();
            }
        }

        public static void CheckException() {
            IntPtr localref = AndroidJNI.ExceptionOccurred();
            if (!(localref != IntPtr.Zero))
                return;
            AndroidJNI.ExceptionClear();
            IntPtr class1 = AndroidJNI.FindClass("java/lang/Throwable");
            IntPtr class2 = AndroidJNI.FindClass("android/util/Log");
            try {
                IntPtr methodId = AndroidJNI.GetMethodID(class1, "toString", "()Ljava/lang/String;");
                IntPtr staticmethodId = AndroidJNI.GetStaticMethodID(class2, "getStackTraceString", "(Ljava/lang/Throwable;)Ljava/lang/String;");
                string message = AndroidJNI.CallStringMethod(localref, methodId, new jvalue[0]);

                jvalue[] args = new jvalue[1];
                args[0].l = localref;

                string javaStackTrace = AndroidJNI.CallStaticStringMethod(class2, staticmethodId, args);
                throw new AndroidJavaException(message, javaStackTrace);
            }
            finally {
                DeleteLocalRef(localref);
                DeleteLocalRef(class1);
                DeleteLocalRef(class2);
            }
        }

        public static IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallStaticObjectMethod(clazz, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static string CallStaticStringMethod(IntPtr clazz, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallStaticStringMethod(clazz, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static char CallStaticCharMethod(IntPtr clazz, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallStaticCharMethod(clazz, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallStaticDoubleMethod(clazz, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static float CallStaticFloatMethod(IntPtr clazz, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallStaticFloatMethod(clazz, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static long CallStaticLongMethod(IntPtr clazz, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallStaticLongMethod(clazz, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static short CallStaticShortMethod(IntPtr clazz, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallStaticShortMethod(clazz, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static byte CallStaticByteMethod(IntPtr clazz, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallStaticByteMethod(clazz, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallStaticBooleanMethod(clazz, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static IntPtr GetObjectClass(IntPtr ptr) {
            try {
                return AndroidJNI.GetObjectClass(ptr);
            }
            finally {
                CheckException();
            }
        }

        public static int CallStaticIntMethod(IntPtr clazz, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallStaticIntMethod(clazz, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static void CallVoidMethod(IntPtr obj, IntPtr methodId, jvalue[] args) {
            try {
                AndroidJNI.CallVoidMethod(obj, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static IntPtr CallObjectMethod(IntPtr obj, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallObjectMethod(obj, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static string CallStringMethod(IntPtr obj, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallStringMethod(obj, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static char CallCharMethod(IntPtr obj, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallCharMethod(obj, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static double CallDoubleMethod(IntPtr obj, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallDoubleMethod(obj, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static float CallFloatMethod(IntPtr obj, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallFloatMethod(obj, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static long CallLongMethod(IntPtr obj, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallLongMethod(obj, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static short CallShortMethod(IntPtr obj, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallShortMethod(obj, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static byte CallByteMethod(IntPtr obj, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallByteMethod(obj, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static bool CallBooleanMethod(IntPtr obj, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallBooleanMethod(obj, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static int CallIntMethod(IntPtr obj, IntPtr methodId, jvalue[] args) {
            try {
                return AndroidJNI.CallIntMethod(obj, methodId, args);
            }
            finally {
                CheckException();
            }
        }

        public static IntPtr GetObjectField(IntPtr obj, IntPtr fieldID) {
            try {
                return AndroidJNI.GetObjectField(obj, fieldID);
            }
            finally {
                CheckException();
            }
        }

        public static string GetStringField(IntPtr obj, IntPtr fieldID) {
            try {
                return AndroidJNI.GetStringField(obj, fieldID);
            }
            finally {
                CheckException();
            }
        }

        public static char GetCharField(IntPtr obj, IntPtr fieldID) {
            try {
                return AndroidJNI.GetCharField(obj, fieldID);
            }
            finally {
                CheckException();
            }
        }

        public static double GetDoubleField(IntPtr obj, IntPtr fieldID) {
            try {
                return AndroidJNI.GetDoubleField(obj, fieldID);
            }
            finally {
                CheckException();
            }
        }

        public static float GetFloatField(IntPtr obj, IntPtr fieldID) {
            try {
                return AndroidJNI.GetFloatField(obj, fieldID);
            }
            finally {
                CheckException();
            }
        }

        public static long GetLongField(IntPtr obj, IntPtr fieldID) {
            try {
                return AndroidJNI.GetLongField(obj, fieldID);
            }
            finally {
                CheckException();
            }
        }

        public static short GetShortField(IntPtr obj, IntPtr fieldID) {
            try {
                return AndroidJNI.GetShortField(obj, fieldID);
            }
            finally {
                CheckException();
            }
        }

        public static byte GetByteField(IntPtr obj, IntPtr fieldID) {
            try {
                return AndroidJNI.GetByteField(obj, fieldID);
            }
            finally {
                CheckException();
            }
        }

        public static bool GetBooleanField(IntPtr obj, IntPtr fieldID) {
            try {
                return AndroidJNI.GetBooleanField(obj, fieldID);
            }
            finally {
                CheckException();
            }
        }

        public static int GetIntField(IntPtr obj, IntPtr fieldID) {
            try {
                return AndroidJNI.GetIntField(obj, fieldID);
            }
            finally {
                CheckException();
            }
        }

        public static IntPtr ToObjectArray(IntPtr[] array, IntPtr type) {
            try {
                return AndroidJNI.ToObjectArray(array, type);
            }
            finally {
                CheckException();
            }
        }
    }
}
#endif