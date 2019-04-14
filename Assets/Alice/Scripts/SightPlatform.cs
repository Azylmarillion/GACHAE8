using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightPlatform : MonoBehaviour
{
    public GameObject m_Player;
    public float m_Range = 3.0f;
    private SpriteRenderer m_Mesh;

    void Start()
    {
        GameManager.m_SightPower = true;
        m_Mesh = GetComponent<SpriteRenderer>();

        if (!m_Mesh)
        {
            Debug.LogError("Can't find mesh renderer on platform");
            return;
        }
        
        m_Mesh.enabled = false;
    }

    void Update()
    {
        if (GameManager.m_SightPower)
        {
          
            Vector3 distanceToPlayer = transform.position - m_Player.transform.position;
            Debug.Log(distanceToPlayer.magnitude);
            SetMeshVisibility(distanceToPlayer.magnitude <= m_Range);
        }
    }

    private void SetMeshVisibility(bool _isVisible)
    {
        m_Mesh.enabled = _isVisible;
    }
}