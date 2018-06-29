using TMPro;
using UnityEngine;

public  class KilledAliveLogic : MonoBehaviour {

    public  TextMeshProUGUI KilledIndicator;
    public  TextMeshProUGUI AliveIndicator;
    public  TextMeshProUGUI LifesIndicator;

    public static int Killed;
  
    public int GetKilled()
    {
        return Killed;
    }

    private void SetKilled(int value)
    {
        Killed = value;
    }

    public static int Alive;

    public int GetAlives()
    {
        return Alive;
    }

    private void SetAlive(int value)
    {
        Alive = value;
    }

    public static int Lifes;

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
        Lifes--;
    }

    public void UpdateDisplay()
    {
        
        //KilledIndicator.text = Killed.ToString();
        //AliveIndicator.text = Alive.ToString();
        //LifesIndicator.text = Lifes.ToString();
    }

    private void Start () {

        Killed = 0;
        Alive = 0;
        Lifes = 10;

      

    }

  

    
    

}
