using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioSource m_audio;
    private Stack<float> m_timeList = new Stack<float>();
    public Stack<float> TimeList { get { return m_timeList; } }

    private void Awake()
    {
        m_audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        m_audio.Play();
    }

    public void SetAudioTime(float time)
    {
        m_audio.time = time;
    }
}
