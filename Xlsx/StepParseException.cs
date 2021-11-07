using System;

namespace QuestEditor.Xlsx
{
    public class StepParseException : Exception
    {
        private readonly string _message;

        private readonly string _questName;

        private readonly int _rowNumber;

        public StepParseException(string message, RawStep rawStep, string questName)
            : this(message, rawStep.RowNumber, questName)
        {
        }

        public StepParseException(string message, int rowNumber, string questName)
            : base(message)
        {
            _message = message;
            _rowNumber = rowNumber;
            _questName = questName;
        }

        public override string Message => $"{_questName} Ошибка в строке {_rowNumber}: {_message}";
    }
}