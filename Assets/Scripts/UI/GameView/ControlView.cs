using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlView : MonoBehaviour
{

	public Button Btn_Front;
	public Button Btn_Back;
	public Button Btn_Left;
	public Button Btn_Right;
	public Button Btn_Stop;
	public Button Btn_Command;

	private GameModel m_Model;

	// Use this for initialization
	void Start ()
	{
		this.m_Model = this.gameObject.transform.parent.gameObject.GetComponent<GameModel> ();

		Btn_Front.onClick.AddListener (delegate {
			this.m_Model.SubmitControl ("w-" + this.m_Model.GetControlledShipID ().ToString ());
		});
		Btn_Back.onClick.AddListener (delegate {
			this.m_Model.SubmitControl ("s-" + this.m_Model.GetControlledShipID ().ToString ());
		});
		Btn_Left.onClick.AddListener (delegate {
			this.m_Model.SubmitControl ("a-" + this.m_Model.GetControlledShipID ().ToString ());
		});
		Btn_Right.onClick.AddListener (delegate {
			this.m_Model.SubmitControl ("d-" + this.m_Model.GetControlledShipID ().ToString ());
		});
		Btn_Stop.onClick.AddListener (delegate {
			this.m_Model.SubmitControl ("p-" + this.m_Model.GetControlledShipID ().ToString ());
		});
	}
	



}

