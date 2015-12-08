using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    private RectTransform _healthBar;
    private Text _playerHealth;
    private Text _playerStamina;
    private Text _playerMana;


	// Use this for initialization
	void Start ()
	{
	    _playerHealth = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<Text>();
        _playerMana = GameObject.FindGameObjectWithTag("PlayerMana").GetComponent<Text>();
        _playerStamina = GameObject.FindGameObjectWithTag("PlayerStamina").GetComponent<Text>();
	}
	
    public void UpdateEnemyHealth(Enemy enemy, float health)
    {
        _healthBar = enemy.GetComponentInChildren<Canvas>().GetComponent<RectTransform>();

        float healthReduction = health / 100;
        _healthBar.sizeDelta = new Vector2(healthReduction, _healthBar.sizeDelta.y);
    }

    public void UpdatePlayerHealth(int health)
    {
        _playerHealth.text = "Health: " + health;
    }

    public void UpdatePlayerMana(int mana)
    {
        _playerMana.text = "Mana: " + mana;
    }

    public void UpdatePlayerStamina(int stamina)
    {
        _playerStamina.text = "Stamina: " + stamina;
    }
}
