using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(ShootComponent))]
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(TeamComponent))]
    public class Player : MonoBehaviour
    {
    }
}
