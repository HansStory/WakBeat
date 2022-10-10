using UnityEngine;

[CreateAssetMenu(menuName = "CustomAnimCurve", fileName = "AnimCurve_")]
public class AnimCurve : ScriptableObject
{
    [SerializeField]
    private AnimationCurve animCurve;

    public AnimationCurve Curve => animCurve;
}
