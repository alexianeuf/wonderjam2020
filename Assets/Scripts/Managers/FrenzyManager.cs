using UnityEngine;

namespace Managers
{
    public class FrenzyManager : MonoBehaviour
    {
        public static FrenzyManager instance;
        void Start()
        {
            instance = this;
        }

        public void OnFrenzyStart()
        {
            // Enable the enemy manager
            // Observe if the player is still in frenzy
            // Red Filter on the camera

            Radio.instance.AngryMode();

        }

        public void OnFrenzyExit()
        {

            Radio.instance.CalmMode();

        }
    }
}