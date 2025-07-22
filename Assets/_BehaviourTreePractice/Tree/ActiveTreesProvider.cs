using System;
using System.Collections.Generic;

namespace BehaviourTreePractice
{
    public sealed class ActiveTreesProvider
    {
        private readonly List<Tree> _trees = new();

        public List<Tree> Trees => _trees;

        public event Action<Tree> OnTreeAdded;
        public event Action<Tree> OnTreeRemoved;


        public void OnTreeSpawned(Tree tree)
        {
            _trees.Add(tree);
            tree.OnTreeDespawned += OnTreeDespawned;
            OnTreeAdded?.Invoke(tree);
        }

        public void OnTreeDespawned(Tree tree)
        {
            _trees.Remove(tree);
            tree.OnTreeDespawned -= OnTreeDespawned;
            OnTreeRemoved?.Invoke(tree);
        }
    }
}
