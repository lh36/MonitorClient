using System.Collections;

public class InitModel
{

    public InitModel()
    {
    }

    public void OnStartClicked()
    {
        UIManager.Instance.ShowViewByName (Constant.UI_Select);
    }

    public void OnVideoClicked()
    {
        UIManager.Instance.ShowViewByName (Constant.UI_Choose);
    }

}

