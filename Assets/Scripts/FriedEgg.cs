using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriedEgg : MonoBehaviour
{
    [SerializeField] private float destroyTime = 2f;//�������܂ł̎���

    // Start is called before the first frame update
    void Start()
    {
        Destroy();//�������ꂽ�u�ԂɌĂяo��
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Destroy()
    {
        StartCoroutine(DestroyFriedEgg());//���Ԃ��g���������A�����Ăяo��
    }

    private IEnumerator DestroyFriedEgg()
    {
        yield return new WaitForSeconds(destroyTime);//��莞�ԏ�����҂��Ă��牺�����s����
        Destroy(gameObject);
    }
}
