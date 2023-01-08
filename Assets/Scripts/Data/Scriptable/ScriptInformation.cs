using UnityEngine;

[CreateAssetMenu(fileName = "SetupInformation", menuName = "ScriptableObjects/ScriptInformation", order = 1)]

public class ScriptInformation : ScriptableObject
{
    public string Version = "0.1.0";

    public string ProgramName = "Orbit or Wakbeat";
    public string Organiation = "느그게임즈";
}
