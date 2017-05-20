using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GlobalManager : SingletonUnity<GlobalManager>
{
    private bool m_bIsGameRunning = false;
    private string m_sDirScene = "";

    private int m_iInstanceID = 0;
    private InstanceResp m_Instance;
	private ConnectInstanceApi m_HeartApi;
    public Vector2 MapSize = new Vector2 (50, 25);

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
		this.gameObject.AddComponent<ConnectInstanceApi> ();
		this.m_HeartApi = this.gameObject.GetComponent<ConnectInstanceApi> ();
		this.m_HeartApi.AddCallback (this.ConnectResult);
		Invoke ("ConnectInstance", 2);

        UIManager.Instance.ShowViewByName (Constant.UI_Init);
    }

	void Update()
	{
		
	}

	private void ConnectInstance()
	{
		if(this.m_bIsGameRunning)
		{
			if(this.m_HeartApi.IsIdle())
			{
				StartCoroutine (this.m_HeartApi.Request (this.m_iInstanceID));
			}
		}

		Invoke ("ConnectInstance", 2);
	}

	private void ConnectResult(object oSender, object oParam)
	{
		if(!(bool)oParam)
		{
            QuitInstance ();
		}
	}

    //对外接口
    public void StartInstance(int iInstanceID, InstanceResp oInstance)
    {
		Debug.Log (iInstanceID);
        this.m_iInstanceID = iInstanceID;
        this.m_Instance = oInstance;

        UIManager.Instance.CloseAllView ();
        ShipManager.Instance.CreateNewInstance (iInstanceID, oInstance);
        UIManager.Instance.ShowViewByName (Constant.UI_Game);
    }

    public void QuitInstance()
    {
        this.m_bIsGameRunning = false;
        ShipManager.Instance.DestroyShip ();
        CameraController.Instance.ResetCameraPosition ();
        UIManager.Instance.CloseAllView ();
        UIManager.Instance.ShowViewByName (Constant.UI_Init);
    }

	public int GetInstanceID()
	{
		return this.m_iInstanceID;
	}

    public InstanceResp GetInstanceData()
    {
        return this.m_Instance;
    }
}


