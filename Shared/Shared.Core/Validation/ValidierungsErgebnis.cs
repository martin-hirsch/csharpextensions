namespace Shared.Core.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ValidierungsErgebnis<TValidatedObject>
    {
        private readonly ValidierungsErgebnis validierungsErgebnis;

        public bool HasErrors => validierungsErgebnis?.HasErrors ?? false;

        public bool HasValidationMessages => validierungsErgebnis.HasValidationMessages;

        public string JoinedErrorMessages
        {
            get
            {
                if (validierungsErgebnis == null || !validierungsErgebnis.Messages.Any(x => x.IsError))
                {
                    return string.Empty;
                }

                return validierungsErgebnis.Messages.Where(x => x.IsError).Select(x => x.Text).Aggregate((x, y) => x + " " + y);
            }
        }

        public TValidatedObject ValidatedObject { get; }

        public ValidierungsErgebnis(TValidatedObject originalObject, ValidierungsErgebnis validationResult)
        {
            ValidatedObject = originalObject;
            validierungsErgebnis = validationResult ?? throw new ArgumentException(nameof(validationResult));
        }

        public static ValidierungsErgebnis<TValidatedObject> Error(TValidatedObject validatedObject, string message)
        {
            return new ValidierungsErgebnis<TValidatedObject>(validatedObject, new ValidierungsErgebnis().AddError(message));
        }

        public IEnumerable<string> GetMessagesTexts()
        {
            return validierungsErgebnis.GetMessageTexts();
        }

        public static ValidierungsErgebnis<TValidatedObject> Ok(TValidatedObject validatedObject)
        {
            return new ValidierungsErgebnis<TValidatedObject>(validatedObject, new ValidierungsErgebnis());
        }

        public void ThrowIfInvalid()
        {
            validierungsErgebnis.ThrowIfInvalid();
        }

        public static implicit operator ValidierungsErgebnis(ValidierungsErgebnis<TValidatedObject> me)
        {
            return me.validierungsErgebnis;
        }
    }

    public class ValidierungsErgebnis
    {
        private readonly List<ValidationMessage> _messages = new List<ValidationMessage>();

        public string Fehlertext
        {
            get
            {
                if (!_messages.Any(x => x.IsError))
                {
                    return string.Empty;
                }

                return _messages.Where(x => x.IsError).Select(x => x.Text).Aggregate((x, y) => x + " " + y);
            }
        }

        public bool HasErrors => _messages.Any(x => x.IsError);

        public bool HasValidationMessages => _messages?.Any() ?? false;

        public bool IsOk => !HasValidationMessages;

        public IEnumerable<ValidationMessage> Messages => _messages ?? Enumerable.Empty<ValidationMessage>();

        public static ValidierungsErgebnis Success => new ValidierungsErgebnis();

        public ValidierungsErgebnis()
        {
        }

        public ValidierungsErgebnis(IEnumerable<ValidierungsErgebnis> other)
        {
            if (other == null)
            {
                return;
            }

            foreach (var message in other.SelectMany(x => x.Messages))
            {
                _messages.Add(message);
            }
        }

        private ValidierungsErgebnis(IEnumerable<ValidationMessage> messages)
        {
            _messages = messages.ToList();
        }

        public ValidierungsErgebnis AddError(string message)
        {
            _messages.Add(new ValidationMessage(ValidationMessageLevel.Error, message));
            return new ValidierungsErgebnis(_messages);
        }

        public ValidierungsErgebnis AddInfo(string message)
        {
            _messages.Add(new ValidationMessage(ValidationMessageLevel.Info, message));
            return new ValidierungsErgebnis(_messages);
        }

        public ValidierungsErgebnis AddRange(IEnumerable<ValidationMessage> validationMessages)
        {
            foreach (var validationMessage in validationMessages)
            {
                _messages.Add(validationMessage);
            }

            return new ValidierungsErgebnis(_messages);
        }

        public ValidierungsErgebnis AddWarning(string message)
        {
            _messages.Add(new ValidationMessage(ValidationMessageLevel.Warning, message));
            return new ValidierungsErgebnis(_messages);
        }

        public ValidierungsErgebnis AssertIsNotNull(object other, string name)
        {
            var result = new ValidierungsErgebnis();

            if (other == null)
            {
                result = Error($"Der Wert {name} darf nicht null sein.");
            }

            return result;
        }

        public static ValidierungsErgebnis Error(string message)
        {
            var validationResult = new ValidierungsErgebnis();
            validationResult.AddError(message);
            return validationResult;
        }

        public IEnumerable<string> Fehlertexte()
        {
            return _messages.Select(x => x.Text);
        }

        public string GetMessage()
        {
            return string.Join("\n", GetMessageTexts());
        }

        public IEnumerable<string> GetMessageTexts()
        {
            return _messages.Select(x => x.Text);
        }

        public static ValidierungsErgebnis Info(string message)
        {
            return new ValidierungsErgebnis().AddInfo(message);
        }

        public ValidierungsErgebnis Merge(ValidierungsErgebnis other)
        {
            var result = new ValidierungsErgebnis();

            result = result.Merge(this);
            return result.Merge(other);
        }

        public ValidierungsErgebnis PrependError(string other)
        {
            var result = new ValidierungsErgebnis();

            result = result.AddError(other);
            result = result.AddMessages(_messages);

            return result;
        }

        public ValidierungsErgebnis PrependMessage(ValidationMessageLevel validationMessageLevel, string message)
        {
            var result = new ValidierungsErgebnis();

            result = result.AddMessage(validationMessageLevel, message);
            result = result.AddMessages(_messages);

            return result;
        }

        public void ThrowIfInvalid()
        {
            if (_messages.Any(x => x.IsError))
            {
                throw new Exception(Fehlertext);
            }
        }

        private ValidierungsErgebnis AddMessage(ValidationMessageLevel validationMessageLevel, string message)
        {
            _messages?.Add(new ValidationMessage(validationMessageLevel, message));
            return new ValidierungsErgebnis(_messages);
        }

        private ValidierungsErgebnis AddMessages(IEnumerable<ValidationMessage> validationMessages)
        {
            _messages.AddRange(validationMessages);
            return new ValidierungsErgebnis(_messages);
        }
    }
}