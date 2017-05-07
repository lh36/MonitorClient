using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingView : MonoBehaviour
{

    private AsyncOperation asyn;

/*    private float time = 0f;
    private bool isLoading = false;*/

    void Update()
    {
		/*time += Time.fixedDeltaTime;

        if(time > 1 && !isLoading)
        {
            isLoading = true;
            StartCoroutine (loadScene ());
        }*/
    }


	void Start()
	{
		StartCoroutine (loadScene ());
	}

    IEnumerator loadScene()
    {
        yield return new WaitForEndOfFrame ();
        asyn = SceneManager.LoadSceneAsync (GlobalManager.Instance.DirScene);
        yield return asyn;
    }
}

