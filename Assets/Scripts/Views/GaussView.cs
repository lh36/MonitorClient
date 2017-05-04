using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GaussView : MonoBehaviour
{

    private int mScreenx = 0;
    private int mScreeny = 0;

    void Start () {
        mScreenx = Screen.width;
        mScreeny = Screen.height;
        GetComponent<Image>().material = Resources.Load<Material>("Materials/UIGauss.mat");
        RefreshBackground();
    }

    void OnEnable(){
        RefreshBackground();
    }

    void RefreshBackground(){
        if(mScreenx <= 0){ 
            //第一次OnEnable时，没有准备好。
            return;
        }

        RenderTexture rt = new RenderTexture(mScreenx/32, mScreeny/32, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        Camera.main.targetTexture = rt;
        Camera.main.Render();

        RenderTexture.active = rt;
        Texture2D screenShort = new Texture2D(mScreenx/32, mScreeny/32, TextureFormat.ARGB32, false);
        screenShort.ReadPixels(new Rect(0, 0, mScreenx/32, mScreeny/32), 0, 0, false);
        screenShort.wrapMode = TextureWrapMode.Clamp;//不能用Repeat，否则边缘颜色会相互渗透。
        screenShort.Apply();
        Camera.main.targetTexture  = null;
        RenderTexture.active = null;
        Destroy(rt);

        Sprite sp = Sprite.Create(screenShort, new Rect(0, 0, mScreenx/32, mScreeny/32), Vector2.zero);
        GetComponent<Image>().sprite = sp;
    }



}

