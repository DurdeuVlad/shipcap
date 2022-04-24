using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    private static UnitSelection _instance;

    public static UnitSelection Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void AddUnit(GameObject unit)
    {
        UnitData unitData;
        if (unit.TryGetComponent(out unitData))
        {
            Debug.Log(PlayerData.Instance.PlayerId+" - Trying to add unit with player id " + unitData.ownerPlayer);
            if (PlayerData.Instance.PlayerId == unitData.ownerPlayer)
            {
                if (!unitsSelected.Contains(unit))
                { 
                    unitsSelected.Add(unit);
                    unit.GetComponent<UnitData>().isSelected = true;
                }
            }
        }
    }
    public bool IsUnitSelected(GameObject unit)
    {
        return unitsSelected.Contains(unit);
    }
    public void ClickSelect(GameObject unit)
    {
        DeselectAll();
        AddUnit(unit);
    }

    public void ShiftSelect(GameObject unit)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            AddUnit(unit);
        }
        else
        {
            Deselect(unit);

        }
    }

    public void DragSelect(GameObject unit)
    {
        AddUnit(unit);
    }

    public void DeselectAll()
    {
        foreach(GameObject gameObject in unitsSelected)
        {
            gameObject.GetComponent<UnitData>().isSelected = false;
        }
        unitsSelected.Clear();
    }

    public void Deselect(GameObject unit)
    {
        unitsSelected.Remove(unit);
        unit.GetComponent<UnitData>().isSelected = false;

    }

    public void StopAll()
    {
        foreach (GameObject gameObject in unitsSelected)
        {
            gameObject.GetComponent<UnitData>().isMoving = false;
            gameObject.GetComponent<UnitOrderHandler>().move.Stop();
        }
    }
}
