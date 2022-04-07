using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MousePointTriggerChecker : MonoBehaviour
{
    private string _targetTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_targetTag)) return;
        
        Destroy(transform.parent.gameObject);
    }
}
