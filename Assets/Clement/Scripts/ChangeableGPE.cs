using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeableGPE : MonoBehaviour
{
    [SerializeField]
    protected bool TESTbigSmallTEST;
    protected bool TESTbigsmallBufferTEST;

    [SerializeField]
    protected GPE m_BigGPE;
    [SerializeField]
    protected GPE m_MediumGPE;
    [SerializeField]
    protected GPE m_SmallGPE;

    [SerializeField]
    protected bool m_IsGPELocked;
    protected GPE m_CurrentGPE;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentGPE = null;

        if (TESTbigSmallTEST)
        {
            m_CurrentGPE = Instantiate<GPE>(m_BigGPE, this.transform.position, Quaternion.identity, this.gameObject.transform);
        }

        else
        {
            m_CurrentGPE = Instantiate<GPE>(m_SmallGPE, this.transform.position, Quaternion.identity, this.gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TESTbigsmallBufferTEST != TESTbigSmallTEST && !m_IsGPELocked)
        {
            if (TESTbigSmallTEST)
            {
                ChangeCurrentTo(m_BigGPE);
            }

            else
            {
                ChangeCurrentTo(m_SmallGPE);
            }
        }

        TESTbigsmallBufferTEST = TESTbigSmallTEST;
    }

    void ChangeCurrentTo(GPE _WhichGPE)
    {
        Destroy(m_CurrentGPE.gameObject);
        m_CurrentGPE = Instantiate(_WhichGPE, this.transform.position, Quaternion.identity, this.gameObject.transform);
    }
}
