#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityAndroidNative.Android.Content.Pm;
using UnityAndroidNative.Android.Os;
using UnityAndroidNative.Private;
using UnityEngine;
using Object = UnityAndroidNative.Java.Lang.Object;
using Uri = UnityAndroidNative.Android.Net.Uri;

namespace UnityAndroidNative.Android.Content {
    public class Intent : Object, Parcelable {
        private const string IntentClassFullName = "android/content/Intent";

        /// <summary>
        /// Activity Action: Display an activity chooser, allowing the user to pick
        /// what they want to before proceeding.  This can be used as an alternative
        /// to the standard activity picker that is displayed by the system when
        /// you try to start an activity with multiple possible matches, with these
        /// differences in behavior:
        /// <list type="bullet">
        /// <item><description>You can specify the title that will appear in the activity chooser.</description></item>
        /// <item><description>The user does not have the option to make one of the matching
        /// activities a preferred activity, and all possible activities will
        /// always be shown even if one of them is currently marked as the
        /// preferred activity.
        /// </description>
        /// </item>
        /// </list>
        /// <para>
        /// This action should be used when the user will naturally expect to
        /// select an activity in order to proceed.  An example if when not to use
        /// it is when the user clicks on a "mailto:" link.  They would naturally
        /// expect to go directly to their mail app, so startActivity() should be
        /// called directly: it will
        /// either launch the current preferred app, or put up a dialog allowing the
        /// user to pick an app to use and optionally marking that as preferred.
        /// </para>
        /// <para>
        /// In contrast, if the user is selecting a menu item to send a picture
        /// they are viewing to someone else, there are many different things they
        /// may want to do at this point: send it through e-mail, upload it to a
        /// web service, etc.  In this case the CHOOSER action should be used, to
        /// always present to the user a list of the things they can do, with a
        /// nice title given by the caller such as "Send this photo with:".
        /// </para>
        /// <para>
        /// If you need to grant URI permissions through a chooser, you must specify
        /// the permissions to be granted on the <see cref="ACTION_CHOOSER"/> Intent
        /// <em>in addition</em> to the EXTRA_INTENT inside.  This means using
        /// {@link #setClipData} to specify the URIs to be granted as well as
        /// {@link #FLAG_GRANT_READ_URI_PERMISSION} and/or
        /// {@link #FLAG_GRANT_WRITE_URI_PERMISSION} as appropriate.
        /// </para>
        /// <para>
        /// As a convenience, an Intent of this form can be created with the
        /// <see cref="CreateChooser"/> function.
        /// </para>
        /// <para>
        /// Input: No data should be specified.  get///Extra must have
        /// a <see cref="EXTRA_INTENT"/> field containing the Intent being executed,
        /// and can optionally have a <see cref="EXTRA_TITLE"/> field containing the
        /// title text to display in the chooser.
        /// </para>
        /// <para>
        /// Output: Depends on the protocol of {@link #EXTRA_INTENT}.
        /// </para>
        /// </summary>
        public const string ACTION_CHOOSER = "android.intent.action.CHOOSER";

        /// <summary>
        /// Activity Action: Display the data to the user.  This is the most common
        /// action performed on data -- it is the generic action you can use on
        /// a piece of data to get the most reasonable thing to occur.  For example,
        /// when used on a contacts entry it will view the entry; when used on a
        /// mailto: URI it will bring up a compose window filled with the information
        /// supplied by the URI; when used with a tel: URI it will invoke the
        /// dialer.
        /// <para>Input: <see cref="GetData"/>} is URI from which to retrieve data.</para>
        /// <para>Output: nothing.</para>
        /// </summary>
        public const string ACTION_VIEW = "android.intent.action.VIEW";

        public const string ACTION_SEND = "android.intent.action.SEND";

        /// <summary>
        /// Activity Action: Pick an activity given an intent, returning the class selected.
        /// <para>
        /// Input: get Extra field <see cref="EXTRA_INTENT"/> is an <see cref="Intent"/>
        /// used with <see cref="PackageManager.QueryIntentActivities"/> to determine the
        /// set of activities from which to pick.
        /// </para>
        /// <para>Output: Class name of the activity that was selected.</para>
        /// </summary>
        public const string ACTION_PICK_ACTIVITY = "android.intent.action.PICK_ACTIVITY";

        public const string EXTRA_SUBJECT = "android.intent.extra.SUBJECT";

        public const string EXTRA_TITLE = "android.intent.extra.TITLE";

        /// <summary>
        /// An Intent describing the choices you would like shown with
        /// <see cref="ACTION_PICK_ACTIVITY"/> or <see cref="ACTION_CHOOSER"/>.
        /// </summary>
        public const string EXTRA_INTENT = "android.intent.extra.INTENT";

