using UnityEngine;

namespace TalentCity.InputSystem
{
    public class CloseButtonPC : MonoBehaviour
    {
        void Start()
        {
            #if !UNITY_EDITOR && !UNITY_STANDALONE
            gameObject.SetActive(false);
            #endif
        }

        //unity event
        public void OnButtonClick()
        {
            Application.Quit();
        }
    }
}
