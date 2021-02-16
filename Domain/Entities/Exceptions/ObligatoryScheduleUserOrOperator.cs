using System;
using System.Runtime.Serialization;

namespace Domain.Entities.Exceptions
{
    [Serializable]
    internal class ObligatoryScheduleUserOrOperator : Exception
    {
        public ObligatoryScheduleUserOrOperator()
        {
        }

        public ObligatoryScheduleUserOrOperator(string message) : base(message)
        {
        }

        public ObligatoryScheduleUserOrOperator(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ObligatoryScheduleUserOrOperator(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}