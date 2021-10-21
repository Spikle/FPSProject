using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameMenu : UIMenu
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
