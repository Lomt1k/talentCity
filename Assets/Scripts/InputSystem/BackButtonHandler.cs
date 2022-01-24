using TalentCity.GameModes;
using UnityEngine;

public class BackButtonHandler : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackButtonPressed();
        }
    }

    private void OnBackButtonPressed()
    {
        bool exitFromGameMode = GameModeManager.instance.TryExitCurrentGameMode();
        if (exitFromGameMode)
            return;
        
        #if !PLATFORM_WEBGL && !UNITY_EDITOR
        Application.Quit();
        #endif
    }
}
