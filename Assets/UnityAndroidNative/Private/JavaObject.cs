#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityAndroidNative.Android.App;
using UnityAndroidNative.Java.Lang;
using UnityEngine;
using Object = UnityAndroidNative.Java.Lang.Object;

namespace UnityAndroidNative.Private {
    public abstract class JavaObject : IDisposable {
        internal static bool mDebug = false;

        internal IntPtr mObject;
        internal IntPtr mClass;
//        internal string mJavaClassName = null;

        private bool mDisposed = false;
        private static IntPtr mJavaLangClass;

        protected static void DebugPrint(string msg) {
            if (!mDebug)
                return;

            Debug.LogFormat(msg);
        }

        private static readonly Dictionary<Type, string> mClassNames = new Dictionary<Type, string>();

        //        protected virtual string GetClass() {
        //            if (mJavaClassName == null) {
        //                mJavaClassName = GetType().FullName;
        //                System.Diagnostics.Debug.Assert(mJavaClassName != null, "mClassName != null");
        //                mJavaClassName = mJavaClassName.Substring(mJavaClassName.IndexOf('.') + 1).ToLower();
        //                var last = mJavaClassName.LastIndexOf('.');
        //                mJavaClassName = mJavaClassName.Substring(0, last) + mJavaClassName[last + 1] + mJavaClassName.Substring(last + 2);
        //            }
        //            Debug.LogWarning("mJavaClassName: " + mJavaClassName);
        //            return mJavaClassName;
        //        }

        /// <summary>
        /// <para>
        /// Get java native class name for C# class that represent Java native class
        /// </para>
        /// 
        /// <para>
        /// NOTE: be sure you passing correct type (it shoud be the type derived from <see cref="JavaObject"/>)!
        /// </para>
        /// 
        /// </summary>
        /// <param name="type">C# representation of Java class</param>
        /// <returns>Java class full name (e.g. for <see cref="Activity"/> it returns android/app/Activity</returns>
        protected static string GetClass(Type type) {
            string clsName;

            if (mClassNames.TryGetValue(type, out clsName))
                return clsName;

            // replace + for nested class
            var temp = type.FullName.Replace('+', '$');
            System.Diagnostics.Debug.Assert(temp != null, "class name != null");
            var start = temp.IndexOf(Utils.GetProjectPrefix(), StringComparison.Ordinal) + Utils.GetProjectPrefix().Length + 1;
            var last = temp.LastIndexOf('.');

            clsName = temp.Substring(start, last - start).ToLower() + "." + temp.Substring(last + 1);
            mClassNames[type] = clsName;

            return clsName;
        }

        /// <summary>
        /// <para>
        /// Retrieves the raw pointer to the Java object.
        /// </para>
        /// </summary>
        public IntPtr RawObject {
            get { return mObject; }
        }

        /// <summary>
        /// <para>
        /// Retrieves the raw pointer to the Java class.
        /// </para>
        /// </summary>
        public virtual IntPtr RawClass {
            get {
                if (mClass == IntPtr.Zero) {
                    mClass = AndroidJNI.GetObjectClass(RawObject);
                }
                return mClass;
            }
        }

        /// <summary>
        /// Constructs the argument array for calling a JNI method.
        /// </summary>
        /// <returns>The java native argument array.</returns>
        /// <param name="args">Arguments.</param>
        protected static jvalue[] ConstructArgArray(object[] args) {
            object[] a = new object[args.Length];
            for (int i = 0; i < args.Length; i++) {
                var javaObject = args[i] as JavaObject;
                if (javaObject != null) {
                    a[i] = javaObject.RawObject;
                }
                else {
                    a[i] = args[i];
                }
            }

            jvalue[] jArgs = AndroidJNIHelper.CreateJNIArgArray(a);

            for (int i = 0; i < args.Length; i++) {
                var javaObject = args[i] as JavaObject;
                if (javaObject != null) {
                    jArgs[i].l = javaObject.RawObject;
                }
            }

            return jArgs;
        }

