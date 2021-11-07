using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            tempParentNode.Nodes = (ObservableCollection<NodeStepVM>)hierarchy;

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

            AddNeededItems(currentNode.Nodes, currentNumber, currentNode.Parent);
            var currentNumberIndex = currentNumber - 1;

            if (CountNumbers(depth) < 2)
            {
                if (currentNode.Nodes[currentNumberIndex] == null)
                {
                    currentNode.Nodes[currentNumberIndex] = new NodeStepVM(needSet.Parent, needSet.RawStepVM);
                }
                else
                {
                    currentNode.Nodes[currentNumberIndex].Parent = needSet.Parent;
                    currentNode.Nodes[currentNumberIndex].RawStepVM = needSet.RawStepVM;
                }

                return;
            }

            if (currentNode.Nodes[currentNumberIndex] == null)
            {
                currentNode.Nodes[currentNumberIndex] = new NodeStepVM(needSet.Parent, null);
            }

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

        private static void AddNeededItems(IList<NodeStepVM> nodes, int count, RawQuestVM parent)
        {
            for (var index = nodes.Count; index < count; index++)
            {
                var item = new NodeStepVM();
                item.Parent = parent;
                nodes.Add(item);
            }
        }
    }
}