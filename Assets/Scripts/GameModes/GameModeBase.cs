using UnityEngine;

namespace TalentCity.GameModes
{
    public abstract class GameModeBase : MonoBehaviour
    {
        public abstract void Execute();

        public virtual void Exit()
        {
            Destroy(gameObject);
        }
    }
}
