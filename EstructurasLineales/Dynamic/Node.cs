namespace EstructurasLineales.Dynamic
{
    /// <summary>Nodo doblemente enlazado: guarda el dato y dos referencias (siguiente/anterior).</summary>
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T>? Next { get; set; }
        public Node<T>? Previous { get; set; }

        public Node(T data)
        {
            Data = data;
        }
    }
}