        public static IntPtr ConvertToJNIArray<TElement>(Array array) {
            System.Type elementType = typeof(TElement);

            System.Diagnostics.Debug.Assert(elementType != null, "elementType != null");
            if (elementType.IsSubclassOf(typeof(JavaObject))) {
                int length1 = array.GetLength(0);
                IntPtr[] array2 = new IntPtr[length1];
                IntPtr class1 = FindClass(GetClass(elementType));
                IntPtr type = IntPtr.Zero;
                for (int index = 0; index < length1; ++index) {
                    JavaObject obj = array.GetValue(index) as JavaObject;
                    if (obj != null) {
                        array2[index] = obj.RawObject;
                        IntPtr rawClass = obj.RawClass;
                        if (type != rawClass)
                            type = !(type == IntPtr.Zero) ? class1 : rawClass;
                    }
                    else
                        array2[index] = IntPtr.Zero;
                }
                IntPtr num = JNISafe.ToObjectArray(array2, type);
                JNISafe.DeleteLocalRef(class1);

                return num;
            }

            return AndroidJNIHelper.ConvertToJNIArray(array);
        }

        ~JavaObject() {
            Dispose();
        }

        public void Dispose() {
            if (mDisposed)
                return;

            mDisposed = true;

            if (mObject != IntPtr.Zero) {
                JNISafe.DeleteGlobalRef(mObject);
            }

            if (mClass != IntPtr.Zero) {
                JNISafe.DeleteGlobalRef(mClass);
            }
        }

        internal static IntPtr CreateGlobalRef(IntPtr jobject) {
            return !(jobject == IntPtr.Zero) ? AndroidJNI.NewGlobalRef(jobject) : IntPtr.Zero;
        }

        internal JavaObject() {
        }

        internal JavaObject(IntPtr jobject) : this() {
            if (jobject == IntPtr.Zero)
                throw new Exception("JavaObject: have tried to init JavaObject with null ptr!");

            IntPtr objectClass = JNISafe.GetObjectClass(jobject);
            mObject = CreateGlobalRef(jobject);
            mClass = CreateGlobalRef(objectClass);

            JNISafe.DeleteLocalRef(objectClass);
        }

        internal JavaObject(params object[] args) {
            if (args == null)
                args = new object[0];
            mClass = CreateGlobalRef(FindClass(GetClass(GetType())));
            jvalue[] jniArgArray = ConstructArgArray(args);
            try {
                IntPtr num = JNISafe.NewObject(mClass, AndroidJNIHelper.GetConstructorID(mClass, args), jniArgArray);
                mObject = CreateGlobalRef(num);
                JNISafe.DeleteLocalRef(num);
            }
            finally {
                AndroidJNIHelper.DeleteJNIArgArray(args, jniArgArray);
            }
        }

        public static string GetSignature<TReturnType>(object[] args) {
            StringBuilder stringBuilder = new StringBuilder();
            if (args != null) {
                stringBuilder.Append('(');
                foreach (object obj in args) {
                    stringBuilder.Append(GetItemSignature(obj));
                }
                stringBuilder.Append(')');
            }

            var t = typeof (TReturnType);

            if (t.IsSubclassOf(typeof (JavaObject))) {
                // TODO: [optimization]
                using (JavaObject androidJavaObject = new Object(FindClass(GetClass(t))))
                    stringBuilder.Append("L" + androidJavaObject.Call<string>("getName") + ";");
            }
            else
                stringBuilder.Append(AndroidJNIHelper.GetSignature(t));

            string sig = stringBuilder.ToString();

            if (mDebug)
                DebugPrint("GetSignature<" + t.FullName + ">" + ": " + sig);

            return sig;
        }

        public static string GetSignature<T>() {
            return GetSignature<T>(null);
        }

