using System;

namespace EstructurasLineales.Static
{
    /// <summary>
    /// Cola circular con arreglo estático. Los índices avanzan con aritmética modular,
    /// por lo que se reutilizan los espacios liberados. Se usa un contador explícito
    /// para distinguir cola llena de cola vacía (ambas tendrían front == rear+1).
    /// </summary>
    public class StaticCircularQueue<T>
    {
        private readonly T[] _data;
        private int _front = 0;
        private int _rear = -1;
        private int _count = 0;

        public int Capacity => _data.Length;
        public int Count => _count;
        public int Front => _front;
        public int Rear => _rear;

        /// <summary>Copia del arreglo interno; la interfaz gráfica la usa solo para dibujar las celdas.</summary>
        public T[] CopyArray() => (T[])_data.Clone();

        public StaticCircularQueue(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "La capacidad debe ser mayor que cero.");
            _data = new T[capacity];
        }

        public bool IsEmpty() => _count == 0;
        public bool IsFull() => _count == _data.Length;

        public void Enqueue(T item)
        {
            if (IsFull()) throw new InvalidOperationException("Cola circular llena.");
            _rear = (_rear + 1) % _data.Length;
            _data[_rear] = item;
            _count++;
        }

        public T Dequeue()
        {
            if (IsEmpty()) throw new InvalidOperationException("La cola circular está vacía.");
            T value = _data[_front];
            _data[_front] = default!;
            _front = (_front + 1) % _data.Length;
            _count--;
            return value;
        }

        public T Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException("La cola circular está vacía.");
            return _data[_front];
        }

        public override string ToString()
        {
            if (IsEmpty()) return "front -> [ ] <- rear (vacía)";
            var parts = new string[_count];
            for (int i = 0; i < _count; i++)
                parts[i] = _data[(_front + i) % _data.Length]?.ToString() ?? "null";
            return $"front(idx {_front}) -> [{string.Join(", ", parts)}] <- rear(idx {_rear})";
        }
    }
}
