using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreController : MonoBehaviour
{
    public float m_score = 0f;

    public void IncreaseScore(float points)
    {
        m_score += points;
    }
}
