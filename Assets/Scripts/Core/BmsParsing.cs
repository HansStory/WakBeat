using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public struct BMS
{
    public BMS(string title, string artist, int bpm, int time, List<Charting> chartings)
    {
        this.title = title;
        this.artist = artist;
        this.bpm = bpm;
        this.time = time;
        Chartings = chartings;
    }

    public string title;
    public string artist;
    public int bpm;
    public int time;
    public List<Charting> Chartings;
}

public struct Charting
{
    public int beat;
    public string upPrickle;
    public string downPrickle;
    public int savePoint;
    public int speed;
    public int speedTime;
    public double playerAngle;
    public int isKnockback;
    public double knockBackAngle;
}

public class BmsParsing : MonoBehaviour
{
    private BMS bms;

    private Charting charting;

    private void Awake()
    {
        bms = new BMS();
        charting = new Charting();
        bms.Chartings = new List<Charting>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Dictionary<string, object> ParsingBms(string fileName)
    {
        var dict = new Dictionary<string, object>();
        StreamReader sr = new StreamReader(Application.dataPath + "/BMS/" + fileName + ".csv");
        string datas = sr.ReadToEnd();
        
        string[] data = datas.Split("\r\n");
        string[] sp;
        bool isHeaderParsing = false;
        bool isChartingParsing = false;
        sr.Close();
        foreach (var d in data)
        {
            if (d.IndexOf("#MUSICINFO") == 0)
            {
                isHeaderParsing = true;
            }
            else if ((d.IndexOf(";") == -1) && isHeaderParsing)
            {
               sp = d.Split(",");
               bms.title = sp[0];
               bms.artist = sp[1];
               bms.bpm = Convert.ToInt16(sp[2]);
               bms.time = Convert.ToInt16(sp[3]);
               isHeaderParsing = false;
            }
            else if (d.IndexOf("#CHARTING") == 0)
            {
                isChartingParsing = true;
            }
            else if ((d.IndexOf(";") == -1) && isChartingParsing)
            {
                sp = d.Split(",");
                charting.beat = Convert.ToInt16(sp[0]);
                charting.upPrickle = sp[1];
                charting.downPrickle = sp[2];
                charting.savePoint = Convert.ToInt16(sp[3]);
                charting.speed = Convert.ToInt16(sp[4]);
                charting.speedTime = Convert.ToInt16(sp[5]);
                charting.playerAngle = Convert.ToDouble(sp[6]);
                charting.isKnockback = Convert.ToInt16(sp[7]);
                charting.knockBackAngle = Convert.ToDouble(sp[8]);
                
                // bms.Chartings.Add(charting);
                
                bms.Chartings.Add(charting);
            }
            
        }
        dict.Add(bms.title, bms);
        return dict;
    }
}
