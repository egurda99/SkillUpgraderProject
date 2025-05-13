using System;

namespace Lessons.Architecture.PM
{
    public sealed class PopupViewModelFactory : IPopupViewModelFactory
    {
        private readonly PlayerPopupSectionsFactory _playerPopupSectionsFactory;

        public PopupViewModelFactory(PlayerPopupSectionsFactory playerPopupSectionsFactory)
        {
            _playerPopupSectionsFactory = playerPopupSectionsFactory;
        }

        public IPopupViewModel Create(PopupName name)
        {
            return name switch
            {
                PopupName.PLAYER_STATS => new PlayerPopupViewModel(_playerPopupSectionsFactory),
                _ => throw new ArgumentOutOfRangeException(nameof(name), $"ViewModel для {name} не найдена")
            };
        }
    }
}