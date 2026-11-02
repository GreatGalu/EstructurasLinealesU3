using System;
using System.Drawing;
using System.Windows.Forms;
using EstructurasLineales.Application;

namespace EstructurasLineales.UI.Controls
{
    /// <summary>
    /// Panel dedicado exclusivamente a la simulación del historial del navegador web con dos pilas.
    /// </summary>
    public class WebBrowserPanel : UserControl
    {
        private readonly WebBrowserHistory _browser = new();
        private readonly TextBox _txtUrl = new() { Width = 260 };
        private readonly Label _lblCurrent = new() { AutoSize = true, Padding = new Padding(6, 6, 0, 6), Font = new Font("Segoe UI", 11f, FontStyle.Bold) };
        private readonly ListBox _lstBack = new();
        private readonly ListBox _lstForward = new();
        private readonly Label _lblMessage = UiBuilder.MessageLabel();

        public WebBrowserPanel()
        {
            Dock = DockStyle.Fill;

            _txtUrl.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                Visit();
            };

            var bar = UiBuilder.Row(
                UiBuilder.Label("URL:"), _txtUrl,
                UiBuilder.Button("Ir →", (s, e) => Visit(), 70),
                UiBuilder.Button("← Atrás", (s, e) => GoBack(), 90),
                UiBuilder.Button("Adelante →", (s, e) => GoForward(), 100));

            var stacks = UiBuilder.Horizontal(
                (UiBuilder.Group("Pila ATRÁS (tope arriba)", _lstBack), UiBuilder.Col(50)),
                (UiBuilder.Group("Pila ADELANTE (tope arriba)", _lstForward), UiBuilder.Col(50)));

            var groupBrowser = UiBuilder.Group("Historial de navegación (2 pilas dinámicas)", UiBuilder.Vertical(
                (bar, UiBuilder.Auto),
                (_lblCurrent, UiBuilder.Auto),
                (_lblMessage, UiBuilder.Auto),
                (stacks, UiBuilder.Fill)));

            Controls.Add(groupBrowser);
            RefreshBrowser();
        }

        private void Visit()
        {
            try
            {
                _browser.Visit(_txtUrl.Text);
                _txtUrl.Clear();
                UiBuilder.Notify(_lblMessage, "Página visitada. El historial hacia adelante se borró.", false);
            }
            catch (ArgumentException ex) { UiBuilder.Notify(_lblMessage, ex.Message, true); }
            RefreshBrowser();
            _txtUrl.Focus();
        }

        private void GoBack()
        {
            if (_browser.Back()) UiBuilder.Notify(_lblMessage, "Retrocediste una página.", false);
            else UiBuilder.Notify(_lblMessage, "No hay páginas anteriores.", true);
            RefreshBrowser();
        }

        private void GoForward()
        {
            if (_browser.Forward()) UiBuilder.Notify(_lblMessage, "Avanzaste una página.", false);
            else UiBuilder.Notify(_lblMessage, "No hay páginas siguientes.", true);
            RefreshBrowser();
        }

        private void RefreshBrowser()
        {
            _lblCurrent.Text = "Página actual: " + (_browser.CurrentPage ?? "(en blanco)");

            _lstBack.Items.Clear();
            foreach (string url in _browser.BackHistory) _lstBack.Items.Add(url);

            _lstForward.Items.Clear();
            foreach (string url in _browser.ForwardHistory) _lstForward.Items.Add(url);
        }
    }
}
