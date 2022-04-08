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

        var ecsPoolButtonEvent = _initButtonEntity.mainWorld.GetPool<PressedButtonEventComponent>();
        if (!ecsPoolButtonEvent.Has(_initButtonEntity.myEntity))
            ecsPoolButtonEvent.Add(_initButtonEntity.myEntity);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(_initButtonEntity.targetTag)) return;

        var unpressBtn = _initButtonEntity.mainWorld.GetPool<UnpressedButtonEventComponent>();
        if (!unpressBtn.Has(_initButtonEntity.myEntity))
        {
            unpressBtn.Add(_initButtonEntity.myEntity);
        }
    }
}