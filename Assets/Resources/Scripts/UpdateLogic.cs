using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateLogic : MonoBehaviour
{

    public int FireRatePrice;
    public int RangePrice;
    public int PowerPrice;

    public static GameObject targetObject;
    private Turret turret;

    public TextMeshProUGUI FireRatePriceGO;
    public TextMeshProUGUI FireRateValueGO;

    public TextMeshProUGUI RangePriceGO;
    public TextMeshProUGUI RangeValueGO;

    public TextMeshProUGUI PowerPriceGO;
    public TextMeshProUGUI PowerValueGO;

    private CurrencyLogic currencyLogic;

    void Awake()
    {
        FireRatePriceGO.text = FireRatePrice.ToString();
        RangePriceGO.text = RangePrice.ToString();
        PowerPriceGO.text = PowerPrice.ToString();

        FireRateValueGO.text = "1/5";
        RangeValueGO.text = "1/5";
        PowerValueGO.text = "1/5";
    }

    private void Start()
    {
        currencyLogic = CurrencyLogic.getinstance();
    }

    private void Update()
    {
        if (targetObject != null)
        {
            turret = targetObject.GetComponent<Turret>();
            FireRateValueGO.text = turret.getFireRateLevel().ToString() + "/5";
            RangeValueGO.text = turret.getRangeLevel().ToString() + "/5";
            PowerValueGO.text = turret.getPowerLevel().ToString() + "/5";
        }
    }


    public void UpgradeFireRate()
    {
        var currentCurrency = currencyLogic.GetCurrency();
        if (FireRatePrice < currentCurrency)
        {
            turret.levelUpFireRate();
            currencyLogic.DecreaseCurrencyByAmount(FireRatePrice);

        }
    }

    public void UpgradeRange()
    {
        var currentCurrency = currencyLogic.GetCurrency();
        if (RangePrice < currentCurrency)
        {
            turret.levelUpRange();
            currencyLogic.DecreaseCurrencyByAmount(RangePrice);

        }
    }

    public void UpgradePower()
    {
        var currentCurrency = currencyLogic.GetCurrency();
        if (PowerPrice < currentCurrency)
        {
            turret.levelUpPower();
            currencyLogic.DecreaseCurrencyByAmount(PowerPrice);
            
        }

    }




}