        public static string GetItemSignature(object obj) {
            var o = obj as Java.Lang.Object;
            if (o != null) {
//                using (var clsObject = o.Call<Class>("getClass"))
                    return "L" + o.GetClass().GetName() + ";";
            }
            return AndroidJNIHelper.GetSignature(obj);
        }

        public static string GetSignature(string returnType, object[] args) {
            if(args == null)
                args = new object[0];

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append('(');
            foreach (object obj in args) {
                stringBuilder.Append(GetItemSignature(obj));
            }

            if (!string.IsNullOrEmpty(returnType))
                stringBuilder.Append(")L" + returnType).Append(";");
            else
                stringBuilder.Append(")V");

            var sign = stringBuilder.ToString();

            if (mDebug)
                DebugPrint("GetSignature: " + sign);

            return sign;
        }

        protected static IntPtr FindClass(string name) {
            //            IntPtr objCls = AndroidJNI.FindClass("java/lang/Class");
            //
            //            var sign = GetSignature(new object[] {name.Replace('/', '.')}, "java/lang/Object;");
            //            Debug.LogWarning("sign: " + sign);
            //            IntPtr methodId = AndroidJNIHelper.GetMethodID(objCls, "forName", sign, true);
            //            Debug.LogWarning("sign2: " + sign);
            //            IntPtr jObject = JNISafe.CallStaticObjectMethod(objCls, methodId, ConstructArgArray(new object[] { name.Replace('/', '.') }));

            return ObjectCallStatic(JavaLangClass, "forName", "java/lang/Object", name.Replace('/', '.'));
        }

        protected static IntPtr JavaLangClass {
            get {
                if (mJavaLangClass == IntPtr.Zero)
                    mJavaLangClass = AndroidJNI.FindClass("java/lang/Class");

                return mJavaLangClass;
            }
        }

        /// <summary>
        /// Calls a non-static method with a void return type.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="args">Arguments.</param>
        public void CallVoid(string name, params object[] args) {
            string sig = GetSignature(null, args);
            IntPtr method = AndroidJNIHelper.GetMethodID(RawClass, name, sig, false);

            jvalue[] jArgs = ConstructArgArray(args);
            try {             
                JNISafe.CallVoidMethod(mObject, method, jArgs);
            }
            finally {
                AndroidJNIHelper.DeleteJNIArgArray(args, jArgs);
            }
        }

//        public TReturnType ObjectCall<TReturnType>(string name, params object[] args) {
//            Debug.LogWarning("ObjectCall1: " + name);
//            string sig = GetSignature<TReturnType>(args);
//            Debug.LogWarning("ObjectCall2: " + name + " => " + sig);
//            IntPtr methodId = AndroidJNIHelper.GetMethodID(RawClass, name, sig, false);
//
//            jvalue[] jArgs = ConstructArgArray(args);
//            try {
//                IntPtr val = JNISafe.CallObjectMethod(RawObject, methodId, jArgs);
//
//                if (val.Equals(IntPtr.Zero)) {
//                    return default(TReturnType);
//                }
//
//                var t = typeof(TReturnType);
//                if (t.IsSubclassOf(typeof(JavaObject))) {
//                    ConstructorInfo c = t.GetConstructor(new[] { val.GetType() });
//                    if (c != null) {
//                        return (TReturnType)c.Invoke(new object[] { val });
//                    }
//                }
//            }
//            finally {
//                AndroidJNIHelper.DeleteJNIArgArray(args, jArgs);
//            }            
//
//            return default(TReturnType);
//        }

        public IntPtr ObjectCall(string name, string className, params object[] args) {
            string sig = GetSignature(className, args);
            IntPtr methodId = AndroidJNIHelper.GetMethodID(RawClass, name, sig, false);

            jvalue[] jArgs = ConstructArgArray(args);
            try {
                IntPtr val = JNISafe.CallObjectMethod(RawObject, methodId, jArgs);

                if (val.Equals(IntPtr.Zero)) {
                    return IntPtr.Zero;
                }

                return val;
            }
            finally {
                AndroidJNIHelper.DeleteJNIArgArray(args, jArgs);
            }
        }

