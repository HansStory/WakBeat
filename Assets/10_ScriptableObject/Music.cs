using UnityEngine;

[CreateAssetMenu(fileName = "Music", menuName = "Music/Music")]
public class Music : ScriptableObject
{
    [SerializeField]
    protected Sprite m_background;
    [SerializeField]
    protected Sprite m_circle;
    [SerializeField]
    protected Sprite m_title;

    public Sprite BackGround => m_background;
    public Sprite Icon => m_circle;
    public Sprite Title => m_title;
}
