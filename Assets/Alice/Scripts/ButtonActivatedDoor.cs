using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivatedDoor : MonoBehaviour
{
    [SerializeField] private List<Button> m_Buttons;
    private SpriteRenderer m_Mesh;
    private BoxCollider2D m_Collider;
    private bool m_haveVerfiOnce = false;

    void Start()
    {
        m_Mesh = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(!m_haveVerfiOnce)
        {
            VerifButton();
        }
    }

    void VerifButton()
    {
        foreach (Button b in m_Buttons)
        {
            if (!b.IsActivated)
            {
                if (!m_Mesh.enabled || !m_Collider.enabled)
                {
                    m_Mesh.enabled = true;
                    m_Collider.isTrigger = false;
                }

                return;
            }
        }

        m_Mesh.enabled = false;
        m_Collider.isTrigger = true;
        GameManager.m_RespawnPoint = transform.position;

        GameManager.E_LevelUp.Invoke();

        m_haveVerfiOnce = true;
    }
}
