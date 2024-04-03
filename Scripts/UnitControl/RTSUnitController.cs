using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSUnitController : MonoBehaviour
{
    [SerializeField]
    private UnitSpawner2 unitSpawner;
    public List<Soldier_UnitController> selectedUnitList { set; get; } // �÷��̾ Ŭ�� or �巡�׷� ������ ����
    public List<Soldier_UnitController> UnitList { private set; get; } // �ʿ� �����ϴ� ��� ����

    public List<Archer_UnitController> selectedUnitList2 { set; get; } // �÷��̾ Ŭ�� or �巡�׷� ������ ����
    public List<Archer_UnitController> UnitList2 { private set; get; } // �ʿ� �����ϴ� ��� ����


    private void Awake() {
        selectedUnitList = new List<Soldier_UnitController>();
        selectedUnitList2 = new List<Archer_UnitController>();
        UnitList = unitSpawner.SpawnUnits();
        UnitList2 = unitSpawner.SpawnUnits2();
     
    }

    /// <summary>
    /// ���콺 Ŭ������ ������ ������ �� ȣ��
    /// </summary>
    public void ClickSelectUnit(Soldier_UnitController newUnit) {
        // ������ ���õǾ� �ִ� ��� ���� ����
        DeselectAll();

        SelectUnit(newUnit);
    }

    /// <summary>
    /// Shift+���콺 Ŭ������ ������ ������ �� ȣ��
    /// </summary>
    public void ShiftClickSelectUnit(Soldier_UnitController newUnit) {
        // ������ ���õǾ� �ִ� ������ ����������
        if (selectedUnitList.Contains(newUnit)) {
            DeselectUnit(newUnit);
        }
        // ���ο� ������ ����������
        else {
            SelectUnit(newUnit);
        }
    }

    /// <summary>
    /// ���콺 �巡�׷� ������ ������ �� ȣ��
    /// </summary>
    public void DragSelectUnit(Soldier_UnitController newUnit) {
        // ���ο� ������ ����������
        if (!selectedUnitList.Contains(newUnit)) {
            SelectUnit(newUnit);
        }
    }

    /// <summary>
    /// ���õ� ��� ������ �̵��� �� ȣ��
    /// </summary>
    public void MoveSelectedUnits(Vector3 end) {
        for (int i = 0; i < selectedUnitList.Count; ++i) {
            selectedUnitList[i].MoveTo(end);
        }
    }

    /// <summary>
    /// ��� ������ ������ ������ �� ȣ��
    /// </summary>
    public void DeselectAll() {
        for (int i = 0; i < selectedUnitList.Count; ++i) {
            selectedUnitList[i].DeselectUnit();
        }

        selectedUnitList.Clear();
    }

    /// <summary>
    /// �Ű������� �޾ƿ� newUnit ���� ����
    /// </summary>
    private void SelectUnit(Soldier_UnitController newUnit) {
        // ������ ���õǾ��� �� ȣ���ϴ� �޼ҵ�
        newUnit.SelectUnit();
        // ������ ���� ������ ����Ʈ�� ����
        selectedUnitList.Add(newUnit);
    }

    /// <summary>
    /// �Ű������� �޾ƿ� newUnit ���� ���� ����
    /// </summary>
    private void DeselectUnit(Soldier_UnitController newUnit) {
        // ������ �����Ǿ��� �� ȣ���ϴ� �޼ҵ�
        newUnit.DeselectUnit();
        // ������ ���� ������ ����Ʈ���� ����
        selectedUnitList.Remove(newUnit);
    }


    public void ClickSelectUnit(Archer_UnitController newUnit) {
        // ������ ���õǾ� �ִ� ��� ���� ����
        DeselectAll2();

        SelectUnit(newUnit);
    }

    /// <summary>
    /// Shift+���콺 Ŭ������ ������ ������ �� ȣ��
    /// </summary>
    public void ShiftClickSelectUnit(Archer_UnitController newUnit) {
        // ������ ���õǾ� �ִ� ������ ����������
        if (selectedUnitList2.Contains(newUnit)) {
            DeselectUnit(newUnit);
        }
        // ���ο� ������ ����������
        else {
            SelectUnit(newUnit);
        }
    }

    /// <summary>
    /// ���콺 �巡�׷� ������ ������ �� ȣ��
    /// </summary>
    public void DragSelectUnit(Archer_UnitController newUnit) {
        // ���ο� ������ ����������
        if (!selectedUnitList2.Contains(newUnit)) {
            SelectUnit(newUnit);
        }
    }

    /// <summary>
    /// ���õ� ��� ������ �̵��� �� ȣ��
    /// </summary>
    public void MoveSelectedUnits2(Vector3 end) {
        for (int i = 0; i < selectedUnitList2.Count; ++i) {
            selectedUnitList2[i].MoveTo(end);
        }
    }

    /// <summary>
    /// ��� ������ ������ ������ �� ȣ��
    /// </summary>
    public void DeselectAll2() {
        for (int i = 0; i < selectedUnitList2.Count; ++i) {
            selectedUnitList2[i].DeselectUnit();
        }

        selectedUnitList2.Clear();
    }

    /// <summary>
    /// �Ű������� �޾ƿ� newUnit ���� ����
    /// </summary>
    private void SelectUnit(Archer_UnitController newUnit) {
        // ������ ���õǾ��� �� ȣ���ϴ� �޼ҵ�
        newUnit.SelectUnit();
        // ������ ���� ������ ����Ʈ�� ����
        selectedUnitList2.Add(newUnit);
    }

    /// <summary>
    /// �Ű������� �޾ƿ� newUnit ���� ���� ����
    /// </summary>
    private void DeselectUnit(Archer_UnitController newUnit) {
        // ������ �����Ǿ��� �� ȣ���ϴ� �޼ҵ�
        newUnit.DeselectUnit();
        // ������ ���� ������ ����Ʈ���� ����
        selectedUnitList2.Remove(newUnit);
    }


  
   
}
