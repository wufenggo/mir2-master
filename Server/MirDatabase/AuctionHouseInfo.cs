using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S = ServerPackets;
using Server.MirDatabase;
using Server.MirObjects;
using Server.MirEnvir;
using System.IO;

namespace Server.MirDatabase
{
    /// <summary>
    /// Used only on Auction House so far.. Pete107
    /// </summary>
    public enum CurrencyType : byte
    {
        Gold = 0,
        Credits = 1,
        Pearl = 2
    }
    /// <summary>
    /// To replace the Trust Merchant to allow Bidding of items. Pete107
    /// </summary>
    public class AuctionHouseInfo
    {
        /// <summary>
        /// The Auctions index
        /// </summary>
        public ulong Index;
        /// <summary>
        /// The Item Listed for Auction
        /// </summary>
        public UserItem ListedItem;
        /// <summary>
        /// The Currency Type for the sale of this Item.
        /// </summary>
        public CurrencyType CurrencyType = CurrencyType.Gold;
        /// <summary>
        /// Indicates if the Item has now changed hands
        /// </summary>
        public bool Sold;
        /// <summary>
        /// The desired starting Price of the item.
        /// </summary>
        public uint BasePrice;
        /// <summary>
        /// The seller can choose to allow the buyers to 'buy it now'
        /// </summary>
        public bool CanBuyNow = false;
        /// <summary>
        /// If Buy It Now was chosen by the seller the Buy Now Price will be set by them also.
        /// </summary>
        public uint BuyNowPrice;
        /// <summary>
        /// The Current Bid for the Item on Auction
        /// </summary>
        public uint CurrentBid;
        /// <summary>
        /// The Index of the Current Bidder
        /// </summary>
        public int HighestBidderIndex = -1;
        /// <summary>
        /// The Index of the User Auctioning the Item
        /// </summary>
        public int SellersIndex;
        /// <summary>
        /// The Commission to take off after sale.
        /// </summary>
        public byte Commission = 10;//10% from sales
        /// <summary>
        /// The end time of the Auction.
        /// </summary>
        public long ListEndTime;
        /// <summary>
        /// Base Price default is 10,000
        /// </summary>
        public uint MinPrice = 10000;

        /// <summary>
        /// A skeleton constructor.
        /// </summary>
        public AuctionHouseInfo()
        {

        }

        /// <summary>
        /// Construct a new AuctionHouse List Item. (New Sale)
        /// </summary>
        /// <param name="listedItem">The Item that is up for Auction</param>
        /// <param name="sellerIndex">The Players Index</param>
        /// <param name="basePrice">The base price of the item</param>
        /// <param name="listEndTime">[0 = 1 Hour] - | - [1 = 2 Hour] - | - [2 = 4 Hour] - | - [3 = 6 Hour] - | - [4 = 12 Hour] - | - [5 = 24 Hour] - | - [6 = 48 Hour]</param>
        /// <param name="currencyType">Current Options : Gold, Credit, Pearl. Default : Gold</param>
        /// <param name="canBuyNow">Can Buy Now. Default : False</param>
        /// <param name="buyNowPrice">Buy Now Price. Default : 0</param>
        public AuctionHouseInfo(UserItem listedItem, int sellerIndex, uint basePrice, byte listEndTime, CurrencyType currencyType = CurrencyType.Gold, bool canBuyNow = false, uint buyNowPrice = 0)
        {
            //  Item is invalid
            if (listedItem == null)
                return;
            PlayerObject seller = SMain.Envir.GetPlayer((uint)sellerIndex);
            //  Sellers Index is wrong.
            if (seller == null)
                return;
            //  Base price is to low or too high
            if (basePrice < 10000 ||
                basePrice > 2100000000)
                return;
            //  Invalid end time requested
            if (listEndTime < 0 || listEndTime > 6)
                return;
            if (canBuyNow && buyNowPrice < basePrice)
                return;

            ListedItem = listedItem;
            SellersIndex = sellerIndex;
            BasePrice = basePrice;
            long endTime = 0;
            switch (listEndTime)
            {
                //  1 Hour (Default)
                default:
                case 0:
                    endTime = Settings.Hour * 1;
                    break;
                //  2 Hours
                case 1:
                    endTime = Settings.Hour * 2;
                    break;
                //  4 Hours
                case 2:
                    endTime = Settings.Hour * 4;
                    break;
                //  6 Hours
                case 3:
                    endTime = Settings.Hour * 6;
                    break;
                // 12 Hours
                case 4:
                    endTime = Settings.Hour * 12;
                    break;
                //  24 Hours
                case 5:
                    endTime = Settings.Day * 1;
                    break;
                //  48 Hours
                case 6:
                    endTime = Settings.Day * 2;
                    break;
            }
            ListEndTime = endTime;
            CurrencyType = currencyType;
            CanBuyNow = canBuyNow;
            BuyNowPrice = buyNowPrice;
        }

