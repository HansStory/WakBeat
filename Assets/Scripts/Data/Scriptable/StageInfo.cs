using UnityEngine;

[CreateAssetMenu(fileName = "SetupStageInfo", menuName = "ScriptableObjects/SetupStageInfo", order = 4)]
public class StageInfo : ScriptableObject
{
    [Header("[ Skill GameObjects ]")]
    public GameObject[] VFXs;

    [Header("[ Ball Skins ]")]
    public Sprite[] BallSkins;

    [Header("[ Circle Skins ]")]
    public Sprite[] CircleSkins;

    [Header("[ Circle Skins ]")]
    public Sprite[] BackGroundSkins;

    [Header("[ Obstacle Skins ]")]
    public Sprite[] ObstacleSkins;


}