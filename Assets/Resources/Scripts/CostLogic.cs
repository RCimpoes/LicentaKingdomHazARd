//using TMPro;
//using UnityEngine;

//public class CostLogic : MonoBehaviour {

//    public int wallCostParamaeter;
//    public int turretCostParamaeter;


//    public static int wallCost;
//    public static int turretCost;

//    public TextMeshProUGUI wallCostIndicator;
//    public TextMeshProUGUI turretCostIndicator;


//    private CurrencyLogic currencyLogic;
//    private static CostLogic costLogic;
//    private CostLogic()
//    {
       
//    }
//    public static CostLogic getInstance()
//    {
//        if (!costLogic)
//        {
//            costLogic = new CostLogic();
//            return costLogic;
//        }
//        else
//        {
//            return costLogic;
//        }
//    }

//    private void Awake()
//    {
//        currencyLogic = CurrencyLogic.getinstance();
//    }

//    void Start () {

//        //currencyLogic = CurrencyLogic.currencyLogic;

//        wallCost = wallCostParamaeter;
//        turretCost = turretCostParamaeter;
//        wallCostIndicator.text = wallCost.ToString();
//        turretCostIndicator.text = turretCost.ToString();


//    }


//    public bool canBuyWall()
//    {
//        if (currencyLogic.GetCurrency() > wallCost)
//        {
//            return true;
//        }
//        return false;
//    }

//    public void BuyWall()
//    {
//        currencyLogic.DecreaseCurrencyByAmount(wallCost);
       
//    }

//    public bool canBuyTurret()
//    {


//        if (currencyLogic.GetCurrency() > turretCost)
//        {
//            return true;
//        }
//        return false;
//    }

//    public void BuyTurret()
//    {
//        currencyLogic.DecreaseCurrencyByAmount(turretCost);
//    }


//    void Update () {
		
//	}
//}
