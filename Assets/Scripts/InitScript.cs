using UnityEngine;

public class InitScript : MonoBehaviour
{
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        #if UNITY_ANDROID && !UNITY_EDITOR
        AndroidInit();
        #endif
    }

    private void AndroidInit()
    {
        Screen.fullScreen = false; //для отображения системных кнопок
    }
    
    
}
