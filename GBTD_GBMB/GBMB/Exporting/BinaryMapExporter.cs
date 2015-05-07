using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBRFile;
using System.IO;
using GB.Shared.GBMFile;

namespace GB.GBMB.Exporting
{
	public sealed class BinaryMapExporter : IMapExporter
	{
		public bool SupportsExportMain {
			get { return true; }
		}

		public bool SupportsExportInclude {
			get { return false; }
		}

		public void ExportMain(GBMFile gbmFile, GBRFile gbrFile, Stream stream, string fileName) {
			using (BinaryWriter writer = new BinaryWriter(stream)) {
				var settings = gbmFile.GetOrCreateObjectOfType<GBMObjectMapExportSettings>();

				int planeCount = settings.PlaneCount.GetNumberOfPlanes();

				if (settings.PlaneOrder == PlaneOrder.Tiles_Are_Continues) {
					Byte[][] data = MapDataMaker.GetTileContinuousData(gbmFile, gbrFile);

					for (int i = 0; i < data.Length; i++) {
						writer.Write(data[i]);
					}
				} else { //Planes are continues.
					Byte[,][] planedData = MapDataMaker.GetPlaneContinuousData(gbmFile, gbrFile);

					int blockCount = planedData.GetLength(1);

					for (int block = 0; block < blockCount; block++) {
						for (int plane = 0; plane < planeCount; plane++) {
							writer.Write(planedData[plane, block]);
						}
					}
				}
			}
		}

		public void ExportInclude(GBMFile gbmFile, GBRFile gbrFile, Stream stream, string fileName) {
			throw new InvalidOperationException("This map exporter does not support exporting main files!");
		}
	}
}
