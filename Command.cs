using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Command
{
	public string Keys { get; set; }
	public IMenuItem Item { get; set; }
	public Action Action { get; set; }
}

