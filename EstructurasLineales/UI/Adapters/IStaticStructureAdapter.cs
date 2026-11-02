namespace EstructurasLineales.UI.Adapters
{
    /// <summary>
    /// Contrato para interactuar con cualquier estructura lineal estática desde la UI,
    /// abstrayendo las diferencias entre pilas, colas simples y colas circulares.
    /// </summary>
    public interface IStaticStructureAdapter
    {
        string Name { get; }
        string InsertOperationName { get; }
        string ExtractOperationName { get; }
        int Capacity { get; }
        int Count { get; }

        void Insert(string value);
        string Extract();
        string Peek();
        ArrayVisualState GetVisualState();
    }
}
