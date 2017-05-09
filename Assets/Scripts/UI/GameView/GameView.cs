using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameView : MonoBehaviour {

    public GameObject pf_ChooseShipButton;
    public GameObject ChooPos;
    public Vector3 m_ChooBtnPos = new Vector3(30, -30 ,0);

    private float m_ChooBtnWidth = 70;
    private GameModel m_Model;

    void Awake()
    {
        this.gameObject.AddComponent<GameModel> ();
        this.m_Model = this.gameObject.GetComponent<GameModel> ();
    }

	// Use this for initialization
	void Start () {
        this.m_ChooBtnWidth = this.pf_ChooseShipButton.GetComponent<RectTransform> ().sizeDelta.x;
        SetChooseShipButton ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetChooseShipButton()
    {
        foreach (var item in GlobalManager.Instance.GetInstanceData().shape) 
        {
            string sShipID = item.Key;
            GameObject oButton = Instantiate (this.pf_ChooseShipButton) as GameObject;
            oButton.transform.parent = ChooPos.transform;
            oButton.transform.localPosition = this.m_ChooBtnPos;
            oButton.GetComponent<Button> ().onClick.AddListener (delegate {
                this.m_Model.ChooseShip (int.Parse (sShipID));
            });
            oButton.transform.Find ("Text").gameObject.GetComponent<Text> ().text = sShipID;
            this.m_ChooBtnPos += new Vector3 (this.m_ChooBtnPos.x + this.m_ChooBtnWidth, 0, 0);
        }
    }


}