        public AuctionHouseInfo(BinaryReader reader)
        {
            Index = reader.ReadUInt64();
            bool validItem = reader.ReadBoolean();
            if (validItem)
                ListedItem = new UserItem(reader);
            Sold = reader.ReadBoolean();
            CurrencyType = (CurrencyType)reader.ReadByte();
            BasePrice = reader.ReadUInt32();
            BuyNowPrice = reader.ReadUInt32();
            CanBuyNow = reader.ReadBoolean();
            CurrentBid = reader.ReadUInt32();
            HighestBidderIndex = reader.ReadInt32();
            SellersIndex = reader.ReadInt32();
            Commission = reader.ReadByte();
            ListEndTime = reader.ReadInt64();
            //  Set the time (unpause)
            ListEndTime = ListEndTime + SMain.Envir.Time;
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Index);
            writer.Write(ListedItem != null ? true : false);
            if (ListedItem != null)
                ListedItem.Save(writer);
            writer.Write(Sold);
            writer.Write((byte)CurrencyType);
            writer.Write(BasePrice);
            writer.Write(BuyNowPrice);
            writer.Write(CanBuyNow);
            writer.Write(CurrentBid);
            writer.Write(HighestBidderIndex);
            writer.Write(SellersIndex);
            writer.Write(Commission);
            //  Set the Time (pause)
            ListEndTime = ListEndTime - SMain.Envir.Time;
            writer.Write(ListEndTime);
        }

