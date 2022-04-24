using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{

    private Camera camera;

    public LayerMask clickable;
    public LayerMask ground;
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(PlayerData.Instance.stop))
        {
            UnitSelection.Instance.StopAll();
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (UnitSelection.Instance.IsUnitSelected(hit.collider.gameObject))
                    {
                        UnitSelection.Instance.Deselect(hit.collider.gameObject);
                    }
                    else
                    {
                        UnitSelection.Instance.ShiftSelect(hit.collider.gameObject);
                    }
                    
                }
                else
                {
                    UnitSelection.Instance.ClickSelect(hit.collider.gameObject);
                    Debug.Log("unit "+ hit.collider.gameObject.GetComponent<UnitData>().name + " selected");

                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelection.Instance.DeselectAll();
                }
                
            }
        }
    }
}
