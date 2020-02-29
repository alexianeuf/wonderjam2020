using UnityEngine;

namespace TestScripts
{
    // TODO : Remove this test class and the associate prefab/folder when passent are done
    public class FakePassent : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GameObject player = other.transform.parent.gameObject;

            if (player.CompareTag("Player"))
            {
                PlayerFrenzyController frenzyController = player.GetComponent<PlayerFrenzyController>();
                frenzyController.IncreaseFrenzyLevel();
            }
        }
    }
}
