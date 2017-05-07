using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InitView : MonoBehaviour
{
    private InitModel m_Model;

    private Button Btn_Start;

    void Awake()
    {
        this.m_Model = new InitModel ();

        Btn_Start = transform.Find ("Btn_Start").gameObject.GetComponent<Button> ();
    }

    void Start()
    {

        Btn_Start.onClick.AddListener (delegate {
            OnStartClick ();
        });
    }

    private void OnStartClick()
    {
        this.m_Model.OnStartClicked ();
    }

}

