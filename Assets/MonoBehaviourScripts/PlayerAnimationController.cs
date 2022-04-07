using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using Zenject;

public class PlayerAnimationController : MonoBehaviour
{
    [Inject] private ThirdPersonCharacter _character;

    [Inject] private NavMeshAgent _navMeshAgent;

    void Start()
    {
        _navMeshAgent.updateRotation = false;
    }


    void Update()
    {
        if (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance)
            _character.Move(_navMeshAgent.desiredVelocity, false, false);
        else
            _character.Move(Vector3.zero, false, false);
    }
}
