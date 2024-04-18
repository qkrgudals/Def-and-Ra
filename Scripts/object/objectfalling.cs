using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectfalling : MonoBehaviour {
    public float fallDelay = 2f; // ������������� ��� �ð�
    public float riseDelay = 1f; // �ö󰡱������ ��� �ð�
    public float moveDistance = 8f; // �̵� �Ÿ�
    public int repeatCount = 4; // �ݺ� 
    CountdownTimer timer;

    void Start() {
        StartCoroutine(S());
    }

    IEnumerator S() {
        timer = FindObjectOfType<CountdownTimer>(); // Ÿ�̸� ����
        for (int i = 0; i < repeatCount; i++) {
            //Debug.Log("dd");
            yield return StartCoroutine(Fall());
            yield return new WaitForSeconds(riseDelay);
            yield return StartCoroutine(Rise());
            yield return new WaitForSeconds(riseDelay);
        }
    }

    public IEnumerator Fall() {
        //float initialTime = timer.timeRemaining; // �ʱ� Ÿ�̸� �� ����
        yield return new WaitUntil(() => timer.timeRemaining <= 0); // Ÿ�̸� ���� ��� �ð���ŭ ������ ������ ���
        transform.Translate(Vector3.down * moveDistance); // �̵�
    }

    public IEnumerator Rise() {
        //float initialTime = timer.timeRemaining; // �ʱ� Ÿ�̸� �� ����
        yield return new WaitUntil(() => timer.timeRemaining >= 30.0f); // Ÿ�̸� ���� ��� �ð���ŭ ������ ������ ���
        transform.Translate(Vector3.up * moveDistance); // �̵�
    }
}
