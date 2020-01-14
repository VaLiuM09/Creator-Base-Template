﻿using Innoactive.Hub.SDK.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Innoactive.Hub.PlayerSetup
{
    /// <summary>
    /// Does the complete setup of the player. This includes disabling the headset player if
    /// chosen in the config, spawning the spectator cam if activated in config and
    /// configuring the cameras to the scene specific needs.
    /// </summary>
    public class SpectatorCameraSetup : MonoBehaviour
    {
        private static readonly Common.Logging.ILog logger = Logging.LogManager.GetLogger<SpectatorCameraSetup>();

        [Tooltip("This prefab will be used if set. Otherwise, the default Spectator Cam is used.")]
        [SerializeField]
        protected GameObject spectatorCamPrefabOverload;

        [Tooltip("The font used in the spectator view.")]
        [SerializeField]
        protected Font font;
        
        private GameObject currentSpectatorInstance = null;

        protected virtual void Start()
        {
            InstantiateSpectator();
        }
        
        private void InstantiateSpectator()
        {
            if (currentSpectatorInstance != null)
            {
                Destroy(currentSpectatorInstance);
            }
            GameObject spectatorPrefab = spectatorCamPrefabOverload == null ? Resources.Load<GameObject>("Spectator Camera") : spectatorCamPrefabOverload;
            currentSpectatorInstance = Instantiate(spectatorPrefab, transform.position, transform.rotation);
            Camera spectatorCamera = currentSpectatorInstance.GetComponentInChildren<Camera>();

            SetFont();
        }

        private void SetFont()
        {
            foreach (Text text in currentSpectatorInstance.GetComponentsInChildren<Text>(true))
            {
                text.font = font;
            }
        }
    }
}