        /// <summary>
        /// Calls a class method.
        /// 
        /// <para>
        /// NOTE: be sure you passing correct type (it shoud be the primitive or the type derived from <see cref="JavaObject"/>)!
        /// </para>
        /// 
        /// </summary>
        /// <returns>The invoke call.</returns>
        /// <param name="name">Method name.</param>
        /// <param name="args">Arguments.</param>
        /// <typeparam name="TReturnType"></typeparam>
        public TReturnType Call<TReturnType>(string name, params object[] args) {
            Type t = typeof(TReturnType);
            string sig = GetSignature<TReturnType>(args);
            IntPtr methodId = AndroidJNIHelper.GetMethodID(RawClass, name, sig, false);
            jvalue[] jArgs = ConstructArgArray(args);
            try {
                if (methodId == IntPtr.Zero) {
                    Debug.LogError("Cannot get method for " + name);

                    throw new Exception("Cannot get method for " + name);
                }
                if (AndroidReflection.IsPrimitive(t)) {
                    if (t == typeof(bool)) {
                        return (TReturnType)(object)JNISafe.CallBooleanMethod(RawObject, methodId, jArgs);
                    }
                    if (t == typeof(int)) {
                        return (TReturnType)(object)JNISafe.CallIntMethod(RawObject, methodId, jArgs);
                    }
                    if (t == typeof(float)) {
                        return (TReturnType)(object)JNISafe.CallFloatMethod(RawObject, methodId, jArgs);
                    }
                    if (t == typeof(double)) {
                        return (TReturnType)(object)JNISafe.CallDoubleMethod(RawObject, methodId, jArgs);
                    }
                    if (t == typeof(byte)) {
                        return (TReturnType)(object)JNISafe.CallByteMethod(RawObject, methodId, jArgs);
                    }
                    if (t == typeof(char)) {
                        return (TReturnType)(object)JNISafe.CallCharMethod(RawObject, methodId, jArgs);
                    }
                    if (t == typeof(long)) {
                        return (TReturnType)(object)JNISafe.CallLongMethod(RawObject, methodId, jArgs);
                    }
                    if (t == typeof(short)) {
                        return (TReturnType)(object)JNISafe.CallShortMethod(RawObject, methodId, jArgs);
                    }
                }

                if (t == typeof(string)) {
                    return (TReturnType)(object)JNISafe.CallStringMethod(RawObject, methodId, jArgs);
                }

                if (t.IsSubclassOf(typeof(JavaObject))) {
                    IntPtr val = JNISafe.CallObjectMethod(RawObject, methodId, jArgs);

                    if (val == IntPtr.Zero)
                        return default(TReturnType);

                    ConstructorInfo c = t.GetConstructor(new[] { val.GetType() });

                    if (c != null) {
                        return (TReturnType)c.Invoke(new object[] { val });
                    }
                }
            }
            finally {
                AndroidJNIHelper.DeleteJNIArgArray(args, jArgs);
            }

            return default(TReturnType);
        }

        public static TReturnType CallStatic<TReturnType, TClass>(string name, params object[] args) {
            return CallStatic<TReturnType>(GetClass(typeof(TClass)), name, args);
        }

