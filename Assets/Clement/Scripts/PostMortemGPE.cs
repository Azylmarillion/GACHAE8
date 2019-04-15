using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PostMortemGPE : MonoBehaviour
{
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
        PlayerCharacter player = collision.GetComponent<PlayerCharacter>();

        if (collision.CompareTag("Player") && player.m_CanDie)
        {
            GameManager.E_Death.Invoke();
        }
    }
    
}
