using UnityEngine;

public static class AndroidIntentHelper
{
    public static void LaunchExternalActivity()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                using (AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent"))
                {
                    using (AndroidJavaObject componentName = new AndroidJavaObject(
                        "android.content.ComponentName",
                        "com.VRapeutic.masterapp",
                        "com.VRapeutic.masterapp.activities.MainActivity")) 
                    {
                        intent.Call<AndroidJavaObject>("setComponent", componentName);
                    }

                    int FLAG_ACTIVITY_NEW_TASK = new AndroidJavaClass("android.content.Intent")
                        .GetStatic<int>("FLAG_ACTIVITY_NEW_TASK");

                    intent.Call<AndroidJavaObject>("setFlags", FLAG_ACTIVITY_NEW_TASK);
                    currentActivity.Call("startActivity", intent);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to launch activity: " + e.ToString());
        }
#endif
    }
}
