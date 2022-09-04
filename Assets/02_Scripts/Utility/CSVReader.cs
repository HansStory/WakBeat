using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    //static string CHARACTER = "Characters";

    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }

    //public static Dictionary<string, T> ReadOrder<T>(string version, T[] dataArr = null) where T : struct
    //{
    //    var returnDictionary = new Dictionary<string, T>();
    //    TextAsset data = Resources.Load(CHARACTER) as TextAsset;

    //    //행들의 값을 string[]으로 변환
    //    var lines = Regex.Split(data.text, LINE_SPLIT_RE);

    //    //행이 1이하면, 키 값만 있거나 아예 빈 파일이므로 return
    //    if (lines.Length <= 1)
    //    {
    //        return returnDictionary;
    //    }

    //    //버전 목록을 배열로 나눈 뒤, 인덱스 값을 구한다.
    //    string[] header = Regex.Split(lines[0], SPLIT_RE);
    //    int versionIndex = string.IsNullOrEmpty(version) ? header.Length - 1 : Array.FindIndex(header, str => str == version);

    //    for (var i = 1; i < lines.Length; i++)
    //    {
    //        var values = Regex.Split(lines[i], SPLIT_RE);
    //        if (values.Length == 0 || string.IsNullOrEmpty(values[versionIndex]))
    //        {
    //            break;
    //        }
    //        else
    //        {
    //            returnDictionary.Add(values[versionIndex], dataArr == null ? new T() : dataArr[i - 1]);
    //        }
    //    }

    //    return returnDictionary;
    //}
}
