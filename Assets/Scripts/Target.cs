using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private Transform[] _target;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] float _stoppingDistance = 0.05f;
    
    int _currentTargetIndex = 0;

    void Update()
    {
        float distance = Vector2.Distance(this.transform.position, _target[_currentTargetIndex].position);
        if (distance > _stoppingDistance)
        {
            Vector3 dir = (_target[_currentTargetIndex].transform.position - this.transform.position).normalized *
                          _moveSpeed;
            this.transform.Translate(dir * Time.deltaTime);
        }
        else
        {
            _currentTargetIndex++;
            _currentTargetIndex %= _target.Length;

        }
    }
}
