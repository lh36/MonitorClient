using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VideoManager : SingletonUnity<VideoManager>
{
    public float UpdateTime = 0.1f;

  //  private float m_fValue = 0f;
    private int m_CurrentIndex = 0;
    private int m_VideoLength = 0;
    private int m_VideoTime = 0;
    private int m_iShipNum = 0;
    private float m_fTime = 0f;

    private Dictionary<int, List<SShipParam>> VideoData;
    private Dictionary<int, int> VideoLength;

    // Use this for initialization
    void Start ()
    {
    
    }
    
    // Update is called once per frame
    void Update ()
    {
        if(!GlobalManager.Instance.IsVideoRunning)
        {
            return;
        }

        this.m_fTime += Time.deltaTime;

        if(this.m_fTime > UpdateTime)
        {
            this.m_fTime = 0f;

            if(this.m_CurrentIndex > this.m_VideoLength)
            {
                SetCurrentIndex (1f);
                return;
            }

            Dictionary<int, SShipParam> dData = new Dictionary<int, SShipParam> ();
            for(int i=1; i <= this.m_iShipNum; i++)
            {
                if(VideoLength[i] <= this.m_CurrentIndex)
                {
                    continue;
                }
                dData.Add (i, VideoData [i] [this.m_CurrentIndex]);
            }
            this.m_CurrentIndex += 1;
            ShipManager.Instance.SetShipParam (null, dData);
        }
    }

    public void SetVideoData(CollectionData oData)
    {
        this.m_iShipNum = 0;
        VideoData = new Dictionary<int, List<SShipParam>> ();
        VideoLength = new Dictionary<int, int> ();

        if (oData.d1 != null)
        {
            VideoData.Add (1, oData.d1);
            this.m_iShipNum += 1;
            this.m_VideoLength = oData.d1.Count;
            this.m_VideoTime = (int)(oData.d1 [oData.d1.Count - 1].time - oData.d1 [0].time);
            VideoLength.Add (1, oData.d1.Count);
        }
        if (oData.d2 != null)
        {
            VideoData.Add (2, oData.d2);
            this.m_iShipNum += 1;
            if (oData.d2.Count > this.m_VideoLength)
            {
                this.m_VideoLength = oData.d2.Count;
                this.m_VideoTime = (int)(oData.d2 [oData.d2.Count - 1].time - oData.d2 [0].time);
            }
            VideoLength.Add (2, oData.d2.Count);
        }
        if (oData.d3 != null)
        {
            VideoData.Add (3, oData.d3);
            this.m_iShipNum += 1;
            if (oData.d3.Count > this.m_VideoLength)
            {
                this.m_VideoLength = oData.d3.Count;
                this.m_VideoTime = (int)(oData.d2 [oData.d2.Count - 1].time - oData.d2 [0].time);
            }
            VideoLength.Add (3, oData.d3.Count);
        }

        ShipManager.Instance.CreateNewVideoInstance (GlobalManager.Instance.GetInstanceID (), this.m_iShipNum);
    }

    public void Clear()
    {
        //m_fValue = 0f;
        m_CurrentIndex = 0;
        m_VideoLength = 0;
        m_VideoTime = 0;
        m_iShipNum = 0;
        m_fTime = 0f;

		if(VideoData!=null)
        	VideoData.Clear ();
		if(VideoLength!=null)
			VideoLength.Clear ();
    }

    public float GetCurrentProgress()
    {
        return (float)this.m_CurrentIndex / this.m_VideoLength;
    }

    public void SetCurrentIndex(float fProgress)
    {
        this.m_CurrentIndex = (int)(this.m_VideoLength * fProgress);
        SignalManager.Instance.DispatchSignal (SignalID.SetTime, null, this.m_VideoTime * fProgress);
    }
}

