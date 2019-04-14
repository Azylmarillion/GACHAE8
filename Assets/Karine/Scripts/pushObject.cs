using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushObject : MonoBehaviour
{
    public Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(GameManager.m_PushPower == true)
            {
                rb.constraints = RigidbodyConstraints2D.None;
            } else
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(20, 0));
            //rb.velocity = new Vector2(1, 0);
        }
    }
}
