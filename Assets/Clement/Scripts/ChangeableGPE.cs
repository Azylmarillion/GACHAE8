using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeableGPE : MonoBehaviour
{
    protected enum e_PlayerSize
    {
        e_Big,
        e_Medium,
        e_Small
    };

    protected e_PlayerSize m_PlayerCurrentSize = e_PlayerSize.e_Big;
    protected e_PlayerSize m_SizeBuffer = e_PlayerSize.e_Big;

    [SerializeField]
    protected GPE m_BigGPE;
    [SerializeField]
    protected GPE m_MediumGPE;
    [SerializeField]
    protected GPE m_SmallGPE;

    [SerializeField]
    private bool m_IsGPELocked;
    protected GPE m_CurrentGPE;

    public bool SetIsGPELocked { set => m_IsGPELocked = value; }

    // Start is called before the first frame update
    private void Start()
    {
        m_CurrentGPE = null;
        GPE GPEToInstantiateAtStart = null;

        switch (m_PlayerCurrentSize)
        {
            case e_PlayerSize.e_Big:
                GPEToInstantiateAtStart = m_BigGPE;
                break;
            case e_PlayerSize.e_Medium:
                GPEToInstantiateAtStart = m_MediumGPE;
                break;
            case e_PlayerSize.e_Small:
                GPEToInstantiateAtStart = m_SmallGPE;
                break;
        }

        m_SizeBuffer = m_PlayerCurrentSize;
    }

    // Update is called once per frame
    private void Update()
    {

        if (!m_IsGPELocked && m_SizeBuffer != m_PlayerCurrentSize)
        {
            switch (m_PlayerCurrentSize)
            {
                case e_PlayerSize.e_Big:
                    ChangeCurrentTo(m_BigGPE);
                    break;
                case e_PlayerSize.e_Medium:
                    ChangeCurrentTo(m_MediumGPE);
                    break;
                case e_PlayerSize.e_Small:
                    ChangeCurrentTo(m_SmallGPE);
                    break;
            }
        }

        m_SizeBuffer = m_PlayerCurrentSize;
    }

    private void ChangeCurrentTo(GPE _WhichGPE)
    {
        Destroy(m_CurrentGPE.gameObject);
        m_CurrentGPE = Instantiate(_WhichGPE, this.transform.position, Quaternion.identity, this.gameObject.transform);
    }
}
