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
            SkyModelData selectedSky = skyDatas[Random.Range(0, skyDatas.Count)];
            GameObject backgroundSky = new GameObject();
            backgroundSky.transform.SetParent(transform);
            backgroundSky.name = "Sky";
            backgroundSky.AddComponent<SpriteRenderer>();
            sky = backgroundSky.GetComponent<SpriteRenderer>();
            sky.transform.localPosition = Vector3.zero;
            sky.sortingLayerName = "Background";
            sky.sprite = selectedSky.background;
            for (int i = 0; i < selectedSky.clouds.Length; i++)
            {
                GameObject parallaxParent = new GameObject();
                parallaxParent.transform.SetParent(transform);
                parallaxParent.name = "ParallaxLayer" + i;
                parallaxParent.transform.localPosition = Vector3.zero;
                parallaxLayers.Add(parallaxParent);
                for (int j = 0; j < 3; j++)
                {
                    GameObject parallaxImage = new GameObject();
                    parallaxImage.transform.parent = parallaxParent.transform;
                    parallaxImage.transform.localPosition = new Vector3(imageSize * j, 0, 0);
                    parallaxImage.name = "Parallax" + i + "_" + j;
                    parallaxImage.AddComponent<SpriteRenderer>();
                    SpriteRenderer spriteRenderer = parallaxImage.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = selectedSky.clouds[i];
                    spriteRenderer.sortingLayerName = "Background";
                    spriteRenderer.sortingOrder = i+1;
                }
            }
        }

        private void UpdateParallax()
        {
            SkyModelData selectedSky = skyDatas[Random.Range(0, skyDatas.Count)];
            sky.sprite = selectedSky.background;
            for (int i = 0; i < parallaxLayers.Count; i++)
            {
                Transform parallax = parallaxLayers[i].transform;
                for (int j = 0; j < parallax.transform.childCount; j++)
                {
                    Transform currentParallax = parallax.transform.GetChild(j);
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
                    if (currentParallax.localPosition.x <= -imageSize)
                    {
                        currentParallax.localPosition = new Vector3((imageSize * 2),0, 0);
                    }
                }

            }
        }

        private void MoveParallax()
        {
            for (int i = 0; i < parallaxLayers.Count; i++)
            {
                for (int j = 0; j < parallaxLayers[i].transform.childCount; j++)
                {
                    Transform currentParallax = parallaxLayers[i].transform.GetChild(j);
                    Vector3 newPosition = currentParallax.localPosition - new Vector3((parallaxSpeed * (i + 1) * ((i + 1) * distanceSlowMultiplier)) * Time.deltaTime, 0, 0);
                    currentParallax.localPosition = newPosition;
                }
            }
        }
    }
}
