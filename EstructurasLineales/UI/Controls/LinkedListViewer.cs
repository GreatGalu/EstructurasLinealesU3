using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EstructurasLineales.UI.Controls
{
    /// <summary>Dibuja una lista doblemente enlazada como nodos conectados con flechas.</summary>
    public class LinkedListViewer : Panel
    {
        private string[] _nodes = Array.Empty<string>();

        public LinkedListViewer()
        {
            DoubleBuffered = true;
            AutoScroll = true;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
        }

        public void Show(IEnumerable<string> data)
        {
            _nodes = data.ToArray();
            AutoScrollMinSize = new Size(120 + _nodes.Length * 110, 0);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.TranslateTransform(AutoScrollPosition.X, 0);
            using var font = new Font("Segoe UI", 10f);
            using var smallFont = new Font("Segoe UI", 8.5f);
            using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            if (_nodes.Length == 0)
            {
                g.DrawString("Lista vacía (cabeza = null, cola = null)", font, Brushes.Gray, new PointF(12, 40));
                return;
            }

            g.DrawString("null", smallFont, Brushes.Gray, new Rectangle(4, 40, 44, 30), format);
            for (int i = 0; i < _nodes.Length; i++)
            {
                var node = new Rectangle(60 + i * 110, 36, 70, 44);
                using (var brush = new SolidBrush(Color.LightSkyBlue)) g.FillRectangle(brush, node);
                g.DrawRectangle(Pens.Black, node);
                g.DrawString(_nodes[i], font, Brushes.Black, node, format);

                if (i == 0)
                    g.DrawString("cabeza", smallFont, Brushes.Firebrick, new Rectangle(node.X - 5, 10, node.Width + 10, 20), format);
                if (i == _nodes.Length - 1)
                    g.DrawString("cola", smallFont, Brushes.Firebrick, new Rectangle(node.X - 5, 84, node.Width + 10, 20), format);

                var link = new Rectangle(node.Right, 36, 40, 44);
                string text = i < _nodes.Length - 1 ? "<->" : "->";
                g.DrawString(i < _nodes.Length - 1 ? text : "null", smallFont, Brushes.Black, link, format);
            }
        }
    }
}
