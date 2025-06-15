using System;
using System.Collections.Generic;
using System.Linq;
using Atomic.Elements;
using Atomic.Entities;
using Zenject;

public sealed class ActiveZombiesProvider : IInitializable, IDisposable
{
    private readonly List<ActiveZombie> _activeZombies = new();
    public IReadOnlyList<SceneEntity> ActiveZombies => _activeZombies.Select(z => z.Entity).ToList();

    private readonly ZombieSpawner _zombieSpawner;
    public event Action OnZombieRemoved;

    public ActiveZombiesProvider(ZombieSpawner zombieSpawner)
    {
        _zombieSpawner = zombieSpawner;
    }

    public void Initialize()
    {
        _zombieSpawner.OnZombieSpawned += OnZombieSpawned;
    }

    private void OnZombieSpawned(SceneEntity zombieEntity)
    {
        var isDead = zombieEntity.GetIsDead();
        void OnDeathChanged(bool dead) => OnZombieDead(zombieEntity, dead);

        isDead.Subscribe(OnDeathChanged);
        _activeZombies.Add(new ActiveZombie(zombieEntity, isDead, OnDeathChanged));
    }

    private void OnZombieDead(SceneEntity zombieEntity, bool isDead)
    {
        if (!isDead)
            return;

        var index = _activeZombies.FindIndex(z => z.Entity == zombieEntity);
        if (index == -1)
            return;

        var zombie = _activeZombies[index];

        zombie.IsDead.Unsubscribe(zombie.DeathCallback);
        _activeZombies.RemoveAt(index);


        OnZombieRemoved?.Invoke();
    }

    public void Dispose()
    {
        _zombieSpawner.OnZombieSpawned -= OnZombieSpawned;

        foreach (var zombie in _activeZombies)
        {
            zombie.IsDead.Unsubscribe(zombie.DeathCallback);
        }

        _activeZombies.Clear();
    }

    private readonly struct ActiveZombie
    {
        public SceneEntity Entity { get; }
        public ReactiveVariable<bool> IsDead { get; }
        public Action<bool> DeathCallback { get; }

        public ActiveZombie(SceneEntity entity, ReactiveVariable<bool> isDead, Action<bool> deathCallback)
        {
            Entity = entity;
            IsDead = isDead;
            DeathCallback = deathCallback;
        }
    }
}
