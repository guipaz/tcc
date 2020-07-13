using System;
using UnityEngine;

namespace Assets.Source.Game
{
    public class Transition
    {
        public float duration;
        public Action OnStart;
        public Action<float> OnUpdate;
        public Action OnFinish;

        float timeLeft;

        public void Play()
        {
            timeLeft = duration;
            OnStart?.Invoke();
        }

        public bool Update()
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;

                if (timeLeft <= 0)
                {
                    OnFinish?.Invoke();
                    timeLeft = 0;
                }
                else
                {
                    OnUpdate?.Invoke(timeLeft);
                    
                }

                return true;
            }

            return false;
        }
    }
}
