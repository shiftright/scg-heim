using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftRight.Common.Extensions {
	public static class StreamExtensions {

		struct FileSignature {
			public byte[] Hex;
			public string Extension;
		}

		private static int _longestSignature = 0;

		//private static Dictionary<byte[], string> _signatures = new Dictionary<byte[], string>();
		private static FileSignature[] _signatures = new FileSignature[]{
														new FileSignature{
															Hex = new byte[]{ 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 },
															Extension = ".gif"
														},
														new FileSignature{
															Hex = new byte[]{ 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 },
															Extension = ".gif"
														},
														new FileSignature{
															Hex = new byte[]{ 0x50, 0x4B, 0x03, 0x04 },
															Extension = ".zip",
														},
														new FileSignature{
															Hex = new byte[]{ 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A },
															Extension = ".png",
														},
														new FileSignature{
															Hex = new byte[]{ 0xFF, 0xD8, 0xFF },
															Extension = ".jpg",
														},
														new FileSignature{
															Hex = new byte[]{ 0x55, 0x6e, 0x69, 0x74, 0x79, 0x57, 0x65, 0x62 },
															Extension = ".unity3d",
														},
													 };

		/// <summary>
		/// Get file extension of the stream by checking file signature
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static string GetFileExtension(this Stream stream) {

			long previousPosition = -1;
			if(_longestSignature == 0) {
				_longestSignature = _signatures.Max(s => s.Hex.Length);
			}

			try {

				previousPosition = stream.Position;
				byte[] buffer = new byte[_longestSignature];

				int nRead = stream.Read(buffer, 0, _longestSignature);

				if(nRead > 0) {
					for(int i = 0; i < _signatures.Length; i++) {
						if(SequenceEquals(_signatures[i].Hex, buffer)) {
							return _signatures[i].Extension;
						}
					}
				}

			}finally {
				if(previousPosition != -1 && stream.CanSeek) {
					stream.Position = previousPosition;
				}
			}

			return null;
		}

		private static bool SequenceEquals(byte[] primary, byte[] buffer) {
			if(primary.Length == buffer.Length) {
				return primary.SequenceEqual(buffer);
			} else if(primary.Length < buffer.Length) {
				return primary.SequenceEqual(buffer.Take(primary.Length));
			} else {
				return false;
			}
		}
	}
}
