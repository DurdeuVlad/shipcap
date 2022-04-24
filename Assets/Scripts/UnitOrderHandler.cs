using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitOrderHandler : MonoBehaviour
{
    UnitData unitData;
    public Move move;

    void Start()
    {
        unitData = GetComponent<UnitData>();
        move = new Move(gameObject);
        
    }

    bool StopMovement => move.destinations.Count == 0 && unitData.isMoving;

    void Update()
    {
        if (StopMovement)
        {
            move.Stop();
        }
        if (unitData.isSelected)
        {
            // show health
            
            move.UpdateLines();
        }
        else
        {
            move.DeleteLines();
        }
    }
    [Serializable]
    public class Move
    {
        public Move(GameObject par)
        {
            parent = par;
            pointTemplate = PlayerData.Instance.movePoint;
            unitData = parent.GetComponent<UnitData>();
        }
        
        public List<Vector3> destinations = new List<Vector3>();
        private List<GameObject> clickPoints = new List<GameObject>(); // for visual effect
        GameObject line = null;
        GameObject pointTemplate;
        GameObject parent;
        UnitData unitData;
        public void Moving(bool condition)
        {
            unitData.isMoving = condition;
        }
        public void Direct(Vector3 pos)
        {
            Stop();
            Add(pos);
        }

        public void Add(Vector3 pos)
        {
            if (!destinations.Contains(pos))
            {
                destinations.Add(pos);
            }
            Moving(true);
        }
        public void Stop()
        {
            DeleteLines();
            this.destinations = new List<Vector3>();
            Moving(false);
        }

        public void UpdateLines()
        {
            if (destinations.Count != 0){
                if (line == null)
                {
                    line = Instantiate(PlayerData.Instance.moveLine);
                }
                LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
                lineRenderer.SetPosition(0, parent.transform.position);
                
                lineRenderer.positionCount = destinations.Count+1;
                int i = 1;
                foreach (Vector3 vec3 in destinations)
                {
                    lineRenderer.SetPosition(i, vec3);
                    if (clickPoints.Count < destinations.Count)
                    { 
                        clickPoints.Add(Instantiate(pointTemplate));
                        
                    }
                    clickPoints[i - 1].transform.position = vec3;
                    i++;

                }
            }
        }

        public void DeleteLines()
        {
            Destroy(line);
            foreach (GameObject point in clickPoints)
            {
                Destroy(point);

            }

            clickPoints.Clear();
        }


    }
}
