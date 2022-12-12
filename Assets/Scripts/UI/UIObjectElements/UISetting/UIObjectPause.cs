using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectPause : MonoBehaviour
{
    public Button ButtonBack;
    public Button ButtonRestart;
    public Button ButtonHome;

    [SerializeField] private GameObject _PopupMusicInfo;
    [SerializeField] private UIElementPopUp UIElementPopUp;

    private Boolean _isDestroy = false;

    // 팝업창 호출 시 UI 제어
    private float _duration = 0.15f;

    // 버튼 이벤트 세팅
    public void SetButtonEvent()
    {
        ButtonBack.onClick.AddListener(() => { SetButtonClickEvent("Back"); });
        ButtonRestart.onClick.AddListener(() => { SetButtonClickEvent("Restart"); });
        ButtonHome.onClick.AddListener(() => { SetButtonClickEvent("Home"); });
    }

    // 버튼 이벤트 처리
    public void SetButtonClickEvent(string Division)
    {
        if (Division.Equals("Back")) 
        { 
            // 뒤로 가기 > 게임 이어 하기 > 3초 필요 예정
        }
        else if (Division.Equals("Restart"))
        {
            // 재시작 하기 > 게임 초기화
            // **** 초기화 프로세스 작성

            // Music Info Restart
            Destroy(GameObject.Find(_PopupMusicInfo.name + "(Clone)"));

            // **** 수정 사항 : 복수 클릭 시 Destroy가 재대로 안됨
            //_isDestroy = true;
            //UIElementPopUp.SetPopUpMusicInfo();
        }
        else if (Division.Equals("Home"))
        {
            // 홈으로 이동 (음악 선택 화면으로 이동) > 현 진행도 관련 글로벌 데이터 저장
            // **** 홈으로 이동 및 글로벌 데이터 세팅 프로세스 작성

            // Music Info Destroy
            Destroy(GameObject.Find(_PopupMusicInfo.name + "(Clone)"));
        }

        // 버튼 이벤트 Unlock
        DataManager.dataBackgroundProcActive = true;

        // 창 닫기 버튼 이벤트
        UIElementSetting.Instance.ButtonClickControll("Pause", "Close");

        // 게임 일시 정지 프로세스 > 실행
        Stage.Instance.OnClickPause();
    }

    // 팝업 창 호출 시 UI 출력 제어
    private void OnEnable()
    {
        /*
        // 2022.12.12 > Stage의 OnClickPause 실행 시 Animation 멈춤으로 인해 Dotween 작동 안함
        // 사이즈 0부터 시작
        this.transform.localScale = Vector3.zero;
        // 1까지 커지면서 시간은 0.2초, 변환 시 큐빅 형태로 등장
        this.transform.DOScale(Vector3.one, _duration).SetEase(Ease.InCubic);
        */
    }

    void Start()
    {
        // 버튼 이벤트 설정
        SetButtonEvent();
    }

    /*
    public void OnDestroy()
    {
        Debug.Log(">>>>>>>>>>>>>> OnDestroy : " + _isDestroy);
        if(_isDestroy)
        {
            UIElementPopUp.SetPopUpMusicInfo();
        }
    }
    */
}
