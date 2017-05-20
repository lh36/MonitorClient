using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlView : MonoBehaviour
{
	
	public Button Btn_Start;
	public Button Btn_Finish;
	public Button Btn_Front;
	public Button Btn_Back;
	public Button Btn_Left;
	public Button Btn_Right;
	public Button Btn_Stop;
	public Button Btn_Command;
	public InputField IF_Command;

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
	}

	private string GetCommandData()
	{
		string sData = IF_Command.text;
		return sData;
	}



}

