using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicInfoItem
{
    public string Title;
    public string Artist;
    public int BPM;
    public int Time;

    public MusicInfoItem(string title, string artist, int bpm, int time)
    {
        Title = title;
        Artist = artist;
        BPM = bpm;
        Time = time;
    }

}

public class DodgePoint
{
    public int Index = -1;

    public DodgePoint(int index)
    {
        Index = index;
    }
}

public class OutObstacle
{
    public int Index = -1;

    public OutObstacle(int index)
    {
        Index = index;
    }
}

public class InObstacle
{
    public int Index = -1;

    public InObstacle(int index)
    {
        Index = index;
    }
}

public class ChartingItem
{
    public int Beat;                     // 채보프로그램의 한줄(4/4박이면 4마디가 한줄)을 기준 
    public string DodgePoint;            // 정박 스폰위치
    public string OutObstacle;           // 바깥쪽 가시 스폰위치
    public string InObstacle;            // 안쪽 가시 스폰 위치
    public int SavePoint;                // 세이브 포인트
    public float Speed;                  // Beat당 공이 몇도 이동하는지
    public float SpeedTime;              // 바뀐 Speed로 도달하는 시간
    public float BallAngle;              // 강제로 볼의 각도를 변경
    public int IsKnockback;              // 넛백 기믹 활성화
    public float KnockBackAngle;         // 넛백 기믹 활성화시 넛백 각도

    public List<DodgePoint> DodgePointElements = new List<DodgePoint>();        // 정박 스폰위치
    public List<OutObstacle> OutObstacleElements = new List<OutObstacle>();     // 바깥쪽 가시 스폰위치
    public List<InObstacle> InObstacleElements = new List<InObstacle>();        // 안쪽 가시 스폰 위치

    public ChartingItem(int beat, string correctBeat, string outObstacle, string inObstacle, int savePoint, float speed, float speedTime, float ballAngle, int isKnockBack, float knockBackAngle)
    {
        // Beat
        Beat = beat;
        DodgePoint = correctBeat;
        OutObstacle = outObstacle;
        InObstacle = inObstacle;

        // Dodge Point Elements
        string[] correctBeats = correctBeat.Split('|');

        for (int i = 0; i < correctBeats.Length; i++)
        {
            int idx = Convert.ToInt32(correctBeats[i]);
            DodgePointElements.Add(new DodgePoint(idx));
        }

        // Out Obstacle Elements
        string[] outObstacles = outObstacle.Split('|');

        for (int i = 0; i < outObstacles.Length; i++)
        {
            int idx = Convert.ToInt32(outObstacles[i]);
            OutObstacleElements.Add(new OutObstacle(idx));
        }

        // In Obstacle Elements
        string[] inObstacles = inObstacle.Split('|');

        for (int i = 0; i < inObstacles.Length; i++)
        {
            int idx = Convert.ToInt32(inObstacles[i]);
            InObstacleElements.Add(new InObstacle(idx));
        }

        SavePoint = savePoint;
        Speed = speed;
        SpeedTime = speedTime;
        BallAngle = ballAngle;
        IsKnockback = isKnockBack;
        KnockBackAngle = knockBackAngle;
    }
}

public class ChartingList : List<ChartingItem>
{

}

public class BMWReader : CsvReader
{
    private MusicInfoItem _musicInfoItem;
    private List<ChartingItem> _chartingItem = new List<ChartingItem>();

    public MusicInfoItem MusicInfoItem { get { return _musicInfoItem; } }
    public List<ChartingItem> ChartingItem { get { return _chartingItem; } }

    enum CSVBLOCK { Unkown, MusicInfo, Charting }
    CSVBLOCK parseBlock = CSVBLOCK.Unkown;

    protected override void Init()
    {
        base.Init();
    }

    protected override void GetItem(string line, int lineNumber)
    {
        string[] args = null;

        line = line.Trim();

        try
        {
            if (line[0] == '#')
            {
                if (line.Contains("#MUSICINFO"))
                {
                    parseBlock = CSVBLOCK.MusicInfo;
                }
                else if (line.Contains("#CHARTING"))
                {
                    parseBlock = CSVBLOCK.Charting;
                }
            }
            else
            {
                //parsed element
                args = line.Split(',');
                int count = 0;

                switch (parseBlock)
                {
                    case CSVBLOCK.MusicInfo:
                        count = 0;

                        _musicInfoItem = new MusicInfoItem(
                            args[count++].Trim(),
                            args[count++].Trim(),
                            Convert.ToInt32(args[count++].Trim()),
                            Convert.ToInt32(args[count++].Trim())                            
                            );

                        // 작곡가 에 ^ 들어갈시 ,로 변경
                        if (_musicInfoItem.Artist.Contains("^"))
                        {
                            _musicInfoItem.Artist = _musicInfoItem.Artist.Replace("^", ",");
                        }

                        Debug.Log($"{_musicInfoItem.Title},{_musicInfoItem.Artist},{_musicInfoItem.BPM},{_musicInfoItem.Time}");
                        break;
                    case CSVBLOCK.Charting:
                        count = 0;
                        try
                        {
                            var itemCharting = new ChartingItem(
                                Convert.ToInt32(args[count++].Trim()),        // 채보프로그램의 한줄(4/4박이면 4마디가 한줄)을 기준
                                args[count++].Trim(),                         // 정박 스폰위치
                                args[count++].Trim(),                         // 바깥쪽 가시 스폰위치
                                args[count++].Trim(),                         // 안쪽 가시 스폰 위치
                                Convert.ToInt32(args[count++].Trim()),        // 세이브 포인트
                                float.Parse(args[count++].Trim()),            // Beat당 공이 몇도 이동하는지
                                float.Parse(args[count++].Trim()),            // 바뀐 Speed로 도달하는 시간
                                float.Parse(args[count++].Trim()),            // 강제로 볼의 각도를 변경
                                Convert.ToInt32(args[count++].Trim()),        // 넛백 기믹 활성화
                                float.Parse(args[count++].Trim())             // 넛백 기믹 활성화시 넛백 각도
                                );

                            _chartingItem.Add(itemCharting);
                            Debug.Log($"line : {itemCharting.Beat},{itemCharting.DodgePoint},{itemCharting.OutObstacle},{itemCharting.InObstacle},{itemCharting.SavePoint},{itemCharting.Speed},{itemCharting.SpeedTime},{itemCharting.BallAngle},{itemCharting.IsKnockback},{itemCharting.KnockBackAngle}" );
                        }
                        catch (Exception e)
                        {
                            Debug.unityLogger.LogException(e);
                            Debug.LogError("Charting BMSReader :" + line + ",line number" + lineNumber);
                        }
                        break;
                    default:
                        break;
                }
            }

        }
        catch(Exception e)
        {
            Debug.unityLogger.LogException(e);
            Debug.LogError($"Exception : {FileName} : {lineNumber} : {line}");
        }
    }
}
