using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableManager : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private ScoreManager _scoreManager;

    private List<Pickable> _pickableList = new List<Pickable>();
    // Start is called before the first frame update
    void Start()
    {
        InitPickableList();
    }
    private void InitPickableList()
    {
        Pickable[] pickableObjects = GameObject.FindObjectsOfType<Pickable>();
            for (int i = 0; i < pickableObjects.Length; i++)
            {
                _pickableList.Add(pickableObjects[i]);
                pickableObjects[i].OnPicked += OnPickablePicked;
            }
        _scoreManager.SetMaxScore(_pickableList.Count);
    }
    private void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        if(_scoreManager != null)
        {
            _scoreManager.AddScore(1);
        }
        if(pickable.pickableType == PickableType.PowerUp)
        {
            _player?.PickPowerUp();
        }
        //Debug.Log("Pickable List: "+_pickableList.Count);
        if(_pickableList.Count <=0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
