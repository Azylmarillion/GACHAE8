using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaze : MonoBehaviour
{
    GameObject m_Target = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Target)
        {
            transform.LookAt(m_Target.transform);
            Vector3 rotBuf = transform.eulerAngles;
            rotBuf.z = rotBuf.x;
            rotBuf.x = 0;
            rotBuf.y = 0;
            transform.eulerAngles = rotBuf;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_Target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_Target = null;
        }
    }
}
