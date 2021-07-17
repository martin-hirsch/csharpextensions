namespace Shared.Core.Validation
{
    public class ValidationMessage
    {
        public bool IsError => Level == ValidationMessageLevel.Error;

        public bool IsWarning => Level == ValidationMessageLevel.Warning;

        public ValidationMessageLevel Level { get; }

        public string Text { get; }

        public ValidationMessage(ValidationMessageLevel level, string text)
        {
            Level = level;
            Text = text;
        }
    }
}