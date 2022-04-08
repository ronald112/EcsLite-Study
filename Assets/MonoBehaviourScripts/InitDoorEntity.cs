using System;
using Client;
using Enums;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

public class InitDoorEntity : MonoBehaviour
{
    private EcsFilter _ecsFilter = null;
    [NonSerialized] public EcsWorld mainWorld = null;
    [NonSerialized] public int myEntity = -1;
    
    [SerializeField] public ModelColor color = ModelColor.Undefined;

    void Start()
    {
        mainWorld = WorldHandler.GetMainWorld();
        EcsPool<ModelColorComponent> poolModelColor = mainWorld.GetPool<ModelColorComponent>();
        _ecsFilter = mainWorld.Filter<DoorTag>().Inc<ModelColorComponent>().End();
        foreach (var entity in _ecsFilter)
        {
            ref var color = ref poolModelColor.Get(entity).color;
            if (color != this.color) continue;

            myEntity = entity;
            break;
        }

        var buttonMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        ref var moveByTwoPoints = ref mainWorld.GetPool<MoveByTwoPointsComponent>().Add(myEntity);
        moveByTwoPoints.start = transform.position;
        moveByTwoPoints.end = transform.position;
        moveByTwoPoints.end.y -= buttonMeshRenderer.bounds.size.y * 1.1f;
        
        mainWorld.GetPool<DoorReadyToMoveTag>().Add(myEntity);
    }

}
