using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InsItem : MonoBehaviour {

	public Text T_Time;
    public Text T_Name;
    public Text T_Desp;
    public Text T_Amount;

    public void SetItemView(long lTime, string sName, string sDesp, int iAmount)
    {
        this.T_Time.text = TimeTool.ConvertUnixToDateTime (lTime).ToString ();
        this.T_Name.text = sName;
        this.T_Desp.text = sDesp;
        this.T_Amount.text = iAmount.ToString ();
    }
}
