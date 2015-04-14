using System;
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

		private GBMObjectMapPropertiesRecord[] propertiesData;
		private GBMObjectMapPropertyColorsRecord[] propColorsData;

		private LocationPropertiesDialog() {

			InitializeComponent();
		}

		public LocationPropertiesDialog(GBMObjectMapProperties properties, GBMObjectMapPropertyColors propColors) : this() {
			this.Properties = properties;
			this.PropColors = propColors;

			this.propertiesData = properties.Properties;
			this.propColorsData = propColors.Data;

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

		private void removeButton_Click(object sender, EventArgs e) {
			//this.Properties.Master.PropCount--;
		}

		private void addButton_Click(object sender, EventArgs e) {
			//this.Properties.Master.PropCount++;
		}

		void PropCountChanged(object sender, EventArgs e) {
			redPropertyComboBox.Items.Clear();
			redPropertyComboBox.Items.AddRange(propertiesData.Select(r => r.Name).ToArray());
			greenPropertyComboBox.Items.Clear();
			greenPropertyComboBox.Items.AddRange(propertiesData.Select(r => r.Name).ToArray());

			if (redPropertyComboBox.SelectedIndex >= propertiesData.Length) {
				redPropertyComboBox.SelectedIndex = propertiesData.Length - 1;
			}
			if (greenPropertyComboBox.SelectedIndex >= propertiesData.Length) {
				greenPropertyComboBox.SelectedIndex = propertiesData.Length - 1;
			}
			//Make sure SelectedIndex isn't -1 if it can be anything else.
			if (propertiesData.Length != 0) {
				if (redPropertyComboBox.SelectedIndex < 0) {
					redPropertyComboBox.SelectedIndex = 0;
				}
				if (greenPropertyComboBox.SelectedIndex < 0) {
					greenPropertyComboBox.SelectedIndex = 0;
				}
			}

			removeButton.Enabled = (propertiesData.Length > 0);
			addButton.Enabled = (propertiesData.Length <= 32); //The maximum from GBMB.

			redPropertyComboBox.Enabled = (propertiesData.Length > 0);
			redOperatorComboBox.Enabled = (propertiesData.Length > 0);
			redOperandTextBox.Enabled = (propertiesData.Length > 0);

			greenPropertyComboBox.Enabled = (propertiesData.Length > 0);
			greenOperatorComboBox.Enabled = (propertiesData.Length > 0);
			greenOperandTextBox.Enabled = (propertiesData.Length > 0);
		}

		protected override void OnClosing(CancelEventArgs e) {
			//TODO validate data.
			//TODO check if dialogresult is OK.
			propColorsData[0].Property = (uint)redPropertyComboBox.SelectedIndex;
			propColorsData[0].Operator = (PropertyColorOperator)redOperatorComboBox.SelectedIndex;
			propColorsData[0].Value = redOperandTextBox.Value;

			propColorsData[1].Property = (uint)greenPropertyComboBox.SelectedIndex;
			propColorsData[1].Operator = (PropertyColorOperator)greenOperatorComboBox.SelectedIndex;
			propColorsData[1].Value = greenOperandTextBox.Value;

			base.OnClosing(e);
		}
	}
}
