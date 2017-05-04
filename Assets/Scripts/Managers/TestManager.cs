using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestManager : SingletonUnity<TestManager>
{
    private int i_ShipNumber = 0;
    private List<ShipController> ls_ShipControllers = new List<ShipController> ();

    private GameObject pf_Ship = null;

    void Start()
    {
        pf_Ship = Resources.Load<GameObject> ("World/Ship/Ship") as GameObject;

    }



}

