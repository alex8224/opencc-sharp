using System;
using System.Text;
using System.Runtime.InteropServices;

namespace OpenCC
{
	public class Converter:IDisposable
	{
		[DllImport("opencc")]
		public static extern IntPtr opencc_open(string configfile);

		[DllImport("opencc")]
		public static extern IntPtr opencc_convert_utf8 (IntPtr opencc, string buf, UInt32 buflen);

		[DllImport("opencc")]
		public static extern void opencc_convert_utf8_free (IntPtr buffptr);

		[DllImport("opencc")]
		public static extern int opencc_close (IntPtr opencc);

		private IntPtr opencc_ptr = IntPtr.Zero;

		public Converter() {
		}

		//高级转换方法，可以通过指定相应的转换配置文件进行转换
		public string Convert(string tradstr, string configfile) {
			if(string.IsNullOrEmpty(configfile)) configfile = "zht2zhs.ini";
			opencc_ptr = opencc_open (configfile);
			byte[] u8array = Encoding.UTF8.GetBytes (tradstr);
			string u8str = Encoding.UTF8.GetString (u8array);
			IntPtr result_ptr =	opencc_convert_utf8 (opencc_ptr, u8str, (UInt32)u8array.Length);
			string chsstr = Marshal.PtrToStringAnsi (result_ptr);
			opencc_convert_utf8_free (result_ptr);
			return chsstr;
		}

		//简体转繁体
		public string Simple2Trad(string zhsstr) {
			return Convert (zhsstr, "zhs2zhtw_vp.ini");
		}

		//繁体转简体
		public string Trad2Simple(string zhtstr) {
			return Convert (zhtstr, "zht2zhs.ini");
		}
			
		public void Dispose() {
			if(opencc_ptr == IntPtr.Zero) {
				opencc_close (opencc_ptr);
			}
		}
	}
}
