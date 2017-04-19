using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Task
{
    /// <summary>
    /// Represents a first-in, first-out collection of objects.
    /// </summary>
    /// <typeparam name="T">Specifies the type of elements in the queue.</typeparam>
    public class Queue<T> : IEnumerable, IEnumerable<T>
    {
        #region Private fields

        private T[] _array;
        private int _head;
        private int _tail;
        private int _size;
        private const int DefaultCapacity = 10;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of elements contained in the Queue.
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// Defines whether the Query is empty
        /// </summary>
        public bool IsEmpty => _size == 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Queue class that is empty and has the default initial capacity.
        /// </summary>
        public Queue() : this(DefaultCapacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Queue class that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the Queue can contain.</param>
        /// <exception cref="ArgumentException"></exception>
        public Queue(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException();

            Capacity = capacity;
            _array = new T[Capacity];
            _size = _head = _tail = 0;
        }

        /// <summary>
        /// Initializes a new instance of th eQueue class that contains elements copied from the specified collection 
        /// and has sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="queue">The collection whose elements are copied to the new Queue.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Queue(IEnumerable<T> queue)
        {
            if (ReferenceEquals(queue, null))
                throw new ArgumentNullException();

            _array = new T[Math.Max(queue.ToArray().Length, DefaultCapacity)];
            _size = _head = _tail = 0;
            foreach (var element in queue)
            {
                Enqueue(element);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Adds an object to the end of the Queue.
        /// </summary>
        /// <param name="element">The object to add to the Queue. The value can be null for reference types.</param>
        public void Enqueue(T element)
        {
            if (_size == Capacity)
            {
                Capacity += _array.Length;
                T[] newArray = new T[Capacity];
                Array.Copy(_array, 0, newArray, 0, _array.Length);
                _array = newArray;
            }

            _size++;
            _array[_tail++ % Capacity] = element;
        }

        /// <summary>
        /// Removes and returns the object at the beginning of the Queue.
        /// </summary>
        /// <returns>The object that is removed from the beginning of the Queue.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Dequeue()
        {
            if(_size == 0)
                throw new InvalidOperationException();

            _size--;
            return _array[_head++ % Capacity];
        }

        /// <summary>
        /// Removes all objects from the Queue.
        /// </summary>
        public void Clear()
        {
            if (_head < _tail)
                Array.Clear(_array, _head, _size);
            else
            {
                Array.Clear(_array, _head, _array.Length - _head);
                Array.Clear(_array, 0, _head);
            }
            _head = _tail = _size = 0;
        }

        /// <summary>
        /// Returns the object at the beginning of the Queue without removing it.
        /// </summary>
        /// <returns>The object at the beginning of the Queue.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Peek()
        {
            if (_size == 0)
                throw new InvalidOperationException();

            return _array[_head];
        }

        /// <summary>
        /// Returns the object at the specified index of the Queue without removing it.
        /// </summary>
        /// <param name="index">Index of the object to peek</param>
        /// <returns>The object at the specified index of the Queue.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T GetElement(int index)
        {
            if (index < 0)
                throw new InvalidOperationException();

            return _array[(_head + index) % _array.Length];
        }

        /// <summary>
        /// Returns an enumerator that iterates through the Queue.
        /// </summary>
        /// <returns>An Queue.Enumerator for the Queue.</returns>
        public IEnumerator<T> GetEnumerator() => new Iterator(this);

        /// <summary>
        /// Returns an enumerator that iterates through the Queue.
        /// </summary>
        /// <returns>An Queue.Enumerator for the Queue.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Private methods

        /// <summary>
        /// Represents iterator for the Queue
        /// </summary>
        private struct Iterator : IEnumerator<T>
        {
            private readonly Queue<T> _queue;
            private int _currentIndex;
            private T _currentElement;

            /// <summary>
            /// Initializes a new instance of the Iterator class that contains the specified queue instance.
            /// </summary>
            /// <param name="queue">The collection of elements that the Iterator contains</param>
            public Iterator(Queue<T> queue)
            {
                _currentIndex = -1;
                _queue = queue;
                _currentElement = default(T);
            }

            /// <summary>
            /// Returns the current element of the Queue
            /// </summary>
            public T Current
            {
                get
                {
                    if (_currentIndex == -1 || _currentIndex == _queue.Capacity)
                        throw new InvalidOperationException();
                    return _currentElement;
                }
            }

            /// <summary>
            /// Returns the current element of the Queue
            /// </summary>
            object IEnumerator.Current
            {
                get
                {
                    if (_currentIndex < 0)
                        throw new InvalidOperationException();
                    return _currentElement;
                }
            }

            /// <summary>
            /// Sets the Iterator to the beginning of the Queue
            /// </summary>
            public void Reset()
            {
                _currentIndex = -1;
                _currentElement = default(T);
            }

            /// <summary>
            /// Moves Iterator to the next element of the Queue
            /// </summary>
            /// <returns>False if the Iterator has reached the Queue's end; otherwise false</returns>
            public bool MoveNext()
            {
                if (_currentIndex == -2)
                    return false;

                _currentIndex++;
                if (_currentIndex == _queue.Capacity)
                {
                    Reset();
                    return false;
                }

                _currentElement = _queue.GetElement(_currentIndex);
                return true;
            }
            /// <summary>
            /// Disposes the Iterator instance
            /// </summary>
            public void Dispose() { }
        }

        #endregion
    }
}
