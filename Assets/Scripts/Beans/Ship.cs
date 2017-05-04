using System.Collections;

public class Ship {
    //船舶参数
    private SShipParam param;
    private ShipController controller;

    #region 索引器
    public SShipParam Param
    {
        get{return param;}
        set
        {
            param = value;
            controller.SetShipStatus ();
        }
    }
    public float Lon {
        get{ return param.lon; }
        set{ param.lon = value; }
    }

    public float Lat {
        get{ return param.lat; }
        set{ param.lat = value; }
    }

    public float PosX {
        get{ return param.posX; }
        set{ param.posX = value; }
    }

    public float PosY {
        get{ return param.posY; }
        set{ param.posY = value; }
    }

    public float RudAng {
        get{ return param.rudAng; }
        set{ param.rudAng = value; }
    }

    public float TraAng {
        get{ return param.traAng; }
        set{ param.traAng = value; }
    }

    public float Speed {
        get{ return param.speed; }
        set{ param.speed = value; }
    }

    public int Gear {
        get{ return param.gear; }
        set{ param.gear = value; }
    }

    public long Time {
        get{ return param.time; }
        set{ param.time = value; }
    }
    #endregion

    public Ship(ShipController controller)
    {
        this.controller = controller;
    }

}
