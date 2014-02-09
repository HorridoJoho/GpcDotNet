using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Gpc
{
	public partial class MainForm : Form
	{
		private sealed class PolygonWrapper
		{
			public readonly String Filepath;
			public readonly IPolygon Polygon;

			internal PolygonWrapper(String filepath, IPolygon polygon)
			{
				Filepath = filepath;
				Polygon = polygon;
			}

			public override string ToString()
			{
				return Filepath;
			}
		}

		public MainForm()
		{
			InitializeComponent();
		}

		private void _UpdatePictureBox()
		{
			if (pbDrawing.Image != null)
			{
				pbDrawing.Image.Dispose();
				pbDrawing.Image = null;
			}

			if (lbPolygons.Items.Count == 0)
			{
				return;
			}

			IPolygon finalPolygon = ((PolygonWrapper)lbPolygons.Items[0]).Polygon;
			bool first = true;
			bool disposeIt = false;

			foreach (Object obj in lbPolygons.Items)
			{
				if (first)
				{
					first = false;
					continue;
				}

				PolygonWrapper wrapper = (PolygonWrapper)obj;

				IPolygon oldPolygon = finalPolygon;
				finalPolygon = finalPolygon.ClipPolygon(wrapper.Polygon, (ClipOp)cbClipOps.SelectedItem);
				if (disposeIt)
				{
					oldPolygon.Dispose();
				}
				else
				{
					disposeIt = true;
				}
			}

			GraphicsPath polyPath = finalPolygon.ToGraphicsPath(ContourType.All, GraphicsPathType.Polygons);
			if (disposeIt)
			{
				finalPolygon.Dispose();
			}

			RectangleF bounds = polyPath.GetBounds();
			if ((int)bounds.Width == 0 || (int)bounds.Height == 0)
			{
				pbDrawing.Image = null;
				polyPath.Dispose();
				return;
			}
			Matrix translateMatrix = new Matrix();
			translateMatrix.Translate(bounds.X * -1.0f, bounds.Y * -1.0f);
			polyPath.Transform(translateMatrix);
			Image image = new Bitmap((int)bounds.Width, (int)bounds.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			using (Graphics bitmapGraphics = Graphics.FromImage(image))
			{
				bitmapGraphics.SmoothingMode = SmoothingMode.HighQuality;
				bitmapGraphics.Clear(Color.LightBlue);
				bitmapGraphics.FillPath(Brushes.LightGreen, polyPath);
				using (Pen p = new Pen(Color.DarkGreen, Math.Max(Math.Max(bounds.Width, bounds.Height) * 0.0025f, 2)))
				{
					bitmapGraphics.DrawPath(p, polyPath);
					p.Color = Color.DarkBlue;
					bitmapGraphics.DrawRectangle(p, 0, 0, bounds.Width, bounds.Height);
				}
			}

			polyPath.Dispose();
			pbDrawing.Image = image;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.cbClipOps.DataSource = System.Enum.GetValues(typeof(ClipOp));
		}

		private void cbClipOps_SelectedIndexChanged(object sender, EventArgs e)
		{
			_UpdatePictureBox();
		}

		private void btnAddPoly_Click(object sender, EventArgs e)
		{
			if (ofdPolygon.ShowDialog(this) == DialogResult.Cancel)
			{
				return;
			}

			using (Stream input = ofdPolygon.OpenFile())
			{
				PolygonWrapper wrapper = new PolygonWrapper(ofdPolygon.FileName, PolygonFactory.Read(new StreamReader(input, Encoding.ASCII), true));
				lbPolygons.Items.Add(wrapper);
				_UpdatePictureBox();
			}
		}

		private void btnRemPoly_Click(object sender, EventArgs e)
		{
			if (lbPolygons.SelectedIndex == -1)
			{
				return;
			}

			lbPolygons.Items.RemoveAt(lbPolygons.SelectedIndex);
			_UpdatePictureBox();
		}
	}
}
