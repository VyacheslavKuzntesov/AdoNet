using AdoNet;

var dbService = new StudentsVisitationService();
dbService.CreateTable();
dbService.AddVisit(1, "Kate", new DateOnly(2000, 1, 1));
Console.WriteLine("DONE");