using UnityEngine;

namespace Managers
{
    public class FrenzyManager : MonoBehaviour
    {
        public static FrenzyManager instance;
        public static bool isFrenzy = false;
        void Start()
        {
            instance = this;
        }

        public void OnFrenzyStart()
        {
            // Enable the enemy manager
            // Observe if the player is still in frenzy
            // Red Filter on the camera

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