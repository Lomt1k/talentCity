using TalentCity.GameModes;
using UnityEngine;

public class ExitGameModeButton : MonoBehaviour
{
    //unity event
    public void OnClick()
    {
        GameModeManager.instance.ExitCurrentGameMode();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClick();
        }
    }
}
