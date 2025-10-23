using CarAuction.Models;
using CarAuction.Models.Auctions;
using CarAuction.Models.Requests;
using CarAuction.Models.Vehicles;
using CarAuction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarAuctionXUnit
{
    public class AuctionTests
    {
        private readonly IAuctionService _auctionService;
        public AuctionTests()
        {
            _auctionService = new AuctionService();
        }

        #region Create Auction
        [Fact]
        public void CreateAuction_ShouldAddAuction_WhenNoActiveAuctionExists()
        {
            // Arrange
            var auction = new Auction
            {
                VehicleId = Guid.NewGuid(),
                Bid = 10
            };

            // Act
            _auctionService.CreateAuction(auction);

            // Assert
            Assert.Contains(auction, _auctionService.GetAllAuctions());
        }

        [Fact]
        public void CreateAuction_ShouldThrowException_WhenActiveAuctionExistsForSameVehicle()
        {
            var vehicleId = Guid.NewGuid();

            var auction1 = new Auction
            {
                VehicleId = vehicleId,
                IsActive = true,
                Bid = 1000
            };

            var auction2 = new Auction
            {
                VehicleId = vehicleId,
                IsActive = true,
                Bid = 1500
            };

            _auctionService.CreateAuction(auction1);

            var ex = Assert.Throws<InvalidOperationException>(() => _auctionService.CreateAuction(auction2));
            Assert.Equal("An active auction already exists for this vehicle.", ex.Message);
        }
        #endregion

        #region Place Bid
        [Fact]
        public void PlaceBid_ShouldUpdateBid_WhenValid()
        {
            var vehicleId = Guid.NewGuid();

            var auction = new Auction
            {
                VehicleId = vehicleId,
                IsActive = true,
                Bid = 1000
            };

            _auctionService.CreateAuction(auction);

            var placeBid = new PlaceBid
            {
                VehicleId = vehicleId,
                BidAmount = 1500
            };

            _auctionService.PlaceBid(placeBid);

            var updated = _auctionService.GetAuctionByVehicleId(vehicleId);

            Assert.Equal(1500, updated.Bid);
        }

        [Fact]
        public void PlaceBid_ShouldThrow_WhenAuctionNotFound()
        {
            var placeBid = new PlaceBid
            {
                VehicleId = Guid.NewGuid(),
                BidAmount = 1200
            };

            var ex = Assert.Throws<InvalidOperationException>(() => _auctionService.PlaceBid(placeBid));
            Assert.Equal("Auction not found.", ex.Message);
        }

        [Fact]
        public void PlaceBid_ShouldThrow_WhenAuctionIsInactive()
        {
            var vehicleId = Guid.NewGuid();

            var auction = new Auction
            {
                VehicleId = vehicleId,
                IsActive = false,
                Bid = 1000
            };

            _auctionService.CreateAuction(auction);

            var placeBid = new PlaceBid
            {
                VehicleId = vehicleId,
                BidAmount = 1500
            };

            var ex = Assert.Throws<InvalidOperationException>(() => _auctionService.PlaceBid(placeBid));
            Assert.Equal("Cannot place a bid on an inactive auction.", ex.Message);
        }

        [Fact]
        public void PlaceBid_ShouldThrow_WhenBidAmountIsNotGreaterThanCurrent()
        {
            var vehicleId = Guid.NewGuid();

            var auction = new Auction
            {
                VehicleId = vehicleId,
                IsActive = true,
                Bid = 1500
            };

            _auctionService.CreateAuction(auction);

            var placeBid = new PlaceBid
            {
                VehicleId = vehicleId,
                BidAmount = 1400 
            };

            var ex = Assert.Throws<InvalidOperationException>(() => _auctionService.PlaceBid(placeBid));
            Assert.Equal("Bid must be greater than current bid.", ex.Message);
        }

        [Fact]
        public void PlaceBid_ShouldThrow_WhenBidAmountIsZeroOrNegative()
        {
            var vehicleId = Guid.NewGuid();

            var auction = new Auction
            {
                VehicleId = vehicleId,
                IsActive = true,
                Bid = 1000
            };

            _auctionService.CreateAuction(auction);

            var placeBid = new PlaceBid
            {
                VehicleId = vehicleId,
                BidAmount = 0
            };

            var ex = Assert.Throws<InvalidOperationException>(() => _auctionService.PlaceBid(placeBid));
            Assert.Equal("Bid amount must be greater than zero.", ex.Message);
        }
        #endregion

        #region Close Auction
        [Fact]
        public void CloseAuctionById_ShouldCloseActiveAuction()
        {
            var auction = new Auction
            {
                VehicleId = Guid.NewGuid(),
                IsActive = true,
                Bid = 1000
            };

            _auctionService.CreateAuction(auction);

            var closed = _auctionService.CloseAuctionById(auction.Id);

            Assert.NotNull(closed);
            Assert.False(closed.IsActive);
        }

        [Fact]
        public void CloseAuctionById_ShouldReturnNull_WhenAuctionIsAlreadyClosed()
        {
            var auction = new Auction
            {
                VehicleId = Guid.NewGuid(),
                IsActive = false,
                Bid = 1000
            };

            _auctionService.CreateAuction(auction);

            var result = _auctionService.CloseAuctionById(auction.Id);

            Assert.Null(result);
        }

        [Fact]
        public void CloseAuctionById_ShouldReturnNull_WhenAuctionNotFound()
        {
            var result = _auctionService.CloseAuctionById(Guid.NewGuid());
            Assert.Null(result);
        }
        #endregion
    }
}
