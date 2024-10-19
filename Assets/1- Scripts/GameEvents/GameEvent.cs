using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameEvent", menuName = "Events/GameEvent")]
public class GameEvent : ScriptableObject
{
    public string eventName;
    public string description;
    public float effectDuration;

    public virtual void TriggerEvent()
    {
        // Eventin etkisini burada tanýmlarsýnýz
    }
}
