using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;


namespace WindowsPhoneUWP.UpgradeHelpers
{
    class IconicTileData : TileHelper
    {      

        private string stile =
    "<tile >" +
      "<visual version=\"3\">" +
           "<binding template = \"TileSquare70x70Logo\" > " +
               "<image id = \"1\" src = \"\" alt = \"\" />" +
           "</binding >" +
           "<binding template = \"TileSquare71x71IconWithBadge\" > " +
               "<image id = \"1\" src = \"\" alt = \"\" />" +
           "</binding >" +
           "<binding template = \"TileSquare150x150IconWithBadge\">" +
            "<image id = \"1\" src = \"\" alt = \"\" />" +
           "</binding>" +
           "<binding template=\"TileWide310x150IconWithBadgeAndText\">" +
              "<image id = \"1\" src = \"\" alt = \"\" />" +
              "<text id = \"1\" > </text >" +
              "<text id = \"2\" > </text >" +
              "<text id = \"3\" > </text >" +
            "</binding > " +
      "</visual> " +
  "</tile>";


        public XmlDocument tile;
        
       
        public IconicTileData()
        {
            IconImage = new Uri("ms-appx:///Assets/Square150x150Logo.scale-200.png");
            Count = 0;
            SmallIconImage = new Uri("ms-appx:///Assets/StoreLogo.png");
            WideContent1 = "";
            WideContent2 = "";
            WideContent3 = "";

        }               
        public Uri IconImage { get; set; }       
        public Uri SmallIconImage { get; set; }        
        public string WideContent1{ get; set;}             
        public string WideContent2 { get; set; }        
        public string WideContent3 { get; set; }        

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
            ((Windows.Data.Xml.Dom.XmlElement)nodeImage[0]).SetAttribute("src", SmallIconImage.OriginalString);
            ((Windows.Data.Xml.Dom.XmlElement)nodeImage[1]).SetAttribute("src", SmallIconImage.OriginalString);
            ((Windows.Data.Xml.Dom.XmlElement)nodeImage[2]).SetAttribute("src", IconImage.OriginalString);
            ((Windows.Data.Xml.Dom.XmlElement)nodeImage[3]).SetAttribute("src", IconImage.OriginalString);

            var nodeText = tile.GetElementsByTagName("text");
            nodeText[0].InnerText = WideContent1.ToString();
            nodeText[1].InnerText = WideContent2.ToString();
            nodeText[2].InnerText = WideContent3.ToString();
                                                     
            //if (!wide)
            //{
            //    var nodeWide = tile.GetElementsByTagName("binding");
            //    var nodevisual = tile.FirstChild.FirstChild;
            //    nodevisual.RemoveChild(nodeWide[3]);
            //    //(tile.FirstChild).RemoveChild(nodeWide[3]);
            //}
            this.tile.GetXml();
            var tileNotc = new TileNotification(this.tile);
            //tileNotc.Tag = NavigationUri != null ? NavigationUri.OriginalString.ToString() :tileNotc.Tag;
            return tileNotc;
        }

    }
}
