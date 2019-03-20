using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Innoactive.Hub.Training.Template
{
    // Uncomment this attribute to use this condition in the Step Inspector.
    // [ShowInTrainingMenu("Point at Object")]
    [DataContract(IsReference = true)]
    // Condition which is completed when Pointer points at Target.
    public class PointedCondition : Condition
    {
        [DataMember]
        // Reference to a pointer property.
        public TrainingPropertyReference<PointingProperty> Pointer { get; private set; }

        [DisplayName("Target with a collider")]
        [DataMember]
        // Reference to a target property.
        public TrainingPropertyReference<ColliderWithTriggerProperty> Target { get; private set; }

        [JsonConstructor]
        // Make sure that references are initialized.
        public PointedCondition()
        {
            Pointer = new TrainingPropertyReference<PointingProperty>();
            Target = new TrainingPropertyReference<ColliderWithTriggerProperty>();
        }

        // This method is called when the step with that condition has completed activation of its behaviors.
        public override void OnActivate()
        {
            Pointer.Value.PointerEnter += OnPointerEnter;
        }

        // This method is called at deactivation of the step, after every behavior has completed its deactivation.
        public override void OnDeactivate()
        {
            Pointer.Value.PointerEnter -= OnPointerEnter;
        }

        // When PointerProperty points at something,
        private void OnPointerEnter(ColliderWithTriggerProperty pointed)
        {
            // Ignore it if this condition is already fulfilled.
            if (IsCompleted)
            {
                return;
            }

            // Else, if Target references the pointed object, complete the condition.
            if (Target.Value == pointed)
            {
                MarkAsCompleted();
            }
        }
    }
}
