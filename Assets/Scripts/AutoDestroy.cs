using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] public float wait=4;
    void Start()
    {
        StartCoroutine("waitAndDestroy");
    }

    IEnumerator waitAndDestroy()
    {
        yield return new WaitForSeconds(wait);
        Destroy(this.gameObject);
    }
}