        /// <summary>
        /// Calls a class static method.
        /// 
        /// <para>
        /// NOTE: be sure you passing correct type (it shoud be the primitive or the type derived from <see cref="JavaObject"/>)!
        /// </para>
        /// 
        /// </summary>
        /// <returns>The invoke call.</returns>
        /// <param name="type">Type.</param>
        /// <param name="name">Method name.</param>
        /// <param name="args">Arguments.</param>
        /// <typeparam name="TReturnType"></typeparam>
        public static TReturnType CallStatic<TReturnType>(string type, string name, params object[] args) {
            Type t = typeof (TReturnType);
            string sig = GetSignature<TReturnType>(args);
            IntPtr rawClass = FindClass(type);
            IntPtr method = AndroidJNIHelper.GetMethodID(rawClass, name, sig, true);
            jvalue[] jArgs = ConstructArgArray(args);
            try {
                if (AndroidReflection.IsPrimitive(t)) {
                    if (t == typeof(bool)) {
                        return (TReturnType)(object)JNISafe.CallStaticBooleanMethod(rawClass, method, jArgs);
                    }
                    if (t == typeof(int)) {
                        return (TReturnType)(object)JNISafe.CallStaticIntMethod(rawClass, method, jArgs);
                    }
                    if (t == typeof(float)) {
                        return (TReturnType)(object)JNISafe.CallStaticFloatMethod(rawClass, method, jArgs);
                    }
                    if (t == typeof(double)) {
                        return (TReturnType)(object)JNISafe.CallStaticDoubleMethod(rawClass, method, jArgs);
                    }
                    if (t == typeof(byte)) {
                        return (TReturnType)(object)JNISafe.CallStaticByteMethod(rawClass, method, jArgs);
                    }
                    if (t == typeof(char)) {
                        return (TReturnType)(object)JNISafe.CallStaticCharMethod(rawClass, method, jArgs);
                    }
                    if (t == typeof(long)) {
                        return (TReturnType)(object)JNISafe.CallStaticLongMethod(rawClass, method, jArgs);
                    }
                    if (t == typeof(short)) {
                        return (TReturnType)(object)JNISafe.CallStaticShortMethod(rawClass, method, jArgs);
                    }
                }

                if (t == typeof(string)) {
                    return (TReturnType)(object)JNISafe.CallStaticStringMethod(rawClass, method, jArgs);
                }

                if (t.IsSubclassOf(typeof (JavaObject))) {
                    IntPtr val = JNISafe.CallStaticObjectMethod(rawClass, method, jArgs);

                    if (val == IntPtr.Zero)
                        return default(TReturnType);

                    ConstructorInfo c = t.GetConstructor(new[] {val.GetType()});

                    if (c != null) {
                        return (TReturnType) c.Invoke(new object[] {val});
                    }
                }
            }
            finally {
                AndroidJNIHelper.DeleteJNIArgArray(args, jArgs);
            }

            return default(TReturnType);
        }

        public static IntPtr ObjectCallStatic(IntPtr type, string name, string className, params object[] args) {
            string sig = GetSignature(className, args);

            IntPtr method = AndroidJNIHelper.GetMethodID(type, name, sig, true);
            jvalue[] jArgs = ConstructArgArray(args);
            try {
                IntPtr val = JNISafe.CallStaticObjectMethod(type, method, jArgs);

                return val;
            }
            finally {
                AndroidJNIHelper.DeleteJNIArgArray(args, jArgs);
            }
        }

        public static TFieldType GetStatic<TFieldType, TClass>(string name) {
            return GetStatic<TFieldType>(typeof (TClass).FullName, name);
        }

