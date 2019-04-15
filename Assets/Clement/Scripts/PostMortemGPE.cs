using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PostMortemGPE : MonoBehaviour
{
    float m_KillBufferTime = 1;
    bool m_CanKill = true;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D failTrigger = GetComponent<BoxCollider2D>();

        if (!failTrigger)
        {
            Debug.Log("You need to put a BoxCollider2D in the PostMortemGPE for it to work.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && m_CanKill)
        {
            GameManager.E_Death.Invoke();
            m_CanKill = false;
            StartCoroutine(KillBuffer(m_KillBufferTime));
        }


    }

    // This was set up because it was detecting both triggers of the player, and it was killing twice
    private IEnumerator KillBuffer(float _Wait)
    {
        yield return new WaitForSeconds(_Wait);
        m_CanKill = true;
    }
}
