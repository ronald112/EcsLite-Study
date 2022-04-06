using System;
using Client;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;
using Zenject;

public class EcsButtonTriggerChecker : MonoBehaviour
{
    [Inject]
    [SerializeField] private string _targetTag = "Player";
    
    [SerializeField] private ModelColor _color = ModelColor.Undefined;

    [SerializeField] private MeshRenderer _doorMeshRenderer = null;
    
    private int _entity = -1;
    private EcsWorld _mainWorld;
    private EcsFilter _ecsFilter;

    private void Start()
    {
        // _doorBoundsSize.x = _doorMeshRenderer.bounds.size.x;
        // _doorBoundsSize.y = _doorMeshRenderer.bounds.size.y;
        // _doorBoundsSize.z = _doorMeshRenderer.bounds.size.z;
        
        _mainWorld = WorldHandler.GetMainWorld();

        _ecsFilter = _mainWorld.Filter<ButtonTag>().Inc<ModelColorComponent>().End();
        EcsPool<ModelColorComponent> poolModelColor = _mainWorld.GetPool<ModelColorComponent>();
        foreach (var entity in _ecsFilter)
        {
            ref var color = ref poolModelColor.Get(entity).color;
            if (color != _color) continue;

            _entity = entity;
            break;
        }

        var buttonMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        ref var moveByTwoPoints = ref _mainWorld.GetPool<MoveByTwoPointsComponent>().Add(_entity);
        moveByTwoPoints.start = transform.position;
        moveByTwoPoints.end = transform.position;
        moveByTwoPoints.end.y -= buttonMeshRenderer.bounds.size.y * 0.7f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_targetTag)) return;
        
        if (!_mainWorld.GetPool<PressedButtonTagComponent>().Has(_entity))
            _mainWorld.GetPool<PressedButtonTagComponent>().Add(_entity);
        
        var doorFilter = _mainWorld.Filter<DoorTag>().Inc<ModelColorComponent>().End();
        foreach (var doorEntity in doorFilter)
        {
            // add founded door logic
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(_targetTag)) return;

        var unpresBtn = _mainWorld.GetPool<UnpressedButtonTagComponent>();
        if (!unpresBtn.Has(_entity))
        {
            unpresBtn.Add(_entity);
        }
    }
}