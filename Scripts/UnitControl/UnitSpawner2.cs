using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner2 : MonoBehaviour {
    [SerializeField]
    private GameObject soldierUnitPrefab; // ������ �˻� ������ ������
    [SerializeField]
    private GameObject archerUnitPrefab; // ������ �ü� ������ ������
    private Vector2 minSize = new Vector2(0, 1); // ���� ���� ������ �ּ� ��ǥ
    private Vector2 maxSize = new Vector2(1, 2); // ���� ���� ������ �ִ� ��ǥ
    List<Soldier_UnitController> unitList = new List<Soldier_UnitController>();
    List<Archer_UnitController> unitList2 = new List<Archer_UnitController>();

    public List<Soldier_UnitController> SpawnUnits() // ������ ������ �����ϰ� ��ġ�� �����ϴ� �޼ҵ�, ������ ��ġ�� maxUnitCount��ŭ�� ������ �����ϰ�,
                                                     // ������ ���ֵ��� List<UnitController> ���·� ��ȯ
    {
        unitList.Clear();
        /*
        // Your existing code for spawning units goes here
        Vector3 position = new Vector3(Random.Range(minSize.x, maxSize.x), 33, Random.Range(minSize.y, maxSize.y)); // ������ ��ġ�� �������� ����

        GameObject clone = Instantiate(soldierUnitPrefab, position, Quaternion.identity); // ������ ������Ŵ

        Soldier_UnitController unit = clone.GetComponent<Soldier_UnitController>(); // ������ ������ UnitController ������Ʈ�� ������

        // ���� �ʱ� ������ ���� �ٸ� ������ �ٶ󺸵��� ����
        float randomRotationY = Random.Range(0f, 360f);
        unit.transform.rotation = Quaternion.Euler(0f, randomRotationY, 0f);

        // Add the spawned units to the list
        unitList.Add(unit);
        */
        return unitList;
    }
    public void SpawnUnit(GameObject prefab) {

        Vector3 position = new Vector3(Random.Range(minSize.x, maxSize.x), -4.5f, Random.Range(minSize.y, maxSize.y)); // ������ ��ġ�� �������� ����

        GameObject clone = Instantiate(prefab, position, Quaternion.identity); // ������ ������Ŵ
        Soldier_UnitController unit = clone.GetComponent<Soldier_UnitController>(); // ������ ������ UnitController ������Ʈ�� ������

        // ���� �ʱ� ������ ���� �ٸ� ������ �ٶ󺸵��� ����
        float randomRotationY = Random.Range(0f, 360f);
        unit.transform.rotation = Quaternion.Euler(0f, randomRotationY, 0f);

        // Add the spawned unit to the list
        unitList.Add(unit);
    }
    public List<Archer_UnitController> SpawnUnits2() // ������ ������ �����ϰ� ��ġ�� �����ϴ� �޼ҵ�, ������ ��ġ�� maxUnitCount��ŭ�� ������ �����ϰ�,
                                                     // ������ ���ֵ��� List<UnitController> ���·� ��ȯ
    {
        unitList2.Clear();

        return unitList2;
    }
    public void SpawnUnit2(GameObject prefab) {

        Vector3 position = new Vector3(Random.Range(minSize.x, maxSize.x), -4.5f, Random.Range(minSize.y, maxSize.y)); // ������ ��ġ�� �������� ����

        GameObject clone = Instantiate(prefab, position, Quaternion.identity); // ������ ������Ŵ
        Archer_UnitController unit = clone.GetComponent<Archer_UnitController>(); // ������ ������ UnitController ������Ʈ�� ������

        // ���� �ʱ� ������ ���� �ٸ� ������ �ٶ󺸵��� ����
        float randomRotationY = Random.Range(0f, 360f);
        unit.transform.rotation = Quaternion.Euler(0f, randomRotationY, 0f);

        // Add the spawned unit to the list
        unitList2.Add(unit);
    }

}

