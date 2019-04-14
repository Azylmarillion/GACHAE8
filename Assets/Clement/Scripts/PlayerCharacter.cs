using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float m_TimeObservedBeforeDying = 2;
    [SerializeField] private GameObject m_StonePrefab = null;
    private Vector3 m_OriginalScale = Vector3.zero;
    private float m_TimeSpentObserved;
    public List<GameObject> m_corpses;
    GameObject m_MeshInstance = null;

    void Start()
    {
        m_TimeSpentObserved = 0;
        m_OriginalScale = transform.localScale;
    }

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
        GameManager.m_nbrCadavre--;
        GameManager.E_Death.Invoke();
        transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);

        if (m_StonePrefab != null && GameManager.m_nbrCadavre >= 0)
        {
            m_corpses.Add(Instantiate(m_StonePrefab, transform.position, Quaternion.identity));
        }
        else
        {
            IsGameOver();
        }

        transform.position = GameManager.m_RespawnPoint;
        m_TimeSpentObserved = 0;
    }

    public void IsGameOver()
    {
        if (GameManager.m_nbrCadavre < 0)
        {
            //GameOver HUD

            for (int i = 0; i < m_corpses.Count; i++)
            {
                Destroy(m_corpses[i]);
            }

            m_corpses.Clear();
            GameManager.m_nbrCadavre = GameManager.m_nbrCadavreMax;
            transform.localScale = m_OriginalScale;

        }
    }
}
