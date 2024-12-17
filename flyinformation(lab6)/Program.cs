using System;
using System.Collections.Generic;

namespace flyinformation_lab6_
{
    class Program
    {
        static void Main(string[] args)
        {
            var flightSystem = new FlightInformationSystem();
            var queryHandler = new FlightQueryHandler(flightSystem);

            try
            {
                flightSystem.LoadFlightsFromJson("flights.json");
                Console.WriteLine("База даних з рейсами успішно завантажена.\n");

                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("Оберіть дію (1-5):");
                    Console.WriteLine("1 - Рейси за авіакомпанією");
                    Console.WriteLine("2 - Затримані рейси");
                    Console.WriteLine("3 - Рейси на певну дату");
                    Console.WriteLine("4 - Рейси в певному часовому проміжку до певного пункту призначення");
                    Console.WriteLine("5 - Недавні прибуття");
                    Console.WriteLine("6 - Вийти");
                    string option = Console.ReadLine();
                    switch (option)
                    {
                        case "1":
                            Console.WriteLine("Введіть назву авіакомпанії:");
                            string airline = Console.ReadLine();
                            List<Flight> airlineFlights = queryHandler.GetFlightsByAirline(airline);
                            if (airlineFlights.Count > 0)
                            {
                                Console.WriteLine($"Рейси авіакомпанії '{airline}':");
                                foreach (var flight in airlineFlights)
                                    Console.WriteLine($"{flight.FlightNumber} -> {flight.Destination}, Виліт: {flight.DepartureTime}");
                            }
                            else
                            {
                                Console.WriteLine("Не знайдено рейсів для цієї авіакомпанії.");
                            }
                            break;

                        case "2":
                            List<Flight> delayedFlights = queryHandler.GetDelayedFlights();
                            if (delayedFlights.Count > 0)
                            {
                                Console.WriteLine("Затримані рейси:");
                                foreach (var flight in delayedFlights)
                                    Console.WriteLine($"Рейс {flight.FlightNumber}, Затримка: {flight.Duration}");
                            }
                            else
                            {
                                Console.WriteLine("Немає затриманих рейсів.");
                            }
                            break;

                        case "3":
                            Console.WriteLine("Введіть дату вильоту (yyyy-MM-dd):");
                            DateTime date = DateTime.Parse(Console.ReadLine());
                            List<Flight> flightsByDate = queryHandler.GetFlightsByDepartureDate(date);
                            if (flightsByDate.Count > 0)
                            {
                                Console.WriteLine($"Рейси на {date.ToShortDateString()}:");
                                foreach (var flight in flightsByDate)
                                    Console.WriteLine($"{flight.FlightNumber} -> {flight.Destination}, Виліт: {flight.DepartureTime}");
                            }
                            else
                            {
                                Console.WriteLine("Не знайдено рейсів на цю дату.");
                            }
                            break;

                        case "4":
                            Console.WriteLine("Введіть початкову дату та час (yyyy-MM-dd HH:mm):");
                            DateTime startTime = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Введіть кінцеву дату та час (yyyy-MM-dd HH:mm):");
                            DateTime endTime = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Введіть пункт призначення:");
                            string destination = Console.ReadLine();
                            List<Flight> flightsByTimeAndDestination = queryHandler.GetFlightsByTimeRangeAndDestination(startTime, endTime, destination);
                            if (flightsByTimeAndDestination.Count > 0)
                            {
                                Console.WriteLine($"Рейси до {destination} в заданому часовому проміжку:");
                                foreach (var flight in flightsByTimeAndDestination)
                                    Console.WriteLine($"{flight.FlightNumber} -> {flight.DepartureTime}, Виліт: {flight.DepartureTime}");
                            }
                            else
                            {
                                Console.WriteLine("Не знайдено рейсів в заданому часовому проміжку.");
                            }
                            break;

                        case "5":
                            Console.WriteLine("Введіть початкову дату та час для перевірки прибуттів (yyyy-MM-dd HH:mm):");
                            DateTime arrivalStartTime = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Введіть кінцеву дату та час для перевірки прибуттів (yyyy-MM-dd HH:mm):");
                            DateTime arrivalEndTime = DateTime.Parse(Console.ReadLine());
                            List<Flight> recentArrivals = queryHandler.GetFlightsArrivedInTimeRange(arrivalStartTime, arrivalEndTime);
                            if (recentArrivals.Count > 0)
                            {
                                Console.WriteLine("Недавні прибуття:");
                                foreach (var flight in recentArrivals)
                                    Console.WriteLine($"Рейс {flight.FlightNumber} -> {flight.ArrivalTime}");
                            }
                            else
                            {
                                Console.WriteLine("Не знайдено недавніх прибуттів.");
                            }
                            break;

                        case "6":
                            exit = true;
                            Console.WriteLine("До побачення!");
                            break;

                        default:
                            Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}