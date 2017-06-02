using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlView : MonoBehaviour
{
	public GameObject OpenControl;
	public Button Btn_Start;
	public Button Btn_Finish;
	public Button Btn_Front;
	public Button Btn_Back;
	public Button Btn_Left;
	public Button Btn_Right;
	public Button Btn_Stop;
	public Button Btn_Command;
	public InputField IF_Command;
    public Dropdown Dd_CloseControl;
    public Button Btn_OK;

	private GameModel m_Model;

	// Use this for initialization
	void Start ()
	{
		this.m_Model = this.gameObject.transform.parent.parent.gameObject.GetComponent<GameModel> ();

		Btn_Start.onClick.AddListener (delegate {
			this.m_Model.SubmitControl ("startlink");
		});
		Btn_Finish.onClick.AddListener (delegate {
			this.m_Model.SubmitControl ("closelink");
		});
		Btn_Front.onClick.AddListener (delegate {
			this.m_Model.SubmitControl ("o-" + this.m_Model.GetControlledShipID ().ToString () + "-w");
		});
		Btn_Back.onClick.AddListener (delegate {
			this.m_Model.SubmitControl ("o-" + this.m_Model.GetControlledShipID ().ToString () + "-s");
		});
		Btn_Left.onClick.AddListener (delegate {
			this.m_Model.SubmitControl ("o-" + this.m_Model.GetControlledShipID ().ToString () + "-a");
		});
		Btn_Right.onClick.AddListener (delegate {
			this.m_Model.SubmitControl ("o-" + this.m_Model.GetControlledShipID ().ToString () + "-d");
		});
		Btn_Stop.onClick.AddListener (delegate {
			this.m_Model.SubmitControl ("s");
		});

		Btn_Command.onClick.AddListener (delegate {
			this.m_Model.SubmitControl (GetCommandData());
		});

        Dd_CloseControl.onValueChanged.AddListener(delegate {
            
        })
	}

	private string GetCommandData()
	{
		string sData = IF_Command.text;
		return sData;
	}

    private void OnDropValueChanged()
    {
        var iValue = Dd_CloseControl.value;
        switch(iValue)
        {
        case 0:
            OpenControl.SetActive (true);
            this.m_Model.StartOpenControl ();
            break;
        case 1:
            OpenControl.SetActive (false);
            this.m_Model.StartPointControl ();
            break;
        case 2:
            this.m_Model.StartSpecialLineControl ();
            OpenControl.SetActive (false);
            break;
        case 3:
            this.m_Model.StartGenLineControl ();
            OpenControl.SetActive (false);
            break;
        case 4:
            this.m_Model.StartMulLineControl ();
            OpenControl.SetActive (false);
            break;
        case 5:
            this.m_Model.StartCircleControl ();
            OpenControl.SetActive (false);
            break;
        case 6:
            this.m_Model.StartFormationControl ();
            OpenControl.SetActive (false);
            break;
        default:
            break;
        }
    }

}

