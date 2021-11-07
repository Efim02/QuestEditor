using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using QuestEditor.Constants;
using QuestEditor.Vms;

namespace QuestEditor.Utils
{
    /// <summary>
    ///     Функционал для работы отображения иерархии.
    /// </summary>
    public static class HierarchyVmUtils
    {
        /// <summary>
        ///     Установит нод, в правильной глубине.
        /// </summary>
        /// <param name="needSet">Нод.</param>
        /// <param name="hierarchy">Иерархия.</param>
        public static void SetNode(NodeStepVM needSet, IList<NodeStepVM> hierarchy)
        {
            var depth = needSet.RawStepVM.Number;
            var tempParentNode = new NodeStepVM();
            tempParentNode.Nodes = (ObservableCollection<NodeStepVM>) hierarchy;

            RecurseSetNode(tempParentNode, needSet, depth);
        }

        /// <summary>
        ///     Рекурсивно установить нод.
        /// </summary>
        /// <param name="currentNode">Текущий нод.</param>
        /// <param name="needSet">Нод который нужно установить.</param>
        /// <param name="depth">Оставшаяся глубина.</param>
        private static void RecurseSetNode(NodeStepVM currentNode, NodeStepVM needSet, string depth)
        {
            var currentNumber = GetCurrentNumber(depth);

            AddNeededItems(currentNode, currentNumber);
            var currentNumberIndex = currentNumber - 1;

            if (CountNumbers(depth) < 2)
            {
                if (currentNode.Nodes[currentNumberIndex] == null)
                {
                    currentNode.Nodes[currentNumberIndex] = new NodeStepVM(currentNode, needSet.RawStepVM);
                }
                else
                {
                    currentNode.Nodes[currentNumberIndex].Parent = currentNode;
                    currentNode.Nodes[currentNumberIndex].RawStepVM = needSet.RawStepVM;
                }

                return;
            }

            if (currentNode.Nodes[currentNumberIndex] == null)
                currentNode.Nodes[currentNumberIndex] = new NodeStepVM(currentNode, null);

            RemoveCurrentNumber(ref depth);

            // Окончание итерации, дальше в глубь.
            RecurseSetNode(currentNode.Nodes[currentNumberIndex], needSet, depth);
        }

        /// <summary>
        ///     Получить глубину нода.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static int CountNumbers(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            var numbersMas = text.Split('.');

            if (string.IsNullOrEmpty(numbersMas[numbersMas.Length - 1]))
                return numbersMas.Length - 1;

            return numbersMas.Length;
        }

        private static int GetCurrentNumber(string text)
        {
            var numbersMas = text.Split('.');

            if (int.TryParse(numbersMas[0], out var number))
                return number;

            throw new Exception($"Не спарсился номер шага: {text}");
        }

        private static void RemoveCurrentNumber(ref string text)
        {
            var numbersMas = text.Split('.');

            if (numbersMas.Length < 1)
            {
                text = "";
                return;
            }

            var result = "";
            for (var i = 1; i < numbersMas.Length; i++)
            {
                result += numbersMas[i];
                if (i != numbersMas.Length - 1)
                    result += ".";
            }

            text = result;
        }

        /// <summary>
        ///     Добавляет не хватающих элементов в узел.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="count"></param>
        private static void AddNeededItems(NodeStepVM parent, int count)
        {
            if (count > MaxMinValues.MAX_ITEM_ADDED)
                throw new Exception("Требуется добавить в иерархию слишком большое количество элементов.\n" +
                                    $"Node?Number: {parent.RawStepVM?.Number}; Количество: {count}.");

            var nodes = parent.Nodes;

            for (var index = nodes.Count; index < count; index++)
            {
                var item = new NodeStepVM();
                item.Parent = parent;
                nodes.Add(item);
            }
        }

        /// <summary>
        ///     Установить number для нового узла.
        /// </summary>
        /// <param name="parent">Родитель.</param>
        /// <param name="node">Нод.</param>
        public static void SetNumberForNewNode(NodeStepVM parent, NodeStepVM node)
        {
            node.RawStepVM.Number = $"{parent.RawStepVM.Number}.{parent.Nodes.Count + 1}";
        }

        /// <summary>
        ///     Пересчитывает number для узлов родителя.
        /// </summary>
        /// <param name="parent">Родитель.</param>
        public static void UpdateNumbersInNode(NodeStepVM parent)
        {
            var index = 1;
            foreach (var node in parent.Nodes)
            {
                if (parent.RawStepVM.Number.Length > 0)
                    node.RawStepVM.Number = $"{parent.RawStepVM.Number}.";

                node.RawStepVM.Number = $"{index}";
                index++;
            }
        }

        /// <summary>
        ///     Устанавливает все шаги из узлов, в Steps.
        /// </summary>
        /// <param name="rawQuestVm"></param>
        public static void AddAllNodesInSteps(RawQuestVM rawQuestVm)
        {
            var steps = rawQuestVm.Steps;
            steps.Clear();

            var hierarchy = rawQuestVm.Hierarchy;

            RecurseAllNodesInSteps(steps, hierarchy);
        }

        /// <summary>
        ///     Рекурсивная функция добавления всех шагов.
        /// </summary>
        /// <param name="steps">Общий список шагов.</param>
        /// <param name="nodes">Иерархия.</param>
        private static void RecurseAllNodesInSteps(IList<RawStepVM> steps, IList<NodeStepVM> nodes)
        {
            foreach (var node in nodes)
            {
                steps.Add(node.RawStepVM);
                RecurseAllNodesInSteps(steps, node.Nodes);
            }
        }
    }
}