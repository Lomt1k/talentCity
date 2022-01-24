using TalentCity.GameModes;
using UnityEngine;

public class ExitGameModeButton : MonoBehaviour
{
    //unity event
    public void OnClick()
    {
        GameModeManager.instance.TryExitCurrentGameMode();
    }
}
