using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrackerSpese.Models;

public static class Functionality
{
    public static void Get()
    {
        string jsonPath = @"C:\Users\lucaa\source\repos\TrackerSpese\TrackerSpese\Data\Expenses.json";
        string content = File.ReadAllText(jsonPath);

        ExpenseList expensesList = JsonSerializer.Deserialize<ExpenseList>(content);

        Console.WriteLine("{0,-5} {1,-20} {2,-10} {3,-20}", "ID", "Descrizione", "Totale", "Aggiunto il");
        foreach (var expense in expensesList.Expenses)
        {
            Console.WriteLine("{0,-5} {1,-20} {2,-10} {3,-20}", expense.Id, expense.Description ?? "N/A", expense.Amount.ToString("0.00") + "€", expense.Date.ToString("dd/MM/yyyy"));
        }
        Console.WriteLine($"Totale di 💸: {expensesList.TotalAmount.ToString("0.00") + "€"}");
    }

    public static void Add(Double amount, string description)
    {
        string jsonPath = @"C:\Users\lucaa\source\repos\TrackerSpese\TrackerSpese\Data\Expenses.json";
        string content = File.ReadAllText(jsonPath);

        ExpenseList expenseList = JsonSerializer.Deserialize<ExpenseList>(content);

        var lastExpenseId = expenseList.Expenses.OrderBy(e => e.Id).Last().Id;
        Expenses expense = new Expenses()
        {
            Id = lastExpenseId + 1,
            Amount = amount,
            Date = DateTime.Now,
            Description = description
        };

        expenseList.Expenses.Add(expense);
        expenseList.TotalAmount += amount;

        string jsonString = JsonSerializer.Serialize(expenseList);
        File.WriteAllText(jsonPath, jsonString);

        Console.WriteLine("Pagamento Aggiunto Con Successo!!");
    }

    public static void Remove(int id)
    {
        string jsonPath = @"C:\Users\lucaa\source\repos\TrackerSpese\TrackerSpese\Data\Expenses.json";
        string content = File.ReadAllText(jsonPath);

        ExpenseList expenseList = JsonSerializer.Deserialize<ExpenseList>(content);
        Expenses expenseToRemove = expenseList.Expenses.FirstOrDefault(e => e.Id == id);
        expenseList.Expenses.Remove(expenseToRemove);

        expenseList.TotalAmount -= expenseToRemove.Amount;

        string jsonString = JsonSerializer.Serialize(expenseList);
        File.WriteAllText(jsonPath, jsonString);

        Console.WriteLine("Pagamento Rimosso Con Successo!!");
    }

    public static void FilterByMonth(int month)
    {
        string[] listOfMonth = new string[]
        {
            "Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno",
            "Luglio", "Agosto", "Settembre", "Ottobre", "Novembre", "Dicembre"
        };

        string jsonPath = @"C:\Users\lucaa\source\repos\TrackerSpese\TrackerSpese\Data\Expenses.json";
        string content = File.ReadAllText(jsonPath);

        Console.WriteLine($"\nMese Selezionato ({listOfMonth[month - 1]}) \n");

        ExpenseList expenseList = JsonSerializer.Deserialize<ExpenseList>(content);
        var filteredExpenses = expenseList.Expenses
            .Where(e => e.Date.Month == month)
            .ToList();

        Console.WriteLine("{0,-5} {1,-20} {2,-10} {3,-20}", "ID", "Descrizione", "Totale", "Aggiunto il");
        foreach (var expense in filteredExpenses)
        {
            Console.WriteLine("{0,-5} {1,-20} {2,-10:F2} {3,-20}", expense.Id, expense.Description ?? "N/A", expense.Amount.ToString("0.00") + "€", expense.Date.ToString("dd/MM/yyyy"));
        }
        Double totalFiltered = filteredExpenses.Sum(e => e.Amount);
        Double totalFilteredPlus = filteredExpenses.Where(e => e.Amount >= 0).Sum(e => e.Amount);
        Double totalFilteredNegative = filteredExpenses.Where(e => e.Amount <= 0).Sum(e => e.Amount);
        Console.WriteLine($"\nTotale di 💸 guadagnato ({listOfMonth[month - 1]}): {totalFilteredPlus:F2}" + "€");
        Console.WriteLine($"\nTotale di 💸 speso ({listOfMonth[month - 1]}): {totalFilteredNegative:F2}" + "€");
        Console.WriteLine($"\nTotale di 💸 alla fine del mese ({listOfMonth[month - 1]}): {totalFiltered:F2}" + "€");
    }

    public static void FilterByYear(int year)
    {
        string jsonPath = @"C:\Users\lucaa\source\repos\TrackerSpese\TrackerSpese\Data\Expenses.json";
        string content = File.ReadAllText(jsonPath);

        ExpenseList expenseList = JsonSerializer.Deserialize<ExpenseList>(content);
        var filteredExpenses = expenseList.Expenses
            .Where(e => e.Date.Year == year)
            .ToList();

        Console.WriteLine("{0,-5} {1,-20} {2,-10} {3,-20}", "ID", "Descrizione", "Totale", "Aggiunto il");
        foreach (var expense in filteredExpenses)
        {
            Console.WriteLine("{0,-5} {1,-20} {2,-10:F2} {3,-20}", expense.Id, expense.Description ?? "N/A", expense.Amount.ToString("0.00") + "€", expense.Date.ToString("dd/MM/yyyy"));
        }
        Double totalFiltered = filteredExpenses.Sum(e => e.Amount);
        Double totalFilteredPlus = filteredExpenses.Where(e => e.Amount >= 0).Sum(e => e.Amount);
        Double totalFilteredNegative = filteredExpenses.Where(e => e.Amount <= 0).Sum(e => e.Amount);
        Console.WriteLine($"\nTotale di 💸 speso ({year}): {totalFilteredPlus:F2}" + "€");
        Console.WriteLine($"\nTotal di 💸 speso ({year}): {totalFilteredNegative:F2}" + "€");
        Console.WriteLine($"\nTotale di 💸 alla fine dell'anno ({year}): {totalFiltered:F2}" + "€");
    }

    public static void SetTotalAmount(Double amount)
    {
        string jsonPath = @"C:\Users\lucaa\source\repos\TrackerSpese\TrackerSpese\Data\Expenses.json";
        string content = File.ReadAllText(jsonPath);

        ExpenseList expenseList = JsonSerializer.Deserialize<ExpenseList>(content);

        expenseList.TotalAmount = amount;

        string jsonString = JsonSerializer.Serialize(expenseList);
        File.WriteAllText(jsonPath, jsonString);

        Console.WriteLine($"\nNuovo Totale di 💸: {amount.ToString()}" + "€");
    }
}

