using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace GB.Shared.Controls
{
	/// <summary>
	/// Label that renders without antialiasing.
	/// </summary>
	public class CleanLabel : Control
	{
		protected override Padding DefaultMargin { get { return new Padding(0); } }
		protected override Padding DefaultPadding { get { return new Padding(0); } }

		private StringFormat format = new StringFormat() { HotkeyPrefix = HotkeyPrefix.Show };
		private TextRenderingHint renderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
		private SmoothingMode smoothingMode = SmoothingMode.None;
		private InterpolationMode interpolationMode = InterpolationMode.NearestNeighbor;
		private PixelOffsetMode pixelOffsetMode = PixelOffsetMode.None;

		[Category("Format"), Description("The StringFormat used to render.")]
		public StringFormat Format {
			get { return format; }
			set { if (value == null) { throw new ArgumentNullException("value"); } format = value; this.Invalidate(); }
		}

		[Category("Format"), Description("The TextRenderingHint used to render.")]
		[DefaultValue(TextRenderingHint.SingleBitPerPixelGridFit)]
		public TextRenderingHint RenderingHint {
			get { return renderingHint; }
			set { renderingHint = value; this.Invalidate(); }
		}
		[Category("Format"), Description("The SmoothingMode used to render.")]
		[DefaultValue(SmoothingMode.None)]
		public SmoothingMode SmoothingMode {
			get { return smoothingMode; }
			set { smoothingMode = value; this.Invalidate(); }
		}
		[Category("Format"), Description("The InterpolationMode used to render.")]
		[DefaultValue(InterpolationMode.NearestNeighbor)]
		public InterpolationMode InterpolationMode {
			get { return interpolationMode; }
			set { interpolationMode = value; this.Invalidate(); }
		}
		[Category("Format"), Description("The PixelOffsetMode used to render.")]
		[DefaultValue(PixelOffsetMode.None)]
		public PixelOffsetMode PixelOffsetMode {
			get { return pixelOffsetMode; }
			set { pixelOffsetMode = value; this.Invalidate(); }
		}
		
		public CleanLabel() {
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		protected override void OnTextChanged(EventArgs e) {
			using (Graphics g = this.CreateGraphics()) {
				g.TextRenderingHint = this.renderingHint;
				g.SmoothingMode = this.smoothingMode;
				g.InterpolationMode = this.interpolationMode;
				g.PixelOffsetMode = this.pixelOffsetMode;

				Size size = g.MeasureString(this.Text, this.Font, int.MaxValue, format).ToSize();
				size.Width++;
				size.Height++;
				this.Size = size;
			}
		}

		protected override void OnPaint(PaintEventArgs e) {
			e.Graphics.TextRenderingHint = this.renderingHint;
			e.Graphics.SmoothingMode = this.smoothingMode;
			e.Graphics.InterpolationMode = this.interpolationMode;
			e.Graphics.PixelOffsetMode = this.pixelOffsetMode;

			using (Brush b = new SolidBrush(this.ForeColor)) {
				e.Graphics.DrawString(this.Text, this.Font, b, new RectangleF(0, 0, this.Size.Width, this.Size.Height), format);
			}
			base.OnPaint(e);
		}
	}
}
