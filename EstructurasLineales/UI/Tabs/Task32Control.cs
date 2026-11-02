using System.Windows.Forms;
using EstructurasLineales.UI.Controls;

namespace EstructurasLineales.UI.Tabs
{
    /// <summary>
    /// Pestaña de la Tarea 3.2: organiza y presenta las estructuras dinámicas
    /// (DoublyLinkedList, DynamicStack y DynamicQueue) compuestas por sus respectivos paneles.
    /// </summary>
    public class Task32Control : UserControl
    {
        public Task32Control()
        {
            Dock = DockStyle.Fill;

            var listPanel = new DoublyLinkedListPanel();
            var stackPanel = new DynamicStackPanel();
            var queuePanel = new DynamicQueuePanel();

            Controls.Add(UiBuilder.Vertical(
                (listPanel, UiBuilder.Pct(50)),
                (UiBuilder.Horizontal((stackPanel, UiBuilder.Col(50)), (queuePanel, UiBuilder.Col(50))), UiBuilder.Pct(50))));
        }
    }
}
