using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SetupInformation", menuName = "ScriptableObjects/ScriptInformation", order = 1)]

[System.Serializable]
public class Participations
{
    public string Role;
    public string[] NickName;
}

public class ScriptInformation : ScriptableObject
{
    public string Version = "0.1.0";

    public string ProgramName = "Orbit or Wakbeat";
    public string Organiation = "느그게임즈";

    public List<Participations> Participations;
}
