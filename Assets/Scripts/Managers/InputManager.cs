using UnityEngine;
using System.Collections;

public class InputManager : SingletonUnity<InputManager>
{

    void FixedUpdate()
    {
        if(GlobalManager.Instance.IsGameRunning)
        {
            //input something
        }


    }

}

