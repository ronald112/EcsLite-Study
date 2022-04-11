using System;
using Client;
using UnityEngine;

public class EcsButtonTriggerChecker : MonoBehaviour
{
    private InitButtonEntity _initButtonEntity = null;

    private void Awake()
    {
        _initButtonEntity = GetComponent<InitButtonEntity>();

        if (!_initButtonEntity) throw new NullReferenceException();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_initButtonEntity.targetTag)) return;

        var poolButtonEvent = _initButtonEntity.mainWorld.GetPool<OnTriggerEnterEvent>();
        if (!poolButtonEvent.Has(_initButtonEntity.myEntity))
            poolButtonEvent.Add(_initButtonEntity.myEntity);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(_initButtonEntity.targetTag)) return;

        var poolButtonEvent = _initButtonEntity.mainWorld.GetPool<OnTriggerExitEvent>();
        if (!poolButtonEvent.Has(_initButtonEntity.myEntity))
            poolButtonEvent.Add(_initButtonEntity.myEntity);
    }
}