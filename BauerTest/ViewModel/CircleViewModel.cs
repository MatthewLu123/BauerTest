using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml;



namespace BauerTest
{

		public class CircleViewModel
	{
		private const string  RANDOM_COLOR_URL = "http://www.colourlovers.com/api/colors/random";

		public CircleViewModel ()
		{
		}


		static	public async Task<string> downloadXML(string url)
		{

			var httpClient = new HttpClient();

			Task<string> downloadTask = httpClient.GetStringAsync (url);

			string contents = await downloadTask;


			return contents;

		}

		static	public async Task<CircleViewColorModel> parseXML ()
		{
			CircleViewColorModel color = null;

			Task<string> xml =   downloadXML(RANDOM_COLOR_URL);

			var xmlString = await xml;
			var xmlDoc = new XmlDocument();

			xmlDoc.LoadXml (xmlString);

			var colorXML = xmlDoc.GetElementsByTagName ("color");
		

			foreach (XmlNode node in colorXML) {
				try
				{
					string ID = node["id"].InnerText.Trim();
					string Title = node["title"].InnerText.Trim();

					int red = Convert.ToInt16( node["rgb"]["red"].InnerText.Trim());
					int green =Convert.ToInt16( node["rgb"]["green"].InnerText.Trim());
					int blue = Convert.ToInt16( node["rgb"]["blue"].InnerText.Trim());

					color = new CircleViewColorModel(ID,Title,red,green,blue);
				}
				catch(FormatException ex) {
					Console.WriteLine (@"XML format is not right. " +ex.Message );

				}
				catch(OverflowException ex) {
				
					Console.WriteLine (@"The number can not fit Int16 " + ex.Message);
				}
				catch(NullReferenceException ex) {

					Console.WriteLine (@"The XML element contains nil object " +ex.Message);
				}
			}

			return color;
		}

	}
}

