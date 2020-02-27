using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAniControl : MonoBehaviour
{
    MenuScript menuScript;
    public AniSwitch aniSwitchRef;
    Scoring scoring;

    bool findplayer;

    int Sapphirecount;
    int Rubycount;
    int Emeraldcount;
    int Ameythystcount;
    int Diamondcount;

    public enum UIState
    {
        OnScreen,
        OffScreen,
        SlideOff,
        SlideOn
       
    }

    public UIState _state;

    public Animator Collectables;
    public float timeOnScreen;

    float onScreenTimer;
    

    void Start()
    {
        _state = UIState.OnScreen;
        onScreenTimer = timeOnScreen;
        menuScript = MenuScript._instance;
        scoring = GetComponent<Scoring>();
        findplayer = true;
    }

    
    void Update()
    {
        if (menuScript.inGame)
        {
            switch (_state)
            {
                case UIState.OnScreen:
                    Collectables.SetBool("OnScreen", true);
                    Collectables.SetBool("OffScreen", false);
                    Collectables.SetBool("SlideOff", false);
                    Collectables.SetBool("SlideOn", false);

                    if (!menuScript.paused)
                    {
                        onScreenTimer -= Time.deltaTime;
                        if(onScreenTimer <= 0)
                        {                    
                            _state = UIState.SlideOff;
                        }
                    }
                    else
                    {
                        onScreenTimer = timeOnScreen;
                    }
                    break;

                case UIState.OffScreen:
                    Collectables.SetBool("OnScreen", false);
                    Collectables.SetBool("OffScreen", true);
                    Collectables.SetBool("SlideOff", false);
                    Collectables.SetBool("SlideOn", false);

                    aniSwitchRef.offScreen = false;

                    if (scoring.Sapphirecount > Sapphirecount + 3 || scoring.Rubycount > Rubycount + 3 ||
                        scoring.Emeraldcount > Emeraldcount + 3 || scoring.Ameythystcount > Ameythystcount + 3 ||
                        scoring.Diamondcount > Diamondcount + 3)
                    {
                        _state = UIState.SlideOn;
                    }

                    if (menuScript.paused)
                    {
                        _state = UIState.OnScreen;
                    }

                    break;

                case UIState.SlideOn:
                    Collectables.SetBool("OnScreen", false);
                    Collectables.SetBool("OffScreen", false);
                    Collectables.SetBool("SlideOff", false);
                    Collectables.SetBool("SlideOn", true);

                    if (aniSwitchRef.onScreen)
                    {
                        _state = UIState.OnScreen;
                    }

                    break;

                case UIState.SlideOff:
                    Collectables.SetBool("OnScreen", false);
                    Collectables.SetBool("OffScreen", false);
                    Collectables.SetBool("SlideOff", true);
                    Collectables.SetBool("SlideOn", false);

                    onScreenTimerReset();

                    if (aniSwitchRef.offScreen)
                    {
                        _state = UIState.OffScreen;
                    }

                    updateCount();

                    if (menuScript.paused)
                    {
                        _state = UIState.OnScreen;
                    }

                    break;

            }
        }

    }

    void updateCount()
    {
        Sapphirecount = scoring.Sapphirecount;
        Rubycount = scoring.Rubycount;
        Emeraldcount = scoring.Emeraldcount;
        Ameythystcount = scoring.Ameythystcount;
        Diamondcount = scoring.Diamondcount;
    }

    void onScreenTimerReset()
    {
        onScreenTimer = timeOnScreen;
    }

}
