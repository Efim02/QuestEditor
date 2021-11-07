using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;

namespace QuestEditor.Xlsx
{
    public class XlsxParser : IDisposable
    {
        private readonly IXLWorksheet _englishWorksheet;

        private readonly IXLWorksheet _russianWorksheet;

        private readonly Stream _stream;

        private readonly XLWorkbook _workbook;

        /// <summary>
        ///     Открытие Excel файла.
        /// </summary>
        /// <param name="stream"></param>
        public XlsxParser(Stream stream)
        {
            _stream = stream;
            _workbook = new XLWorkbook(_stream);
            _russianWorksheet = _workbook.Worksheets.Worksheet(1);

            if (_workbook.Worksheets.Count > 1)
                _englishWorksheet = _workbook.Worksheets.Worksheet(2);
        }

        /// <summary>
        ///     Создание Excel файла.
        /// </summary>
        public XlsxParser()
        {
            _workbook = new XLWorkbook();

            _russianWorksheet = _workbook.AddWorksheet("Русский");
            _russianWorksheet.DataType = XLDataType.Text;

            _englishWorksheet = _workbook.AddWorksheet("English");
            _englishWorksheet.DataType = XLDataType.Text;
        }

        public void Dispose()
        {
            _workbook.Dispose();
            _stream?.Dispose();
        }

        /// <summary>
        ///     Сохранит книгу.
        /// </summary>
        /// <param name="stream">Поток для записи.</param>
        public void Save(Stream stream)
        {
            _workbook.SaveAs(stream);
        }

        /// <summary>
        ///     Получение из ячейки значения, конкретного языка.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public Dictionary<Languages, string> GetCellDict(int row, int column)
        {
            var dict =
                new Dictionary<Languages, string>
                {
                    {Languages.Russian, _russianWorksheet.Cell(row, column).Value.ToString()}
                };

            if (_englishWorksheet != null)
                dict[Languages.English] = _englishWorksheet.Cell(row, column).Value.ToString();

            return dict;
        }

        /// <summary>
        ///     Установка в ячейку значения, конкретного языка.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="dictionary"></param>
        public void SetCellDict(int row, int column, Dictionary<Languages, string> dictionary)
        {
            _russianWorksheet.Cell(row, column).Value = dictionary[Languages.Russian];

            if (_englishWorksheet != null)
                _englishWorksheet.Cell(row, column).Value = dictionary[Languages.English];
        }

        /// <summary>
        ///     Парсим ячейки xlsx для партийный квестов и новелл.
        /// </summary>
        public RawQuest ParseParty()
        {
            var rawQuest = RawQuest.GetRawPartyQuest(this);

            if (string.IsNullOrWhiteSpace(rawQuest.StartImageName))
                throw new StepParseException("Не задана начальная картинка", 2, rawQuest.NameDict[0]);

            for (var stepRowIndex = 4; stepRowIndex < _russianWorksheet.RowCount(); ++stepRowIndex)
            {
                var rawStep = RawStep.GetRawStep(this, stepRowIndex, rawQuest.NameDict[0]);

                if (rawStep == null)
                    break;

                rawQuest.Steps.Add(rawStep);
            }

            return rawQuest;
        }

        /// <summary>
        ///     Записать партийный квест.
        /// </summary>
        public void WritePartyQuest(RawQuest rawQuest)
        {
            RawQuest.SetRawPartyQuest(this, rawQuest);

            if (string.IsNullOrWhiteSpace(rawQuest.StartImageName))
                throw new StepParseException("Не задана начальная картинка", 2, rawQuest.NameDict[0]);

            var stepRowIndex = 4;
            for (var i = 0; i < rawQuest.Steps.Count; ++i)
                RawStep.SetPartyRawStep(this, rawQuest.Steps[i], i + stepRowIndex);
        }

        /// <summary>
        ///     Записать событийный квест.
        /// </summary>
        public void WriteEventQuest(RawQuest rawQuest)
        {
            RawQuest.SetRawEventQuest(this, rawQuest);

            if (string.IsNullOrWhiteSpace(rawQuest.StartImageName))
                throw new StepParseException("Не задана начальная картинка", 2, rawQuest.NameDict[0]);

            var stepRowIndex = 4;
            for (var i = 0; i < rawQuest.Steps.Count; ++i)
                RawStep.SetEventRawStep(this, rawQuest.Steps[stepRowIndex + i], i + stepRowIndex);
        }

        /// <summary>
        ///     Записать видео квест.
        /// </summary>
        public void WriteVideoQuest(RawQuest rawQuest)
        {
            RawQuest.SetRawVideoQuest(this, rawQuest);

            if (string.IsNullOrWhiteSpace(rawQuest.StartImageName))
                throw new StepParseException("Не задана начальная картинка", 2, rawQuest.NameDict[0]);

            var stepRowIndex = 4;
            for (var i = 0; i < rawQuest.Steps.Count; ++i)
                RawStep.SetVideoRawStep(this, rawQuest.Steps[stepRowIndex + i], i + stepRowIndex);
        }

        /// <summary>
        ///     Парсим ячейки xlsx для событий.
        /// </summary>
        public RawQuest ParseEvent()
        {
            var rawQuest = RawQuest.GetRawEventQuest(this);

            for (var stepRowIndex = 4; stepRowIndex < _russianWorksheet.RowCount(); ++stepRowIndex)
            {
                var rawStep = RawStep.GetEventRawStep(this, stepRowIndex, rawQuest.NameDict[0]);

                if (rawStep == null)
                    break;

                rawQuest.Steps.Add(rawStep);
            }

            return rawQuest;
        }

        public RawQuest ParseVideo()
        {
            var rawQuest = RawQuest.GetRawVideoQuest(this);

            for (var stepRowIndex = 4; stepRowIndex < _russianWorksheet.RowCount(); ++stepRowIndex)
            {
                var rawStep = RawStep.GetVideoRawStep(this, stepRowIndex);

                if (rawStep == null)
                    break;

                rawQuest.Steps.Add(rawStep);
            }

            return rawQuest;
        }

        internal string GetCell(int row, int column)
        {
            return _russianWorksheet.Cell(row, column).Value.ToString();
        }

        internal float? GetCellFloat(int row, int column, string questName)
        {
            var stringValue = GetCell(row, column);

            if (string.IsNullOrWhiteSpace(stringValue))
                return null;

            if (!float.TryParse(stringValue, out var floatValue))
                throw new StepParseException($"Не удалось преобразовать во float {stringValue}", row, questName);

            return floatValue;
        }

        /// <summary>
        ///     Установить в ячейку данные.
        /// </summary>
        /// <param name="row">Номер строки.</param>
        /// <param name="column">Номер столбца.</param>
        /// <param name="value">Значение.</param>
        public void SetCell(int row, int column, object value)
        {
            if (value is null)
                return;

            _russianWorksheet.Cell(row, column).Value = value.ToString();
            _englishWorksheet.Cell(row, column).Value = value.ToString();
        }
    }
}