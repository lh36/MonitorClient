using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseModel : MonoBehaviour
{
    private GetInstanceDataApi m_Api;
    private int m_iInstanceID = 0;

    public void Init()
    {
        this.m_Api = new GetInstanceDataApi ();
        this.m_Api.AddCallback (this.StartVideo);
    }

    private void StartVideo(object o, object oData)
    {
        GlobalManager.Instance.StartVideo (this.m_iInstanceID, oData as CollectionData);
    }

    public float GetProgress()
    {
        return this.m_Api.GetProgress ();
    }

    public void OnChooseClicked(int iInstanceID)
    {
        this.m_iInstanceID = iInstanceID;
        StartCoroutine (this.m_Api.Request (iInstanceID));
    }

    public void OnBackClicked()
    {
        UIManager.Instance.ShowViewByName (Constant.UI_Init);
    }


}

