using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;// シングルトン
    float _timer;
    public float Timer => _timer;

    public bool IsCountUp => _timer >= 15;// 経過時間が15秒以上ならtrueを返す
    　
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);// シーンが変わっても残す
        }
        else
        {
            Destroy(gameObject);// すでに存在していたら破棄
        }
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;//毎フレーム時間を加算、_timerを進める
    }
}
