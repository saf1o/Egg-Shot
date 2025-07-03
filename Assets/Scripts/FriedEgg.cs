using System.Collections;
using UnityEngine;

public class FriedEgg : MonoBehaviour
{
    [SerializeField] private float destroyTime = 2f;//消されるまでの時間

    // Start is called before the first frame update
    void Start()
    {
        Destroy();//時間経過で消す
    }
    
    private void Destroy()
    {
        StartCoroutine(DestroyFriedEgg());//時間を使った処理、下を呼び出す
    }

    private IEnumerator DestroyFriedEgg()
    {
        yield return new WaitForSeconds(destroyTime);//一定時間処理を待ってから下を実行する
        
        Destroy(gameObject);
    }
}
