using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitOrderGiver : MonoBehaviour
{
    private Camera camera;


    private static UnitOrderGiver _instance;

    public static UnitOrderGiver Instance { get { return _instance; } }


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

    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, PlayerData.Instance.ground))
            {
                Vector3 point = hit.point;
                point.y = 0;
                GoTo(point);
                GameObject click = Instantiate(PlayerData.Instance.moveClick);
                click.transform.position = point;
            }
        }
    }

    void GoTo(Vector3 pos)
    {

        CalculatePosition(pos);
        int i = 0;
        foreach (GameObject unit in UnitSelection.Instance.unitsSelected)
        {
            UnitOrderHandler unitOrderHandler = unit.GetComponent<UnitOrderHandler>();
            if (unit.GetComponent<UnitData>().ownerPlayer == PlayerData.Instance.PlayerId)
            {
                if (Input.GetKey(PlayerData.Instance.shift))
                {
                    unitOrderHandler.move.Add(ReturnFarthestPosition(unitOrderHandler.gameObject.transform.position));
                }
                else
                {
                    unitOrderHandler.move.Direct(ReturnFarthestPosition(unitOrderHandler.gameObject.transform.position));
                }
                i++;
            }
        }
    }

    Vector3 ReturnFarthestPosition(Vector3 origin)
    {
        float max = 0;
        Vector3 vMax = positions[0];
        for (int i = 0; i < positions.Count - 1; i++)
        {
            if ((positions[i] - origin).magnitude > max)
            {
                max = (positions[i] - origin).magnitude;
                vMax = positions[i];
            }
        }
        positions.Remove(vMax);
        return vMax;
    }
    void OrdonateUnitsByDistance(Vector3 destination)
    {
        UnitSelection unitSelectionScript = UnitSelection.Instance;
        for (int i = 0; i < unitSelectionScript.unitsSelected.Count - 1; i++)
        {
            for (int j = i; j < unitSelectionScript.unitsSelected.Count; j++)
            {
                if ((unitSelectionScript.unitsSelected[i].transform.position - destination).magnitude >
                    (unitSelectionScript.unitsSelected[j].transform.position - destination).magnitude)
                {
                    GameObject aux = unitSelectionScript.unitsSelected[i];
                    unitSelectionScript.unitsSelected[i] = unitSelectionScript.unitsSelected[j];
                    unitSelectionScript.unitsSelected[j] = aux;
                }
            }
        }
    }
    //PlayerData.Instance.DistanceUnits
    float offset = 2;
    public List<Vector3> positions = new List<Vector3>();
    bool CheckIfTooClose(Vector3 vector3)
    {
        bool ok = false;
        foreach (Vector3 vector in positions)
        {
            if (vector.magnitude == vector3.magnitude)// sau  offset / 2 > Mathf.Abs(vector.magnitude - vector3.magnitude)
            {
                ok = true;
                break;
            }
        }
        return ok;
    }



    /// <summary>
    /// use this to dynamically calculate the units position on the map so they don't bump on eachother
    /// 
    /// </summary>
    /// <param name="Destination"></param>
    void CalculatePosition(Vector3 Destination)
    {
        OrdonateUnitsByDistance(Destination);
        int maxTryes = 20;
        positions = new List<Vector3>();
        int i = 0;
        int wereWeLeft = 0;


        Vector3 destination = Destination;

        foreach (GameObject unit in UnitSelection.Instance.unitsSelected)
        {


            Transform unitTr = unit.GetComponent<Transform>();
            if (unit.GetComponent<UnitData>().ownerPlayer == PlayerData.Instance.PlayerId)
            {
                //if (i == 0) { positions.Add(destination); i++; }
                //else if (i == 1) {
                //    destination = new Vector3(destination.x + offset, destination.y,
                //     destination.z); positions.Add(destination); i++; }
                //else
                {
                    int trys = 0;
                    do
                    {
                        trys++;
                        if (i != 0)
                        {
                            if (i % 8 == 0)
                            {
                                
                                wereWeLeft++;

                            }
                        }
                        

                        if (positions.Count <= wereWeLeft)
                        {
                            destination = Destination;
                            break;
                        }
                        else
                        {
                            destination = positions[wereWeLeft];
                        }



                        switch (i % 8)
                        {
                            case 0: // 0 is right
                                destination = new Vector3(destination.x + offset, destination.y,
                                     destination.z);
                                break;
                            case 1: // 
                                destination = new Vector3(destination.x + offset, destination.y,
                                     destination.z - offset);
                                break;
                            case 2: // 2 is jos
                                //+ unitTr.localScale.x / 2 + UnitSelection.Instance.unitsSelected[wereWeLeft].transform.localScale.x / 2
                                destination = new Vector3(destination.x, destination.y,
                                   destination.z - offset);
                                break;
                            case 3: // 
                                //+ unitTr.localScale.x / 2 + UnitSelection.Instance.unitsSelected[wereWeLeft].transform.localScale.x / 2
                                destination = new Vector3(destination.x - offset, destination.y,
                                   destination.z - offset);
                                break;
                            case 4: // 4 is stanga
                                // - unitTr.localScale.x / 2 - UnitSelection.Instance.unitsSelected[wereWeLeft].transform.localScale.x / 2)
                                
                                destination = new Vector3(destination.x - offset, destination.y,
                                 destination.z);

                                break;
                            case 5:
                                destination = new Vector3(destination.x - offset, destination.y,
                                 destination.z + offset);
                                break;
                            case 6: // 6 is sus
                                //- (offset + unitTr.localScale.z / 2 + UnitSelection.Instance.unitsSelected[wereWeLeft].transform.localScale.z / 2)
                                
                                destination = new Vector3(destination.x, destination.y,
                                   destination.z + offset); // + unitTr.localScale.z / 2 + UnitSelection.Instance.unitsSelected[wereWeLeft].transform.localScale.z / 2
                                break;
                            case 7: // 6 is sus
                                    //- (offset + unitTr.localScale.z / 2 + UnitSelection.Instance.unitsSelected[wereWeLeft].transform.localScale.z / 2)

                                destination = new Vector3(destination.x + offset, destination.y,
                                   destination.z + offset); // + unitTr.localScale.z / 2 + UnitSelection.Instance.unitsSelected[wereWeLeft].transform.localScale.z / 2
                                break;

                        }



                        i++;
                    } while (CheckIfTooClose(destination) && maxTryes >= trys);
                }
                
                positions.Add(destination);
                
                //DistanceUnits
                //unitTr.position
            }
        }
        //return Destination;
    }
    //UnitSelection.Instance.unitsSelected
}
