using System;
using Client;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;
using Zenject;

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
        
        if (!_initButtonEntity.mainWorld.GetPool<PressedButtonTagComponent>().Has(_initButtonEntity.myEntity))
            _initButtonEntity.mainWorld.GetPool<PressedButtonTagComponent>().Add(_initButtonEntity.myEntity);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(_initButtonEntity.targetTag)) return;

        var unpresBtn = _initButtonEntity.mainWorld.GetPool<UnpressedButtonTagComponent>();
        if (!unpresBtn.Has(_initButtonEntity.myEntity))
        {
            unpresBtn.Add(_initButtonEntity.myEntity);
        }
    }
}