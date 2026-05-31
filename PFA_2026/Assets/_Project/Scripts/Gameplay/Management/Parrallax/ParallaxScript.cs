using System;
using System.Collections.Generic;
using Helteix.Tools.Phases.Listeners;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Naussilus.Gameplay.Parrallax
{
    public class ParallaxScript : MonoPhaseListener<ManagementPhase>
    {
        [SerializeField] private List<SkyModelData> skyDatas;
        [SerializeField] private float parallaxSpeed;
        [SerializeField] private float distanceSlowMultiplier;
        private List<GameObject> parallaxLayers;
        [SerializeField] float imageSize;
        private bool backgroundMove = false;
        private SpriteRenderer sky;

        private void Awake()
        {
            parallaxLayers = new List<GameObject>();
            CreateParallax();
        }

        protected override void OnPhaseBegin(ManagementPhase phase)
        {
            base.OnPhaseBegin(phase);
            UpdateParallax();
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
            SkyModelData selectedSky = skyDatas[Random.Range(0, skyDatas.Count-1)];
            GameObject backgroundSky = new GameObject();
            backgroundSky.transform.SetParent(transform);
            backgroundSky.name = "Sky";
            backgroundSky.AddComponent<SpriteRenderer>();
            sky = backgroundSky.GetComponent<SpriteRenderer>();
            sky.sortingLayerName = "Background";
            sky.sprite = selectedSky.background;
            for (int i = 0; i < selectedSky.clouds.Length; i++)
            {
                GameObject parallaxParent = new GameObject();
                parallaxParent.transform.SetParent(transform);
                parallaxParent.name = "ParallaxLayer" + i;
                parallaxLayers.Add(parallaxParent);
                for (int j = 0; j < 3; j++)
                {
                    GameObject parallaxImage = new GameObject();
                    parallaxImage.transform.parent = parallaxParent.transform;
                    parallaxImage.transform.localPosition = new Vector3(imageSize * j, 0f, 0f);
                    parallaxImage.name = "Parallax" + i + "_" + j;
                    parallaxImage.AddComponent<SpriteRenderer>();
                    SpriteRenderer spriteRenderer = parallaxImage.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = selectedSky.clouds[j];
                    spriteRenderer.sortingLayerName = "Background";
                    spriteRenderer.sortingOrder = i;
                }
            }
        }

        private void UpdateParallax()
        {
            SkyModelData selectedSky = skyDatas[Random.Range(0, skyDatas.Count-1)];
            sky.sprite = selectedSky.background;
            foreach (GameObject parallax in parallaxLayers)
            {
                for (int i = 0; i < parallax.transform.childCount; i++)
                {
                    Transform currentParallax = parallax.transform.GetChild(i);
                    currentParallax.gameObject.GetComponent<SpriteRenderer>().sprite = selectedSky.clouds[i];
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
                        currentParallax.position = new Vector3(imageSize * 2, 0, 0);
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
                    Vector3 newPosition = currentParallax.position -
                                          new Vector3(
                                              (parallaxSpeed * (i + 1) * ((i + 1) * distanceSlowMultiplier)) *
                                              Time.deltaTime, 0, 0);
                    currentParallax.position = newPosition;
                }
            }
        }

        private void SetColor()
        {
            foreach (GameObject parallax in parallaxLayers)
            {
                for (int i = 0; i < parallax.transform.childCount; i++)
                {
                    SpriteRenderer currentParallax = parallax.transform.GetChild(i).GetComponent<SpriteRenderer>();
                }
            }
        }
    }
}
