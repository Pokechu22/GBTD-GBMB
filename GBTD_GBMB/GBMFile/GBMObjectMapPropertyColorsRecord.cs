using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	public class GBMObjectMapPropertyColorsRecord
	{
		public UInt32 Property { get; set; }
		public PropertyColorOperator Operator { get; set; }
		public UInt32 Value { get; set; }

		public GBMObjectMapPropertyColorsRecord(Stream s) {
			this.Property = s.ReadInteger();
			this.Operator = (PropertyColorOperator)s.ReadInteger();
			this.Value = s.ReadInteger();
		}

		public void SaveToStream(Stream s) {
			s.WriteInteger(Property);
			s.WriteInteger((UInt32)Operator);
			s.WriteInteger(Value);
		}

		public TreeNode ToTreeNode(string name) {
			TreeNode node = new TreeNode(name);

			node.Nodes.Add("Property", "Property: " + Property);
			node.Nodes.Add("Operator", "Operator: '" + Operator.Name() + "' (" + ((UInt32)Operator).ToString("X2") + ")");
			node.Nodes.Add("Value", "Value: " + Value);

			return node;
		}
	}

	/// <summary>
	/// Different opperators available for color comparison.
	/// </summary>
	public enum PropertyColorOperator : uint
	{
		/// <summary>
		/// Actual must be equal to expected.
		/// <para><code>actual == expected</code></para>
		/// </summary>
		EQUAL = 0,
		/// <summary>
		/// Actual must not be equal to expected.
		/// <para><code>actual != expected</code></para>
		/// </summary>
		NOT_EQUAL = 1,
		/// <summary>
		/// Actual must be less than expected.
		/// <para><code>actual &lt; expected</code></para>
		/// </summary>
		LESS = 2,
		/// <summary>
		/// Actual must be more than expected.
		/// <para><code>actual &gt; expected</code></para>
		/// </summary>
		MORE = 3,
		/// <summary>
		/// Actual must be less than or equal to expected.
		/// <para><code>actual &lt;= expected</code></para>
		/// </summary>
		LESS_OR_EQUAL = 4,
		/// <summary>
		/// Actual must be equal to or more than expected.
		/// <para><code>actual &gt;= expected</code></para>
		/// </summary>
		MORE_OR_EQUAL = 5
	}

	public static class PropertyColorOperatorMethods
	{
		/// <summary>
		/// Checks if the operation is true.
		/// </summary>
		/// <param name="this">The property color to use.</param>
		/// <param name="expected">The value given to check.</param>
		/// <param name="actual">The actual value.</param>
		/// <returns></returns>
		public static bool IsTrue(this PropertyColorOperator @this, UInt32 actual, UInt32 expected) {
			switch (@this) {
			case PropertyColorOperator.EQUAL: return actual == expected;
			case PropertyColorOperator.NOT_EQUAL: return actual != expected;
			case PropertyColorOperator.LESS: return actual < expected;
			case PropertyColorOperator.MORE: return actual > expected;
			case PropertyColorOperator.LESS_OR_EQUAL: return actual <= expected;
			case PropertyColorOperator.MORE_OR_EQUAL: return actual >= expected;
			default: return false;
			}
		}

		/// <summary>
		/// Gets the name that GBMB would use for the specified opperator.
		/// </summary>
		/// <param name="this"></param>
		/// <returns></returns>
		public static string Name(this PropertyColorOperator @this) {
			switch (@this) {
			case PropertyColorOperator.EQUAL: return "=";
			case PropertyColorOperator.NOT_EQUAL: return "<>";
			case PropertyColorOperator.LESS: return "<";
			case PropertyColorOperator.MORE: return ">";
			case PropertyColorOperator.LESS_OR_EQUAL: return "<=";
			case PropertyColorOperator.MORE_OR_EQUAL: return ">=";
			default: return "Unknown (" + @this + " " + (int)(@this) + ")";
			}
		}
	}
}
