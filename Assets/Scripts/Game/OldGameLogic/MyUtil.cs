using UnityEngine;

public static class MyUtil
{
    public static Color whiteClear = new Color(1, 1, 1, 0);
    public static Color BoundaryColor = new Color(0, 0, 0, 0.5f);
    public static Vector2 MusicCircle_SmallScale = new Vector2(0.7f, 0.7f);

    public static float CalcLerpTime(float lerpTime, float speed = (float)BUTTONSPEED.ANIMATION)
    {
        lerpTime += Time.unscaledDeltaTime * speed;
        return lerpTime = Mathf.Clamp01(lerpTime);
    }

    public static int RepeatIndex(int value, int repeat)
    {
        if(value < 0)
        {
            value = repeat - 1;
        }
        else if(value >= repeat)
        {
            value %= repeat;
        }

        return value;
    }

    public static float RepeatRange(float value, float range)
    {
        if(value <= -range)
        {
            value += range * 2;
        }
        else if(value >= range)
        {
            value -= range * 2;
        }

        return value;
    }

    public static float GetRadAngle(Vector2 pos)
    {
        return Mathf.Atan2(pos.y, pos.x);
    }

    public static Vector2 ConvertRadAngleToVec2(float radAngle)
    {
        return new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle));
    }

    public static Vector2 ConvertAngleToVec2(float _deg)
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    public static float CalcRadius(Sprite sprite, Transform tr)
    {
        return sprite.rect.width / sprite.pixelsPerUnit * tr.localScale.x / 2;
    }
}
