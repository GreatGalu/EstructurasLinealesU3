using System.Drawing;
using System.Windows.Forms;
using EstructurasLineales.UI.Tabs;

namespace EstructurasLineales.UI
{
    /// <summary>Ventana principal: una pestaña por cada tarea de la unidad.</summary>
    public class MainForm : Form
    {
        public MainForm()
        {
            Text = "Unidad 3 - Estructuras Lineales";
            Size = new Size(1150, 760);
            MinimumSize = new Size(950, 620);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9.5f);

            var tabs = new TabControl { Dock = DockStyle.Fill };
            AddTab(tabs, "Tarea 3.1 – Estáticas", new Task31Control());
            AddTab(tabs, "Tarea 3.2 – Dinámicas", new Task32Control());
            AddTab(tabs, "Tarea 3.3 – Aplicación", new Task33Control());
            Controls.Add(tabs);
        }

        private static void AddTab(TabControl tabControl, string title, Control content)
        {
            var page = new TabPage(title) { Padding = new Padding(4) };
            content.Dock = DockStyle.Fill;
            page.Controls.Add(content);
            tabControl.TabPages.Add(page);
        }
    }
}
