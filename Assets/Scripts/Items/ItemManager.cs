using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    [SerializeField]
    public List<Spawnable> m_spawnableList;

    public GameObject m_spawnPointHolder;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        // set item names
        foreach (Spawnable item in m_spawnableList)
        {
            item.SetName(item._prefab.name);
        }

        // disable spawnpoints textures
        for (int i = 0; i < m_spawnPointHolder.transform.childCount; i++)
        {
            m_spawnPointHolder.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void Start()
    {
        foreach (Spawnable item in m_spawnableList)
        {
            StartCoroutine("SpawnItem", item);
        }
    }

    IEnumerator SpawnItem(Spawnable item)
    {
        while (true)
        {
            yield return new WaitForSeconds(item._spawnFrequence);

            if(item._currentNumber < item._maxNumber)
            {
                GameObject spawnPoint = GetRandomSpawnPoint();

                Instantiate(item._prefab, spawnPoint.transform.position, Quaternion.identity, transform);

                item._currentNumber++;
            }
        }
    }

    private GameObject GetRandomSpawnPoint()
    {
        if (m_spawnPointHolder.transform.childCount == 0)
            return null;

        return m_spawnPointHolder.transform.GetChild(Random.Range(0, m_spawnPointHolder.transform.childCount)).gameObject;
    }

    public void ItemWasConsumed(GameObject item)
    {
        foreach (Spawnable i in m_spawnableList)
        {
            if (item.name.Contains(i.GetName()))
            {
                i._currentNumber--;
                Destroy(item);
            }
        }
    }
}
