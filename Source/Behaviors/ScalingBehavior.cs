using System.Collections;
using System.Runtime.Serialization;
using Innoactive.Hub.Threading;
using Newtonsoft.Json;
using UnityEngine;

namespace Innoactive.Hub.Training.Template
{
    // Uncomment this attribute to use this behavior in the Step Inspector.
    // [ShowInTrainingMenu("Scale Object")]
    // This behaviors linearly changes scale of a Target object over Duration seconds, until it matches TargetScale.
    [DataContract(IsReference = true)]
    public class ScalingBehavior : Behavior
    {
        // Training object to scale.
        [DataMember]
        public TrainingObjectReference Target { get; private set; }

        // Target scale.
        [DataMember]
        [DisplayName("Target Scale")]
        public Vector3 TargetScale { get; private set; }

        // Duration of the animation in seconds.
        [DataMember]
        [DisplayName("Animation Duration")]
        public float Duration { get; private set; }

        // Handle data initialization in the constructor.
        [JsonConstructor]
        protected ScalingBehavior()
        {
            Target = new TrainingObjectReference();
            TargetScale = Vector3.one;
            Duration = 0f;
        }

        // Called on activation of the training entity. Define activation logic here.
        // You have to call `SignalActivationStarted()` at the start
        // and `SignalActivationFinished()` after you've done everything you wanted to do during the activation.
        public override void PerformActivation()
        {
            SignalActivationStarted();

            // Start coroutine which will scale our object.
            CoroutineDispatcher.Instance.StartCoroutine(ScaleTarget());
        }

        // Called on deactivation of the training entity. Define deactivation logic here.
        // You have to call `SignalDeactivationStarted()` at the start
        // and `SignalDeactivationFinished()` after you've done everything you wanted to do during the deactivation.
        public override void PerformDeactivation()
        {
            SignalDeactivationStarted();
            SignalDeactivationFinished();
        }

        // Coroutine which scales the target transform over time and then finished the activation.
        private IEnumerator ScaleTarget()
        {
            float startedAt = Time.time;

            Transform scaledTransform = Target.Value.GameObject.transform;

            Vector3 initialScale = scaledTransform.localScale;

            while (Time.time - startedAt < Duration)
            {
                float progress = (Time.time - startedAt) / Duration;

                scaledTransform.localScale = Vector3.Lerp(initialScale, TargetScale, progress);
                yield return null;
            }

            scaledTransform.localScale = TargetScale;

            SignalActivationFinished();
        }
    }
}
