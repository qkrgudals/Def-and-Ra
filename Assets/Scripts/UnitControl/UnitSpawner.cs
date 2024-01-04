using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
	[SerializeField]
	private	GameObject	unitPrefab; // 생성될 유닛의 프리팹
    [SerializeField]
	private	int			maxUnitCount; // 생성할 최대 유닛 수

    private	Vector2		minSize = new Vector2(-70, -70); // 유닛 스폰 가능한 최소 좌표
    private	Vector2		maxSize = new Vector2(70, 70); // 유닛 스폰 가능한 최대 좌표

    public List<UnitController> SpawnUnits() // 유닛을 실제로 생성하고 위치를 설정하는 메소드, 랜덤한 위치에 maxUnitCount만큼의 유닛을 생성하고,
											                   // 생성된 유닛들을 List<UnitController> 형태로 반환
    {
		List<UnitController> unitList = new List<UnitController>(maxUnitCount);

		for ( int i = 0; i < maxUnitCount; ++ i )
		{
			Vector3 position = new Vector3(Random.Range(minSize.x, maxSize.x), 1, Random.Range(minSize.y, maxSize.y)); // 유닛의 위치를 랜덤으로 설정

            GameObject		clone	= Instantiate(unitPrefab, position, Quaternion.identity);   // 유닛을 생성시킴
            UnitController	unit	= clone.GetComponent<UnitController>(); // 생성된 유닛의 UnitController 컴포넌트를 가져옴

            // 유닛 초기 생성시 각자 다른 방향을 바라보도록 설정
            float randomRotationY = Random.Range(0f, 360f);
            unit.transform.rotation = Quaternion.Euler(0f, randomRotationY, 0f);

            unitList.Add(unit); // 리스트에 유닛 추가
		}

		return unitList;
	}
}

