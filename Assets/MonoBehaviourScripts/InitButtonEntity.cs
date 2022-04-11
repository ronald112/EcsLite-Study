using System;
using Client;
using Enums;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;
using Zenject;

public class InitButtonEntity : MonoBehaviour
{
    private EcsFilter _ecsFilter = null;
    [NonSerialized] public EcsWorld mainWorld = null;
    [NonSerialized] public int myEntity = -1;
    
    [SerializeField] public string targetTag = "Player";
    
    [SerializeField] public ModelColor color = ModelColor.Undefined;

    void Start()
    {
        mainWorld = WorldHandler.GetMainWorld();
        EcsPool<ModelColorComponent> poolModelColor = mainWorld.GetPool<ModelColorComponent>();
        _ecsFilter = mainWorld.Filter<ButtonTag>().Inc<ModelColorComponent>().End();
        foreach (var entity in _ecsFilter)
        {
            ref var color = ref poolModelColor.Get(entity).color;
            if (color != this.color) continue;

            myEntity = entity;
            break;
        }

        var buttonMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        ref var moveByTwoPoints = ref mainWorld.GetPool<PathByTwoPointsComponent>().Add(myEntity);
        moveByTwoPoints.start = transform.position;
        moveByTwoPoints.end = transform.position;
        moveByTwoPoints.end.y -= buttonMeshRenderer.bounds.size.y * 0.7f;
    }

}
