using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewChangeTrigger : MonoBehaviour
{
    [SerializeField] private float m_changeTime = 5.0f;
    private bool m_HasBeenTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<Collider2D>())
        {
            Debug.Log("Put a collider2D in the viewchangetriggers please");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!m_HasBeenTriggered)
        {

            m_HasBeenTriggered = true;
            CameraBehaviour.Instance.ToggleGeneralView(m_changeTime);
            StartCoroutine(WaitToUntoggle(m_changeTime));
        }

    }

    private IEnumerator WaitToUntoggle(float _Wait)
    {
        yield return new WaitForSeconds(_Wait);
        CameraBehaviour.Instance.UntoggleGeneralView();
    }
}
