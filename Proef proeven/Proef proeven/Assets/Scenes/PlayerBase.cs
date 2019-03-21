using Controllers;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private int _baseHealth = 3;
    private int _currentID;
    public Player correspondingPlayerPrefeb;
    private Player _correspondingPlayer;
    private int _defendingCard;

    private void Awake()
    {
        SpawnCorrespondingPlayer();
    }

    private void LateUpdate()
    {
        SetPlayerInfo();
    }

    private void SpawnCorrespondingPlayer()
    {
        GameObject spawnedPlayer = Instantiate(correspondingPlayerPrefeb.gameObject, transform.position,
        correspondingPlayerPrefeb.gameObject.transform.rotation);
        _correspondingPlayer = spawnedPlayer.GetComponent<Player>();
        _currentID = spawnedPlayer.GetComponent<Player>().GetPlayerID();
        Debug.Log(_currentID);
    }

    public void SetDefendingCard(int newDefendingCard)
    {
        _defendingCard = newDefendingCard;
    }

    public int GetDefendingCard()
    {
        return _defendingCard;
    }

    public int ID
    {
        get { return _currentID; }
        set { _currentID = value; }
    }

    public void DamageBase()
    {
        _baseHealth--;
        if (BaseHealth == 0)
        {
            Debug.Log("Player " + ID + "died");
            PlayerManager.instance.DestroyPlayerByIndex(ID);
        }

        SetPlayerInfo();
    }

    public void SetPlayerInfo()
    {
        //Debug.Log(_correspondingPlayerPrefeb.GetInventory()._currentAmountOfCardsinInventory + " " +  _correspondingPlayerPrefeb + " " + _correspondingPlayerPrefeb.GetInventory());
        //
        UIController.Instance.SetPlayerInfo(
        PlayerManager.instance.GetCurrentPlayer().GetInventory()._currentAmountOfCardsinInventory, _baseHealth,
        _currentID);
    }

    public int BaseHealth
    {
        get { return _baseHealth; }
    }
}