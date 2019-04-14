using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private float m_TimeObservedBeforeDying = 2;
    private float m_TimeSpentObserved;
    [SerializeField]
    private GameObject m_StonePrefab = null;

// Start is called before the first frame update
void Start()
    {
        m_TimeSpentObserved = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_TimeSpentObserved >= m_TimeObservedBeforeDying)
        {
            TurnIntoStone();
        }

        if (GameManager.m_IsBeingObserved)
        {
            m_TimeSpentObserved += Time.fixedDeltaTime;
        }

        else
        {
            m_TimeSpentObserved = 0;
        }
    }

    private void TurnIntoStone()
    {
        if (m_StonePrefab != null)
        {
            Instantiate(m_StonePrefab, transform.position, Quaternion.identity);
        }

        GameManager.E_Death.Invoke();
        m_TimeSpentObserved = 0;
        transform.position = GameManager.m_RespawnPoint;

    }
}
