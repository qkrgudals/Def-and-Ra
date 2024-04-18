using System.Collections.Generic;
using UnityEngine;

public class RTSUnitController2 : MonoBehaviour
{
	[SerializeField]
	private	UnitSpawner			 unitSpawner;
	private	List<UnitController> selectedUnitList;				// �÷��̾ Ŭ�� or �巡�׷� ������ ����
	public	List<UnitController> UnitList { private set; get; } // �ʿ� �����ϴ� ��� ����


    private void Awake()
	{
		selectedUnitList = new List<UnitController>();
		//UnitList		 = unitSpawner.SpawnUnits();
	}
    private void FixedUpdate() {
        //selectedUnitList = new List<UnitController>();
        UnitList = unitSpawner.SpawnUnits();
    }
    /// <summary>
    /// ���콺 Ŭ������ ������ ������ �� ȣ��
    /// </summary>
    public void ClickSelectUnit(UnitController newUnit)
	{
		// ������ ���õǾ� �ִ� ��� ���� ����
		DeselectAll();

		SelectUnit(newUnit);
	}

	/// <summary>
	/// Shift+���콺 Ŭ������ ������ ������ �� ȣ��
	/// </summary>
	public void ShiftClickSelectUnit(UnitController newUnit)
	{
		// ������ ���õǾ� �ִ� ������ ����������
		if ( selectedUnitList.Contains(newUnit) )
		{
			DeselectUnit(newUnit);
		}
		// ���ο� ������ ����������
		else
		{
			SelectUnit(newUnit);
		}
	}

	/// <summary>
	/// ���콺 �巡�׷� ������ ������ �� ȣ��
	/// </summary>
	public void DragSelectUnit(UnitController newUnit)
	{
		// ���ο� ������ ����������
		if ( !selectedUnitList.Contains(newUnit) )
		{
			SelectUnit(newUnit);
		}
	}

	/// <summary>
	/// ���õ� ��� ������ �̵��� �� ȣ��
	/// </summary>
	public void MoveSelectedUnits(Vector3 end)
	{
		for ( int i = 0; i < selectedUnitList.Count; ++ i )
		{
			selectedUnitList[i].MoveTo(end);
		}
	}

    public void MoveSelectedUnitsEnemy(GameObject enemy)
    {
        for (int i = 0; i < selectedUnitList.Count; ++i)
        {
            selectedUnitList[i].MoveTowardsEnemy(enemy);
        }
    }


    /// <summary>
    /// ��� ������ ������ ������ �� ȣ��
    /// </summary>
    public void DeselectAll()
	{
		for ( int i = 0; i < selectedUnitList.Count; ++ i )
		{
			selectedUnitList[i].DeselectUnit();
		}

		selectedUnitList.Clear();
	}

	/// <summary>
	/// �Ű������� �޾ƿ� newUnit ���� ����
	/// </summary>
	private void SelectUnit(UnitController newUnit)
	{
		// ������ ���õǾ��� �� ȣ���ϴ� �޼ҵ�
		newUnit.SelectUnit();
		// ������ ���� ������ ����Ʈ�� ����
		selectedUnitList.Add(newUnit);
	}

	/// <summary>
	/// �Ű������� �޾ƿ� newUnit ���� ���� ����
	/// </summary>
	private void DeselectUnit(UnitController newUnit)
	{
        // ������ �����Ǿ��� �� ȣ���ϴ� �޼ҵ�
        newUnit.DeselectUnit();
		// ������ ���� ������ ����Ʈ���� ����
		selectedUnitList.Remove(newUnit);
	}
}

