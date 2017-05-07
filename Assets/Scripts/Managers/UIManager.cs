using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : SingletonUnity<UIManager> {

	private GameObject m_RootNode;  //UI根节点

    private List<GameObject> m_ViewList = null; //UI列表

	

	void Awake()
	{
		//Screen.SetResolution (1024, 768, false);
        this.m_ViewList = new List<GameObject> ();

        this.m_RootNode = GameObject.Find ("UI");
	}

	void Start()
	{
	}

    /// <summary>
    /// 显示UI
    /// </summary>
    /// <param name="sViewName">UI name.</param>
    /// <param name="bOver">是否直接覆盖而不做其他操作</param>
    public void ShowViewByName(string sViewName, bool bOver=false)
    {
        GameObject oView = this.m_RootNode.transform.Find (sViewName).gameObject;
        if (oView == null)
        {
            return;
        }

        oView.SetActive (true);

        if (!bOver) 
        {
            var oCurrentView = GetCurrentView();
            if (oCurrentView != null)
            {
                oCurrentView.SetActive (false);
                this.m_ViewList.Remove (oCurrentView);
            }
        }

        this.m_ViewList.Add (oView);
    }

    /// <summary>
    /// 获取当前UI
    /// </summary>
    public GameObject GetCurrentView()
    {
        if (this.m_ViewList == null)
        {
            return null;
        }

        int iLen = this.m_ViewList.Count;
        if (iLen == 0)
        {
            return null;
        }
        return this.m_ViewList[iLen - 1];
    }

    /// <summary>
    /// 关闭指定的UI
    /// </summary>
    /// <param name="sViewName">UI name.</param>
    public void CloseViewByName(string sViewName)
    {
        foreach (var oView in this.m_ViewList) 
        {
            if(oView.name == sViewName)
            {
                oView.SetActive (false);
                this.m_ViewList.Remove (oView);
                return;
            }
        }
    }

    /// <summary>
    /// Closes all view.
    /// </summary>
    public void CloseAllView()
    {
        foreach (var oView in this.m_ViewList) 
        {
            oView.SetActive (false);
        }
        this.m_ViewList.Clear ();
    }

}
