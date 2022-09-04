using UnityEngine;

[CreateAssetMenu(menuName = "AnimCurve", fileName = "AnimCurve_")]
public class AnimCurve : ScriptableObject
{
    [SerializeField]
    private AnimationCurve m_animCurve;

    public AnimationCurve Curve => m_animCurve;
}
