// using UnityEngine;
// using UnityEngine.UI;
// // ReSharper disable UnusedMember.Global
//
// namespace Lessons.PRESENTATION.TUTORIAL
// {
//     public sealed class TutorialController_DisableCloseButton : MonoBehaviour
//     {
//         public void StartStep()
//         {
//             //Найти попап на сцене:
//             var buildPopup = FindObjectOfType<BuildPopup>();
//             
//             //Найти кнопку "Закрыть" и выкл ее:
//             foreach (Transform child in buildPopup.transform)
//             {
//                 if (child.name == "ButtonClose")
//                 {
//                     child.GetComponent<Button>().interactable = false;
//                 }
//             }
//         }
//     }
//
//     
//     
//     
//     public sealed class BuildPopup : MonoBehaviour
//     {
//     }
// }