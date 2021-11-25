using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Text lifeText;
    public Text enemyText;

    private int _lifes = 3;
    private int _enemies = 5;
    // Start is called before the first frame update
    void Start()
    {
        lifeText.text = "Vidas: "+ _lifes;
        enemyText.text = "Enemigos: "+ _enemies;
    }

    public int GetLifes()
    {
        return this._lifes;
    }

    public int GetEnemies()
    {
        return this._enemies;
    }

    public void MinusLifes(int lifes)
    {
        this._lifes -= lifes;
        lifeText.text = "Vidas: "+ _lifes;
    }

    public void MinusEnemies(int enemies)
    {
        this._enemies -= enemies;
        enemyText.text = "Enemigos: "+ _enemies;
    }

}
