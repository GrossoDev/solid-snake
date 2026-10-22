using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimpleRefactor
{
    public class GameManager : MonoBehaviour
    {
        public bool isGameOver;
        public GameObject gameOverUIPanel;
        public Snake player;
        public Arena arena;

        private float lastTickTime;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            float currentTime = Time.time;
            float nextTickTime = lastTickTime + player.GetSpeed();

            if (currentTime >= nextTickTime)
            {
                RunGameTick();
                lastTickTime = currentTime;
            }
        }

        public void Initialize()
        {
            isGameOver = false;
            gameOverUIPanel.SetActive(false);

            arena.Initialize();
            
            Cell startingCell = arena.GetRandomFreeCell();
            player.Initialize(startingCell);

            lastTickTime = 0;
        }

        public void RunGameTick()
        {
            if (!isGameOver)
            {
                player.TryToAdvance();
            }
        }

        public void SnakeDied(Snake snake)
        {
            isGameOver = true;
            gameOverUIPanel.SetActive(true);
        }

        public void Reset()
        {
            arena.Dispose();
            Initialize();
        }
    }
}