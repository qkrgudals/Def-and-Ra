using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 60.0f; // 초기 시간 설정
    public TextMeshProUGUI timeText; // UI Text 요소

    void Update() {
        // 시간이 흐름에 따라 시간 감소
        timeRemaining -= Time.deltaTime;

        // 시간이 0 이하로 떨어졌을 때 처리
        if (timeRemaining <= 0.0f) {
            timeRemaining = 0.0f;
            // 시간이 다 되었을 때 원하는 작업 수행 (예: 게임 오버 처리)
        }

        // 시간을 분과 초로 변환하여 UI에 표시
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        string timeString = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        timeText.text = timeString;
    }
}
