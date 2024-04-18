using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 60.0f; // �ʱ� �ð� ����
    public TextMeshProUGUI timeText; // UI Text ���

    void Update() {
        // �ð��� �帧�� ���� �ð� ����
        timeRemaining -= Time.deltaTime;

        // �ð��� 0 ���Ϸ� �������� �� ó��
        if (timeRemaining <= 0.0f) {
            timeRemaining = 0.0f;
            // �ð��� �� �Ǿ��� �� ���ϴ� �۾� ���� (��: ���� ���� ó��)
        }

        // �ð��� �а� �ʷ� ��ȯ�Ͽ� UI�� ǥ��
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        string timeString = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        timeText.text = timeString;
    }
}
