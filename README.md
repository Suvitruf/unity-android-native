# Unity wrappers for android classes


This is a Unity plugin to work with some Android native sdk classes.

## Introduction
Assume, you want to get your game version name and code. Yes, you will write something like this using [`AndroidJavaClass`](https://docs.unity3d.com/ScriptReference/AndroidJavaClass.html) and [`AndroidJavaObject`](https://docs.unity3d.com/ScriptReference/AndroidJavaObject.html):

```csharp
public static int GetVersionCode() {
  AndroidJavaClass contextCls = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
  AndroidJavaObject context = contextCls.GetStatic<AndroidJavaObject>("currentActivity"); 
  AndroidJavaObject packageMngr = context.Call<AndroidJavaObject>("getPackageManager");
  string packageName = context.Call<string>("getPackageName");
  AndroidJavaObject packageInfo = packageMngr.Call<AndroidJavaObject>("getPackageInfo", packageName, 0);
  return packageInfo.Get<int>("versionCode");
}

public static string GetVersionName() {
  AndroidJavaClass contextCls = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
  AndroidJavaObject context = contextCls.GetStatic<AndroidJavaObject>("currentActivity"); 
  AndroidJavaObject packageMngr = context.Call<AndroidJavaObject>("getPackageManager");
  string packageName = context.Call<string>("getPackageName");
  AndroidJavaObject packageInfo = packageMngr.Call<AndroidJavaObject>("getPackageInfo", packageName, 0);
  return packageInfo.Get<string>("versionName");
}
```

This is kinda ugly. I'm crying internally, when I have to work with this classes.
So, the idea behind this repo was to create comfy lib to work with native java classes. Example from above with this lib:

```csharp
var activity = Internal.GetCurrentActivity();
var pm = activity.GetPackageManager();
var pi = pm.GetPackageInfo(activity.GetPackageName(), 0);
int code = pi.VersionCode;
string name = pi.VersionName;
```

Looks like the code you write when working directly with Java classes. That's it.

## Some Theory
There is `JavaObject` class with some internal methods for JNI access. For example, we want to create C# representation of Java class `Intent`. 

1. Namespace should be to Java full class name with prefix UnityAndroidNative. So, it will be `UnityAndroidNative.Android.Content`.
2. Derive it from our `Object` class (which derived from `JavaObject`).
3. Implement base constructor:
    ```csharp
    public Intent(IntPtr obj) : base(obj) {
    }
    ```
    Or constructor which takes action:
    ```csharp
    public Intent(string action) : base(IntentClassFullName, action) {
    }
    ```
    So, now you can instantiate this class:
    ```csharp
    Intent intent = new Intent(Intent.ACTION_SEND);
    ```
4. Now we can implement methods using methods of `JavaObject`. For example, `setAction`:
    ```csharp
    public Intent SetAction(string action) {
        Call<Intent>("setAction", action);
    
        return this;
    }
    ```
    You can instantiate `Intent` with default constructor and set action:
    ```csharp
    Intent intent = new Intent();
    intent.SetAction(Intent.ACTION_SEND);
    ```
5. Need method too return field value? Easy:
    ```csharp
    public Uri GetData() {
        return Get<Uri>("getData");
    }
    ```

## Real world example
Assume you want to start sharing `Intent` and filter app to show. Easy:
```csharp
  public static void Share(string body, string subject, string mimeType = "text/plain", string chooserTitle = "Choose application") {
      Intent intent = new Intent(Intent.ACTION_SEND);
      intent.SetType(mimeType)
          .PutExtra(Intent.EXTRA_SUBJECT, subject)
          .PutExtra(Intent.EXTRA_TEXT, body);

      var intentList = new List<Intent>();

      var activity = Internal.GetCurrentActivity();
      var pm = activity.GetPackageManager();
      var resInfo = pm.QueryIntentActivities(intent, 0);


      for (int i = 0; i < resInfo.Count; i++) {
          ResolveInfo ri = resInfo[i];
          string packageName = ri.ActivityInfo.GetPackageName();
          if (packageName.Contains("vkontakte")  || packageName.Contains("instagram") || packageName.Contains("skype")) {
              Intent newIntent = new Intent(Intent.ACTION_SEND);
              newIntent.SetPackage(packageName)
                  .PutExtra(Intent.EXTRA_SUBJECT, subject)
                  .PutExtra(Intent.EXTRA_TEXT, body)
                  .SetType(mimeType);

              intentList.Add(newIntent);
          }
      }

      Intent intentt = intentList[0];
      intentList.RemoveAt(0);
      Parcelable[] extraIntents = new Parcelable[intentList.Count];

      for (int i = 0; i < intentList.Count; i++) {
          extraIntents[i] = intentList[i];
      }

      var chooser = Intent.CreateChooser(intentt, chooserTitle);
      chooser.PutExtra(Intent.EXTRA_INITIAL_INTENTS, extraIntents);
      activity.StartActivity(chooser);
  }
 ```
 
## P.S.
There are not much you can do with this lib right now. But I'm going add new classes/methods from time to time.
