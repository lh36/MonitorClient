using UnityEngine;
using System.Collections;

public class InputManager : SingletonUnity<InputManager>
{

    void FixedUpdate()
    {
        if(GameManager.Instance.IsGameRunning)
        {
            //input something
        }


    }

}

