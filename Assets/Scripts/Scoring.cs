using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{

    public static Scoring _instance;

    public static MenuScript menuScriptRef;

    private GameObject controllerObj;
    public static PersonController controllerRef;

    //set up UI for lives
    public Text LivesText;
    public int LivesCount;

    //set up UI for health
    public int HealthCount = 3;
    public RawImage Health1;
    public RawImage Health2;
    public RawImage Health3;

    //set up UI for Fuel gauge
    public int FuelCount;
    public GameObject FuelGaugeFull;
    public GameObject FuelGaugeNearFull;
    public GameObject FuelGaugeMid1;
    public GameObject FuelGaugeMid2;
    public GameObject FuelGaugeLow;
    public GameObject FuelGaugeNearEmpty;


    //set up UI for Collectibles
    public Text SapphirecountText;
    public int Sapphirecount;

    public Text RubycountText;
    public int Rubycount;

    public Text EmeraldcountText;
    public int Emeraldcount;

    public Text AmeythystcountText;
    public int Ameythystcount;

    public Text DiamondcountText;
    public int Diamondcount;

    public Text GoldcountText;
    public int Goldcount;

    public Text QuartzcountText;
    public int Quartzcount;

    //create win text
    public Text winText;

    //create objects for respawing
    private GameObject Player;
    public GameObject Respawn;

    string _type;

    void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnLevelWasLoaded(int level)
    {
        if (menuScriptRef.inGame)
        {
            Player = GameObject.Find("Player");
            Respawn = GameObject.FindGameObjectWithTag("Respawn");
            controllerRef = Player.GetComponent<PersonController>();
        }
    }

    //set all values to default
    void Start()
    {        
        menuScriptRef = GetComponent<MenuScript>();

        countReset();

    }

    public void countReset()
    {
        Sapphirecount = 0;
        Rubycount = 0;
        Emeraldcount = 0;
        Ameythystcount = 0;
        Diamondcount = 0;
        Goldcount = 0;
        Quartzcount = 0;
        LivesCount = 3;
        FuelCount = 6;
        SetCountText();
    }

    //checking to make sure health/lives stays within intended bounds
    private void Update()
    {
        if(HealthCount > 3)
        {
            HealthCount = 3;
        }

        if(HealthCount < 0)
        {
            HealthCount = 0;
        }

        if(LivesCount > 7)
        {
            LivesCount = 7;
            SetCountText();
        }

        if (LivesCount < 0)
        {
            LivesCount = 0;
            SetCountText();
        }

        if (FuelCount > 6)
        {
            FuelCount = 6;
        }

        if (FuelCount < 0)
        {
            FuelCount = 0;
            controllerRef.canDoubleJump = false;
        }
    }

    public void SetCountText()
    {
        //updates the UI count for collectibles, lives, and health
        SapphirecountText.text = Sapphirecount.ToString();
        RubycountText.text = Rubycount.ToString();
        EmeraldcountText.text = Emeraldcount.ToString();
        AmeythystcountText.text = Ameythystcount.ToString();
        DiamondcountText.text = Diamondcount.ToString();
        GoldcountText.text = Goldcount.ToString();
        QuartzcountText.text = Quartzcount.ToString();
        LivesText.text = LivesCount.ToString();

        //if player has gotten all gems, display win text
        if((Sapphirecount == 25) &&(Rubycount == 25) && (Emeraldcount == 25) && (Ameythystcount == 25) && (Diamondcount == 25) && (Goldcount == 25) &&( Quartzcount == 25))
        {
            winText.text = "You Win!";
        }
    }

    public void ResetHealth()
    {
        //remove a life
        LivesCount--;
        //set health count back to 3 
        HealthCount = 3;
        //reset fuel
        FuelCount = 6;
        //respawn fuel cans

        //re-enable all health
        Health1.enabled = true;
        Health2.enabled = true;
        Health3.enabled = true;
        SetCountText();
        FuelUpdate();
    }

    public void KillPlayer()
    {
        if ((LivesCount == 0) && (HealthCount == 0))
        {
            controllerRef.dead = true;
        }
    }

    public void playerTakeDamage()
    {
       
        HealthCount--;
        //check to see which health display needs to be disabled
        if (Health3.enabled)
        {
            Health3.enabled = false;
        }
        else
        if (Health2.enabled)
        {
            Health2.enabled = false;
        }
        else
        if (Health1.enabled)
        {
            Health1.enabled = false;
        }
    }

    //all conditions of the fuel gauge
    public void FuelUpdate()
    {
        if(FuelCount >= 6)
        {
            FuelGaugeFull.SetActive(true);
            FuelGaugeNearFull.SetActive(true);
            FuelGaugeMid1.SetActive(true);
            FuelGaugeMid2.SetActive(true);
            FuelGaugeLow.SetActive(true);
            FuelGaugeNearEmpty.SetActive(true);
        }

        if (FuelCount == 5)
        {
            FuelGaugeFull.SetActive(false);
            FuelGaugeNearFull.SetActive(true);
            FuelGaugeMid1.SetActive(true);
            FuelGaugeMid2.SetActive(true);
            FuelGaugeLow.SetActive(true);
            FuelGaugeNearEmpty.SetActive(true);
        }

        if (FuelCount == 4)
        {
            FuelGaugeNearFull.SetActive(false);
            FuelGaugeMid1.SetActive(true);
            FuelGaugeMid2.SetActive(true);
            FuelGaugeLow.SetActive(true);
            FuelGaugeNearEmpty.SetActive(true);
        }

        if (FuelCount == 3)
        {
            FuelGaugeMid1.SetActive(false);
            FuelGaugeMid2.SetActive(true);
            FuelGaugeLow.SetActive(true);
            FuelGaugeNearEmpty.SetActive(true);
        }

        if (FuelCount == 2)
        {
            FuelGaugeMid2.SetActive(false);
            FuelGaugeLow.SetActive(true);
            FuelGaugeNearEmpty.SetActive(true);
        }
        if (FuelCount == 1)
        {
            FuelGaugeLow.SetActive(false);
            FuelGaugeNearEmpty.SetActive(true);
        }

        if (FuelCount == 0)
        {
            FuelGaugeFull.SetActive(false);
            FuelGaugeNearFull.SetActive(false);
            FuelGaugeMid1.SetActive(false);
            FuelGaugeMid2.SetActive(false);
            FuelGaugeLow.SetActive(false);
            FuelGaugeNearEmpty.SetActive(false);
        }
    }


}
