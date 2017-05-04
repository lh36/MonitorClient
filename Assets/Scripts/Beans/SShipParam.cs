using System;

public struct SShipParam
{
    public float lat;//经度
    public float lon;//纬度
    public float posX;//X坐标
    public float posY;//Y坐标
    public float rudAng;//舵角
    public float traAng;//航迹角
    public float speed;//船速
    public int gear;//船速等级
    public long time;//运行时间

    public SShipParam(float lat, float lon, float posX, float posY, 
        float rudAng, float traAng, float speed, int gear, long time)
    {
        this.lat = lat;
        this.lon = lon;
        this.posX = posX;
        this.posY = posY;
        this.rudAng = rudAng;
        this.traAng = traAng;
        this.speed = speed;
        this.gear = gear;
        this.time = time;
    }

}

