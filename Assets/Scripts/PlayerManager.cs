using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerState playerState;
    public Material playerColor;
    public List<GameObject> collidedList;
    public Transform collectedPool;
    [SerializeField] public LevelState levelState;
    
    
    public enum PlayerState
    {
        Stop,
        Move
    }

    public enum LevelState
    {
        NotFinished,

        Finshished,
    }
}