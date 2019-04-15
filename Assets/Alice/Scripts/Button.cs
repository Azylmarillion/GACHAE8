using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private bool m_IsActivated;
    private int m_objectsOnButton;

    public bool IsActivated { get => m_IsActivated; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box") || collision.CompareTag("Player") || collision.CompareTag("Corpse"))
        {
            m_objectsOnButton++;
            m_IsActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Box") || collision.CompareTag("Corpse"))
        {
            m_objectsOnButton--;

            if (m_objectsOnButton <= 0)
            {
                m_IsActivated = false;
            }

        }
    }
}
