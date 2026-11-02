using System;

namespace EstructurasLineales.Static
{
    /// <summary>
    /// Cola simple (FIFO) con arreglo estático. front y rear solo avanzan:
    /// los espacios liberados por Dequeue NO se reutilizan (desperdicio),
    /// salvo que la cola quede vacía y se reinicien los índices.
    /// </summary>
    public class StaticLinearQueue<T>
    {
        private readonly T[] _data;
        private int _front = 0;
        private int _rear = -1;

        public int Capacity => _data.Length;
        public int Count => _rear - _front + 1;
        public int Front => _front;
        public int Rear => _rear;

        /// <summary>Copia del arreglo interno; la interfaz gráfica la usa solo para dibujar las celdas.</summary>
        public T[] CopyArray() => (T[])_data.Clone();

        public StaticLinearQueue(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "La capacidad debe ser mayor que cero.");
            _data = new T[capacity];
        }

        public bool IsEmpty() => _rear < _front;
        /// <summary>Llena cuando rear alcanzó el final, aunque haya huecos al inicio.</summary>
        public bool IsFull() => _rear == _data.Length - 1;

        public void Enqueue(T item)
        {
            if (IsFull()) throw new InvalidOperationException("Cola llena (rear llegó al final del arreglo).");
            _data[++_rear] = item;
        }

        public T Dequeue()
        {
            if (IsEmpty()) throw new InvalidOperationException("La cola está vacía.");
            T value = _data[_front];
            _data[_front++] = default!;
            if (IsEmpty()) { _front = 0; _rear = -1; } // reinicio al vaciarse
            return value;
        }

        public T Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException("La cola está vacía.");
            return _data[_front];
        }

        public override string ToString()
        {
            if (IsEmpty()) return "front -> [ ] <- rear (vacía)";
            var parts = new string[Count];
            for (int i = _front; i <= _rear; i++) parts[i - _front] = _data[i]?.ToString() ?? "null";
            return $"front(idx {_front}) -> [{string.Join(", ", parts)}] <- rear(idx {_rear})";
        }
    }
}
