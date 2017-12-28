//
//                       _oo0oo_
//                      o8888888o
//                      88" . "88
//                      (| -_- |)
//                      0\  =  /0
//                    ___/`---'\___
//                  .' \|     |// '.
//                 / \|||  :  |||// \
//                / _||||| -:- |||||- \
//               |   | \  -  /// |     |
//               | \_|  ''\---/''  |_/ |
//               \  .-\__  '-'  ___/-. /
//             ___'. .'  /--.--\  `. .'___
//          ."" '<  `.___\_<|>_/___.' >' "".
//         | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//         \  \ `_.   \_ __\ /__ _/   .-` /  /
//     =====`-.____`.___ \_____/___.-`___.-'=====
//                       `=---='
//
//
//     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//
//               佛祖保佑         永无BUG
//
//
/*脚本名(name):  Food
  作者(author):  周志强
  时间(time):     #CREATIONDATE#
  描述(description):
  
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    #region public variables
    public GameObject foodPre;
    public Transform foodPosLeftUpLimiteTrans, foodPosRightDownLimiteTrans;
    #endregion
    #region private variables
    private float createFoodDeltaTime = 2;
    private float foodLiveTime = 10f;
    private List<Vector3> allFoodPlacePos = new List<Vector3>();
    private Transform foodContainer;
    #endregion

    void Start()
    {
        foodContainer = this.transform;
        GetAllFoodPlacePos();
        StartCoroutine(CreateFood());
    }

    void GetAllFoodPlacePos()
    {
        float tempX = foodPosLeftUpLimiteTrans.position.x;
        float limiteX = foodPosRightDownLimiteTrans.position.x;
        float tempY = foodPosLeftUpLimiteTrans.position.y;
        float limiteY = foodPosRightDownLimiteTrans.position.y;
        while (tempX < limiteX - 0.4f)
        {
            tempX += 0.4f;
            tempY = foodPosLeftUpLimiteTrans.position.y;
            while (tempY > limiteY + 0.4f)
            {
                tempY -= 0.4f;
                //GameObject go = new GameObject();
                allFoodPlacePos.Add(new Vector3(tempX, tempY, 0));
                //go.transform.position = new Vector3(tempX, tempY, 0);
            }
        }
    }

    IEnumerator CreateFood()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            yield return new WaitForSeconds(createFoodDeltaTime);
            _CreateFood();
        }
    }

    void _CreateFood()
    {
        GameObject food = Instantiate<GameObject>(foodPre, foodContainer);
        GameManager.instance.SetFood(food);
        food.gameObject.SetActive(true);
        DestroyObject(food, foodLiveTime);
        food.transform.position = RandomFoodPos();
    }

    Vector3 RandomFoodPos()
    {
        int index = UnityEngine.Random.Range(0, allFoodPlacePos.Count);
        Vector3 pos = allFoodPlacePos[index];
        while (IsCoverExistFoodOrSnake(pos))
        {
            index = UnityEngine.Random.Range(0, allFoodPlacePos.Count);
            pos = allFoodPlacePos[index];
        }
        return pos;
    }

    bool IsCoverExistFoodOrSnake(Vector3 pos)
    {
        List<GameObject> tempFood = GameManager.instance.GetAllFood();
        List<GameObject> tempSnake = GameManager.instance.GetSnakeBody();
        for (int i = 0; i < tempFood.Count; i++)
        {
            if (Vector3.Distance(pos, tempFood[i].transform.position) < 0.5f)
            {
                return true;
            }
        }
        for (int i = 0; i < tempSnake.Count; i++)
        {
            if (Vector3.Distance(pos, tempSnake[i].transform.position) < 0.5f)
            {
                return true;
            }
        }
        return false;
    }


    void OnDestroy()
    {

    }
}
