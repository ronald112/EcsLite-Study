using Client;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;
using Zenject;

public class PlayerInputManager : MonoBehaviour
{
    [Inject]
    [SerializeField] private Camera _cam = null;
    
    

    void Update()
    {
        if (_cam == null) throw new UnassignedReferenceException();
        if (!Input.GetMouseButtonDown(0)) return;
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out var hit)) return;

        EcsWorld mainWorld = WorldHandler.GetMainWorld();
        int entity = mainWorld.NewEntity();
        ref var component = ref mainWorld.GetPool<MoveToCoordinateComponent>().Add(entity);
        component.coordinate = hit.point;
    }
}
