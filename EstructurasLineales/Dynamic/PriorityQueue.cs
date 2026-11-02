using System;
using System.Collections.Generic;

namespace EstructurasLineales.Dynamic
{
    /// <summary>
    /// Cola de prioridad implementada con lista enlazada ordenada.
    /// Menor número = mayor prioridad; a igual prioridad se respeta el orden de llegada (FIFO estable).
    /// Enqueue: O(n) por inserción ordenada; Dequeue/Peek: O(1).
    /// </summary>
    public class PriorityQueue<T>
    {
        private class PriorityNode
        {
            public T Data;
            public int Priority;
            public PriorityNode? Next;

            public PriorityNode(T data, int priority)
            {
                Data = data;
                Priority = priority;
            }
        }

        private PriorityNode? _front;

        public int Count { get; private set; }
        public bool IsEmpty() => _front == null;

        public void Enqueue(T data, int priority)
        {
            var newNode = new PriorityNode(data, priority);
            if (_front == null || priority < _front.Priority)
            {
                newNode.Next = _front;
                _front = newNode;
            }
            else
            {
                PriorityNode current = _front;
                while (current.Next != null && current.Next.Priority <= priority)
                    current = current.Next;
                newNode.Next = current.Next;
                current.Next = newNode;
            }
            Count++;
        }

        public T Dequeue()
        {
            if (_front == null) throw new InvalidOperationException("La cola de prioridad está vacía.");
            T data = _front.Data;
            _front = _front.Next;
            Count--;
            return data;
        }

        public T Peek() => _front != null ? _front.Data : throw new InvalidOperationException("La cola de prioridad está vacía.");

        /// <summary>Recorre los elementos en orden de atención (solo lectura, para visualización).</summary>
        public IEnumerable<(T Data, int Priority)> Elements()
        {
            for (PriorityNode? n = _front; n != null; n = n.Next) yield return (n.Data, n.Priority);
        }
    }
}
