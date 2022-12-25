using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;



public class Game : MonoBehaviour
{
    public Text scoreText;
    private int score;   //Игровая валюта
    private int bonus = 1;
    private int workersCount, workersBonus = 1;
    public int[] ShopIdleCosts;
    public int[] ShopBonusesIdle;
    public Text[] shopButtonsIdleText;
    public GameObject ShopIdle;
    public Button[] ShopButtons;
    public float[] BonusTime;

    private Save sv = new Save();


    public void Start()
    {
        StartCoroutine(BonusPerSec()); //Запустить просчёт бонуса в секунду
    }

   

    public void Update()
    {
        scoreText.text = score + "▐";
    }
    [Header("Магазин кликов")]
    public GameObject ShopClick;
    public int[] ShopCosts;
    public int[] ShopBonuses;
    public Text[] shopButtonsText;





    public void ShopButtonBonus(int index)
    {
        if (score >= ShopCosts[index])
        {
            bonus += ShopBonuses[index];
            score -= ShopCosts[index];
            ShopCosts[index] *= 2;
            shopButtonsText[index].text = ShopCosts[index] + "▐";
        }
        else
        {
            Debug.Log("Не хватает энергии");
        }
    }

    public void HireWorker(int index)
    {
        if (score >= ShopIdleCosts[index])
        {
            workersCount += ShopBonusesIdle[index] ;
            score -= ShopIdleCosts[index];
            ShopIdleCosts[index] *= 2;
            shopButtonsIdleText[index].text = ShopIdleCosts[index] + "▐";
        }
    }

   /* public void StartBonusTimer(int index)
    {
        int cost = 2 * workersCount;
        shopButtonsText[2].text = ShopCosts[index] + cost + "▐";
        if (score > cost)
        {
            StartCoroutine(BonusTimer(BonusTime[index], index));
            score -= cost;
        }
    }*/

    IEnumerator BonusPerSec() // Бонус в секунду
    {
        while (true) // Бесконечный цикл
        {
            
            score += (workersCount* workersBonus);
            yield return new WaitForSeconds(1); // Делаем задержку в 1 секунду
        }

    }

    IEnumerator BonusTimer (float time, int index)
    {
        ShopButtons[index].interactable = false;
        if (index == 0 && workersCount > 0)
        {
            workersBonus *= 2;
            yield return new WaitForSeconds(time);
            workersBonus /= 2;
        }
        ShopButtons[index].interactable = true;
    }

    public void ShopClicks()
    {
        if (ShopIdle.activeSelf)
        {
            ShopIdle.SetActive(!ShopIdle.activeSelf);
        }
        ShopClick.SetActive(!ShopClick.activeSelf);
    }

    public void ShopIdles()
    {
        if (ShopClick.activeSelf)
        {
            ShopClick.SetActive(!ShopClick.activeSelf);
        }
        ShopIdle.SetActive(!ShopIdle.activeSelf);
    }

    public void OnClick()
    {
        score += bonus;
    }
    [Serializable]
    public class Save
    {
        public int score;
        public int[] levelOfItem;
        public int[] bonusCounter;
    }
        
}
