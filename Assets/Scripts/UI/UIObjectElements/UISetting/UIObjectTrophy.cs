using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObjectTrophy : MonoBehaviour
{
    // 팝업창 호출 시 UI 제어
    private float _duration = 0.15f;

    // 팝업 창 호출 시 UI 출력 제어
    private void OnEnable()
    {
        // 사이즈 0부터 시작
        this.transform.localScale = Vector3.zero;
        // 1까지 커지면서 시간은 0.2초, 변환 시 큐빅 형태로 등장
        this.transform.DOScale(Vector3.one, _duration).SetEase(Ease.InCubic);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClickHide()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}