        /// <summary>
        /// A constant CharSequence that is associated with the Intent, used with
        /// <see cref="ACTION_SEND"/> to supply the literal data to be sent.  Note that
        /// this may be a styled CharSequence, so you must use
        /// <see cref="Bundle.GetCharSequence(string)"/> and <see cref="Bundle.GetCharSequence()"/> to retrieve it.
        /// </summary>
        public const string EXTRA_TEXT = "android.intent.extra.TEXT";

        public const string EXTRA_INITIAL_INTENTS = "android.intent.extra.INITIAL_INTENTS";

        /// <summary>
        /// If set, the recipient of this Intent will be granted permission to
        /// perform read operations on the URI in the Intent's data and any URIs
        /// specified in its ClipData.  When applying to an Intent's ClipData,
        /// all URIs as well as recursive traversals through data or other ClipData
        /// in Intent items will be granted; only the grant flags of the top-level
        /// Intent are used.
        /// </summary>
        public const int FLAG_GRANT_READ_URI_PERMISSION = 0x00000001;

        /// <summary>
        /// <para>
        /// If set, this activity will become the start of a new task on this
        /// history stack.  A task (from the activity that started it to the
        /// next task activity) defines an atomic group of activities that the
        /// user can move to.  Tasks can be moved to the foreground and background;
        /// all of the activities inside of a particular task always remain in
        /// the same order.
        ///</para>
        /// <para>
        /// This flag is generally used by activities that want
        /// to present a "launcher" style behavior: they give the user a list of
        /// separate things that can be done, which otherwise run completely
        /// independently of the activity launching them.
        ///</para>
        /// <para>
        /// When using this flag, if a task is already running for the activity
        /// you are now starting, then a new activity will not be started; instead,
        /// the current task will simply be brought to the front of the screen with
        /// the state it was last in.  See <see cref="FLAG_ACTIVITY_MULTIPLE_TASK"/> for a flag
        /// to disable this behavior.
        ///</para>
        ///<para>
        /// This flag can not be used when the caller is requesting a result from
        /// the activity being launched.
        /// </para>
        /// </summary>
        public const int FLAG_ACTIVITY_NEW_TASK = 0x10000000;

        /// <summary>
        /// <para>
        /// This flag is used to create a new task and launch an activity into it.
        /// This flag is always paired with either {@link #FLAG_ACTIVITY_NEW_DOCUMENT}
        /// or <see cref="FLAG_ACTIVITY_NEW_TASK"/>. In both cases these flags alone would
        /// search through existing tasks for ones matching this Intent. Only if no such
        /// task is found would a new task be created. When paired with
        /// <see cref="FLAG_ACTIVITY_MULTIPLE_TASK"/> both of these behaviors are modified to skip
        /// the search for a matching task and unconditionally start a new task.
        ///</para>
        /// <para>
        /// <strong>When used with <see cref="FLAG_ACTIVITY_NEW_TASK"/> do not use this
        /// flag unless you are implementing your own
        /// top-level application launcher.</strong>  Used in conjunction with
        /// <see cref="FLAG_ACTIVITY_NEW_TASK"/> to disable the
        /// behavior of bringing an existing task to the foreground.  When set,
        /// a new task is <em>always</em> started to host the Activity for the
        /// Intent, regardless of whether there is already an existing task running
        /// the same thing.
        ///</para>
        /// <para><strong>Because the default system does not include graphical task management,
        /// you should not use this flag unless you provide some way for a user to
        /// return back to the tasks you have launched.</strong>
        ///</para>
        /// See {@link #FLAG_ACTIVITY_NEW_DOCUMENT} for details of this flag's use for
        /// creating new document tasks.
        ///
        /// <para>This flag is ignored if one of <see cref="FLAG_ACTIVITY_NEW_TASK"/> or
        /// {@link #FLAG_ACTIVITY_NEW_DOCUMENT} is not also set.
        ///</para>
        /// </summary>
        public const int FLAG_ACTIVITY_MULTIPLE_TASK = 0x08000000;

        /// <summary>
        /// If set, the recipient of this Intent will be granted permission to
        /// perform write operations on the URI in the Intent's data and any URIs
        /// specified in its ClipData.  When applying to an Intent's ClipData,
        /// all URIs as well as recursive traversals through data or other ClipData
        /// in Intent items will be granted; only the grant flags of the top-level
        /// Intent are used.
        /// 
        /// </summary>
        public const int FLAG_GRANT_WRITE_URI_PERMISSION = 0x00000002;

        /// <summary>
        /// Create an intent with a given action.  All other fields (data, type,
        /// class) are null.  Note that the action <em>must</em> be in a
        /// namespace because Intents are used globally in the system -- for
        /// example the system VIEW action is android.intent.action.VIEW; an
        /// application's custom action would be something like
        /// com.google.app.myapp.CUSTOM_ACTION.
        /// </summary>
        /// <param name="action">The Intent action, such as <see cref="ACTION_VIEW"/>.</param>
        public Intent(string action) : base(IntentClassFullName, action) {
        }

