using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameView : MonoBehaviour {

    public GameObject pf_ChooseShipToggle;
    public GameObject ChooPos;
    public Vector3 m_ChooBtnStartPos = new Vector3(30, -30 ,0);
    public Button Btn_Setting;
    public GameObject SettingPanel;
	public Button Btn_Video;
	public GameObject GO_Video;

    private float m_ChooBtnWidth = 50;
    private GameModel m_Model;
    private Vector3 m_ChooBtnPos;
	private List<GameObject> m_ControlToggleList = new List<GameObject> ();

    void Awake()
    {
        this.gameObject.AddComponent<GameModel> ();
    }

	// Use this for initialization
	void Start () 
	{
		this.m_Model = this.gameObject.GetComponent<GameModel> ();
        this.m_ChooBtnWidth = this.pf_ChooseShipToggle.GetComponent<RectTransform> ().sizeDelta.x;

        Btn_Setting.onClick.AddListener (delegate {
            ShowSettingPanel ();
        });

		Btn_Video.onClick.AddListener (delegate {
			if (GO_Video.activeSelf) {
				GO_Video.SetActive (false);
			} else {
				GO_Video.SetActive (true);
			}
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
        if(this.m_ControlToggleList.Count > 0)
        {
            foreach(var oButton in m_ControlToggleList)
            {
                Destroy (oButton);
            }
            this.m_ControlToggleList.Clear ();
        }
        foreach (var item in GlobalManager.Instance.GetInstanceData().shape) 
        {
            string sShipID = item.Key;
			GameObject oToggle = Instantiate (this.pf_ChooseShipToggle) as GameObject;
			oToggle.name = "Tg_" + sShipID;
			oToggle.transform.SetParent(ChooPos.transform);
            oToggle.transform.localPosition = this.m_ChooBtnPos;
			Toggle compToggle = oToggle.GetComponent<Toggle> ();
			compToggle.group = ChooPos.GetComponent<ToggleGroup> ();
			if(sShipID == "1")
			{
				compToggle.isOn = true;
			}
			else
			{
				compToggle.isOn = false;
			}
			compToggle.onValueChanged.AddListener(delegate {
				if(compToggle.isOn)
				{
					this.m_Model.ChooseShip (int.Parse (sShipID));	
				}
            });
            oToggle.transform.Find ("Label").gameObject.GetComponent<Text> ().text = sShipID;
            this.m_ControlToggleList.Add (oToggle);
            this.m_ChooBtnPos += new Vector3 (this.m_ChooBtnWidth + this.m_ChooBtnStartPos.x, 0, 0);
        }
    }

    private void ShowSettingPanel()
    {
        SettingPanel.SetActive (true);
    }


}
