using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaze : MonoBehaviour
{
    GameObject m_Target = null;
    [SerializeField]
    Transform m_DetectionMesh;
    [SerializeField]
    LayerMask m_DetectionMask;

    // Start is called before the first frame update
    void Start()
    {
        m_DetectionMesh.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Target)
        {


            var dir = m_Target.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(m_Target.transform.position.x, m_Target.transform.position.y) - new Vector2(transform.position.x, transform.position.y), 5000, m_DetectionMask);

            // If it hits something...
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                GameManager.m_IsBeingObserved = true;
                m_DetectionMesh.gameObject.SetActive(true);
            }

            else
            {
                GameManager.m_IsBeingObserved = false;
                m_DetectionMesh.gameObject.SetActive(false);
            }
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
            GameManager.m_IsBeingObserved = false;
            m_DetectionMesh.gameObject.SetActive(false);
            m_Target = null;
        }
    }
}
