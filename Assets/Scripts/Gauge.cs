using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    [SerializeField] private Image _gauge;// ゲージのImageコンポーネント
    [SerializeField] private float _fillSpeed;// ゲージにの増減速度
    
    
    private bool _increase = false;// ゲージが増量中かどうかのフラグ
    public bool _isCharging = false;// ゲージのチャージ中かどうかのフラグ
    public float _fillAmount;// 現在のゲージの割合(0~1)

    private void Update()
    {
        // チャージ中のみ処理を行う
        if (_isCharging == true)
        {
            if (_increase == true)
            {
                // ゲージを増加させる(毎秒_fillSpeed分増える)
                _gauge.fillAmount += _fillSpeed * Time.deltaTime;
                _fillAmount = _gauge.fillAmount;
                
                // ゲージが最大値(1,0)を超えないようにし、増加フラグをfalseに切り替え
                if (_gauge.fillAmount >= 1f)
                {
                    _gauge.fillAmount = 1f;
                    _increase = false;// これ以上増やさず減少に切り替え
                }
            }
            else
            {
                // ゲージを減少させる(毎秒_fillSpeed分減る)
                _gauge.fillAmount -= _fillSpeed * Time.deltaTime;
                _fillAmount = _gauge.fillAmount;// 現在のゲージ値を保持
                
                // ゲージが最小値(0.0)を下回らないようにし、増加フラグをtrueに切り替え
                if (_gauge.fillAmount <= 0f)
                {
                    _gauge.fillAmount = 0f;
                    _increase = true;// これ以上減らさずに切り替え
                }
            }
        }
    }
}
