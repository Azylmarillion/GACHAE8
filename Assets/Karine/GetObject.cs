using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetObject : MonoBehaviour
{
    public Text ObjectText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ObjectText.text = "Vous avez récupéré " + gameObject.name;
            gameObject.SetActive(false);

        }
    }

}
