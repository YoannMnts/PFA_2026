using System;
using System.Collections.Generic;
using Helteix.Tools.Phases.Listeners;
using UnityEngine;

namespace Naussilus.Gameplay.Parrallax
{
    public class ParallaxScript : MonoPhaseListener<ManagementPhase>
    {
        [SerializeField] private List<Sprite> parallaxSprites;
        [SerializeField] private float parallaxSpeed;
        [SerializeField] private float distanceSlowMultiplier;
        private List<GameObject> parallaxLayers;
        [SerializeField] float imageSize;
        private bool backgroundMove = false;
        private void Awake()
        {
            parallaxLayers = new List<GameObject>();
            CreateParallax();
        }

        protected override void OnPhaseBegin(ManagementPhase phase)
        {
            base.OnPhaseBegin(phase);
            backgroundMove = true;
        }

        protected override void OnPhaseEnd(ManagementPhase phase)
        {
            base.OnPhaseEnd(phase);
            backgroundMove = false;
        }

        private void Update()
        {
            if (backgroundMove)
            {
                CheckLoop();
                MoveParallax();
            }
        }

        private void CreateParallax()
        {
            for (int i = 0; i < parallaxSprites.Count; i++)
            {
                GameObject parallaxParent = new GameObject();
                parallaxParent.transform.SetParent(transform);
                parallaxParent.name= "ParallaxLayer" + i;
                parallaxLayers.Add(parallaxParent);
                for (int j = 0; j < 3; j++)
                {
                    GameObject parallaxImage = new GameObject();
                    parallaxImage.transform.parent = parallaxParent.transform;
                    parallaxImage.transform.localPosition = new Vector3(imageSize*j, 0f, 0f);
                    parallaxImage.name= "Parallax" + i + "_" + j;
                    parallaxImage.AddComponent<SpriteRenderer>();
                    SpriteRenderer spriteRenderer = parallaxImage.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = parallaxSprites[i];
                    spriteRenderer.sortingLayerName = "Background";
                    spriteRenderer.sortingOrder = i;
                }
            }
        }

        private void CheckLoop()
        {
            foreach (GameObject parallax in parallaxLayers)
            {
                for (int i = 0; i < parallax.transform.childCount; i++)
                {
                    Transform currentParallax = parallax.transform.GetChild(i);
                    if (currentParallax.position.x <= -imageSize)
                    {
                        currentParallax.position = new Vector3(imageSize*2, 0, 0);
                    }
                }
                
            }
        }

        private void MoveParallax()
        {
            Debug.Log(parallaxLayers.Count);
            for (int i = 0; i < parallaxLayers.Count; i++)
            {
                Transform currentLayer = parallaxLayers[i].transform;
                for (int j = 0; j < currentLayer.childCount; j++)
                {
                    Transform currentParallax = currentLayer.GetChild(j);
                    Debug.Log(currentParallax);
                    Vector3 newPosition = currentParallax.position - new Vector3((parallaxSpeed*(i+1)*((i+1)*distanceSlowMultiplier)) * Time.deltaTime, 0, 0);
                    currentParallax.position = newPosition;
                }
            }
        }
    }
}
