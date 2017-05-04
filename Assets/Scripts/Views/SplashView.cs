using UnityEngine;
using System.Collections;

public class SplashView : MonoBehaviour {

	private float time = 0f;

	void Start ()
	{
	}

	void Update()
	{
		time += Time.fixedDeltaTime;

		if(time > 2)
		{
            switch(GameManager.Instance.currentScene)
            {
            case 1:
                GameManager.Instance.loadScene (SceneType.Scene1);
                break;
            case 2:
                GameManager.Instance.loadScene (SceneType.Scene2);
                break;
            case 3:
                GameManager.Instance.loadScene (SceneType.Scene3);
                break;
            default:
                break;
            }


		}
	}

}
