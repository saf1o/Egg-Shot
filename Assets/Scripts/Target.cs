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
        if (_target.Length == 0)
        {
            Debug.LogWarning("ターゲットが設定されていません。");
            return;
        }
        
        float distance = Vector2.Distance(this.transform.position, _target[_currentTargetIndex].position);
        Debug.Log($"[Target] 現在のターゲット {_currentTargetIndex} までの距離: {distance:F2}");
        
        if (distance > _stoppingDistance)
        {
            Vector3 dir = (_target[_currentTargetIndex].transform.position - this.transform.position).normalized *
                          _moveSpeed;
            this.transform.Translate(dir * Time.deltaTime);
        }
        else
        {
            Debug.Log($"[Target] ターゲット {_currentTargetIndex} に到達");
            _currentTargetIndex++;
            _currentTargetIndex %= _target.Length;
            Debug.Log($"[Target] 次のターゲットに移動：{_currentTargetIndex}");
        }
    }
}
