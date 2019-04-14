using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]  private float m_TimeObservedBeforeDying = 2;
    [SerializeField]  private GameObject m_DeadBodyPrefab = null;
    private float m_TimeSpentObserved;

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
        if (m_DeadBodyPrefab != null)
        {
            Instantiate(m_DeadBodyPrefab, transform.position, Quaternion.identity);
        }
        if(GameManager.m_nbrCadavre >= 0)
        {
            transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
            GameManager.E_Death.Invoke();
            m_TimeSpentObserved = 0;
            transform.position = GameManager.m_RespawnPoint;
        }
    }
}
