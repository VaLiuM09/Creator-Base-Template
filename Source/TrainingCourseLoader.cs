using System.Collections;
using Innoactive.Hub.Training.Configuration;
using UnityEngine;

namespace Innoactive.Hub.Training.Template
{
    public class TrainingCourseLoader : MonoBehaviour
    {
        private IEnumerator Start()
        {
            // Skip the first two frames to give VRTK time to initialize.
            yield return null;
            yield return null;

            // Load the training course selected from the Runtime Configurator
            // in the '[TRAINING_CONFIGURATION]' game object in the scene.
            ICourse trainingCourse = RuntimeConfigurator.Configuration.LoadCourse();

            // Start the training execution.
            trainingCourse.Activate();
        }
    }
}
