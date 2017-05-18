using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingView : MonoBehaviour
{

    public Button Btn_Quit;
    public Button Btn_Bg;

    // Use this for initialization
    void Start ()
    {
        Btn_Quit.onClick.AddListener (delegate {
            OnQuitClick ();
        });

        Btn_Bg.onClick.AddListener (delegate {
            OnCloseSettingClick();
        });
    }
    
    // Update is called once per frame
    void Update ()
    {
    
    }

    private void OnQuitClick()
    {
        this.gameObject.SetActive (false);
        GlobalManager.Instance.QuitInstance ();
    }

    private void OnCloseSettingClick()
    {
        this.gameObject.SetActive (false);
    }
}

