using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : SingletonUnity<UIManager> {

	private GameObject rootView;
	
    public GameObject CurrentView;

    private GameObject GamePanel;
    private GameObject StartPanel;

	void Awake()
	{
		//Screen.SetResolution (1024, 768, false);
	}

	void Start()
	{
		rootView = GameObject.Find ("UI");
        GamePanel = rootView.transform.Find (Constant.UI_Game).gameObject;
        StartPanel = rootView.transform.Find (Constant.UI_Start).gameObject;

        InitUIPanel ();
        InitStart ();
	}

    public void InitStart()
    {
        GameManager.Instance.IsGameRunning = false;
        GamePanel.SetActive (false);
        StartPanel.SetActive (true);
    }

    public void InitUIPanel()
    {
        GamePanel.SetActive (true);
        StartPanel.SetActive (false);
        GameManager.Instance.IsGameRunning = true;
    }


}
