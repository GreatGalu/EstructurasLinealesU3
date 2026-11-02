using System;
using System.Collections.Generic;

namespace EstructurasLineales.Dynamic
{
    /// <summary>Pila dinámica (LIFO) sin límite de capacidad: lista simplemente enlazada, push/pop en la cabeza.</summary>
    public class DynamicStack<T>
    {
        private Node<T>? _top;

        public int Count { get; private set; }
        public bool IsEmpty() => _top == null;

        /// <summary>Recorre los elementos de la cima al fondo (solo lectura, para visualización).</summary>
        public IEnumerable<T> FromTopToBottom()
        {
            for (Node<T>? n = _top; n != null; n = n.Next) yield return n.Data;
        }

        public void Push(T data)
        {
            _top = new Node<T>(data) { Next = _top };
            Count++;
        }

        public T Pop()
        {
            if (_top == null) throw new InvalidOperationException("La pila está vacía.");
            T data = _top.Data;
            _top = _top.Next;
            Count--;
            return data;
        }

        public T Peek() => _top != null ? _top.Data : throw new InvalidOperationException("La pila está vacía.");

        public void Clear()
        {
            _top = null;
            Count = 0;
        }
    }
}
