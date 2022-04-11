using System;
using Client;
using Enums;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

public class InitDoorEntity : MonoBehaviour
{
    private EcsFilter _ecsFilter = null;
    private EcsWorld _mainWorld = null;
    
    [SerializeField] public ModelColor color = ModelColor.Undefined;
    
    [Range(0, 5)] [SerializeField] private float _openingSpeed = 1.0f;

    void Start()
    {
        _mainWorld = WorldHandler.GetMainWorld();
        EcsPool<ModelColorComponent> poolModelColor = _mainWorld.GetPool<ModelColorComponent>();
        _ecsFilter = _mainWorld.Filter<DoorTag>().Inc<ModelColorComponent>().End();
        int myEntity = -1;
        foreach (var entity in _ecsFilter)
        {
            ref var colorComponent = ref poolModelColor.Get(entity).color;
            if (colorComponent != this.color) continue;

            myEntity = entity;
            break;
        }

        var buttonMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        ref var moveByTwoPoints = ref _mainWorld.GetPool<PathByTwoPointsComponent>().Add(myEntity);
        moveByTwoPoints.start = transform.position;
        moveByTwoPoints.end = transform.position;
        moveByTwoPoints.end.y -= buttonMeshRenderer.bounds.size.y * 1.1f;
        
        _mainWorld.GetPool<DoorReadyToMoveTag>().Add(myEntity);
        ref var openingSpeedComponent = ref _mainWorld.GetPool<SpeedComponent>().Add(myEntity);
        openingSpeedComponent.speed = _openingSpeed;
    }
}
