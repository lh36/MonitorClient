﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MinimapView : MonoBehaviour
{
    private GameModel m_Model;

    public GameObject MapBg;
    public GameObject pf_Point;
	public RawImage VideoImage;
	public int RequestFPS = 5;
	private GetVideoDataApi m_VideoApi;
	private float m_fUpdateTime = 0;

    private Dictionary<int, GameObject> m_PointDict = new Dictionary<int, GameObject> ();
    private Vector2 m_v2RealMapSize;
    private Vector2 m_v2MinimapSize;

    void Awake()
    {
		var v2Size = VideoImage.gameObject.GetComponent<RectTransform> ().sizeDelta;

		this.m_VideoApi = new GetVideoDataApi ();
		this.m_VideoApi.AddCallback ((int)v2Size.x, (int)v2Size.y, this.SetVideoImage);
    }

    // Use this for initialization
    void Start ()
    {
		this.m_Model = this.gameObject.transform.parent.gameObject.GetComponent<GameModel> ();

        this.m_v2RealMapSize = GlobalManager.Instance.MapSize;
        this.m_v2MinimapSize = this.MapBg.GetComponent<RectTransform> ().sizeDelta;
		this.m_v2MinimapSize = new Vector2 (this.m_v2MinimapSize.y, this.m_v2MinimapSize.x);

        SignalManager.Instance.AddHandler (SignalID.ShipParamChanged, this.SetPointPos);
        SignalManager.Instance.AddHandler (SignalID.GameView_ControlChanged, this.SetPointShape);

    }
	
    void OnEnable()
    {
        if(this.m_PointDict.Count > 0)
        {
            foreach(var item in this.m_PointDict)
            {
                Destroy (item.Value);
            }
            this.m_PointDict.Clear ();
        }
    }
    
    // Update is called once per frame
    void Update ()
    {
		this.m_fUpdateTime += Time.fixedDeltaTime;

		if(this.m_fUpdateTime < 1f / (float)RequestFPS)
		{
			return;
		}

		if (GlobalManager.Instance.IsGameRunning && this.m_VideoApi.IsIdle())
		{
			StartCoroutine (this.m_VideoApi.Request ());
			this.m_fUpdateTime = 0;
		}
			
    }

    private void SetPointShape(object oSender, object oParam)
    {
        Debug.Log (oParam.ToString ());
        int iShipID = (int)oParam;
        //坐标颜色
        foreach(var item in this.m_PointDict)
        {
            if (iShipID == item.Key)
            {
                item.Value.GetComponent<Image> ().color = new Color (1, 0, 0);
            }
            else
            {
                item.Value.GetComponent<Image> ().color = new Color (0, 0, 1);
            }
        }
    }

    private void SetPointPos(object oSender, object oParam)
    {
        int iShipID = (int)oSender;
        GameObject oPoint = null;
        if (!this.m_PointDict.ContainsKey(iShipID))
        {
            oPoint = Instantiate (this.pf_Point);
            oPoint.transform.SetParent(this.MapBg.transform);
            this.m_PointDict.Add (iShipID, oPoint);
            //文本设置
            oPoint.transform.Find ("Text").gameObject.GetComponent<Text> ().text = iShipID.ToString ();
            //坐标颜色
            SetPointShape (null, this.m_Model.GetControlledShipID());
        }
        oPoint = this.m_PointDict[iShipID];
        //坐标位置
        SShipParam oShipParam = oParam as SShipParam;
        if(oShipParam == null)
        {
            return;
        }
        float fPosX = (float)oShipParam.posX / this.m_v2RealMapSize.x * this.m_v2MinimapSize.x;
        float fPosY = (float)oShipParam.posY / this.m_v2RealMapSize.y * this.m_v2MinimapSize.y;
        oPoint.transform.localPosition = new Vector3 (fPosY, fPosX, 0);

    }


	private void SetVideoImage(object oSender, object oParam)
	{
		var texture = oParam as Texture2D;
		VideoImage.texture = texture;
	}

}

