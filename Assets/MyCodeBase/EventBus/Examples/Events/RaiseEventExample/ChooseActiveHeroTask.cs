// using System.Collections.Generic;
// using System.Threading.Tasks;
// using _CardGame.Events;
// using _CardGame.Services;
// using _CardGame.Systems;
// using Cysharp.Threading.Tasks;
// using UI;
//
// namespace _CardGame.EventTasks
// {
//     public sealed class ChooseActiveHeroTask : BaseTask
//     {
//         private TaskCompletionSource<bool> _taskCompletionSource;
//         private readonly IEventBus _eventBus;
//         private readonly HeroListView _blueHeroList;
//
//         private int _previousRedIndex = -1;
//         private int _previousBlueIndex = -1;
//
//         private IReadOnlyList<HeroView> _views;
//         private readonly HeroListView _redHeroList;
//         private readonly ActiveTeamService _activeTeamService;
//         private readonly ActiveCardService _activeCardService;
//         private readonly FrozenHeroesService _frozenHeroesService;
//
//         public ChooseActiveHeroTask(IEventBus eventBus, UIService uiService, ActiveTeamService activeTeamService,
//             ActiveCardService activeCardService, FrozenHeroesService frozenHeroesService)
//         {
//             _eventBus = eventBus;
//             _blueHeroList = uiService.GetBluePlayerList();
//             _redHeroList = uiService.GetRedPlayerList();
//             _activeTeamService = activeTeamService;
//             _activeCardService = activeCardService;
//             _frozenHeroesService = frozenHeroesService;
//         }
//
//         public override async UniTask Run()
//         {
//             if (_activeTeamService.ActiveTeam == Team.Red)
//             {
//                 _views = _redHeroList.GetViews();
//             }
//
//             else
//             {
//                 _views = _blueHeroList.GetViews();
//             }
//
//             _activeCardService.SetActiveHeroView(GetNextHero());
//
//             _eventBus.RaiseEvent(new ActiveHeroChosenEvent(_activeCardService.ActiveHeroView));
//             await UniTask.Yield();
//         }
//
//         public HeroView GetNextHero()
//         {
//             if (_views == null || _views.Count == 0)
//                 return null;
//
//             var startIndex = 0;
//
//             if (_activeTeamService.ActiveTeam == Team.Red)
//             {
//                 if (_previousRedIndex >= 0 && _previousRedIndex < _views.Count)
//                     startIndex = (_previousRedIndex + 1) % _views.Count;
//             }
//             else
//             {
//                 if (_previousBlueIndex >= 0 && _previousBlueIndex < _views.Count)
//                     startIndex = (_previousBlueIndex + 1) % _views.Count;
//             }
//
//             for (var i = 0; i < _views.Count; i++)
//             {
//                 var index = (startIndex + i) % _views.Count;
//                 var candidate = _views[index];
//
//                 if (_frozenHeroesService.IsHeroFrozen(candidate))
//                 {
//                     _frozenHeroesService.UnfreezeHero(candidate);
//                     continue;
//                 }
//
//                 if (_activeTeamService.ActiveTeam == Team.Red)
//                     _previousRedIndex = index;
//                 else
//                     _previousBlueIndex = index;
//
//                 return candidate;
//             }
//
//             var fallback = _views[0];
//
//             if (_activeTeamService.ActiveTeam == Team.Red)
//                 _previousRedIndex = 0;
//             else
//                 _previousBlueIndex = 0;
//
//             return fallback;
//         }
//     }
// }
