using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner2 : MonoBehaviour {
    [SerializeField]
    private GameObject soldierUnitPrefab; // 생성될 검사 유닛의 프리팹
    [SerializeField]
    private GameObject archerUnitPrefab; // 생성될 궁수 유닛의 프리팹
    private Vector2 minSize = new Vector2(0, 1); // 유닛 스폰 가능한 최소 좌표
    private Vector2 maxSize = new Vector2(1, 2); // 유닛 스폰 가능한 최대 좌표
    List<Soldier_UnitController> unitList = new List<Soldier_UnitController>();
    List<Archer_UnitController> unitList2 = new List<Archer_UnitController>();

    public List<Soldier_UnitController> SpawnUnits() // 유닛을 실제로 생성하고 위치를 설정하는 메소드, 랜덤한 위치에 maxUnitCount만큼의 유닛을 생성하고,
                                                     // 생성된 유닛들을 List<UnitController> 형태로 반환
    {
        unitList.Clear();
        /*
        // Your existing code for spawning units goes here
        Vector3 position = new Vector3(Random.Range(minSize.x, maxSize.x), 33, Random.Range(minSize.y, maxSize.y)); // 유닛의 위치를 랜덤으로 설정

        GameObject clone = Instantiate(soldierUnitPrefab, position, Quaternion.identity); // 유닛을 생성시킴

        Soldier_UnitController unit = clone.GetComponent<Soldier_UnitController>(); // 생성된 유닛의 UnitController 컴포넌트를 가져옴

        // 유닛 초기 생성시 각자 다른 방향을 바라보도록 설정
        float randomRotationY = Random.Range(0f, 360f);
        unit.transform.rotation = Quaternion.Euler(0f, randomRotationY, 0f);

        // Add the spawned units to the list
        unitList.Add(unit);
        */
        return unitList;
    }
    public void SpawnUnit(GameObject prefab) {

        Vector3 position = new Vector3(Random.Range(minSize.x, maxSize.x), -4.5f, Random.Range(minSize.y, maxSize.y)); // 유닛의 위치를 랜덤으로 설정

        GameObject clone = Instantiate(prefab, position, Quaternion.identity); // 유닛을 생성시킴
        Soldier_UnitController unit = clone.GetComponent<Soldier_UnitController>(); // 생성된 유닛의 UnitController 컴포넌트를 가져옴

        // 유닛 초기 생성시 각자 다른 방향을 바라보도록 설정
        float randomRotationY = Random.Range(0f, 360f);
        unit.transform.rotation = Quaternion.Euler(0f, randomRotationY, 0f);

        // Add the spawned unit to the list
        unitList.Add(unit);
    }
    public List<Archer_UnitController> SpawnUnits2() // 유닛을 실제로 생성하고 위치를 설정하는 메소드, 랜덤한 위치에 maxUnitCount만큼의 유닛을 생성하고,
                                                     // 생성된 유닛들을 List<UnitController> 형태로 반환
    {
        unitList2.Clear();

        return unitList2;
    }
    public void SpawnUnit2(GameObject prefab) {

        Vector3 position = new Vector3(Random.Range(minSize.x, maxSize.x), -4.5f, Random.Range(minSize.y, maxSize.y)); // 유닛의 위치를 랜덤으로 설정

        GameObject clone = Instantiate(prefab, position, Quaternion.identity); // 유닛을 생성시킴
        Archer_UnitController unit = clone.GetComponent<Archer_UnitController>(); // 생성된 유닛의 UnitController 컴포넌트를 가져옴

        // 유닛 초기 생성시 각자 다른 방향을 바라보도록 설정
        float randomRotationY = Random.Range(0f, 360f);
        unit.transform.rotation = Quaternion.Euler(0f, randomRotationY, 0f);

        // Add the spawned unit to the list
        unitList2.Add(unit);
    }

}

