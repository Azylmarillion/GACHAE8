using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PostMortemGPE : MonoBehaviour
{
    [SerializeField]
    GameObject m_CorpseToReveal;
    BoxCollider2D m_FailTrigger;
    GameObject m_CorpseInstance = null;

    // Start is called before the first frame update
    void Start()
    {
        m_FailTrigger = GetComponent<BoxCollider2D>();
        GameManager.E_Death.AddListener(OnPlayerDeath);

        if (!m_CorpseToReveal)
        {
            Debug.Log("You need to put a MeshToReveal in the PostMortemGPE (a corpse) for it to work.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCharacter player = collision.GetComponent<PlayerCharacter>();

        if (player && m_CorpseToReveal)
        {
            GameManager.m_nbrCadavre--;
            GameManager.E_Death.Invoke();
            collision.transform.position = GameManager.m_RespawnPoint;
            collision.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f); 

            if (GameManager.m_nbrCadavre >= 0)
            {
                m_CorpseInstance = Instantiate(m_CorpseToReveal, transform.position, transform.rotation);
                player.m_corpses.Add(m_CorpseInstance);
            }

            else
            {
                player.IsGameOver();
            }
        }

    }

    private void OnPlayerDeath()
    {
        // m_MeshToReveal.SetActive(false);
        // m_FailTrigger.enabled = true;
    }
}
