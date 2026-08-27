using System;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX;
    private float cameraHalfWidth;
    
    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
         mainCamera = Camera.main;
         cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
         CalculateImageLength();
    }

    private void FixedUpdate()
    {
        float currentCameraPositionX = mainCamera.transform.position.x;
        float distanceToMove = currentCameraPositionX - lastCameraPositionX;
        lastCameraPositionX = currentCameraPositionX;
        
        float cameraLeftEdge = currentCameraPositionX - cameraHalfWidth;
        float cameraRightEdge = currentCameraPositionX + cameraHalfWidth;

        foreach (ParallaxLayer parallaxLayer in backgroundLayers)
        {
            parallaxLayer.Move(distanceToMove);
            parallaxLayer.LoopBackground(cameraLeftEdge, cameraRightEdge); 
        }
    }
    
    private void CalculateImageLength()
    {
        foreach (ParallaxLayer parallaxLayer in backgroundLayers)
        {
            parallaxLayer.CalculateImageWidth();
        }
    }
}
