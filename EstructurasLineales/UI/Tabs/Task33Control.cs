using System.Windows.Forms;
using EstructurasLineales.UI.Controls;

namespace EstructurasLineales.UI.Tabs
{
    /// <summary>
    /// Pestaña de la Tarea 3.3: organiza las aplicaciones prácticas de las estructuras lineales
    /// (Historial de navegación web con WebBrowserHistory y Sala de espera con WaitingRoom).
    /// </summary>
    public class Task33Control : UserControl
    {
        public Task33Control()
        {
            Dock = DockStyle.Fill;

            var browserPanel = new WebBrowserPanel();
            var waitingRoomPanel = new WaitingRoomPanel();

            Controls.Add(UiBuilder.Horizontal(
                (browserPanel, UiBuilder.Col(58)),
                (waitingRoomPanel, UiBuilder.Col(42))));
        }
    }
}
