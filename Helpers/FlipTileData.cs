using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;


namespace WindowsPhoneUWP.UpgradeHelpers
{
    class FlipTileData : StandardTileData
    {      
             
//        public XmlDocument tile;
        private string stile;
       
        public FlipTileData()
        {
            BackgroundImage = new Uri("ms-appx:///Assets/Square150x150Logo.scale-200.png");
            Count = 0;
            stile =
                @"<tile>
	<visual version=""4"">
        <binding template = ""TileSquare70x70Logo"" >
             <image id = ""1"" src = """" />
         </binding >
         <binding template = ""TileSquare71x71Image"" >
             <image id = ""1"" src = """" />
          </binding >
          <binding template = ""TileSquare150x150PeekImageAndText02"" fallback = ""TileSquarePeekImageAndText02"" >
              <image id = ""1"" src = """" />
              <text id = ""1"" ></text >
              <text id = ""2"" ></text >
           </binding >
           <binding template = ""TileWide310x150PeekImage01"" fallback = ""TileWidePeekImage01"" >
              <image id = ""1"" src = """" />
              <text id = ""1"" ></text >
              <text id = ""2"" ></text >
           </binding >
          </visual >
      </tile>";

            SmallBackgroundImage = new Uri("ms-appx:///Assets/StoreLogo.png");
            WideBackContent = "";
            WideBackgroundImage = new Uri("ms-appx:///Assets/Wide310x150Logo.scale-200.png");


        }
        public Uri SmallBackgroundImage { get; set; }
        public Uri WideBackBackgroundImage { get; set; }
        public Uri WideBackgroundImage { get; set; }
        public string WideBackContent { get; set;}        
        

        public override BadgeNotification GetBadge()
        {
            XmlDocument xmldoc = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
            ((XmlElement)xmldoc.SelectSingleNode("badge")).SetAttribute("value", this.Count.ToString());
            //string xx = xmldoc.GetXml();
            return new BadgeNotification(xmldoc);
        }


        public override TileNotification GetNotificacion()
        {
            tile = new XmlDocument();
            tile.LoadXml(stile);

            var nodeImage = tile.GetElementsByTagName("image");
            ((Windows.Data.Xml.Dom.XmlElement)nodeImage[0]).SetAttribute("src", SmallBackgroundImage.OriginalString);
            ((Windows.Data.Xml.Dom.XmlElement)nodeImage[1]).SetAttribute("src", SmallBackgroundImage.OriginalString);
            ((Windows.Data.Xml.Dom.XmlElement)nodeImage[2]).SetAttribute("src", BackgroundImage.OriginalString);
            ((Windows.Data.Xml.Dom.XmlElement)nodeImage[3]).SetAttribute("src", WideBackgroundImage.OriginalString);

            var nodeText = tile.GetElementsByTagName("text");
            nodeText[0].InnerText = Title.ToString()?? "Title";
            nodeText[1].InnerText = BackContent.ToString()?? "";
            nodeText[2].InnerText = BackTitle.ToString()??"";
            nodeText[3].InnerText = WideBackContent.ToString()??"";
                                                     
            
            //this.tile.GetXml();
            var tileNotc = new TileNotification(this.tile);
            //tileNotc.Tag = NavigationUri != null ? NavigationUri.OriginalString.ToString() :tileNotc.Tag;
            return tileNotc;
        }

    }
}
