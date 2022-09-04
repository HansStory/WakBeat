using UnityEngine;
using UnityEngine.UI;

public class SettingCtrl : MonoBehaviour
{
    [SerializeField]
    private NewSlider m_SFXSlider;
    [SerializeField]
    private NewSlider m_BGMSlider;
    [SerializeField]
    private Image m_combineImage;
    [SerializeField]
    private Image m_divideImage;

    private Sprite[] m_enableSprites;
    private Sprite[] m_disableSprites;

    private void Awake()
    {
        LoadSprites();
    }

    private void LoadSprites()
    {
        m_enableSprites = Resources.LoadAll<Sprite>(ResourcePath.SettingOnSprites);
        m_disableSprites = Resources.LoadAll<Sprite>(ResourcePath.SettingOffSprites);
    }

    public void Init()
    {
        m_SFXSlider.SetSliderValue(DataManager.Instance.SoundFX);
        m_BGMSlider.SetSliderValue(DataManager.Instance.Music);

        SetKeyTypeImages();
    }

    private void SetKeyTypeImages()
    {
        m_combineImage.sprite = DataManager.Instance.KeyType == KEYTYPE.COMBINE
            ? m_enableSprites[(int)KEYTYPE.COMBINE] : m_disableSprites[(int)KEYTYPE.COMBINE];
        m_divideImage.sprite = DataManager.Instance.KeyType == KEYTYPE.DIVIDE
            ? m_enableSprites[(int)KEYTYPE.DIVIDE] : m_disableSprites[(int)KEYTYPE.DIVIDE];
    }

    public void SetSFXVolume(float volume) => SoundManager.Instance.CtrlSFXVolume(volume);
    public void CheckSFXVolume() => SoundManager.Instance.PlaySoundFX();
    public void SetBGMVolume(float volume) => SoundManager.Instance.CtrlBGMVolume(volume);

    public void SelectKeyType(Keytype type)
    {
        DataManager.Instance.KeyType = type.Arg1;
        SetKeyTypeImages();
    }
}
