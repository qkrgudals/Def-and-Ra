using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHider : MonoBehaviour {
    // "Hide Object" 태그를 가진 오브젝트에 적용되는 스크립트
    // (NavMesh Surface로 지면을 구울때 건물 내부까지 구워지는걸 방지하기 위한 오브젝트를 게임 실행시 숨김)

    void Start() {
        // 게임 실행 시 오브젝트를 숨김
        HideObjectsWithTag("HideObject");
    }

    void HideObjectsWithTag(string tag) {
        // 해당 태그를 가진 모든 오브젝트를 찾음
        GameObject[] objectsToHide = GameObject.FindGameObjectsWithTag(tag);

        // 찾은 모든 오브젝트를 숨김
        foreach (GameObject obj in objectsToHide) {
            obj.SetActive(false);
        }
    }
}