        public void Process()
        {
            if (SMain.Envir.Time > ListEndTime)
            {
                //  Not sold
                if (CurrentBid == BasePrice ||
                    HighestBidderIndex == -1)
                {

                }
            }
        }
    }

    /// <summary>
    /// A players Auction info
    /// </summary>
    public class PlayersAuctions
    {
        public Envir Envir
        {
            get { return SMain.Envir; }
        }
        public int PlayerIndex;
        public ulong AuctionIndex;
        public bool IsSeller = false;
        public bool GoldRetrived;
        public uint MyBid;
        public bool FirstBid = true;

        public PlayersAuctions(BinaryReader reader)
        {
            PlayerIndex = reader.ReadInt32();
            AuctionIndex = reader.ReadUInt64();
            bool validItem = reader.ReadBoolean();
            IsSeller = reader.ReadBoolean();
            GoldRetrived = reader.ReadBoolean();
            MyBid = reader.ReadUInt32();
            FirstBid = reader.ReadBoolean();
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(PlayerIndex);
            writer.Write(AuctionIndex);
            writer.Write(IsSeller);
            writer.Write(GoldRetrived);
            writer.Write(MyBid);
            writer.Write(FirstBid);
        }

        public PlayersAuctions()
        {

        }
        /// <summary>
        /// Construction and Make first bid
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="aIndex"></param>
        /// <param name="myBid"></param>
        public PlayersAuctions(int pIndex, ulong aIndex, uint myBid)
        {
            //  Set Defaults
            IsSeller = false;
            GoldRetrived = false;
            PlayerIndex = pIndex;
            AuctionIndex = aIndex;
            MyBid = myBid;
            MakeBid(myBid);
        }

        /// <summary>
        /// Construction of Sellers data
        /// </summary>
        /// <param name="pIndex">Player Index</param>
        /// <param name="aIndex">Auction Index</param>
        public PlayersAuctions(int pIndex, ulong aIndex)
        {
            IsSeller = true;
            PlayerIndex = pIndex;
            AuctionIndex = aIndex;
        }

        public bool BuyNow()
        {
            //  Sellers cannot bid on their own items
            if (IsSeller)
            {
                SMain.EnqueueDebugging(string.Format("[BuyNow()]Auction House Error : Cannot bid on own item.", AuctionIndex));
                return false;
            }
            AuctionHouseInfo ListedItem = Envir.GetAuction(AuctionIndex);
            //  Can't find the Auction
            if (ListedItem == null)
            {
                SMain.EnqueueDebugging(string.Format("[BuyNow()]Auction House Error : {0} Index was not found.", AuctionIndex));
                return false;
            }
            if (ListedItem.Sold)
            {
                SMain.EnqueueDebugging(string.Format("[BuyNow()]Auction House Error : {0} Auction has already finished or been purchased.", AuctionIndex));
                return false;
            }
            if (!ListedItem.CanBuyNow)
            {
                SMain.EnqueueDebugging(string.Format("[BuyNow()]Auction House Error : {0} Auction is not Buy now.", AuctionIndex));
                return false;
            }
            if (ListedItem.BuyNowPrice <= 0 || ListedItem.BuyNowPrice > uint.MaxValue)
            {
                SMain.EnqueueDebugging(string.Format("[BuyNow()]Auction House Error : {0} Auction Buy now price out of range.", AuctionIndex));
                return false;
            }
            //  The Auctions ended
            if (SMain.Envir.Time > ListedItem.ListEndTime)
            {
                SMain.EnqueueDebugging(string.Format("[BuyNow()]Auction House Error : {0} Auction has already finished.", AuctionIndex));
                return false;
            }
            //  The Item up for Auction doesn't exist
            if (ListedItem.ListedItem == null)
            {
                SMain.EnqueueDebugging(string.Format("[BuyNow()]Auction House Error : {0} Item not found.", AuctionIndex));
                return false;
            }
            PlayerObject bidder = Envir.GetPlayer((uint)PlayerIndex);
            //  Can't find the player
            if (bidder == null)
            {
                SMain.EnqueueDebugging(string.Format("[BuyNow()]Auction House Error : Buyer not found {0}.", PlayerIndex));
                return false;
            }
            //  Can't find the players account
            if (bidder.Account == null)
            {
                SMain.EnqueueDebugging(string.Format("[BuyNow()]Auction House Error : Buyer Account not found {0}.", PlayerIndex));
                return false;
            }
            PlayerObject seller = Envir.GetPlayer((uint)ListedItem.SellersIndex);
            if (seller == null)
            {
                SMain.EnqueueDebugging(string.Format("[BuyNow()]Auction House Error : Sellers Account not found {0}.", PlayerIndex));
                return false;
            }
            uint gold = 0;
            string message = "";
            List<UserItem> items = new List<UserItem>();
            switch(ListedItem.CurrencyType)
            {
                default:
                case CurrencyType.Gold:
                    {
                        if (bidder.Account.Gold < ListedItem.BuyNowPrice)
                        {
                            SMain.EnqueueDebugging(string.Format("[BuyNow()]Auction House Error : {0} Player does not have enough gold.", AuctionIndex));
                            return false;
                        }
                        gold = ListedItem.BuyNowPrice;

                        if (gold >= bidder.Account.Gold)
                            gold = bidder.Account.Gold;

                        bidder.Account.Gold -= gold;
                        bidder.Enqueue(new S.LoseGold { Gold = gold });
                        items.Add(ListedItem.ListedItem);
                        message = string.Format("You purchased {0} from {1} for {2:#,###,###,###}.", ListedItem.ListedItem.FriendlyName, seller.Name, ListedItem.BuyNowPrice);
                        MailInfo mail = new MailInfo(PlayerIndex, false)
                        {
                            Gold = 0,
                            Message= message,
                            Items = items,
                            Sender = "AuctionHouse",
                            MailID = ++Envir.NextMailID
                        };
                        mail.Send();
                        mail = null;
                        items.Add(ListedItem.ListedItem);
                        message = string.Format("{0} purchased {1} from you for {2:#,###,###,###}.", bidder.Name, ListedItem.ListedItem.FriendlyName, ListedItem.BuyNowPrice);
                        mail = new MailInfo(PlayerIndex, false)
                        {
                            Gold = ListedItem.BuyNowPrice - (uint)Functions.GetPercentage(ListedItem.Commission, (int)ListedItem.BuyNowPrice),
                            Message = message,
                            Sender = "AuctionHouse",
                            MailID = ++Envir.NextMailID
                        };
                        mail.Send();
                        mail = null;
                        //  End it.
                        ListedItem.ListedItem = null;
                        ListedItem.CurrentBid = gold;
                        ListedItem.HighestBidderIndex = PlayerIndex;
                        ListedItem.ListEndTime = Envir.Time;
                        ListedItem.Sold = true;
                        GoldRetrived = true;
                        /*
                        gold = MyBid;
                        //  Message contents.
                        message = string.Format("{0:#,###,###,###} Gold has been returned to you.", MyBid);
                        //  Create the mail
                        MailInfo mail = new MailInfo(PlayerIndex, true)
                        {
                            MailID = ++Envir.NextMailID,
                            Sender = "AuctionHouse",
                            Message = message,
                            Gold = gold,
                        };
                        */
                    }
                    break;
                case CurrencyType.Credits:
                    {
                        if (bidder.Account.Credit < ListedItem.BuyNowPrice)
                        {
                            SMain.EnqueueDebugging(string.Format("[BuyNow()]Auction House Error : {0} Player does not have enough credits.", AuctionIndex));
                            return false;
                        }
                    }
                    break;
                case CurrencyType.Pearl:
                    {
                        if (bidder.Info.PearlCount < ListedItem.BuyNowPrice)
                        {
                            SMain.EnqueueDebugging(string.Format("[BuyNow()]Auction House Error : {0} Player does not have enough pearls.", AuctionIndex));
                            return false;
                        }
                        
                        
                       
                    }
                    break;
            }
            return true;
        }
        

        public bool MakeBid(uint myBid)
        {
            //  Range Check
            if (myBid <= 0 || myBid > uint.MaxValue)
            {
                SMain.EnqueueDebugging(string.Format("[MakeBid(uint myBid)]Auction House Error : Bid amount {0} out of range.", myBid));
                return false;
            }
            //  Sellers cannot bid on their own items
            if (IsSeller)
            {
                SMain.EnqueueDebugging(string.Format("[MakeBid(uint myBid)]Auction House Error : Cannot bid on own item.", AuctionIndex));
                return false;
            }
            AuctionHouseInfo ListedItem = Envir.GetAuction(AuctionIndex);
            //  Can't find the Auction
            if (ListedItem == null)
            {
                SMain.EnqueueDebugging(string.Format("[MakeBid(uint myBid)]Auction House Error : {0} Index was not found.", AuctionIndex));
                return false;
            }
            //  The Auctions ended
            if (SMain.Envir.Time > ListedItem.ListEndTime)
            {
                SMain.EnqueueDebugging(string.Format("[MakeBid(uint myBid)]Auction House Error : {0} Auction has already finished.", AuctionIndex));
                return false;
            }
            //  The Item up for Auction doesn't exist
            if (ListedItem.ListedItem == null)
            {
                SMain.EnqueueDebugging(string.Format("[MakeBid(uint myBid)]Auction House Error : {0} Item not found.", AuctionIndex));
                return false;
            }
            //  Bidder was the previous bidder
            if (ListedItem.HighestBidderIndex == PlayerIndex)
            {
                SMain.EnqueueDebugging(string.Format("[MakeBid(uint myBid)]Auction House Error : Buyer Already made bid."));
                return false;
            }
            PlayerObject bidder = Envir.GetPlayer((uint)PlayerIndex);
            //  Can't find the player
            if (bidder == null)
            {
                SMain.EnqueueDebugging(string.Format("[MakeBid(uint myBid)]Auction House Error : Buyer not found {0}.", PlayerIndex));
                return false;
            }
            //  Can't find the players account
            if (bidder.Account == null)
            {
                SMain.EnqueueDebugging(string.Format("[MakeBid(uint myBid)]Auction House Error : Buyer Account not found {0}.", PlayerIndex));
                return false;
            }
            uint gold = 0;
            string message = "";
            //  Switch between the currency
            switch (ListedItem.CurrencyType)
            {
                default:
                case CurrencyType.Gold:
                    {
                        //  Bid was more than the Account has
                        if (bidder.Account.Gold < myBid)
                        {
                            SMain.EnqueueDebugging(string.Format("[MakeBid(uint myBid)]Auction House Error : Buyer Not enough gold {0}.", myBid - bidder.Account.Gold));
                            return false;
                        }
                        //  Bid was too low
                        if (myBid <= ListedItem.CurrentBid + 1000)
                        {
                            SMain.EnqueueDebugging(string.Format("[MakeBid(uint myBid)]Auction House Error : Buyer Bid is too low {0}.", myBid - ListedItem.CurrentBid));
                            return false;
                        }
                        if (!FirstBid && !GoldRetrived)
                        {
                             gold = MyBid;
                            //  Message contents.
                            message = string.Format("{0:#,###,###,###} Gold has been returned to you.", MyBid);
                            //  Create the mail
                            MailInfo mail = new MailInfo(PlayerIndex, true)
                            {
                                MailID = ++Envir.NextMailID,
                                Sender = "AuctionHouse",
                                Message = message,
                                Gold = gold,
                            };
                            GoldRetrived = true;
                        }

                        MyBid = myBid;

                        if (myBid >= bidder.Account.Gold)
                            myBid = bidder.Account.Gold;

                        bidder.Account.Gold -= myBid;
                        bidder.Enqueue(new S.LoseGold { Gold = myBid });

                        ListedItem.CurrentBid = myBid;
                        ListedItem.HighestBidderIndex = PlayerIndex;

                        GoldRetrived = false;

                        bidder.ReceiveChat(string.Format("You bid {0:#,###,###,###} for {1}{2} has been placed.", MyBid, ListedItem.ListedItem.FriendlyName, ListedItem.ListedItem.Count >= 1 ? string.Format("[{0}]", ListedItem.ListedItem.Count) : ""), ChatType.System);
                        if (FirstBid)
                            FirstBid = false;
                        return true;
                    }
                case CurrencyType.Credits:
                    {
                        if (bidder.Account.Credit < myBid)
                        {
                            SMain.EnqueueDebugging(string.Format("[MakeBid(uint myBid)]Auction House Error : Buyer Not enough credit {0}.", myBid - bidder.Account.Credit));
                            return false;
                        }
                        //  Only allow Increments of 1k
                        if (myBid <= ListedItem.CurrentBid + 5)
                        {
                            SMain.EnqueueDebugging(string.Format("[MakeBid(uint myBid)]Auction House Error : Buyer Bid is too low {0}.", myBid - ListedItem.CurrentBid));
                            return false;
                        }
                        if (!FirstBid && !GoldRetrived)
                        {
                            gold = MyBid;
                            //  Message contents.
                            message = string.Format("{0:#,###,###,###} Gold has been returned to you.", MyBid);
                            //  Create the mail
                            MailInfo mail = new MailInfo(PlayerIndex, true)
                            {
                                MailID = ++Envir.NextMailID,
                                Sender = "AuctionHouse",
                                Message = message,
                                Gold = gold,
                            };
                            GoldRetrived = true;
                        }

                        MyBid = myBid;

                        if (myBid >= bidder.Account.Credit)
                            myBid = bidder.Account.Credit;

                        bidder.Account.Credit -= myBid;
                        bidder.Enqueue(new S.LoseCredit { Credit = myBid });

                        ListedItem.CurrentBid = myBid;
                        ListedItem.HighestBidderIndex = PlayerIndex;
                        GoldRetrived = false;

                        bidder.ReceiveChat(string.Format("You bid {0:#,###,###,###} for {1}{2} has been placed.", MyBid, ListedItem.ListedItem.FriendlyName, ListedItem.ListedItem.Count >= 1 ? string.Format("[{0}]", ListedItem.ListedItem.Count) : ""), ChatType.System);
                        if (FirstBid)
                            FirstBid = false;
                        return true;
                    }
                case CurrencyType.Pearl:
                    {
                        if (bidder.Info.PearlCount < myBid)
                        {
                            SMain.EnqueueDebugging(string.Format("[MakeBid(uint myBid)]Auction House Error : Buyer Not enough pearl {0}.", myBid - bidder.Info.PearlCount));
                            return false;
                        }
                        //  Only allow Increments of 1k
                        if (myBid <= ListedItem.CurrentBid + 1)
                        {
                            SMain.EnqueueDebugging(string.Format("[MakeBid(uint myBid)]Auction House Error : Buyer Bid is too low {0}.", myBid - ListedItem.CurrentBid));
                            return false;
                        }
                        if (!FirstBid && !GoldRetrived)
                        {
                            gold = MyBid;
                            //  Message contents.
                            message = string.Format("{0:#,###,###,###} Gold has been returned to you.", MyBid);
                            //  Create the mail
                            MailInfo mail = new MailInfo(PlayerIndex, true)
                            {
                                MailID = ++Envir.NextMailID,
                                Sender = "AuctionHouse",
                                Message = message,
                                Gold = gold,
                            };
                            GoldRetrived = true;
                        }

                        MyBid = myBid;

                        if (myBid >= bidder.Info.PearlCount)
                            myBid = (uint)bidder.Info.PearlCount;

                        bidder.Info.PearlCount -= (int)myBid;

                        ListedItem.CurrentBid = myBid;
                        ListedItem.HighestBidderIndex = PlayerIndex;

                        GoldRetrived = false;

                        bidder.ReceiveChat(string.Format("You bid {0:#,###,###,###} Pearl{2} for {1}{3} has been placed.", MyBid, ListedItem.ListedItem.FriendlyName, MyBid > 1 ? "s" : "", ListedItem.ListedItem.Count >= 1 ? string.Format("[{0}]", ListedItem.ListedItem.Count) : ""), ChatType.System);
                        if (FirstBid)
                            FirstBid = false;
                        return true;
                    }

            }
            
        }
    }
}
