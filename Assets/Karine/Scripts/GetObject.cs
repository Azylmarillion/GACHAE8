using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetObject : MonoBehaviour
{
    [SerializeField]
    private Text objectText;

    protected bool pickedUp = false;

    [SerializeField]
    private ParticleSystem particles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (objectText != null)
            {
                objectText.text = "Vous avez récupéré " + gameObject.name;
                gameObject.SetActive(false);
            }
            if (!pickedUp)
            {
                switch (GameManager.m_PickUpsGot)
                {
                    case 0:
                        GameManager.m_JumpSpeedPower = true;
                        GameManager.m_PickUpsGot += 1;
                        Debug.Log("Jump & Speed Got");
                        pickedUp = true;

                        break;
                    case 1:
                        GameManager.m_PushPower = true;
                        GameManager.m_PickUpsGot += 1;
                        GameManager.m_VignetteMaxSize = 0.3f;
                        GameManager.m_VignetteMinSize = 0.1f;
                        Debug.Log("Push Got");
                        pickedUp = true;
                        break;
                    case 2:
                        GameManager.m_SightPower = true;
                        GameManager.m_PickUpsGot += 1;
                        GameManager.m_VignetteMaxSize = 0f;
                        GameManager.m_VignetteMinSize = 0f;
                        GameManager.m_SaturationValue = -30f;
                        Debug.Log("Sight Got");
                        pickedUp = true;
                        break;
                    case 3:
                        GameManager.m_ProjectPower = true;
                        GameManager.m_PickUpsGot += 1;
                        GameManager.m_SaturationValue = 0f;
                        Debug.Log("Projectil Got");
                        pickedUp = true;
                        break;
                }
                
            }
        }
    }

}
