using UnityEngine;
using UnityEngine.SceneManagement;

namespace TalentCity
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private float _timeToLoad = 5f;

        private void Start()
        {
            Invoke(nameof(LoadScene), _timeToLoad);
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                CancelInvoke(nameof(LoadScene));
                LoadScene();
            }
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
