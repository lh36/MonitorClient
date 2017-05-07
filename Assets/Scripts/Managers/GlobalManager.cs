using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GlobalManager : SingletonUnity<GlobalManager>
{
    private bool m_bIsGameRunning = false;
    private string m_sDirScene = "";

    public bool IsGameRunning
    {
        get {return this.m_bIsGameRunning;}
    }
    public string DirScene
    {
        get {return this.m_sDirScene;}
    }

    void Start()
    {
        UIManager.Instance.ShowViewByName (Constant.UI_Init);
    }


    //对外接口
    public void StartInstance(int iID)
    {
        UIManager.Instance.CloseAllView ();

    }
}


