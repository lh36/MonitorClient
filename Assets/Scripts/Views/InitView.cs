using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InitView : MonoBehaviour
{
    private Button Btn_Start;

    void Awake()
    {
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
        Debug.Log ("start");
    }

}

