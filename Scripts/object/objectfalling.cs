using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectfalling : MonoBehaviour {
    public float fallDelay = 2f; // 떨어지기까지의 대기 시간
    public float riseDelay = 1f; // 올라가기까지의 대기 시간
    public float moveDistance = 8f; // 이동 거리
    public int repeatCount = 4; // 반복 
    CountdownTimer timer;

    void Start() {
        StartCoroutine(S());
    }

    IEnumerator S() {
        timer = FindObjectOfType<CountdownTimer>(); // 타이머 참조
        for (int i = 0; i < repeatCount; i++) {
            //Debug.Log("dd");
            yield return StartCoroutine(Fall());
            yield return new WaitForSeconds(riseDelay);
            yield return StartCoroutine(Rise());
            yield return new WaitForSeconds(riseDelay);
        }
    }

    public IEnumerator Fall() {
        //float initialTime = timer.timeRemaining; // 초기 타이머 값 저장
        yield return new WaitUntil(() => timer.timeRemaining <= 0); // 타이머 값이 대기 시간만큼 감소할 때까지 대기
        transform.Translate(Vector3.down * moveDistance); // 이동
    }

    public IEnumerator Rise() {
        //float initialTime = timer.timeRemaining; // 초기 타이머 값 저장
        yield return new WaitUntil(() => timer.timeRemaining >= 30.0f); // 타이머 값이 대기 시간만큼 감소할 때까지 대기
        transform.Translate(Vector3.up * moveDistance); // 이동
    }
}
