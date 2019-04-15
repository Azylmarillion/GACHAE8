using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float m_TimeObservedBeforeDying = 2;
    [SerializeField] private GameObject m_DeadBodyPrefab = null;
    [SerializeField] private Image m_LittleDoll;
    [SerializeField] private Image m_MediumDoll;
    [SerializeField] private Image m_BigDoll;
    [SerializeField] private GameObject m_GameOverCanvas;
    private Vector3 m_OriginalScale = Vector3.zero;
    private float m_TimeSpentObserved;
    public List<GameObject> m_corpses;
    GameObject m_MeshInstance = null;

    void Start()
    {
        if (!m_DeadBodyPrefab)
        {
            Debug.Log("You need to put a DeadBodyPrefab in the PlayerCharacter (a corpse) for it to work.");
        }

        m_TimeSpentObserved = 0;
        GameManager.E_Death.AddListener(OnPlayerDeath);
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

    private void OnPlayerDeath()
    {
        GameManager.m_nbrCadavre--;
        GameManager.m_SaturationValue -= 25f;
        GameManager.m_VignetteMaxSize += 0.2f;
        GameManager.m_VignetteMinSize += 0.1f;

        if (m_DeadBodyPrefab != null && GameManager.m_nbrCadavre >= 0)
        {
            GameObject corpse = Instantiate(m_DeadBodyPrefab, transform.position, Quaternion.identity);
            corpse.transform.localScale = transform.localScale;
            m_corpses.Add(corpse);
        }
        else
        {
            IsGameOver();
        }

        FeedBackTrace();

        float newScaleX = transform.localScale.x * 0.8f;
        float newScaleY = transform.localScale.y * 0.8f;
        transform.localScale = new Vector3(newScaleX, newScaleY, 1);
        transform.position = GameManager.m_RespawnPoint;
    }

    private void TurnIntoStone()
    {
        GameManager.E_Death.Invoke();
        m_TimeSpentObserved = 0;
    }

    public void IsGameOver()
    {
        if (GameManager.m_nbrCadavre < 0)
        {
            Time.timeScale = 0;
            m_GameOverCanvas.SetActive(true);

            for (int i = 0; i < m_corpses.Count; i++)
            {
                Destroy(m_corpses[i]);
            }

            m_corpses.Clear();
            GameManager.m_nbrCadavre = GameManager.m_nbrCadavreMax;
            transform.localScale = m_OriginalScale;

        }
    }

    public void FeedBackTrace()
    {
        if(GameManager.m_nbrCadavre == 2)
        {
            m_BigDoll.enabled = false;
        }
        else if(GameManager.m_nbrCadavre == 1)
        {
            m_MediumDoll.enabled = false;
        }
        else if(GameManager.m_nbrCadavre < 1)
        {
            m_LittleDoll.enabled = false;
        }
        
    }
}
