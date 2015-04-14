﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.GBMFile;

namespace GB.GBMB.Dialogs
{
	public partial class LocationPropertiesDialog : Form
	{
		public GBMObjectMapProperties Properties {
			get;
			private set;
		}
		public GBMObjectMapPropertyColors PropColors {
			get;
			private set;
		}

		private LocationPropertiesDialog() {
			InitializeComponent();
		}

		public LocationPropertiesDialog(GBMObjectMapProperties properties, GBMObjectMapPropertyColors propColors) : this() {
			this.Properties = properties;
			this.PropColors = propColors;

			if (propColors.Master.PropColorCount < 2) {
				//Not valid right now.
				this.Close();
				return;
			}

			redPropertyComboBox.Items.AddRange(properties.Properties.Select(r => r.Name).ToArray());
			greenPropertyComboBox.Items.AddRange(properties.Properties.Select(r => r.Name).ToArray());

			redPropertyComboBox.SelectedIndex = (int)propColors.Data[0].Property;
			redOperatorComboBox.SelectedIndex = (int)propColors.Data[0].Operator;
			redOperandTextBox.Value = propColors.Data[0].Value;

			greenPropertyComboBox.SelectedIndex = (int)propColors.Data[1].Property;
			greenOperatorComboBox.SelectedIndex = (int)propColors.Data[1].Operator;
			greenOperandTextBox.Value = propColors.Data[1].Value;
		}

		protected override void OnClosing(CancelEventArgs e) {
			//TODO validate data.
			//TODO check if dialogresult is OK.
			PropColors.Data[0].Property = (uint)redPropertyComboBox.SelectedIndex;
			PropColors.Data[0].Operator = (PropertyColorOperator)redOperatorComboBox.SelectedIndex;
			PropColors.Data[0].Value = redOperandTextBox.Value;

			PropColors.Data[1].Property = (uint)greenPropertyComboBox.SelectedIndex;
			PropColors.Data[1].Operator = (PropertyColorOperator)greenOperatorComboBox.SelectedIndex;
			PropColors.Data[1].Value = greenOperandTextBox.Value;

			base.OnClosing(e);
		}
	}
}
