namespace RestaurantManager.Utilities
{
    public enum OrderStatus
    {
        Waiting, // Guest waiting for  Waiter
        Accepted, // Order taken from Guest by Waiter
        HandedOverToCooking, // Order handed over to Chef by Waiter
        Cooked, // Order cooked by Chef
        HandedOverToWaiter, // Chef handed over the order to Waiter
        Released, // Order released for consumption to Guest
    }
}