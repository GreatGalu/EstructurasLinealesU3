using System;

namespace EstructurasLineales.Static
{
    /// <summary>
    /// Pila (LIFO) con arreglo estático de capacidad fija.
    /// El índice top apunta al último elemento insertado (-1 = vacía).
    /// Todas las operaciones son O(1).
    /// </summary>
    public class StaticStack<T>
    {
        private readonly T[] _data;
        private int _top = -1;

        public int Capacity => _data.Length;
        public int Count => _top + 1;
        /// <summary>Índice de la cima (-1 si está vacía). Solo lectura, para visualización.</summary>
        public int Top => _top;

        /// <summary>Copia del arreglo interno; la interfaz gráfica la usa solo para dibujar las celdas.</summary>
        public T[] CopyArray() => (T[])_data.Clone();

        public StaticStack(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "La capacidad debe ser mayor que cero.");
            _data = new T[capacity];
        }

        public bool IsEmpty() => _top == -1;
        public bool IsFull() => _top == _data.Length - 1;

        /// <summary>Inserta un elemento en la cima. Lanza excepción si está llena (desbordamiento).</summary>
        public void Push(T item)
        {
            if (IsFull()) throw new InvalidOperationException("Desbordamiento: la pila está llena.");
            _data[++_top] = item;
        }

        /// <summary>Extrae y devuelve la cima. Lanza excepción si está vacía (subdesbordamiento).</summary>
        public T Pop()
        {
            if (IsEmpty()) throw new InvalidOperationException("Subdesbordamiento: la pila está vacía.");
            T value = _data[_top];
            _data[_top--] = default!; // libera la referencia
            return value;
        }

        /// <summary>Devuelve la cima sin extraerla.</summary>
        public T Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException("La pila está vacía.");
            return _data[_top];
        }

        public override string ToString()
        {
            if (IsEmpty()) return "[ ] (vacía)";
            var parts = new string[Count];
            for (int i = 0; i <= _top; i++) parts[i] = _data[i]?.ToString() ?? "null";
            return "[" + string.Join(", ", parts) + "] <- top";
        }
    }
}
