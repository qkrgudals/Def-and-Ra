using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSUnitController : MonoBehaviour
{
    [SerializeField]
    private UnitSpawner2 unitSpawner;
    public List<Soldier_UnitController> selectedUnitList { set; get; } // 플레이어가 클릭 or 드래그로 선택한 유닛
    public List<Soldier_UnitController> UnitList { private set; get; } // 맵에 존재하는 모든 유닛

    public List<Archer_UnitController> selectedUnitList2 { set; get; } // 플레이어가 클릭 or 드래그로 선택한 유닛
    public List<Archer_UnitController> UnitList2 { private set; get; } // 맵에 존재하는 모든 유닛


    private void Awake() {
        selectedUnitList = new List<Soldier_UnitController>();
        selectedUnitList2 = new List<Archer_UnitController>();
        UnitList = unitSpawner.SpawnUnits();
        UnitList2 = unitSpawner.SpawnUnits2();
     
    }

    /// <summary>
    /// 마우스 클릭으로 유닛을 선택할 때 호출
    /// </summary>
    public void ClickSelectUnit(Soldier_UnitController newUnit) {
        // 기존에 선택되어 있는 모든 유닛 해제
        DeselectAll();

        SelectUnit(newUnit);
    }

    /// <summary>
    /// Shift+마우스 클릭으로 유닛을 선택할 때 호출
    /// </summary>
    public void ShiftClickSelectUnit(Soldier_UnitController newUnit) {
        // 기존에 선택되어 있는 유닛을 선택했으면
        if (selectedUnitList.Contains(newUnit)) {
            DeselectUnit(newUnit);
        }
        // 새로운 유닛을 선택했으면
        else {
            SelectUnit(newUnit);
        }
    }

    /// <summary>
    /// 마우스 드래그로 유닛을 선택할 때 호출
    /// </summary>
    public void DragSelectUnit(Soldier_UnitController newUnit) {
        // 새로운 유닛을 선택했으면
        if (!selectedUnitList.Contains(newUnit)) {
            SelectUnit(newUnit);
        }
    }

    /// <summary>
    /// 선택된 모든 유닛을 이동할 때 호출
    /// </summary>
    public void MoveSelectedUnits(Vector3 end) {
        for (int i = 0; i < selectedUnitList.Count; ++i) {
            selectedUnitList[i].MoveTo(end);
        }
    }

    /// <summary>
    /// 모든 유닛의 선택을 해제할 때 호출
    /// </summary>
    public void DeselectAll() {
        for (int i = 0; i < selectedUnitList.Count; ++i) {
            selectedUnitList[i].DeselectUnit();
        }

        selectedUnitList.Clear();
    }

    /// <summary>
    /// 매개변수로 받아온 newUnit 선택 설정
    /// </summary>
    private void SelectUnit(Soldier_UnitController newUnit) {
        // 유닛이 선택되었을 때 호출하는 메소드
        newUnit.SelectUnit();
        // 선택한 유닛 정보를 리스트에 저장
        selectedUnitList.Add(newUnit);
    }

    /// <summary>
    /// 매개변수로 받아온 newUnit 선택 해제 설정
    /// </summary>
    private void DeselectUnit(Soldier_UnitController newUnit) {
        // 유닛이 해제되었을 때 호출하는 메소드
        newUnit.DeselectUnit();
        // 선택한 유닛 정보를 리스트에서 삭제
        selectedUnitList.Remove(newUnit);
    }


    public void ClickSelectUnit(Archer_UnitController newUnit) {
        // 기존에 선택되어 있는 모든 유닛 해제
        DeselectAll2();

        SelectUnit(newUnit);
    }

    /// <summary>
    /// Shift+마우스 클릭으로 유닛을 선택할 때 호출
    /// </summary>
    public void ShiftClickSelectUnit(Archer_UnitController newUnit) {
        // 기존에 선택되어 있는 유닛을 선택했으면
        if (selectedUnitList2.Contains(newUnit)) {
            DeselectUnit(newUnit);
        }
        // 새로운 유닛을 선택했으면
        else {
            SelectUnit(newUnit);
        }
    }

    /// <summary>
    /// 마우스 드래그로 유닛을 선택할 때 호출
    /// </summary>
    public void DragSelectUnit(Archer_UnitController newUnit) {
        // 새로운 유닛을 선택했으면
        if (!selectedUnitList2.Contains(newUnit)) {
            SelectUnit(newUnit);
        }
    }

    /// <summary>
    /// 선택된 모든 유닛을 이동할 때 호출
    /// </summary>
    public void MoveSelectedUnits2(Vector3 end) {
        for (int i = 0; i < selectedUnitList2.Count; ++i) {
            selectedUnitList2[i].MoveTo(end);
        }
    }

    /// <summary>
    /// 모든 유닛의 선택을 해제할 때 호출
    /// </summary>
    public void DeselectAll2() {
        for (int i = 0; i < selectedUnitList2.Count; ++i) {
            selectedUnitList2[i].DeselectUnit();
        }

        selectedUnitList2.Clear();
    }

    /// <summary>
    /// 매개변수로 받아온 newUnit 선택 설정
    /// </summary>
    private void SelectUnit(Archer_UnitController newUnit) {
        // 유닛이 선택되었을 때 호출하는 메소드
        newUnit.SelectUnit();
        // 선택한 유닛 정보를 리스트에 저장
        selectedUnitList2.Add(newUnit);
    }

    /// <summary>
    /// 매개변수로 받아온 newUnit 선택 해제 설정
    /// </summary>
    private void DeselectUnit(Archer_UnitController newUnit) {
        // 유닛이 해제되었을 때 호출하는 메소드
        newUnit.DeselectUnit();
        // 선택한 유닛 정보를 리스트에서 삭제
        selectedUnitList2.Remove(newUnit);
    }


  
   
}
