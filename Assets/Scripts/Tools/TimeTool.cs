using UnityEngine;
using System.Collections;
using System;

public class TimeTool
{

    //转换时间为Unix时间戳
	public static long ConvertDateTimeToUnix(DateTime time)  
    {          
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));          
        long t = (time.Ticks - startTime.Ticks) / 1000000;   //除10000调整为10位      
        return t;  
    }  

    //转换Unix时间戳为时间
    public static DateTime ConvertUnixToDateTime(long timeStamp)        
    {            
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));            
        long lTime = long.Parse(timeStamp.ToString() + "0000000");            
        TimeSpan toNow = new TimeSpan(lTime); 
        return dtStart.Add(toNow);        
    } 

}

