using System.Collections;
using UnityEngine;

public class FriedEgg : MonoBehaviour
{
    [SerializeField] private float destroyTime = 2f;//消されるまでの時間
    
    void Start()
    {
        Debug.Log($"[FriedEgg] 目玉焼き生成: {gameObject.name}, 削除まで {destroyTime} 秒");
        
        //時間経過で消す
        Destroy();
    }
    
    private void Destroy()
    {
        //時間を使った処理、下を呼び出す
        StartCoroutine(DestroyFriedEgg());
    }

    private IEnumerator DestroyFriedEgg()
    {
        //一定時間処理を待ってから下を実行する
        yield return new WaitForSeconds(destroyTime);
        Debug.Log($"[FriedEgg] 目玉焼き削除: {gameObject.name}");
        Destroy(gameObject);
    }
}
