using UnityEngine;

[System.Serializable]
public class Spawnable
{
    [System.NonSerialized]
    private string m_name;

    public GameObject _prefab;

    public float _spawnFrequence = 5f;
    public float _maxNumber = 3f;
    public float _currentNumber = 0f;

    public void SetName(string name)
    {
        m_name = name;
    }

    public string GetName()
    {
        return m_name;
    }
}