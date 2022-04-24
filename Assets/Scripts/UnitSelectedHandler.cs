using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedHandler : MonoBehaviour
{
    UnitData unitData;
    Transform tr;
    
    void Start()
    {
        unitData = gameObject.GetComponent<UnitData>();
        tr = gameObject.transform;
        //PlayerData.Instance

        // take a reference to the default healthBar

        // take a reference to the default selection circle

        // take a reference to the default command line

    }

    GameObject _SelectionCircle = null;
    private void Update()
    {
        //if selected
        if (unitData.isSelected)
        {
            if (_SelectionCircle == null)
            {
                _SelectionCircle = Instantiate(PlayerData.Instance.selectionCircle, gameObject.transform);
                _SelectionCircle.transform.localPosition = new Vector3(0, -tr.localScale.y / 2);
                _SelectionCircle.transform.localScale = new Vector3(tr.localScale.x+1, _SelectionCircle.transform.localScale.y, tr.localScale.y + 1);
            }

            // shows healthbar+shieldbar
            // generates shows selection circle
            // generates and shows the way
        }
        else
        {
            if (_SelectionCircle != null)
            {
                Destroy(_SelectionCircle);
                _SelectionCircle = null;
            }
        }
        //else
        // destroys everything
    }
}
