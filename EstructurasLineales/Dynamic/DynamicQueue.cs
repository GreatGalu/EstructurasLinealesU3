using System;

namespace EstructurasLineales.Dynamic
{
    /// <summary>Cola dinámica (FIFO): punteros a frente y final, enqueue/dequeue en O(1).</summary>
    public class DynamicQueue<T>
    {
        private Node<T>? _front;
        private Node<T>? _rear;

        public int Count { get; private set; }
        public bool IsEmpty() => _front == null;

        public void Enqueue(T data)
        {
            var newNode = new Node<T>(data);
            if (_rear == null) _front = _rear = newNode; else { _rear.Next = newNode; _rear = newNode; }
            Count++;
        }

        public T Dequeue()
        {
            if (_front == null) throw new InvalidOperationException("La cola está vacía.");
            T data = _front.Data;
            _front = _front.Next;
            if (_front == null) _rear = null;
            Count--;
            return data;
        }

        public T Peek() => _front != null ? _front.Data : throw new InvalidOperationException("La cola está vacía.");
    }
}
