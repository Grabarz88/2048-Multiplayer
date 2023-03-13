using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptToRememberColors : MonoBehaviour
{

    public int Player1ColorID = 0;
    public int Player2ColorID = 0;

    public void Player1ColorSetter(int colorID)
    {
        Player1ColorID = colorID;
    }

    public void Player2ColorSetter(int colorID)
    {
        Player2ColorID = colorID;
    }

    public int Player1ColorGetter()
    {
        return Player1ColorID; 
    }

    public int Player2ColorGetter()
    {
        return Player2ColorID; 
    }

}
