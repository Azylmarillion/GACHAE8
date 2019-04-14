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
        if (collision.CompareTag("Player"))
        {

            GameManager.E_Death.Invoke();
            collision.transform.position = GameManager.m_RespawnPoint;

            m_MeshToReveal.SetActive(true);
            m_FailTrigger.enabled = false;

        }
    }

    private void OnPlayerDeath()
    {
        m_MeshToReveal.SetActive(false);
        m_FailTrigger.enabled = true;
    }
}
