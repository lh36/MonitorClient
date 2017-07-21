using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChooseView : MonoBehaviour
{
    private ChooseModel m_Model;

    private InputField IF_Input;
    private Button Btn_Choose;
    private Button Btn_Quit;
    private Slider Sd_Progress;
    private GameObject Choose;
    private GameObject Load;

    void Awake()
    {

        Choose = transform.Find ("Choose").gameObject;
        Load = transform.Find ("Load").gameObject;
        Sd_Progress = Load.transform.Find ("Sd_Progress").gameObject.GetComponent<Slider> ();
        IF_Input = Choose.transform.Find ("IF_Input").gameObject.GetComponent<InputField> ();
        Btn_Choose = Choose.transform.Find ("Btn_Choose").gameObject.GetComponent<Button> ();
        Btn_Quit = Choose.transform.Find ("Btn_Quit").gameObject.GetComponent<Button> ();

        this.m_Model = gameObject.GetComponent<ChooseModel> ();
        this.m_Model.Init ();
    }

    void Start()
    {
        Btn_Choose.onClick.AddListener (delegate {
            OnChooseClick ();
        });
        Btn_Quit.onClick.AddListener (delegate {
            OnQuitClick ();
        });
    }

    void OnEnable()
    {
        Choose.SetActive (true);
        Load.SetActive (false);
    }

    void Update()
    {
        if(Load.activeSelf)
        {
            Sd_Progress.value = this.m_Model.GetProgress ();
        }
    }

    private void OnChooseClick()
    {
        this.m_Model.OnChooseClicked (int.Parse(IF_Input.text));
        Choose.SetActive (false);
        Load.SetActive (true);
    }

    private void OnQuitClick()
    {
        this.m_Model.OnBackClicked ();
    }

}

