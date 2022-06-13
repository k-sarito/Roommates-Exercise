using System;
using System.Collections.Generic;
using Roommates.Repositories;
using Roommates.Models;
using System.Linq;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true;TrustServerCertificate=true;";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository rmRepo = new RoommateRepository(CONNECTION_STRING);
            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Update a room"):
                        List<Room> roomOptions = roomRepo.GetAll();
                        foreach (Room r in roomOptions)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.Write("Which room would you like to update? ");
                        int selectedRoomId = int.Parse(Console.ReadLine());
                        Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

                        Console.Write("New Name: ");
                        selectedRoom.Name = Console.ReadLine();

                        Console.Write("New Max Occupancy: ");
                        selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

                        roomRepo.Update(selectedRoom);

                        Console.WriteLine("Room has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Delete a room"):
                        List<Room> allRooms = roomRepo.GetAll();
                        foreach (Room r in allRooms)
                        {
                            Console.WriteLine($"{r.Id} {r.Name}");
                        }
                        Console.Write("Choose a room to delete");
                        int chosenRoomId = int.Parse(Console.ReadLine());
                        roomRepo.Delete(chosenRoomId);
                        Console.Write("Room absolutely destroyed, you monster. Press any key to get back to your ruins of a house: ");
                        Console.ReadKey();
                        break;
                    case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAll();
                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Id} {c.Name}");
                        }
                        Console.Write("Press any key you coward");
                        Console.ReadKey();
                        break;
                    case ("Search for a chore"):
                        Console.Write("Chore Id: ");
                        int choreId = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetById(choreId);

                        Console.WriteLine($"{chore.Id} - {chore.Name})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("See unassigned chores"):
                        List<Chore> unassignedChores = choreRepo.GetUnassignedChores();
                        foreach (Chore c in unassignedChores)
                        {
                            Console.WriteLine($"{c.Id} {c.Name}");
                        }
                        Console.Write("GTFO");
                        Console.ReadKey();
                        break;
                    case ("Add a chore"):
                        Console.Write("Chore name: ");
                        string newName = Console.ReadLine();

                        Chore choreToAdd = new Chore()
                        {
                            Name = newName
                        };

                        choreRepo.Insert(choreToAdd);

                        Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key, dick");
                        Console.ReadKey();
                        break;
                    case ("Assign a chore"):
                        List<Chore> choresToAssign = choreRepo.GetUnassignedChores();
                        foreach (Chore c in choresToAssign)
                        {
                            Console.WriteLine($"{c.Id} {c.Name}");
                        }
                        Console.Write("Which chore would you like to assign? ");
                        int chosenChore = int.Parse(Console.ReadLine());
                        List<Roommate> allRoommates = rmRepo.GetAll();
                        foreach (Roommate roommate in allRoommates)
                        {
                            Console.WriteLine($"{roommate.Id} {roommate.FirstName}");
                        }
                        Console.Write("Choose a roommate to assign to chore: ");
                        int chosenMate = int.Parse(Console.ReadLine());
                        choreRepo.AssignChore(chosenMate, chosenChore);
                        Console.WriteLine("Successfully assigned chore. Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Update a chore"):
                        List<Chore> choreOptions = choreRepo.GetAll();
                        foreach (Chore c in choreOptions)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }

                        Console.Write("Which chore would you like to update? ");
                        int selectedChoreId = int.Parse(Console.ReadLine());
                        Chore selectedChore = choreOptions.FirstOrDefault(c => c.Id == selectedChoreId);

                        Console.Write("New Name: ");
                        selectedChore.Name = Console.ReadLine();

                        choreRepo.UpdateChore(selectedChore);

                        Console.WriteLine("Chore has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Delete a chore"):
                        List<Chore> allChores = choreRepo.GetAll();
                        foreach (Chore c in allChores)
                        {
                            Console.WriteLine($"{c.Id} {c.Name}");
                        }
                        Console.Write("Choose a chore to delete");
                        int chosenChoreId = int.Parse(Console.ReadLine());
                        choreRepo.DeleteChore(chosenChoreId);
                        Console.Write("Chore deleted. Congrats, you slob: ");
                        Console.ReadKey();
                        break;
                    case ("Search for a roommate"):
                        Console.Write("Roommate Id: ");
                        int rmateId = int.Parse(Console.ReadLine());

                        Roommate rMate = rmRepo.GetById(rmateId);
                        Console.WriteLine($"{rMate.FirstName} {rMate.LastName} lives in {rMate.Room.Name} and their rent portion is {rMate.RentPortion}.");
                        Console.Write("Now get out of my room");
                        Console.ReadKey();
                        break;
                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }

        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Update a room",
                "Add a room",
                "Delete a room",
                "Show all chores",
                "Search for a chore",
                "See unassigned chores",
                "Add a chore",
                "Assign a chore",
                "Update a chore",
                "Delete a chore",
                "Search for a roommate",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}
