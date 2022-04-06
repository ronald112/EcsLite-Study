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

    private Vector3 _doorBoundsSize = Vector3.zero;
    private Vector3 _buttonBoundsSize = Vector3.zero;
    
    private int _entity = -1;
    private EcsWorld _mainWorld;

    private void Start()
    {
        // _doorBoundsSize.x = _doorMeshRenderer.bounds.size.x;
        // _doorBoundsSize.y = _doorMeshRenderer.bounds.size.y;
        // _doorBoundsSize.z = _doorMeshRenderer.bounds.size.z;

        var buttonMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        _buttonBoundsSize.x = buttonMeshRenderer.bounds.size.x;
        _buttonBoundsSize.y = buttonMeshRenderer.bounds.size.y;
        _buttonBoundsSize.z = buttonMeshRenderer.bounds.size.z;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_targetTag)) return;
        
        _mainWorld = WorldHandler.GetMainWorld();
        if (_entity == -1)
            _entity = _mainWorld.NewEntity();

        ref var activeButton = ref _mainWorld.GetPool<PressedButtonTagComponent>().Add(_entity);
        ref var modelColor = ref _mainWorld.GetPool<ModelColorComponent>().Add(_entity);
        // ref var modelBounds = ref _mainWorld.GetPool<ModelBoundsComponent>().Add(_entity);
        ref var moveTo = ref _mainWorld.GetPool<MoveToCoordinateComponent>().Add(_entity);
        ref var movableProp = ref _mainWorld.GetPool<MovablePropComponent>().Add(_entity);
        ref var transfomComponent = ref _mainWorld.GetPool<ModelTransformComponent>().Add(_entity);
        modelColor.color = _color;
        // modelBounds.bounds = _buttonBoundsSize;
        transfomComponent.transform = transform;
        moveTo.coordinate = transform.position;
        moveTo.coordinate.y -= _buttonBoundsSize.y * 0.7f;
        
        Debug.Log($"{_buttonBoundsSize}");

        var doorFilter = _mainWorld.Filter<PressedButtonTagComponent>().End();

        foreach (var doorEntity in doorFilter)
        {
            // add to founded found door moveComponent
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(_targetTag)) return;
        
        ref var moveTo = ref _mainWorld.GetPool<MoveToCoordinateComponent>().Add(_entity);
        ref var movableProp = ref _mainWorld.GetPool<MovablePropComponent>().Add(_entity);
        ref var transfomComponent = ref _mainWorld.GetPool<ModelTransformComponent>().Add(_entity);
        // modelBounds.bounds = _buttonBoundsSize;
        transfomComponent.transform = transform;
        
        
        WorldHandler.GetMainWorld().GetPool<PressedButtonTagComponent>().Del(_entity);
    }
}