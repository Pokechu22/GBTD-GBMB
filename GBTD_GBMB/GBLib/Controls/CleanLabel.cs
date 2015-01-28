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
		protected override bool ProcessMnemonic(char charCode) {
			if (IsMnemonic(charCode, this.Text)) {
				//Based off of the Mono source, now I know how to do this.
				//https://github.com/mono/mono/blob/effa4c07ba850bedbe1ff54b2a5df281c058ebcb/mcs/class/Managed.Windows.Forms/System.Windows.Forms/Label.cs#L635
				if (this.Parent != null) {
					Parent.SelectNextControl(this, true, false, true, true);
				}
				return true;
			}
			return base.ProcessMnemonic(charCode);
		}

		protected override Padding DefaultMargin { get { return new Padding(0); } }
		protected override Padding DefaultPadding { get { return new Padding(0); } }

		private StringFormat format = new StringFormat() { HotkeyPrefix = HotkeyPrefix.Show };
		private TextRenderingHint renderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
		private SmoothingMode smoothingMode = SmoothingMode.None;
		private InterpolationMode interpolationMode = InterpolationMode.NearestNeighbor;
		private PixelOffsetMode pixelOffsetMode = PixelOffsetMode.None;

		private Color disabledForeColor = Color.FromArgb(128, 128, 128);
		private Color disabledBackColor = Color.FromArgb(255, 255, 255);

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

		[Category("Appearance"), Description("The color that should be used when this is disabled.")]
		public Color DisabledForeColor {
			get { return disabledForeColor; }
			set { disabledForeColor = value; this.Invalidate(); }
		}
		[Category("Appearance"), Description("The color that should be used when this is disabled.")]
		public Color DisabledBackColor {
			get { return disabledBackColor; }
			set { disabledBackColor = value; this.Invalidate(); }
		}
		
		public CleanLabel() {
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			this.SetStyle(ControlStyles.Selectable, false);
			this.TabStop = false;
		}

		protected override void OnEnabledChanged(EventArgs e) {
			base.OnEnabledChanged(e);
			this.Invalidate();
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

			if (this.Enabled) {
				using (Brush b = new SolidBrush(this.ForeColor)) {
					e.Graphics.DrawString(this.Text, this.Font, b, new RectangleF(0, 0, this.Size.Width, this.Size.Height), format);
				}
			} else {
				//ControlPaint.DrawStringDisabled may exist, but doesn't do what is needed.
				using (Brush b = new SolidBrush(this.disabledBackColor)) {
					e.Graphics.DrawString(this.Text, this.Font, b, new RectangleF(1, 1, this.Size.Width + 1, this.Size.Height + 1), format);
				}
				using (Brush b = new SolidBrush(this.disabledForeColor)) {
					e.Graphics.DrawString(this.Text, this.Font, b, new RectangleF(0, 0, this.Size.Width + 0, this.Size.Height + 0), format);
				}
			}
			base.OnPaint(e);
		}
	}
}
