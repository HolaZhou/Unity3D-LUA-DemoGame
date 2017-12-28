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
/*脚本名(name):  GameManager
  作者(author):  周志强
  时间(time):     #CREATIONDATE#
  描述(description):
  
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;
public class GameManager : MonoBehaviour
{
    #region public variables
    public static GameManager instance = null;
    public bool isGameStart = false;
    private Button startBtn;
    private List<GameObject> allFood = new List<GameObject>();
    private List<GameObject> snakeBody = new List<GameObject>();
    #endregion
    #region private variables

    #endregion

    void Awake()
    {
        instance = this;
        //InitUI();
    }

    void Update()
    {
        if (isGameStart)
        {

        }
    }

    void InitUI()
    {
        startBtn = this.transform.FindChild("UIRoot/Canvas/OperatePanel/StartBtn").GetComponent<Button>();
        startBtn.onClick.AddListener(delegate
        {
            if (isGameStart)
            {
                isGameStart = false;
                startBtn.GetComponentInChildren<Text>().text = "Start";
            }
            else
            {
                isGameStart = true;
                startBtn.GetComponentInChildren<Text>().text = "Stop";
            }
        });
    }

    public void SetSnakeBody(GameObject body)
    {
        if (snakeBody.Contains(body))
        {
            snakeBody.Remove(body);
        }
        snakeBody.Add(body);
    }

    public List<GameObject> GetSnakeBody()
    {
        return snakeBody;
    }

    public void SetFood(GameObject food)
    {
        if (allFood.Contains(food))
        {
            allFood.Remove(food);
        }
        allFood.Add(food);
    }

    public List<GameObject> GetAllFood()
    {
        for (int i = 0; i < allFood.Count; i++)
        {
            if (allFood[i] == null)
            {
                allFood.Remove(allFood[i]);
            }
        }
        return allFood;
    }

    void OnDestroy()
    {

    }
}
