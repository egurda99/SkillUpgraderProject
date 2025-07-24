using Atomic.Elements;
using BehaviorDesigner.Runtime;
using BehaviourTreePractice;

namespace _BehaviourTreePractice
{
    public sealed class TreeReservationPolicy
    {
        private readonly ReactiveVariable<string> _id;
        private readonly SharedBool _isBackpackFull;
        private readonly TreeSensorObserver _observer;

        public ReactiveVariable<string> ID => _id;


        public TreeReservationPolicy(ReactiveVariable<string> id, SharedBool isBackpackFull,
            TreeSensorObserver treeSensorObserver)
        {
            _id = id;
            _isBackpackFull = isBackpackFull;
            _observer = treeSensorObserver;
        }

        public bool TryReserve(Tree tree, Tree currentTree, out Tree reservedTree)
        {
            reservedTree = null;

            if (_isBackpackFull.Value)
            {
                _observer.ClearTree();
                return false;
            }

            if (tree.IsTreeOccupied && !tree.IsReservedBy(_id.Value))
                return false;

            if (currentTree == tree)
                return false;


            if (currentTree != null)
            {
                if (currentTree.IsReservedBy(_id.Value))
                    return false;

                if (currentTree == tree && currentTree.IsReservedBy(_id.Value))
                    return false;

                if (tree.TryReserve(_id.Value))
                {
                    reservedTree = tree;
                    return true;
                }

                return false;
            }

            if (currentTree == null)
            {
                if (tree.TryReserve(_id.Value))
                {
                    reservedTree = tree;
                    return true;
                }
            }

            return false;
        }
    }
}
