using System.Collections;
using UnityEngine;

public class FriedEgg : MonoBehaviour
{
    [SerializeField] private float destroyTime = 2f;//�������܂ł̎���

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"[FriedEgg] �ڋʏĂ�����: {gameObject.name}, �폜�܂� {destroyTime} �b");
        Destroy();//���Ԍo�߂ŏ���
    }
    
    private void Destroy()
    {
        StartCoroutine(DestroyFriedEgg());//���Ԃ��g���������A�����Ăяo��
    }

    private IEnumerator DestroyFriedEgg()
    {
        yield return new WaitForSeconds(destroyTime);//��莞�ԏ�����҂��Ă��牺�����s����
        Debug.Log($"[FriedEgg] �ڋʏĂ��폜: {gameObject.name}");
        Destroy(gameObject);
    }
}
