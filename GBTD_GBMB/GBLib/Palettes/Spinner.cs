using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.Shared.Palettes
{
	/// <summary>
	/// Provides a spinner control that can be used independantly.
	/// Based off of the inner class of UpDownBase.  
	/// </summary>
	public partial class Spinner : UserControl
	{
		/// <summary>
		/// Paints a button.
		/// </summary>
		private static class SpinnerButtonRenderer
		{
			public static Image Render(ButtonState state, ScrollButton button, Size size) {
				Bitmap returned;

				//Wacky code to make the thing sized properly.  Some of this is just making sure that it's scalled with odd/even pixels.
				Rectangle buttonRect = new Rectangle(-2,
					-4 + (size.Height & 0x01), //Up 4 if even, up 3 if odd.  
					(size.Width | 0x01) + 7, //Size of 7 if odd, 8 if even.
					(size.Height | 0x01) + 7); //Size of 7 if odd, 8 if even.

				Rectangle borderRect = new Rectangle(0, 0, size.Width, size.Height);
				Rectangle borderRectIn = new Rectangle(1, 1, size.Width - 2, size.Height - 2);

				using (Bitmap temp = new Bitmap(size.Width, size.Height)) {
					using (Graphics g = Graphics.FromImage(temp)) {
						if (state.HasFlag(ButtonState.Pushed)) {
							ControlPaint.DrawScrollButton(g, buttonRect, button, state);
							ControlPaint.DrawBorder3D(g, borderRect, Border3DStyle.SunkenInner, Border3DSide.Right | Border3DSide.Bottom);
							ControlPaint.DrawBorder3D(g, borderRect, Border3DStyle.SunkenInner, Border3DSide.Top | Border3DSide.Left);
							ControlPaint.DrawBorder3D(g, borderRectIn, Border3DStyle.SunkenOuter, Border3DSide.Top | Border3DSide.Left);
						} else {
							ControlPaint.DrawScrollButton(g, buttonRect, button, state);
							ControlPaint.DrawBorder3D(g, borderRect, Border3DStyle.RaisedInner, Border3DSide.Right | Border3DSide.Bottom);
							ControlPaint.DrawBorder3D(g, borderRect, Border3DStyle.RaisedInner, Border3DSide.Top | Border3DSide.Left);
						}
					}

					returned = new Bitmap(temp);
				}
				return returned;
			}
		}

		protected override Size DefaultSize { get { return new Size(16, 22); } }

		private enum SpinnerButton {
			UP, DOWN
		}

		SpinnerButton? clicked = null;
		//SpinnerButton? hovered = null; //Not currently used.

		private int timerInterval = 100;
		public int TimerInterval {
			get { return timerInterval; }
			set { timer.Interval = timerInterval = value; }
		}

		/// <summary>
		/// Occurs when the up button is pressed - either when clicked or when the timer ticks.
		/// </summary>
		public event EventHandler Up;

		/// <summary>
		/// Occurs when the down button is pressed - either when clicked or when the timer ticks.
		/// </summary>
		public event EventHandler Down;

		private void StartButtonPress(MouseEventArgs e) {
			if (e.Y < this.Height / 2) {
				clicked = SpinnerButton.UP;
			} else {
				clicked = SpinnerButton.DOWN;
			}
			this.Capture = true;

			timer.Start();

			this.Invalidate(); //Redraw buttons
		}

		private void EndButtonPress() {
			clicked = null;
			this.Capture = false;
			StopTimer();

			this.Invalidate(); //Redraw buttons
		}

		/// <summary>
		/// Stops the timer
		/// </summary>
		private void StopTimer() {
			timer.Stop();
		}

		/// <summary>
		/// Starts the timer, and resets the interval.
		/// </summary>
		private void StartTimer() {
			timer.Interval = timerInterval;
			timer.Start();
		}

		public Spinner() {
			InitializeComponent();
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			if (e.Button.HasFlag(MouseButtons.Left)) {
				StartButtonPress(e);
			}

			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e) {
			if (e.Button.HasFlag(MouseButtons.Left)) {
				EndButtonPress();
			}

			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			//Currently unused as it doesn't render.
			//Validate which button is hovered.
			/*Rectangle upperButtonBounds = this.ClientRectangle;
			upperButtonBounds.Height /= 2;
			Rectangle lowerButtonBounds = this.ClientRectangle;
			lowerButtonBounds.Height -= upperButtonBounds.Height;
			lowerButtonBounds.Y = upperButtonBounds.Bottom;

			if (upperButtonBounds.Contains(e.Location)) {
				hovered = SpinnerButton.DOWN;
				Invalidate();
			} else if (lowerButtonBounds.Contains(e.Location)) {
				hovered = SpinnerButton.UP;
				Invalidate();
			}*/

			base.OnMouseMove(e);
		}

		protected override void OnMouseLeave(EventArgs e) {
			//hovered = null;

			this.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e) {
			Rectangle upperButtonBounds = this.ClientRectangle;
			upperButtonBounds.Height /= 2;
			Rectangle lowerButtonBounds = this.ClientRectangle;
			lowerButtonBounds.Height -= upperButtonBounds.Height;
			lowerButtonBounds.Y = upperButtonBounds.Bottom;

			upperButtonBounds.Y++;
			upperButtonBounds.X++;
			upperButtonBounds.Width--;
			upperButtonBounds.Height--;
			lowerButtonBounds.X++;
			lowerButtonBounds.Width--;
			lowerButtonBounds.Height--;

			ButtonState lowerButtonState = (clicked == SpinnerButton.DOWN ? ButtonState.Pushed : ButtonState.Normal);
			ButtonState upperButtonState = (clicked == SpinnerButton.UP ? ButtonState.Pushed : ButtonState.Normal);

			if (!Enabled) {
				upperButtonState = lowerButtonState = ButtonState.Inactive;
			}

			if (clicked == SpinnerButton.DOWN) {
				ControlPaint.DrawBorder3D(e.Graphics, e.ClipRectangle, Border3DStyle.SunkenOuter, Border3DSide.Bottom);
				ControlPaint.DrawBorder3D(e.Graphics, e.ClipRectangle, Border3DStyle.SunkenOuter, Border3DSide.Left | Border3DSide.Top);
			} else {
				ControlPaint.DrawBorder3D(e.Graphics, e.ClipRectangle, Border3DStyle.SunkenOuter, Border3DSide.Left | Border3DSide.Top);
				ControlPaint.DrawBorder3D(e.Graphics, e.ClipRectangle, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
			}

			e.Graphics.DrawImage(SpinnerButtonRenderer.Render(upperButtonState, ScrollButton.Up, upperButtonBounds.Size), upperButtonBounds);
			e.Graphics.DrawImage(SpinnerButtonRenderer.Render(lowerButtonState, ScrollButton.Down, lowerButtonBounds.Size), lowerButtonBounds);
		}

		private void timer_Tick(object sender, EventArgs e) {
			if (!Capture) {
				EndButtonPress();
				return;
			}

			if (!clicked.HasValue) {
				EndButtonPress();
				return;
			}

			switch (clicked.Value) {
			case SpinnerButton.UP: OnUp(); break;
			case SpinnerButton.DOWN: OnDown(); break;
			}

			//Accelerate the timer.
			int accelerated = (timer.Interval * 7) / 10;
			if (accelerated < 1) { accelerated = 1; }
			timer.Interval = accelerated;
		}

		private void OnUp() {
			if (Up != null) {
				Up(this, new EventArgs());
			}
		}

		private void OnDown() {
			if (Down != null) {
				Down(this, new EventArgs());
			}
		}
	}
}
