using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GameObject playerPrefab;

        private GameObject player;

        public static GameObject Player => Instance.player;

        protected override void Awake()
        {
            base.Awake();
            SceneManager.LoadSceneAsync("SceneUI", LoadSceneMode.Additive);
        }

        private void Start()
        {
            PlayerSpawn();
        }

        private void PlayerSpawn()
        {
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            EventManager.OnSetHealthPlayer?.Invoke(player.GetComponent<HealthController>().Health);
        }
    }
}
