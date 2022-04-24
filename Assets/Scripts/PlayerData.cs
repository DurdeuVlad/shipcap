using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData: MonoBehaviour
{
    private static PlayerData _instance;
    
    public static PlayerData Instance { get { return _instance; } }

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

    [Header("Player Data")]
    public int PlayerId = 1;

    [Header("Selection Options")]
    public GameObject selectionCircle;

    public LayerMask ground;

    [Header("Key Options")]
    public KeyCode shift;
    public KeyCode stop;



    public GameObject moveLine;
    public GameObject movePoint;
    public GameObject moveClick;
    public GameObject attackLine;
    public GameObject attackPoint;

    public float DistanceUnits;


}
