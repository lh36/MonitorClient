using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameView : MonoBehaviour {

    public GameObject pf_ChooseShipButton;
    public GameObject ChooPos;
    public Vector3 m_ChooBtnStartPos = new Vector3(30, -30 ,0);
    public Button Btn_Setting;
    public GameObject SettingPanel;

    private float m_ChooBtnWidth = 70;
    private GameModel m_Model;
    private Vector3 m_ChooBtnPos;
    private List<GameObject> m_ControlButtonList = new List<GameObject> ();

    void Awake()
    {
        this.gameObject.AddComponent<GameModel> ();
    }

	// Use this for initialization
	void Start () 
	{
		this.m_Model = this.gameObject.GetComponent<GameModel> ();
        this.m_ChooBtnWidth = this.pf_ChooseShipButton.GetComponent<RectTransform> ().sizeDelta.x;

        Btn_Setting.onClick.AddListener (delegate {
            ShowSettingPanel ();
        });
	}

    void OnEnable()
    {
        SetChooseShipButton ();
    }
	
	// Update is called once per frame
	void Update () 
	{
	
	}

    public void SetChooseShipButton()
    {
        this.m_ChooBtnPos = this.m_ChooBtnStartPos;
        if(this.m_ControlButtonList.Count > 0)
        {
            foreach(var oButton in m_ControlButtonList)
            {
                Destroy (oButton);
            }
            this.m_ControlButtonList.Clear ();
        }
        foreach (var item in GlobalManager.Instance.GetInstanceData().shape) 
        {
            string sShipID = item.Key;
            GameObject oButton = Instantiate (this.pf_ChooseShipButton) as GameObject;
			oButton.transform.SetParent(ChooPos.transform);
            oButton.transform.localPosition = this.m_ChooBtnPos;
            oButton.GetComponent<Button> ().onClick.AddListener (delegate {
                this.m_Model.ChooseShip (int.Parse (sShipID));
            });
            oButton.transform.Find ("Text").gameObject.GetComponent<Text> ().text = sShipID;
            this.m_ControlButtonList.Add (oButton);
            this.m_ChooBtnPos += new Vector3 (this.m_ChooBtnWidth + this.m_ChooBtnStartPos.x, 0, 0);
        }
    }

    private void ShowSettingPanel()
    {
        SettingPanel.SetActive (true);
    }


}
