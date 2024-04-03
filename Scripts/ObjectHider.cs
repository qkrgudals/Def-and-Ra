using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHider : MonoBehaviour {
    // "Hide Object" �±׸� ���� ������Ʈ�� ����Ǵ� ��ũ��Ʈ
    // (NavMesh Surface�� ������ ���ﶧ �ǹ� ���α��� �������°� �����ϱ� ���� ������Ʈ�� ���� ����� ����)

    void Start() {
        // ���� ���� �� ������Ʈ�� ����
        HideObjectsWithTag("HideObject");
    }

    void HideObjectsWithTag(string tag) {
        // �ش� �±׸� ���� ��� ������Ʈ�� ã��
        GameObject[] objectsToHide = GameObject.FindGameObjectsWithTag(tag);

        // ã�� ��� ������Ʈ�� ����
        foreach (GameObject obj in objectsToHide) {
            obj.SetActive(false);
        }
    }
}
