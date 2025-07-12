using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //シングルトンのインスタンス
    public static ScoreManager instance { get;private set; }
    
    [Header("UIの参照")]
    [SerializeField] private Text _scoreText;// UIテキスト
    
    // スコアは他のクラスから取得
    private int _score = 0;
    public int Score  => _score;

    public bool isCountUp = false;

    void Awake()
    {
        isCountUp = false;
        // シングストンの基本実装
        if (instance == null)
        {
            instance = this;
            
            // シーンをまたいでも保持
            DontDestroyOnLoad(gameObject);
            Debug.Log("[ScoreManager] インスタンス生成＆永続化されました");
        }
        else
        {
            // すでに存在する場合は破棄
            Destroy(gameObject);
            Debug.Log("[ScoreManager] 既に存在するため破棄されました");
        }
    }
    
    // スコア加算のメソッド
    public void AddScore(int amount)
    {
        //Enemyなどから加算
        _score += amount;
        Debug.Log($"[ScoreManager] スコア加算: +{amount}（現在のスコア: {_score}）");
        
        UpdateScoreText();
    }
    
    // スコア更新
    private void UpdateScoreText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score : " + _score.ToString();
            Debug.Log($"[ScoreManager] スコアUI更新: {_scoreText.text}");
        }
        else
        {
            Debug.LogWarning("[ScoreManager] _scoreText が未設定です！");
        }
    }
}