        /// <summary>
        /// Gets the value of a class static field.
        /// </summary>
        /// <returns>The static object field.</returns>
        /// <param name="clsName">Class name (e.g. "com/unity3d/player/UnityPlayer").</param>
        /// <param name="name">Field name.</param>
        public static TFieldType GetStatic<TFieldType>(string clsName, string name) {
            IntPtr rawClass = FindClass(clsName);

            IntPtr fieldId = AndroidJNIHelper.GetFieldID(rawClass, name, GetSignature<TFieldType>(), true);

            var t = typeof(TFieldType);

            if (AndroidReflection.IsPrimitive(t)) {
                if (typeof (TFieldType) == typeof (int))
                    return (TFieldType) (object) AndroidJNI.GetStaticIntField(rawClass, fieldId);
                if (typeof (TFieldType) == typeof (bool))
                    return (TFieldType) (object) AndroidJNI.GetStaticBooleanField(rawClass, fieldId);
                if (typeof (TFieldType) == typeof (byte))
                    return (TFieldType) (object) AndroidJNI.GetStaticByteField(rawClass, fieldId);
                if (typeof (TFieldType) == typeof (short))
                    return (TFieldType) (object) AndroidJNI.GetStaticShortField(rawClass, fieldId);
                if (typeof (TFieldType) == typeof (long))
                    return (TFieldType) (object) AndroidJNI.GetStaticLongField(rawClass, fieldId);
                if (typeof (TFieldType) == typeof (float))
                    return (TFieldType) (object) AndroidJNI.GetStaticFloatField(rawClass, fieldId);
                if (typeof (TFieldType) == typeof (double))
                    return (TFieldType) (object) AndroidJNI.GetStaticDoubleField(rawClass, fieldId);
                if (typeof (TFieldType) == typeof (char))
                    return (TFieldType) (object) AndroidJNI.GetStaticCharField(rawClass, fieldId);
                return default(TFieldType);
            }

            if (t == typeof (string))
                return (TFieldType) (object)AndroidJNI.GetStaticStringField(rawClass, fieldId);

            if (t.IsSubclassOf(typeof (JavaObject))) {
                IntPtr val = AndroidJNI.GetStaticObjectField(rawClass, fieldId);

                if (val == IntPtr.Zero)
                    return default(TFieldType);

                ConstructorInfo c = t.GetConstructor(new[] {val.GetType()});

                if (c != null) {
                    return (TFieldType) c.Invoke(new object[] {val});
                }
            }

            return default(TFieldType);
        }

        /// <summary>
        /// Gets the value of an object field.
        /// 
        /// <para>
        /// NOTE: be sure you passing correct type (it shoud be the primitive or the type derived from <see cref="JavaObject"/>)!
        /// </para>
        /// 
        /// </summary>
        /// <returns>The static object field.</returns>
        /// <param name="name">Field name.</param>
        /// <typeparam name="TFieldType"></typeparam>
        public TFieldType Get<TFieldType>(string name) {
            IntPtr fieldId = AndroidJNIHelper.GetFieldID(mClass, name, GetSignature<TFieldType>(), false);

            var t = typeof (TFieldType);

            if (AndroidReflection.IsPrimitive(t)) {
                if (typeof (TFieldType) == typeof (int))
                    return (TFieldType) (object) JNISafe.GetIntField(mObject, fieldId);
                if (typeof (TFieldType) == typeof (bool))
                    return (TFieldType) (object) JNISafe.GetBooleanField(mObject, fieldId);
                if (typeof (TFieldType) == typeof (byte))
                    return (TFieldType) (object) JNISafe.GetByteField(mObject, fieldId);
                if (typeof (TFieldType) == typeof (short))
                    return (TFieldType) (object) JNISafe.GetShortField(mObject, fieldId);
                if (typeof (TFieldType) == typeof (long))
                    return (TFieldType) (object) JNISafe.GetLongField(mObject, fieldId);
                if (typeof (TFieldType) == typeof (float))
                    return (TFieldType) (object) JNISafe.GetFloatField(mObject, fieldId);
                if (typeof (TFieldType) == typeof (double))
                    return (TFieldType) (object) JNISafe.GetDoubleField(mObject, fieldId);
                if (typeof (TFieldType) == typeof (char))
                    return (TFieldType) (object) JNISafe.GetCharField(mObject, fieldId);
                return default(TFieldType);
            }

            if (t == typeof (string))
                return (TFieldType) (object) JNISafe.GetStringField(mObject, fieldId);

            if (t.IsSubclassOf(typeof (JavaObject))) {
                IntPtr val = JNISafe.GetObjectField(mObject, fieldId);

                if (val == IntPtr.Zero)
                    return default(TFieldType);

                ConstructorInfo c = t.GetConstructor(new[] {val.GetType()});
                if (c != null) {
                    return (TFieldType) c.Invoke(new object[] {val});
                }
            }

            return default(TFieldType);
        }
    }
}
#endif