using UnityEngine;

namespace Managers
{
    public class FrenzyManager : MonoBehaviour
    {
        public static FrenzyManager instance;
        public static bool isFrenzy = false;
        void Awake()
        {
            instance = this;
            isFrenzy = false;
        }

        public void OnFrenzyStart()
        {
            // Enable the enemy manager
            isFrenzy = true;
            Radio.instance.AngryMode();

        }

        public void OnFrenzyExit()
        {
            isFrenzy = false;
            Radio.instance.CalmMode();
        }
    }
}