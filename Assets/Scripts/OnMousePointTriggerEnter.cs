using UnityEngine;

public class OnMousePointTriggerEnter : MonoBehaviour
{
    private string _targetTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_targetTag)) return;
        
        Destroy(transform.parent.gameObject);
    }
}
