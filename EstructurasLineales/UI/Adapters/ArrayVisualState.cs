using System.Collections.Generic;

namespace EstructurasLineales.UI.Adapters
{
    /// <summary>
    /// Representa el estado visual de un arreglo para ser mostrado en ArrayViewer.
    /// Contiene los valores de las celdas, las marcas o punteros y el texto descriptivo del estado.
    /// </summary>
    public class ArrayVisualState
    {
        public string?[] Cells { get; }
        public Dictionary<int, string> Markers { get; }
        public string StatusDescription { get; }

        public ArrayVisualState(string?[] cells, Dictionary<int, string> markers, string statusDescription)
        {
            Cells = cells;
            Markers = markers;
            StatusDescription = statusDescription;
        }
    }
}
