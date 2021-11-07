using TMPro;
using UnityEngine;

public class VersionView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _versionText;

    void Start()
    {
        _versionText.text = "version " + Application.version;
    }
}
