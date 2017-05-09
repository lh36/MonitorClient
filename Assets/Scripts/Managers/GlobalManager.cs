using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GlobalManager : SingletonUnity<GlobalManager>
{
    private bool m_bIsGameRunning = false;
    private string m_sDirScene = "";

    private int m_iInstanceID = 0;
    private InstanceResp m_Instance;
    public Vector2 MapSize = new Vector2 (100, 100);

    public bool IsGameRunning
    {
        set {this.m_bIsGameRunning = value;}
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
    public void StartInstance(int iInstanceID, InstanceResp oInstance)
    {
        this.m_iInstanceID = iInstanceID;
        this.m_Instance = oInstance;

        UIManager.Instance.CloseAllView ();
        ShipManager.Instance.CreateNewInstance (iInstanceID, oInstance);
        UIManager.Instance.ShowViewByName (Constant.UI_Game);
    }

    public InstanceResp GetInstanceData()
    {
        return this.m_Instance;
    }
}


