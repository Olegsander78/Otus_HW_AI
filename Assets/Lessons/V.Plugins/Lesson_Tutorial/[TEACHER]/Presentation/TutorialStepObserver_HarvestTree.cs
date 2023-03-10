// using GameElements;
// using Sirenix.Utilities;
// // ReSharper disable UnusedType.Global
// // ReSharper disable UnusedMember.Global
//
// namespace Lessons.PRESENTATION.TUTORIAL
// {
//     
//     
//     
//     public class TutorialStepObserver_HarvestTree
//     {
//         private TutorialProvider_HarvestTrees treesProvider;
//
//         public void InitGame(IGameSystem gameSystem)
//         {
//             this.treesProvider = gameSystem.GetService<TutorialProvider_HarvestTrees>();
//         }
//         
//         public void OnStartStep()
//         {
//             this.treesProvider.targetTree.SetActive(true);
//             this.treesProvider.otherTress.ForEach(it => it.SetActive(false));
//         }
//
//         public void OnEndStep()
//         {
//             this.treesProvider.otherTress.ForEach(it => it.SetActive(true));
//         }
//     }
//     
//     
//     
//     
// }