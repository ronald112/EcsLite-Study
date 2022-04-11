using Leopotam.EcsLite;

namespace Client
{
    sealed class DoorInitOpeningByButtonPressSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter _filterPressedButton = null;
        private EcsPool<PathByTwoPointsComponent> _poolTwoPoints = null;
        private EcsPool<ModelColorComponent> _poolModelColor = null;
        private EcsPool<ModelTransformComponent> _poolModelTransform = null;
        private EcsFilter _filterClosedDoor = null;
        private EcsPool<MoveToCoordinateComponent> _poolMoveToCoordinate = null;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterPressedButton = _world.Filter<PressedButtonEvent>().Inc<ModelColorComponent>().End();
            
            _filterClosedDoor = _world.Filter<DoorTag>().Inc<ModelColorComponent>()
                .Inc<PathByTwoPointsComponent>().Inc<ModelTransformComponent>().Exc<DoorOpenedTag>().End();
            
            _poolTwoPoints = _world.GetPool<PathByTwoPointsComponent>();
            _poolModelColor = _world.GetPool<ModelColorComponent>();
            _poolModelTransform = _world.GetPool<ModelTransformComponent>();
            _poolMoveToCoordinate = _world.GetPool<MoveToCoordinateComponent>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var buttonEntity in _filterPressedButton)
            {
                var buttonColorComponent = _poolModelColor.Get(buttonEntity);
                var buttonColor = buttonColorComponent.color;

                foreach (var doorEntity in _filterClosedDoor)
                {
                    
                    var doorColorComponent = _poolModelColor.Get(doorEntity);
                    var doorColor = doorColorComponent.color;

                    if (buttonColor != doorColor) continue;

                    ref var moveByTwoPointsComponent = ref _poolTwoPoints.Get(doorEntity);

                    if (!_poolMoveToCoordinate.Has(doorEntity))
                    {
                        ref var moveToCoordinateComponent = ref _poolMoveToCoordinate.Add(doorEntity);
                        
                        moveToCoordinateComponent.toCoordinate = moveByTwoPointsComponent.end;
                    }
                }
            }
        }
    }
}