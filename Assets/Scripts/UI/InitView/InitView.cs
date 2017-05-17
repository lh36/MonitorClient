using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InitView : MonoBehaviour
{
    private InitModel m_Model;

    private Button Btn_Start;
    private Button Btn_Quit;

    void Awake()
    {
        this.m_Model = new InitModel ();

        Btn_Start = transform.Find ("Btn_Start").gameObject.GetComponent<Button> ();
        Btn_Quit = transform.Find ("Btn_Quit").gameObject.GetComponent<Button> ();
    }

    void Start()
    {

        Btn_Start.onClick.AddListener (delegate {
            OnStartClick ();
        });
        Btn_Quit.onClick.AddListener (delegate {
            OnQuitClick ();
        });
    }

    private void OnStartClick()
    {
        this.m_Model.OnStartClicked ();
    }

    private void OnQuitClick()
    {
        Application.Quit ();
    }

}

