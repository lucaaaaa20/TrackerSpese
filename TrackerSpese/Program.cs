// See https://aka.ms/new-console-template for more information
using System.CommandLine;
using System.Text.Json;
using TrackerSpese.Models;

var rootCommand = new RootCommand("App di gestione");

//----------------------------------------------------------------------------------------------------------------------------------------------------
//ADD
var addCommand = new Command("add", "Aggiunge una nuova spesa")
{
    new Option<Double>("--amount", "Importo della spesa") { IsRequired = true },
    new Option<string>("--description", "Descrizione della spesa") { IsRequired = true }
};

addCommand.SetHandler((Double amount, string description) =>
{
    Console.WriteLine($"✅ Aggiunto: {description} | {amount}€");
    Functionality.Add(amount, description);
}, addCommand.Options[0] as Option<Double>, addCommand.Options[1] as Option<string>);

//----------------------------------------------------------------------------------------------------------------------------------------------------
//REMOVE
var removeCommand = new Command("remove", "Rimuove una spesa")
{
    new Option<int>("--id", "ID della spesa da rimuovere") { IsRequired = true }
};

removeCommand.SetHandler((int id) =>
{
    Functionality.Remove(id);
    Console.WriteLine($"❌ Rimosso l'elemento con ID: {id}");
}, removeCommand.Options[0] as Option<int>);

//----------------------------------------------------------------------------------------------------------------------------------------------------
//SHOW ALL
var showCommand = new Command("show", "Mostra le informazioni")
{

};

showCommand.SetHandler(() =>
{
    Functionality.Get();
});

//----------------------------------------------------------------------------------------------------------------------------------------------------
//FILTER BY MONTH
var filterByMonthCommand = new Command("showMonth", "Mostra le informazioni")
{
    new Option<int>("--month", "Numero del mese per cui si vuole filtrare") { IsRequired = true }
};

filterByMonthCommand.SetHandler((int month) =>
{
    Functionality.FilterByMonth(month);
}, filterByMonthCommand.Options[0] as Option<int>);

//----------------------------------------------------------------------------------------------------------------------------------------------------
//FILTER BY YEAR
var filterByYearCommand = new Command("showYear", "Mostra le informazioni")
{
    new Option<int>("--year", "Numero del mese per cui si vuole filtrare") { IsRequired = true }
};

filterByYearCommand.SetHandler((int year) =>
{
    Functionality.FilterByYear(year);
}, filterByYearCommand.Options[0] as Option<int>);

//----------------------------------------------------------------------------------------------------------------------------------------------------
//SET TOTAL AMOUNT
var setTotalAmountCommand = new Command("totalAmount", "Mostra le informazioni")
{
    new Option<Double>("--amount", "Imposta il totale") { IsRequired = true }
};

setTotalAmountCommand.SetHandler((Double amount) =>
{
    Functionality.SetTotalAmount(amount);
}, setTotalAmountCommand.Options[0] as Option<Double>);


rootCommand.AddCommand(addCommand);
rootCommand.AddCommand(removeCommand);
rootCommand.AddCommand(showCommand);
rootCommand.AddCommand(filterByMonthCommand);
rootCommand.AddCommand(filterByYearCommand);
rootCommand.AddCommand(setTotalAmountCommand);

return await rootCommand.InvokeAsync(args);