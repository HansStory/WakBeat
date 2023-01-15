using System;
using System.IO;
using UnityEngine;

public abstract class CsvReader
{
    private string _path = string.Empty;

    public string Path { get { return _path; } }

    protected bool _wantScriptReadEnd = false;
    protected virtual void Init()
    {

    }

    public bool ReadFile(string directory, string fileName)
    {
        Init();
        _path = directory + "/" + fileName;

#if UNITY_ANDROID
        WWW wwwfile = new WWW(_path);
        while (!wwwfile.isDone) { }

        var filepath = Application.persistentDataPath + "/" + fileName;
        File.WriteAllBytes(filepath, wwwfile.bytes);

        _path = filepath;
#else
        //_fileName = path;
#endif

        //_fileName = path;

        try
        {
            int lineNumber = 1;
            string line = string.Empty;

            StreamReader theReader = new StreamReader(_path);

            _wantScriptReadEnd = false;
            using (theReader)
            {
                do
                {
                    line = theReader.ReadLine().Trim();

                    if (!string.IsNullOrEmpty(line) && line.Length != 0 && line[0] != ';')
                    {
                        GetItem(line, lineNumber);
                    }

                    lineNumber++;
                }
                while (line != null && !_wantScriptReadEnd);

                theReader.Close();
                return true;
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("{0}\n", e.Message);
            return false;
        }
    }

    protected abstract void GetItem(string line, int lineNumber);

    public void Refresh()
    {
        //ReadFile(_path);
    }


}
