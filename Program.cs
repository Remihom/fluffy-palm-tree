using System;
using System.Collections.Generic;

public interface IVehicle
{
    void Start();
    void Stop();
}

public interface IDriver
{
    void Drive(IVehicle vehicle);
}

public delegate void RideCompletedEventHandler(string message, bool success);

public class RideService
{
    public event RideCompletedEventHandler RideCompleted;

    public void CompleteRide(string message, bool success)
    {
        RideCompleted?.Invoke(message, success);
    }
}

public class Taxi : IVehicle
{
    public void Start()
    {
        Console.WriteLine("Таксі почало поїздку.");
    }

    public void Stop()
    {
        Console.WriteLine("Таксі закінчило поїздку.");
    }
}

public class ComfortTaxi : IVehicle
{
    public void Start()
    {
        Console.WriteLine("Таксі (Комфорт) почало поїздку.");
    }

    public void Stop()
    {
        Console.WriteLine("Таксі (Комфорт) закінчило поїздку.");
    }
}

public class BusinessTaxi : IVehicle
{
    public void Start()
    {
        Console.WriteLine("Таксі (Бізнес) почало поїздку.");
    }

    public void Stop()
    {
        Console.WriteLine("Таксі (Бізнес) закінчило поїздку.");
    }
}

public class ChildSeatTaxi : IVehicle
{
    public void Start()
    {
        Console.WriteLine("Таксі (З дитячим кріслом) почало поїздку.");
    }

    public void Stop()
    {
        Console.WriteLine("Таксі (З дитячим кріслом) закінчило поїздку.");
    }
}

public class TaxiDriver : IDriver
{
    public void Drive(IVehicle vehicle)
    {
        vehicle.Start();
        Console.WriteLine("Таксі їде.");
        vehicle.Stop();
    }
}

public class Passenger<T>
{
    public T RideType { get; set; }
}

public class TripManager
{
    private List<string> completedTrips;

    public TripManager()
    {
        completedTrips = new List<string>();
    }

    public void TrackTrip(string tripInfo)
    {
        completedTrips.Add(tripInfo);
        Console.WriteLine($"Трекова поїздка: {tripInfo}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Taxi standardTaxi = new Taxi();
        ComfortTaxi comfortTaxi = new ComfortTaxi();
        BusinessTaxi businessTaxi = new BusinessTaxi();
        ChildSeatTaxi childSeatTaxi = new ChildSeatTaxi();

        TaxiDriver driver = new TaxiDriver();
        TripManager tripManager = new TripManager();

        driver.Drive(standardTaxi);

        RideService rideService = new RideService();
        rideService.RideCompleted += (message, success) =>
        {
            Console.WriteLine($"Статус поїздки: {message}. Успішно: {success}");
            if (success)
            {
                tripManager.TrackTrip(message);
            }
        };

        rideService.CompleteRide("Стандартна поїздка успішно завершена!", true);
        rideService.CompleteRide("Комфортна поїздка успішно завершена!", true);
        rideService.CompleteRide("Бізнес поїздка успішно завершена!", true);
        rideService.CompleteRide("Поїздка з Дитячим кріслом успішно завершена!", true);

        Passenger<IVehicle> passenger = new Passenger<IVehicle>
        {
            RideType = childSeatTaxi
        };

        Console.ReadLine();
    }
}