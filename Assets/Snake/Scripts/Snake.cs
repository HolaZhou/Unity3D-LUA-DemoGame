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
/*脚本名(name):  Snake
  作者(author):  周志强
  时间(time):     #CREATIONDATE#
  描述(description):
  
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    #region public variables

    #endregion
    #region private variables
    public enum SnakeDirection
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    public float snakeMoveSpeed = 0.1f;
    private bool isCanChangeDirection = false;
    private float snakeMoveDistance = 0.4f;
    public SnakeDirection currentSnakeDirection = SnakeDirection.RIGHT;
    public GameObject snakeBodyPre;
    private Transform snakeParentNode, snakeHead;
    private List<Transform> snakeBodyList = new List<Transform>();
    #endregion
    void Start()
    {
        snakeParentNode = this.transform;
        InitSnakeBody();
        StartCoroutine(SnakeMove());
    }

    IEnumerator SnakeMove()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            yield return new WaitForSeconds(snakeMoveSpeed);
            if (GameManager.instance.isGameStart)
            {
                _SnakeMove();
            }
        }
    }

    void InitSnakeBody()
    {
        for (int i = 0; i < 3; i++)
        {
            snakeBodyList.Add(Instantiate<GameObject>(snakeBodyPre, snakeParentNode).transform);
            snakeBodyList[i].gameObject.SetActive(true);
            GameManager.instance.SetSnakeBody(snakeBodyList[i].gameObject);
        }
        snakeHead = snakeBodyList[0];
        snakeHead.transform.position = new Vector3(0, 0, 0);
        snakeHead.GetComponent<SpriteRenderer>().material.color = Color.red;
        snakeBodyList[1].transform.position = new Vector3(-snakeMoveDistance * 1, 0, 0);
        snakeBodyList[2].transform.position = new Vector3(-snakeMoveDistance * 2, 0, 0);
    }

    void Update()
    {
        CheckFood();
        KeyBoardListener();
    }

    void KeyBoardListener()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentSnakeDirection != SnakeDirection.DOWN)
        {
            currentSnakeDirection = SnakeDirection.UP;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && currentSnakeDirection != SnakeDirection.UP)
        {
            currentSnakeDirection = SnakeDirection.DOWN;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentSnakeDirection != SnakeDirection.RIGHT)
        {
            currentSnakeDirection = SnakeDirection.LEFT;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentSnakeDirection != SnakeDirection.LEFT)
        {
            currentSnakeDirection = SnakeDirection.RIGHT;
        }
    }

    void _SnakeMove()
    {
        if (snakeBodyList.Count <= 0 || snakeBodyList == null)
        {
            return;
        }
        for (int i = snakeBodyList.Count - 1; i > 0; i--)
        {
            snakeBodyList[i].transform.position = snakeBodyList[i - 1].transform.position;
        }
        Vector3 snakeHeadCurPos = snakeBodyList[0].transform.position;
        Vector3 snakeHeadNextPos = new Vector3();
        if (currentSnakeDirection == SnakeDirection.UP)
        {
            snakeHeadNextPos = new Vector3(snakeHeadCurPos.x, snakeHeadCurPos.y + snakeMoveDistance, 0);
        }
        else if (currentSnakeDirection == SnakeDirection.DOWN)
        {
            snakeHeadNextPos = new Vector3(snakeHeadCurPos.x, snakeHeadCurPos.y - snakeMoveDistance, 0);
        }
        else if (currentSnakeDirection == SnakeDirection.LEFT)
        {
            snakeHeadNextPos = new Vector3(snakeHeadCurPos.x - snakeMoveDistance, snakeHeadCurPos.y, 0);
        }
        else if (currentSnakeDirection == SnakeDirection.RIGHT)
        {
            snakeHeadNextPos = new Vector3(snakeHeadCurPos.x + snakeMoveDistance, snakeHeadCurPos.y, 0);
        }
        else
        {
            snakeHeadNextPos = new Vector3(snakeHeadCurPos.x + snakeMoveDistance, snakeHeadCurPos.y, 0);
        }
        snakeBodyList[0].transform.position = snakeHeadNextPos;
    }

    void OnEatFood()
    {
        SnakeGrowUp();
    }

    void SnakeGrowUp()
    {
        snakeBodyList.Add(Instantiate<GameObject>(snakeBodyPre, snakeParentNode).transform);
        int growSnakeBodyIndex = snakeBodyList.Count - 1;
        snakeBodyList[growSnakeBodyIndex].gameObject.SetActive(true);
        GameManager.instance.SetSnakeBody(snakeBodyList[growSnakeBodyIndex].gameObject);
        snakeBodyList[growSnakeBodyIndex].transform.position = snakeBodyList[growSnakeBodyIndex - 1].transform.position;
    }

    List<GameObject> allFood = new List<GameObject>();
    void CheckFood()
    {
        if (!GameManager.instance.isGameStart) return;
        allFood = GameManager.instance.GetAllFood();
        if (allFood.Count <= 0 || allFood == null) return;
        for (int i = 0; i < allFood.Count; i++)
        {
            if (Vector3.Distance(allFood[i].transform.position, snakeHead.position) <= 0.2f)
            {
                DestroyImmediate(allFood[i]);
                OnEatFood();
            }
        }
    }

    void OnDestroy()
    {

    }
}
