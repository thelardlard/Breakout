using System;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType
    {
        IncreasePaddle,
        Multiball,
        Guns
    }
    public PowerupType currentPowerupType;
    private MeshRenderer objectRenderer;

    private void Awake()
    {
        currentPowerupType = (PowerupType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(PowerupType)).Length);
        
    }

    private void Start()
    {
        objectRenderer = GetComponent<MeshRenderer>();
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (objectRenderer == null) return;

        switch (currentPowerupType)
        {
            case PowerupType.IncreasePaddle:
                
                objectRenderer.material.color = Color.green; // Green for Increase Paddle
                break;

            case PowerupType.Multiball:
                
                objectRenderer.material.color = Color.blue; // Blue for Multiball
                break;

            case PowerupType.Guns:
                
                objectRenderer.material.color = Color.red; // Red for Guns
                break;
            
            default:
                
                break;
        }
    }
    
}
