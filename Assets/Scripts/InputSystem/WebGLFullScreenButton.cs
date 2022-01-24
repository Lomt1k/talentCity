using UnityEngine;

public class WebGLFullScreenButton : MonoBehaviour
{
    private void Start()
    {
        #if !PLATFORM_WEBGL || UNITY_EDITOR
        gameObject.SetActive(false);
        #endif
    }

    //unity event
    public void OnClickFullScreen()
    {
        Screen.fullScreen = true;
        gameObject.SetActive(false);
    }

    //unity event
    public void OnClickWindowed()
    {
        gameObject.SetActive(false);
    }
}
