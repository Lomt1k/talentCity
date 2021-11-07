using UnityEngine;

namespace TalentCity.UI.Animations
{
    public class LoadingIconAnim : MonoBehaviour
    {
        [SerializeField] private float _speed = 100f;

        private float _rotationZ = 0f;
        
        private void Update()
        {
            _rotationZ -= _speed * Time.deltaTime;
            Vector3 eulerRotation = new Vector3(0f, 0f, _rotationZ);
            transform.rotation = Quaternion.Euler(eulerRotation);
        }
        
    }
}

