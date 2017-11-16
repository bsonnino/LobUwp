using System;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;

namespace LobUwp.Services
{
    internal partial class LiveTileService
    {
        private const string TileTitle = "LoB App with UWP";

        public void UpdateCustomerCount(int custCount)
        {
            string tileContent = $@"There are {(custCount > 0 ? custCount.ToString() : "no")} customers in the database";
            UpdateTileData(tileContent, "Customer");
        }

        public void UpdateOrderCount(int orderCount)
        {
            string tileContent = $@"There are {(orderCount > 0 ? orderCount.ToString() : "no")} orders in the database";
            UpdateTileData(tileContent,"Order");
        }

        private void UpdateTileData(string tileBody, string tileTag)
        {
            TileContent tileContent = new TileContent()
            {
                Visual = new TileVisual()
                {
                    TileMedium = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = TileTitle,
                                    HintWrap = true
                                },
                                new AdaptiveText()
                                {
                                    Text = tileBody,
                                    HintStyle = AdaptiveTextStyle.CaptionSubtle,
                                    HintWrap = true
                                }
                            }
                        }
                    },

                    TileWide = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = $"{TileTitle}",
                                    HintStyle = AdaptiveTextStyle.Caption
                                },
                                new AdaptiveText()
                                {
                                    Text = tileBody,
                                    HintStyle = AdaptiveTextStyle.CaptionSubtle,
                                    HintWrap = true
                                }
                            }
                        }
                    }
                }
            };
            var notification = new TileNotification(tileContent.GetXml())
            {
                Tag = tileTag
            };

            UpdateTile(notification);
        }
    }
}
