using TMPro;
using UnityEngine;

public class CurrencyLogic : MonoBehaviour
{

    public int wallCostParamaeter;
    public int turretCostParamaeter;
    public int startCurrencyParameter;

    public TextMeshProUGUI GoldIndicator;
    public TextMeshProUGUI KilledIndicator;
    public TextMeshProUGUI AliveIndicator;
    public TextMeshProUGUI LifesIndicator;
    public TextMeshProUGUI wallCostIndicator;
    public TextMeshProUGUI turretCostIndicator;

    public GameObject gameOverMenu;
    private bool isGameOver;
    private static CurrencyLogic currencyLogic;



    private CurrencyLogic()
    {

    }

    public static CurrencyLogic getinstance()
    {
        if (!currencyLogic)
        {
            currencyLogic = new CurrencyLogic();
            return currencyLogic;
        }
        else
        {
            return currencyLogic;
        }
    }


    public static int Currency;
    public static int Killed;
    public static int Alive;
    public static int Lifes;
    public static int wallCost;
    public static int turretCost;

    private bool valueChanged;

    public int GetKilled()
    {
        return Killed;
    }

    private void SetKilled(int value)
    {
        Killed = value;
    }

    public int GetAlives()
    {
        return Alive;
    }

    private void SetAlive(int value)
    {
        Alive = value;
    }

    public int GetLifes()
    {
        return Lifes;
    }

    private void SetLifes(int value)
    {
        Lifes = value;
    }

    public void AddKill()
    {
        Killed++;
    }

    public void AddAlive()
    {
        Alive++;
    }

    public void DecreaseAlive()
    {
        Alive--;
    }

    public void DecreaseLifes()
    {
        if (Lifes > 0)
        {
            Lifes--;
        }


    }

    public int GetCurrency()
    {
        return Currency;
    }

    public void AddAmountToCurrency(int amount)
    {
        Currency += amount;
    }

    public void DecreaseCurrencyByAmount(int amount)
    {
        Currency -= amount;
    }

    public void UpdateCurrencyOnDisplay()
    {
        valueChanged = true;

    }

    public bool canBuyWall()
    {
        if (currencyLogic.GetCurrency() > wallCost)
        {
            return true;
        }
        return false;
    }

    public void BuyWall()
    {
        currencyLogic.DecreaseCurrencyByAmount(wallCost);

    }

    public bool canBuyTurret()
    {


        if (currencyLogic.GetCurrency() > turretCost)
        {
            return true;
        }
        return false;
    }

    public void BuyTurret()
    {
        currencyLogic.DecreaseCurrencyByAmount(turretCost);
    }

    public int GetWallCost()
    {
        return wallCost;
    }

    public int GetTurretCost()
    {
        return turretCost;
    }

    void Start()
    {
        Killed = 0;
        Alive = 0;
        Lifes = 100;
        valueChanged = true;
        Currency = startCurrencyParameter;
        isGameOver = false;


        wallCost = wallCostParamaeter;
        turretCost = turretCostParamaeter;
        wallCostIndicator.text = wallCost.ToString();
        turretCostIndicator.text = turretCost.ToString();


    }

    void Update()
    {
        if (Alive <= 0) { Alive = 0; }

        GoldIndicator.text = Currency.ToString();
        KilledIndicator.text = Killed.ToString();
        AliveIndicator.text = Alive.ToString();
        LifesIndicator.text = Lifes.ToString();
        if (Lifes == 0)
        {
            if (!isGameOver)
            {
                gameOverMenu.SetActive(!isGameOver);
                Time.timeScale = 0;
            }
            isGameOver = true;


        }

    }
}
