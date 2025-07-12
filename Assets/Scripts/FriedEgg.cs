using System.Collections;
using UnityEngine;

public class FriedEgg : MonoBehaviour
{
    [SerializeField] private float destroyTime = 2f;//�������܂ł̎���
    
    void Start()
    {
        Debug.Log($"[FriedEgg] �ڋʏĂ�����: {gameObject.name}, �폜�܂� {destroyTime} �b");
        
        //���Ԍo�߂ŏ���
        Destroy();
    }
    
    private void Destroy()
    {
        //���Ԃ��g���������A�����Ăяo��
        StartCoroutine(DestroyFriedEgg());
    }

    private IEnumerator DestroyFriedEgg()
    {
        //��莞�ԏ�����҂��Ă��牺�����s����
        yield return new WaitForSeconds(destroyTime);
        Debug.Log($"[FriedEgg] �ڋʏĂ��폜: {gameObject.name}");
        Destroy(gameObject);
    }
}
