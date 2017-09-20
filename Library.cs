using System;
using System.Collections.Generic;
using System.Linq;

public class Library
{
    public List<ILibraryItem> Items = new List<ILibraryItem>();

	private IMenuItem exitItem;
	private IMenuItem returnItem;


	public Library()
	{
		Items.Add(new Book() { Name = "HeadFirst with C#", Author = "Andrew Stellman", Available = true });
		Items.Add(new Book() { Name = "Mastering the Console App", Author = "Anonymous", Available = true });
		Items.Add(new Book() { Name = "C# Game Programming: For Serious Game Creation", Author = "Daniel Schuller", Available = true });
		Items.Add(new Book() { Name = "Pro C# 5.0 and the .NET 4.5 Framework", Author = "Andrew Troelsen", Available = true });
		Items.Add(new Video() { Name = "Spaceballs", Available = true });

		exitItem = new MenuItem() { Name = "Exit" };
		returnItem = new MenuItem() { Name = "Return Item" };

		listAvailableItems();
	}

	private void listAvailableItems()
	{
		var commands = new List<Command>
		{
			new Command { Keys = "r", Item = returnItem, Action = () => {  } },
			new Command { Keys = "x", Item = exitItem, Action = () => { Environment.Exit(0); } }
		};

		var counter = 1;

		foreach(var item in Items.Where(i => i.Available == true))
		{
			var command = new Command {
				Keys = counter.ToString(),
				Item = item
			};

			command.Action = () => { checkoutItem((ILibraryItem)command.Item); };
			commands.Add(command);

			counter++;
		}

		showMenu("Available Items", commands);
	}

	private void showMenu(string title, List<Command> commands)
	{
		Command action = null;
		string info = "";

		while (true)
		{
			display(title, info, commands);

			var choice = Console.ReadLine();

			action = commands.FirstOrDefault(c => c.Keys == choice);

			if (action == null) info = "Invalid Choice!";
			else
			{
				action.Action();
			}
		}		
	}
	private void display(string title, string info, List<Command> commands)
	{
		Console.Clear();
		Console.WriteLine("");
		Console.WriteLine(title);
		Console.WriteLine(new String('-', title.Length));

		Console.WriteLine("");
		Console.WriteLine("Actions");
		Console.WriteLine(new String('-', "Actions".Length));

		foreach (var command in commands.OrderBy(c => c.Keys))
		{

			var description = "";

			if (command.Item is ILibraryItem)
			{
				var item = (ILibraryItem)command.Item;

				description = command.Item.Name + (item.Available ? " (Available)" : " (Not Available)");
			}
			else {
				description = command.Item.Name;
			}

			Console.WriteLine($"{command.Keys} = {description}");
		}

		Console.WriteLine("");

		if(info.Length > 0) Console.WriteLine(info);

		Console.Write("Choose an action: ");
	}

	private void checkoutItem(ILibraryItem item)
	{
		Console.WriteLine($"Checking out {item.Name}");
		item.Available = false;
		listAvailableItems();
	}
}