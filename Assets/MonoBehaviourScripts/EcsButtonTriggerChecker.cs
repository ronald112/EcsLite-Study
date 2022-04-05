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

    private int _entity = 0;
    private EcsWorld _mainWorld;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_targetTag)) return;
        
        _mainWorld = WorldHandler.GetMainWorld();
        _entity = _mainWorld.NewEntity();

        ref var activeButton = ref _mainWorld.GetPool<ActiveButtonComponent>().Add(_entity);
        activeButton.color = _color;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(_targetTag)) return;
        
        WorldHandler.GetMainWorld().GetPool<ActiveButtonComponent>().Del(_entity);
    }
}