using System;
using System.Collections;
using System.Collections.Generic;

namespace EstructurasLineales.Dynamic
{
    /// <summary>
    /// Lista doblemente enlazada genérica con punteros a cabeza y cola.
    /// Insertar/eliminar en extremos: O(1). Buscar / acceso por índice: O(n).
    /// </summary>
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        private Node<T>? _head;
        private Node<T>? _tail;

        public int Count { get; private set; }
        public bool IsEmpty() => Count == 0;

        public void AddFirst(T data)
        {
            var newNode = new Node<T>(data) { Next = _head };
            if (_head != null) _head.Previous = newNode; else _tail = newNode;
            _head = newNode;
            Count++;
        }

        public void AddLast(T data)
        {
            var newNode = new Node<T>(data) { Previous = _tail };
            if (_tail != null) _tail.Next = newNode; else _head = newNode;
            _tail = newNode;
            Count++;
        }

        public void InsertAt(int index, T data)
        {
            if (index < 0 || index > Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (index == 0) { AddFirst(data); return; }
            if (index == Count) { AddLast(data); return; }
            Node<T> current = GetNodeAt(index);
            var newNode = new Node<T>(data) { Next = current, Previous = current.Previous };
            current.Previous!.Next = newNode;
            current.Previous = newNode;
            Count++;
        }

        public T RemoveFirst()
        {
            if (_head == null) throw new InvalidOperationException("La lista está vacía.");
            T data = _head.Data;
            _head = _head.Next;
            if (_head != null) _head.Previous = null; else _tail = null;
            Count--;
            return data;
        }

        public T RemoveLast()
        {
            if (_tail == null) throw new InvalidOperationException("La lista está vacía.");
            T data = _tail.Data;
            _tail = _tail.Previous;
            if (_tail != null) _tail.Next = null; else _head = null;
            Count--;
            return data;
        }

        /// <summary>Elimina la primera aparición del dato. Devuelve false si no existe.</summary>
        public bool Remove(T data)
        {
            for (Node<T>? n = _head; n != null; n = n.Next)
            {
                if (!EqualityComparer<T>.Default.Equals(n.Data, data)) continue;
                if (n.Previous != null) n.Previous.Next = n.Next; else _head = n.Next;
                if (n.Next != null) n.Next.Previous = n.Previous; else _tail = n.Previous;
                Count--;
                return true;
            }
            return false;
        }

        public bool Contains(T data)
        {
            foreach (T d in this)
                if (EqualityComparer<T>.Default.Equals(d, data)) return true;
            return false;
        }

        public T First() => _head != null ? _head.Data : throw new InvalidOperationException("La lista está vacía.");
        public T Last() => _tail != null ? _tail.Data : throw new InvalidOperationException("La lista está vacía.");

        public T this[int index] => GetNodeAt(index).Data;

        private Node<T> GetNodeAt(int index)
        {
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
            Node<T> n;
            // Optimización: recorre desde el extremo más cercano.
            if (index < Count / 2)
            {
                n = _head!;
                for (int i = 0; i < index; i++) n = n.Next!;
            }
            else
            {
                n = _tail!;
                for (int i = Count - 1; i > index; i--) n = n.Previous!;
            }
            return n;
        }

        public void Clear()
        {
            _head = _tail = null;
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (Node<T>? n = _head; n != null; n = n.Next) yield return n.Data;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => IsEmpty() ? "null" : string.Join(" <-> ", this) + " -> null";
    }
}
