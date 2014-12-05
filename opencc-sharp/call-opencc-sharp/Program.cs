using System;
using System.IO;
using OpenCC;

namespace callopenccsharp
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string zhs = "日照香炉生紫烟，遥看瀑布挂前川。飞流直下三千尺，疑是银河落九天";
			string zht = "日照香爐生紫煙，遙看瀑布掛前川。飛流直下三千尺，疑是銀河落九天";

			string[] specialchars = {"硬件","文件夹","鼠标","头发", "恭喜发财"};


			Console.WriteLine(Converter.Simple2Trad(zhs));
			Console.WriteLine(Converter.Trad2Simple(zht));
			using (Converter c = new Converter ()) {
				foreach (string spec in specialchars) {
					Console.WriteLine (c.Convert(spec, "zhs2zhtw_vp.ini"));
				}
			}
		}
	}
}
