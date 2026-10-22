using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleRefactor
{
    public class InputManager : MonoBehaviour
    {
        public Snake snake;
        public Dictionary<KeyCode, Action> keyDownActions = new Dictionary<KeyCode, Action>();
        public Dictionary<KeyCode, Action> keyUpActions = new Dictionary<KeyCode, Action>();

        private void Start()
        {
            keyDownActions = new Dictionary<KeyCode, Action>()
            {
                { KeyCode.W, () => snake.SetDirection(Direction.Up) },
                { KeyCode.S, () => snake.SetDirection(Direction.Down) },
                { KeyCode.A, () => snake.SetDirection(Direction.Left) },
                { KeyCode.D, () => snake.SetDirection(Direction.Right) },
                { KeyCode.LeftShift, () => snake.ActivateSpeedup() },
            };

            keyUpActions = new Dictionary<KeyCode, Action>()
            {
                { KeyCode.LeftShift, () => snake.DeactivateSpeedup() },
            };
        }

        private void Update()
        {
            PollInputsAndUpdateDirectionAndSpeed();
        }

        public void PollInputsAndUpdateDirectionAndSpeed()
        {
            foreach (var keyAndAction in keyDownActions)
            {
                KeyCode key = keyAndAction.Key;
                Action action = keyAndAction.Value;

                if (Input.GetKeyDown(key))
                {
                    action.Invoke();
                }
            }

            foreach (var keyAndAction in keyUpActions)
            {
                KeyCode key = keyAndAction.Key;
                Action action = keyAndAction.Value;

                if (Input.GetKeyUp(key))
                {
                    action.Invoke();
                }
            }
        }
    }
}