using NUnit.Framework;

namespace Task.Tests
{
    public class QueueTests
    {
        [TestCase(new[] { 1, 2, 3, 4, 5, 6 }, 3, ExpectedResult = "1234563000")]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0}, 1, ExpectedResult = "12345678901000000000")]
        public string Enqueue_ElementEnqueued(int[] array, int element)
        {
            Queue<int> queue = new Queue<int>(array);
            queue.Enqueue(element);

            string result = string.Empty;
            foreach (var el in queue)
            {
                result += el;
            }

            return result;
        }

        [TestCase(new[] { 1, 2, 3, 4, 5, 6 }, ExpectedResult = 1)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, ExpectedResult = 1)]
        public int Dequeue_ElementDequeued(int[] array)
        {
            Queue<int> queue = new Queue<int>(array);
            return queue.Dequeue();
        }

        [TestCase(new[] { 1, 2, 3, 4, 5, 6 }, ExpectedResult = true)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, ExpectedResult = true)]
        public bool Clear_EmptyQueue(int[] array)
        {
            Queue<int> queue = new Queue<int>(array);
            queue.Clear();
            return queue.IsEmpty;
        }

        [TestCase(new[] { 1, 2, 3, 4, 5, 6 }, ExpectedResult = 1)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, ExpectedResult = 1)]
        public int Peek_HeadElement(int[] array)
        {
            Queue<int> queue = new Queue<int>(array);
            return queue.Peek();
        }

        [TestCase(new[] { 1, 2, 3, 4, 5, 6 }, 2, ExpectedResult = 3)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6 }, 8, ExpectedResult = 0)]
        public int GetElement_ElementByIndex(int[] array, int index)
        {
            Queue<int> queue = new Queue<int>(array);
            return queue.GetElement(index);
        }
    }
}
