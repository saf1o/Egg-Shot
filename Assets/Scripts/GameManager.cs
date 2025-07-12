using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;// シングルトン
    float _timer;
    public float Timer => _timer;

    // 経過時間が15秒以上ならtrueを返す
    public bool IsCountUp => _timer >= 15;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            // シーンが変わっても残す
            DontDestroyOnLoad(gameObject);
            Debug.Log("[GameManager] インスタンス生成＆永続化");
        }
        else
        {
            // すでに存在していたら破棄
            Destroy(gameObject);
            Debug.Log("[GameManager] 既に存在していたため破棄されました");
        }
    }
    
    void Update()
    {
        //毎フレーム時間を加算、_timerを進める
        _timer += Time.deltaTime;
        if (Mathf.FloorToInt(_timer) % 1 == 0)
        {
            Debug.Log("$\"[GameManager] 経過時間: {_timer:F2} 秒\"");
        }
    }
}
