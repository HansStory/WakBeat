using System;
using System.IO;

public abstract class CsvReader
{
    private string _fileName = string.Empty;

    public string FileName { get { return _fileName; } }

    protected bool _wantScriptReadEnd = false;
    protected virtual void Init()
    {

    }

    public bool ReadFile(string fileName_)
    {
        Init();

        _fileName = fileName_;

        try
        {
            int lineNumber = 1;
            string line = string.Empty;

            StreamReader theReader = new StreamReader(fileName_);

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
        ReadFile(_fileName);
    }


}
