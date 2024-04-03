using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShrinkAndDamage : MonoBehaviour
{
    public float mapShrinkSpeed = 0.1f; // ���� �������� �ӵ�
    public float damagePerSecond = 10f; // �ʴ� �޴� ������

    private float initialMapSize; // �ʱ� �� ũ��
    private Vector3 initialMapPosition; // �ʱ� �� ��ġ

    void Start() {
        // �ʱ� �� ũ�� �� ��ġ ����
        initialMapSize = transform.localScale.x;
        initialMapPosition = transform.position;
    }

    void Update() {
        // �� ũ�⸦ �� �����Ӹ��� ����
        transform.localScale -= new Vector3(mapShrinkSpeed, 0f, mapShrinkSpeed) * Time.deltaTime;

        // �� ������ �������� Ȯ��
        if (!IsInsideMap(transform.position)) {
            // �� ������ �������� ������ ����
            ApplyDamage();
        }
    }

    bool IsInsideMap(Vector3 position) {
        // �� �ȿ� �ִ��� ���θ� �Ǵ��ϴ� �Լ�
        return position.x > initialMapPosition.x - initialMapSize / 2 &&
               position.x < initialMapPosition.x + initialMapSize / 2 &&
               position.z > initialMapPosition.z - initialMapSize / 2 &&
               position.z < initialMapPosition.z + initialMapSize / 2;
    }

    void ApplyDamage() {
        // ������ ����
        // ���÷� �ֿܼ� �������� ����ϵ��� �ۼ��Ͽ����ϴ�.
        Debug.Log("������: " + damagePerSecond * Time.deltaTime);
        // ���⿡ �������� �޴� ������ �߰��Ͻø� �˴ϴ�.
    }
}
