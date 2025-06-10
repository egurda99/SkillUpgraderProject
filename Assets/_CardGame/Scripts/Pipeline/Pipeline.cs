using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _CardGame.EventTasks;
using UnityEngine;

namespace _CardGame.Pipeline
{
    public class Pipeline
    {
        private readonly List<BaseTask> _tasks = new();
        public event Action Completed;

        public void AddTask(BaseTask task)
        {
            _tasks.Add(task);
        }

        public virtual async Task Run()
        {
            OnStarted();
            foreach (var task in _tasks)
            {
                await task.Run();
            }

            OnCompleted();
        }

        public void Clear()
        {
            _tasks.Clear();
        }

        protected virtual void OnStarted()
        {
            Debug.Log($"Pipeline {GetType().Name} started");
        }

        protected virtual void OnCompleted()
        {
            Debug.Log($"Pipeline {GetType().Name} completed");
            Completed?.Invoke();
        }
    }
}
