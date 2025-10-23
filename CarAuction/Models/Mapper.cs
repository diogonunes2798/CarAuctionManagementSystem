using CarAuction.Models.Auctions;
using CarAuction.Models.Requests;
using CarAuction.Models.Vehicles;
using System;
using System.Diagnostics;

namespace CarAuction.Models
{
    public class Mapper
    {
        public static Vehicle RequestToEntity(CreateVehicle createVehicle)
        {
            switch (createVehicle.Type)
            {
                case VehicleType.Hatchback:
                    return new Hatchback()
                    {
                        Manufacturer = createVehicle.Manufacturer,
                        Model = createVehicle.Model,
                        Year = createVehicle.Year,
                        StartingBid = createVehicle.StartingBid,
                        NumberOfDoors = Convert.ToInt32(createVehicle.CustomProperty)
                    };

                case VehicleType.Sudan:
                    return new Sudan
                    {
                        Manufacturer = createVehicle.Manufacturer,
                        Model = createVehicle.Model,
                        Year = createVehicle.Year,
                        StartingBid = createVehicle.StartingBid,
                        NumberOfDoors = Convert.ToInt32(createVehicle.CustomProperty)
                    };

                case VehicleType.SUV:
                    return new SUV
                    {
                        Manufacturer = createVehicle.Manufacturer,
                        Model = createVehicle.Model,
                        Year = createVehicle.Year,
                        StartingBid = createVehicle.StartingBid,
                        NumberOfSeats = Convert.ToInt32(createVehicle.CustomProperty)
                    };

                case VehicleType.Truck:
                    return new Truck
                    {
                        Manufacturer = createVehicle.Manufacturer,
                        Model = createVehicle.Model,
                        Year = createVehicle.Year,
                        StartingBid = createVehicle.StartingBid,
                        LoadCapacity = createVehicle.CustomProperty
                    };

                default:
                    throw new InvalidOperationException($"Vehicle type is invalid: {createVehicle.Type}");
            }
        }

        public static Auction VehicleToAuction(Vehicle vehicle)
        {
            return new Auction
            {
                VehicleId = vehicle.Id,
                Bid = vehicle.StartingBid,
                IsActive = true,
            };
        }
    }
}
