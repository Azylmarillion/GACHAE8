using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PostMortemGPE : MonoBehaviour
{
    [SerializeField]
    GameObject m_MeshToReveal;
    BoxCollider2D m_FailTrigger;


    // Start is called before the first frame update
    void Start()
    {
        m_FailTrigger = GetComponent<BoxCollider2D>();
        m_MeshToReveal.SetActive(false);
        GameManager.E_Death.AddListener(OnPlayerDeath);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.m_nbrCadavre--;
        GameManager.E_Death.Invoke();
        collision.transform.position = GameManager.m_RespawnPoint;
        Debug.Log(GameManager.m_nbrCadavre);

        if (GameManager.m_nbrCadavre >= 0)
        {
            m_MeshToReveal.SetActive(true);
            m_FailTrigger.enabled = false;
        } else
        {
            Debug.Log("Vous êtes mort");
        }

    }

    private void OnPlayerDeath()
    {
       // m_MeshToReveal.SetActive(false);
       // m_FailTrigger.enabled = true;
    }
}
