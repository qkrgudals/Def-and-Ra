using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShrinkAndDamage : MonoBehaviour
{
    public float mapShrinkSpeed = 0.1f; // 맵이 좁아지는 속도
    public float damagePerSecond = 10f; // 초당 받는 데미지

    private float initialMapSize; // 초기 맵 크기
    private Vector3 initialMapPosition; // 초기 맵 위치

    void Start() {
        // 초기 맵 크기 및 위치 설정
        initialMapSize = transform.localScale.x;
        initialMapPosition = transform.position;
    }

    void Update() {
        // 맵 크기를 매 프레임마다 줄임
        transform.localScale -= new Vector3(mapShrinkSpeed, 0f, mapShrinkSpeed) * Time.deltaTime;

        // 맵 밖으로 나갔는지 확인
        if (!IsInsideMap(transform.position)) {
            // 맵 밖으로 나갔으면 데미지 적용
            ApplyDamage();
        }
    }

    bool IsInsideMap(Vector3 position) {
        // 맵 안에 있는지 여부를 판단하는 함수
        return position.x > initialMapPosition.x - initialMapSize / 2 &&
               position.x < initialMapPosition.x + initialMapSize / 2 &&
               position.z > initialMapPosition.z - initialMapSize / 2 &&
               position.z < initialMapPosition.z + initialMapSize / 2;
    }

    void ApplyDamage() {
        // 데미지 적용
        // 예시로 콘솔에 데미지를 출력하도록 작성하였습니다.
        Debug.Log("데미지: " + damagePerSecond * Time.deltaTime);
        // 여기에 데미지를 받는 로직을 추가하시면 됩니다.
    }
}
