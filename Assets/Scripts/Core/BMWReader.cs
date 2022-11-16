using System;
using System.Collections.Generic;
using UnityEngine;


public class MusicInfoItem
{
    public string Title;    // 곡명
    public string Artist;   // 작곡가
    public int BPM;         // BPM
    public int Bar;         // Bar (한마디 == 한칸)
    public int Time;        // 곡의 시간

    public MusicInfoItem(string title, string artist, int bpm, int bar, int time)
    {
        Title = title;
        Artist = artist;
        BPM = bpm;
        Bar = bar;
        Time = time;
    }

}

public class AnimationItem
{
    public int Index;
    public string StageName;
    public string AnimationName;

    public AnimationItem(int index, string stageName, string animationName)
    {
        Index = index;
        StageName = stageName;
        AnimationName = animationName;
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
    public int AnimationIndex;           // 애니메이션의 인덱스
    public float Interval;               // 시간적인 간격 조정(Delay)
    public string[] DodgePoints;         // 정박 스폰위치
    public string[] OutObstacles;        // 바깥쪽 가시 스폰위치
    public string[] InObstacles;         // 안쪽 가시 스폰 위치
    public int SavePoint;                // 세이브 포인트
    public float Speed;                  // Beat당 공이 몇도 이동하는지
    public float SpeedTime;              // 바뀐 Speed로 도달하는 시간
    public float BallAngle;              // 강제로 볼의 각도를 변경
    public int IsKnockback;              // 넛백 기믹 활성화
    public float KnockBackAngle;         // 넛백 기믹 활성화시 넛백 각도

    // For Debuging
    public string DodgePoint;
    public string OutObstacle;
    public string InObstacle;

    public List<DodgePoint> DodgePointElements = new List<DodgePoint>();        // 정박 스폰위치
    public List<OutObstacle> OutObstacleElements = new List<OutObstacle>();     // 바깥쪽 가시 스폰위치
    public List<InObstacle> InObstacleElements = new List<InObstacle>();        // 안쪽 가시 스폰 위치

    public ChartingItem(int beat, int animationIndex, float interval, string dodgePoint, string outObstacle, string inObstacle, int savePoint, float speed, float speedTime, float ballAngle, int isKnockBack, float knockBackAngle)
    {
        // Beat
        Beat = beat;

        //AnimationIndex
        AnimationIndex = animationIndex;

        //Interval
        Interval = interval;

        // Dodge Point Elements
        DodgePoint = dodgePoint; // For Debuging

        string[] dodgePoints = dodgePoint.Split('|');
        DodgePoints = dodgePoints;

        for (int i = 0; i < dodgePoints.Length; i++)
        {
            int idx = Convert.ToInt32(dodgePoints[i]);
            DodgePointElements.Add(new DodgePoint(idx));
        }

        // Out Obstacle Elements
        OutObstacle = outObstacle; // For Debuging

        string[] outObstacles = outObstacle.Split('|');
        OutObstacles = outObstacles;

        for (int i = 0; i < outObstacles.Length; i++)
        {
            int idx = Convert.ToInt32(outObstacles[i]);
            OutObstacleElements.Add(new OutObstacle(idx));
        }

        // In Obstacle Elements
        InObstacle = inObstacle; // For Debuging

        string[] inObstacles = inObstacle.Split('|');
        InObstacles = inObstacles;

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
    private List<AnimationItem> _animationItem = new List<AnimationItem>();  // KD_Han : 필요시 추가하겠습니다.
    private List<ChartingItem> _chartingItem = new List<ChartingItem>();

    public MusicInfoItem MusicInfoItem { get { return _musicInfoItem; } }
    public List<AnimationItem> AnimationItem { get { return _animationItem; } }
    public List<ChartingItem> ChartingItem { get { return _chartingItem; } }

    enum CSVBLOCK { Unkown, MusicInfo, Animation, Charting }
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
                else if (line.Contains("#ANIMATION"))
                {
                    parseBlock = CSVBLOCK.Animation;
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
                    case CSVBLOCK.Animation:
                        count = 0;
                        try
                        {
                            var itemAnimation = new AnimationItem(
                                Convert.ToInt32(args[count++].Trim()),        // 채보프로그램의 한줄(4/4박이면 4마디가 한줄)을 기준
                                args[count++].Trim(),                         // 정박 스폰위치
                                args[count++].Trim()                          // 바깥쪽 가시 스폰위치
                                );

                            _animationItem.Add(itemAnimation);


                            Debug.Log($"Anim Items : {itemAnimation.Index},{itemAnimation.StageName},{itemAnimation.AnimationName}");
                        }
                        catch (Exception e)
                        {
                            Debug.unityLogger.LogException(e);
                            Debug.LogError($"Animation BMWReader :" + line + ",line number" + lineNumber);
                        }
                        break;
                    case CSVBLOCK.Charting:
                        count = 0;
                        try
                        {
                            var itemCharting = new ChartingItem(
                                Convert.ToInt32(args[count++].Trim()),        // 채보프로그램의 한줄(4/4박이면 4마디가 한줄)을 기준
                                Convert.ToInt32(args[count++].Trim()),        // 사용할 애니메이션의 인덱스
                                float.Parse(args[count++].Trim()),            // 정박에서 시작하기위한 Interval
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

                            //_chartingItemList.Add(itemCharting);

                            Debug.Log($"line : {itemCharting.Beat},{itemCharting.AnimationIndex},{itemCharting.Interval},{itemCharting.DodgePoint},{itemCharting.OutObstacle},{itemCharting.InObstacle},{itemCharting.SavePoint},{itemCharting.Speed},{itemCharting.SpeedTime},{itemCharting.BallAngle},{itemCharting.IsKnockback},{itemCharting.KnockBackAngle}" );
                            
                            for (int i = 0; i < itemCharting.DodgePointElements.Count; i++)
                            {
                                Debug.Log($"{itemCharting.DodgePointElements[i].Index}");
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.unityLogger.LogException(e);
                            Debug.LogError($"Charting BMWReader :" + line + ",line number" + lineNumber);
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