        public Intent(IntPtr obj) : base(obj) {
        }

        [NotNull]
        public Intent SetAction([CanBeNull] string action) {
            action = string.IsNullOrEmpty(action) ? null : action;
            Call<Intent>("setAction", action);

            return this;
        }

        /// <summary>
        /// Convenience function for creating a <see cref="ACTION_CHOOSER"/> Intent.
        /// Builds a new <see cref="ACTION_CHOOSER"/> Intent that wraps the given
        /// target intent, also optionally supplying a title.  If the target
        /// intent has specified <see cref="FLAG_GRANT_READ_URI_PERMISSION"/> or
        /// <see cref="FLAG_GRANT_WRITE_URI_PERMISSION"/>, then these flags will also be
        /// set in the returned chooser intent, with its ClipData set appropriately:
        /// either a direct reflection of {@link #getClipData()} if that is non-null,
        /// or a new ClipData built from {@link #getData()}.
        /// </summary>
        /// <param name="target">The Intent that the user will be selecting an activity to perform.</param>
        /// <param name="title">Optional title that will be displayed in the chooser.</param>
        /// <returns>Return a new Intent object that you can hand to</returns>
        public static Intent CreateChooser(Intent target, string title) {
            Intent chooser = CallStatic<Intent>("android/content/Intent", "createChooser", target, title);

            return chooser;
        }

        [NotNull]
        public Intent SetType([CanBeNull] string type) {
            Call<Intent>("setType", type);

            return this;
        }

        /// <summary>
        /// Add extended data to the intent.  The name must include a package
        /// prefix, for example the app com.UnityAndroidNative.contacts would use names
        /// like "com.UnityAndroidNative.contacts.ShowAll".
        /// </summary>
        /// <param name="name">The name of the extra data, with package prefix.</param>
        /// <param name="value">The String data value.</param>
        /// <returns>Returns the same Intent object, for chaining multiple calls into a single statement.</returns>
        /// <seealso cref="putExtras"/>
        /// <seealso cref="RemoveExtra"/> 
        /// <seealso cref="getStringExtra(string)"/>
        public Intent PutExtra(string name, string value) {
            Call<Intent>("putExtra", name, value);

            return this;
        }

        public ComponentName ResolveActivity(PackageManager pm) {
            return Call<ComponentName>("resolveActivity", pm);
        }

        public string GetPackage() {
            return Call<string>("getPackage");
        }

        public Uri GetData() {
            return Get<Uri>("getData");
        }


        /// <summary>
        /// (Usually optional) Set an explicit application package name that limits
        /// the components this Intent will resolve to.  If left to the default
        /// value of null, all components in all applications will considered.
        /// If non-null, the Intent can only match the components in the given
        /// application package.
        /// </summary>
        /// <param name="packageName">The name of the application package to handle theintent, or null to allow any application package.</param>
        /// <returns>Returns the same Intent object, for chaining multiple calls</returns>
        /// <seealso cref="GetPackage"/>
        /// <seealso cref="ResolveActivity"/>
        public Intent SetPackage([CanBeNull] string packageName) {
            Call<Intent>("setPackage", packageName);

            return this;
        }

        /// <summary>
        /// Remove extended data from the intent.
        /// </summary>
        /// <remarks></remarks>
        /// <param name="name"></param>
        /// <seealso cref="PutExtra(string,string)"/>
        public void RemoveExtra(string name) {
            CallVoid("removeExtra", name);
        }

        /// <summary>
        /// Add extended data to the intent. The name must include a package
        /// prefix, for example the app com.UnityAndroidNative.contacts would use names
        /// like "com.android.contacts.ShowAll".
        /// </summary>
        /// <param name="name">The name of the extra data, with package prefix.</param>
        /// <param name="value">The Parcelable[] data value.</param>
        /// <returns>Returns the same Intent object, for chaining multiple calls</returns>
        /// <seealso cref="putExtras"/>
        /// <seealso cref="RemoveExtra"/>
        /// <seealso cref="getParcelableArrayExtra(string) "/>
        public Intent PutExtra(string name, Parcelable[] value) {
            IntPtr methodId = AndroidJNIHelper.GetMethodID(mClass, "putExtra", "(Ljava/lang/String;[Landroid/os/Parcelable;)Landroid/content/Intent;", false);

            jvalue[] jArgs = new jvalue[2];
            jArgs[0].l = AndroidJNI.NewStringUTF(name);
            jArgs[1].l = ConvertToJNIArray<Intent>(value);
            AndroidJNI.CallObjectMethod(RawObject, methodId, jArgs);

            return this;
        }

        public Intent SetComponent([CanBeNull] ComponentName component) {
            //            Call<JavaObject>("setComponent", component != null ? component.GetInternalObject() : null);

            return this;
        }

        public JavaObject GetInternalObject() {
            return this;
        }
    }
}

#endif